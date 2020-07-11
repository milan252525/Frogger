namespace Frogger
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
            this.components = new System.ComponentModel.Container();
            this.newGameButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label_lives_left = new System.Windows.Forms.Label();
            this.levelSelector = new System.Windows.Forms.TrackBar();
            this.level_easy = new System.Windows.Forms.Label();
            this.level_medium = new System.Windows.Forms.Label();
            this.level_hard = new System.Windows.Forms.Label();
            this.buttonHint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.levelSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // newGameButton
            // 
            this.newGameButton.Location = new System.Drawing.Point(364, 137);
            this.newGameButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.newGameButton.Name = "newGameButton";
            this.newGameButton.Size = new System.Drawing.Size(202, 57);
            this.newGameButton.TabIndex = 0;
            this.newGameButton.Text = "NEW GAME";
            this.newGameButton.UseVisualStyleBackColor = true;
            this.newGameButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 130;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label_lives_left
            // 
            this.label_lives_left.AutoSize = true;
            this.label_lives_left.BackColor = System.Drawing.Color.Transparent;
            this.label_lives_left.Location = new System.Drawing.Point(784, 11);
            this.label_lives_left.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_lives_left.Name = "label_lives_left";
            this.label_lives_left.Size = new System.Drawing.Size(0, 17);
            this.label_lives_left.TabIndex = 1;
            // 
            // levelSelector
            // 
            this.levelSelector.LargeChange = 1;
            this.levelSelector.Location = new System.Drawing.Point(229, 383);
            this.levelSelector.Maximum = 2;
            this.levelSelector.Name = "levelSelector";
            this.levelSelector.Size = new System.Drawing.Size(456, 56);
            this.levelSelector.TabIndex = 1;
            // 
            // level_easy
            // 
            this.level_easy.AutoSize = true;
            this.level_easy.Location = new System.Drawing.Point(160, 442);
            this.level_easy.Name = "level_easy";
            this.level_easy.Size = new System.Drawing.Size(169, 34);
            this.level_easy.TabIndex = 2;
            this.level_easy.Text = "EASY\r\nOLDSCHOOL FROGGER\r\n";
            this.level_easy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // level_medium
            // 
            this.level_medium.AutoSize = true;
            this.level_medium.Location = new System.Drawing.Point(399, 442);
            this.level_medium.Name = "level_medium";
            this.level_medium.Size = new System.Drawing.Size(118, 34);
            this.level_medium.TabIndex = 3;
            this.level_medium.Text = "MEDIUM\r\nBUSY HIGHWAY ";
            this.level_medium.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.level_medium.Click += new System.EventHandler(this.level_medium_Click);
            // 
            // level_hard
            // 
            this.level_hard.AutoSize = true;
            this.level_hard.Location = new System.Drawing.Point(622, 442);
            this.level_hard.Name = "level_hard";
            this.level_hard.Size = new System.Drawing.Size(90, 34);
            this.level_hard.TabIndex = 4;
            this.level_hard.Text = "HARD\r\nDEEP RIVER";
            this.level_hard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonHint
            // 
            this.buttonHint.Location = new System.Drawing.Point(364, 218);
            this.buttonHint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonHint.Name = "buttonHint";
            this.buttonHint.Size = new System.Drawing.Size(202, 57);
            this.buttonHint.TabIndex = 5;
            this.buttonHint.Text = "HOW TO PLAY";
            this.buttonHint.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 709);
            this.Controls.Add(this.buttonHint);
            this.Controls.Add(this.level_hard);
            this.Controls.Add(this.level_medium);
            this.Controls.Add(this.level_easy);
            this.Controls.Add(this.levelSelector);
            this.Controls.Add(this.label_lives_left);
            this.Controls.Add(this.newGameButton);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Frogger";
            ((System.ComponentModel.ISupportInitialize)(this.levelSelector)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button newGameButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_lives_left;
        private System.Windows.Forms.TrackBar levelSelector;
        private System.Windows.Forms.Label level_easy;
        private System.Windows.Forms.Label level_medium;
        private System.Windows.Forms.Label level_hard;
        private System.Windows.Forms.Button buttonHint;
    }
}

