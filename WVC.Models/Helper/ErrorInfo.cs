using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WVC.Models
{
    public enum ErrorType
    {
        Error = 1,
        Warning = 2,
        NotFound = 3
    }

    public class ErrorInfo
    {
        public ErrorType ErrorType = ErrorType.Error;
        /// <summary>
        /// To support serialization
        /// </summary>
        public ErrorInfo()
        {

        }
        public ErrorInfo(string propertyName, string errorMessage)
        {
            this.PropertyName = propertyName;
            this.ErrorMessage = errorMessage;
        }
        public ErrorInfo(string propertyName, string errorMessage, object onObject)
        {
            this.PropertyName = propertyName;
            this.ErrorMessage = errorMessage;
            this.Object = onObject;
        }
        public ErrorInfo(string propertyName, string errorMessage, object onObject, ErrorType errorType)
        {
            this.PropertyName = propertyName;
            this.ErrorMessage = errorMessage;
            this.Object = onObject;
            this.ErrorType = errorType;
        }

        public string ErrorMessage { get; set; }
        public object Object { get; set; }
        public string PropertyName { get; set; }
    }

    public interface IErrorProvider
    {
        List<ErrorInfo> Errors { get; set; }
        void Add(string propertyName, string error);
        void Add(ErrorInfo error);
        string Name { get; }
    }

    public class ErrorProvider : IErrorProvider
    {

        //[ThreadStatic]
        //private static IErrorProvider _current = null;
        /// <summary>
        /// Returns the default error provider
        /// </summary>
        public static IErrorProvider Current
        {
            get
            {
                //if (_current == null) {
                //    // Dont use HttpContextErrorProvider. The problem is that the Context Provider adds the Error to
                //    // the Context using Context.AddError. MVC API throws InternalServerError in that case, even if you explicitly 
                //    // set the StatusCode on the HttpResponseMessage
                //    //_current = new HttpContextErrorProvider();
                //    _current = new ErrorProvider();
                //}
                //return _current;
                IErrorProvider _current = HttpContextFactory.GetHttpContext().Items["ErrorProvider"] as IErrorProvider;
                if (_current == null)
                {
                    _current = new ErrorProvider();
                    HttpContextFactory.GetHttpContext().Items.Add("ErrorProvider", _current);
                }
                //Debug.WriteLine("Name:ErrorInfo.Current:" + _current.Name);
                return _current;
            }
        }


        private List<ErrorInfo> _errors = null;
        public List<ErrorInfo> Errors
        {
            get
            {
                return _errors;
            }
            set
            {
                _errors = value;
            }
        }

        private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
        }

        public ErrorProvider()
        {
            _name = Guid.NewGuid().ToString() + " " + DateTime.Now.Ticks.ToString();
            _errors = new List<ErrorInfo>();
            //Debug.WriteLine(string.Format("Name: {0}", _name));
        }

        public ErrorProvider(string propertyName, string error)
        {
            Errors.Add(new ErrorInfo(propertyName, error));
        }

        public virtual void Add(string propertyName, string error)
        {
            Errors.Add(new ErrorInfo(propertyName, error));
        }

        public virtual void Add(ErrorInfo error)
        {
            Errors.Add(error);
        }

        public virtual void CopyErrorsToModelState(System.Web.Mvc.ModelStateDictionary ModelState)
        {
            if (Errors.Any())
            {
                foreach (var err in Errors)
                {
                    ModelState.AddModelError(err.PropertyName, err.ErrorMessage);
                }
            }
        }
    }

    public static class HttpContextFactory
    {
        [ThreadStatic]
        private static HttpContextBase mockHttpContext;

        public static void SetHttpContext(HttpContextBase httpContextBase)
        {
            mockHttpContext = httpContextBase;
        }

        public static void ResetHttpContext()
        {
            mockHttpContext = null;
        }

        public static HttpContextBase GetHttpContext()
        {
            if (mockHttpContext != null)
            {
                return mockHttpContext;
            }

            if (HttpContext.Current != null)
            {
                return new HttpContextWrapper(HttpContext.Current);
            }

            return null;
        }
    }

    public class ValidationHelper
    {
        /// <summary>
        /// Get any errors associated with the model also investigating any rules dictated by attached Metadata buddy classes.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static IEnumerable<ErrorInfo> Validate(object instance)
        {
            //return new List<ErrorInfo>();
            var metadataAttrib = instance.GetType().GetCustomAttributes(typeof(MetadataTypeAttribute), true).OfType<MetadataTypeAttribute>().FirstOrDefault();
            var buddyClassOrModelClass = metadataAttrib != null ? metadataAttrib.MetadataClassType : instance.GetType();
            var buddyClassProperties = TypeDescriptor.GetProperties(buddyClassOrModelClass).Cast<PropertyDescriptor>();
            var modelClassProperties = TypeDescriptor.GetProperties(instance.GetType()).Cast<PropertyDescriptor>();

            List<ErrorInfo> errors = (from buddyProp in buddyClassProperties
                                      join modelProp in modelClassProperties on buddyProp.Name equals modelProp.Name
                                      from attribute in buddyProp.Attributes.OfType<ValidationAttribute>()
                                      where !attribute.IsValid(modelProp.GetValue(instance))
                                      select new ErrorInfo(buddyProp.Name, attribute.FormatErrorMessage(attribute.ErrorMessage), instance)).ToList();
            // Add in the class level custom attributes
            IEnumerable<ErrorInfo> classErrors = from attribute in TypeDescriptor.GetAttributes(buddyClassOrModelClass).OfType<ValidationAttribute>()
                                                 where !attribute.IsValid(instance)
                                                 select new ErrorInfo("ClassLevelCustom", attribute.FormatErrorMessage(attribute.ErrorMessage), instance);

            errors.AddRange(classErrors);
            return errors.AsEnumerable();
        }

        public static string GetErrorInfo(IEnumerable<ErrorInfo> errorInfo)
        {
            StringBuilder errors = new StringBuilder();
            if (errorInfo != null)
            {
                foreach (var err in errorInfo.ToList())
                {
                    errors.Append(err.ErrorMessage + "\n");
                }
            }
            return errors.ToString();
        }

    }
}
