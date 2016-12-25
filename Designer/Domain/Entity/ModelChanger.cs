using Designer.Acquaintance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Designer.Domain.Entity;
using Designer.Domain.Mediator;

namespace Designer.Domain.Entity
{
    /// <summary>
    /// Класс необходим для инкапсуляции процесса создания модели
    /// </summary>
    public class ModelChanger
    {
        TextureLibFloor textureLibFloor;
        TextureLibSide textureLibSide;
        FurnitureLib furnitureLib;

        public ModelChanger()
        {
            textureLibFloor = new TextureLibFloor();
            textureLibSide = new TextureLibSide();
            furnitureLib = new FurnitureLib();
        }
        /// <summary>
        /// Получение начальной модели
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public IModel GetStartModel(int width, int height)
        {
            return new RoomCreater();
        }
        /// <summary>
        /// Смена модели
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IModel ChangeModel(IModel model)
        {

            if(model.GetType() == typeof(RoomCreater))
            {
                return new TextureSelector(((RoomCreater)model).PlanSideRoom, textureLibFloor, textureLibSide);
            }
            else if (model.GetType() == typeof(TextureSelector))
            {
                return new FurnitureAdder(((TextureSelector)model).PlanSideRoom,furnitureLib);
            }

            return null;
        }
        public IModel GetModel(string type,Room room)
        {

            if (type == "RoomCreater")
            {
                return new RoomCreater(room);
            }
            else if(type == "TextureSelector")
            {
                return new TextureSelector(room, textureLibFloor, textureLibSide);
            }
            else if (type == "FurnitureAdder")
            {
                return new FurnitureAdder(room, furnitureLib);
            }
            return null;
        }
    }
}
