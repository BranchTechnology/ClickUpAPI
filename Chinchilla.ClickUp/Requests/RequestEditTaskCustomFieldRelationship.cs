using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Chinchilla.ClickUp.Requests
{
	/// <summary>
	/// Request object for method SetTaskCustomField()
	/// </summary>
	public class RequestEditTaskCustomFieldRelationship 
	{
		#region Attributes

		/// <summary>
		/// Name of the list
		/// </summary>
		[JsonProperty("value")]
		public CustomField Value { get; set; }

		#endregion


		#region Constructor

		/// <summary>
		/// The constructor of RequestEditList
		/// </summary>
		/// <param name="value"></param>
		public RequestEditTaskCustomFieldRelationship(List<string> add, List<string> remove)
		{
            Value = new CustomField(add, remove);
		}

		/// <summary>
		/// The constructor of RequestEditList
		/// </summary>
		/// <param name="value"></param>
		public RequestEditTaskCustomFieldRelationship(string add)
		{
            Value = new CustomField(new List<string> { add }, null);
		}

		#endregion

        public class CustomField
        {
		    [JsonProperty("add")]
            public List<string> Add { get; }
            
		    [JsonProperty("rem")]
            public List<string> Remove { get; }
            
            
            public CustomField(List<string> add, List<string> remove)
            {
                Add = add;
                Remove = remove;
            }
        }

		#region Public Methods

		/// <summary>
		/// Validation method of data
		/// </summary>
		public void ValidateData()
		{
			if (Value == null)
			{
				throw new ArgumentNullException("Value");
			}
		}

		#endregion
	}
}