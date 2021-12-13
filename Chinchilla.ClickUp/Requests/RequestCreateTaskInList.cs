using Newtonsoft.Json;
using Chinchilla.ClickUp.Enums;
using System;
using System.Collections.Generic;
using Chinchilla.ClickUp.Helpers;

namespace Chinchilla.ClickUp.Requests
{
	/// <summary>
	/// Request object for method CreateTaskInList()
	/// </summary>
	public class RequestCreateTaskInList
	{
		#region Attributes

		/// <summary>
		/// Name of the task
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		/// Description of the task
		/// </summary>
		[JsonProperty("description")]
		public string Description { get; set; }

		/// <summary>
		/// Content of the task
		/// </summary>
		[JsonProperty("content")]
		public string Content { get; set; }

		/// <summary>
		/// List of user id that will be added to this task
		/// </summary>
		[JsonProperty("assignees")]
		public List<long> Assignees { get; set; }

		/// <summary>
		/// List of tags that will be added to this task
		/// </summary>
		[JsonProperty("tags")]
		public List<long> Tags { get; set; }

		/// <summary>
		/// Status of the task
		/// </summary>
		[JsonProperty("status")]
		public string Status { get; set; }

		/// <summary>
		/// Prioriry of the task
		/// </summary>
		[JsonProperty("priority")]
		public TaskPriority? Priority { get; set; }

		/// <summary>
		/// Due Date of the task
		/// </summary>
		[JsonProperty("due_date")]
		[JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
		public DateTime? DueDate { get; set; }

        [JsonProperty("due_date_time")]
        public bool? DueDateTime { get; set; }

        [JsonProperty("time_estimate")]
		[JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
        public bool? TimeEstimate { get; set; }

        [JsonProperty("start_date")]
		[JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
        public bool? StartDate { get; set; }

        [JsonProperty("start_date_time")]
        public bool? StartDateTime { get; set; }

        [JsonProperty("notify_all")]
        public bool? NotifyAll { get; set; }

        [JsonProperty("check_required_custom_fields")]
        public bool? CheckRequiredCustomFields { get; set; }

        // Not sure how to specify custom fields yet... seems like an array of structs with a template parameter type... no idea

        #endregion

        #region structs
        private struct CustomFieldURL
        {
            public string id { get; set; }
            public string value { get; set; }
        }

        private struct CustomFieldDropDown
        {
            public string id { get; set; }
            public int value { get; set; } 
        }

        private struct CustomFieldNumber
        {
            public string id { get; set; }
            public double value { get; set; } 
        }

        private struct CustomFieldLabel
        {
            public string id { get; set; }
            public List<string> value { get; set; } 
        }


        #endregion  


        #region Constructor

        /// <summary>
        /// Constructor of RequestCreateTaskInList
        /// </summary>
        /// <param name="name"></param>
        public RequestCreateTaskInList(string name)
		{
			Name = name;
		}

		#endregion


		#region Public Methods

		/// <summary>
		/// Validation method of data
		/// </summary>
		public void ValidateData()
		{
			if (string.IsNullOrEmpty(Name))
			{
				throw new ArgumentNullException("Name");
			}
		}

		#endregion
	}
}