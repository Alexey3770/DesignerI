namespace Designer.Presentation.ControlListFur
{
    partial class ListView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.pnl_listFurs = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.listbut = new System.Windows.Forms.Button();
            this.btn_3d = new System.Windows.Forms.PictureBox();
            this.btn_2d = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btn_back = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_3d)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_2d)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_back)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label1.Location = new System.Drawing.Point(0, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 27);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_listFurs
            // 
            this.pnl_listFurs.AutoScroll = true;
            this.pnl_listFurs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_listFurs.Location = new System.Drawing.Point(0, 73);
            this.pnl_listFurs.Name = "pnl_listFurs";
            this.pnl_listFurs.Size = new System.Drawing.Size(277, 502);
            this.pnl_listFurs.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.listbut);
            this.panel1.Location = new System.Drawing.Point(1, 581);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(276, 140);
            this.panel1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label2.Location = new System.Drawing.Point(3, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(270, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listbut
            // 
            this.listbut.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.listbut.Location = new System.Drawing.Point(3, 105);
            this.listbut.Name = "listbut";
            this.listbut.Size = new System.Drawing.Size(268, 30);
            this.listbut.TabIndex = 0;
            this.listbut.UseVisualStyleBackColor = true;
            this.listbut.Visible = false;
            // 
            // btn_3d
            // 
            this.btn_3d.Image = global::Designer.Properties.Resources._3dbut;
            this.btn_3d.InitialImage = global::Designer.Properties.Resources.butback;
            this.btn_3d.Location = new System.Drawing.Point(210, 0);
            this.btn_3d.Name = "btn_3d";
            this.btn_3d.Size = new System.Drawing.Size(70, 45);
            this.btn_3d.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btn_3d.TabIndex = 2;
            this.btn_3d.TabStop = false;
            this.btn_3d.Click += new System.EventHandler(this.btn_3d_Click);
            // 
            // btn_2d
            // 
            this.btn_2d.Image = global::Designer.Properties.Resources._2dbuton;
            this.btn_2d.InitialImage = null;
            this.btn_2d.Location = new System.Drawing.Point(140, 0);
            this.btn_2d.Name = "btn_2d";
            this.btn_2d.Size = new System.Drawing.Size(70, 45);
            this.btn_2d.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btn_2d.TabIndex = 2;
            this.btn_2d.TabStop = false;
            this.btn_2d.Click += new System.EventHandler(this.btn_2d_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Designer.Properties.Resources.buthome;
            this.pictureBox2.InitialImage = global::Designer.Properties.Resources.butback;
            this.pictureBox2.Location = new System.Drawing.Point(70, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(70, 45);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // btn_back
            // 
            this.btn_back.Image = global::Designer.Properties.Resources.butback;
            this.btn_back.InitialImage = global::Designer.Properties.Resources.butback;
            this.btn_back.Location = new System.Drawing.Point(0, 0);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(70, 45);
            this.btn_back.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btn_back.TabIndex = 2;
            this.btn_back.TabStop = false;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // ListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnl_listFurs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_3d);
            this.Controls.Add(this.btn_2d);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btn_back);
            this.Name = "ListView";
            this.Size = new System.Drawing.Size(280, 724);
            this.Resize += new System.EventHandler(this.ListFurniture_Resize);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btn_3d)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_2d)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_back)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox btn_back;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnl_listFurs;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button listbut;
        private System.Windows.Forms.PictureBox btn_2d;
        private System.Windows.Forms.PictureBox btn_3d;
    }
}
