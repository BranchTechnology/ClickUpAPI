using Newtonsoft.Json;
using Chinchilla.ClickUp.Responses.Model;
using System.Collections.Generic;
using Chinchilla.ClickUp.CustomFields;

namespace Chinchilla.ClickUp.Responses
{

	/// <summary>
	/// Response object of the method GetListCustomFields()
	/// </summary>
	public class ResponseListCustomFields
		: Helpers.IResponse
	{
		/// <summary>
		/// List of CustomField Model with information of the List
		/// </summary>
		[JsonProperty("fields")]
		//public List<ResponseModelListCustomField> Fields { get; set; }
        public List<CustomField> Fields { get; set; }
	}
}