using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Designer.Domain.Entity
{
    /// <summary>
    /// Класс LIMB отвечает за логические единицы 3D объектов в загружаемой сцене
    /// </summary>
    
    public class LIMB
    {

        /// <summary>
        ///  при инициализации мы должны указать  и полигонов (face) которые описывают геометри под-объекта
        /// </summary>
        /// <param name="a">Количество вершин (vertex), которые описывают геометри под-объекта</param>
        /// <param name="b">Количество полигонов (face), которые описывают геометри под-объекта</param>
        public LIMB(int a, int b)
        {
            if (temp[0] == 0)
                temp[0] = 1;

            // записываем количество вершин и полигонов
            VandF[0] = a;
            VandF[1] = b;

            // выделяем память
            memcompl();

        }
        /// <summary>
        /// флаг успешности
        /// </summary>
        private int itog;

        /// <summary>
        /// массивы для хранения данных (геометрии и текстурных координат)
        /// </summary>
        private float[,] vert;
        private int[,] face;
        private float[,] t_vert;
        private int[,] t_face;


        /// <summary>
        ///  Номер материала (текстуры) данного под-объекта
        /// </summary>
        private int MaterialNom = -1;

        /// <summary>
        /// Временное хранение информации
        /// </summary>
        public int[] VandF = new int[4];
        private int[] temp = new int[2];

        /// <summary>
        /// Флаг , говорящий о том, что модель использует текстуру
        /// </summary>
        private bool modelHasTexture = false;

        public float[,] Vert
        {
            get
            {
                return vert;
            }

            set
            {
                vert = value;
            }
        }

        public int[,] Face
        {
            get
            {
                return face;
            }

            set
            {
                face = value;
            }
        }

        public float[,] T_vert
        {
            get
            {
                return t_vert;
            }

            set
            {
                t_vert = value;
            }
        }

        public int[,] T_face
        {
            get
            {
                return t_face;
            }

            set
            {
                t_face = value;
            }
        }

        public int Itog
        {
            get
            {
                return itog;
            }

            set
            {
                itog = value;
            }
        }

        /// <summary>
        /// Функция для определения значения флага (о наличии текстуры)
        /// </summary>
        /// <returns></returns>
        public bool NeedTexture()
        {
            // возвращаем значение флага
            return modelHasTexture;
        }
        /// <summary>
        /// Устанвка номера текстуры
        /// </summary>
        /// <param name="new_nom">Номер текстуры</param>
        public void SetMaterialNom(int new_nom)
        {
            MaterialNom = new_nom;
            if (MaterialNom > -1)
                // отмечаем флаг о наличии текстуры
                modelHasTexture = true;
        }

        /// <summary>
        /// Массивы для текстурных координат
        /// </summary>
        /// <param name="a"></param>
        public void СreateTextureVertexMem(int a)
        {
            VandF[2] = a;
            T_vert = new float[3, VandF[2]];
        }

        /// <summary>
        /// Привязка значений текстурных координат к полигонам 
        /// </summary>
        /// <param name="b"></param>
        public void СreateTextureFaceMem(int b)
        {
            VandF[3] = b;
            T_face = new int[3, VandF[3]];

        }

        /// <summary>
        /// память для геометрии
        /// </summary>
        private void memcompl()
        {
            Vert = new float[3, VandF[0]];
            Face = new int[3, VandF[1]];
        }

        /// <summary>
        /// Номер текстуры
        /// </summary>
        /// <returns></returns>
        public int GetTextureNom()
        {
            return MaterialNom;
        }



    }
}
