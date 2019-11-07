using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SharpGL;

namespace Object {
    class Shape {
        protected Point startPoint;
        protected Point endPoint;
        protected OpenGL gl;
        protected float[] color;
        protected float lineWidth;

        public Shape(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.gl = gl;
            this.color = color;
            this.lineWidth = lineWidth;
        }

        public Point getStartPoint() { return this.startPoint; }
        public Point getEngPoint() { return this.endPoint; }
        public OpenGL getGL() { return this.gl; }
        public float[] getColor() { return this.color; }
        public float getLineWidth() { return this.lineWidth; }
        public void setLineWidth(float lineWidth) { this.lineWidth = lineWidth; }
        public void setGL(OpenGL gl) { this.gl = gl; }
        public void setColor(float[] color) { this.color = color; }
        public void setStartPoint(Point p) { this.startPoint = p; }
        public void setEngPoint(Point p) { this.endPoint = p; }

        public virtual void Draw(){}
    }

    class Line: Shape {
        public Line(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw() {
            base.Draw();
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.Color(color);
            gl.LineWidth(lineWidth);
            gl.Begin(OpenGL.GL_LINES);
            //sinh tu giai thich
            gl.Vertex(startPoint.X, gl.RenderContextProvider.Height -  startPoint.Y);
            gl.Vertex(endPoint.X, gl.RenderContextProvider.Height - endPoint.Y);
            gl.End();
            gl.Flush();
        }
    }

    class Circle: Shape {
        public Circle(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw() {
            base.Draw();
            //gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            //gl.Color(color);
            //gl.LineWidth(lineWidth);
            //gl.Begin(OpenGL.);
            
            ////sinh tu giai thich
            //gl.Vertex(startPoint.X, gl.RenderContextProvider.Height - startPoint.Y);
            //gl.Vertex(endPoint.X, gl.RenderContextProvider.Height - endPoint.Y);
            //gl.End();
            //gl.Flush();
            
        }
    }

    class Rectangle: Shape {
        public Rectangle(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw() {
            base.Draw();

        }
    }

    class Ellipse: Shape {
        public Ellipse(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw() {
            base.Draw();

        }
    }

    class EqTriagle: Shape {
        public EqTriagle(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw() {
            base.Draw();
            double d = Math.Sqrt(Math.Pow(endPoint.X - startPoint.X, 2) + Math.Pow(endPoint.Y - startPoint.Y, 2));
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.Color(color);
            gl.LineWidth(lineWidth);
            gl.Begin(OpenGL.GL_TRIANGLES);
            //sinh tu giai thich
            gl.Vertex(startPoint.X, gl.RenderContextProvider.Height - startPoint.Y);
            gl.Vertex(endPoint.X, gl.RenderContextProvider.Height - endPoint.Y);
            gl.Vertex(endPoint.X - d, gl.RenderContextProvider.Height - (startPoint.Y + d*Math.Sqrt(3)/2));   
            gl.End();
            gl.Flush();
        }
    }

    class EqPentagon: Shape {
        public EqPentagon(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw() {
            base.Draw();

        }
    }

    class EqHexagon: Shape {
        public EqHexagon(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw() {
            base.Draw();

        }
    }
}
