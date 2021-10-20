using Chinchilla.ClickUp.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public class CustomFieldTypeConfig
        {
        }

        public class CustomFieldTypeConfigListRelationship : CustomFieldTypeConfig
        {
            [JsonProperty("fields")]
            public List<TypeConfigField> Fields { get; set; }
        }

        public class CustomFieldTypeConfigDropDown : CustomFieldTypeConfig
        {
            [JsonProperty("default")]
            public int Default { get; set; }

            [JsonProperty("options")]
            public List<TypeConfigDropDownOption> Options { get; set; }
        }

        public class CustomFieldTypeConfigLabel : CustomFieldTypeConfig
        {
            [JsonProperty("options")]
            public List<TypeConfigLabelOption> Options { get; set; }
        }


        public class TypeConfigField
        {
            [JsonProperty("field")]
            public string Field { get; set; }

            [JsonProperty("width")]
            public int Width { get; set; }

            [JsonProperty("hidden")]
            public bool Hidden { get; set; }
        }

        public class TypeConfigOption
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("color")]
            public string Color { get; set; }

            [JsonProperty("orderindex")]
            public int OrderIndex { get; set; }
        }

        public class TypeConfigDropDownOption : TypeConfigOption
        {
            [JsonProperty("name")]
            public string Name { get; set; }
        }

        public class TypeConfigLabelOption : TypeConfigOption
        {
            [JsonProperty("label")]
            public string Label { get; set; }
        }


        public class CustomFieldValue
        {
        }

        public class CustomFieldValueDropDown : CustomFieldValue
        {
            [JsonProperty("value")]
            public int Value { get; set; }
        }

        public class CustomFieldValueUrl : CustomFieldValue
        {
            [JsonProperty("value")]
            public string Value { get; set; }
        }

        public class CustomFieldValueNumber : CustomFieldValue
        {
            [JsonProperty("value")]
            public double Value { get; set; }
        }

        public class CustomFieldValueLabel : CustomFieldValue
        {
            [JsonProperty("value")]
            public List<string> Value { get; set; }
        }

        public class CustomFieldValueListRelationship : CustomFieldValue
        {
            [JsonProperty("value")]
            public List<ValueListRelationship> Value { get; set; }
        }

        public class ValueListRelationship 
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("color")]
            public string Color { get; set; }

            [JsonProperty("custom_type")]
            public string CustomType { get; set; }

            [JsonProperty("team_id")]
            public string TeamId { get; set; }

            [JsonProperty("deleted")]
            public bool Deleted { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("access")]
            public bool Access { get; set; }
        }

        public class CustomField
        {
            [JsonProperty("id")]
            public string Id { get; }

            [JsonProperty("name")]
            public string Name { get; }

            [JsonProperty("type")]
            public string TypeString { get; } // store json input
            public FieldType Type { get; } // set by constructor

            /// <summary>
            /// value type is not known until you look at the json data
            /// must be collected as an object then set as a CustomFieldValue
            /// </summary>
            [JsonProperty("value")]
            public object ValueObject { get; } // store json input
            public CustomFieldValue Value { get; } // set by constructor

            /// <summary>
            /// typeConfig type is not known until you look at the json data
            /// must be collected as an object then set as a CustomFieldTypeConfig
            /// </summary>
            [JsonProperty("type_config")]
            public object TypeConfigObject { get; } // store json input
            public CustomFieldTypeConfig TypeConfig {get;} // set by constructor


            public CustomField(string id, string name, string typeString, object valueObject, object typeConfigObject) // arguments must match name and type of JsonProperties above
            {
                Id = id;
                Name = name;
                TypeString = typeString;
                ValueObject = valueObject;
                TypeConfigObject = typeConfigObject;

                if (!Enum.TryParse(typeString, out FieldType fieldType))
                {
                    Type = FieldType.bad_type;
                    return;
                }
                // type must be known to Determine CustomFieldValue and CustomFieldTypeConfig
                Type = fieldType;
                Value = DetermineCustomFieldValue(valueObject);
                TypeConfig = DetermineCustomFieldTypeConfig(typeConfigObject);
            }

            private CustomFieldTypeConfig
            DetermineCustomFieldTypeConfig(object typeConfig)
            {
                try
                {
                    switch (Type)
                    {
                        case FieldType.drop_down:
                            var customfieldTypeConfigDropDown = new CustomFieldTypeConfigDropDown()
                            {
                                Options = new List<TypeConfigDropDownOption>()
                            };
                            if (!(typeConfig is JObject jObjectTypeConfigDropDown))
                            {
                                return customfieldTypeConfigDropDown;
                            }
                            List<JToken> dropDownResults = jObjectTypeConfigDropDown["options"].Children().ToList();
                            foreach (JToken result in dropDownResults)
                            {
                                TypeConfigDropDownOption option = result.ToObject<TypeConfigDropDownOption>();
                                customfieldTypeConfigDropDown.Options.Add(option);
                            }
                            return customfieldTypeConfigDropDown;

                        case FieldType.labels:
                            var customfieldTypeConfigLabel = new CustomFieldTypeConfigLabel()
                            {
                                Options = new List<TypeConfigLabelOption>()
                            };
                            if (!(typeConfig is JObject jObjectTypeConfigLabel))
                            {
                                return customfieldTypeConfigLabel;
                            }
                            List<JToken> labelResults = jObjectTypeConfigLabel["options"].Children().ToList();
                            foreach (JToken result in labelResults)
                            {
                                TypeConfigLabelOption option = result.ToObject<TypeConfigLabelOption>();
                                customfieldTypeConfigLabel.Options.Add(option);
                            }
                            return customfieldTypeConfigLabel;

                        case FieldType.list_relationship:
                            var customfieldTypeConfigListRelationship = new CustomFieldTypeConfigListRelationship()
                            {
                                Fields = new List<TypeConfigField>()
                            };
                            if (!(typeConfig is JObject jObjectTypeConfigListRelationship))
                            {
                                return customfieldTypeConfigListRelationship;
                            }

                            List<JToken> listRelationshipResults = jObjectTypeConfigListRelationship["fields"].Children().ToList();
                            foreach (JToken result in listRelationshipResults)
                            {
                                TypeConfigField field = result.ToObject<TypeConfigField>();
                                customfieldTypeConfigListRelationship.Fields.Add(field);
                            }
                            return customfieldTypeConfigListRelationship;

                        default:
                            break;
                    }
                }
                catch(Exception e)
                {
                    var debug = true;
                }
                return null;
            }

            private CustomFieldValue 
            DetermineCustomFieldValue(object value)
            {
                try
                {
                    switch (Type)
                    {
                        case FieldType.url:
                            if (value == null)
                            {
                                value = "";
                            }
                            var customFieldValueUrl = new CustomFieldValueUrl()
                            {
                                Value = value.ToString()
                            };
                            return customFieldValueUrl;

                        case FieldType.number:
                            var customFieldValueNumber = new CustomFieldValueNumber();
                            if (value == null)
                            {
                                return customFieldValueNumber;
                            }
                            if (double.TryParse(value.ToString(), out double doubleValue))
                            {
                                customFieldValueNumber.Value = doubleValue;
                            }
                            return customFieldValueNumber;

                        case FieldType.labels:
                            var customFieldValueLabel = new CustomFieldValueLabel()
                            {
                                Value = new List<string>()
                            };
                            if (!(value is JArray jArrayValueLabel))
                            {
                                return customFieldValueLabel;
                            }
                            foreach (var item in jArrayValueLabel)
                            {
                                customFieldValueLabel.Value.Add(item.ToString());
                            }
                            return customFieldValueLabel;

                        case FieldType.drop_down:
                            var customFieldValueDropDown = new CustomFieldValueDropDown();
                            if (value == null)
                            {
                                return customFieldValueDropDown;
                            }
                            if (int.TryParse(value.ToString(), out int intValue))
                            {
                                customFieldValueDropDown.Value = intValue;
                            }
                            return customFieldValueDropDown;

                        case FieldType.list_relationship:
                            var customFieldValueListRelationship = new CustomFieldValueListRelationship()
                            {
                                Value = new List<ValueListRelationship>()
                            };
                            if (!(value is JArray jArrayValueListRelationship))
                            {
                                return customFieldValueListRelationship;
                            }
                            foreach (JObject jObject in jArrayValueListRelationship)
                            {
                                var valueListRelationship = new ValueListRelationship()
                                {
                                    Id = jObject.GetValue("id").ToString(),
                                    Name = jObject.GetValue("name").ToString(), 
                                    Status = jObject.GetValue("status").ToString(), 
                                    Color = jObject.GetValue("color").ToString(), 
                                    CustomType = jObject.GetValue("custom_type").ToString(), 
                                    TeamId = jObject.GetValue("team_id").ToString(), 
                                    Deleted = (bool)jObject.GetValue("deleted"), 
                                    Url = jObject.GetValue("url").ToString(), 
                                    Access = (bool)jObject.GetValue("access"), 
                                };
                                customFieldValueListRelationship.Value.Add(valueListRelationship);
                            }
                            return customFieldValueListRelationship;

                        default:
                            break;
                    }
                }
                catch(Exception e){
                    var debug = true;
                }
                return null;
            }

            public enum FieldType
            {
                bad_type = -1, // not a real type

                url = 0,
                drop_down = 1,
                //email = 2,
                //phone = 3,
                //date = 4,
                //text = 5,
                //checkbox = 6,
                number = 7,
                //currency = 8,
                //tasks = 9,
                //users = 10,
                //emoji = 11,
                labels = 13,
                //automatic_progress = 14,
                //manual_progress = 15,
                //short_text = 16,
                list_relationship = 17,
            }
        }
	}
}