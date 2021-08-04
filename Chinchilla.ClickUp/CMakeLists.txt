cmake_minimum_required(VERSION 3.15)

set(ClickUpAPI_SOURCES
	ClickUpApi.cs
	Enums/TaskOrderBy.cs
	Enums/TaskPriority.cs
	Enums/UserRole.cs
	Helpers/IResponse.cs
	Helpers/JsonConverterDateTimeMilliseconds.cs
	Helpers/JsonConverterTimeSpanMilliseconds.cs
	Helpers/ResponseGeneric.cs
	Helpers/RestSharperHelper.cs
	Params/ParamsAccessToken.cs
	Params/ParamsCreateFolder.cs
	Params/ParamsCreateFolderlessList.cs
	Params/ParamsCreateFolderList.cs
	Params/ParamsCreateTaskInList.cs
	Params/ParamsCreateTeamSpace.cs
	Params/ParamsCreateTeamWebhook.cs
	Params/ParamsEditList.cs
	Params/ParamsEditTask.cs
	Params/ParamsGetFolderlessLists.cs
	Params/ParamsGetListById.cs
	Params/ParamsGetSpaceFolders.cs
	Params/ParamsGetTaskById.cs
	Params/ParamsGetTasks.cs
	Params/ParamsGetTasksByListId.cs
	Params/ParamsGetTeamById.cs
	Params/ParamsGetTeamSpaces.cs
	Params/ParamsGetTeamWebhooks.cs
	Requests/RequestCreateFolder.cs
	Requests/RequestCreateList.cs
	Requests/RequestCreateTaskInList.cs
	Requests/RequestCreateTeamSpace.cs
	Requests/RequestCreateTeamWebhook.cs
	Requests/RequestEditList.cs
	Requests/RequestEditTask.cs
	Requests/RequestModel/RequestModelSupportAssignees.cs
	Responses/ResponseAccessToken.cs
	Responses/ResponseAuthorizedTeams.cs
	Responses/ResponseAuthorizedUser.cs
	Responses/ResponseError.cs
	Responses/ResponseFolderlessLists.cs
	Responses/ResponseSpaceFolders.cs
	Responses/ResponseSuccess.cs
	Responses/ResponseTasks.cs
	Responses/ResponseTeam.cs
	Responses/ResponseTeamSpaces.cs
	Responses/ResponseWebhook.cs
	Responses/ResponseWebhooks.cs
	Responses/ResponseModel/ResponseModelCustomFields.cs
	Responses/ResponseModel/ResponseModelDependencyWarning.cs
	Responses/ResponseModel/ResponseModelDueDates.cs
	Responses/ResponseModel/ResponseModelFeatures.cs
	Responses/ResponseModel/ResponseModelFolder.cs
	Responses/ResponseModel/ResponseModelList.cs
	Responses/ResponseModel/ResponseModelMultipleAssignees.cs
	Responses/ResponseModel/ResponseModelPriority.cs
	Responses/ResponseModel/ResponseModelPriorities.cs
	Responses/ResponseModel/ResponseModelSpace.cs
	Responses/ResponseModel/ResponseModelStatus.cs
	Responses/ResponseModel/ResponseModelTag.cs
	Responses/ResponseModel/ResponseModelTask.cs
	Responses/ResponseModel/ResponseModelTeam.cs
	Responses/ResponseModel/ResponseModelTimeEstimates.cs
	Responses/ResponseModel/ResponseModelTimeTracking.cs
	Responses/ResponseModel/ResponseModelUser.cs
	Responses/ResponseModel/ResponseModelWebhook.cs
	Responses/ResponseModel/ResponseModelWebhookHealth.cs
)


add_library(ClickUpAPI SHARED ${ClickUpAPI_SOURCES})

# RestSharp Package
set_property(TARGET ClickUpAPI PROPERTY
	VS_PACKAGE_REFERENCES "Newtonsoft.Json_12.0.3;RestSharp_106.10.1;RestSharp.Serializers.NewtonsoftJson_106.10.1"
)

list(APPEND ClickUpAPI_DOTNET_REFERENCES
    System
    System.Core
    System.Data
    System.Data.DataSetExtensions
    System.Drawing
    System.Net.Http
    System.Xml
    System.Xml.Linq
    System.Windows.Forms
    System.Runtime.Serialization
    #Eto
    #RhinoCommon
    #Rhino.UI
)

set_target_properties(ClickUpAPI PROPERTIES
	VS_GLOBAL_RootNamespace PartLogger
    VS_DOTNET_REFERENCES_COPY_LOCAL OFF
    VS_DOTNET_REFERENCES "${ClickUpAPI_DOTNET_REFERENCES}"
    #VS_DOTNET_REFERENCE_RhinoCommon "${Rhino6_EXE_DIR}/RhinoCommon.dll"
    #VS_DOTNET_REFERENCE_Rhino.UI "${Rhino6_EXE_DIR}/Rhino.UI.dll"
    #VS_DOTNET_REFERENCE_Eto "${Rhino6_EXE_DIR}/Eto.dll"
    #VS_GLOBAL_StartAction Program
    #VS_GLOBAL_StartProgram "${Rhino6_EXE}"
    #VS_GLOBAL_StartArguments "/nosplash /runscript=\"PartLoggerCommand _enter\""
)

#add_dependencies(PartLogger
#    ShelfDotNetCSharp
#)
#target_link_libraries(PartLogger PUBLIC
#    ShelfDotNetCSharp
#)
#
#add_custom_command(TARGET PartLogger
#    POST_BUILD
#    COMMAND
#    ${CMAKE_COMMAND} -E copy
#    $<SHELL_PATH:$<TARGET_FILE:PartLogger>>
#    $<SHELL_PATH:$<TARGET_FILE_DIR:PartLogger>/PartLogger.rhp>
#    COMMAND
#    ${CMAKE_COMMAND} -E remove_directory
#    "${CMAKE_CURRENT_BINARY_DIR}/../../bin"
#    COMMAND
#    ${CMAKE_COMMAND} -E copy_directory
#    $<SHELL_PATH:$<TARGET_FILE_DIR:PartLogger>>
#    "${CMAKE_CURRENT_BINARY_DIR}/../../bin"
#)

