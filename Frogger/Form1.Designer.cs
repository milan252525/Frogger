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
            this.level_slider = new System.Windows.Forms.TrackBar();
            this.level_easy = new System.Windows.Forms.Label();
            this.level_medium = new System.Windows.Forms.Label();
            this.level_hard = new System.Windows.Forms.Label();
            this.buttonHint = new System.Windows.Forms.Button();
            this.level_random = new System.Windows.Forms.Label();
            this.level_select = new System.Windows.Forms.Label();
            this.picture_game_logo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.level_slider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture_game_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // newGameButton
            // 
            this.newGameButton.Location = new System.Drawing.Point(364, 222);
            this.newGameButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.newGameButton.Name = "newGameButton";
            this.newGameButton.Size = new System.Drawing.Size(203, 57);
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
            this.label_lives_left.BackColor = System.Drawing.Color.Ivory;
            this.label_lives_left.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_lives_left.Location = new System.Drawing.Point(781, 0);
            this.label_lives_left.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_lives_left.Name = "label_lives_left";
            this.label_lives_left.Size = new System.Drawing.Size(147, 24);
            this.label_lives_left.TabIndex = 1;
            this.label_lives_left.Text = "LIVES LEFT: 5";
            this.label_lives_left.Visible = false;
            // 
            // level_slider
            // 
            this.level_slider.Cursor = System.Windows.Forms.Cursors.Default;
            this.level_slider.LargeChange = 1;
            this.level_slider.Location = new System.Drawing.Point(189, 514);
            this.level_slider.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.level_slider.Maximum = 3;
            this.level_slider.Name = "level_slider";
            this.level_slider.Size = new System.Drawing.Size(604, 56);
            this.level_slider.TabIndex = 1;
            // 
            // level_easy
            // 
            this.level_easy.AutoSize = true;
            this.level_easy.Location = new System.Drawing.Point(129, 574);
            this.level_easy.Name = "level_easy";
            this.level_easy.Size = new System.Drawing.Size(147, 34);
            this.level_easy.TabIndex = 2;
            this.level_easy.Text = "EASY\r\nORIGINAL FROGGER\r\n";
            this.level_easy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // level_medium
            // 
            this.level_medium.AutoSize = true;
            this.level_medium.Location = new System.Drawing.Point(339, 574);
            this.level_medium.Name = "level_medium";
            this.level_medium.Size = new System.Drawing.Size(118, 34);
            this.level_medium.TabIndex = 3;
            this.level_medium.Text = "MEDIUM\r\nBUSY HIGHWAY ";
            this.level_medium.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // level_hard
            // 
            this.level_hard.AutoSize = true;
            this.level_hard.Location = new System.Drawing.Point(533, 574);
            this.level_hard.Name = "level_hard";
            this.level_hard.Size = new System.Drawing.Size(90, 34);
            this.level_hard.TabIndex = 4;
            this.level_hard.Text = "HARD\r\nDEEP RIVER";
            this.level_hard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonHint
            // 
            this.buttonHint.Location = new System.Drawing.Point(364, 320);
            this.buttonHint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonHint.Name = "buttonHint";
            this.buttonHint.Size = new System.Drawing.Size(203, 57);
            this.buttonHint.TabIndex = 5;
            this.buttonHint.Text = "HOW TO PLAY";
            this.buttonHint.UseVisualStyleBackColor = true;
            this.buttonHint.Click += new System.EventHandler(this.buttonHint_Click);
            // 
            // level_random
            // 
            this.level_random.AutoSize = true;
            this.level_random.Location = new System.Drawing.Point(741, 574);
            this.level_random.Name = "level_random";
            this.level_random.Size = new System.Drawing.Size(69, 34);
            this.level_random.TabIndex = 6;
            this.level_random.Text = "RANDOM\r\nLEVEL\r\n";
            this.level_random.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // level_select
            // 
            this.level_select.AutoSize = true;
            this.level_select.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.level_select.Location = new System.Drawing.Point(128, 455);
            this.level_select.Name = "level_select";
            this.level_select.Size = new System.Drawing.Size(205, 29);
            this.level_select.TabIndex = 7;
            this.level_select.Text = "SELECT LEVEL:";
            this.level_select.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picture_game_logo
            // 
            this.picture_game_logo.Image = global::Frogger.Properties.Resources.game_logo;
            this.picture_game_logo.Location = new System.Drawing.Point(164, 48);
            this.picture_game_logo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picture_game_logo.Name = "picture_game_logo";
            this.picture_game_logo.Size = new System.Drawing.Size(624, 112);
            this.picture_game_logo.TabIndex = 8;
            this.picture_game_logo.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(939, 709);
            this.Controls.Add(this.picture_game_logo);
            this.Controls.Add(this.level_select);
            this.Controls.Add(this.level_random);
            this.Controls.Add(this.buttonHint);
            this.Controls.Add(this.level_hard);
            this.Controls.Add(this.level_medium);
            this.Controls.Add(this.level_easy);
            this.Controls.Add(this.level_slider);
            this.Controls.Add(this.label_lives_left);
            this.Controls.Add(this.newGameButton);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Frogger";
            ((System.ComponentModel.ISupportInitialize)(this.level_slider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture_game_logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button newGameButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_lives_left;
        private System.Windows.Forms.TrackBar level_slider;
        private System.Windows.Forms.Label level_easy;
        private System.Windows.Forms.Label level_medium;
        private System.Windows.Forms.Label level_hard;
        private System.Windows.Forms.Button buttonHint;
        private System.Windows.Forms.Label level_random;
        private System.Windows.Forms.Label level_select;
        private System.Windows.Forms.PictureBox picture_game_logo;
    }
}

