using System.Collections.Generic;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Linq;

namespace WVC.Framework
{
    public class SLExcelStatus
    {
        public string Message { get; set; }
        public bool Success
        {
            get { return string.IsNullOrWhiteSpace(Message); }
        }
    }

    public class SLExcelData
    {
		public SLExcelData() {
			Status = new SLExcelStatus();
			Headers = new List<string>();
			DataRows = new List<List<string>>();
		}

        public SLExcelStatus Status { get; set; }
        public Columns ColumnConfigurations { get; set; }
        public List<string> Headers { get; set; }
        public List<List<string>> DataRows { get; set; }
        public string SheetName { get; set; }

		public  string GetValue(List<string> row, string columnName) {
			return row[this.Headers.IndexOf(columnName)];
		}

		public int GetColumnIndex(string columnName) {
			return this.Headers.IndexOf(columnName);
		}

		public void SetValue(List<string> row, string columnName, string value) {
			row[this.Headers.IndexOf(columnName)] = value;
		}
    }
}