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
        protected ArrayList pointArr;
        protected ArrayList controlPoints;

        public Shape(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            this.gl = gl;
            this.color = color;
            this.lineWidth = lineWidth;
            this.pointArr = new ArrayList();
            controlPoints = new ArrayList();
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
        public ArrayList getPointArr() { return this.pointArr; }
        public void addPointArr(Point p) { this.pointArr.Add(p); }
        public ArrayList getControlPoints() { return this.controlPoints; }
        public void setControlPoints(ArrayList controlPoints) { this.controlPoints = controlPoints; }
        public void addControlPoint(Point p) { this.controlPoints.Add(p); }

        public virtual void Draw(bool erase = true) { }
        public virtual void Erase() {
            float[] currentColor = this.getColor();
            this.setColor(new float[] { 0f, 0f, 0f });
            this.pointArr = new ArrayList();
            Draw();
            this.setColor(currentColor);
        }

        public virtual void DrawControlPoint(bool erase = true, bool showControlPoint = true) {
            if (erase) {
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            }
            float[] controlColor = new float[] { 1f, 1f, 1f };
            if (!showControlPoint) {
                controlColor = new float[] { 0f, 0f, 0f };
            }
            gl.Color(controlColor);
            gl.LineWidth(lineWidth + 2);
            
            gl.Begin(OpenGL.GL_POINTS);
            foreach (Point p in controlPoints) {
                gl.Vertex(p.X, gl.RenderContextProvider.Height - (p.Y));
                gl.Vertex(p.X+1, gl.RenderContextProvider.Height - (p.Y));
                gl.Vertex(p.X-1, gl.RenderContextProvider.Height - (p.Y));
                gl.Vertex(p.X, gl.RenderContextProvider.Height - (p.Y+1));
                gl.Vertex(p.X, gl.RenderContextProvider.Height - (p.Y-1));
                gl.Vertex(p.X-1, gl.RenderContextProvider.Height - (p.Y-1));
                gl.Vertex(p.X+1, gl.RenderContextProvider.Height - (p.Y+1));
                gl.Vertex(p.X-1, gl.RenderContextProvider.Height - (p.Y+1));
                gl.Vertex(p.X+1, gl.RenderContextProvider.Height - (p.Y-1));
            }
            gl.End();
            gl.Flush();
        }
    }

    class Line: Shape {
        public Line(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw(bool erase = true) {
            base.Draw();
            this.pointArr = new ArrayList();
            this.controlPoints = new ArrayList();
            this.addControlPoint(startPoint);
            this.addControlPoint(endPoint);
            if (erase) {
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            }
            gl.Color(color);
            gl.LineWidth(lineWidth);
            gl.Begin(OpenGL.GL_POINTS);
            int x = startPoint.X, y = startPoint.Y;
            int w = endPoint.X - startPoint.X;
            int h = endPoint.Y - startPoint.Y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest)) {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++) {
                gl.Vertex(x, gl.RenderContextProvider.Height - (y));
                this.addPointArr(new Point(x, y));
                numerator += shortest;
                if (!(numerator < longest)) {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                } else {
                    x += dx2;
                    y += dy2;
                }
            }
            gl.End();
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(startPoint.X, gl.RenderContextProvider.Height - (startPoint.Y));
            gl.Vertex(endPoint.X, gl.RenderContextProvider.Height - (endPoint.Y));
            gl.End();
            gl.Flush();
        }
    }

    class Circle: Shape {
        public Circle(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw(bool erase = true) {
            base.Draw();
            this.pointArr = new ArrayList();
            this.controlPoints = new ArrayList();
            if (erase) {
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            }
            //double r = Math.Sqrt(Math.Pow(endPoint.X - startPoint.X, 2) + Math.Pow(endPoint.Y - startPoint.Y, 2));
            double r = Math.Abs(endPoint.X - startPoint.X);
            this.addControlPoint(new Point(Convert.ToInt32(startPoint.X + r), startPoint.Y));
            this.addControlPoint(new Point(Convert.ToInt32(startPoint.X - r), startPoint.Y));
            this.addControlPoint(new Point(startPoint.X,Convert.ToInt32(startPoint.Y - r)));
            this.addControlPoint(new Point(startPoint.X, Convert.ToInt32(startPoint.Y + r)));
            this.addControlPoint(new Point(Convert.ToInt32(startPoint.X + r), Convert.ToInt32(startPoint.Y + r)));
            this.addControlPoint(new Point(Convert.ToInt32(startPoint.X + r), Convert.ToInt32(startPoint.Y - r)));
            this.addControlPoint(new Point(Convert.ToInt32(startPoint.X - r), Convert.ToInt32(startPoint.Y + r)));
            this.addControlPoint(new Point(Convert.ToInt32(startPoint.X - r), Convert.ToInt32(startPoint.Y - r)));
            for (int i = 0; i < lineWidth; i++) {
                r = r - 1;
                double p = 5 / 4 - r;
                int x = 0, y = Convert.ToInt32(r);

                gl.Color(color);
                gl.LineWidth(lineWidth);
                gl.Begin(OpenGL.GL_POINTS);
                gl.Vertex(x + startPoint.X, gl.RenderContextProvider.Height - (y + startPoint.Y));
                this.addPointArr(new Point(x + startPoint.X, y + startPoint.Y));
                gl.Vertex(-x + startPoint.X, gl.RenderContextProvider.Height - (y + startPoint.Y));
                this.addPointArr(new Point(-x + startPoint.X, y + startPoint.Y));
                gl.Vertex(-y + startPoint.X, gl.RenderContextProvider.Height - (x + startPoint.Y));
                this.addPointArr(new Point(-y + startPoint.X, x + startPoint.Y));
                gl.Vertex(-y + startPoint.X, gl.RenderContextProvider.Height - (-x + startPoint.Y));
                this.addPointArr(new Point(-y + startPoint.X, -x + startPoint.Y));
                gl.Vertex(-x + startPoint.X, gl.RenderContextProvider.Height - (-y + startPoint.Y));
                this.addPointArr(new Point(-x + startPoint.X, -y + startPoint.Y));
                gl.Vertex(x + startPoint.X, gl.RenderContextProvider.Height - (-y + startPoint.Y));
                this.addPointArr(new Point(x + startPoint.X, -y + startPoint.Y));
                gl.Vertex(y + startPoint.X, gl.RenderContextProvider.Height - (-x + startPoint.Y));
                this.addPointArr(new Point(y + startPoint.X, -x + startPoint.Y));
                gl.Vertex(y + startPoint.X, gl.RenderContextProvider.Height - (x + startPoint.Y));
                this.addPointArr(new Point(y + startPoint.X, x + startPoint.Y));
                
                while (x < y) {
                    if (p < 0) {
                        p = p + 2 * x + 3;
                    } else {
                        p = p + 2 * (x - y) + 5;
                        y = y - 1;
                    }
                    x = x + 1;
                    gl.Vertex(x + startPoint.X, gl.RenderContextProvider.Height - (y + startPoint.Y));
                    this.addPointArr(new Point(x + startPoint.X, y + startPoint.Y));
                    gl.Vertex(-x + startPoint.X, gl.RenderContextProvider.Height - (y + startPoint.Y));
                    this.addPointArr(new Point(-x + startPoint.X, y + startPoint.Y));
                    gl.Vertex(-y + startPoint.X, gl.RenderContextProvider.Height - (x + startPoint.Y));
                    this.addPointArr(new Point(-y + startPoint.X, x + startPoint.Y));
                    gl.Vertex(-y + startPoint.X, gl.RenderContextProvider.Height - (-x + startPoint.Y));
                    this.addPointArr(new Point(-y + startPoint.X, -x + startPoint.Y));
                    gl.Vertex(-x + startPoint.X, gl.RenderContextProvider.Height - (-y + startPoint.Y));
                    this.addPointArr(new Point(-x + startPoint.X, -y + startPoint.Y));
                    gl.Vertex(x + startPoint.X, gl.RenderContextProvider.Height - (-y + startPoint.Y));
                    this.addPointArr(new Point(x + startPoint.X, -y + startPoint.Y));
                    gl.Vertex(y + startPoint.X, gl.RenderContextProvider.Height - (-x + startPoint.Y));
                    this.addPointArr(new Point(y + startPoint.X, -x + startPoint.Y));
                    gl.Vertex(y + startPoint.X, gl.RenderContextProvider.Height - (x + startPoint.Y));
                    this.addPointArr(new Point(y + startPoint.X, x + startPoint.Y));
                }
            }
            gl.End();
            gl.Flush();
            
        }
    }

    class Rectangle: Shape {
        public Rectangle(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw(bool erase = true) {
            base.Draw();
            this.pointArr = new ArrayList();
            this.controlPoints = new ArrayList();
            this.addControlPoint(startPoint);
            this.addControlPoint(endPoint);
            this.addControlPoint(new Point(endPoint.X,startPoint.Y));
            this.addControlPoint(new Point(startPoint.X, endPoint.Y));
            this.addControlPoint(new Point(startPoint.X + (endPoint.X - startPoint.X) / 2, startPoint.Y));
            this.addControlPoint(new Point(startPoint.X + (endPoint.X - startPoint.X) / 2, endPoint.Y));
            this.addControlPoint(new Point(startPoint.X, startPoint.Y + (endPoint.Y - startPoint.Y) / 2));
            this.addControlPoint(new Point(endPoint.X, startPoint.Y + (endPoint.Y - startPoint.Y) / 2));
            if (erase) {
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            }
            Line l1 = new Line(new Point(startPoint.X,startPoint.Y), new Point(endPoint.X,startPoint.Y), gl, color, lineWidth);
            Line l2 = new Line(new Point(endPoint.X, startPoint.Y), new Point(endPoint.X, endPoint.Y), gl, color, lineWidth);
            Line l3 = new Line(new Point(endPoint.X, endPoint.Y), new Point(startPoint.X, endPoint.Y), gl, color, lineWidth);
            Line l4 = new Line(new Point(startPoint.X, endPoint.Y), new Point(startPoint.X, startPoint.Y), gl, color, lineWidth);
            l1.Draw(false);
            l2.Draw(false);
            l3.Draw(false);
            l3.Draw(false);
            l4.Draw(false);
            this.pointArr.Add(l1.getPointArr());
            this.pointArr.Add(l2.getPointArr());
            this.pointArr.Add(l3.getPointArr());
            this.pointArr.Add(l4.getPointArr());
        }
    }

    class Ellipse: Shape {
        public Ellipse(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw(bool erase = true) {
            base.Draw();
            this.pointArr = new ArrayList();
            this.controlPoints = new ArrayList();
            if (erase) {
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            }
            double ry = Math.Abs(endPoint.Y - startPoint.Y);
            double rx = Math.Abs(endPoint.X - startPoint.X);
            this.addControlPoint(new Point(startPoint.X, Convert.ToInt32(startPoint.Y - ry)));
            this.addControlPoint(new Point(startPoint.X, Convert.ToInt32(startPoint.Y + ry)));
            this.addControlPoint(new Point(Convert.ToInt32(startPoint.X - rx), startPoint.Y));
            this.addControlPoint(new Point(Convert.ToInt32(startPoint.X + rx), startPoint.Y));
            this.addControlPoint(new Point(Convert.ToInt32(startPoint.X - rx), Convert.ToInt32(startPoint.Y - ry)));
            this.addControlPoint(new Point(Convert.ToInt32(startPoint.X + rx), Convert.ToInt32(startPoint.Y - ry)));
            this.addControlPoint(new Point(Convert.ToInt32(startPoint.X - rx), Convert.ToInt32(startPoint.Y + ry)));
            this.addControlPoint(new Point(Convert.ToInt32(startPoint.X + rx), Convert.ToInt32(startPoint.Y + ry)));


            //this.addControlPoint(endPoint);
            //this.addControlPoint(new Point(Convert.ToInt32(endPoint.X - 2 * rx), endPoint.Y));
            //this.addControlPoint(new Point(Convert.ToInt32(endPoint.X - 2 * rx), Convert.ToInt32(endPoint.Y - 2 * ry)));
            //this.addControlPoint(new Point(endPoint.X, Convert.ToInt32(endPoint.Y - 2 * ry)));
            for (int i = 0; i < lineWidth; i++) {
                ry = ry - 1;
                rx = rx - 1;
                //r2y -r2x .ry +1⁄4.r2x
                double p = Math.Pow(ry, 2) - Math.Pow(rx, 2) * ry + Math.Pow(rx, 2) * 1 / 4;
                int x = 0, y = Convert.ToInt32(ry);
                gl.Color(color);
                gl.LineWidth(lineWidth);
                gl.Begin(OpenGL.GL_POINTS);
                gl.Vertex(x + startPoint.X, gl.RenderContextProvider.Height - (y + startPoint.Y));
                this.addPointArr(new Point(x + startPoint.X, y + startPoint.Y));
                gl.Vertex(-x + startPoint.X, gl.RenderContextProvider.Height - (y + startPoint.Y));
                this.addPointArr(new Point(-x + startPoint.X, y + startPoint.Y));
                gl.Vertex(-x + startPoint.X, gl.RenderContextProvider.Height - (-y + startPoint.Y));
                this.addPointArr(new Point(-x + startPoint.X, -y + startPoint.Y));
                gl.Vertex(x + startPoint.X, gl.RenderContextProvider.Height - (-y + startPoint.Y));
                this.addPointArr(new Point(x + startPoint.X, -y + startPoint.Y));
                while (2 * Math.Pow(ry, 2) * x < 2 * Math.Pow(rx, 2) * y) {
                    if (p < 0) {
                        p = p + 2 * Math.Pow(ry, 2) * (x + 1) + Math.Pow(ry, 2);
                    } else {
                        p = p + 2 * Math.Pow(ry, 2) * (x + 1) - 2 * Math.Pow(rx, 2) * (y - 1) + Math.Pow(ry, 2);
                        y = y - 1;
                    }
                    x = x + 1;
                    gl.Vertex(x + startPoint.X, gl.RenderContextProvider.Height - (y + startPoint.Y));
                    this.addPointArr(new Point(x + startPoint.X, y + startPoint.Y));
                    gl.Vertex(-x + startPoint.X, gl.RenderContextProvider.Height - (y + startPoint.Y));
                    this.addPointArr(new Point(-x + startPoint.X, y + startPoint.Y));
                    gl.Vertex(-x + startPoint.X, gl.RenderContextProvider.Height - (-y + startPoint.Y));
                    this.addPointArr(new Point(-x + startPoint.X, -y + startPoint.Y));
                    gl.Vertex(x + startPoint.X, gl.RenderContextProvider.Height - (-y + startPoint.Y));
                    this.addPointArr(new Point(x + startPoint.X, -y + startPoint.Y));
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
                    this.addPointArr(new Point(x + startPoint.X, y + startPoint.Y));
                    gl.Vertex(-x + startPoint.X, gl.RenderContextProvider.Height - (y + startPoint.Y));
                    this.addPointArr(new Point(-x + startPoint.X, y + startPoint.Y));
                    gl.Vertex(-x + startPoint.X, gl.RenderContextProvider.Height - (-y + startPoint.Y));
                    this.addPointArr(new Point(-x + startPoint.X, -y + startPoint.Y));
                    gl.Vertex(x + startPoint.X, gl.RenderContextProvider.Height - (-y + startPoint.Y));
                    this.addPointArr(new Point(x + startPoint.X, -y + startPoint.Y));
                }
            }
            gl.End();
            gl.Flush();

        }
    }

    class EqTriagle: Shape {
        public EqTriagle(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }
        public override void Draw(bool erase = true) {
            base.Draw();
            this.pointArr = new ArrayList();
            this.controlPoints = new ArrayList();
            int bY = Convert.ToInt32(endPoint.Y - Math.Abs(endPoint.X - startPoint.X) * Math.Cos(30 * Math.PI / 180));
            int bX = Convert.ToInt32(startPoint.X + (endPoint.X - startPoint.X) * Math.Sin(30 * Math.PI / 180));
            if (erase) {
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            }
            this.addControlPoint(new Point(startPoint.X, endPoint.Y));
            this.addControlPoint(new Point(bX, bY));
            this.addControlPoint(new Point(endPoint.X, endPoint.Y));
            this.addControlPoint(new Point(startPoint.X + (endPoint.X - startPoint.X)/2,endPoint.Y));
            this.addControlPoint(new Point(startPoint.X, bY));
            this.addControlPoint(new Point(endPoint.X, bY));
            this.addControlPoint(new Point(startPoint.X, endPoint.Y - (endPoint.Y - bY) / 2));
            this.addControlPoint(new Point(endPoint.X, endPoint.Y - (endPoint.Y - bY) / 2));
            Line l1 = new Line(new Point(startPoint.X, endPoint.Y), new Point(bX, bY), gl, color, lineWidth);
            Line l2 = new Line(new Point(bX, bY), new Point(endPoint.X, endPoint.Y), gl, color, lineWidth);
            Line l3 = new Line(new Point(endPoint.X, endPoint.Y), new Point(startPoint.X, endPoint.Y), gl, color, lineWidth);
            l1.Draw(false);
            l2.Draw(false);
            l3.Draw(false);
            this.pointArr.Add(l1.getPointArr());
            this.pointArr.Add(l2.getPointArr());
            this.pointArr.Add(l3.getPointArr());

        }
    }

    class EqPentagon: Shape {
        public EqPentagon(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw(bool erase = true) {
            base.Draw();
            this.pointArr = new ArrayList();
            this.controlPoints = new ArrayList();
            double a = Math.Abs(endPoint.X - startPoint.X)*2*3/5;
            double R = a /(2*Math.Sin(36*Math.PI/180));
            double x = Math.Sin(72 * Math.PI / 180) * a;
            if (erase) {
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            }
            Point pTop = new Point(startPoint.X, Convert.ToInt32(endPoint.Y - x - Math.Abs(endPoint.X - startPoint.X) * Math.Sin(36 * Math.PI / 180)));
            Point p2 = new Point(startPoint.X + Math.Abs(endPoint.X - startPoint.X), Convert.ToInt32(endPoint.Y - x));
            Point p3 = new Point(Convert.ToInt32(startPoint.X + a / 2), endPoint.Y);
            Point p4 = new Point(Convert.ToInt32(startPoint.X - a / 2), endPoint.Y);
            Point p5 = new Point(Convert.ToInt32(startPoint.X - Math.Abs(endPoint.X - startPoint.X)),Convert.ToInt32(endPoint.Y - x));

            this.addControlPoint(pTop);
            this.addControlPoint(new Point(pTop.X, endPoint.Y));
            this.addControlPoint(new Point(pTop.X - (endPoint.X - pTop.X), pTop.Y + (endPoint.Y - pTop.Y) / 2));
            this.addControlPoint(new Point(pTop.X + (endPoint.X - pTop.X), pTop.Y + (endPoint.Y - pTop.Y) / 2));
            this.addControlPoint(new Point(endPoint.X, pTop.Y));
            this.addControlPoint(endPoint);
            this.addControlPoint(new Point(pTop.X - (endPoint.X - pTop.X), endPoint.Y));
            this.addControlPoint(new Point(pTop.X - (endPoint.X - pTop.X), pTop.Y));

            Line l1 = new Line(pTop, p2, gl, color, lineWidth);
            Line l2 = new Line(p2,p3, gl, color, lineWidth);
            Line l3 = new Line(p3,p4, gl, color, lineWidth);
            Line l4 = new Line(p4,p5, gl, color, lineWidth);
            Line l5 = new Line(p5, pTop, gl, color, lineWidth);
            l1.Draw(false);
            l2.Draw(false);
            l3.Draw(false);
            l4.Draw(false);
            l5.Draw(false);
            this.pointArr.Add(l1.getPointArr());
            this.pointArr.Add(l2.getPointArr());
            this.pointArr.Add(l3.getPointArr());
            this.pointArr.Add(l4.getPointArr());
            this.pointArr.Add(l5.getPointArr());
        }
    }

    class EqHexagon: Shape {
        public EqHexagon(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw(bool erase = true) {
            base.Draw();
            this.pointArr = new ArrayList();
            this.controlPoints = new ArrayList();
            double a = Math.Abs(endPoint.X - startPoint.X) * 2 * 2 / 4;
            double x = Math.Sin(60 * Math.PI / 180) * a;
            if (erase) {
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            }

            Point pLeft = new Point(startPoint.X - Math.Abs(endPoint.X - startPoint.X), startPoint.Y);
            Point p2 = new Point(Convert.ToInt32(startPoint.X - a / 2), Convert.ToInt32(startPoint.Y - x));
            Point p3 = new Point(Convert.ToInt32(startPoint.X + a / 2), Convert.ToInt32(startPoint.Y - x));
            Point pRight = new Point(startPoint.X + Math.Abs(endPoint.X - startPoint.X), startPoint.Y);
            Point p5 = new Point(Convert.ToInt32(startPoint.X + a / 2), Convert.ToInt32(startPoint.Y + x));
            Point p6 = new Point(Convert.ToInt32(startPoint.X - a / 2), Convert.ToInt32(startPoint.Y + x));
            Point pTop = new Point(p2.X + Convert.ToInt32((p3.X - p2.X) / 2), p2.Y);
            Point pBottom = new Point(p6.X + Convert.ToInt32((p5.X - p6.X) / 2), p6.Y);
            this.addControlPoint(pTop);
            this.addControlPoint(pBottom);
            this.addControlPoint(pLeft);
            this.addControlPoint(pRight);
            this.addControlPoint(new Point(pLeft.X, pTop.Y));
            this.addControlPoint(new Point(pLeft.X, pBottom.Y));
            this.addControlPoint(new Point(pRight.X, pTop.Y));
            this.addControlPoint(new Point(pRight.X, pBottom.Y));
            Line l1 = new Line(pLeft, p2, gl, color, lineWidth);
            Line l2 = new Line(p2, p3, gl, color, lineWidth);
            Line l3 = new Line(p3, pRight, gl, color, lineWidth);
            Line l4 = new Line(pRight, p5, gl, color, lineWidth);
            Line l5 = new Line(p5, p6, gl, color, lineWidth);
            Line l6 = new Line(p6, pLeft, gl, color, lineWidth);
            l1.Draw(false);
            l2.Draw(false);
            l3.Draw(false);
            l4.Draw(false);
            l5.Draw(false);
            l6.Draw(false);
            this.pointArr.Add(l1.getPointArr());
            this.pointArr.Add(l2.getPointArr());
            this.pointArr.Add(l3.getPointArr());
            this.pointArr.Add(l4.getPointArr());
            this.pointArr.Add(l5.getPointArr());
            this.pointArr.Add(l6.getPointArr());
        }
    }

    class Polygon {

        private ArrayList vertexArr;
        private ArrayList pointArr;
        private OpenGL gl;
        private float[] color;
        float lineWidth;

        public Polygon(ArrayList vertexArr, OpenGL gl, float[] color, float lineWidth) {
            this.vertexArr = vertexArr;
            this.gl = gl;
            this.color = color;
            this.lineWidth = lineWidth;
            this.pointArr = new ArrayList();
        }

        public ArrayList getPointArr() { return this.pointArr; }
        public void addPointArr(Point p) { this.pointArr.Add(p); }
        public ArrayList getVertexArr() { return vertexArr; }
        public void setLineArr(ArrayList vertexArr) { this.vertexArr = vertexArr; }
        public void addVertex(Point point) { this.vertexArr.Add(point); }
        public void removeLastVertex() { this.vertexArr.RemoveAt(vertexArr.Count - 1); }
        public OpenGL getGL(){return gl;}
        public void setGL(OpenGL gl){this.gl = gl;}
        public float getLineWidth(){return lineWidth;}
        public void setLineWidth(float lineWidth) { this.lineWidth = lineWidth; }
        public float[] getColor() { return color; }
        public void setColor(float[] color) { this.color = color; }

        public void Draw(bool erase = true) {
            this.pointArr = new ArrayList();
            if (erase) {
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            }
            gl.Color(color);
            gl.LineWidth(lineWidth);
            gl.Begin(OpenGL.GL_LINE_LOOP);
            Point[] ps = new Point[vertexArr.Count];
            vertexArr.CopyTo(ps);
            if (ps.Length > 0) {
                for (int i = 0; i < ps.Length - 1; i++) {
                    Line l = new Line(ps[i], ps[i + 1], gl, color, lineWidth);
                    l.Draw(false);
                    this.pointArr.Add(l.getPointArr());
                }
                Line lineLast = new Line(ps[ps.Length - 1], ps[0], gl, color, lineWidth);
                lineLast.Draw(false);
                this.pointArr.Add(lineLast.getPointArr());
            }
            gl.End();
            gl.Flush();
        }

        public void DrawControlPoint(bool erase = true, bool showControlPoint = true) {
            if (erase) {
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            }
            float[] controlColor = new float[] { 1f, 1f, 1f };
            if (!showControlPoint) {
                controlColor = new float[] { 0f, 0f, 0f };
            }
            gl.Color(controlColor);
            gl.LineWidth(lineWidth + 2);

            gl.Begin(OpenGL.GL_POINTS);
            foreach (Point p in vertexArr) {
                gl.Vertex(p.X, gl.RenderContextProvider.Height - (p.Y));
                gl.Vertex(p.X + 1, gl.RenderContextProvider.Height - (p.Y));
                gl.Vertex(p.X - 1, gl.RenderContextProvider.Height - (p.Y));
                gl.Vertex(p.X, gl.RenderContextProvider.Height - (p.Y + 1));
                gl.Vertex(p.X, gl.RenderContextProvider.Height - (p.Y - 1));
                gl.Vertex(p.X - 1, gl.RenderContextProvider.Height - (p.Y - 1));
                gl.Vertex(p.X + 1, gl.RenderContextProvider.Height - (p.Y + 1));
                gl.Vertex(p.X - 1, gl.RenderContextProvider.Height - (p.Y + 1));
                gl.Vertex(p.X + 1, gl.RenderContextProvider.Height - (p.Y - 1));
            }
            gl.End();
            gl.Flush();
        }
    }
}
