using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Object;
using System.Collections;

namespace ComputerGraphicsPJ{
    public partial class Form1 : Form{

        private static int BUTTON_COUNT = 10;
        private static double PERCENT_GLHEIGHT = 0.8;
        enum DRAW_TYPE {
            LINE,
            CIRCLE,
            RECTANGLE,
            ELLIPSE,
            EQ_TRIANGLE,
            EQ_PENTAGON,
            EQ_HEXAGON,
            POLYGON
        }

        //enum LINE_WIDTH {
        //    W_1F,
        //    W_2F,
        //    W_3F
        //}

        private DRAW_TYPE currentDrawType = DRAW_TYPE.LINE;
        private Shape currentShape;
        private ArrayList arrCurrentShape = new ArrayList();
        private bool onPress = false;
        private bool onPolyDraw = false;
        private int timeStart = 0, timeEnd = 0;
        private Polygon currentPoly;

        public Form1(){
            InitializeComponent();
        }

        private void openGLControl_Load(object sender, EventArgs e){
            resizeUI();
            currentDrawType = DRAW_TYPE.LINE;
            currentShape = new Line(new Point(0, 0), new Point(0, 0), openGLControl.OpenGL, new float[] { 1f, 0, 0 }, 1f);
            hidePannelWidth();
            //buttonLineWidth.Image = System.Resources.ResourceManager.GetObjet("width");
            openGLControl.OpenGL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
        }
        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            resizeUI();
        }
        private void resizeUI() {
            //glControl
            openGLControl.Location = new Point(0, Convert.ToInt32(this.Height * (1 - PERCENT_GLHEIGHT)));
            openGLControl.Width = this.Width;
            openGLControl.Height = Convert.ToInt32(this.Height * PERCENT_GLHEIGHT);

            labelTime.Location = openGLControl.Location;

            //button
            resizeControHaft1(buttonLine, new Point(0, 0),true);
            resizeControHaft1(buttonCircle, new Point(buttonLine.Location.X + buttonLine.Width, 0), true);
            resizeControHaft1(buttonRectangle, new Point(buttonCircle.Location.X + buttonCircle.Width, 0), true);
            resizeControHaft1(buttonEllipse, new Point(buttonRectangle.Location.X + buttonRectangle.Width, 0), true);
            resizeControHaft1(buttonEqTriangle, new Point(buttonEllipse.Location.X + buttonEllipse.Width, 0), true);
            resizeControHaft1(buttonEqPentagon, new Point(buttonEqTriangle.Location.X + buttonEqTriangle.Width, 0), true);
            resizeControHaft1(buttonEqHexagon, new Point(buttonEqPentagon.Location.X + buttonEqPentagon.Width, 0), true);
            resizeControHaft1(buttonPolygon, new Point(buttonEqHexagon.Location.X + buttonEqHexagon.Width, 0),true);
            resizeControHaft1(buttonLineWidth, new Point(buttonPolygon.Location.X + buttonPolygon.Width, 0), true);
            resizeControHaft1(panelLineWidth, new Point(buttonLineWidth.Location.X, buttonLineWidth.Height),false);
            resizeControHaft1(pictureBoxColorPicker, new Point(buttonLineWidth.Location.X + buttonLineWidth.Width, 0), true);

            //button line width
            resizeControlInsidePannel(panelLineWidth, buttonWidth1f, new Point(0, 0), true, 4);
            resizeControlInsidePannel(panelLineWidth, buttonWidth3f, new Point(0, buttonWidth1f.Height), true, 4);
            resizeControlInsidePannel(panelLineWidth, buttonWidth5f, new Point(0, buttonWidth3f.Location.Y + buttonWidth3f.Height), true, 4);
            resizeControlInsidePannel(panelLineWidth, buttonWidth8f, new Point(0, buttonWidth5f.Location.Y + buttonWidth5f.Height), true, 4);

            
        }
        private void resizeControHaft1(Control button, Point location, bool autoSize) {
            location.X = location.X - 1;
            button.Location = location;
            if (autoSize) {
                button.Width = this.Width / BUTTON_COUNT;
                button.Height = Convert.ToInt32((this.Height - openGLControl.Height) * 0.5);
            }
        }
        private void resizeControHaft2(Control button, Point location, bool autoSize) {
            location.X = location.X - 1 + (this.Width/2);
            button.Location = location;
            if (autoSize) {
                button.Width = this.Width / BUTTON_COUNT;
                button.Height = Convert.ToInt32((this.Height - openGLControl.Height) * 0.5);
            }
        }

        private void resizeControlInsidePannel(Panel panel, Control control, Point location, bool autoSize, int controlCounts) {
            //location.X = location.X + panel.Location.X;
            control.Location = location;
            if (autoSize) {
                control.Width = panel.Width;
                control.Height = panel.Height / controlCounts;
            }
        }

        private void openGLControl_OpenGLInitialized(object sender, EventArgs e) {
            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Set the clear color.
            gl.ClearColor(0, 0, 0, 0);

            //  Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            //  Load the identity.
            gl.LoadIdentity();
        }

        private void openGLControl_Resized(object sender, EventArgs e) {
            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            //  Create a perspective transformation.
            gl.Viewport(0, 0, openGLControl.Width, openGLControl.Height);

            //  Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            //  Load the identity.
            gl.LoadIdentity();
            gl.Ortho2D(0, openGLControl.Width, 0, openGLControl.Height);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs e) {

        }

        private void openGLControl_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                currentPoly.addPoint(new Point(e.X, e.Y));
                onPolyDraw = false;
                return;
            }
            currentShape.setStartPoint(new Point(e.X,e.Y));
            currentShape.setStartPoint(new Point(e.X, e.Y));
            if (onPolyDraw) {
                currentPoly.addPoint(new Point(e.X, e.Y));
            } else {
                onPress = true;
                if (panelLineWidth.Visible == true) {
                    hidePannelWidth();
                }
            }
        }

        private void openGLControl_MouseMove(object sender, MouseEventArgs e) {
            timeStart = System.DateTime.Now.Millisecond;
            if (onPolyDraw) {
                currentPoly.addPoint(new Point(e.X, e.Y));
                currentPoly.Draw();
                currentPoly.removeLastPoint();
            } else {
                if (onPress) {
                    //currentShape.Erase();
                    currentShape.setEndPoint(new Point(e.X, e.Y));
                    currentShape.Draw();
                }
                
            }
            timeEnd = System.DateTime.Now.Millisecond;
            labelTime.Text = (timeEnd - timeStart) + " ms";
        }

        private void openGLControl_MouseUp(object sender, MouseEventArgs e) {
            if (onPolyDraw) {
                currentPoly.addPoint(new Point(e.X, e.Y));
                currentPoly.Draw();
            } else {
                onPress = false;
            }
            arrCurrentShape.Add(currentShape);
        }

        private void createNewShapeType(DRAW_TYPE type){
            switch(type){
                case DRAW_TYPE.LINE: {
                        currentDrawType = DRAW_TYPE.LINE;
                        currentShape = new Line(currentShape.getStartPoint(), currentShape.getEndPoint(), currentShape.getGL(), currentShape.getColor(), currentShape.getLineWidth());
                        break;
                    }
                case DRAW_TYPE.CIRCLE: {
                    currentDrawType = DRAW_TYPE.CIRCLE;
                        currentShape = new Circle(currentShape.getStartPoint(), currentShape.getEndPoint(), currentShape.getGL(), currentShape.getColor(), currentShape.getLineWidth());
                        break;
                    }
                case DRAW_TYPE.ELLIPSE: {
                        currentDrawType = DRAW_TYPE.ELLIPSE;
                        currentShape = new Ellipse(currentShape.getStartPoint(), currentShape.getEndPoint(), currentShape.getGL(), currentShape.getColor(), currentShape.getLineWidth());
                        break;
                    }
                case DRAW_TYPE.RECTANGLE: {
                        currentDrawType = DRAW_TYPE.RECTANGLE;
                        currentShape = new Object.Rectangle(currentShape.getStartPoint(), currentShape.getEndPoint(), currentShape.getGL(), currentShape.getColor(), currentShape.getLineWidth());
                        break;
                    }
                case DRAW_TYPE.EQ_TRIANGLE: {
                        currentDrawType = DRAW_TYPE.EQ_TRIANGLE;
                        currentShape = new EqTriagle(currentShape.getStartPoint(), currentShape.getEndPoint(), currentShape.getGL(), currentShape.getColor(), currentShape.getLineWidth());
                        break;
                    }
                case DRAW_TYPE.EQ_PENTAGON: {
                        currentDrawType = DRAW_TYPE.EQ_PENTAGON;
                        currentShape = new EqPentagon(currentShape.getStartPoint(), currentShape.getEndPoint(), currentShape.getGL(), currentShape.getColor(), currentShape.getLineWidth());
                        break;
                    }
                case DRAW_TYPE.EQ_HEXAGON: {
                        currentDrawType = DRAW_TYPE.EQ_HEXAGON;
                        currentShape = new EqHexagon(currentShape.getStartPoint(), currentShape.getEndPoint(), currentShape.getGL(), currentShape.getColor(), currentShape.getLineWidth());
                        break;
                    }
                case DRAW_TYPE.POLYGON: {
                    currentDrawType = DRAW_TYPE.POLYGON;
                    currentShape = new Line(currentShape.getStartPoint(), currentShape.getEndPoint(), currentShape.getGL(), currentShape.getColor(), currentShape.getLineWidth());
                    currentPoly = new Polygon(new ArrayList(), currentShape.getGL(), currentShape.getColor(), currentShape.getLineWidth());
                    break;
                   }
            }
        }

        //click
        private void buttonLine_Click(object sender, EventArgs e) {
            createNewShapeType(DRAW_TYPE.LINE);
        }

        private void buttonCircle_Click(object sender, EventArgs e) {
            createNewShapeType(DRAW_TYPE.CIRCLE);
            hidePannelWidth();
        }

        private void buttonRectangle_Click(object sender, EventArgs e) {
            createNewShapeType(DRAW_TYPE.RECTANGLE);
            hidePannelWidth();
        }

        private void buttonEllipse_Click(object sender, EventArgs e) {
            createNewShapeType(DRAW_TYPE.ELLIPSE);
            hidePannelWidth();
        }

        private void buttonEqTriangle_Click(object sender, EventArgs e) {
            createNewShapeType(DRAW_TYPE.EQ_TRIANGLE);
            hidePannelWidth();
        }

        private void buttonEqPentagon_Click(object sender, EventArgs e) {
            createNewShapeType(DRAW_TYPE.EQ_PENTAGON);
            hidePannelWidth();
        }

        private void buttonEqHexagon_Click(object sender, EventArgs e) {
            createNewShapeType(DRAW_TYPE.EQ_HEXAGON);
            hidePannelWidth();
        }

        private void buttonLineWidth_Click(object sender, EventArgs e) {
            if (panelLineWidth.Visible) {
                hidePannelWidth();
            } else {
                showPannelWidth();
            }
        }

        private void buttonWidth_Click(object sender, EventArgs e) {
            hidePannelWidth();
        }

        private void pictureBoxColorPicker_Click(object sender, EventArgs e) {
            hidePannelWidth();
            colorDialog.ShowDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK) {
                pictureBoxColorPicker.BackColor = colorDialog.Color;
                currentShape.setColor(new float[] { colorDialog.Color.R / 255f, colorDialog.Color.G / 255f, colorDialog.Color.B/255f });
                currentShape.Draw();

            } 
        }


        private void showPannelWidth() {

            if (currentShape.getLineWidth() == 1f) {
                buttonWidth1f.Select();
            } else if(currentShape.getLineWidth() == 3f) {
                buttonWidth3f.Select();
            } else if (currentShape.getLineWidth() == 5f) {
                buttonWidth5f.Select();
            } else if (currentShape.getLineWidth() == 8f) {
                buttonWidth8f.Select();
            }
            panelLineWidth.Visible = true;
        }

        private void hidePannelWidth(){
            panelLineWidth.Visible = false;
        }

        private void buttonWidth1f_MouseMove(object sender, MouseEventArgs e) {
            if (currentShape.getLineWidth() != 1f) {
                
                currentShape.setLineWidth(1f);
                if (currentDrawType == DRAW_TYPE.POLYGON) {
                    currentPoly.setLineWidth(1f);
                    currentPoly.Draw();
                } else {
                    //currentShape.Erase();
                    currentShape.Draw();
                }
            }
            
        }

        private void buttonWidth3f_MouseMove(object sender, MouseEventArgs e) {
            if (currentShape.getLineWidth() != 3f) {
                
                currentShape.setLineWidth(3f);
                if (currentDrawType == DRAW_TYPE.POLYGON) {
                    currentPoly.setLineWidth(3f);
                    currentPoly.Draw();
                } else {
                    //currentShape.Erase();
                    currentShape.Draw();
                }
            }
        }

        private void buttonWidth5f_MouseMove(object sender, MouseEventArgs e) {
            if (currentShape.getLineWidth() != 5f) {
                currentShape.setLineWidth(5f);
                if (currentDrawType == DRAW_TYPE.POLYGON) {
                    currentPoly.setLineWidth(5f);
                    currentPoly.Draw();
                } else {
                    //currentShape.Erase();
                    currentShape.Draw();
                }
            }
        }

        private void buttonWidth8f_MouseMove(object sender, MouseEventArgs e) {
            if (currentShape.getLineWidth() != 8f) {
                currentShape.setLineWidth(8f);
                if (currentDrawType == DRAW_TYPE.POLYGON) {
                    currentPoly.setLineWidth(8f);
                    currentPoly.Draw();
                } else {
                    //currentShape.Erase();
                    currentShape.Draw();
                }
            }
        }

        private void buttonPolygon_Click(object sender, EventArgs e) {
            createNewShapeType(DRAW_TYPE.POLYGON);
            hidePannelWidth();
            onPolyDraw = true;
        }
    }
}
