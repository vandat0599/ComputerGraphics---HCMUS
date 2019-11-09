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
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.buttonLineWidth = new System.Windows.Forms.Button();
            this.panelLineWidth = new System.Windows.Forms.Panel();
            this.buttonWidth8f = new System.Windows.Forms.Button();
            this.buttonWidth5f = new System.Windows.Forms.Button();
            this.buttonWidth3f = new System.Windows.Forms.Button();
            this.buttonWidth1f = new System.Windows.Forms.Button();
            this.labelTime = new System.Windows.Forms.Label();
            this.pictureBoxColorPicker = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
            this.panelLineWidth.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxColorPicker)).BeginInit();
            this.SuspendLayout();
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // openGLControl
            // 
            this.openGLControl.DrawFPS = false;
            this.openGLControl.Location = new System.Drawing.Point(-1, 159);
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
            this.buttonLine.Size = new System.Drawing.Size(163, 76);
            this.buttonLine.TabIndex = 1;
            this.buttonLine.Text = "Đoạn thẳng";
            this.buttonLine.UseVisualStyleBackColor = true;
            this.buttonLine.Click += new System.EventHandler(this.buttonLine_Click);
            // 
            // buttonCircle
            // 
            this.buttonCircle.Location = new System.Drawing.Point(168, -2);
            this.buttonCircle.Name = "buttonCircle";
            this.buttonCircle.Size = new System.Drawing.Size(141, 76);
            this.buttonCircle.TabIndex = 1;
            this.buttonCircle.Text = "Hình tròn";
            this.buttonCircle.UseVisualStyleBackColor = true;
            this.buttonCircle.Click += new System.EventHandler(this.buttonCircle_Click);
            // 
            // buttonRectangle
            // 
            this.buttonRectangle.Location = new System.Drawing.Point(315, -1);
            this.buttonRectangle.Name = "buttonRectangle";
            this.buttonRectangle.Size = new System.Drawing.Size(139, 76);
            this.buttonRectangle.TabIndex = 1;
            this.buttonRectangle.Text = "Chữ nhật";
            this.buttonRectangle.UseVisualStyleBackColor = true;
            this.buttonRectangle.Click += new System.EventHandler(this.buttonRectangle_Click);
            // 
            // buttonEllipse
            // 
            this.buttonEllipse.Location = new System.Drawing.Point(460, -1);
            this.buttonEllipse.Name = "buttonEllipse";
            this.buttonEllipse.Size = new System.Drawing.Size(165, 76);
            this.buttonEllipse.TabIndex = 1;
            this.buttonEllipse.Text = "Ellipse";
            this.buttonEllipse.UseVisualStyleBackColor = true;
            this.buttonEllipse.Click += new System.EventHandler(this.buttonEllipse_Click);
            // 
            // buttonEqTriangle
            // 
            this.buttonEqTriangle.Location = new System.Drawing.Point(631, -2);
            this.buttonEqTriangle.Name = "buttonEqTriangle";
            this.buttonEqTriangle.Size = new System.Drawing.Size(162, 76);
            this.buttonEqTriangle.TabIndex = 1;
            this.buttonEqTriangle.Text = "Tam giác đều";
            this.buttonEqTriangle.UseVisualStyleBackColor = true;
            this.buttonEqTriangle.Click += new System.EventHandler(this.buttonEqTriangle_Click);
            // 
            // buttonEqPentagon
            // 
            this.buttonEqPentagon.Location = new System.Drawing.Point(799, -2);
            this.buttonEqPentagon.Name = "buttonEqPentagon";
            this.buttonEqPentagon.Size = new System.Drawing.Size(132, 76);
            this.buttonEqPentagon.TabIndex = 1;
            this.buttonEqPentagon.Text = "Ngũ giác đều";
            this.buttonEqPentagon.UseVisualStyleBackColor = true;
            this.buttonEqPentagon.Click += new System.EventHandler(this.buttonEqPentagon_Click);
            // 
            // buttonEqHexagon
            // 
            this.buttonEqHexagon.Location = new System.Drawing.Point(937, -2);
            this.buttonEqHexagon.Name = "buttonEqHexagon";
            this.buttonEqHexagon.Size = new System.Drawing.Size(158, 76);
            this.buttonEqHexagon.TabIndex = 1;
            this.buttonEqHexagon.Text = "Lục giác đều";
            this.buttonEqHexagon.UseVisualStyleBackColor = true;
            this.buttonEqHexagon.Click += new System.EventHandler(this.buttonEqHexagon_Click);
            // 
            // buttonLineWidth
            // 
            this.buttonLineWidth.Location = new System.Drawing.Point(1104, -2);
            this.buttonLineWidth.Name = "buttonLineWidth";
            this.buttonLineWidth.Size = new System.Drawing.Size(160, 75);
            this.buttonLineWidth.TabIndex = 2;
            this.buttonLineWidth.Text = "Line width";
            this.buttonLineWidth.UseVisualStyleBackColor = true;
            this.buttonLineWidth.Click += new System.EventHandler(this.buttonLineWidth_Click);
            // 
            // panelLineWidth
            // 
            this.panelLineWidth.Controls.Add(this.buttonWidth8f);
            this.panelLineWidth.Controls.Add(this.buttonWidth5f);
            this.panelLineWidth.Controls.Add(this.buttonWidth3f);
            this.panelLineWidth.Controls.Add(this.buttonWidth1f);
            this.panelLineWidth.Location = new System.Drawing.Point(1101, 81);
            this.panelLineWidth.Name = "panelLineWidth";
            this.panelLineWidth.Size = new System.Drawing.Size(200, 320);
            this.panelLineWidth.TabIndex = 3;
            // 
            // buttonWidth8f
            // 
            this.buttonWidth8f.Location = new System.Drawing.Point(3, 226);
            this.buttonWidth8f.Name = "buttonWidth8f";
            this.buttonWidth8f.Size = new System.Drawing.Size(194, 61);
            this.buttonWidth8f.TabIndex = 0;
            this.buttonWidth8f.Text = "8f";
            this.buttonWidth8f.UseVisualStyleBackColor = true;
            this.buttonWidth8f.Click += new System.EventHandler(this.buttonWidth_Click);
            this.buttonWidth8f.MouseMove += new System.Windows.Forms.MouseEventHandler(this.buttonWidth8f_MouseMove);
            // 
            // buttonWidth5f
            // 
            this.buttonWidth5f.Location = new System.Drawing.Point(6, 159);
            this.buttonWidth5f.Name = "buttonWidth5f";
            this.buttonWidth5f.Size = new System.Drawing.Size(194, 61);
            this.buttonWidth5f.TabIndex = 0;
            this.buttonWidth5f.Text = "5f";
            this.buttonWidth5f.UseVisualStyleBackColor = true;
            this.buttonWidth5f.Click += new System.EventHandler(this.buttonWidth_Click);
            this.buttonWidth5f.MouseMove += new System.Windows.Forms.MouseEventHandler(this.buttonWidth5f_MouseMove);
            // 
            // buttonWidth3f
            // 
            this.buttonWidth3f.Location = new System.Drawing.Point(6, 81);
            this.buttonWidth3f.Name = "buttonWidth3f";
            this.buttonWidth3f.Size = new System.Drawing.Size(194, 61);
            this.buttonWidth3f.TabIndex = 0;
            this.buttonWidth3f.Text = "3f";
            this.buttonWidth3f.UseVisualStyleBackColor = true;
            this.buttonWidth3f.Click += new System.EventHandler(this.buttonWidth_Click);
            this.buttonWidth3f.MouseMove += new System.Windows.Forms.MouseEventHandler(this.buttonWidth3f_MouseMove);
            // 
            // buttonWidth1f
            // 
            this.buttonWidth1f.Location = new System.Drawing.Point(3, 3);
            this.buttonWidth1f.Name = "buttonWidth1f";
            this.buttonWidth1f.Size = new System.Drawing.Size(194, 60);
            this.buttonWidth1f.TabIndex = 0;
            this.buttonWidth1f.Text = "1f";
            this.buttonWidth1f.UseVisualStyleBackColor = true;
            this.buttonWidth1f.Click += new System.EventHandler(this.buttonWidth_Click);
            this.buttonWidth1f.MouseMove += new System.Windows.Forms.MouseEventHandler(this.buttonWidth1f_MouseMove);
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelTime.ForeColor = System.Drawing.Color.White;
            this.labelTime.Location = new System.Drawing.Point(12, 180);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(70, 25);
            this.labelTime.TabIndex = 4;
            this.labelTime.Text = "NNms";
            // 
            // pictureBoxColorPicker
            // 
            this.pictureBoxColorPicker.BackColor = System.Drawing.SystemColors.ControlText;
            this.pictureBoxColorPicker.Location = new System.Drawing.Point(1362, -2);
            this.pictureBoxColorPicker.Name = "pictureBoxColorPicker";
            this.pictureBoxColorPicker.Size = new System.Drawing.Size(82, 75);
            this.pictureBoxColorPicker.TabIndex = 6;
            this.pictureBoxColorPicker.TabStop = false;
            this.pictureBoxColorPicker.Click += new System.EventHandler(this.pictureBoxColorPicker_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2060, 752);
            this.Controls.Add(this.pictureBoxColorPicker);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.panelLineWidth);
            this.Controls.Add(this.buttonLineWidth);
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
            this.panelLineWidth.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxColorPicker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Button buttonLineWidth;
        private System.Windows.Forms.Panel panelLineWidth;
        private System.Windows.Forms.Button buttonWidth5f;
        private System.Windows.Forms.Button buttonWidth3f;
        private System.Windows.Forms.Button buttonWidth1f;
        private System.Windows.Forms.Button buttonWidth8f;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.PictureBox pictureBoxColorPicker;
    }
}

