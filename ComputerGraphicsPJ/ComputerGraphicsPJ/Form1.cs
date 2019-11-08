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

namespace ComputerGraphicsPJ{
    public partial class Form1 : Form{

        private static int BUTTON_COUNT = 7;
        private static double PERCENT_GLHEIGHT = 0.9;
        enum DRAW_TYPE {
            LINE,
            CIRCLE,
            RECTANGLE,
            ELLIPSE,
            EQ_TRIANGLE,
            EQ_PENTAGON,
            EQ_HEXAGON
        }
        private DRAW_TYPE currentDrawType = DRAW_TYPE.LINE;
        private Shape currentShape;
        private bool onPress = false;
        private int currentMove, prevMove;

        public Form1(){
            InitializeComponent();
        }

        private void openGLControl_Load(object sender, EventArgs e){
            resizeUI();
            currentDrawType = DRAW_TYPE.LINE;
            currentShape = new Line(new Point(0, 0), new Point(0, 0), openGLControl.OpenGL, new float[] { 1f, 1f, 0, 0 }, 2f);
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

            //button
            resizeButton(buttonLine, new Point(0, 0));
            resizeButton(buttonCircle, new Point(buttonLine.Location.X + buttonLine.Width, 0));
            resizeButton(buttonRectangle, new Point(buttonCircle.Location.X + buttonCircle.Width, 0));
            resizeButton(buttonEllipse, new Point(buttonRectangle.Location.X + buttonRectangle.Width, 0));
            resizeButton(buttonEqTriangle, new Point(buttonEllipse.Location.X + buttonEllipse.Width, 0));
            resizeButton(buttonEqPentagon, new Point(buttonEqTriangle.Location.X + buttonEqTriangle.Width, 0));
            resizeButton(buttonEqHexagon, new Point(buttonEqPentagon.Location.X + buttonEqPentagon.Width, 0));
        }
        private void resizeButton(Button button, Point location) {
            button.Location = location;
            button.Width = this.Width / BUTTON_COUNT;
            button.Height = (this.Height - openGLControl.Height);
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
        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs e) {

        }

        private void openGLControl_MouseDown(object sender, MouseEventArgs e) {
            currentShape.setStartPoint(new Point(e.X,e.Y));
            currentShape.setStartPoint(new Point(e.X, e.Y));
            onPress = true;
            prevMove = currentShape.getStartPoint().X;
        }

        private void openGLControl_MouseMove(object sender, MouseEventArgs e) {
            if (onPress) {
                currentShape.setEngPoint(new Point(e.X, e.Y));
                currentShape.Draw();
            }
        }

        private void openGLControl_MouseUp(object sender, MouseEventArgs e) {
            onPress = false;
        }

        private void createNewShapeType(DRAW_TYPE type){
            switch(type){
                case DRAW_TYPE.LINE: {
                        currentDrawType = DRAW_TYPE.LINE;
                        currentShape = new Line(currentShape.getStartPoint(), currentShape.getEngPoint(), currentShape.getGL(), currentShape.getColor(), currentShape.getLineWidth());
                        break;
                    }
                case DRAW_TYPE.CIRCLE: {
                    currentDrawType = DRAW_TYPE.CIRCLE;
                        currentShape = new Circle(currentShape.getStartPoint(), currentShape.getEngPoint(), currentShape.getGL(), currentShape.getColor(), currentShape.getLineWidth());
                        break;
                    }
                case DRAW_TYPE.ELLIPSE: {
                        currentDrawType = DRAW_TYPE.ELLIPSE;
                        currentShape = new Ellipse(currentShape.getStartPoint(), currentShape.getEngPoint(), currentShape.getGL(), currentShape.getColor(), currentShape.getLineWidth());
                        break;
                    }
                case DRAW_TYPE.RECTANGLE: {
                        currentDrawType = DRAW_TYPE.RECTANGLE;
                        currentShape = new Object.Rectangle(currentShape.getStartPoint(), currentShape.getEngPoint(), currentShape.getGL(), currentShape.getColor(), currentShape.getLineWidth());
                        break;
                    }
                case DRAW_TYPE.EQ_TRIANGLE: {
                        currentDrawType = DRAW_TYPE.EQ_TRIANGLE;
                        currentShape = new EqTriagle(currentShape.getStartPoint(), currentShape.getEngPoint(), currentShape.getGL(), currentShape.getColor(), currentShape.getLineWidth());
                        break;
                    }
                case DRAW_TYPE.EQ_PENTAGON: {
                        currentDrawType = DRAW_TYPE.EQ_PENTAGON;
                        currentShape = new EqPentagon(currentShape.getStartPoint(), currentShape.getEngPoint(), currentShape.getGL(), currentShape.getColor(), currentShape.getLineWidth());
                        break;
                    }
                case DRAW_TYPE.EQ_HEXAGON: {
                        currentDrawType = DRAW_TYPE.EQ_HEXAGON;
                        currentShape = new EqHexagon(currentShape.getStartPoint(), currentShape.getEngPoint(), currentShape.getGL(), currentShape.getColor(), currentShape.getLineWidth());
                        break;
                    }

            }
        }

        private void buttonLine_Click(object sender, EventArgs e) {
            createNewShapeType(DRAW_TYPE.LINE);
        }

        private void buttonCircle_Click(object sender, EventArgs e) {
            createNewShapeType(DRAW_TYPE.CIRCLE);
        }

        private void buttonRectangle_Click(object sender, EventArgs e) {
            createNewShapeType(DRAW_TYPE.RECTANGLE);
        }

        private void buttonEllipse_Click(object sender, EventArgs e) {
            createNewShapeType(DRAW_TYPE.ELLIPSE);
        }

        private void buttonEqTriangle_Click(object sender, EventArgs e) {
            createNewShapeType(DRAW_TYPE.EQ_TRIANGLE);
        }

        private void buttonEqPentagon_Click(object sender, EventArgs e) {
            createNewShapeType(DRAW_TYPE.EQ_PENTAGON);
        }

        private void buttonEqHexagon_Click(object sender, EventArgs e) {
            createNewShapeType(DRAW_TYPE.EQ_HEXAGON);
        }

    }
}
