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
			SuspendLayout();
			// 
			// GV_FullscreenView
			// 
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
			// GraphView
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1898, 1024);
			Controls.Add(btn_Leave);
			Controls.Add(btn_SavePng);
			Controls.Add(GV_FullscreenView);
			Name = "GraphView";
			Text = "GraphView";
			Load += GraphView_Load;
			ResumeLayout(false);
		}

		#endregion

		private ScottPlot.WinForms.FormsPlot GV_FullscreenView;
		private Button btn_SavePng;
		private Button btn_Leave;
		private SaveFileDialog SAV_SaveFile;
	}
}