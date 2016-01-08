namespace Rozumnyky
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
            this.buttonFFF = new System.Windows.Forms.Button();
            this.buttonMainGame = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonFFF
            // 
            this.buttonFFF.Location = new System.Drawing.Point(29, 378);
            this.buttonFFF.Name = "buttonFFF";
            this.buttonFFF.Size = new System.Drawing.Size(342, 23);
            this.buttonFFF.TabIndex = 1;
            this.buttonFFF.Text = "Відбірковий тур";
            this.buttonFFF.UseVisualStyleBackColor = true;
            this.buttonFFF.Click += new System.EventHandler(this.buttonFFF_Click);
            // 
            // buttonMainGame
            // 
            this.buttonMainGame.Location = new System.Drawing.Point(29, 415);
            this.buttonMainGame.Name = "buttonMainGame";
            this.buttonMainGame.Size = new System.Drawing.Size(342, 23);
            this.buttonMainGame.TabIndex = 2;
            this.buttonMainGame.Text = "Основна гра";
            this.buttonMainGame.UseVisualStyleBackColor = true;
            this.buttonMainGame.Click += new System.EventHandler(this.buttonMainGame_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(29, 464);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(342, 23);
            this.buttonExit.TabIndex = 3;
            this.buttonExit.Text = "Вихід";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Rozumnyky.Properties.Resources.LOGO_CDR;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(409, 345);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(405, 502);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonMainGame);
            this.Controls.Add(this.buttonFFF);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Головне меню";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonFFF;
        private System.Windows.Forms.Button buttonMainGame;
        private System.Windows.Forms.Button buttonExit;
    }
}

