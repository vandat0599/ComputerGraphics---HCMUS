using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SharpGL;
using System.Collections;

namespace Object {

    class Constant {
        public const double EPSILON = 4d;
    }

    class Shape {
        protected Point startPoint;
        protected Point endPoint;
        protected OpenGL gl;
        protected float[] color;
        protected float lineWidth;
        protected ArrayList pointArr;
        protected ArrayList controlPoints;
        protected ArrayList vertexs;

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
        public ArrayList getVertexs() { return this.vertexs; }
        public void addVertexs(Point p) { this.vertexs.Add(p); }
        public bool isControlPointsContaint(Point p) {
            Point p1 = new Point(p.X, p.Y);
            Point p2 = new Point(p.X + 1, p.Y);
            Point p3 = new Point(p.X - 1, p.Y);
            Point p4 = new Point(p.X, p.Y + 1);
            Point p5 = new Point(p.X, p.Y - 1);
            Point p6 = new Point(p.X - 1, p.Y - 1);
            Point p7 = new Point(p.X + 1, p.Y + 1);
            Point p8 = new Point(p.X - 1, p.Y + 1);
            Point p9 = new Point(p.X + 1, p.Y - 1);
            return this.controlPoints.Contains(p1)
                || this.controlPoints.Contains(p2)
                || this.controlPoints.Contains(p3)
                || this.controlPoints.Contains(p4)
                || this.controlPoints.Contains(p5)
                || this.controlPoints.Contains(p6)
                || this.controlPoints.Contains(p7)
                || this.controlPoints.Contains(p8)
                || this.controlPoints.Contains(p9);
        }

        public virtual Point getCenterPoint() { return new Point(Convert.ToInt32((endPoint.X + startPoint.X) / 2), Convert.ToInt32((endPoint.Y + startPoint.Y) / 2)); }
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
            if (!showControlPoint) {
                this.Draw();
            }
        }
        public virtual bool havePointInLines(Point point) {
            double dMin = Double.MaxValue;
            foreach (Point p in pointArr) {
                double d = Math.Sqrt(Math.Pow(point.X - p.X, 2) + Math.Pow(point.Y - p.Y, 2));
                if (d < dMin) {
                    dMin = d;
                }
            }
            return dMin <= Constant.EPSILON;
        }

        public virtual bool havePointInside(Point point) {
            if (this.pointArr.Contains(point)) {
                return true;
            }
            bool result = false;
            Point[] polygon = new Point[this.pointArr.Count];
            this.pointArr.CopyTo(polygon);
            int j = polygon.Count() - 1;
            for (int i = 0; i < polygon.Count(); i++) {
                if (polygon[i].Y < point.Y && polygon[j].Y >= point.Y || polygon[j].Y < point.Y && polygon[i].Y >= point.Y) {
                    if (polygon[i].X + (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < point.X) {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }

        public virtual void onMove(Point start, Point end) {
            Point v = new Point(end.X - start.X, end.Y - start.Y);
            this.startPoint.X += v.X;
            this.startPoint.Y += v.Y;
            this.endPoint.X += v.X;
            this.endPoint.Y += v.Y;
            this.Draw(true);
            this.DrawControlPoint(false, true);
            this.startPoint.X -= v.X;
            this.startPoint.Y -= v.Y;
            this.endPoint.X -= v.X;
            this.endPoint.Y -= v.Y;

        }
    }

    class Line : Shape {
        public Line(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw(bool erase = true) {
            base.Draw();
            this.pointArr = new ArrayList();
            this.controlPoints = new ArrayList();
            this.vertexs = new ArrayList();
            this.addControlPoint(startPoint);
            this.addControlPoint(endPoint);
            this.addVertexs(startPoint);
            this.addVertexs(endPoint);
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

    class Circle : Shape {
        public Circle(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw(bool erase = true) {
            base.Draw();
            this.pointArr = new ArrayList();
            this.controlPoints = new ArrayList();
            this.vertexs = new ArrayList();
            if (erase) {
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            }
            double r = Math.Sqrt(Math.Pow(endPoint.X - startPoint.X, 2) + Math.Pow(endPoint.Y - startPoint.Y, 2));
            //double r = Math.Abs(endPoint.X - startPoint.X);
            this.addControlPoint(new Point(Convert.ToInt32(startPoint.X + r), startPoint.Y));
            this.addControlPoint(new Point(Convert.ToInt32(startPoint.X - r), startPoint.Y));
            this.addControlPoint(new Point(startPoint.X, Convert.ToInt32(startPoint.Y - r)));
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

        public override Point getCenterPoint() {
            return this.startPoint;
        }

        public override bool havePointInside(Point point) {
            if (this.pointArr.Contains(point)) {
                return true;
            }
            double r = Math.Sqrt(Math.Pow(endPoint.X - startPoint.X, 2) + Math.Pow(endPoint.Y - startPoint.Y, 2));
            double d = Math.Sqrt(Math.Pow(point.X - startPoint.X, 2) + Math.Pow(point.Y - startPoint.Y, 2));

            return d <= r;
        }
    }

    class Rectangle : Shape {
        public Rectangle(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw(bool erase = true) {
            base.Draw();
            this.pointArr = new ArrayList();
            this.controlPoints = new ArrayList();
            this.vertexs = new ArrayList();
            this.addControlPoint(startPoint);
            this.addControlPoint(endPoint);
            this.addControlPoint(new Point(endPoint.X, startPoint.Y));
            this.addControlPoint(new Point(startPoint.X, endPoint.Y));
            this.addControlPoint(new Point(startPoint.X + (endPoint.X - startPoint.X) / 2, startPoint.Y));
            this.addControlPoint(new Point(startPoint.X + (endPoint.X - startPoint.X) / 2, endPoint.Y));
            this.addControlPoint(new Point(startPoint.X, startPoint.Y + (endPoint.Y - startPoint.Y) / 2));
            this.addControlPoint(new Point(endPoint.X, startPoint.Y + (endPoint.Y - startPoint.Y) / 2));
            if (erase) {
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            }
            Point p1 = new Point(startPoint.X, startPoint.Y);
            Point p2 = new Point(endPoint.X, startPoint.Y);
            Point p3 = new Point(endPoint.X, endPoint.Y);
            Point p4 = new Point(startPoint.X, endPoint.Y);
            this.addVertexs(p1);
            this.addVertexs(p2);
            this.addVertexs(p3);
            this.addVertexs(p4);
            Line l1 = new Line(p1, p2, gl, color, lineWidth);
            Line l2 = new Line(p2, p3, gl, color, lineWidth);
            Line l3 = new Line(p3, p4, gl, color, lineWidth);
            Line l4 = new Line(p4, p1, gl, color, lineWidth);
            l1.Draw(false);
            l2.Draw(false);
            l3.Draw(false);
            l3.Draw(false);
            l4.Draw(false);
            this.pointArr.AddRange(l1.getPointArr());
            this.pointArr.AddRange(l2.getPointArr());
            this.pointArr.AddRange(l3.getPointArr());
            this.pointArr.AddRange(l4.getPointArr());
        }
    }

    class Ellipse : Shape {
        public Ellipse(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw(bool erase = true) {
            base.Draw();
            this.pointArr = new ArrayList();
            this.controlPoints = new ArrayList();
            this.vertexs = new ArrayList();
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

        public override Point getCenterPoint() {
            return this.startPoint;
        }

        public override bool havePointInside(Point point) {
            if (this.pointArr.Contains(point)) {
                return true;
            }
            double ry = Math.Abs(endPoint.Y - startPoint.Y);
            double rx = Math.Abs(endPoint.X - startPoint.X);
            double e1 = Math.Pow(point.X - startPoint.X, 2) / Math.Pow(rx, 2);
            double e2 = Math.Pow(point.Y - startPoint.Y, 2) / Math.Pow(ry, 2);
            return e1 + e2 <= 1f;
        }


    }

    class EqTriagle : Shape {
        public EqTriagle(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }
        public override void Draw(bool erase = true) {
            base.Draw();
            this.pointArr = new ArrayList();
            this.controlPoints = new ArrayList();
            this.vertexs = new ArrayList();
            int bY = Convert.ToInt32(endPoint.Y - Math.Abs(endPoint.X - startPoint.X) * Math.Cos(30 * Math.PI / 180));
            int bX = Convert.ToInt32(startPoint.X + (endPoint.X - startPoint.X) * Math.Sin(30 * Math.PI / 180));
            if (erase) {
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            }
            this.addControlPoint(new Point(startPoint.X, endPoint.Y));
            this.addControlPoint(new Point(bX, bY));
            this.addControlPoint(new Point(endPoint.X, endPoint.Y));
            this.addControlPoint(new Point(startPoint.X + (endPoint.X - startPoint.X) / 2, endPoint.Y));
            this.addControlPoint(new Point(startPoint.X, bY));
            this.addControlPoint(new Point(endPoint.X, bY));
            this.addControlPoint(new Point(startPoint.X, endPoint.Y - (endPoint.Y - bY) / 2));
            this.addControlPoint(new Point(endPoint.X, endPoint.Y - (endPoint.Y - bY) / 2));
            Point p1 = new Point(startPoint.X, endPoint.Y);
            Point p2 = new Point(bX, bY);
            Point p3 = new Point(endPoint.X, endPoint.Y);
            this.addVertexs(p1);
            this.addVertexs(p2);
            this.addVertexs(p3);
            Line l1 = new Line(p1, p2, gl, color, lineWidth);
            Line l2 = new Line(p2, p3, gl, color, lineWidth);
            Line l3 = new Line(p3, p1, gl, color, lineWidth);
            l1.Draw(false);
            l2.Draw(false);
            l3.Draw(false);
            this.pointArr.AddRange(l1.getPointArr());
            this.pointArr.AddRange(l2.getPointArr());
            this.pointArr.AddRange(l3.getPointArr());

        }

        public override Point getCenterPoint() {
            int bY = Convert.ToInt32(endPoint.Y - Math.Abs(endPoint.X - startPoint.X) * Math.Cos(30 * Math.PI / 180));
            int bX = Convert.ToInt32(startPoint.X + (endPoint.X - startPoint.X) * Math.Sin(30 * Math.PI / 180));
            return new Point(bX, (bY + endPoint.Y) / 2);
        }
    }

    class EqPentagon : Shape {
        public EqPentagon(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw(bool erase = true) {
            base.Draw();
            this.pointArr = new ArrayList();
            this.controlPoints = new ArrayList();
            this.vertexs = new ArrayList();
            double a = Math.Abs(endPoint.X - startPoint.X) * 2 * 3 / 5;
            double R = a / (2 * Math.Sin(36 * Math.PI / 180));
            double x = Math.Sin(72 * Math.PI / 180) * a;
            if (erase) {
                gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            }
            Point pTop = new Point(startPoint.X, Convert.ToInt32(endPoint.Y - x - Math.Abs(endPoint.X - startPoint.X) * Math.Sin(36 * Math.PI / 180)));
            Point p2 = new Point(startPoint.X + Math.Abs(endPoint.X - startPoint.X), Convert.ToInt32(endPoint.Y - x));
            Point p3 = new Point(Convert.ToInt32(startPoint.X + a / 2), endPoint.Y);
            Point p4 = new Point(Convert.ToInt32(startPoint.X - a / 2), endPoint.Y);
            Point p5 = new Point(Convert.ToInt32(startPoint.X - Math.Abs(endPoint.X - startPoint.X)), Convert.ToInt32(endPoint.Y - x));
            this.addVertexs(pTop);
            this.addVertexs(p2);
            this.addVertexs(p3);
            this.addVertexs(p4);
            this.addVertexs(p5);

            this.addControlPoint(pTop);
            this.addControlPoint(new Point(pTop.X, endPoint.Y));
            this.addControlPoint(new Point(pTop.X - (endPoint.X - pTop.X), pTop.Y + (endPoint.Y - pTop.Y) / 2));
            this.addControlPoint(new Point(pTop.X + (endPoint.X - pTop.X), pTop.Y + (endPoint.Y - pTop.Y) / 2));
            this.addControlPoint(new Point(endPoint.X, pTop.Y));
            this.addControlPoint(endPoint);
            this.addControlPoint(new Point(pTop.X - (endPoint.X - pTop.X), endPoint.Y));
            this.addControlPoint(new Point(pTop.X - (endPoint.X - pTop.X), pTop.Y));

            Line l1 = new Line(pTop, p2, gl, color, lineWidth);
            Line l2 = new Line(p2, p3, gl, color, lineWidth);
            Line l3 = new Line(p3, p4, gl, color, lineWidth);
            Line l4 = new Line(p4, p5, gl, color, lineWidth);
            Line l5 = new Line(p5, pTop, gl, color, lineWidth);
            l1.Draw(false);
            l2.Draw(false);
            l3.Draw(false);
            l4.Draw(false);
            l5.Draw(false);
            this.pointArr.AddRange(l1.getPointArr());
            this.pointArr.AddRange(l2.getPointArr());
            this.pointArr.AddRange(l3.getPointArr());
            this.pointArr.AddRange(l4.getPointArr());
            this.pointArr.AddRange(l5.getPointArr());
        }

        public override Point getCenterPoint() {
            double a = Math.Abs(endPoint.X - startPoint.X) * 2 * 3 / 5;
            double R = a / (2 * Math.Sin(36 * Math.PI / 180));
            double x = Math.Sin(72 * Math.PI / 180) * a;
            Point pTop = new Point(startPoint.X, Convert.ToInt32(endPoint.Y - x - Math.Abs(endPoint.X - startPoint.X) * Math.Sin(36 * Math.PI / 180)));
            return new Point(pTop.X, (pTop.Y + endPoint.Y) / 2);
        }
    }

    class EqHexagon : Shape {
        public EqHexagon(Point startPoint, Point endPoint, OpenGL gl, float[] color, float lineWidth) : base(startPoint, endPoint, gl, color, lineWidth) { }

        public override void Draw(bool erase = true) {
            base.Draw();
            this.pointArr = new ArrayList();
            this.controlPoints = new ArrayList();
            this.vertexs = new ArrayList();
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

            this.addVertexs(pLeft);
            this.addVertexs(p2);
            this.addVertexs(p3);
            this.addVertexs(pRight);
            this.addVertexs(p5);
            this.addVertexs(p6);
            this.addVertexs(pTop);
            this.addVertexs(pBottom);

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
            this.pointArr.AddRange(l1.getPointArr());
            this.pointArr.AddRange(l2.getPointArr());
            this.pointArr.AddRange(l3.getPointArr());
            this.pointArr.AddRange(l4.getPointArr());
            this.pointArr.AddRange(l5.getPointArr());
            this.pointArr.AddRange(l6.getPointArr());
        }

        public override Point getCenterPoint() {
            return this.startPoint;
        }
    }

    class Polygon {
        private ArrayList vertexs;
        private ArrayList pointArr;
        private OpenGL gl;
        private float[] color;
        float lineWidth;

        public Polygon(ArrayList vertexArr, OpenGL gl, float[] color, float lineWidth) {
            this.vertexs = vertexArr;
            this.gl = gl;
            this.color = color;
            this.lineWidth = lineWidth;
            this.pointArr = new ArrayList();
        }

        public ArrayList getPointArr() { return this.pointArr; }
        public void addPointArr(Point p) { this.pointArr.Add(p); }
        public ArrayList getVertexs() { return vertexs; }
        public void setLineArr(ArrayList vertexArr) { this.vertexs = vertexArr; }
        public void addVertex(Point point) { this.vertexs.Add(point); }
        public void removeLastVertex() { this.vertexs.RemoveAt(vertexs.Count - 1); }
        public OpenGL getGL() { return gl; }
        public void setGL(OpenGL gl) { this.gl = gl; }
        public float getLineWidth() { return lineWidth; }
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
            Point[] ps = new Point[vertexs.Count];
            vertexs.CopyTo(ps);
            if (ps.Length > 0) {
                for (int i = 0; i < ps.Length - 1; i++) {
                    Line l = new Line(ps[i], ps[i + 1], gl, color, lineWidth);
                    l.Draw(false);
                    this.pointArr.AddRange(l.getPointArr());
                }
                Line lineLast = new Line(ps[ps.Length - 1], ps[0], gl, color, lineWidth);
                lineLast.Draw(false);
                this.pointArr.AddRange(lineLast.getPointArr());
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
            foreach (Point p in vertexs) {
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
            //draw centerPoint
            Point centerPoint = getCenterPoint();
            gl.Vertex(centerPoint.X, gl.RenderContextProvider.Height - (centerPoint.Y));
            gl.End();
            gl.Flush();
            if (!showControlPoint) {
                this.Draw();
            }
        }

        public bool havePointInLines(Point point) {
            double dMin = Double.MaxValue;
            foreach (Point p in pointArr) {
                double d = Math.Sqrt(Math.Pow(point.X - p.X, 2) + Math.Pow(point.Y - p.Y, 2));
                if (d < dMin) {
                    dMin = d;
                }
            }
            return dMin <= Constant.EPSILON;
        }

        public bool havePointInside(Point point) {
            if (this.pointArr.Contains(point)) {
                return true;
            }
            bool result = false;
            Point[] polygon = new Point[this.pointArr.Count];
            this.pointArr.CopyTo(polygon);
            int j = polygon.Count() - 1;
            for (int i = 0; i < polygon.Count(); i++) {
                if (polygon[i].Y < point.Y && polygon[j].Y >= point.Y || polygon[j].Y < point.Y && polygon[i].Y >= point.Y) {
                    if (polygon[i].X + (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < point.X) {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }

        public Point getCenterPoint() {
            int num_points = this.vertexs.Count;
            Point[] pts = new Point[num_points + 1];
            this.vertexs.CopyTo(pts, 0);
            pts[num_points] = pts[0];
            // Find the centroid.
            float X = 0;
            float Y = 0;
            float second_factor;
            for (int i = 0; i < num_points; i++) {
                second_factor =
                    pts[i].X * pts[i + 1].Y -
                    pts[i + 1].X * pts[i].Y;
                X += (pts[i].X + pts[i + 1].X) * second_factor;
                Y += (pts[i].Y + pts[i + 1].Y) * second_factor;
            }

            // Divide by 6 times the polygon's area.
            float polygon_area = PolygonArea();
            X /= (6 * polygon_area);
            Y /= (6 * polygon_area);

            // If the values are negative, the polygon is
            // oriented counterclockwise so reverse the signs.
            if (X < 0) {
                X = -X;
                Y = -Y;
            }

            return new Point(Convert.ToInt32(X), Convert.ToInt32(Y));
        }

        private float PolygonArea() {
            // Return the absolute value of the signed area.
            // The signed area is negative if the polyogn is
            // oriented clockwise.
            return Math.Abs(SignedPolygonArea());
        }

        private float SignedPolygonArea() {
            // Add the first point to the end.
            int num_points = this.vertexs.Count;
            Point[] pts = new Point[num_points + 1];
            this.vertexs.CopyTo(pts, 0);
            pts[num_points] = pts[0];

            // Get the areas.
            float area = 0;
            for (int i = 0; i < num_points; i++) {
                area +=
                    (pts[i + 1].X - pts[i].X) *
                    (pts[i + 1].Y + pts[i].Y) / 2;
            }

            // Return the result.
            return area;
        }

        public bool isControlPointsContaint(Point p) {
            Point p1 = new Point(p.X, p.Y);
            Point p2 = new Point(p.X + 1, p.Y);
            Point p3 = new Point(p.X - 1, p.Y);
            Point p4 = new Point(p.X, p.Y + 1);
            Point p5 = new Point(p.X, p.Y - 1);
            Point p6 = new Point(p.X - 1, p.Y - 1);
            Point p7 = new Point(p.X + 1, p.Y + 1);
            Point p8 = new Point(p.X - 1, p.Y + 1);
            Point p9 = new Point(p.X + 1, p.Y - 1);
            return this.vertexs.Contains(p1)
                || this.vertexs.Contains(p2)
                || this.vertexs.Contains(p3)
                || this.vertexs.Contains(p4)
                || this.vertexs.Contains(p5)
                || this.vertexs.Contains(p6)
                || this.vertexs.Contains(p7)
                || this.vertexs.Contains(p8)
                || this.vertexs.Contains(p9);
        }
    }

    class Scanline {
        public ArrayList vertexArr;
        private OpenGL gl;
        private float[] color;
        float lineWidth;
        int xmin, ymin, xmax, ymax, edge;
        Point[] edge_arr = new Point[50];
        byte[][] arr_row_col;

        public Scanline(ArrayList vertexArr, OpenGL gl, float[] color, float lineWidth) {
            //init
            this.vertexArr = vertexArr;
            this.gl = gl;
            this.color = color;
            this.lineWidth = lineWidth;

            //copy to PointArr[]
            Point[] ps = new Point[vertexArr.Count + 1];
            vertexArr.CopyTo(ps);
            ps[vertexArr.Count] = ps[0];

            //update max min x y
            xmin = ps[0].X; ymin = ps[0].Y; xmax = xmin; ymax = ymin;
            for (int i = 0; i < vertexArr.Count + 1; i++) {
                if (this.xmin > ps[i].X) this.xmin = ps[i].X;
                if (this.ymin > ps[i].Y) this.ymin = ps[i].Y;
                if (this.xmax < ps[i].X) this.xmax = ps[i].X;
                if (this.ymax < ps[i].Y) this.ymax = ps[i].Y;
            }
        }
        public void setColor(float[] color) { this.color = color; }

        public float[] getColor() { return this.color; }

        public int getymin() { return ymin; }

        public int getymax() { return ymax; }

        void findedge_poly(int row, Point[] ps, Point[] ps_top, int count) {
            //init
            Point pointcheck = new Point(xmin, row);
            bool top = false;
            int col = xmin;
            //scan left -> right
            while (col <= xmax) {
                //found point
                if (arr_row_col[row][col] == 1) {
                    //init top point check
                    top = false;
                    while (arr_row_col[row][col] == 1) {
                        //create point
                        pointcheck = new Point(col, row);

                        //check top point
                        for (int i = 0; i < count; i++) {
                            if (ps_top[i] == pointcheck) {
                                top = true;
                                break;
                            }
                        }

                        //update col
                        col++;
                    }
                    //update arr_edge
                    if (top) {
                        edge++;
                        edge_arr[edge] = pointcheck;
                        edge++;
                        edge_arr[edge] = pointcheck;
                    } else {
                        edge++;
                        edge_arr[edge] = pointcheck;
                    }
                }
                //update col
                col++;
            }

            if (edge % 2 == 1) {
                for (int i = 2; i < edge; i++) {
                    edge_arr[i] = edge_arr[i + 1];
                }
                edge--;
            }
        }

        void findedge(int row, Point[] ps) {
            for (int col = xmin; col < xmax + 1; col++) {
                if (arr_row_col[row][col] == 1 && arr_row_col[row][col + 1] == 0) {
                    //point left
                    edge++;
                    edge_arr[edge] = new Point(col, row);

                    for (int col2 = col + 1; col2 < xmax + 1; col2++) {
                        if (arr_row_col[row][col2] == 1 && arr_row_col[row][col2 - 1] == 0) {
                            //point right
                            edge++;
                            edge_arr[edge] = new Point(col2, row);
                        }
                    }
                    break;
                }
            }
        }

        public void Draw(int count, ArrayList vertex, bool poly) {
            //init
            Point[] ps = new Point[vertexArr.Count];
            vertexArr.CopyTo(ps);   //  Points_array (x,y)

            Point[] ps_top = new Point[vertex.Count];
            vertex.CopyTo(ps_top);   //  TopPoints_array (x,y)

            //init array
            arr_row_col = new byte[ymax + 3][];   //dong truoc cot sau
            for (int i = 0; i < ymax + 3; i++)
                arr_row_col[i] = new byte[xmax + 3];

            //fill point -> array
            for (int i = 0; i < vertexArr.Count; i++) {
                //(ps[i].X, ps[i].Y)
                arr_row_col[ps[i].Y][ps[i].X] = 1;
            }

            //draw
            gl.Color(color);
            gl.LineWidth(lineWidth);
            gl.Begin(OpenGL.GL_LINES);
            //find edge of row
            for (int row = ymin + 1; row < count; row++) //scan up down
            {
                //init of each row
                edge = 0;

                //find edge
                if (poly) findedge_poly(row, ps, ps_top, vertex.Count);
                else findedge(row, ps);

                //draw = arrayEdge
                for (int k = 0; k < (edge / 2); k++) {
                    if (edge_arr[2 * k + 1] != edge_arr[2 * (k + 1)]) {
                        edge_arr[2 * k + 1].X++;
                        edge_arr[2 * (k + 1)].X--;
                        Line line = new Line(edge_arr[2 * k + 1], edge_arr[2 * (k + 1)], gl, color, lineWidth);
                        line.Draw(false);
                    }
                }
            }
            gl.End();
            gl.Flush();
        }
    }

    class Floodfill {
        public ArrayList vertexArr;
        private OpenGL gl;
        private float[] color;
        float lineWidth;
        int xmin, ymin, xmax, ymax, run;
        byte[][] arr_row_col;
        int[] stack_x, stack_y;
        int count_stack;

        public Floodfill(ArrayList vertexArr, OpenGL gl, float[] color, float lineWidth) {
            this.vertexArr = vertexArr;
            this.gl = gl;
            this.color = color;
            this.lineWidth = lineWidth;
            //copy to PointArr[]
            Point[] ps = new Point[vertexArr.Count + 1];
            vertexArr.CopyTo(ps);
            ps[vertexArr.Count] = ps[0];
            //update max min x y
            xmin = ps[0].X; ymin = ps[0].Y; xmax = xmin; ymax = ymin;
            for (int i = 0; i < vertexArr.Count + 1; i++) {
                if (this.xmin > ps[i].X) this.xmin = ps[i].X;
                if (this.ymin > ps[i].Y) this.ymin = ps[i].Y;
                if (this.xmax < ps[i].X) this.xmax = ps[i].X;
                if (this.ymax < ps[i].Y) this.ymax = ps[i].Y;
            }
        }

        public void setColor(float[] color) { this.color = color; }

        public float[] getColor() { return this.color; }

        public int getymin() { return ymin; }

        public int getymax() { return ymax; }

        public int getxmin() { return xmin; }

        public int getxmax() { return xmax; }

        void floodfill(int x, int y, int count) {
            //init stack
            int now_pos = 0;
            stack_x[now_pos] = x;
            stack_y[now_pos] = y;

            //fill color
            while (run < count) {
                //init
                x = stack_x[now_pos];
                y = stack_y[now_pos];

                //fill color (x,y)
                if (y <= ymax && x <= xmax && y >= ymin && x >= xmin && arr_row_col[y][x] != 1) {
                    gl.Vertex(x, gl.RenderContextProvider.Height - (y));
                    arr_row_col[y][x] = 1;
                }

                // CHECK 4 WAYS
                //(x + 1, y)
                if (y <= ymax && x + 1 <= xmax && y >= ymin && x + 1 >= xmin && arr_row_col[y][x + 1] != 1) {
                    count_stack++;
                    stack_x[count_stack] = x + 1;
                    stack_y[count_stack] = y;

                    gl.Vertex(x + 1, gl.RenderContextProvider.Height - (y));
                    arr_row_col[y][x + 1] = 1;
                }
                //(x - 1, y)
                if (y <= ymax && x - 1 <= xmax && y >= ymin && x - 1 >= xmin && arr_row_col[y][x - 1] != 1) {
                    count_stack++;
                    stack_x[count_stack] = x - 1;
                    stack_y[count_stack] = y;

                    gl.Vertex(x - 1, gl.RenderContextProvider.Height - (y));
                    arr_row_col[y][x - 1] = 1;
                }
                //(x, y + 1)
                if (y + 1 <= ymax && x <= xmax && y + 1 >= ymin && x >= xmin && arr_row_col[y + 1][x] != 1) {
                    count_stack++;
                    stack_x[count_stack] = x;
                    stack_y[count_stack] = y + 1;

                    gl.Vertex(x, gl.RenderContextProvider.Height - (y + 1));
                    arr_row_col[y + 1][x] = 1;
                }
                //(x, y - 1)
                if (y - 1 <= ymax && x <= xmax && y - 1 >= ymin && x >= xmin && arr_row_col[y - 1][x] != 1) {
                    count_stack++;
                    stack_x[count_stack] = x;
                    stack_y[count_stack] = y - 1;

                    gl.Vertex(x, gl.RenderContextProvider.Height - (y - 1));
                    arr_row_col[y - 1][x] = 1;
                }


                //stop loop
                if (now_pos == count_stack)
                    break;

                //update stack
                now_pos++;
                run++;
            }
        }

        public void Draw(int count, int x, int y) {
            //init
            run = 0;
            count_stack = 1;
            stack_x = new int[(ymax - ymin) * (xmax - xmin)];
            stack_y = new int[(ymax - ymin) * (xmax - xmin)];

            //get pixel of center point of control points of shape
            int centerx = x, centery = y;

            //array points of edge
            Point[] ps = new Point[vertexArr.Count + 1];
            vertexArr.CopyTo(ps);
            ps[vertexArr.Count] = ps[0];           //cot truoc dong sau

            //init array
            arr_row_col = new byte[ymax + 1][];   //dong truoc cot sau
            for (int i = 0; i < ymax + 1; i++)
                arr_row_col[i] = new byte[xmax + 1];

            //fill point -> array
            for (int i = 0; i < vertexArr.Count; i++) {
                //(ps[i].X, ps[i].Y)
                arr_row_col[ps[i].Y][ps[i].X] = 1;
            }

            //draw
            gl.Color(color);
            gl.Begin(OpenGL.GL_POINTS);

            //put a pixel
            floodfill(centerx, centery, count);
            //end put
            gl.End();
            gl.Flush();
        }
    }

}
