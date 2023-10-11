using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Params
{

    /// <summary>
    /// The param object of Get List Custom Fields
    /// </summary>
    public class ParamsGetListCustomFields
    {
        #region Attributes

		/// <summary>
		/// The List Id 
		/// </summary>
		[JsonProperty("list_id")]
		[DataMember(Name = "list_id")]
		public string ListId { get; set; }

        #endregion

        #region Constructor

		/// <summary>
		/// The constructor of ParamsGetListCustomFields
		/// </summary>
		/// <param name="listId"></param>
		public ParamsGetListCustomFields(string listId)
		{
			ListId = listId;
		}

        #endregion

        #region Public Methods

		/// <summary>
		/// Method that validate the data insert
		/// </summary>
		public void ValidateData()
		{
			if (string.IsNullOrEmpty(ListId))
			{
				throw new ArgumentNullException("ListId");
			}
		}

        #endregion
    }
}
