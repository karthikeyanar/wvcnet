using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WVC.Framework {
	public class ServerFileUpload : ConfigurationSection, IFileUpload {

		[ConfigurationProperty("UploadPathKeys")]
		public UploadPathKeyCollection UploadPathKeys {
			get { return ((UploadPathKeyCollection)(base["UploadPathKeys"])); }
		}

		public bool DeleteFile(FileModel file) {
			bool result = false;
			try {
				if (file != null) {
					string rootPath = HttpContext.Current.Server.MapPath("~/");
					string fileName = Path.Combine(rootPath, file.FilePath, file.FileName);
					if (File.Exists(fileName)) {
						File.Delete(fileName);
						result = true;
					}
				}
			} catch { }
			return result;
		}

		public FileModel UploadFile(HttpPostedFile uploadFile, string appSettingName, params object[] args) {
			string rootPath = HttpContext.Current.Server.MapPath("~/");
			string uploadFilePath = Path.Combine(rootPath, string.Format(this.UploadPathKeys[appSettingName].Value, args));
			string directoryName = Path.GetDirectoryName(uploadFilePath);
			FileModel uploadFileModel = null;
			if (Directory.Exists(directoryName) == false) {
				Directory.CreateDirectory(directoryName);
			}
			if (File.Exists(uploadFilePath)) {
				File.Delete(uploadFilePath);
			}
			uploadFile.SaveAs(uploadFilePath);
			FileInfo fileInfo = new FileInfo(uploadFilePath);
			uploadFileModel = new FileModel {
				FileName = fileInfo.Name,
				FilePath = directoryName.Replace(rootPath, ""),
				Size = fileInfo.Length
			};
			return uploadFileModel;
		}
		 
		public string GetFullFileName(string appSettingName, string fileName) {
			string url = string.Empty;
			if (string.IsNullOrEmpty(fileName) == false && string.IsNullOrEmpty(appSettingName) == false) {
				string rootPath = HttpContext.Current.Server.MapPath("~/");
				url = Path.Combine(rootPath, string.Format(this.UploadPathKeys[appSettingName].Value, fileName));
			}
			return url;
		}

		public string GetDirectoryPath(string appSettingName) {
			string url = string.Empty;
			if (string.IsNullOrEmpty(appSettingName) == false) {
				string rootPath = HttpContext.Current.Server.MapPath("~/");
				url = Path.Combine(rootPath, string.Format(this.UploadPathKeys[appSettingName].Value, ""));
			}
			return url;
		}

		public string GetPath(string appSettingName) {
			string url = string.Empty;
			if (string.IsNullOrEmpty(appSettingName) == false) {
				url = string.Format(this.UploadPathKeys[appSettingName].Value, "");
			}
			return url;
		}

		public FileInfo WriteFileText(string appSettingName, string fileName, string contents) {
			string rootPath = HttpContext.Current.Server.MapPath("~/");
			string tempFileName = Path.Combine(rootPath, string.Format(this.UploadPathKeys[appSettingName].Value, fileName));
			string directoryName = Path.GetDirectoryName(tempFileName);
			if (Directory.Exists(directoryName) == false) {
				Directory.CreateDirectory(directoryName);
			}
			File.WriteAllText(tempFileName, contents);
			return new FileInfo(tempFileName);
		}

		public bool FileExist(string appSettingName, string fileName) {
			string rootPath = HttpContext.Current.Server.MapPath("~/");
			string tempFileName = Path.Combine(rootPath, string.Format(this.UploadPathKeys[appSettingName].Value, fileName));
			return File.Exists(tempFileName);
		}

		public bool DeleteFile(string appSettingName, string fileName) {
			string rootPath = HttpContext.Current.Server.MapPath("~/");
			string deleteFileName = Path.Combine(rootPath, string.Format(this.UploadPathKeys[appSettingName].Value, fileName));
			bool result = false;
			if (File.Exists(deleteFileName)) {
				File.Delete(deleteFileName);
				result = true;
			}
			return result;
		}

		public FileInfo WriteFileAllBytes(string appSettingName, string fileName, byte[] bytes) {
			string rootPath = HttpContext.Current.Server.MapPath("~/");
			string tempFileName = Path.Combine(rootPath, string.Format(this.UploadPathKeys[appSettingName].Value,fileName));
			string directoryName = Path.GetDirectoryName(tempFileName);
			if (Directory.Exists(directoryName) == false) {
				Directory.CreateDirectory(directoryName);
			}
			File.WriteAllBytes(tempFileName, bytes);
			return new FileInfo(tempFileName);
		}

		//public FileInfo FileWriteAllText(string appSettingName, string fileName, string text) {
		//	string rootPath = HttpContext.Current.Server.MapPath("~/");
		//	string tempFileName = Path.Combine(rootPath, string.Format(this.UploadPathKeys[appSettingName].Value, fileName));  
		//	string directoryName = Path.GetDirectoryName(tempFileName);
		//	if (Directory.Exists(directoryName) == false) {
		//		Directory.CreateDirectory(directoryName);
		//	}
		//	File.WriteAllText(tempFileName, text);
		//	return new FileInfo(tempFileName);
		//}
	}
}
