namespace Designer
{
    partial class ElementFurList
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
            this.components = new System.ComponentModel.Container();
            this.timerEnter = new System.Windows.Forms.Timer(this.components);
            this.timerLeave = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timerEnter
            // 
            this.timerEnter.Interval = 10;
            this.timerEnter.Tick += new System.EventHandler(this.timerEnter_Tick);
            // 
            // timerLeave
            // 
            this.timerLeave.Interval = 10;
            this.timerLeave.Tick += new System.EventHandler(this.timerLeave_Tick);
            // 
            // ElementFurList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ElementFurList";
            this.Size = new System.Drawing.Size(225, 231);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerEnter;
        private System.Windows.Forms.Timer timerLeave;
    }
}
