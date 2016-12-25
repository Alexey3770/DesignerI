using Designer.Domain.Entity;
using Designer.Foundation;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Designer.Domain.Mediator
{
    /// <summary>
    /// Класс предназначен для сохранения и загрузке информации о комнатах созданных пользователем
    /// </summary>
    class SaveData
    {
        const string NAME_ROOM_TABLE = "Room";
        const string NAME_FURS_LOC_TABLE = "LocationFurniture";
        const string NAME_FURS_TABLE = "Furniture";
        const string NAME_USER_TABLE = "[User]";
        const string NAME_TEXTURE_FLOOR_TABLE = "FloorCover";
        const string NAME_TEXTURE_SIDE_TABLE = "SideCover";
        const string NAME_SIDE_TABLE = "Wall";
        const string NAME_FLOOR_TABLE = "Floor";

        DBAccess dbaccess;
        int idUser = -1;

        public SaveData()
        {
            dbaccess = new DBAccess();
            InitId();
        }
        private void InitId()
        {
            DataTable t = dbaccess.GetData(NAME_USER_TABLE);
            string s = "";
            if (File.Exists("id"))            
                s = File.ReadAllText("id");            
            else
            {
                s = Convert.ToString(Guid.NewGuid());
                File.WriteAllText("id",s);
               
            }
            for(int i = 0;i < t.Rows.Count; i++)            
                if((string)t.Rows[i][1] == s)
                {
                    idUser = (int)t.Rows[i][0];
                    break;
                }
            
            if(idUser == -1)
            {
                dbaccess.InsertCommand(string.Format("INSERT INTO {0}(guid) VALUES({1})",NAME_USER_TABLE,s));
                for (int i = 0;i < t.Rows.Count; i++)            
                    if((string)t.Rows[i][1] == s)
                    {
                        idUser = (int)t.Rows[i][0];
                        break;
                    }
            }
        }
        /// <summary>
        /// Загрузка комнаты
        /// </summary>
        /// <param name="name">Название комнаты</param>
        /// <returns></returns>
        public Room LoadRoom(string name)
        {
            DataTable tableRoom = dbaccess.GetData(NAME_ROOM_TABLE);
            
            DataTable tableFloor = dbaccess.GetData(NAME_FLOOR_TABLE);
            
            DataTable tableTextFloor = dbaccess.GetData(NAME_TEXTURE_FLOOR_TABLE);
            for (int i = 0; i < tableRoom.Rows.Count; i++)            
                if ((string)tableRoom.Rows[i][1] == name)
                {
                    Room room = new Room((string)tableRoom.Rows[i][2], (int)tableRoom.Rows[i][3], (int)tableRoom.Rows[i][4]);
                    AddSideTexture(room, (string)tableRoom.Rows[i][0]);
                    AddFloorTexture(room, (string)tableRoom.Rows[i][0]);
                    LoadFurniture(room,(string)tableRoom.Rows[i][0]);
                    return room;
                }

            return null;
            
        }
        private void AddSideTexture(Room planSideRoom, string roomId)
        {
            DataTable tableSide = dbaccess.GetData(NAME_SIDE_TABLE);
            DataTable tableTextSide = dbaccess.GetData(NAME_TEXTURE_SIDE_TABLE);
            for(int i = 0;i < tableSide.Rows.Count; i++)
            {
                if((string)tableSide.Rows[i][1] == roomId)
                {
                    for (int g = 0; g < tableTextSide.Rows.Count; g++)
                        if ((int)tableTextSide.Rows[g][0] == (int)tableSide.Rows[i][2])
                        {
                            planSideRoom.AddTexture((string)tableTextSide.Rows[g][2], "side");
                            return;
                        }
                }
            }
        }
        private void AddFloorTexture(Room planSideRoom, string roomId)
        {
            DataTable tableFloor = dbaccess.GetData(NAME_FLOOR_TABLE);
            DataTable tableTextFloor = dbaccess.GetData(NAME_TEXTURE_FLOOR_TABLE);
            for (int i = 0; i < tableFloor.Rows.Count; i++)
            {
                if ((string)tableFloor.Rows[i][1] == roomId)
                {
                    for (int g = 0; g < tableTextFloor.Rows.Count; g++)
                        if ((int)tableTextFloor.Rows[g][0] == (int)tableFloor.Rows[i][2])
                        {
                            planSideRoom.AddTexture((string)tableTextFloor.Rows[g][2], "floor");
                            return;
                        }
                }
            }
        }

        private void LoadFurniture(Room planSideRoom,string roomId)
        {
            DataTable tableFursLoc = dbaccess.GetData(NAME_FURS_LOC_TABLE);
            DataTable tableFur = dbaccess.GetData(NAME_FURS_TABLE);
            for (int i = 0; i < tableFursLoc.Rows.Count; i++)
                if ((string)tableFursLoc.Rows[i][4] == roomId)
                    for (int g = 0; g < tableFur.Rows.Count; g++)
                        if ((int)tableFur.Rows[g][0] == (int)tableFursLoc.Rows[i][3])
                        {
                            Furniture f = new Furniture((int)tableFur.Rows[g][0], (string)tableFur.Rows[g][1], (string)tableFur.Rows[g][2], (string)tableFur.Rows[g][3], (string)tableFur.Rows[g][4], (float)tableFur.Rows[g][5], (float)tableFur.Rows[g][6], (float)tableFur.Rows[g][7], (float)tableFur.Rows[g][9]);
                            f.Position = new System.Drawing.PointF((float)tableFursLoc.Rows[i][1], (float)tableFursLoc.Rows[i][2]);
                            f.Angle = (float)tableFursLoc.Rows[i][5];
                            planSideRoom.AddFurniture(f);
                            
                        }
        }

        /// <summary>
        /// Сохранение комнаты
        /// </summary>
        /// <param name="room">Ссылка на комнату</param>
        /// <param name="name">Название комнаты</param>
        /// <returns></returns>
        public bool SaveRoom(Room room,string name)
        {
            DataTable t = dbaccess.GetData(NAME_USER_TABLE);
            
            int idTextWall = 0;
            int idTExtFloor = 0;
            
            t = dbaccess.GetData(NAME_TEXTURE_SIDE_TABLE);
            for (int i = 0; i < t.Rows.Count; i++)
                if ((string)t.Rows[i][2] == room.PathTextSide)
                {
                    idTextWall = (int)t.Rows[i][0];
                    break;
                }
            t = dbaccess.GetData(NAME_TEXTURE_FLOOR_TABLE);
            for (int i = 0; i < t.Rows.Count; i++)
                if ((string)t.Rows[i][2] == room.PathTextFloor)
                {
                    idTExtFloor = (int)t.Rows[i][0];
                    break;
                }
            Guid g = Guid.NewGuid();
            List<string> ins = new List<string>();
            ins.Add(string.Format("INSERT INTO {0}(Id, name, type, width, length, idUser) VALUES('{1}', '{2}', '{3}', {4}, {5},{6});",NAME_ROOM_TABLE,g, name,room.Type,room.Width,room.Length,idUser));
            ins.Add(string.Format("INSERT INTO {0}(room_wall, text_wall) VALUES('{1}', {2});",NAME_SIDE_TABLE,g,idTextWall));
            ins.Add(string.Format("INSERT INTO {0}(room_floor, textur_floor) VALUES('{1}', {2});", NAME_FLOOR_TABLE, g, idTExtFloor));
            for (int i = 0;i < room.Furnitures.Count;i++)
                ins.Add(string.Format("INSERT INTO {0}(x, y, Loc_fur, Loc_room, angle) VALUES({1}, {2}, {3}, '{4}',{5})",NAME_FURS_LOC_TABLE,room.Furnitures[i].Position.X, room.Furnitures[i].Position.Y,room.Furnitures[i].ID,g,room.Furnitures[i].Angle));
            return dbaccess.InsertTransaction(ins);
        }
        /// <summary>
        /// Получение комнат созданных пользователе
        /// </summary>
        /// <returns></returns>
        public List<string> GetRooms()
        {
            DataTable tableRoom = dbaccess.GetData(NAME_ROOM_TABLE);
            List<string> rooms = new List<string>();
            for(int i = 0;i < tableRoom.Rows.Count;i++)
                if((int)tableRoom.Rows[i][5] == idUser)
                {
                    rooms.Add((string)tableRoom.Rows[i][1]);
                }
            return rooms;
        }
    }
}
