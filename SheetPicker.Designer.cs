namespace PrototypeV2
{
	partial class SheetPicker
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
			BtnConfirmSheet = new Button();
			LstSelect = new CheckedListBox();
			SuspendLayout();
			// 
			// BtnConfirmSheet
			// 
			BtnConfirmSheet.Location = new Point(12, 182);
			BtnConfirmSheet.Name = "BtnConfirmSheet";
			BtnConfirmSheet.Size = new Size(218, 29);
			BtnConfirmSheet.TabIndex = 1;
			BtnConfirmSheet.Text = "Select";
			BtnConfirmSheet.UseVisualStyleBackColor = true;
			BtnConfirmSheet.Click += BtnConfirmSheet_Click;
			// 
			// LstSelect
			// 
			LstSelect.CheckOnClick = true;
			LstSelect.FormattingEnabled = true;
			LstSelect.Location = new Point(12, 12);
			LstSelect.Name = "LstSelect";
			LstSelect.Size = new Size(218, 158);
			LstSelect.TabIndex = 2;
			LstSelect.ItemCheck += LstSelect_ItemCheck;
			LstSelect.SelectedIndexChanged += LstSelect_SelectedIndexChanged;
			// 
			// SheetPicker
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(242, 223);
			Controls.Add(LstSelect);
			Controls.Add(BtnConfirmSheet);
			Name = "SheetPicker";
			Text = "SheetPicker";
			ResumeLayout(false);
		}

		#endregion
		private Button BtnConfirmSheet;
		private CheckedListBox LstSelect;
	}
}