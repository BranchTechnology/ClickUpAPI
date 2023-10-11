using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Chinchilla.ClickUp.Params
{

	/// <summary>
	/// The param object of Edit Task Custom Field Request
	/// </summary>
	public class ParamsEditTaskCustomField
	{
		#region Attributes

		/// <summary>
		/// The Task Id
		/// </summary>
		[JsonProperty("task_id")]
		[DataMember(Name = "task_id")]
		public string TaskId { get; set; }

		/// <summary>
		/// The Field Id
		/// </summary>
		[JsonProperty("field_id")]
		[DataMember(Name = "field_id")]
		public string FieldId { get; set; }

		#endregion


		#region Constructor

		/// <summary>
		/// Constructor of ParamsEditTask
		/// </summary>
		/// <param name="taskId"></param>
		/// <param name="fieldId"></param>
		public ParamsEditTaskCustomField(string taskId, string fieldId)
		{
			TaskId = taskId;
            FieldId = fieldId;
		}

		#endregion


		#region Public Methods

		/// <summary>
		/// Method that validate data insert
		/// </summary>
		public void ValidateData()
		{
			if (string.IsNullOrEmpty(TaskId))
			{
				throw new ArgumentNullException("TaskId");
			}
			if (string.IsNullOrEmpty(FieldId))
			{
				throw new ArgumentNullException("TaskId");
			}
		}

		#endregion
	}
}