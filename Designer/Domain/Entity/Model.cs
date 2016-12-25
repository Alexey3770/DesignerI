using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Designer.Domain.Entity;
using Designer.Acquaintance;

namespace Designer
{
    class Model
    {
        #region Properties
        /// <summary>
        /// Список выбираемых элементов
        /// </summary>
        public List<IDrawableListFur> CurrentDrawList
        {
            get { return currentDrawList; }
        }

        /// <summary>
        /// Список отображаемых элементов
        /// </summary>
        public List<IDrawableObj> DrawObj
        {
            get { return drawObj; }
        }
        #endregion
        #region Private Field
        int width, height;
        List<IDrawableObj> drawObj;
        List<DrawableElement> pointSide;
        List<IDrawableListFur> currentDrawList;
        Bitmap b = new Bitmap(20, 20);
        #endregion
        
        public Model(int width, int height)
        {
            currentDrawList = new List<IDrawableListFur>();
            drawObj = new List<IDrawableObj>();
            pointSide = new List<DrawableElement>();
            currentDrawList.Add(new DrawableListElement(Properties.Resources.butsearch, "Квадрат", null));
            this.width = width;
            this.height = height;
            
            Graphics g = Graphics.FromImage(b);
            g.DrawImage(Properties.Resources.Round,0,0,20,20);
        }
        
        public void AddPointSide(PointF p)
        {
            pointSide.Add(new DrawableElement(new PointF(p.X - b.Width/2,p.Y - b.Height/2),b));
            if(pointSide.Count > 1)
            {

            }

        }
        public void MovePoint(PointF p)
        {
            if (pointSide.Count == drawObj.Count)
                drawObj.Add(new DrawableElement(new PointF(p.X - b.Width / 2, p.Y - b.Height / 2), b));
            if (pointSide.Count > 0)
            {
                if(Math.Abs(pointSide[pointSide.Count-1].Position.X - p.X) > Math.Abs(pointSide[pointSide.Count - 1].Position.Y - p.Y))
                    drawObj[drawObj.Count - 1].Position = new PointF(p.X - b.Width / 2, pointSide[pointSide.Count - 1].Position.Y);
                else
                    drawObj[drawObj.Count - 1].Position = new PointF(pointSide[pointSide.Count - 1].Position.X, p.Y - b.Height / 2);

            }
            else
                drawObj[drawObj.Count - 1].Position = new PointF(p.X - b.Width / 2, p.Y - b.Height / 2);
        }

        public void SelectDrawObject(string name)
        {
            foreach(IDrawableListFur c in currentDrawList)
            {
                if(c.Name == name)
                {

                }
            }
        }

    }
}
