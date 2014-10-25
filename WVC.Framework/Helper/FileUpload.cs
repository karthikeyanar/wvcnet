using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(WVC.Framework.InitHelper), "Initialize")]
namespace WVC.Framework {
	
	public class InitHelper {

		public static List<string> FileExtensions { get; set; }

		public static void Initialize() {
			FileExtensions = new List<string>();
			string fileExtensions = ConfigurationManager.AppSettings["FileExtensions"];
			string[] arr = fileExtensions.Split((",").ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			foreach (string fileType in arr) {
				FileExtensions.Add(fileType);
			}
		}
	}

	interface IFileUpload {
		UploadPathKeyCollection UploadPathKeys { get; }
		//FileModel UploadFile(HttpPostedFileBase uploadFile, string appSettingName, params object[] args);
		FileModel UploadFile(HttpPostedFile uploadFile, string appSettingName, params object[] args);
		//FileModel UploadTempFile(HttpPostedFileBase uploadFile);
		bool DeleteFile(FileModel file);
		bool DeleteFile(string appSettingName, string fileName);
		FileInfo WriteFileText(string appSettingName, string fileName, string contents);
		bool FileExist(string appSettingName, string fileName);
		string GetFullFileName(string appSettingName, string fileName);
		string GetDirectoryPath(string appSettingName);
		string GetPath(string appSettingName);
		FileInfo WriteFileAllBytes(string appSettingName, string fileName, byte[] bytes);
		//FileInfo FileWriteAllText(string appSettingName, string fileName, string text);
	}

	public class FileModel {

		public FileModel() {
			this.Errors = new List<ErrorInfo>();
		}

		public string FilePath { get; set; }

		public string FileName { get; set; }

		public long Size { get; set; }

		public IEnumerable<ErrorInfo> Errors { get; set; }
	}

	public static class UploadFileHelper {

		private static IFileUpload _FileUpload = null;

		static UploadFileHelper() {
			string windowsazure = ConfigurationManager.AppSettings["WindowsAzure"];
			if (windowsazure == "true")
				_FileUpload = (IFileUpload)ConfigurationManager.GetSection("WindowsAzureFileUpload");
			else
				_FileUpload = (IFileUpload)ConfigurationManager.GetSection("ServerFileUpload");
		}

		public static FileModel Upload(HttpPostedFile uploadFile, string appSettingName, params object[] args) {
			List<ErrorInfo> errors = (List<ErrorInfo>)CheckFileExtension(uploadFile.FileName);
			if (errors.Any() == false) {
				return _FileUpload.UploadFile(uploadFile, appSettingName, args);
			}
			return new FileModel { Errors = errors };
		}

		public static bool DeleteFile(FileModel file) {
			return _FileUpload.DeleteFile(file);
		}

		public static bool FileExist(string appSettingName, string fileName) {
			return _FileUpload.FileExist(appSettingName, fileName);
		}

		public static bool DeleteFile(string appSettingName, string fileName) {
			return _FileUpload.DeleteFile(appSettingName, fileName);
		}

		public static FileInfo WriteFileAllBytes(string appSettingName, string fileName, byte[] bytes) {
			return _FileUpload.WriteFileAllBytes(appSettingName, fileName, bytes);
		}

		public static FileInfo WriteFileText(string appSettingName, string fileName, string text) {
			return _FileUpload.WriteFileText(appSettingName, fileName, text);
		}

		public static string GetDirectoryPath(string appSettingName) {
			return _FileUpload.GetDirectoryPath(appSettingName);
		}

		public static string GetPath(string appSettingName) {
			return _FileUpload.GetPath(appSettingName);
		}

		public static string GetFullFileName(string appSettingName, string fileName) {
			return _FileUpload.GetFullFileName(appSettingName, fileName);
		}

		public static bool CheckFilePath(string filePath) {
			Regex regex = new Regex(
									@"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)"
									+ @"*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$",
									RegexOptions.IgnoreCase
									| RegexOptions.Multiline
									| RegexOptions.IgnorePatternWhitespace
									| RegexOptions.Compiled
									);
			return regex.IsMatch(filePath);
		}

		public static IEnumerable<ErrorInfo> CheckFileExtension(string fileName) {
			FileInfo fileInfo = new FileInfo(fileName);
			List<ErrorInfo> errors = new List<ErrorInfo>();
			string extension = fileInfo.Extension.ToLower();
			bool isExist = (from ex in InitHelper.FileExtensions
							where ex == extension
							select ex).Count() > 0;
			if (isExist == false) {
				errors.Add(new ErrorInfo { ErrorMessage = string.Format("{0} Extension is not allowed", extension) });
			}
			return errors;
		}


		public static string AppSetting(string key) {
			return _FileUpload.UploadPathKeys[key].Value;
		}

	}

	/// <summary>
	/// The collection class that will store the list of each element/item that
	/// is returned back from the configuration manager.
	/// </summary>
	[ConfigurationCollection(typeof(UploadPathElement))]
	public class UploadPathKeyCollection : ConfigurationElementCollection {
		protected override ConfigurationElement CreateNewElement() {
			return new UploadPathElement();
		}

		protected override object GetElementKey(ConfigurationElement element) {
			return ((UploadPathElement)(element)).Key;
		}

		public UploadPathElement this[int idx] {
			get {
				return (UploadPathElement)BaseGet(idx);
			}
		}

		public UploadPathElement this[string key] {
			get {
				return (UploadPathElement)BaseGet(key);
			}
		}
	}

	/// <summary>
	/// The class that holds onto each element returned by the configuration manager.
	/// </summary>
	public class UploadPathElement : ConfigurationElement {
		[ConfigurationProperty("key", DefaultValue = "", IsKey = true, IsRequired = true)]
		public string Key {
			get {
				return ((string)(base["key"]));
			}
			set {
				base["key"] = value;
			}
		}

		[ConfigurationProperty("value", DefaultValue = "", IsKey = false, IsRequired = false)]
		public string Value {
			get {
				return ((string)(base["value"]));
			}
			set {
				base["value"] = value;
			}
		}
	}
}
