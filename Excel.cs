using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ScottPlot;

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
			//if no sheet is picked, it uses the default name for a sheet in an excel workbook
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
				//MessageBox.Show(pickedSheet);
				sheet = book.GetSheet(pickedSheet);
				//sheet1 is the default first page in a workbook, add the option to change this later
			}
			catch (Exception)
			{
				MessageBox.Show("Can't open book", "Error");
				//make the code return to the main section!
				new Home();
			}
		}
		public void ReadWorkbook()
		{
			string value1;
			TimeSpan DayNumber = TimeSpan.Zero;
			DateTime StartDate = DateTime.MinValue;
			//string xtitle;
			string value2;
			int startLine = 1;
			//string ytitle;
			//IRow FirstRow = sheet.GetRow(0);
			//xtitle = Convert.ToString(FirstRow.GetCell(0));
			//ytitle = Convert.ToString(FirstRow.GetCell(1));
			for (int row = startLine; row <= sheet.LastRowNum; row++)
			{
				IRow CurrentRow = sheet.GetRow(row);
				double cell2;
				double cell1;
				ICell CurrentCell1 = CurrentRow.GetCell(0);
				value1 = CurrentCell1.ToString();
				ICell CurrentCell2 = CurrentRow.GetCell(1);
				value2 = CurrentCell2.ToString();
				if (CurrentRow == null)
				{
					
				}
				else
				{

					
					if (double.TryParse(value1, out cell1) == true && double.TryParse(value2, out cell2) == true )
					{
						//if both columns contain values, keep the current set of headers, unless it is the last value pair, in which case use the defaults
						Coordinate ExcelInput = new Coordinate(x: cell1, y: cell2);
						DataOut.Add(ExcelInput);
					}
					else if (DateTime.TryParse(value1.ToString(), out DateTime date) == true && Double.TryParse(value2, out double number))
					{
						if (StartDate == DateTime.MinValue)
						{
							StartDate = date;
						}
						//if left column is a date, wait until the last pair of values to write the headers
						DayNumber = date - StartDate;
						//MessageBox.Show(outdate +" "+ value1);
						xtitle = "Days";
						ytitle = sheet.SheetName;
						Coordinate ExcelInput = new Coordinate(x: DayNumber.Days, y: number);
						DataOut.Add(ExcelInput);
						DayNumber = TimeSpan.Zero;
					}
					else if (double.TryParse(value1, out cell1) == false && double.TryParse(value2, out cell2) == false)
					{
						//if both columns are not values, assume it is a header and write the header FIRST; the pair of non-values closest to the bottom becomes the header
						xtitle = value1;
						ytitle = value2;
						startLine = 0;
					}
					else
					{
						MessageBox.Show(value1 + " " + value2);
						//if left column or right column 
						xtitle = "Date";
						ytitle = sheet.SheetName;
					}
					//Console.WriteLine($"({CurrentCell1}, {CurrentCell2}) ");
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
