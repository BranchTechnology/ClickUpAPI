using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Chinchilla.ClickUp.Helpers;
using Chinchilla.ClickUp.Params;
using Chinchilla.ClickUp.Requests;
using Chinchilla.ClickUp.Responses;
using Chinchilla.ClickUp.Responses.Model;
using RestSharp;

namespace Chinchilla.ClickUp
{

	/// <summary>
	/// Object that interact through methods the API (v2) of ClickUp
	/// </summary>
	public class ClickUpApi
	{
		#region Private Attributes

		/// <summary>
		/// Base Address of API call
		/// </summary>
		private static readonly Uri _baseAddress = new Uri("https://api.clickup.com/api/v2/");

		/// <summary>
		/// The Access Token to add during the request
		/// </summary>
		///
		public string AccessToken { get; protected set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Create object with Personal Access Token
		/// </summary>
		/// <param name="accessToken">Personal Access Token</param>
		public ClickUpApi(string accessToken)
		{
			AccessToken = accessToken;
		}

		/// <summary>
		/// Create Object with <see cref="ParamsAccessToken"/>
		/// </summary>
		/// <param name="paramAccessToken">param access token object</param>
		public static ClickUpApi Create(ParamsAccessToken paramAccessToken)
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest("oauth/token", Method.POST);
			request.AddParameter("client_id", paramAccessToken.ClientId);
			request.AddParameter("client_secret", paramAccessToken.ClientSecret);
			request.AddParameter("code", paramAccessToken.Code);

			// execute the request
			ResponseGeneric<ResponseAccessToken, ResponseError> response = RestSharperHelper.ExecuteRequest<ResponseAccessToken, ResponseError>(client, request);

			string accessToken;
			// Manage Response
			if (response.ResponseSuccess == null)
				throw new Exception(response.ResponseError.Err);

			accessToken = response.ResponseSuccess.AccessToken;

			return new ClickUpApi(accessToken);
		}

		/// <summary>
		/// Create Object with <see cref="ParamsAccessToken"/>
		/// </summary>
		/// <param name="paramAccessToken">param access token object</param>
		public static Task<ClickUpApi> CreateAsync(ParamsAccessToken paramAccessToken)
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest("oauth/token", Method.POST);
			request.AddParameter("client_id", paramAccessToken.ClientId);
			request.AddParameter("client_secret", paramAccessToken.ClientSecret);
			request.AddParameter("code", paramAccessToken.Code);

			// execute the request
			var taskCompletionSource = new TaskCompletionSource<ClickUpApi>();
			var task = RestSharperHelper.ExecuteRequestAsync<ResponseAccessToken, ResponseError>(client, request)
				.ContinueWith(responseTask => {
					ResponseGeneric<ResponseAccessToken, ResponseError> response = responseTask.Result;

					// Manage Response
					if (response.ResponseSuccess == null)
						throw new Exception(response.ResponseError.Err);

					string accessToken = response.ResponseSuccess.AccessToken;

					taskCompletionSource.SetResult(new ClickUpApi(accessToken));
				});

			return taskCompletionSource.Task;
		}

		#endregion

		#region API Methods

		#region User
		/// <summary>
		/// Get the user that belongs to this token
		/// </summary>
		/// <returns>ResponseGeneric with ResponseAuthorizedUser response object</returns>
		public ResponseGeneric<ResponseAuthorizedUser, ResponseError> GetAuthorizedUser()
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"user", Method.GET);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			ResponseGeneric<ResponseAuthorizedUser, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseAuthorizedUser, ResponseError>(client, request);
			return result;
		}
		#endregion

		#region Teams
		/// <summary>
		/// Get the authorized teams for this token
		/// </summary>
		/// <returns>ResponseGeneric with ResponseAuthorizedTeams response object</returns>
		public ResponseGeneric<ResponseAuthorizedTeams, ResponseError> GetAuthorizedTeams()
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"team", Method.GET);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			ResponseGeneric<ResponseAuthorizedTeams, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseAuthorizedTeams, ResponseError>(client, request);
			return result;
		}

		/// <summary>
		/// Get a team's details. This team must be one of the authorized teams for this token.
		/// </summary>
		/// <param name="paramsGetTeamByID">param object of get team by ID request</param>
		/// <returns>ResponseGeneric with ResponseTeam response object</returns>
		public ResponseGeneric<ResponseTeam, ResponseError> GetTeamById(ParamsGetTeamById paramsGetTeamById)
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"team/{paramsGetTeamById.TeamId}", Method.GET);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			ResponseGeneric<ResponseTeam, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseTeam, ResponseError>(client, request);
			return result;
		}
        #endregion

        #region Tag

        public ResponseGeneric<ResponseModelTag, ResponseError> 
        SetTaskTag(string taskId, string tagName)
        {
            ParamsEditTask paramsEditTask = new ParamsEditTask(taskId);
            RequestEditTask requestEditTask = new RequestEditTask()
            {
                Tag = tagName,
            };
            var client = new RestClient(_baseAddress);
            var request = new RestRequest($"task/{paramsEditTask.TaskId}/tag/{requestEditTask.Tag}", Method.POST);
            request.AddHeader("authorization", AccessToken);
            request.AddJsonBody(requestEditTask);

            ResponseGeneric<ResponseModelTag, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseModelTag, ResponseError>(client, request);
            return result;
        } 

        #endregion Tag

        #region CustomFields
        public ResponseGeneric<ResponseListCustomFields, ResponseError> GetListCustomFields(ParamsGetListCustomFields paramsGetListCustomFields)
        {
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"list/{paramsGetListCustomFields.ListId}/field", Method.GET);
			request.AddHeader("authorization", AccessToken);

            // execute the request
			ResponseGeneric<ResponseListCustomFields, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseListCustomFields, ResponseError>(client, request);
			return result;
        }

        public ResponseGeneric<ResponseListCustomFields, ResponseError> GetListCustomFields(string listId)
        {
            ParamsGetListCustomFields paramsGetListCustomFields = new ParamsGetListCustomFields(listId);
            var responseGetListCustomFields = GetListCustomFields(paramsGetListCustomFields);
            return responseGetListCustomFields;
        }


        public ResponseGeneric<ResponseModelTask, ResponseError> SetTaskCustomField(ParamsEditTaskCustomField paramsEditTaskCustomField, RequestEditTaskCustomField requestData)
        {
			requestData.ValidateData();

			var client = new RestClient(_baseAddress);
			var createListRequest = new RestRequest($"task/{paramsEditTaskCustomField.TaskId}/field/{paramsEditTaskCustomField.FieldId}", Method.POST);
			createListRequest.AddHeader("authorization", AccessToken);
			createListRequest.AddJsonBody(requestData);

			// execute the request
			ResponseGeneric<ResponseModelTask, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseModelTask, ResponseError>(client, createListRequest);
			return result;
        }

        public ResponseGeneric<ResponseModelTask, ResponseError> SetTaskCustomField(string taskId, string fieldId, object value)
        {
            ParamsEditTaskCustomField paramsEditTaskCustomField = new ParamsEditTaskCustomField(taskId, fieldId);
            RequestEditTaskCustomField requestEditTaskCustomField = new RequestEditTaskCustomField(value);
            var responseSetTaskCustomField = SetTaskCustomField(paramsEditTaskCustomField, requestEditTaskCustomField);
            return responseSetTaskCustomField;
        }

        public ResponseGeneric<ResponseModelTask, ResponseError> SetTaskCustomFieldRelationship(ParamsEditTaskCustomField paramsEditTaskCustomField, RequestEditTaskCustomFieldRelationship requestData)
        {
			requestData.ValidateData();

			var client = new RestClient(_baseAddress);
			var createListRequest = new RestRequest(
                $"task/{paramsEditTaskCustomField.TaskId}/field/{paramsEditTaskCustomField.FieldId}", 
                Method.POST);
			createListRequest.AddHeader("authorization", AccessToken);
			createListRequest.AddJsonBody(requestData);

			// execute the request
			ResponseGeneric<ResponseModelTask, ResponseError> result = 
                RestSharperHelper.ExecuteRequest<ResponseModelTask, ResponseError>(client, createListRequest);
			return result;
        }

        public ResponseGeneric<ResponseModelTask, ResponseError> 
        SetTaskCustomFieldRelationship(string taskId, string fieldId, string value)
        {
            ParamsEditTaskCustomField paramsEditTaskCustomField = 
                new ParamsEditTaskCustomField(taskId, fieldId);
            RequestEditTaskCustomFieldRelationship requestEditTaskCustomFieldRelationship = 
                new RequestEditTaskCustomFieldRelationship(value);
            var responseSetTaskCustomField = SetTaskCustomFieldRelationship(paramsEditTaskCustomField, 
                requestEditTaskCustomFieldRelationship);
            return responseSetTaskCustomField;
        }


        public ResponseGeneric<ResponseModelTask, ResponseError> AddTaskToList(ParamsEditTask paramsEditTask, ParamsEditList paramsEditList)
        {
            paramsEditTask.ValidateData();
            paramsEditList.ValidateData();

            var client = new RestClient(_baseAddress);
            var createListRequest = new RestRequest($"list/{paramsEditList.ListId}/task/{paramsEditTask.TaskId}", Method.POST);
            createListRequest.AddHeader("authorization", AccessToken);

            // execute the request
			ResponseGeneric<ResponseModelTask, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseModelTask, ResponseError>(client, createListRequest);
            return result;
        }

        public ResponseGeneric<ResponseModelTask, ResponseError> AddTaskToList(string taskId, string listId)
        {
            ParamsEditTask paramsEditTask = new ParamsEditTask(taskId);
            ParamsEditList paramsEditList = new ParamsEditList(listId);
            var response = AddTaskToList(paramsEditTask, paramsEditList);
            return response;
        }


		/// <summary>
		/// Edit Task informations.
		/// </summary>
		/// <param name="paramsEditTask">param object of Edit Task request</param>
		/// <param name="requestData">RequestEditTask object</param>
		/// <returns>ResponseGeneric with ResponseSuccess response object</returns>
		public ResponseGeneric<ResponseModelTask, ResponseError> EditTask(ParamsEditTask paramsEditTask, RequestEditTask requestData)
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"task/{paramsEditTask.TaskId}", Method.PUT);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			ResponseGeneric<ResponseModelTask, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseModelTask, ResponseError>(client, request);
			return result;
		}

        #endregion CustomFields

        #region Spaces
        /// <summary>
        /// Get a team's spaces. This team must be one of the authorized teams for this token.
        /// </summary>
        /// <param name="paramsGetTeamSpace">param object of get team space request</param>
        /// <returns>ResponseGeneric with ResponseTeamSpace response object</returns>
        public ResponseGeneric<ResponseTeamSpaces, ResponseError> GetTeamSpaces(ParamsGetTeamSpaces paramsGetTeamSpace)
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"team/{paramsGetTeamSpace.TeamId}/space", Method.GET);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			ResponseGeneric<ResponseTeamSpaces, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseTeamSpaces, ResponseError>(client, request);
			return result;
		}

		/// <summary>
		/// Create space in a Team
		/// </summary>
		/// <param name="paramsCreateTeamSpace">param object of create space request</param>
		/// <param name="requestData">RequestCreateTeamSpace object</param>
		/// <returns>ResponseGeneric with ModelList response object</returns>
		public ResponseGeneric<ResponseModelSpace, ResponseError> CreateTeamSpace(ParamsCreateTeamSpace paramsCreateTeamSpace, RequestCreateTeamSpace requestData)
		{
			requestData.ValidateData();

			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"team/{paramsCreateTeamSpace.TeamId}/space", Method.POST);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			ResponseGeneric<ResponseModelSpace, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseModelSpace, ResponseError>(client, request);
			return result;
		}
		#endregion

		#region Folders
		/// <summary>
		/// Get a space's folders. The folders' lists will also be included.
		/// </summary>
		/// <param name="paramsGetSpaceFolders">param object of get space folder request</param>
		/// <returns>ResponseGeneric with ResponseSpaceFolders response object</returns>
		public ResponseGeneric<ResponseSpaceFolders, ResponseError> GetSpaceFolders(ParamsGetSpaceFolders paramsGetSpaceFolders)
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"space/{paramsGetSpaceFolders.SpaceId}/folder", Method.GET);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			ResponseGeneric<ResponseSpaceFolders, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseSpaceFolders, ResponseError>(client, request);
			return result;
		}

		/// <summary>
        /// Gets an object representing ResponseGenericSpaceFolder
		/// </summary>
		/// <param name="spaceId">param space id for folder request</param>
		/// <returns>List of ResponseObjectFolder</returns>
		public ResponseGeneric<ResponseSpaceFolders, ResponseError> GetSpaceFolders(string spaceId)
		{
            ParamsGetSpaceFolders paramsGSF = new ParamsGetSpaceFolders(spaceId);
            return GetSpaceFolders(paramsGSF);
		}


		/// <summary>
		/// Create a folder
		/// </summary>
		/// <param name="paramsCreateList">param object of create folder request</param>
		/// <param name="requestData">RequestCreateFolder object</param>
		/// <returns>ResponseGeneric with ModelFolder object expected</returns>
		public ResponseGeneric<ResponseModelFolder, ResponseError> CreateFolder(ParamsCreateFolder paramsCreateFolder, RequestCreateFolder requestData)
		{
			requestData.ValidateData();

			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"space/{paramsCreateFolder.SpaceId}/folder", Method.POST);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			ResponseGeneric<ResponseModelFolder, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseModelFolder, ResponseError>(client, request);
			return result;
		}
		#endregion

		#region Lists

		/// <summary>
		/// Get a list by id
		/// </summary>
		/// <param name="paramsGetListById">param object of get list by id request</param>
		/// <returns>ResponseGeneric with ResponseModelList response object</returns>
		public ResponseGeneric<ResponseModelList, ResponseError> GetListById(ParamsGetListById paramsGetListById)
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"list/{paramsGetListById.ListId}", Method.GET);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			ResponseGeneric<ResponseModelList, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseModelList, ResponseError>(client, request);
			return result;
		}

        public ResponseGeneric<ResponseModelList, ResponseError> GetListById(string listId)
        {
            ParamsGetListById paramsGetListById = new ParamsGetListById(listId);
            var response = GetListById(paramsGetListById);
            return response;
        }


		/// <summary>
		/// Create List in Folder
		/// </summary>
		/// <param name="paramsCreateList">param object of create list request</param>
		/// <param name="requestData">RequestCreateList object</param>
		/// <returns>ResponseGeneric with ModelList response object</returns>
		public ResponseGeneric<ResponseModelList, ResponseError> CreateList(ParamsCreateFolderList paramsCreateList, RequestCreateList requestData)
		{
			requestData.ValidateData();

			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"folder/{paramsCreateList.FolderId}/list", Method.POST);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			ResponseGeneric<ResponseModelList, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseModelList, ResponseError>(client, request);
			return result;
		}

		/// <summary>
		/// Get a space's lists AKA folderless lists
		/// </summary>
		/// <param name="paramsGetFolderlessLists">param object of get folderless lists request</param>
		/// <returns>ResponseGeneric with ResponseFolderlessLists response object</returns>
		public ResponseGeneric<ResponseFolderlessLists, ResponseError> GetFolderlessLists(ParamsGetFolderlessLists paramsGetFolderlessLists)
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"space/{paramsGetFolderlessLists.SpaceId}/list", Method.GET);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			ResponseGeneric<ResponseFolderlessLists, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseFolderlessLists, ResponseError>(client, request);
			return result;
		}

		public ResponseGeneric<ResponseFolderlessLists, ResponseError> GetFolderlessLists(string spaceId)
        {
            var paramsGetFolderlessLists = new ParamsGetFolderlessLists(spaceId);
            return GetFolderlessLists(paramsGetFolderlessLists);
        }

		/// <summary>
		/// Create folderless List
		/// </summary>
		/// <param name="paramsCreateList">param object of create list request</param>
		/// <param name="requestData">RequestCreateList object</param>
		/// <returns>ResponseGeneric with ModelList response object</returns>
		public ResponseGeneric<ResponseModelList, ResponseError> CreateFolderlessList(ParamsCreateFolderlessList paramsCreateList, RequestCreateList requestData)
		{
			requestData.ValidateData();

			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"space/{paramsCreateList.SpaceId}/list", Method.POST);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			ResponseGeneric<ResponseModelList, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseModelList, ResponseError>(client, request);
			return result;
		}

		/// <summary>
		/// Edit List informations
		/// </summary>
		/// <param name="paramsEditList">params object of edit list request</param>
		/// <param name="requestData">RequestEditList object</param>
		/// <returns>ResponseGeneric with ModelList response object</returns>
		public ResponseGeneric<ResponseModelList, ResponseError> EditList(ParamsEditList paramsEditList, RequestEditList requestData)
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"list/{paramsEditList.ListId}", Method.PUT);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			ResponseGeneric<ResponseModelList, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseModelList, ResponseError>(client, request);
			return result;
		}
		#endregion

		#region Tasks
		/// <summary>
		/// Get a task by id
		/// </summary>
		/// <param name="paramsGetTaskById">param object of get task by id request</param>
		/// <returns>ResponseGeneric with ResponseModelTask response object</returns>
		public ResponseGeneric<ResponseModelTask, ResponseError> GetTaskById(ParamsGetTaskById paramsGetTaskById)
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"task/{paramsGetTaskById.TaskId}", Method.GET);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			ResponseGeneric<ResponseModelTask, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseModelTask, ResponseError>(client, request);
			return result;
		}

        public ResponseGeneric<ResponseModelTask, ResponseError> GetTaskById(string taskId)
        {
            ParamsGetTaskById paramsGetTaskById = new ParamsGetTaskById(taskId);
            return GetTaskById(paramsGetTaskById);
        }

		/// <summary>
		/// Get a tasks by list id
		/// </summary>
		/// <param name="paramsGetTasksByListId">param object of get task by id request</param>
		/// <returns>ResponseGeneric with ResponseModelTasks response object</returns>
		public ResponseGeneric<ResponseTasks, ResponseError> GetTasksByListId(ParamsGetTasksByListId paramsGetTasksByListId)
		{

            var testResource = $"list/{paramsGetTasksByListId.ListId}/task";
            var testRequest = new RestRequest(testResource, Method.GET);

            var requestResource = $"list/{paramsGetTasksByListId.ListId}/task";
            var addedParams = false;
            if (paramsGetTasksByListId.Archived != null)
            {
                if (!addedParams)
                {
                    requestResource += "?";
                    addedParams = true;
                }
                requestResource += $"archived={paramsGetTasksByListId.Archived}";
                testRequest.AddQueryParameter("archived", paramsGetTasksByListId.Archived.ToString());
            }
            if (paramsGetTasksByListId.Page != null && paramsGetTasksByListId.Page > 0)
            {
                if (!addedParams)
                {
                    requestResource += "?";
                    addedParams = true;
                }
                else
                {
                    requestResource += "&";
                }
                requestResource += $"page={paramsGetTasksByListId.Page}";
                testRequest.AddQueryParameter("page", paramsGetTasksByListId.Page.ToString());
            }
            if (paramsGetTasksByListId.IncludeClosed == true)
            {
                if (!addedParams)
                {
                    requestResource += "?";
                    addedParams = true;
                }
                else
                {
                    requestResource += "&";
                }
                requestResource += $"include_closed={paramsGetTasksByListId.IncludeClosed}";
                testRequest.AddQueryParameter("include_closed", paramsGetTasksByListId.IncludeClosed.ToString());
            }

			var client = new RestClient(_baseAddress);
			var request = new RestRequest(requestResource, Method.GET);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			ResponseGeneric<ResponseTasks, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseTasks, ResponseError>(client, request);
			return result;
		}

		/// <summary>
		/// Get a tasks by list id
		/// </summary>
		/// <param name="paramsGetTasksByListId">param object of get task by id request</param>
		/// <returns>ResponseGeneric with ResponseModelTasks response object</returns>
		public ResponseGeneric<ResponseTasks, ResponseError> GetTasksByListId(string listId, bool includeClosed = true, bool includeArchived = false, int page = 0)
		{
            var ParamsGetTasksByListId = new ParamsGetTasksByListId(listId)
            {
                IncludeClosed = includeClosed,
                Archived = includeArchived,
                Page = page,
            };
            return GetTasksByListId(ParamsGetTasksByListId);
		}

		/// <summary>
		/// Get Tasks of the Team and filter its by optionalParams
		/// </summary>
		/// <param name="paramsGetTasks">params obkect of get tasks request</param>
		/// <param name="optionalParams">OptionalParamsGetTask object</param>
		/// <returns>ResponseGeneric with ResponseTasks response object</returns>
		public ResponseGeneric<ResponseTasks, ResponseError> GetTasks(ParamsGetTasks paramsGetTasks)
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"team/{paramsGetTasks.TeamId}/task", Method.GET);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			ResponseGeneric<ResponseTasks, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseTasks, ResponseError>(client, request);
			return result;
		}

		/// <summary>
		/// Create Task in List.
		/// </summary>
		/// <param name="paramCreateTaskInList">params object of create task in list request</param>
		/// <param name="requestData">RequestCreateTaskInList object</param>
		/// <returns>ResponseGeneric with ModelTask object Expected</returns>
		public ResponseGeneric<ResponseModelTask, ResponseError> CreateTaskInList(ParamsCreateTaskInList paramsCreateTaskInList, RequestCreateTaskInList requestData)
		{
			requestData.ValidateData();

			var client = new RestClient(_baseAddress);
			var createListRequest = new RestRequest($"list/{paramsCreateTaskInList.ListId}/task", Method.POST);
			createListRequest.AddHeader("authorization", AccessToken);
			createListRequest.AddJsonBody(requestData);

			// execute the request
			ResponseGeneric<ResponseModelTask, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseModelTask, ResponseError>(client, createListRequest);
			return result;
		}

		public ResponseGeneric<ResponseModelTask, ResponseError> CreateTaskInList(string listId, string taskName)
        {
            ParamsCreateTaskInList paramsCreateTaskInList = new ParamsCreateTaskInList(listId);
            RequestCreateTaskInList requestCreateTaskInList = new RequestCreateTaskInList(taskName);
            return CreateTaskInList(paramsCreateTaskInList, requestCreateTaskInList);
        }

        public ResponseGeneric<ResponseModelTask, ResponseError> EditTaskStatus(string taskId, string taskStatus)
        {
            ParamsEditTask paramsEditTask = new ParamsEditTask(taskId);
            RequestEditTask requestEditTask = new RequestEditTask()
            {
                Status = taskStatus
            };
            var response = EditTask(paramsEditTask, requestEditTask);
            return response;
        }

		#endregion

		#region Webhooks
		/// <summary>
		/// Get a team's webhooks. This team must be one of the authorized teams for this token.
		/// </summary>
		/// <param name="paramsGetTeamWebhook">param object of get team Webhook request</param>
		/// <returns>ResponseGeneric with ResponseTeamWebhook response object</returns>
		public ResponseGeneric<ResponseWebhooks, ResponseError> GetTeamWebhooks(ParamsGetTeamWebhooks paramsGetTeamWebhook)
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"team/{paramsGetTeamWebhook.TeamId}/webhook", Method.GET);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			ResponseGeneric<ResponseWebhooks, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseWebhooks, ResponseError>(client, request);
			return result;
		}

		/// <summary>
		/// Create a webhook in a Team
		/// </summary>
		/// <param name="paramsCreateTeamWebhook">param object of create webhook request</param>
		/// <param name="requestData">RequestCreateTeamWebhook object</param>
		/// <returns>ResponseGeneric with ResponseWebhook response object</returns>
		public ResponseGeneric<ResponseWebhook, ResponseError> CreateTeamWebhook(ParamsCreateTeamWebhook paramsCreateTeamWebhook, RequestCreateTeamWebhook requestData)
		{
			requestData.ValidateData();

			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"team/{paramsCreateTeamWebhook.TeamId}/webhook", Method.POST);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			ResponseGeneric<ResponseWebhook, ResponseError> result = RestSharperHelper.ExecuteRequest<ResponseWebhook, ResponseError>(client, request);
			return result;
		}
		#endregion

		#endregion

		#region API Methods Async

		#region User
		/// <summary>
		/// Get the user that belongs to this token
		/// </summary>
		/// <returns>ResponseGeneric with ResponseAuthorizedUser object expected</returns>
		public Task<ResponseGeneric<ResponseAuthorizedUser, ResponseError>> GetAuthorizedUserAsync()
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"user", Method.GET);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseAuthorizedUser, ResponseError>(client, request);
		}
		#endregion

		#region Teams
		/// <summary>
		/// Get the authorized teams for this token
		/// </summary>
		/// <returns>ResponseGeneric with ResponseAuthorizedTeams expected</returns>
		public Task<ResponseGeneric<ResponseAuthorizedTeams, ResponseError>> GetAuthorizedTeamsAsync()
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"team", Method.GET);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseAuthorizedTeams, ResponseError>(client, request);
		}

		/// <summary>
		/// Get a team's details. This team must be one of the authorized teams for this token.
		/// </summary>
		/// <param name="paramGetTeamByID">param object of get team by ID request</param>
		/// <returns>ResponseGeneric with ResponseTeam response object</returns>
		public Task<ResponseGeneric<ResponseTeam, ResponseError>> GetTeamByIdAsync(ParamsGetTeamById paramsGetTeamById)
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"team/{paramsGetTeamById.TeamId}", Method.GET);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseTeam, ResponseError>(client, request);
		}
		#endregion

		#region Spaces
		/// <summary>
		/// Get a team's spaces. This team must be one of the authorized teams for this token.
		/// </summary>
		/// <param name="paramGetTeamSpace">param object of get team space request</param>
		/// <returns>ResponseGeneric with ResponseTeamSpace object expected</returns>
		public Task<ResponseGeneric<ResponseTeamSpaces, ResponseError>> GetTeamSpacesAsync(ParamsGetTeamSpaces paramsGetTeamSpace)
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"team/{paramsGetTeamSpace.TeamId}/space", Method.GET);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseTeamSpaces, ResponseError>(client, request);
		}

		/// <summary>
		/// Create space in a Team
		/// </summary>
		/// <param name="paramsCreateTeamSpace">param object of create space request</param>
		/// <param name="requestData">RequestCreateTeamSpace object</param>
		/// <returns>ResponseGeneric with ModelList response object</returns>
		public Task<ResponseGeneric<ResponseModelSpace, ResponseError>> CreateTeamSpaceAsync(ParamsCreateTeamSpace paramsCreateTeamSpace, RequestCreateTeamSpace requestData)
		{
			requestData.ValidateData();

			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"team/{paramsCreateTeamSpace.TeamId}/space", Method.POST);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseModelSpace, ResponseError>(client, request);
		}
		#endregion

		#region Folders
		/// <summary>
		/// Get a space's folders. The folders' lists will also be included.
		/// </summary>
		/// <param name="paramsGetSpaceFolders">params object of get space folders request</param>
		/// <returns>ResponseGeneric with ResponseSpaceFolders object expected</returns>
		public Task<ResponseGeneric<ResponseSpaceFolders, ResponseError>> GetSpaceFoldersAsync(ParamsGetSpaceFolders paramsGetSpaceFolders)
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"space/{paramsGetSpaceFolders.SpaceId}/folder", Method.GET);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseSpaceFolders, ResponseError>(client, request);
		}

		/// <summary>
		/// Create a folder
		/// </summary>
		/// <param name="paramsCreateList">param object of create folder request</param>
		/// <param name="requestData">RequestCreateFolder object</param>
		/// <returns>ResponseGeneric with ModelFolder object expected</returns>
		public Task<ResponseGeneric<ResponseModelFolder, ResponseError>> CreateFolderAsync(ParamsCreateFolder paramsCreateFolder, RequestCreateFolder requestData)
		{
			requestData.ValidateData();

			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"space/{paramsCreateFolder.SpaceId}/folder", Method.POST);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseModelFolder, ResponseError>(client, request);
		}
		#endregion

		#region Lists
		/// <summary>
		/// Get a list by id
		/// </summary>
		/// <param name="paramsGetListById">param object of get list by id request</param>
		/// <returns>ResponseGeneric with ResponseModelList response object</returns>
		public Task<ResponseGeneric<ResponseModelList, ResponseError>> GetListByIdAsync(ParamsGetListById paramsGetListById)
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"list/{paramsGetListById.ListId}", Method.GET);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseModelList, ResponseError>(client, request);
		}

		/// <summary>
		/// Create List in Folder
		/// </summary>
		/// <param name="paramsCreateList">param object of create list request</param>
		/// <param name="requestData">RequestCreateList object</param>
		/// <returns>ResponseGeneric with ModelList object expected</returns>
		public Task<ResponseGeneric<ResponseModelList, ResponseError>> CreateListAsync(ParamsCreateFolderList paramsCreateList, RequestCreateList requestData)
		{
			requestData.ValidateData();

			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"folder/{paramsCreateList.FolderId}/list", Method.POST);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseModelList, ResponseError>(client, request);
		}

		/// <summary>
		/// Get a space's lists AKA folderless lists
		/// </summary>
		/// <param name="paramsGetFolderlessLists">param object of get folderless lists request</param>
		/// <returns>ResponseGeneric with ResponseFolderlessLists response object</returns>
		public Task<ResponseGeneric<ResponseFolderlessLists, ResponseError>> GetFolderlessListsAsync(ParamsGetFolderlessLists paramsGetFolderlessLists)
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"space/{paramsGetFolderlessLists.SpaceId}/list", Method.GET);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseFolderlessLists, ResponseError>(client, request);
		}

		/// <summary>
		/// Create a folderless List
		/// </summary>
		/// <param name="paramsCreateList">param object of create list request</param>
		/// <param name="requestData">RequestCreateList object</param>
		/// <returns>ResponseGeneric with ModelList object expected</returns>
		public Task<ResponseGeneric<ResponseModelList, ResponseError>> CreateFolderlessListAsync(ParamsCreateFolderlessList paramsCreateList, RequestCreateList requestData)
		{
			requestData.ValidateData();

			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"space/{paramsCreateList.SpaceId}/list", Method.POST);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseModelList, ResponseError>(client, request);
		}

		/// <summary>
		/// Edit List informations
		/// </summary>
		/// <param name="paramsEditList">param object of Edi List request</param>
		/// <param name="requestData">RequestEditList object</param>
		/// <returns>ResponseGeneric with ModelList object expected</returns>
		public Task<ResponseGeneric<ResponseModelList, ResponseError>> EditListAsync(ParamsEditList paramsEditList, RequestEditList requestData)
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"list/{paramsEditList.ListId}", Method.PUT);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseModelList, ResponseError>(client, request);
		}

		#endregion

		#region Tasks
		/// <summary>
		/// Get a task by id
		/// </summary>
		/// <param name="paramsGetTaskById">param object of get task by id request</param>
		/// <returns>ResponseGeneric with ResponseModelTask response object</returns>
		public Task<ResponseGeneric<ResponseModelTask, ResponseError>> GetTaskByIdAsync(ParamsGetTaskById paramsGetTaskById)
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"task/{paramsGetTaskById.TaskId}", Method.GET);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseModelTask, ResponseError>(client, request);
		}

		/// <summary>
		/// Get Tasks of the Team and filter its by optionalParams
		/// </summary>
		/// <param name="paramsGetTasks">param object of get tasks request</param>
		/// <param name="optionalParams">OptionalParamsGetTask object</param>
		/// <returns>ResponseGeneric with ResponseTasks object expected</returns>
		public Task<ResponseGeneric<ResponseTasks, ResponseError>> GetTasksAsync(ParamsGetTasks paramsGetTasks)
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"team/{paramsGetTasks.TeamId}/task", Method.GET);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseTasks, ResponseError>(client, request);
		}

		/// <summary>
		/// Create Task in List.
		/// </summary>
		/// <param name="paramsCreateTaskInList">param object of Create Task in List request</param>
		/// <param name="requestData">RequestCreateTaskInList object</param>
		/// <returns>ResponseGeneric with ModelTask object Expected</returns>
		public Task<ResponseGeneric<ResponseModelTask, ResponseError>> CreateTaskInListAsync(ParamsCreateTaskInList paramsCreateTaskInList, RequestCreateTaskInList requestData)
		{
			requestData.ValidateData();

			var client = new RestClient(_baseAddress);
			var createListRequest = new RestRequest($"list/{paramsCreateTaskInList.ListId}/task", Method.POST);
			createListRequest.AddHeader("authorization", AccessToken);
			createListRequest.AddJsonBody(requestData);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseModelTask, ResponseError>(client, createListRequest);
		}

		/// <summary>
		/// Edit Task informations.
		/// </summary>
		/// <param name="paramsEditTask">param object of edit task request</param>
		/// <param name="requestData">RequestEditTask object</param>
		/// <returns>ResponseGeneric with ResponseSuccess object expected</returns>
		public Task<ResponseGeneric<ResponseModelTask, ResponseError>> EditTaskAsync(ParamsEditTask paramsEditTask, RequestEditTask requestData)
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"task/{paramsEditTask.TaskId}", Method.PUT);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseModelTask, ResponseError>(client, request);
		}
		#endregion

		#region Webhooks
		/// <summary>
		/// Get a team's webhooks. This team must be one of the authorized teams for this token.
		/// </summary>
		/// <param name="paramsGetTeamWebhook">param object of get team Webhook request</param>
		/// <returns>ResponseGeneric with ResponseTeamWebhook response object</returns>
		public Task<ResponseGeneric<ResponseWebhooks, ResponseError>> GetTeamWebhooksAsync(ParamsGetTeamWebhooks paramsGetTeamWebhook)
		{
			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"team/{paramsGetTeamWebhook.TeamId}/webhook", Method.GET);
			request.AddHeader("authorization", AccessToken);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseWebhooks, ResponseError>(client, request);
		}

		/// <summary>
		/// Create a webhook in a Team
		/// </summary>
		/// <param name="paramsCreateTeamWebhook">param object of create webhook request</param>
		/// <param name="requestData">RequestCreateTeamWebhook object</param>
		/// <returns>ResponseGeneric with ResponseWebhook response object</returns>
		public Task<ResponseGeneric<ResponseWebhook, ResponseError>> CreateTeamWebhookAsync(ParamsCreateTeamWebhook paramsCreateTeamWebhook, RequestCreateTeamWebhook requestData)
		{
			requestData.ValidateData();

			var client = new RestClient(_baseAddress);
			var request = new RestRequest($"team/{paramsCreateTeamWebhook.TeamId}/webhook", Method.POST);
			request.AddHeader("authorization", AccessToken);
			request.AddJsonBody(requestData);

			// execute the request
			return RestSharperHelper.ExecuteRequestAsync<ResponseWebhook, ResponseError>(client, request);
		}
		#endregion

		#endregion
	}
}
