cmake_minimum_required(VERSION 3.15)

project(ClickUpAPI CSharp)
set(CMAKE_DOTNET_TARGET_FRAMEWORK_VERSION v4.7.2)

# Remove "/langversion:3" and "/nowin32manifest" from the default flags
string(REPLACE " " ";" CSharp_FLAGS_LIST ${CMAKE_CSharp_FLAGS})
list(REMOVE_ITEM CSharp_FLAGS_LIST "/langversion:3")
list(REMOVE_ITEM CSharp_FLAGS_LIST "/nowin32manifest")
string(REPLACE ";" " " CMAKE_CSharp_FLAGS "${CSharp_FLAGS_LIST}")
set(CMAKE_CSharp_FLAGS "${CMAKE_CSharp_FLAGS}" CACHE STRING "" FORCE)

# Change default warning level for debug builds
string(REPLACE " " ";" CSharp_DEBUG_FLAGS_LIST ${CMAKE_CSharp_FLAGS_DEBUG})
list(FIND CSharp_DEBUG_FLAGS_LIST "/warn:3" _index)
if (_index GREATER_EQUAL 0)
    list(REMOVE_AT CSharp_DEBUG_FLAGS_LIST ${_index})
    list(INSERT CSharp_DEBUG_FLAGS_LIST ${_index} "/warn:4")
endif()
string(REPLACE ";" " " CMAKE_CSharp_FLAGS_DEBUG "${CSharp_DEBUG_FLAGS_LIST}")
set(CMAKE_CSharp_FLAGS_DEBUG "${CMAKE_CSharp_FLAGS_DEBUG}"
    CACHE STRING ""
    FORCE
)

add_subdirectory(Chinchilla.ClickUp)
#add_subdirectory(Chinchilla.ClickUp.Tests)

#set_directory_properties(PROPERTIES VS_STARTUP_PROJECT ClickUpAPI)
# Package
#set(__package_name PartLogger)
#set(__package_suffix prealpha)
#set(__package_file_name "${__package_name}-${__package_suffix}")
#add_custom_target(PartLogger_PACKAGE_RHI
#    COMMAND
#    "${CMAKE_COMMAND}" --build "${CMAKE_BINARY_DIR}" --target PartLogger
#        --config Release
#    COMMAND
#    "${CMAKE_COMMAND}" -E make_directory
#    "${PartLogger_PACKAGE_DIR}/temp/${__package_name}/Rhino\ 6"
#    COMMAND
#    "${CMAKE_COMMAND}" -E copy
#	"${CMAKE_CURRENT_BINARY_DIR}/bin/PartLogger.rhp"
#	"${PartLogger_PACKAGE_DIR}/temp/${__package_name}/Rhino\ 6"
#    COMMAND
#    powershell.exe -nologo -noprofile -command
#        "& {Compress-Archive -Force \
#            -Path 'temp\\${__package_name}' \
#            -DestinationPath '${__package_file_name}'}"
#    COMMAND
#    ${CMAKE_COMMAND} -E rename
#    "${__package_file_name}.zip"
#    "${__package_file_name}.rhi"
#    WORKING_DIRECTORY
#    "${PartLogger_PACKAGE_DIR}"
#)
