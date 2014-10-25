using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Text;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;

namespace WVC.Framework {
	public class Helper {
		public static T ParseFromJson<T>(object o) {
			dynamic dyn = o;
			T obj = Activator.CreateInstance<T>();
			// loop through all the properties of T and try to find   if J also has that property. If yes, then copy the values from J to T
			Type t = typeof(T);
			PropertyInfo[] properties = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			foreach(PropertyInfo property in properties) {
				try {
					property.SetValue(obj,dyn[property.Name],null);
				} catch {
				}
			}
			return obj;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="J"></typeparam>
		/// <param name="objectToCopyFrom"></param>
		/// <param name="mapping">Mapping of TargetPropertyName(Key) and SourcePropertyName(Value)</param>
		/// <returns></returns>
		public static T CopyValues<T,J>(J objectToCopyFrom,NameValueCollection mapping = null) {
			T objectToCopyTo = Activator.CreateInstance<T>();
			return CopyValues(objectToCopyFrom,objectToCopyTo,mapping);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="J"></typeparam>
		/// <param name="objectToCopyFrom"></param>
		/// <param name="mapping">Mapping of TargetPropertyName(Key) and SourcePropertyName(Value). If you want some properties to be excluded, set the value as string.Empty</param>
		/// <returns></returns>
		public static T CopyValues<T,J>(J objectToCopyFrom,T objectToCopyTo,NameValueCollection mapping = null) {
			// loop through all the properties of T and try to find   if J also has that property. If yes, then copy the values from J to T
			Type t = typeof(T);
			PropertyInfo[] properties = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			PropertyInfo[] sourceObjProperties = typeof(J).GetProperties();
			foreach(PropertyInfo property in properties) {
				string targetPropertyName = property.Name;
				if(mapping != null) {
					if(mapping[property.Name] != null) {
						targetPropertyName = mapping[property.Name];
					}
				}

				if(!string.IsNullOrEmpty(targetPropertyName)) {
					PropertyInfo targetProperty =
							sourceObjProperties.Where(x => x.Name == targetPropertyName).FirstOrDefault();
					if(targetProperty != null) {
						try {
							property.SetValue(objectToCopyTo,targetProperty.GetValue(objectToCopyFrom,null),null);
						} catch {
						}
					}
				}
			}
			return objectToCopyTo;
		}
	}

	public enum ConfigUtil {
		//public static int SystemEntityID = 1;
		///// <summary>
		///// Each entity should get an ID greater or equal to this value.
		///// This is used in the validation to make sure we are not inserting any
		///// data which is for an invalid Entity( 0, or SystemEntity)
		///// </summary>
		//public static int EntityIDStartRange = 2;
		///// <summary>
		///// Data point used to make sure all the IDs start from this range. 
		///// Used for validation
		///// </summary>
		//public static int IDStartRange = 1;
		//public static int CurrentEntityID = 2;
		SystemEntityID = 1,

		/// <summary>
		/// Each entity should get an ID greater or equal to this value.
		/// This is used in the validation to make sure we are not inserting any
		/// data which is for an invalid Entity( 0, or SystemEntity)
		/// </summary>
		EntityIDStartRange = 1,

		/// <summary>
		/// Data point used to make sure all the IDs start from this range. 
		/// Used for validation
		/// </summary>
		IDStartRange = 1
		//CurrentEntityID = 2
	}
}
