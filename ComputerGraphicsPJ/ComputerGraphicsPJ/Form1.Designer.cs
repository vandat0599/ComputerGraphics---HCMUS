namespace ComputerGraphicsPJ
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.openGLControl = new SharpGL.OpenGLControl();
            this.buttonLine = new System.Windows.Forms.Button();
            this.buttonCircle = new System.Windows.Forms.Button();
            this.buttonRectangle = new System.Windows.Forms.Button();
            this.buttonEllipse = new System.Windows.Forms.Button();
            this.buttonEqTriangle = new System.Windows.Forms.Button();
            this.buttonEqPentagon = new System.Windows.Forms.Button();
            this.buttonEqHexagon = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.SuspendLayout();
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // openGLControl
            // 
            this.openGLControl.DrawFPS = false;
            this.openGLControl.Location = new System.Drawing.Point(116, 166);
            this.openGLControl.Margin = new System.Windows.Forms.Padding(0);
            this.openGLControl.Name = "openGLControl";
            this.openGLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl.Size = new System.Drawing.Size(1237, 576);
            this.openGLControl.TabIndex = 0;
            this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
            this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl_OpenGLDraw);
            this.openGLControl.Resized += new System.EventHandler(this.openGLControl_Resized);
            this.openGLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseDown);
            this.openGLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseMove);
            this.openGLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.openGLControl_MouseUp);
            // 
            // buttonLine
            // 
            this.buttonLine.Location = new System.Drawing.Point(-1, -1);
            this.buttonLine.Name = "buttonLine";
            this.buttonLine.Size = new System.Drawing.Size(302, 76);
            this.buttonLine.TabIndex = 1;
            this.buttonLine.Text = "Đoạn thẳng";
            this.buttonLine.UseVisualStyleBackColor = true;
            this.buttonLine.Click += new System.EventHandler(this.buttonLine_Click);
            // 
            // buttonCircle
            // 
            this.buttonCircle.Location = new System.Drawing.Point(307, -1);
            this.buttonCircle.Name = "buttonCircle";
            this.buttonCircle.Size = new System.Drawing.Size(302, 76);
            this.buttonCircle.TabIndex = 1;
            this.buttonCircle.Text = "Hình tròn";
            this.buttonCircle.UseVisualStyleBackColor = true;
            this.buttonCircle.Click += new System.EventHandler(this.buttonCircle_Click);
            // 
            // buttonRectangle
            // 
            this.buttonRectangle.Location = new System.Drawing.Point(615, -1);
            this.buttonRectangle.Name = "buttonRectangle";
            this.buttonRectangle.Size = new System.Drawing.Size(302, 76);
            this.buttonRectangle.TabIndex = 1;
            this.buttonRectangle.Text = "Chữ nhật";
            this.buttonRectangle.UseVisualStyleBackColor = true;
            this.buttonRectangle.Click += new System.EventHandler(this.buttonRectangle_Click);
            // 
            // buttonEllipse
            // 
            this.buttonEllipse.Location = new System.Drawing.Point(923, -1);
            this.buttonEllipse.Name = "buttonEllipse";
            this.buttonEllipse.Size = new System.Drawing.Size(302, 76);
            this.buttonEllipse.TabIndex = 1;
            this.buttonEllipse.Text = "Ellipse";
            this.buttonEllipse.UseVisualStyleBackColor = true;
            this.buttonEllipse.Click += new System.EventHandler(this.buttonEllipse_Click);
            // 
            // buttonEqTriangle
            // 
            this.buttonEqTriangle.Location = new System.Drawing.Point(-1, 81);
            this.buttonEqTriangle.Name = "buttonEqTriangle";
            this.buttonEqTriangle.Size = new System.Drawing.Size(302, 76);
            this.buttonEqTriangle.TabIndex = 1;
            this.buttonEqTriangle.Text = "Tam giác đều";
            this.buttonEqTriangle.UseVisualStyleBackColor = true;
            this.buttonEqTriangle.Click += new System.EventHandler(this.buttonEqTriangle_Click);
            // 
            // buttonEqPentagon
            // 
            this.buttonEqPentagon.Location = new System.Drawing.Point(307, 81);
            this.buttonEqPentagon.Name = "buttonEqPentagon";
            this.buttonEqPentagon.Size = new System.Drawing.Size(302, 76);
            this.buttonEqPentagon.TabIndex = 1;
            this.buttonEqPentagon.Text = "Ngũ giác đều";
            this.buttonEqPentagon.UseVisualStyleBackColor = true;
            this.buttonEqPentagon.Click += new System.EventHandler(this.buttonEqPentagon_Click);
            // 
            // buttonEqHexagon
            // 
            this.buttonEqHexagon.Location = new System.Drawing.Point(615, 81);
            this.buttonEqHexagon.Name = "buttonEqHexagon";
            this.buttonEqHexagon.Size = new System.Drawing.Size(302, 76);
            this.buttonEqHexagon.TabIndex = 1;
            this.buttonEqHexagon.Text = "Lục giác đều";
            this.buttonEqHexagon.UseVisualStyleBackColor = true;
            this.buttonEqHexagon.Click += new System.EventHandler(this.buttonEqHexagon_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1504, 752);
            this.Controls.Add(this.buttonEllipse);
            this.Controls.Add(this.buttonEqHexagon);
            this.Controls.Add(this.buttonRectangle);
            this.Controls.Add(this.buttonEqPentagon);
            this.Controls.Add(this.buttonCircle);
            this.Controls.Add(this.buttonEqTriangle);
            this.Controls.Add(this.buttonLine);
            this.Controls.Add(this.openGLControl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.openGLControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Diagnostics.EventLog eventLog1;
        private SharpGL.OpenGLControl openGLControl;
        private System.Windows.Forms.Button buttonLine;
        private System.Windows.Forms.Button buttonEllipse;
        private System.Windows.Forms.Button buttonEqHexagon;
        private System.Windows.Forms.Button buttonRectangle;
        private System.Windows.Forms.Button buttonEqPentagon;
        private System.Windows.Forms.Button buttonCircle;
        private System.Windows.Forms.Button buttonEqTriangle;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

