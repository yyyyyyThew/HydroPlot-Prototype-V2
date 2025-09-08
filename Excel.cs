using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace PrototypeV2
{
	class Excel
	{
		private string[] Sheets;
		public Table LocalColumn;
		private string BookPath;
		string pickedSheet;
		private IWorkbook book;
		private ISheet sheet;
		//private string site1;
		public string xtitle;
		public string ytitle;
		//private string site2;
		public List<Coordinate> DataOut = new List<Coordinate>();

		public Excel(string path)
		{
			bool readyCheck = false;
			string pickedSheet = "sheet1";
			BookPath = path;
			//IWorkbook book;
			try
			{
				FileStream XFS = new FileStream(BookPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

				book = new XSSFWorkbook(XFS);
				Sheets = ListSheets();
				//MessageBox.Show(Sheets[0]);
				if (Sheets.Length > 1)
				{
					SheetPicker Select = new SheetPicker(Sheets);
					Select.ShowDialog();
					pickedSheet = Select.PickedSheet;
					if (pickedSheet == "")
					{
						//pickedSheet = Select.PickedSheet;
						throw new Exception("aaaaahhhhh");
					}
				}
				else
				{
					pickedSheet = Sheets[0];
				}
				MessageBox.Show(pickedSheet);
				sheet = book.GetSheet(pickedSheet);
				//sheet1 is the default first page in a workbook, add the option to change this later
			}
			catch (Exception)
			{
				MessageBox.Show("Can't open book", "Error");
			}
		}
		public void ReadWorkbook()
		{
			double value1;
			//string xtitle;
			double value2;
			int startLine = 1;
			//string ytitle;
			IRow FirstRow = sheet.GetRow(0);
			xtitle = Convert.ToString(FirstRow.GetCell(0));
			ytitle = Convert.ToString(FirstRow.GetCell(1));
			if (double.TryParse(xtitle, out double cell1))
			{
				xtitle = "Site 1";
				ytitle = "Site 2";
				startLine = 0;
			}
			else
			{
				Console.WriteLine("Header Found");
			}
			for (int row = startLine; row <= sheet.LastRowNum; row++)
			{
				IRow CurrentRow = sheet.GetRow(row);
				if (sheet.GetRow(row) == null) //null is when the row only contains empty cells 
				{
					Console.WriteLine("Reached end of file 😁");
					break;
				}
				else
				{

					ICell CurrentCell1 = CurrentRow.GetCell(0);
					value1 = Convert.ToDouble(CurrentCell1.NumericCellValue);
					ICell CurrentCell2 = CurrentRow.GetCell(1);
					value2 = Convert.ToDouble(CurrentCell2.NumericCellValue);
					//Console.WriteLine($"({CurrentCell1}, {CurrentCell2}) ");
					Coordinate ExcelInput = new Coordinate(x: value1, y: value2);
					DataOut.Add(ExcelInput);
					LocalColumn = new Table(DataOut, $"Excel Data For {xtitle} and {ytitle}", xtitle, ytitle);
				}
			}
		}

		public string[] ListSheets()
		{
			int sheetCount = book.NumberOfSheets;
			string[] sheetList = new string[book.NumberOfSheets];
			for (int i = 0; i < sheetCount; i++)
			{
				ISheet sheet = book.GetSheetAt(i);
				Console.WriteLine("Sheet name: " + sheet.SheetName);
				sheetList[i] = sheet.SheetName;
			}
			return sheetList;

		}
	}
}
