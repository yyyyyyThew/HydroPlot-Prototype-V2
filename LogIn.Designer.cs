namespace Prototype_V2
{
    partial class Home
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			btnLogin = new Button();
			btnCancel = new Button();
			txtPassword = new TextBox();
			txtUserID = new TextBox();
			SuspendLayout();
			// 
			// btnLogin
			// 
			btnLogin.Location = new Point(208, 244);
			btnLogin.Name = "btnLogin";
			btnLogin.Size = new Size(94, 29);
			btnLogin.TabIndex = 0;
			btnLogin.Text = "Enter";
			btnLogin.UseVisualStyleBackColor = true;
			btnLogin.Click += btnLogin_Click;
			// 
			// btnCancel
			// 
			btnCancel.Location = new Point(308, 244);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(94, 29);
			btnCancel.TabIndex = 1;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += btnCancel_Click;
			// 
			// txtPassword
			// 
			txtPassword.Location = new Point(208, 211);
			txtPassword.Name = "txtPassword";
			txtPassword.PasswordChar = '*';
			txtPassword.Size = new Size(194, 27);
			txtPassword.TabIndex = 2;
			txtPassword.TextChanged += txtPassword_TextChanged;
			// 
			// txtUserID
			// 
			txtUserID.Location = new Point(208, 178);
			txtUserID.Name = "txtUserID";
			txtUserID.Size = new Size(194, 27);
			txtUserID.TabIndex = 3;
			txtUserID.TextChanged += txtUserID_TextChanged;
			// 
			// Home
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(txtUserID);
			Controls.Add(txtPassword);
			Controls.Add(btnCancel);
			Controls.Add(btnLogin);
			Name = "Home";
			Text = "Home";
			Load += Home_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button btnLogin;
		private Button btnCancel;
		private TextBox txtPassword;
		private TextBox txtUserID;
	}
}
