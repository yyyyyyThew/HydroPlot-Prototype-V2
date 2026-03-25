namespace Prototype_V2
{
	partial class GraphView
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
			GV_FullscreenView = new ScottPlot.WinForms.FormsPlot();
			btn_SavePng = new Button();
			btn_Leave = new Button();
			SAV_SaveFile = new SaveFileDialog();
			btn_Export = new Button();
			SAV_ExportTable = new SaveFileDialog();
			SuspendLayout();
			// 
			// GV_FullscreenView
			// 
			GV_FullscreenView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			GV_FullscreenView.DisplayScale = 1.5F;
			GV_FullscreenView.Location = new Point(12, 52);
			GV_FullscreenView.Name = "GV_FullscreenView";
			GV_FullscreenView.Size = new Size(1874, 960);
			GV_FullscreenView.TabIndex = 0;
			GV_FullscreenView.Load += GV_FullscreenView_Load;
			// 
			// btn_SavePng
			// 
			btn_SavePng.Location = new Point(134, 12);
			btn_SavePng.Name = "btn_SavePng";
			btn_SavePng.Size = new Size(112, 34);
			btn_SavePng.TabIndex = 1;
			btn_SavePng.Text = "Save";
			btn_SavePng.UseVisualStyleBackColor = true;
			btn_SavePng.Click += btn_SavePng_Click;
			// 
			// btn_Leave
			// 
			btn_Leave.Location = new Point(16, 12);
			btn_Leave.Name = "btn_Leave";
			btn_Leave.Size = new Size(112, 34);
			btn_Leave.TabIndex = 2;
			btn_Leave.Text = "Exit";
			btn_Leave.UseVisualStyleBackColor = true;
			btn_Leave.Click += btn_Leave_Click;
			// 
			// SAV_SaveFile
			// 
			SAV_SaveFile.FileOk += SAV_SaveFile_FileOk;
			// 
			// btn_Export
			// 
			btn_Export.Location = new Point(1764, 12);
			btn_Export.Name = "btn_Export";
			btn_Export.Size = new Size(112, 34);
			btn_Export.TabIndex = 3;
			btn_Export.Text = "Export";
			btn_Export.UseVisualStyleBackColor = true;
			btn_Export.Click += btn_Export_Click;
			// 
			// SAV_ExportTable
			// 
			SAV_ExportTable.FileOk += SAV_ExportTable_FileOk;
			// 
			// GraphView
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1898, 1024);
			Controls.Add(btn_Export);
			Controls.Add(btn_Leave);
			Controls.Add(btn_SavePng);
			Controls.Add(GV_FullscreenView);
			Name = "GraphView";
			Text = "GraphView";
			Load += GraphView_Load;
			Resize += GraphView_Resize;
			ResumeLayout(false);
		}

		#endregion

		private ScottPlot.WinForms.FormsPlot GV_FullscreenView;
		private Button btn_SavePng;
		private Button btn_Leave;
		private SaveFileDialog SAV_SaveFile;
		private Button btn_Export;
		private SaveFileDialog SAV_ExportTable;
	}
}