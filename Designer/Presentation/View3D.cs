using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.DevIl;
using Tao.FreeGlut;
using Tao.OpenGl;
using Tao.Platform.Windows;
using System.Drawing;

namespace Designer.Presentation
{
    class View3D : SimpleOpenGlControl
    {
        /// <summary>
        /// Текущее положение камеры
        /// </summary>
        public Camera3D Camera
        {
            get { return camera; }
            set
            {
                camera = value;
                if(drawObjs != null)
                    Draw();
            }
        }

        List<IDrawableObj> drawObjs;
        Point3D upVector;
        Camera3D camera;
        Pen pLine;

        public View3D()
        {
            upVector = new Point3D(0,1, 0);
            pLine = new Pen(Brushes.Black, 1);
            camera = new Camera3D(new Point3D(0, 0, 400));
            BorderStyle = BorderStyle.FixedSingle;
            //DoubleBuffered = true;
        }
        /// <summary>
        /// Изменение размеров области вывода трёхмерного изображения
        /// </summary>
        public void ChangeViewPort()
        {
            Gl.glViewport(0, 0, Width, Height); Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Glu.gluPerspective(65, (float)Width / (float)Height, 0.1, 500); Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
        }
        /// <summary>
        /// Инициализация Opengl
        /// </summary>
        public void Init()
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Il.ilInit();

            Il.ilEnable(Il.IL_ORIGIN_SET);
            
            Gl.glClearColor(255, 255, 255, 1);

            Gl.glViewport(0, 0, Width, Height);

            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            
            Glu.gluPerspective(65, (float)Width / (float)Height, 0.1, 500);
           
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            
            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);
            //Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, new float[] { 10, 10, 100,1 });
            //Gl.glLightfv(Gl.GL_LIGHT0, Gl, new float[] { 10, 10, 0 });

            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glEnable(Gl.GL_LINE_SMOOTH);
            Gl.glLineWidth(1.0f);
            if (drawObjs != null)
                Draw();
        }
        /// <summary>
        /// Отрисовка новых трёхмерных объектов
        /// </summary>
        /// <param name="objs"></param>
        public void DrawObjects(List<IDrawableObj> objs)
        {
            drawObjs = objs;
            Draw();
        }
        /// <summary>
        /// Отрисовка текущих трёхмерных объектов
        /// </summary>
        public new void Draw()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();
            Glu.gluLookAt(camera.Position.X, camera.Position.Y, camera.Position.Z,
                camera.Target.X, camera.Target.Y, camera.Target.Z,
                camera.UpVector.X, camera.UpVector.Y, camera.UpVector.Z);
            Gl.glColor3i(255, 0, 0);

            Gl.glPushMatrix();
            if(drawObjs != null)
                foreach (IDrawableObj d in drawObjs)
                    d.Draw3D();

            Gl.glPopMatrix();

            Gl.glFlush();

            Invalidate();
        }
        
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // View3D
            // 
            this.Name = "View3D";
            this.ResumeLayout(false);

        }
    }
}
