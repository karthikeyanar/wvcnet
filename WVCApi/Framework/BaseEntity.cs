using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Reflection;
using System.Diagnostics;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WVCApi.Models {
	public class BaseTable<T> : BaseModel<T> where T : BaseModel
    {
		//protected bool ValidateForeignKey(EnumeratedValueType enumeratedValueType, int? id)
		//{
		//    if (!EnumeratedValue.IsValid(enumeratedValueType, id))
		//    {
		//        this.Errors.Add(enumeratedValueType.ToString(), string.Format("Invalid Value: {0}", id.Value));
		//        IsValid = false;
		//        return false;
		//    }
		//    return true;
		//}

		//protected bool ValidateForeignKeys(EnumeratedValueType enumeratedValueType, List<int> ids)
		//{
		//    List<int> invalidIds = EnumeratedValue.IsValid(enumeratedValueType, ids);
		//    if (invalidIds.Count > 0)
		//    {
		//        foreach (int invalidId in invalidIds)
		//        {
		//            this.Errors.Add(enumeratedValueType.ToString(), string.Format("Invalid Value: {0}", invalidId));
		//        }
		//        IsValid = false;
		//        return false;
		//    }
		//    return true;
		//}
    }

	public class BaseModel {
		protected static IServiceFactory _factory = null;
		public static void ConfigureServiceFactory(IServiceFactory factory) {
			_factory = factory;
		}

		public int ID { get; set; }

		protected int CurrentUserId {
			get { 
				//return Authentication.Authentication.CurrentUser.UserID; 
				return 0;
			}
		}

		public System.DateTime CreatedDate { get; set; }
		public int CreatedBy { get; set; }
		public System.DateTime? LastUpdatedDate { get; set; }
		public int? LastUpdatedBy { get; set; }
		private EntityState _entityState = EntityState.UnChanged;

		[NotMapped]
		public EntityState EntityState {
			get { return _entityState; }
			set { _entityState = value; }
		}

		private IErrorProvider _errorProvider = ErrorProvider.Current;

		//[NotMapped]
		//public IErrorProvider Errors {
		//    get { return _errorProvider; } //set { _errorProvider = value; } 
		//}

		[NotMapped]
		public IErrorProvider Errors {
			get { return ErrorProvider.Current; } //set { _errorProvider = value; } 
		}

		//[NotMapped]
		//public List<ErrorInfo> Errors { get; set; }


		//public List<EnumeratedValue> GetEnumeratedValues(string type) {
		//    Type typ = type.GetType();
		//    using (WVCApiContext context = new WVCApiContext()) {


		//         MethodInfo method = typeof(DbContext).GetMethod("GetTable");         


		//             MethodInfo genericMethod = method.MakeGenericMethod(new Type[] {typ});             
		//             genericMethod.Invoke(context, null); 


		//    }
		//}

		//private DbSet<T> GetTable<T>(this DbContext context) {
		//    return null;
		//}
	}

	[Serializable]
	public enum EntityState {
		Added = 1,
		Deleted = 2,
		Modified = 3,
		UnChanged = 4,
		Destroy = 5
	}

	public class BaseModel<T> : BaseModel where T : BaseModel {
		protected bool IsValid = true;
		//private IService<T> _service = null;

		//public IService<T> Service {
		//    get {
		//        if (_service == null) {
		//            //Type type = typeof(Service<>).MakeGenericType(typeof(T));
		//            //_service = (IService<T>)Activator.CreateInstance(type);
		//            _service = _factory.Create<T>();
		//        }
		//        return _service;
		//    }
		//    set { _service = value; }
		//}

		private static IService<T> _service = null;
		public static IService<T> Service {
			get {
				// we cant cache the service instance as static. That will make the instance same across all the
				// requests. If you want to cache it, then that responsibility should be part of the factory
				// I was getting the following error. Possibly cos the context were cached as static on the application, and so
				// there is a possibility that services from different contexts get mingled
				// The specified LINQ expression contains references to queries that are associated with different contexts.
				//if (_service == null) {
				//Type type = typeof(Service<>).MakeGenericType(typeof(T));
				//_service = (IService<T>)Activator.CreateInstance(type);
				_service = _factory.Create<T>();
				//}
				return _service;
			}
			//set { _service = value; }
			set { _factory.Register(value); }
		}
		
		public virtual bool Validate() {
			// return true;
			IEnumerable<ErrorInfo> errors =  ValidationHelper.Validate(this);
			foreach (var errorInfo in errors) {
				this.Errors.Add(errorInfo);
			}
			return errors.Count() <= 0;
		}

		public virtual void OnSaving() {
			// Set the EntityID
			if (this.ID > 0) {
				this.EntityState = EntityState.Modified;
			} else {
				this.EntityState = EntityState.Added;
			}
		}

		public virtual void Save() {
			OnSaving();
			//switch (this.EntityState) {
			//    case EntityState.Added:
			//        this.Create();
			//        break;
			//    case EntityState.Modified:
			//        this.Update();
			//        break;
			//    case EntityState.Deleted:
			//        this.Delete();
			//        break;
			//}
			//if (this.ID > 0)
			if (this.EntityState == EntityState.Modified) {
				//this.EntityState = EntityState.Modified;
				// We are updating the entity
				//this.LastUpdatedDate = DateTime.Now;
				//this.LastUpdatedBy = CurrentUserId;
				//this.OnUpdate();
				//OnUpdating();
				IsValid = this.Validate();
				if (IsValid) {
					OnUpdating();
					Update();
					OnUpdated();
				}
			} else {
				//this.EntityState = EntityState.Added;
				// the entity is being created
				//this.LastUpdatedDate = null;
				//this.LastUpdatedBy = null;
				//this.CreatedDate = DateTime.Now;
				//this.CreatedBy = CurrentUserId;
				//this.OnCreate();
				//OnCreating();
				IsValid = this.Validate();
				if (IsValid) {
					OnCreating();
					Create();
					OnCreated();
				}
			}
			OnSaved();
		}

		public virtual void OnSaved() {

		}

		/// <summary>
		/// Inserts the entity into the database.
		/// </summary>
		public virtual void OnCreating() {
			this.LastUpdatedDate = null;
			this.LastUpdatedBy = null;
			this.CreatedDate = DateTime.Now;
			this.CreatedBy = CurrentUserId;
		}

		public virtual void OnCreated() {

		}

		/// <summary>
		/// Inserts the entity into the database.
		/// </summary>
		/// <param name="performValidation">
		/// True: perform validation.
		/// <para>
		/// False: do not perform validation.
		/// </para>
		/// </param>
		//public virtual void OnCreate(bool performValidation) {
		//    //For Validation - the entity state must be set to use some custom validation
		//    this.EntityState = EntityState.Added;
		//    if (performValidation) {
		//        IsValid = this.Validate(ValidationRuleSets.Create);
		//    }
		//}

		/// <summary>
		/// Updates the entity in the database.
		/// </summary>
		public virtual void OnUpdating() {
			this.LastUpdatedDate = DateTime.Now;
			this.LastUpdatedBy = CurrentUserId;
		}

		public virtual void OnUpdated() {

		}

		/// <summary>
		/// Updates the entity in the database.
		/// </summary>
		/// <param name="performValidation">
		/// True: perform validation.
		/// <para>
		/// False: do not perform validation.
		/// </para>
		/// </param>
		//public virtual void OnUpdate(bool performValidation) {
		//    this.EntityState = EntityState.Modified;
		//    if (performValidation) {
		//        IsValid = this.Validate(ValidationRuleSets.Update);
		//    }
		//}

		protected virtual void Create() {
			//// Set the Created date and Created By
			//this.CreatedDate = DateTime.Now;
			//this.CreatedBy = CurrentUserId;
			Service.Create((T)((object)this));
			//Context.Add((T) ((object) this));
		}

		protected virtual void Update() {
			//// Set the Created date and Created By
			//this.LastUpdatedDate = DateTime.Now;
			//this.LastUpdatedBy = CurrentUserId;
			Service.Update((T)((object)this));
		}

		public virtual void OnDeleting() {
		}

		public virtual void Delete() {
			OnDeleting();
			//Repository.Delete(this.ID);
			Service.Delete(this);
			OnDeleted();
		}

		public virtual void OnDeleted() {
		}

		public virtual void OnDestroying() {
		}

		protected virtual void Destroy() {
			OnDestroying();
			Service.Delete(this.ID);
			OnDestroyed();
		}

		public virtual void OnDestroyed() {
		}

		///// <summary>
		///// Save nested entities
		///// </summary>
		//protected virtual void SaveChildObjects()
		//{
		//}

		//public List<EnumeratedValue> GetEnumeratedValues(string type) {
		//    Type typ = type.GetType();
		//    using (WVCApiContext context = new WVCApiContext()) {


		//         MethodInfo method = typeof(DbContext).GetMethod("GetTable");         


		//             MethodInfo genericMethod = method.MakeGenericMethod(new Type[] {typ});             
		//             genericMethod.Invoke(context, null); 


		//    }
		//}

		//private DbSet<T> GetTable<T>(this DbContext context) {
		//    return null;
		//}

		#region EntityFactory methods
		public static IQueryable<T> GetById(int? id) {
			if (id.HasValue) {
				return GetById(id.Value);
			} else {
				return All();
			}
		}

		public static IQueryable<T> GetById(int id) {
			return Service.GetById(id); 
		}

		public static IQueryable<T> GetByIds(List<int> ids) {
			return Service.GetByIds(ids);
		}

		/// <summary>
		/// Use this method if you know that the Entity doesnt have EntityID. The only difference from All is that Query doesnt filter on EntityID
		/// </summary>
		/// <returns></returns>
		public static IQueryable<T> Query() {
			return Service.All();
		}

		public static IQueryable<T> All() {
			return Service.All();
		}

		public static IQueryable<T> Where(T criteria) {
			return All().Where(criteria);
		}
		#endregion
	}

	public interface IEntityFactory<T> where T : BaseModel<T> {
		IQueryable<T> GetById(int? id, bool entityFilter = true);
		IQueryable<T> GetById(int id, bool entityFilter = true);
		IQueryable<T> GetByIds(List<int> ids);
		IQueryable<T> Query();
		IQueryable<T> All(bool entityFilter = true);
		IQueryable<T> Where(T criteria, bool entityFilter = true);
		T Instance { get; }
	}
	 

	public static class EntityFactory<T> where T : BaseModel<T> {
		public static IQueryable<T> GetById(int? id) {
			return BaseModel<T>.GetById(id);
		}

		public static IQueryable<T> GetById(int id) {
			return BaseModel<T>.GetById(id);
		}

		public static IQueryable<T> GetByIds(List<int> ids) {
			return BaseModel<T>.GetByIds(ids);
		}

		/// <summary>
		/// Use this method if you know that the Entity doesnt have EntityID. The only difference from All is that Query doesnt filter on EntityID
		/// </summary>
		/// <returns></returns>
		public static IQueryable<T> Query() {
			return BaseModel<T>.Query();
		}

		public static IQueryable<T> All() {
			return BaseModel<T>.All();
		}

		public static IQueryable<T> Where(T criteria) {
			return BaseModel<T>.Where(criteria);
		}
	}

	public interface IService<T> where T : BaseModel {
		IQueryable<T> GetById(int id, bool entityFilter = true);
		IQueryable<T> GetByIds(List<int> ids, bool entityFilter = true);
		IQueryable<T> All(bool entityFilter = true);

		T Create(T obj, bool delaySave = false);
		bool Update(T obj, bool delaySave = false);
		void Delete(int id, bool delaySave = false);
		void Delete<T>(T obj, bool delaySave = false) where T : BaseModel;
		void Destroy(int id, bool delaySave = false);
		void Destroy<T>(T obj, bool delaySave = false) where T : BaseModel;
		void SaveChanges();
	}

	public interface IServiceFactory {
		IService<T> Create<T>() where T : BaseModel;
		void Register<T>(IService<T> service) where T : BaseModel;
	}

	public enum ErrorType {
		Error = 1,
		Warning = 2,
		NotFound = 3
	}


	public class ErrorInfo {
		public ErrorType ErrorType = ErrorType.Error;
		/// <summary>
		/// To support serialization
		/// </summary>
		public ErrorInfo() {

		}
		public ErrorInfo(string propertyName, string errorMessage) {
			this.PropertyName = propertyName;
			this.ErrorMessage = errorMessage;
		}
		public ErrorInfo(string propertyName, string errorMessage, object onObject) {
			this.PropertyName = propertyName;
			this.ErrorMessage = errorMessage;
			this.Object = onObject;
		}
		public ErrorInfo(string propertyName, string errorMessage, object onObject, ErrorType errorType) {
			this.PropertyName = propertyName;
			this.ErrorMessage = errorMessage;
			this.Object = onObject;
			this.ErrorType = errorType;
		}

		public string ErrorMessage { get; set; }
		public object Object { get; set; }
		public string PropertyName { get; set; }
	}

	public interface IErrorProvider {
		List<ErrorInfo> Errors { get; set; }
		void Add(string propertyName, string error);
		void Add(ErrorInfo error);
		string Name { get; }
	}

	public class ErrorProvider : IErrorProvider {

		//[ThreadStatic]
		//private static IErrorProvider _current = null;
		/// <summary>
		/// Returns the default error provider
		/// </summary>
		public static IErrorProvider Current {
			get {
				//if (_current == null) {
				//    // Dont use HttpContextErrorProvider. The problem is that the Context Provider adds the Error to
				//    // the Context using Context.AddError. MVC API throws InternalServerError in that case, even if you explicitly 
				//    // set the StatusCode on the HttpResponseMessage
				//    //_current = new HttpContextErrorProvider();
				//    _current = new ErrorProvider();
				//}
				//return _current;
				IErrorProvider _current = HttpContextFactory.GetHttpContext().Items["ErrorProvider"] as IErrorProvider;
				if (_current == null) {
					_current = new ErrorProvider();
					HttpContextFactory.GetHttpContext().Items.Add("ErrorProvider", _current);
				}
				Debug.WriteLine("Name:ErrorInfo.Current:" + _current.Name);
				return _current;
			}
		}


		private List<ErrorInfo> _errors = null;
		public List<ErrorInfo> Errors {
			get {
				return _errors;
			}
			set {
				_errors = value;
			}
		}

		private string _name = string.Empty;
		public string Name {
			get { return _name; }
		}

		public ErrorProvider() {
			_name = Guid.NewGuid().ToString() + " " + DateTime.Now.Ticks.ToString();
			_errors = new List<ErrorInfo>();
			Debug.WriteLine(string.Format("Name: {0}", _name));
		}

		public ErrorProvider(string propertyName, string error) {
			Errors.Add(new ErrorInfo(propertyName, error));
		}

		public virtual void Add(string propertyName, string error) {
			Errors.Add(new ErrorInfo(propertyName, error));
		}

		public virtual void Add(ErrorInfo error) {
			Errors.Add(error);
		}

		public virtual void CopyErrorsToModelState(System.Web.Mvc.ModelStateDictionary ModelState) {
			if (Errors.Any()) {
				foreach (var err in Errors) {
					ModelState.AddModelError(err.PropertyName, err.ErrorMessage);
				}
			}
		}
	}

	public static class HttpContextFactory {
		[ThreadStatic]
		private static HttpContextBase mockHttpContext;

		public static void SetHttpContext(HttpContextBase httpContextBase) {
			mockHttpContext = httpContextBase;
		}

		public static void ResetHttpContext() {
			mockHttpContext = null;
		}

		public static HttpContextBase GetHttpContext() {
			if (mockHttpContext != null) {
				return mockHttpContext;
			}

			if (HttpContext.Current != null) {
				return new HttpContextWrapper(HttpContext.Current);
			}

			return null;
		}
	}

	public class ValidationHelper {
		/// <summary>
		/// Get any errors associated with the model also investigating any rules dictated by attached Metadata buddy classes.
		/// </summary>
		/// <param name="instance"></param>
		/// <returns></returns>
		public static IEnumerable<ErrorInfo> Validate(object instance) {

			var metadataAttrib =
				instance.GetType().GetCustomAttributes(typeof(MetadataTypeAttribute), true).OfType
					<MetadataTypeAttribute>().FirstOrDefault();
			var buddyClassOrModelClass = metadataAttrib != null ? metadataAttrib.MetadataClassType : instance.GetType();
			var buddyClassProperties = TypeDescriptor.GetProperties(buddyClassOrModelClass).Cast<PropertyDescriptor>();
			var modelClassProperties = TypeDescriptor.GetProperties(instance.GetType()).Cast<PropertyDescriptor>();

			List<ErrorInfo> errors = (from buddyProp in buddyClassProperties
									  join modelProp in modelClassProperties on buddyProp.Name equals modelProp.Name
									  from attribute in buddyProp.Attributes.OfType<ValidationAttribute>()
									  where !attribute.IsValid(modelProp.GetValue(instance))
									  select
										  new ErrorInfo(buddyProp.Name,
														attribute.FormatErrorMessage(attribute.ErrorMessage), instance))
				.ToList();
			// Add in the class level custom attributes
			IEnumerable<ErrorInfo> classErrors =
				from attribute in TypeDescriptor.GetAttributes(buddyClassOrModelClass).OfType<ValidationAttribute>()
				where !attribute.IsValid(instance)
				select new ErrorInfo("ClassLevelCustom", attribute.FormatErrorMessage(attribute.ErrorMessage), instance);

			errors.AddRange(classErrors);
			 
			return errors.AsEnumerable();
		}

		public static string GetErrorInfo(IEnumerable<ErrorInfo> errorInfo) {
			StringBuilder errors = new StringBuilder();
			if (errorInfo != null) {
				foreach (var err in errorInfo.ToList()) {
					errors.Append(err.ErrorMessage + "\n");
				}
			}
			return errors.ToString();
		}

	}
}
