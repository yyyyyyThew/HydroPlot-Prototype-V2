namespace Prototype_V2
{
	partial class SettingsForm
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
			XMLContents = new RichTextBox();
			btnInsert = new Button();
			txtUserInput = new TextBox();
			txtConnectionName = new TextBox();
			textBox1 = new TextBox();
			SuspendLayout();
			// 
			// XMLContents
			// 
			XMLContents.Location = new Point(12, 58);
			XMLContents.Name = "XMLContents";
			XMLContents.Size = new Size(776, 380);
			XMLContents.TabIndex = 0;
			XMLContents.Text = "";
			XMLContents.TextChanged += XMLContents_TextChanged;
			// 
			// btnInsert
			// 
			btnInsert.Location = new Point(12, 12);
			btnInsert.Name = "btnInsert";
			btnInsert.Size = new Size(112, 34);
			btnInsert.TabIndex = 1;
			btnInsert.Text = "Insert";
			btnInsert.UseVisualStyleBackColor = true;
			btnInsert.Click += btnInsert_Click;
			// 
			// txtUserInput
			// 
			txtUserInput.Location = new Point(130, 12);
			txtUserInput.Name = "txtUserInput";
			txtUserInput.Size = new Size(306, 31);
			txtUserInput.TabIndex = 2;
			txtUserInput.Text = "Connection String";
			txtUserInput.TextChanged += txtUserInput_TextChanged;
			// 
			// txtConnectionName
			// 
			txtConnectionName.Location = new Point(442, 12);
			txtConnectionName.Name = "txtConnectionName";
			txtConnectionName.Size = new Size(150, 31);
			txtConnectionName.TabIndex = 3;
			txtConnectionName.Text = "Name";
			txtConnectionName.TextChanged += txtConnectionName_TextChanged;
			// 
			// textBox1
			// 
			textBox1.Location = new Point(598, 12);
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(190, 31);
			textBox1.TabIndex = 4;
			textBox1.TextChanged += textBox1_TextChanged;
			// 
			// SettingsForm
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(textBox1);
			Controls.Add(txtConnectionName);
			Controls.Add(txtUserInput);
			Controls.Add(btnInsert);
			Controls.Add(XMLContents);
			Name = "SettingsForm";
			Text = "SettingsForm";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private RichTextBox XMLContents;
		private Button btnInsert;
		private TextBox txtUserInput;
		private TextBox txtConnectionName;
		private TextBox textBox1;
	}
}