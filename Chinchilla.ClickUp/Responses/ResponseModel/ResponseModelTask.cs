using Chinchilla.ClickUp.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Chinchilla.ClickUp.Responses.Model
{

	/// <summary>
	/// Model object of Task information response
	/// </summary>
	public class ResponseModelTask
		: Helpers.IResponse
	{
		/// <summary>
		/// Id of the Task
		/// </summary>
		[JsonProperty("id")]
		public string Id { get; set; }

		/// <summary>
		/// Name of the task
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		/// Text Content (Description) of the task
		/// </summary>
		[JsonProperty("text_content")]
		public string TextContent { get; set; }

		/// <summary>
		/// Status of the task
		/// </summary>
		[JsonProperty("status")]
		public ResponseModelStatus Status { get; set; }

		/// <summary>
		/// Order index of Task
		/// </summary>
		[JsonProperty("orderindex")]
		public string OrderIndex { get; set; }

		/// <summary>
		/// Date Creation of the Task
		/// </summary>
		[JsonProperty("date_created")]
		[JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
		public DateTime? DateCreated { get; set; }

		/// <summary>
		/// Date last updated of the task
		/// </summary>
		[JsonProperty("date_updated")]
		[JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
		public DateTime? DateUpdated { get; set; }

		/// <summary>
		/// Date when task closed
		/// </summary>
		[JsonProperty("date_closed")]
		[JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
		public DateTime? DateClosed { get; set; }

		/// <summary>
		/// Model user with the information of the creator
		/// </summary>
		[JsonProperty("creator")]
		public ResponseModelUser Creator { get; set; }

		/// <summary>
		/// List of Model User with the information of the user assigned at this task
		/// </summary>
		[JsonProperty("assignees")]
		public List<ResponseModelUser> Assignees { get; set; }

		/// <summary>
		/// List of Model Tags with the information of the tag associated at this task
		/// </summary>
		[JsonProperty("tags")]
		public List<ResponseModelTag> Tags { get; set; }

		/// <summary>
		/// The Id of the parent Task
		/// </summary>
		[JsonProperty("parent")]
		public string Parent { get; set; }

		/// <summary>
		/// Model Priority with the information of priority assigned
		/// </summary>
		[JsonProperty("priority")]
		public ResponseModelPriority Priority { get; set; }

		/// <summary>
		/// Due Date of the Task
		/// </summary>
		[JsonProperty("due_date")]
		[JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
		public DateTime? DueDate { get; set; }

		/// <summary>
		/// Start Date of the Task
		/// </summary>
		[JsonProperty("start_date")]
		[JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// Points of the task
		/// </summary>
		[JsonProperty("points")]
		public double? Points { get; set; }                 // TO DO : CONTROL ! NO RESPONSE TESTED !

		/// <summary>
		/// Estimated Time of execution of the Task
		/// </summary>
		[JsonProperty("time_estimated")]
		[JsonConverter(typeof(JsonConverterTimeSpanMilliseconds))]
		public TimeSpan TimeEstimate { get; set; }

		/// <summary>
		/// Model List with information of the list where task it's assigned
		/// </summary>
		[JsonProperty("list")]
		public ResponseModelList List { get; set; }

		/// <summary>
		/// Model Project with information of the project where task it's assigned
		/// </summary>
		[JsonProperty("project")]
		public ResponseModelFolder Project { get; set; }

		/// <summary>
		/// Model Space with information of the space where task it's assigned
		/// </summary>
		[JsonProperty("space")]
		public ResponseModelSpace Space { get; set; }
		
		/// <summary>
		/// Url of the Task
		/// </summary>
		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("custom_fields")]
		public List<CustomField> CustomFields { get; set; }

        public class CustomField
        {
            [JsonProperty("id")]
            public string Id { get; }

            [JsonProperty("name")]
            public string Name { get; }

            [JsonProperty("type")]
            public string Type { get; }

            [JsonProperty("value")]
            public object Value {get;}
            
            public CustomField (string id, string name, string type, object value)
            {
                Id = id;
                Name = name;
                Type = type;
                Value = value;
            }

            public ResponseModelTask
            ValueToTask()
            {
                /// check if Value can convert to a task
                if (Value is JArray jArray)
                {
                    if (jArray.Count == 1)
                    {
                        var item = (JObject)jArray[0];
                        var task = new ResponseModelTask()
                        {
                            Id = item.GetValue("id").ToString(),
                            Name = item.GetValue("name").ToString(),
                        };
                        return task;
                    }
                }
                return null;
            }
        }
	}
}