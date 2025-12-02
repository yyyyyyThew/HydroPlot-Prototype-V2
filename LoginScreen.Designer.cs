namespace Prototype_V2
{
	partial class LoginScreen
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
			txtUserID = new TextBox();
			txtPassword = new TextBox();
			btnCancel = new Button();
			btnLogin = new Button();
			SuspendLayout();
			// 
			// txtUserID
			// 
			txtUserID.Location = new Point(303, 178);
			txtUserID.Name = "txtUserID";
			txtUserID.PlaceholderText = "User Name";
			txtUserID.Size = new Size(194, 27);
			txtUserID.TabIndex = 7;
			txtUserID.TextChanged += txtUserID_TextChanged;
			// 
			// txtPassword
			// 
			txtPassword.Location = new Point(303, 211);
			txtPassword.Name = "txtPassword";
			txtPassword.PasswordChar = '*';
			txtPassword.PlaceholderText = "Password";
			txtPassword.Size = new Size(194, 27);
			txtPassword.TabIndex = 6;
			txtPassword.TextChanged += txtPassword_TextChanged;
			// 
			// btnCancel
			// 
			btnCancel.Location = new Point(403, 244);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(94, 29);
			btnCancel.TabIndex = 5;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += btnCancel_Click;
			// 
			// btnLogin
			// 
			btnLogin.Location = new Point(303, 244);
			btnLogin.Name = "btnLogin";
			btnLogin.Size = new Size(94, 29);
			btnLogin.TabIndex = 4;
			btnLogin.Text = "Enter";
			btnLogin.UseVisualStyleBackColor = true;
			btnLogin.Click += btnLogin_Click;
			// 
			// LoginScreen
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(txtUserID);
			Controls.Add(txtPassword);
			Controls.Add(btnCancel);
			Controls.Add(btnLogin);
			Name = "LoginScreen";
			Text = "LoginScreen";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox txtUserID;
		private TextBox txtPassword;
		private Button btnCancel;
		private Button btnLogin;
	}
}