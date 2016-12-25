namespace Designer
{
    partial class Controller
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.listFurniture1 = new Designer.ListFurniture();
            this.view2D1 = new Designer.View2D();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.button1.Location = new System.Drawing.Point(12, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(170, 28);
            this.button1.TabIndex = 3;
            this.button1.Text = "3D";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.button2.Location = new System.Drawing.Point(188, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(178, 28);
            this.button2.TabIndex = 4;
            this.button2.Text = "2D Схема";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.textBox1.Location = new System.Drawing.Point(569, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(237, 26);
            this.textBox1.TabIndex = 8;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.button3.Location = new System.Drawing.Point(372, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(190, 28);
            this.button3.TabIndex = 4;
            this.button3.Text = "Меню";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // listFurniture1
            // 
            this.listFurniture1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.listFurniture1.Location = new System.Drawing.Point(569, 40);
            this.listFurniture1.Name = "listFurniture1";
            this.listFurniture1.Size = new System.Drawing.Size(279, 496);
            this.listFurniture1.TabIndex = 7;
            // 
            // view2D1
            // 
            this.view2D1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.view2D1.Location = new System.Drawing.Point(12, 40);
            this.view2D1.Name = "view2D1";
            this.view2D1.Size = new System.Drawing.Size(550, 496);
            this.view2D1.TabIndex = 9;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(812, 7);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(35, 27);
            this.button4.TabIndex = 10;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // Controller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 548);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.view2D1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listFurniture1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Controller";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private ListFurniture listFurniture1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private View2D view2D1;
        private System.Windows.Forms.Button button4;
    }
}

