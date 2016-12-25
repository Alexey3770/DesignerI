using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Designer.Domain.Entity
{
    /// <summary>
    /// Класс содержит константы с размерами
    /// </summary>
    public class Sizes
    {
        /// <summary>
        /// Ширина 1 м в двухмерном представлении
        /// </summary>
        public const float WIDTH2D = 50;
        /// <summary>
        /// Высота 1 м в двухмерном представлении
        /// </summary>
        public const float HEIGHT2D = 2;
        /// <summary>
        /// Высота в трёхмерном представлении
        /// </summary>
        public const float HEIGHT3D = 3;
        /// <summary>
        /// Ширина 1 м в трёхмерном представлении
        /// </summary>
        public const float WIDTH3D = 1;
        /// <summary>
        /// Длина в трёхмерном представлении
        /// </summary>
        public const float LENGTH3D = 0.05f;
    }
}
