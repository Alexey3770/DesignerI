using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.DevIl;
using Tao.OpenGl;

namespace Designer.Domain.Entity
{
    /// <summary>
    /// Класс для работы с текстурами
    /// </summary>
    
    public class TexturesForObjects
    {
        public TexturesForObjects()
        {

        }

        /// <summary>
        /// Имя текстуры
        /// </summary>
        private string texture_name = "";
        /// <summary>
        /// ИД текстуры
        /// </summary>
        private int imageId = 0;

        /// <summary>
        /// Идетификатор текстуры в памяти openGL
        /// </summary>
        private uint mGlTextureObject = 0;

        /// <summary>
        /// Получение этого идентификатора
        /// </summary>
        /// <returns></returns>
        public uint GetTextureObj()
        {
            return mGlTextureObject;
        }



        /// <summary>
        /// Загрузка текстуры
        /// </summary>
        /// <param name="FileName"></param>
        public void LoadTextureForModel(string FileName)
        {
            // запоминаем имя файла
            texture_name = FileName;
            // создаем изображение с индификатором imageId 
            Il.ilGenImages(1, out imageId);
            // делаем изображение текущим 
            Il.ilBindImage(imageId);

            string url = texture_name;
            
            // если загрузка удалась
            if (Il.ilLoadImage(url))
            {
                // если загрузка прошла успешно 
                // сохраняем размеры изображения 
                int width = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
                int height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);

                // определяем число бит на пиксель 
                int bitspp = Il.ilGetInteger(Il.IL_IMAGE_BITS_PER_PIXEL);

                switch (bitspp)// в зависимости оп полученного результата 
                {
                    // создаем текстуру используя режим GL_RGB или GL_RGBA 
                    case 24:
                        mGlTextureObject = MakeGlTexture(Gl.GL_RGB, Il.ilGetData(), width, height);
                        break;
                    case 32:
                        mGlTextureObject = MakeGlTexture(Gl.GL_RGBA, Il.ilGetData(), width, height);
                        break;
                }

                // очищаем память 
                Il.ilDeleteImages(1, ref imageId);


            }
        }

        /// <summary>
        /// Создание текстуры в панями openGL 
        /// </summary>
        /// <param name="Format"></param>
        /// <param name="pixels"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        private static uint MakeGlTexture(int Format, IntPtr pixels, int w, int h)
        {
            // индетефекатор текстурного объекта 
            uint texObject;

            // генерируем текстурный объект 
            Gl.glGenTextures(1, out texObject);

            // устанавливаем режим упаковки пикселей 
            Gl.glPixelStorei(Gl.GL_UNPACK_ALIGNMENT, 1);

            // создаем привязку к только что созданной текстуре 
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texObject);

            // устанавливаем режим фильтрации и повторения текстуры 
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_REPLACE);

            // создаем RGB или RGBA текстуру 
            switch (Format)
            {
                case Gl.GL_RGB:
                    Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB, w, h, 0, Gl.GL_RGB, Gl.GL_UNSIGNED_BYTE, pixels);
                    break;

                case Gl.GL_RGBA:
                    Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, w, h, 0, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, pixels);
                    break;
            }

            // возвращаем индетефекатор текстурного объекта 
            return texObject;
        }

    }
}
