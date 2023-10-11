using Newtonsoft.Json;
using Chinchilla.ClickUp.Enums;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Chinchilla.ClickUp.CustomFields
{
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
        public CustomFieldTypeConfig TypeConfig { get; } // set by constructor


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
            Value = SetCustomFieldValue(valueObject);
            TypeConfig = SetCustomFieldTypeConfig(typeConfigObject);
        }

        private CustomFieldTypeConfig
        SetCustomFieldTypeConfig(object typeConfig)
        {
            try
            {
                switch (Type)
                {
                    case FieldType.drop_down:
                        var customfieldTypeConfigDropDown = new CustomFieldTypeConfigDropDown()
                        {
                            Options = new List<CustomFieldTypeConfigOptionDropDown>()
                        };
                        if (!(typeConfig is JObject jObjectTypeConfigDropDown))
                        {
                            return customfieldTypeConfigDropDown;
                        }
                        List<JToken> dropDownResults = jObjectTypeConfigDropDown["options"].Children().ToList();
                        foreach (JToken result in dropDownResults)
                        {
                            CustomFieldTypeConfigOptionDropDown option = result.ToObject<CustomFieldTypeConfigOptionDropDown>();
                            customfieldTypeConfigDropDown.Options.Add(option);
                        }
                        return customfieldTypeConfigDropDown;

                    case FieldType.labels:
                        var customfieldTypeConfigLabel = new CustomFieldTypeConfigLabel()
                        {
                            Options = new List<CustomFieldTypeConfigOptionLabel>()
                        };
                        if (!(typeConfig is JObject jObjectTypeConfigLabel))
                        {
                            return customfieldTypeConfigLabel;
                        }
                        List<JToken> labelResults = jObjectTypeConfigLabel["options"].Children().ToList();
                        foreach (JToken result in labelResults)
                        {
                            CustomFieldTypeConfigOptionLabel option = result.ToObject<CustomFieldTypeConfigOptionLabel>();
                            customfieldTypeConfigLabel.Options.Add(option);
                        }
                        return customfieldTypeConfigLabel;

                    case FieldType.list_relationship:
                        var customfieldTypeConfigListRelationship = new CustomFieldTypeConfigListRelationship()
                        {
                            Fields = new List<CustomFieldTypeConfigField>()
                        };
                        if (!(typeConfig is JObject jObjectTypeConfigListRelationship))
                        {
                            return customfieldTypeConfigListRelationship;
                        }

                        List<JToken> listRelationshipResults = jObjectTypeConfigListRelationship["fields"].Children().ToList();
                        foreach (JToken result in listRelationshipResults)
                        {
                            CustomFieldTypeConfigField field = result.ToObject<CustomFieldTypeConfigField>();
                            customfieldTypeConfigListRelationship.Fields.Add(field);
                        }
                        return customfieldTypeConfigListRelationship;

                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                var debug = true;
            }
            return null;
        }

        private CustomFieldValue
        SetCustomFieldValue(object value)
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
                            Value = new List<CustomFieldValueListRelationshipValue>()
                        };
                        if (!(value is JArray jArrayValueListRelationship))
                        {
                            return customFieldValueListRelationship;
                        }
                        foreach (JObject jObject in jArrayValueListRelationship)
                        {
                            var valueListRelationship = new CustomFieldValueListRelationshipValue()

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
            catch (Exception e)
            {
                var debug = true;
            }
            return null;
        }

        public Dictionary<string, string>
        GetCustomFieldSelection()
        {
            var ret = new Dictionary<string, string>();
            /// This is a pile... but it has to be done.
            /// Based on the FieldType the return is different... yay
            /// Some types have useful data in value and some have useful data in typeConfig
            /// Some need a mix of data from both... super sweet
            try
            {
                switch (Type)
                {
                    case FieldType.drop_down:
                        /// use the taskField.value to get the index of the selected dropdown from TypeConfigOptions
                        /// return a dictionary with the index and name of the selected dropdown element
                        if (!(Value is CustomFieldValueDropDown dropDownValue))
                        {
                            return null;
                        }
                        if (!(TypeConfig is CustomFieldTypeConfigDropDown dropDownTypeConfig))
                        {
                            return null;
                        }
                        var optionIndex = dropDownValue.Value;
                        var selectedValue = dropDownTypeConfig.Options[optionIndex];
                        ret.Add(optionIndex.ToString(), selectedValue.Name);
                        return ret;

                    case FieldType.labels:
                        /// use taskField.value to get list of Ids of selected labels from TypeConfigOptions
                        /// return dictionary of ids and names of the selected labels
                        if (!(Value is CustomFieldValueLabel labelValue))
                        {
                            return null;
                        }
                        if (!(TypeConfig is CustomFieldTypeConfigLabel labelTypeConfig))
                        {
                            return null;
                        }
                        var selectedOptionIds = labelValue.Value;
                        if (selectedOptionIds == null || selectedOptionIds.Count == 0)
                        {
                            return null;
                        }
                        var options = labelTypeConfig.Options;
                        if (options == null || options.Count == 0)
                        {
                            return null;
                        }
                        var selectedOptions = new List<string>();
                        foreach (var selectedOptionId in selectedOptionIds)
                        {
                            if (selectedOptionId == null || selectedOptionId == "")
                            {
                                continue;
                            }
                            foreach (var option in options)
                            {
                                if (option.Id == selectedOptionId)
                                {
                                    ret.Add(selectedOptionId, option.Label);
                                }
                            }
                        }
                        return ret;

                    case FieldType.list_relationship:
                        /// use taskField.Value to get the tasks in the list_relationship
                        /// return a dictionary with the id and name of the related tasks
                        if (!(Value is CustomFieldValueListRelationship listRelationshipValue))
                        {
                            return null;
                        }
                        var relatedTasks = listRelationshipValue.Value;
                        if (relatedTasks == null || relatedTasks.Count == 0)
                        {
                            return null;
                        }
                        foreach (var task in relatedTasks)
                        {
                            if (task.Id == null || task.Id == "")
                            {
                                continue;
                            }
                            if (task.Name == null || task.Name == "")
                            {
                                continue;
                            }
                            ret.Add(task.Id, task.Name);
                        }
                        return ret;

                    case FieldType.number:
                        /// use the taskField.value to get the set number
                        /// return a dictionary for whatever reason
                        if (!(Value is CustomFieldValueNumber numberValue))
                        {
                            return null;
                        }
                        ret.Add("number", numberValue.Value.ToString());
                        return ret;

                    case FieldType.url:
                        /// use the taskField.value to get the set url
                        /// return a dictionary for whatever reason
                        if (!(Value is CustomFieldValueUrl urlValue))
                        {
                            return ret;
                        }
                        ret.Add("url", urlValue.Value.ToString());
                        return ret;

                    default:
                        return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return null;
        }
    }
}
