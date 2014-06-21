namespace Noriy
{
    partial class Logout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Logout));
            this.passTextBox = new System.Windows.Forms.TextBox();
            this.passLabel = new System.Windows.Forms.Label();
            this.mssgLabel = new System.Windows.Forms.Label();
            this.userLabel = new System.Windows.Forms.Label();
            this.submitButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // passTextBox
            // 
            this.passTextBox.BackColor = System.Drawing.Color.SeaShell;
            this.passTextBox.Location = new System.Drawing.Point(113, 125);
            this.passTextBox.Name = "passTextBox";
            this.passTextBox.PasswordChar = '*';
            this.passTextBox.Size = new System.Drawing.Size(224, 27);
            this.passTextBox.TabIndex = 0;
            // 
            // passLabel
            // 
            this.passLabel.AutoSize = true;
            this.passLabel.BackColor = System.Drawing.Color.Transparent;
            this.passLabel.Location = new System.Drawing.Point(12, 128);
            this.passLabel.Name = "passLabel";
            this.passLabel.Size = new System.Drawing.Size(95, 18);
            this.passLabel.TabIndex = 1;
            this.passLabel.Text = "Password";
            // 
            // mssgLabel
            // 
            this.mssgLabel.AutoSize = true;
            this.mssgLabel.BackColor = System.Drawing.Color.Transparent;
            this.mssgLabel.Location = new System.Drawing.Point(13, 9);
            this.mssgLabel.Name = "mssgLabel";
            this.mssgLabel.Size = new System.Drawing.Size(281, 18);
            this.mssgLabel.TabIndex = 2;
            this.mssgLabel.Text = "Please enter the password for:";
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.BackColor = System.Drawing.Color.Transparent;
            this.userLabel.Location = new System.Drawing.Point(13, 40);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(98, 18);
            this.userLabel.TabIndex = 3;
            this.userLabel.Text = "Username";
            // 
            // submitButton
            // 
            this.submitButton.BackColor = System.Drawing.Color.SeaShell;
            this.submitButton.BackgroundImage = global::Noriy.Properties.Resources._11;
            this.submitButton.Location = new System.Drawing.Point(137, 185);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(111, 37);
            this.submitButton.TabIndex = 4;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = false;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // Logout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(142)))), ((int)(((byte)(147)))));
            this.BackgroundImage = global::Noriy.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.mssgLabel);
            this.Controls.Add(this.passLabel);
            this.Controls.Add(this.passTextBox);
            this.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.MaximumSize = new System.Drawing.Size(400, 300);
            this.Name = "Logout";
            this.Text = "Logout";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox passTextBox;
        private System.Windows.Forms.Label passLabel;
        private System.Windows.Forms.Label mssgLabel;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.Button submitButton;
    }
}