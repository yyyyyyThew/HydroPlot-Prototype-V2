namespace PrototypeV2
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
			components = new System.ComponentModel.Container();
			BtnSort = new Button();
			BtnFile = new Button();
			BtnCreate = new Button();
			BtnRegress = new Button();
			FpkExcel = new OpenFileDialog();
			BtnView = new Button();
			DgvCurrentData = new DataGridView();
			TtiOpen = new ToolTip(components);
			pltHome = new ScottPlot.WinForms.FormsPlot();
			btnLogIn = new Button();
			BtnPrint = new Button();
			((System.ComponentModel.ISupportInitialize)DgvCurrentData).BeginInit();
			SuspendLayout();
			// 
			// BtnSort
			// 
			BtnSort.Location = new Point(404, 12);
			BtnSort.Name = "BtnSort";
			BtnSort.Size = new Size(94, 29);
			BtnSort.TabIndex = 1;
			BtnSort.Text = "Sort";
			BtnSort.UseVisualStyleBackColor = true;
			BtnSort.Click += BtnSort_Click;
			// 
			// BtnFile
			// 
			BtnFile.Location = new Point(104, 12);
			BtnFile.Name = "BtnFile";
			BtnFile.Size = new Size(94, 29);
			BtnFile.TabIndex = 2;
			BtnFile.Text = "Open";
			BtnFile.UseVisualStyleBackColor = true;
			BtnFile.Click += BtnFile_Click;
			BtnFile.MouseHover += BtnFile_MouseHover;
			// 
			// BtnCreate
			// 
			BtnCreate.Location = new Point(4, 12);
			BtnCreate.Name = "BtnCreate";
			BtnCreate.Size = new Size(94, 29);
			BtnCreate.TabIndex = 3;
			BtnCreate.Text = "New";
			BtnCreate.UseVisualStyleBackColor = true;
			// 
			// BtnRegress
			// 
			BtnRegress.Location = new Point(204, 12);
			BtnRegress.Name = "BtnRegress";
			BtnRegress.Size = new Size(94, 29);
			BtnRegress.TabIndex = 4;
			BtnRegress.Text = "Best Fit";
			BtnRegress.UseVisualStyleBackColor = true;
			BtnRegress.Click += BtnRegress_Click;
			// 
			// FpkExcel
			// 
			FpkExcel.FileName = "openFileDialog1";
			FpkExcel.ShowPinnedPlaces = false;
			FpkExcel.FileOk += FpkExcel_FileOk;
			// 
			// BtnView
			// 
			BtnView.Location = new Point(304, 12);
			BtnView.Name = "BtnView";
			BtnView.Size = new Size(94, 29);
			BtnView.TabIndex = 6;
			BtnView.Text = "View";
			BtnView.UseVisualStyleBackColor = true;
			// 
			// DgvCurrentData
			// 
			DgvCurrentData.BackgroundColor = SystemColors.ControlLight;
			DgvCurrentData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DgvCurrentData.Location = new Point(12, 64);
			DgvCurrentData.Name = "DgvCurrentData";
			DgvCurrentData.RowHeadersWidth = 51;
			DgvCurrentData.ScrollBars = ScrollBars.Vertical;
			DgvCurrentData.Size = new Size(286, 360);
			DgvCurrentData.TabIndex = 7;
			// 
			// pltHome
			// 
			pltHome.DisplayScale = 1.25F;
			pltHome.Location = new Point(304, 64);
			pltHome.Name = "pltHome";
			pltHome.Size = new Size(484, 328);
			pltHome.TabIndex = 8;
			pltHome.Load += pltHome_Load;
			// 
			// btnLogIn
			// 
			btnLogIn.Location = new Point(582, 12);
			btnLogIn.Name = "btnLogIn";
			btnLogIn.Size = new Size(94, 29);
			btnLogIn.TabIndex = 9;
			btnLogIn.Text = "Log In";
			btnLogIn.UseVisualStyleBackColor = true;
			btnLogIn.Click += btnLogIn_Click;
			// 
			// BtnPrint
			// 
			BtnPrint.Location = new Point(404, 47);
			BtnPrint.Name = "BtnPrint";
			BtnPrint.Size = new Size(94, 29);
			BtnPrint.TabIndex = 10;
			BtnPrint.Text = "Print";
			BtnPrint.UseVisualStyleBackColor = true;
			BtnPrint.Click += btnPrint_Click;
			// 
			// Home
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(BtnPrint);
			Controls.Add(btnLogIn);
			Controls.Add(pltHome);
			Controls.Add(DgvCurrentData);
			Controls.Add(BtnView);
			Controls.Add(BtnRegress);
			Controls.Add(BtnCreate);
			Controls.Add(BtnFile);
			Controls.Add(BtnSort);
			Name = "Home";
			Text = "Home";
			Load += Home_Load;
			((System.ComponentModel.ISupportInitialize)DgvCurrentData).EndInit();
			ResumeLayout(false);
		}

		#endregion
		private Button BtnSort;
		private Button BtnFile;
		private Button BtnCreate;
		private Button BtnRegress;
		private OpenFileDialog FpkExcel;
		private Button BtnView;
		private DataGridView DgvCurrentData;
		private ToolTip TtiOpen;
		private ScottPlot.WinForms.FormsPlot pltHome;
		private Button btnLogIn;
		private Button BtnPrint;
		//private TextBox textBox1;
	}
}
