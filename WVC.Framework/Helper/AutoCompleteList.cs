using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVC.Framework {
	public class AutoCompleteList {
		public AutoCompleteList() {
			id = 0;
			label = string.Empty;
			value = string.Empty;
		}
		public int id { get; set; }
		public string label { get; set; }
		public string value { get; set; }
	}

	public class DateTimeConverter : DateTimeConverterBase {
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
			return DateTime.Parse(reader.Value.ToString());
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
			writer.WriteValue(((DateTime)value).ToString("MM/dd/yyyy"));
		}
	}
}
