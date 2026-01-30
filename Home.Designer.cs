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
			chk_ConnectPoints = new CheckBox();
			((System.ComponentModel.ISupportInitialize)DgvCurrentData).BeginInit();
			SuspendLayout();
			// 
			// BtnSort
			// 
			BtnSort.Location = new Point(505, 15);
			BtnSort.Margin = new Padding(4);
			BtnSort.Name = "BtnSort";
			BtnSort.Size = new Size(118, 36);
			BtnSort.TabIndex = 1;
			BtnSort.Text = "Sort";
			BtnSort.UseVisualStyleBackColor = true;
			BtnSort.Click += BtnSort_Click;
			// 
			// BtnFile
			// 
			BtnFile.Location = new Point(130, 15);
			BtnFile.Margin = new Padding(4);
			BtnFile.Name = "BtnFile";
			BtnFile.Size = new Size(118, 36);
			BtnFile.TabIndex = 2;
			BtnFile.Text = "Open";
			BtnFile.UseVisualStyleBackColor = true;
			BtnFile.Click += BtnFile_Click;
			BtnFile.MouseHover += BtnFile_MouseHover;
			// 
			// BtnCreate
			// 
			BtnCreate.Location = new Point(5, 15);
			BtnCreate.Margin = new Padding(4);
			BtnCreate.Name = "BtnCreate";
			BtnCreate.Size = new Size(118, 36);
			BtnCreate.TabIndex = 3;
			BtnCreate.Text = "New";
			BtnCreate.UseVisualStyleBackColor = true;
			// 
			// BtnRegress
			// 
			BtnRegress.Location = new Point(255, 15);
			BtnRegress.Margin = new Padding(4);
			BtnRegress.Name = "BtnRegress";
			BtnRegress.Size = new Size(118, 36);
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
			BtnView.Location = new Point(380, 15);
			BtnView.Margin = new Padding(4);
			BtnView.Name = "BtnView";
			BtnView.Size = new Size(118, 36);
			BtnView.TabIndex = 6;
			BtnView.Text = "View";
			BtnView.UseVisualStyleBackColor = true;
			BtnView.Click += BtnView_Click;
			// 
			// DgvCurrentData
			// 
			DgvCurrentData.BackgroundColor = SystemColors.ControlLight;
			DgvCurrentData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DgvCurrentData.Location = new Point(15, 80);
			DgvCurrentData.Margin = new Padding(4);
			DgvCurrentData.Name = "DgvCurrentData";
			DgvCurrentData.RowHeadersWidth = 51;
			DgvCurrentData.ScrollBars = ScrollBars.Vertical;
			DgvCurrentData.Size = new Size(358, 450);
			DgvCurrentData.TabIndex = 7;
			// 
			// pltHome
			// 
			pltHome.DisplayScale = 1.25F;
			pltHome.Location = new Point(380, 80);
			pltHome.Margin = new Padding(4);
			pltHome.Name = "pltHome";
			pltHome.Size = new Size(605, 410);
			pltHome.TabIndex = 8;
			pltHome.Load += pltHome_Load;
			// 
			// btnLogIn
			// 
			btnLogIn.Location = new Point(867, 15);
			btnLogIn.Margin = new Padding(4);
			btnLogIn.Name = "btnLogIn";
			btnLogIn.Size = new Size(118, 36);
			btnLogIn.TabIndex = 9;
			btnLogIn.Text = "Log In";
			btnLogIn.UseVisualStyleBackColor = true;
			btnLogIn.Click += btnLogIn_Click;
			// 
			// BtnPrint
			// 
			BtnPrint.Location = new Point(505, 59);
			BtnPrint.Margin = new Padding(4);
			BtnPrint.Name = "BtnPrint";
			BtnPrint.Size = new Size(118, 36);
			BtnPrint.TabIndex = 10;
			BtnPrint.Text = "Print";
			BtnPrint.UseVisualStyleBackColor = true;
			BtnPrint.Click += btnPrint_Click;
			// 
			// chk_ConnectPoints
			// 
			chk_ConnectPoints.AutoSize = true;
			chk_ConnectPoints.Location = new Point(630, 20);
			chk_ConnectPoints.Name = "chk_ConnectPoints";
			chk_ConnectPoints.Size = new Size(156, 29);
			chk_ConnectPoints.TabIndex = 11;
			chk_ConnectPoints.Text = "Connect Points";
			chk_ConnectPoints.UseVisualStyleBackColor = true;
			chk_ConnectPoints.CheckedChanged += chk_ConnectPoints_CheckedChanged;
			// 
			// Home
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1000, 562);
			Controls.Add(chk_ConnectPoints);
			Controls.Add(BtnPrint);
			Controls.Add(btnLogIn);
			Controls.Add(pltHome);
			Controls.Add(DgvCurrentData);
			Controls.Add(BtnView);
			Controls.Add(BtnRegress);
			Controls.Add(BtnCreate);
			Controls.Add(BtnFile);
			Controls.Add(BtnSort);
			Margin = new Padding(4);
			Name = "Home";
			Text = "Home";
			Load += Home_Load;
			((System.ComponentModel.ISupportInitialize)DgvCurrentData).EndInit();
			ResumeLayout(false);
			PerformLayout();
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
		private CheckBox chk_ConnectPoints;
		//private TextBox textBox1;
	}
}
