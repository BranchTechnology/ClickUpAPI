set(ClickUpAPITests_SOURCES
	ClickUpApiTests.cs
)

#list(APPEND PartLogger_DOTNET_REFERENCES
#    System
#    System.Core
#    System.Data
#    System.Data.DataSetExtensions
#    System.Drawing
#    System.Net.Http
#    System.Xml
#    System.Xml.Linq
#    System.Windows.Forms
#    Eto
#    RhinoCommon
#    Rhino.UI
#)

add_executable(ClickUpAPITests ${ClickUpAPITests_SOURCES})
#set_target_properties(PartLogger PROPERTIES
#	VS_GLOBAL_RootNamespace PartLogger
#    VS_DOTNET_REFERENCES_COPY_LOCAL OFF
#    VS_DOTNET_REFERENCES "${PartLogger_DOTNET_REFERENCES}"
#    VS_DOTNET_REFERENCE_RhinoCommon "${Rhino6_EXE_DIR}/RhinoCommon.dll"
#    VS_DOTNET_REFERENCE_Rhino.UI "${Rhino6_EXE_DIR}/Rhino.UI.dll"
#    VS_DOTNET_REFERENCE_Eto "${Rhino6_EXE_DIR}/Eto.dll"
#    VS_GLOBAL_StartAction Program
#    VS_GLOBAL_StartProgram "${Rhino6_EXE}"
#    VS_GLOBAL_StartArguments "/nosplash /runscript=\"PartLoggerCommand _enter\""
#)

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

