using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using System.IO;
using System.Drawing;

namespace Designer.Domain.Entity
{
    /// <summary>
    /// Класс содержит информацию необходимую для отображение 3д модели
    /// </summary>
    public class Object3D
    {
        public enum Axis
        {
            X,Y,Z
        }
        /// <summary>
        /// Положение модели в пространстве
        /// </summary>
        public Point3D Position
        {
            get { return position; }
            set { position = value; }
        }
        /// <summary>
        /// Размеры 3д модели
        /// </summary>
        //public SizeF Size
        //{
        //    get { if (size.IsEmpty) size = GetSize(); return size; }
        //}
        /// <summary>
        /// Масштаб 3д модели
        /// </summary>
        public float Scale
        {
            get;
            set;
        }
        SizeF size;
        #region Private Fields
        /// <summary>
        /// Произведена ли загрузка (флаг)
        /// </summary>
        protected bool isLoad = false;
        /// <summary>
        /// Счетчик по-объектов
        /// </summary>
        protected int count_limbs;
        /// <summary>
        /// Переменная для номера текстуры
        /// </summary>
        int mat_nom = 0;
        /// <summary>
        /// Номер дисплейного списка с данной моделью
        /// </summary>
        protected int thisList = 0;
        /// <summary>
        /// Данная переменная будет указывать на количество прочитанных символов в строке при чтении информации из файла
        /// </summary>
        int GlobalStringFrom = 0;
        /// <summary>
        /// Массив под-объектов
        /// </summary>
        protected LIMB[] limbs = null;
        /// <summary>
        /// Массив для хранения текстур
        /// </summary>
        TexturesForObjects[] text_objects = null;
        TexturesForObjects text_object;
        Point3D basePos;
        // описание ориентации модели
        //Model3D_Prop coord = new Model3D_Prop();
        /// <summary>
        /// Описание позиции модели в пространстве
        /// </summary>
        Point3D position;
        #endregion

        public Object3D()
        {
            position = new Point3D(0, 0, 0);
            basePos = new Point3D(0, 0, 0);
            Scale = 1;
        }

        public Object3D(Point3D position) : this()
        {
            this.position = position;
            Scale = 1;
        }

        /// <summary>
        /// Перемещение 3Д модели
        /// </summary>
        /// <param name="dx">Сдвиг по X</param>
        /// <param name="dy">Сдвиг по Y</param>
        /// <param name="dz">Сдвиг по Z</param>
        public void Move(float dx, float dy, float dz)
        {
            position.X += dx;
            position.Y += dy;
            position.Z += dz;
        }

        /// <summary>
        /// Вращение 3д модели
        /// </summary>
        /// <param name="da">Угол, на который необходимо повернуть модель</param>
        /// <param name="ax">Ось, вокруг которой необходимо вращать</param>
        public void Rotate(float da,Axis ax)
        {
            switch (ax)
            {
                case Axis.X: position.AngleAxisX += da; break;
                case Axis.Y: position.AngleAxisY += da; break;
                case Axis.Z: position.AngleAxisZ += da; break;
            }
        }

        public void AddTexture(string name)
        {
            text_object = new TexturesForObjects();
            text_object.LoadTextureForModel(name);
            CreateList();
        }

        #region Load Model
        public int LoadModelFromPath(string path, int mashtabX = 1)
        {
            // модель может содержать до 256 под-объектов
            limbs = new LIMB[256];
            // счетчик скинут
            int limb_ = -1;

            //StreamReader sw = File.OpenText("Models//"+FileName + ".ase");

            // начинаем чтение файла

            StreamReader sw = new StreamReader(path);

            // временные буферы
            string a_buff = "";
            string b_buff = "";
            string c_buff = "";
            float maxOffsetX = 0, maxOffsetY = 0, maxOffsetZ = 0;
            // счетчики вершин и полигонов
            int ver = 0, fac = 0;

            // если строка успешно прочитана
            while ((a_buff = sw.ReadLine()) != null)
            {
                // получаем первое слово
                b_buff = GetFirstWord(a_buff, 0);
                if (b_buff[0] == '*') // определеям, является ли первый символ звездочкой
                {
                    switch (b_buff) // если да, то проверяем какое управляющее слово содержится в первом прочитаном слове
                    {
                        case "*MATERIAL_COUNT": // счетчик материалов
                            {
                                // получаем первое слово от символа указанного в GlobalStringFrom
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                int mat = System.Convert.ToInt32(c_buff);
                                text_objects = new TexturesForObjects[mat];
                                // создаем объект для текстуры в памяти
                                continue;
                            }

                        case "*MATERIAL_REF": // номер текстуры
                            {
                                // записываем для текущего под-объекта номер текстуры
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                int mat_ref = System.Convert.ToInt32(c_buff);

                                // устанавливаем номер материала, соответствующий данной модели.
                                limbs[limb_].SetMaterialNom(mat_ref);
                                continue;
                            }

                        case "*MATERIAL": // указание на материал
                            {
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                mat_nom = System.Convert.ToInt32(c_buff);
                                continue;
                            }

                        case "*GEOMOBJECT": // начинается описание геметрии под-объекта
                            {
                                limb_++; // записываем в счетчик под-объектов
                                continue;
                            }

                        case "*MESH_NUMVERTEX": // количесвто вершин в под-объекте
                            {
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                ver = System.Convert.ToInt32(c_buff);
                                continue;
                            }

                        case "*MESH_NUMTVERTEX": // количество текстурных координат, данное слово говорит о наличии текстурных координат - следовательно мы должны выделить память для них
                            {
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                if (limbs[limb_] != null)
                                {
                                    limbs[limb_].СreateTextureVertexMem(System.Convert.ToInt32(c_buff));
                                }
                                continue;
                            }

                        case "*MESH_NUMTVFACES":  // память для текстурных координат (faces)
                            {
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);

                                if (limbs[limb_] != null)
                                {
                                    // выделяем память для текстурныйх координат
                                    limbs[limb_].СreateTextureFaceMem(System.Convert.ToInt32(c_buff));
                                }
                                continue;
                            }
                        case "*BITMAP": // имя текстуры
                            {
                                    c_buff = ""; // обнуляем временный буффер

                                    for (int ax = GlobalStringFrom + 2; ax < a_buff.Length - 1; ax++)
                                        c_buff += a_buff[ax]; // считываем имя текстуры

                                    text_objects[mat_nom] = new TexturesForObjects(); // новый объект для текстуры

                                    text_objects[mat_nom].LoadTextureForModel(c_buff); // загружаем текстуру
                                
                                continue;
                            }
                        case "*MESH_NUMFACES": // количество полиговов в под-объекте
                            {
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                fac = System.Convert.ToInt32(c_buff);

                                // если было объвляющее слово *GEOMOBJECT (гарантия выполнения условия limb_ > -1) и были указаны количство вершин
                                if (limb_ > -1 && ver > -1 && fac > -1)
                                {
                                    // создаем новый под-объект в памяти
                                    limbs[limb_] = new LIMB(ver, fac);
                                }
                                else
                                {
                                    // иначе завершаем неудачей
                                    return -1;
                                }
                                continue;
                            }

                        case "*MESH_VERTEX": // информация о вершине
                            {
                                // под-объект создан в памяти
                                if (limb_ == -1)
                                    return -2;
                                if (limbs[limb_] == null)
                                    return -3;

                                string a1 = "", a2 = "", a3 = "", a4 = "";
                                
                                // полчучаем информацию о кооринатах и номере вершины
                                // (получаем все слова в строке)
                                a1 = GetFirstWord(a_buff, GlobalStringFrom);
                                a2 = GetFirstWord(a_buff, GlobalStringFrom);
                                a3 = GetFirstWord(a_buff, GlobalStringFrom);
                                a4 = GetFirstWord(a_buff, GlobalStringFrom);

                                // преобразовываем в целое цисло
                                int NomVertex = System.Convert.ToInt32(a1);

                                // заменяем точки в представлении числа с плавающей точкой, на запятые, чтобы правильно выполнилась функция 
                                // преобразования строки в дробное число
                                a2 = a2.Replace('.', ',');
                                a3 = a3.Replace('.', ',');
                                a4 = a4.Replace('.', ',');

                                // записываем информацию о вершине
                                limbs[limb_].Vert[0, NomVertex] = (float)System.Convert.ToDouble(a2) * mashtabX; // x
                                limbs[limb_].Vert[1, NomVertex] = (float)System.Convert.ToDouble(a3); // y
                                limbs[limb_].Vert[2, NomVertex] = (float)System.Convert.ToDouble(a4); // z

                                if (maxOffsetX == 0)
                                    maxOffsetX = limbs[limb_].Vert[0, NomVertex];

                                if(maxOffsetY == 0)
                                    maxOffsetY = limbs[limb_].Vert[1, NomVertex];

                                if (maxOffsetZ == 0)
                                    maxOffsetZ = limbs[limb_].Vert[2, NomVertex];

                                if (maxOffsetX > limbs[limb_].Vert[0, NomVertex])
                                    maxOffsetX = limbs[limb_].Vert[0, NomVertex];

                                if (maxOffsetY > limbs[limb_].Vert[1, NomVertex])
                                    maxOffsetY = limbs[limb_].Vert[1, NomVertex];

                                if (maxOffsetZ > limbs[limb_].Vert[2, NomVertex])
                                    maxOffsetZ = limbs[limb_].Vert[2, NomVertex];
                                continue;

                            }

                        case "*MESH_FACE": // информация о полигоне
                            {
                                // под-объект создан в памяти
                                if (limb_ == -1)
                                    return -2;
                                if (limbs[limb_] == null)
                                    return -3;

                                // временные перменные
                                string a1 = "", a2 = "", a3 = "", a4 = "", a5 = "", a6 = "", a7 = "";

                                // получаем все слова в строке
                                a1 = GetFirstWord(a_buff, GlobalStringFrom);
                                a2 = GetFirstWord(a_buff, GlobalStringFrom);
                                a3 = GetFirstWord(a_buff, GlobalStringFrom);
                                a4 = GetFirstWord(a_buff, GlobalStringFrom);
                                a5 = GetFirstWord(a_buff, GlobalStringFrom);
                                a6 = GetFirstWord(a_buff, GlobalStringFrom);
                                a7 = GetFirstWord(a_buff, GlobalStringFrom);

                                // получаем нмоер полигона из первого слова в строке, заменив последний символ ":" после номера на флаг окончания строки.
                                int NomFace = System.Convert.ToInt32(a1.Replace(':', '\0'));

                                // записываем номера вершин, которые нас интересуют
                                limbs[limb_].Face[0, NomFace] = System.Convert.ToInt32(a3);
                                limbs[limb_].Face[1, NomFace] = System.Convert.ToInt32(a5);
                                limbs[limb_].Face[2, NomFace] = System.Convert.ToInt32(a7);

                                continue;

                            }

                        // текстурые координаты
                        case "*MESH_TVERT":
                            {
                                // под-объект создан в памяти
                                if (limb_ == -1)
                                    return -2;
                                if (limbs[limb_] == null)
                                    return -3;

                                // временные перменные
                                string a1 = "", a2 = "", a3 = "", a4 = "";

                                // получаем все слова в строке
                                a1 = GetFirstWord(a_buff, GlobalStringFrom);
                                a2 = GetFirstWord(a_buff, GlobalStringFrom);
                                a3 = GetFirstWord(a_buff, GlobalStringFrom);
                                a4 = GetFirstWord(a_buff, GlobalStringFrom);

                                // преобразуем первое слово в номер вершины
                                int NomVertex = System.Convert.ToInt32(a1);

                                // заменяем точки в представлении числа с плавающей точкой, на запятые, чтобы правильно выполнилась функция 
                                // преобразования строки в дробное число
                                a2 = a2.Replace('.', ',');
                                a3 = a3.Replace('.', ',');
                                a4 = a4.Replace('.', ',');

                                // записываем значение вершины
                                limbs[limb_].T_vert[0, NomVertex] = (float)System.Convert.ToDouble(a2); // x
                                limbs[limb_].T_vert[1, NomVertex] = (float)System.Convert.ToDouble(a3); // y
                                limbs[limb_].T_vert[2, NomVertex] = (float)System.Convert.ToDouble(a4); // z

                                continue;

                            }

                        // привязка текстурных координат к полигонам
                        case "*MESH_TFACE":
                            {
                                // под-объект создан в памяти
                                if (limb_ == -1)
                                    return -2;
                                if (limbs[limb_] == null)
                                    return -3;

                                // временные перменные
                                string a1 = "", a2 = "", a3 = "", a4 = "";

                                // получаем все слова в строке
                                a1 = GetFirstWord(a_buff, GlobalStringFrom);
                                a2 = GetFirstWord(a_buff, GlobalStringFrom);
                                a3 = GetFirstWord(a_buff, GlobalStringFrom);
                                a4 = GetFirstWord(a_buff, GlobalStringFrom);

                                // преобразуем первое слово в номер полигона
                                int NomFace = System.Convert.ToInt32(a1);

                                // записываем номера вершин, которые опиывают полигон
                                limbs[limb_].T_face[0, NomFace] = System.Convert.ToInt32(a2);
                                limbs[limb_].T_face[1, NomFace] = System.Convert.ToInt32(a3);
                                limbs[limb_].T_face[2, NomFace] = System.Convert.ToInt32(a4);

                                continue;

                            }


                    }



                }



            }
            // пересохраняем количесвто полигонов
            count_limbs = limb_;
            basePos = new Point3D(-maxOffsetX, -maxOffsetY,-maxOffsetZ);
            sw.Close();

            // отрисовываем геометрию
            CreateListForOth();


            return 0;
        }
        /// <summary>
        /// Загрузка модели
        /// </summary>
        /// <param name="txtmodel">Имя загружаемого файла</param>
        /// <returns></returns>
        public virtual int LoadModel(string text,int mashtabX = 1)
        {
            text_object = new TexturesForObjects();
            text_object.LoadTextureForModel("Textures//base.jpg");
            // модель может содержать до 256 под-объектов
            limbs = new LIMB[256];
            // счетчик скинут
            int limb_ = -1;

            //StreamReader sw = File.OpenText("Models//"+FileName + ".ase");

            // начинаем чтение файла

            StringReader sw = new StringReader(text);

            // временные буферы
            string a_buff = "";
            string b_buff = "";
            string c_buff = "";

            // счетчики вершин и полигонов
            int ver = 0, fac = 0;

            // если строка успешно прочитана
            while ((a_buff = sw.ReadLine()) != null)
            {
                // получаем первое слово
                b_buff = GetFirstWord(a_buff, 0);
                if (b_buff[0] == '*') // определеям, является ли первый символ звездочкой
                {
                    switch (b_buff) // если да, то проверяем какое управляющее слово содержится в первом прочитаном слове
                    {
                        case "*MATERIAL_COUNT": // счетчик материалов
                            {
                                // получаем первое слово от символа указанного в GlobalStringFrom
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                int mat = System.Convert.ToInt32(c_buff);

                                // создаем объект для текстуры в памяти
                                continue;
                            }

                        case "*MATERIAL_REF": // номер текстуры
                            {
                                // записываем для текущего под-объекта номер текстуры
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                int mat_ref = System.Convert.ToInt32(c_buff);

                                // устанавливаем номер материала, соответствующий данной модели.
                                limbs[limb_].SetMaterialNom(mat_ref);
                                continue;
                            }

                        case "*MATERIAL": // указание на материал
                            {
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                mat_nom = System.Convert.ToInt32(c_buff);
                                continue;
                            }

                        case "*GEOMOBJECT": // начинается описание геметрии под-объекта
                            {
                                limb_++; // записываем в счетчик под-объектов
                                continue;
                            }

                        case "*MESH_NUMVERTEX": // количесвто вершин в под-объекте
                            {
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                ver = System.Convert.ToInt32(c_buff);
                                continue;
                            }
                            
                        case "*MESH_NUMTVERTEX": // количество текстурных координат, данное слово говорит о наличии текстурных координат - следовательно мы должны выделить память для них
                            {
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                if (limbs[limb_] != null)
                                {
                                    limbs[limb_].СreateTextureVertexMem(System.Convert.ToInt32(c_buff));
                                }
                                continue;
                            }

                        case "*MESH_NUMTVFACES":  // память для текстурных координат (faces)
                            {
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);

                                if (limbs[limb_] != null)
                                {
                                    // выделяем память для текстурныйх координат
                                    limbs[limb_].СreateTextureFaceMem(System.Convert.ToInt32(c_buff));
                                }
                                continue;
                            }

                        case "*MESH_NUMFACES": // количество полиговов в под-объекте
                            {
                                c_buff = GetFirstWord(a_buff, GlobalStringFrom);
                                fac = System.Convert.ToInt32(c_buff);

                                // если было объвляющее слово *GEOMOBJECT (гарантия выполнения условия limb_ > -1) и были указаны количство вершин
                                if (limb_ > -1 && ver > -1 && fac > -1)
                                {
                                    // создаем новый под-объект в памяти
                                    limbs[limb_] = new LIMB(ver, fac);
                                }
                                else
                                {
                                    // иначе завершаем неудачей
                                    return -1;
                                }
                                continue;
                            }

                        case "*MESH_VERTEX": // информация о вершине
                            {
                                    // под-объект создан в памяти
                                    if (limb_ == -1)
                                        return -2;
                                    if (limbs[limb_] == null)
                                        return -3;

                                    string a1 = "", a2 = "", a3 = "", a4 = "";

                                    // полчучаем информацию о кооринатах и номере вершины
                                    // (получаем все слова в строке)
                                    a1 = GetFirstWord(a_buff, GlobalStringFrom);
                                    a2 = GetFirstWord(a_buff, GlobalStringFrom);
                                    a3 = GetFirstWord(a_buff, GlobalStringFrom);
                                    a4 = GetFirstWord(a_buff, GlobalStringFrom);

                                    // преобразовываем в целое цисло
                                    int NomVertex = System.Convert.ToInt32(a1);

                                    // заменяем точки в представлении числа с плавающей точкой, на запятые, чтобы правильно выполнилась функция 
                                    // преобразования строки в дробное число
                                    a2 = a2.Replace('.', ',');
                                    a3 = a3.Replace('.', ',');
                                    a4 = a4.Replace('.', ',');

                                    // записываем информацию о вершине
                                    limbs[limb_].Vert[0, NomVertex] = (float)System.Convert.ToDouble(a2) * mashtabX; // x
                                    limbs[limb_].Vert[1, NomVertex] = (float)System.Convert.ToDouble(a3); // y
                                    limbs[limb_].Vert[2, NomVertex] = (float)System.Convert.ToDouble(a4); // z
                                


                                continue;

                            }

                        case "*MESH_FACE": // информация о полигоне
                            {
                                // под-объект создан в памяти
                                if (limb_ == -1)
                                    return -2;
                                if (limbs[limb_] == null)
                                    return -3;

                                // временные перменные
                                string a1 = "", a2 = "", a3 = "", a4 = "", a5 = "", a6 = "", a7 = "";

                                // получаем все слова в строке
                                a1 = GetFirstWord(a_buff, GlobalStringFrom);
                                a2 = GetFirstWord(a_buff, GlobalStringFrom);
                                a3 = GetFirstWord(a_buff, GlobalStringFrom);
                                a4 = GetFirstWord(a_buff, GlobalStringFrom);
                                a5 = GetFirstWord(a_buff, GlobalStringFrom);
                                a6 = GetFirstWord(a_buff, GlobalStringFrom);
                                a7 = GetFirstWord(a_buff, GlobalStringFrom);

                                // получаем нмоер полигона из первого слова в строке, заменив последний символ ":" после номера на флаг окончания строки.
                                int NomFace = System.Convert.ToInt32(a1.Replace(':', '\0'));

                                // записываем номера вершин, которые нас интересуют
                                limbs[limb_].Face[0, NomFace] = System.Convert.ToInt32(a3);
                                limbs[limb_].Face[1, NomFace] = System.Convert.ToInt32(a5);
                                limbs[limb_].Face[2, NomFace] = System.Convert.ToInt32(a7);

                                continue;

                            }

                        // текстурые координаты
                        case "*MESH_TVERT":
                            {
                                // под-объект создан в памяти
                                if (limb_ == -1)
                                    return -2;
                                if (limbs[limb_] == null)
                                    return -3;

                                // временные перменные
                                string a1 = "", a2 = "", a3 = "", a4 = "";

                                // получаем все слова в строке
                                a1 = GetFirstWord(a_buff, GlobalStringFrom);
                                a2 = GetFirstWord(a_buff, GlobalStringFrom);
                                a3 = GetFirstWord(a_buff, GlobalStringFrom);
                                a4 = GetFirstWord(a_buff, GlobalStringFrom);

                                // преобразуем первое слово в номер вершины
                                int NomVertex = System.Convert.ToInt32(a1);

                                // заменяем точки в представлении числа с плавающей точкой, на запятые, чтобы правильно выполнилась функция 
                                // преобразования строки в дробное число
                                a2 = a2.Replace('.', ',');
                                a3 = a3.Replace('.', ',');
                                a4 = a4.Replace('.', ',');

                                // записываем значение вершины
                                limbs[limb_].T_vert[0, NomVertex] = (float)System.Convert.ToDouble(a2); // x
                                limbs[limb_].T_vert[1, NomVertex] = (float)System.Convert.ToDouble(a3); // y
                                limbs[limb_].T_vert[2, NomVertex] = (float)System.Convert.ToDouble(a4); // z

                                continue;

                            }

                        // привязка текстурных координат к полигонам
                        case "*MESH_TFACE":
                            {
                                // под-объект создан в памяти
                                if (limb_ == -1)
                                    return -2;
                                if (limbs[limb_] == null)
                                    return -3;

                                // временные перменные
                                string a1 = "", a2 = "", a3 = "", a4 = "";

                                // получаем все слова в строке
                                a1 = GetFirstWord(a_buff, GlobalStringFrom);
                                a2 = GetFirstWord(a_buff, GlobalStringFrom);
                                a3 = GetFirstWord(a_buff, GlobalStringFrom);
                                a4 = GetFirstWord(a_buff, GlobalStringFrom);

                                // преобразуем первое слово в номер полигона
                                int NomFace = System.Convert.ToInt32(a1);

                                // записываем номера вершин, которые опиывают полигон
                                limbs[limb_].T_face[0, NomFace] = System.Convert.ToInt32(a2);
                                limbs[limb_].T_face[1, NomFace] = System.Convert.ToInt32(a3);
                                limbs[limb_].T_face[2, NomFace] = System.Convert.ToInt32(a4);

                                continue;

                            }


                    }



                }



            }
            // пересохраняем количесвто полигонов
            count_limbs = limb_;


            
            // отрисовываем геометрию
            CreateList();
            

            return 0;

        }

        /// <summary>
        /// Добавление текстуры
        /// </summary>
        private void CreateList()
        { 
            // получаем ID для создаваемого дисплейного списка
            int nom_l = Gl.glGenLists(1);
            thisList = nom_l;
            // генерируем новый дисплейный список
            Gl.glNewList(nom_l, Gl.GL_COMPILE);
            // сохраняем тек матрицу

            Gl.glPushMatrix();

            // проходим циклом по всем под-объектам
            for (int l = 0; l <= count_limbs; l++)
            {
                // если текстура необходима
                if (limbs[l].NeedTexture())
                    if (text_object != null) // текстурный объект существует
                    {
                        Gl.glEnable(Gl.GL_TEXTURE_2D); // включаем режим текстурирования

                        // ID текстуры в памяти
                        uint nn = text_object.GetTextureObj();
                        // активируем (привязываем) эту текстуру
                        Gl.glBindTexture(Gl.GL_TEXTURE_2D, nn);
                    }


                Gl.glEnable(Gl.GL_NORMALIZE);

                // начинаем отрисовку полигонов
                Gl.glBegin(Gl.GL_TRIANGLES);

                // по всем полигонам
                for (int i = 0; i < limbs[l].VandF[1]; i++)
                {
                    // временные переменные, чтобы код был более понятен
                    float x1, x2, x3, y1, y2, y3, z1, z2, z3 = 0;

                    // вытакскиваем координаты треугольника (полигона)
                    x1 = limbs[l].Vert[0, limbs[l].Face[0, i]];
                    x2 = limbs[l].Vert[0, limbs[l].Face[1, i]];
                    x3 = limbs[l].Vert[0, limbs[l].Face[2, i]];
                    y1 = limbs[l].Vert[1, limbs[l].Face[0, i]];
                    y2 = limbs[l].Vert[1, limbs[l].Face[1, i]];
                    y3 = limbs[l].Vert[1, limbs[l].Face[2, i]];
                    z1 = limbs[l].Vert[2, limbs[l].Face[0, i]];
                    z2 = limbs[l].Vert[2, limbs[l].Face[1, i]];
                    z3 = limbs[l].Vert[2, limbs[l].Face[2, i]];

                    // рассчитываем номраль
                    float n1 = (y2 - y1) * (z3 - z1) - (y3 - y1) * (z2 - z1);
                    float n2 = (z2 - z1) * (x3 - x1) - (z3 - z1) * (x2 - x1);
                    float n3 = (x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1);

                    // устанавливаем номраль
                    Gl.glNormal3f(n1, n2, n3);

                    // если установлена текстура
                    if (limbs[l].NeedTexture() && (limbs[l].T_vert != null) && (limbs[l].T_face != null))
                    {
                        // устанавливаем текстурные координаты для каждой вершины, ну и сами вершины
                        Gl.glTexCoord2f(limbs[l].T_vert[0, limbs[l].T_face[0, i]], limbs[l].T_vert[1, limbs[l].T_face[0, i]]);
                        Gl.glVertex3f(x1, y1, z1);

                        Gl.glTexCoord2f(limbs[l].T_vert[0, limbs[l].T_face[1, i]], limbs[l].T_vert[1, limbs[l].T_face[1, i]]);
                        Gl.glVertex3f(x2, y2, z2);

                        Gl.glTexCoord2f(limbs[l].T_vert[0, limbs[l].T_face[2, i]], limbs[l].T_vert[1, limbs[l].T_face[2, i]]);
                        Gl.glVertex3f(x3, y3, z3);

                    }
                    else // иначе - отрисовка только вершин
                    {

                        Gl.glVertex3f(x1, y1, z1);
                        Gl.glVertex3f(x2, y2, z2);
                        Gl.glVertex3f(x3, y3, z3);
                    }


                }

                // завершаем отрисовку
                Gl.glEnd();
                Gl.glDisable(Gl.GL_NORMALIZE);

                // открлючаем текстурирование
                Gl.glDisable(Gl.GL_TEXTURE_2D);

            }

            // возвращаем сохраненную ранее матрицу
            Gl.glPopMatrix();
            // завершаем дисплейный список
            Gl.glEndList();

            // загрузка завершена
            isLoad = true;
        }
        private void CreateListForOth()
        {
            // получаем ID для создаваемого дисплейного списка
            int nom_l = Gl.glGenLists(1);
            thisList = nom_l;
            // генерируем новый дисплейный список
            Gl.glNewList(nom_l, Gl.GL_COMPILE);
            // сохраняем тек матрицу

            Gl.glPushMatrix();

            // проходим циклом по всем под-объектам
            for (int l = 0; l <= count_limbs; l++)
            {
                // если текстура необходима
                if (limbs[l].NeedTexture())
                    if (text_objects[limbs[l].GetTextureNom()] != null) // текстурный объект существует
                    {
                        Gl.glEnable(Gl.GL_TEXTURE_2D); // включаем режим текстурирования

                        // ID текстуры в памяти
                        uint nn = text_objects[limbs[l].GetTextureNom()].GetTextureObj();
                        // активируем (привязываем) эту текстуру
                        Gl.glBindTexture(Gl.GL_TEXTURE_2D, nn);
                    }


                Gl.glEnable(Gl.GL_NORMALIZE);

                // начинаем отрисовку полигонов
                Gl.glBegin(Gl.GL_TRIANGLES);

                // по всем полигонам
                for (int i = 0; i < limbs[l].VandF[1]; i++)
                {
                    // временные переменные, чтобы код был более понятен
                    float x1, x2, x3, y1, y2, y3, z1, z2, z3 = 0;

                    // вытакскиваем координаты треугольника (полигона)
                    x1 = limbs[l].Vert[0, limbs[l].Face[0, i]];
                    x2 = limbs[l].Vert[0, limbs[l].Face[1, i]];
                    x3 = limbs[l].Vert[0, limbs[l].Face[2, i]];
                    y1 = limbs[l].Vert[1, limbs[l].Face[0, i]];
                    y2 = limbs[l].Vert[1, limbs[l].Face[1, i]];
                    y3 = limbs[l].Vert[1, limbs[l].Face[2, i]];
                    z1 = limbs[l].Vert[2, limbs[l].Face[0, i]];
                    z2 = limbs[l].Vert[2, limbs[l].Face[1, i]];
                    z3 = limbs[l].Vert[2, limbs[l].Face[2, i]];

                    // рассчитываем номраль
                    float n1 = (y2 - y1) * (z3 - z1) - (y3 - y1) * (z2 - z1);
                    float n2 = (z2 - z1) * (x3 - x1) - (z3 - z1) * (x2 - x1);
                    float n3 = (x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1);

                    // устанавливаем номраль
                    Gl.glNormal3f(n1, n2, n3);

                    // если установлена текстура
                    if (limbs[l].NeedTexture() && (limbs[l].T_vert != null) && (limbs[l].T_face != null))
                    {
                        // устанавливаем текстурные координаты для каждой вершины, ну и сами вершины
                        Gl.glTexCoord2f(limbs[l].T_vert[0, limbs[l].T_face[0, i]], limbs[l].T_vert[1, limbs[l].T_face[0, i]]);
                        Gl.glVertex3f(x1, y1, z1);

                        Gl.glTexCoord2f(limbs[l].T_vert[0, limbs[l].T_face[1, i]], limbs[l].T_vert[1, limbs[l].T_face[1, i]]);
                        Gl.glVertex3f(x2, y2, z2);

                        Gl.glTexCoord2f(limbs[l].T_vert[0, limbs[l].T_face[2, i]], limbs[l].T_vert[1, limbs[l].T_face[2, i]]);
                        Gl.glVertex3f(x3, y3, z3);

                    }
                    else // иначе - отрисовка только вершин
                    {

                        Gl.glVertex3f(x1, y1, z1);
                        Gl.glVertex3f(x2, y2, z2);
                        Gl.glVertex3f(x3, y3, z3);
                    }


                }

                // завершаем отрисовку
                Gl.glEnd();
                Gl.glDisable(Gl.GL_NORMALIZE);

                // открлючаем текстурирование
                Gl.glDisable(Gl.GL_TEXTURE_2D);

            }

            // возвращаем сохраненную ранее матрицу
            Gl.glPopMatrix();
            // завершаем дисплейный список
            Gl.glEndList();

            // загрузка завершена
            isLoad = true;
        }
        /// <summary>
        /// Функиция получения первого слова строки
        /// </summary>
        /// <param name="word">Входная строка</param>
        /// <param name="from">Указывает на позицию, начиная с которой будет выполнятся чтение файла</param>
        /// <returns></returns>
        protected string GetFirstWord(string word, int from)
        {

            // from указывает на позицию, начиная с которой будет выполнятся чтение файла
            char a = word[from]; // первый символ
            string res_buff = ""; // временный буффер
            int L = word.Length; // длина слова

            if (word[from] == ' ' || word[from] == '\t') // если первый символ, с которого предстоит искать слово является пробелом или знаком табуляции
            {
                // необходимо вычисслить наличие секции проблеов или знаков табуляции и откинуть их
                int ax = 0;
                // проходим до конца слова
                for (ax = from; ax < L; ax++)
                {
                    a = word[ax];
                    if (a != ' ' && a != '\t') // если встречаем символ пробела или табуляции
                        break; // выходим из цикла. 
                    // таким образом мы откидываем все последовательности пробелов или знаков табуляции, с которых могла начинатся переданная строка
                }

                if (ax == L) // если вся представленная строка является набором пробелов или знаков табуляции - возвращаем res_buff
                    return res_buff;
                else
                    from = ax; // иначе сохраняем значение ax
            }
            int bx = 0;

            // теперь, когда пробелы и табуляция откинуты мы непосредственно вычисляем слово
            for (bx = from; bx < L; bx++)
            {
                // если встретили знак пробела или табуляции - завершаем чтение слова
                if (word[bx] == ' ' || word[bx] == '\t')
                    break;
                // записываем символ в бременный буффер, постепенно получая таким образом слово
                res_buff += word[bx];
            }

            // если дошли до конца строки
            if (bx == L)
                bx--; // убераем посл значение

            GlobalStringFrom = bx; // позиция в данной строке, для чтения следующего слова в данной строке

            return res_buff; // возвращаем слово
        }
        #endregion

        /// <summary>
        /// функция отрисовки 3D модели
        /// </summary>
        public void DrawModel()
        {
            // если модель не загружена - возврат из функции
            if (!isLoad)
                return;

            // сохраняем матрицу
            Gl.glPushMatrix();
Gl.glTranslated(position.X, position.Y, position.Z);
            //Gl.glTranslated(basePos.X*Scale, basePos.Y * Scale, basePos.Z * Scale);
            Gl.glRotated(position.AngleAxisX, 1, 0, 0);
            Gl.glRotated(position.AngleAxisY, 0, 1, 0);
            Gl.glRotated(position.AngleAxisZ, 0, 0, 1);
            //Gl.glTranslated(-basePos.X * Scale, -basePos.Y * Scale, -basePos.Z * Scale);

            
            // масштабирование по умолчанию
            Gl.glScalef(Scale, Scale, Scale);

            // вызов дисплейного списка

            //CreateList();

            Gl.glCallList(thisList);

            // возврат матрицы
            Gl.glPopMatrix();

        }

        /// <summary>
        /// Получение размеров 3d модели
        /// </summary>
        /// <returns></returns>
        //private SizeF GetSize()
        //{
        //    //float maxX = 0;
        //    //float maxY = 0;
        //    //for (int l = 0; l <= count_limbs; l++)
        //    //    for (int l1 = l; l1 <= count_limbs; l1++)
        //    //        for (int i = 0; i < limbs[l].VandF[1]; i++)
        //    //            for (int i1 = i; i1 < limbs[l1].VandF[1]; i1++)
        //    //                for (int g = 0;g < 3;g++)
        //    //                    for (int g1 = g; g1 < 3; g1++)
        //    //                    { 
        //    //                        if(Math.Abs(limbs[l].Vert[0, limbs[l].Face[g, i]] - limbs[l1].Vert[0, limbs[l1].Face[g1, i1]]) > maxX)
        //    //                        {
        //    //                            maxX = Math.Abs(limbs[l].Vert[0, limbs[l].Face[g, i]] - limbs[l1].Vert[0, limbs[l1].Face[g1, i1]]);
        //    //                        }
        //    //                        if(Math.Abs(limbs[l].Vert[1, limbs[l].Face[g, i]] - limbs[l1].Vert[1, limbs[l1].Face[g1, i1]]) > maxY)
        //    //                        {
        //    //                            maxY = Math.Abs(limbs[l].Vert[1, limbs[l].Face[0, i]] - limbs[l1].Vert[1, limbs[l1].Face[0, i1]]);
        //    //                        }
        //    //                    }

        //    //return new SizeF(maxX, maxY);

        //}

    }
}
