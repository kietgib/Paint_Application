namespace Midterm
{
    partial class Game
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
            this.Buttonbox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Buttonbox
            // 
            this.Buttonbox.FormattingEnabled = true;
            this.Buttonbox.Items.AddRange(new object[] {
            "Line",
            "Ellipse",
            "Retangle ",
            "Circle"});
            this.Buttonbox.Location = new System.Drawing.Point(12, 12);
            this.Buttonbox.Name = "Buttonbox";
            this.Buttonbox.Size = new System.Drawing.Size(121, 28);
            this.Buttonbox.TabIndex = 1;
            this.Buttonbox.SelectedIndexChanged += new System.EventHandler(this.Buttonbox_SelectedIndexChanged);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Buttonbox);
            this.Name = "Game";
            this.Text = "Game";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox Buttonbox;
    }
}