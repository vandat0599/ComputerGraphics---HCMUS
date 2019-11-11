using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SharpGL;
using System.Collections;

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
        public Point getEndPoint() { return this.endPoint; }
        public OpenGL getGL() { return this.gl; }
        public float[] getColor() { return this.color; }
        public float getLineWidth() { return this.lineWidth; }
        public void setLineWidth(float lineWidth) { this.lineWidth = lineWidth; }
        public void setGL(OpenGL gl) { this.gl = gl; }
        public void setColor(float[] color) { this.color = color; }
        public void setStartPoint(Point p) { this.startPoint = p; }
        public void setEndPoint(Point p) { this.endPoint = p; }

        public virtual void Draw(){}
        public virtual void Erase() {
            float[] currentColor = this.getColor();
            this.setColor(new float[] { 0f, 0f, 0f });
            Draw();
            this.setColor(currentColor);
        }
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
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            for (int i = 0; i < lineWidth; i++) {
                double r = Math.Sqrt(Math.Pow(endPoint.X - startPoint.X, 2) + Math.Pow(endPoint.Y - startPoint.Y, 2)) - i;
                double p = 5 / 4 - r;
                int x = 0, y = Convert.ToInt32(r);

                gl.Color(color);
                gl.LineWidth(lineWidth);
                gl.Begin(OpenGL.GL_POINTS);
                gl.Vertex(x + startPoint.X, gl.RenderContextProvider.Height - (y + startPoint.Y));
                gl.Vertex(-x + startPoint.X, gl.RenderContextProvider.Height - (y + startPoint.Y));
                gl.Vertex(-y + startPoint.X, gl.RenderContextProvider.Height - (x + startPoint.Y));
                gl.Vertex(-y + startPoint.X, gl.RenderContextProvider.Height - (-x + startPoint.Y));
                gl.Vertex(-x + startPoint.X, gl.RenderContextProvider.Height - (-y + startPoint.Y));
                gl.Vertex(x + startPoint.X, gl.RenderContextProvider.Height - (-y + startPoint.Y));
                gl.Vertex(y + startPoint.X, gl.RenderContextProvider.Height - (-x + startPoint.Y));
                gl.Vertex(y + startPoint.X, gl.RenderContextProvider.Height - (x + startPoint.Y));

                while (x < y) {
                    if (p < 0) {
                        p = p + 2 * x + 3;
                    } else {
                        p = p + 2 * (x - y) + 5;
                        y = y - 1;
                    }
                    x = x + 1;
                    gl.Vertex(x + startPoint.X, gl.RenderContextProvider.Height - (y + startPoint.Y));
                    gl.Vertex(-x + startPoint.X, gl.RenderContextProvider.Height - (y + startPoint.Y));
                    gl.Vertex(-y + startPoint.X, gl.RenderContextProvider.Height - (x + startPoint.Y));
                    gl.Vertex(-y + startPoint.X, gl.RenderContextProvider.Height - (-x + startPoint.Y));
                    gl.Vertex(-x + startPoint.X, gl.RenderContextProvider.Height - (-y + startPoint.Y));
                    gl.Vertex(x + startPoint.X, gl.RenderContextProvider.Height - (-y + startPoint.Y));
                    gl.Vertex(y + startPoint.X, gl.RenderContextProvider.Height - (-x + startPoint.Y));
                    gl.Vertex(y + startPoint.X, gl.RenderContextProvider.Height - (x + startPoint.Y));
                }
            }

            

            gl.End();
            gl.Flush();
            
        }
    }

    class Rectangle: Shape {
        public Rectangle(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw() {
            base.Draw();
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.Color(color);
            gl.LineWidth(lineWidth);
            gl.Begin(OpenGL.GL_LINE_LOOP);
            gl.Vertex(startPoint.X, gl.RenderContextProvider.Height - startPoint.Y);
            gl.Vertex(endPoint.X, gl.RenderContextProvider.Height - startPoint.Y);
            gl.Vertex(endPoint.X, gl.RenderContextProvider.Height - endPoint.Y);
            gl.Vertex(startPoint.X, gl.RenderContextProvider.Height - endPoint.Y);
            gl.End();
            gl.Flush();
        }
    }

    class Ellipse: Shape {
        public Ellipse(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw() {
            base.Draw();
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            for (int i = 0; i < lineWidth; i++) {
                double ry = Math.Abs(endPoint.Y - startPoint.Y) - i;
                double rx = Math.Abs(endPoint.X - startPoint.X) - i;
                //r2y -r2x .ry +1⁄4.r2x
                double p = Math.Pow(ry, 2) - Math.Pow(rx, 2) * ry + Math.Pow(rx, 2) * 1 / 4;
                int x = 0, y = Convert.ToInt32(ry);
                gl.Color(color);
                gl.LineWidth(lineWidth);
                gl.Begin(OpenGL.GL_POINTS);
                gl.Vertex(x + startPoint.X, gl.RenderContextProvider.Height - (y + startPoint.Y));
                gl.Vertex(-x + startPoint.X, gl.RenderContextProvider.Height - (y + startPoint.Y));
                gl.Vertex(-x + startPoint.X, gl.RenderContextProvider.Height - (-y + startPoint.Y));
                gl.Vertex(x + startPoint.X, gl.RenderContextProvider.Height - (-y + startPoint.Y));
                while (2 * Math.Pow(ry, 2) * x < 2 * Math.Pow(rx, 2) * y) {
                    if (p < 0) {
                        p = p + 2 * Math.Pow(ry, 2) * (x + 1) + Math.Pow(ry, 2);
                    } else {
                        p = p + 2 * Math.Pow(ry, 2) * (x + 1) - 2 * Math.Pow(rx, 2) * (y - 1) + Math.Pow(ry, 2);
                        y = y - 1;
                    }
                    x = x + 1;
                    gl.Vertex(x + startPoint.X, gl.RenderContextProvider.Height - (y + startPoint.Y));
                    gl.Vertex(-x + startPoint.X, gl.RenderContextProvider.Height - (y + startPoint.Y));
                    gl.Vertex(-x + startPoint.X, gl.RenderContextProvider.Height - (-y + startPoint.Y));
                    gl.Vertex(x + startPoint.X, gl.RenderContextProvider.Height - (-y + startPoint.Y));
                }

                int xL = x, yL = y;
                double p2 = Math.Pow(ry, 2) * Math.Pow(xL + 1 / 2, 2) + Math.Pow(rx, 2) * Math.Pow(yL - 1, 2) - Math.Pow(rx, 2) * Math.Pow(ry, 2);
                while (y >= 0) {
                    if (p2 > 0) {
                        y = y - 1;
                        p2 = p2 - 2 * Math.Pow(rx, 2) * y + Math.Pow(rx, 2);
                    } else {
                        x = x + 1;
                        y = y - 1;
                        p2 = p2 + 2 * Math.Pow(ry, 2) * x - 2 * Math.Pow(rx, 2) * y + Math.Pow(rx, 2);

                    }
                    gl.Vertex(x + startPoint.X, gl.RenderContextProvider.Height - (y + startPoint.Y));
                    gl.Vertex(-x + startPoint.X, gl.RenderContextProvider.Height - (y + startPoint.Y));
                    gl.Vertex(-x + startPoint.X, gl.RenderContextProvider.Height - (-y + startPoint.Y));
                    gl.Vertex(x + startPoint.X, gl.RenderContextProvider.Height - (-y + startPoint.Y));
                }
            }
            gl.End();
            gl.Flush();

        }
    }

    class EqTriagle: Shape {
        public EqTriagle(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw() {
            base.Draw();
            int bY = Convert.ToInt32(endPoint.Y - Math.Abs(endPoint.X - startPoint.X) * Math.Cos(30 * Math.PI / 180));
            int bX = Convert.ToInt32(startPoint.X + (endPoint.X - startPoint.X) * Math.Sin(30 * Math.PI/180));
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.Color(color);
            gl.LineWidth(lineWidth);
            gl.Begin(OpenGL.GL_LINE_LOOP);
            gl.Vertex(startPoint.X, gl.RenderContextProvider.Height - endPoint.Y);
            gl.Vertex(bX, gl.RenderContextProvider.Height - bY);
            gl.Vertex(endPoint.X, gl.RenderContextProvider.Height - endPoint.Y);
            gl.End();
            gl.Flush();
        }
    }

    class EqPentagon: Shape {
        public EqPentagon(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw() {
            base.Draw();
            double a = Math.Abs(endPoint.X - startPoint.X)*2*3/5;
            double R = a /(2*Math.Sin(36*Math.PI/180));
            double x = Math.Sin(72 * Math.PI / 180) * a;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.Color(color);
            gl.LineWidth(lineWidth);
            gl.Begin(OpenGL.GL_LINE_LOOP);
            gl.Vertex(startPoint.X, gl.RenderContextProvider.Height - (endPoint.Y - x - Math.Abs(endPoint.X - startPoint.X)*Math.Sin(36*Math.PI/180)));
            gl.Vertex(startPoint.X + Math.Abs(endPoint.X - startPoint.X), gl.RenderContextProvider.Height - (endPoint.Y - x));
            gl.Vertex(startPoint.X + a/2, gl.RenderContextProvider.Height - endPoint.Y);
            gl.Vertex(startPoint.X - a/2, gl.RenderContextProvider.Height - endPoint.Y);
            gl.Vertex(startPoint.X - Math.Abs(endPoint.X - startPoint.X), gl.RenderContextProvider.Height - (endPoint.Y - x));
            gl.End();
            gl.Flush();
        }
    }

    class EqHexagon: Shape {
        public EqHexagon(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw() {
            base.Draw();
            double a = Math.Abs(endPoint.X - startPoint.X) * 2 * 2 / 4;
            double x = Math.Sin(60 * Math.PI / 180) * a;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.Color(color);
            gl.LineWidth(lineWidth);
            gl.Begin(OpenGL.GL_LINE_LOOP);
            gl.Vertex(startPoint.X - Math.Abs(endPoint.X - startPoint.X), gl.RenderContextProvider.Height - startPoint.Y);
            gl.Vertex(startPoint.X - a / 2, gl.RenderContextProvider.Height - (startPoint.Y - x));
            gl.Vertex(startPoint.X + a / 2, gl.RenderContextProvider.Height - (startPoint.Y - x));
            gl.Vertex(startPoint.X + Math.Abs(endPoint.X - startPoint.X), gl.RenderContextProvider.Height - startPoint.Y);
            gl.Vertex(startPoint.X + a/2, gl.RenderContextProvider.Height - (startPoint.Y+x));
            gl.Vertex(startPoint.X - a / 2, gl.RenderContextProvider.Height - (startPoint.Y + x));
            gl.End();
            gl.Flush();
        }
    }
    class Polygon {

        private ArrayList pointArr;
        private OpenGL gl;
        private float[] color;
        float lineWidth;

        public Polygon(ArrayList lineArr, OpenGL gl, float[] color, float lineWidth) {
            this.pointArr = lineArr;
            this.gl = gl;
            this.color = color;
            this.lineWidth = lineWidth;
        }

        public ArrayList getPointArr() { return pointArr; }
        public void setLineArr(ArrayList pointArr) { this.pointArr = pointArr; }
        public void addPoint(Point point) { this.pointArr.Add(point); }
        public void removeLastPoint() { this.pointArr.RemoveAt(pointArr.Count - 1); }
        public OpenGL getGL(){return gl;}
        public void setGL(OpenGL gl){this.gl = gl;}
        public float getLineWidth(){return lineWidth;}
        public void setLineWidth(float lineWidth) { this.lineWidth = lineWidth; }

        public void Draw(){
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.Color(color);
            gl.LineWidth(lineWidth);
            gl.Begin(OpenGL.GL_LINE_LOOP);
            foreach(Point point in pointArr){
                gl.Vertex(point.X, gl.RenderContextProvider.Height - point.Y);
            }
            gl.End();
            gl.Flush();
        }
    }
}
