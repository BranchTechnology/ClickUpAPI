using Newtonsoft.Json;
using System;

namespace Chinchilla.ClickUp.Requests
{
	/// <summary>
	/// Request object for method SetTaskCustomField()
	/// </summary>
	public class RequestEditTaskCustomFieldString 
	{
		#region Attributes

		/// <summary>
		/// Name of the list
		/// </summary>
		[JsonProperty("value")]
		public string Value { get; set; }

		#endregion


		#region Constructor

		/// <summary>
		/// The constructor of RequestEditList
		/// </summary>
		/// <param name="value"></param>
		public RequestEditTaskCustomFieldString(string value)
		{
            Value = value;
		}

		#endregion


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
	/// <summary>
	/// Request object for method SetTaskCustomField()
	/// </summary>
	public class RequestEditTaskCustomFieldDouble 
	{
		#region Attributes

		/// <summary>
		/// Name of the list
		/// </summary>
		[JsonProperty("value")]
		public double Value { get; set; }

		#endregion


		#region Constructor

		/// <summary>
		/// The constructor of RequestEditList
		/// </summary>
		/// <param name="value"></param>
		public RequestEditTaskCustomFieldDouble(double value)
		{
            Value = value;
		}

		#endregion


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