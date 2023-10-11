using Chinchilla.ClickUp.Enums;
using Chinchilla.ClickUp.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Params
{

	/// <summary>
	/// The param object of Get Tasks By List ID request
	/// </summary>
	public class ParamsGetTasksByListId
	{
		#region Attributes

		/// <summary>
		/// The Team Id 
		/// </summary>
		[JsonProperty("list_id")]
		[DataMember(Name = "list_id")]
		public string ListId { get; set; }

		/// <summary>
		/// Include Archived Task [Not set Statuses filter!]
		/// </summary>
		[JsonProperty("archived")]
		[DataMember(Name = "archived")]
		public bool? Archived { get; set; }

		/// <summary>
		/// Rappresent the Page Filter
		/// </summary>
		[JsonProperty("page")]
		[DataMember(Name = "page")]
		public int? Page { get; set; }

		/// <summary>
		/// Rappresent the order of Data
		/// </summary>
		[JsonProperty("order_by")]
		[DataMember(Name = "order_by")]
		public TaskOrderBy? OrderBy { get; set; }

		/// <summary>
		/// Reverse Order
		/// </summary>
		[JsonProperty("reverse")]
		[DataMember(Name = "reverse")]
		public bool? Reverse { get; set; }

		/// <summary>
		/// Include Subtasks
		/// </summary>
		[JsonProperty("subtasks")]
		[DataMember(Name = "subtasks")]
		public bool? Subtasks { get; set; }

		/// <summary>
		/// Query only on spaces with this ids
		/// </summary>
		[JsonProperty("space_ids")]
		[DataMember(Name = "space_ids")]
		public List<string> SpaceIds { get; set; }

		/// <summary>
		/// Query only on projects with this ids
		/// </summary>
		[JsonProperty("projects_ids")]
		[DataMember(Name = "projects_ids")]
		public List<string> ProjectIds { get; set; }

		/// <summary>
		/// Query only on lists with this ids
		/// </summary>
		[JsonProperty("list_ids")]
		[DataMember(Name = "list_ids")]
		public List<string> ListIds { get; set; }

		/// <summary>
		/// Query only on status with this status
		/// </summary>
		[JsonProperty("statuses")]
		[DataMember(Name = "statuses")]
		public List<string> Statuses { get; set; }

		/// <summary>
		/// Include Closed Task [Not set Statuses filter!]
		/// </summary>
		[JsonProperty("include_closed")]
		[DataMember(Name = "include_closed")]
		public bool? IncludeClosed { get; set; }

		/// <summary>
		/// Query only on task assign at users with this ids
		/// </summary>
		[JsonProperty("assignees")]
		[DataMember(Name = "assignees")]
		public List<long> Assignees { get; set; }

		/// <summary>
		/// Filter due date greater than posix time
		/// </summary>
		[JsonProperty("due_date_gt")]
		[DataMember(Name = "due_date_gt")]
		[JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
		public DateTime? DueDateGreaterThan { get; set; }

		/// <summary>
		/// Filter due date less than posix time
		/// </summary>
		[JsonProperty("due_date_lt")]
		[DataMember(Name = "due_date_lt")]
		[JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
		public DateTime? DueDateLessThan { get; set; }

		/// <summary>
		/// Filter date created greater than posix time
		/// </summary>
		[JsonProperty("date_created_gt")]
		[DataMember(Name = "date_created_gt")]
		[JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
		public DateTime? DateCreatedGreaterThan { get; set; }

		/// <summary>
		/// Filter date created less than posix time
		/// </summary>
		[JsonProperty("date_created_lt")]
		[DataMember(Name = "date_created_lt")]
		[JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
		public DateTime? DateCreatedLessThan { get; set; }

		/// <summary>
		/// Filter date updated greater than posix time
		/// </summary>
		[JsonProperty("date_updated_gt")]
		[DataMember(Name = "date_updated_gt")]
		[JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
		public DateTime? DateUpdatedGreaterThan { get; set; }

		/// <summary>
		/// Filter date updated less than posix time
		/// </summary>
		[JsonProperty("date_updated_lt")]
		[DataMember(Name = "date_updated_lt")]
		[JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
		public DateTime? DateUpdatedLessThan { get; set; }

		#endregion


		#region Constructor

		/// <summary>
		/// The constructor of ParamsGetTasksByListId
		/// </summary>
		/// <param name="teamId"></param>
		public ParamsGetTasksByListId(string listId)
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