using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;

namespace Markekraus.Mekspaaf.Util
{
    public static class Utilities
    {
        public const string DebugVariableName = "DEBUG_ENABLED";
        public static void DebugLog (ILogger log, string message){
            if(IsDebugMode())
            {
                log.LogInformation(message);
            }
        }

        public static bool IsDebugMode ()
        {
            bool debug;
            if(!bool.TryParse(Environment.GetEnvironmentVariable(DebugVariableName), out debug))
            {
                debug = false;
            }
            return debug;
        }

        /// <summary>
        /// Returns the full folder path of the script root for the project at run time
        /// </summary>
        public static string GetScriptPath()
        {
            return Path.Combine(Environment.GetEnvironmentVariable("HOME"),  @"site\wwwroot");
        }

        /// <summary>
        /// Determins if a child path is a hosted under a parent path.
        /// </summary>
        /// <param name="parentPath">
        /// The parent path.
        /// </param>
        /// <param name="childPath">
        /// The child path.
        /// </param>
        public static bool IsInDirectory(string parentPath, string childPath)
        {
            var parent = new DirectoryInfo(parentPath);
            var child = new DirectoryInfo(childPath);

            var dir = child;
            do
            {
                if (dir.FullName == parent.FullName)
                {
                    return true;
                }
                dir = dir.Parent;
            } while (dir != null);

            return false;
        }

        /// <summary>
        /// Retruns default file to use for the / path.
        /// This will either be the value of the DEFAULT_PAGE app setting or index.html if not present.
        /// </summary>
        public static string GetDefaultPage()
        {
            return string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DEFAULT_PAGE")) ? 
            "index.html" : Environment.GetEnvironmentVariable("DEFAULT_PAGE");
        }

         /// <summary>
        /// Retruns the folder to use for static files
        /// This will either be the value of the STATIC_FILES_PATH app setting or www if not present.
        /// </summary>
        public static string GetStaticFilesPath()
        {
            return string.IsNullOrEmpty(Environment.GetEnvironmentVariable("STATIC_FILES_PATH")) ? 
            "www" : Environment.GetEnvironmentVariable("STATIC_FILES_PATH");
        }

        /// <summary>
        /// Retruns the full file path for the file provided by file query parameter in the HTTP request
        /// </summary>
        /// <param name="req">
        /// The HttpRequest provided as input from the function.
        /// </param>
        /// <param name="log">
        /// The ILogger object used for logging function activity.
        /// </param>
        /// <param name="caller">
        /// String representing the caller. Used tor logging and tracing.
        /// </param>
        public static string GetFilePath(HttpRequest req, ILogger log, [System.Runtime.CompilerServices.CallerMemberName] string caller = "unknown")
        {
            var source = $"{caller}.{nameof(GetFilePath)}";
            log.LogInformation($"{source} begin");
            var pathValue = req.Query["file"].FirstOrDefault();
            log.LogInformation($"{source} pathValue {pathValue}");

            var path = pathValue ?? "";

            var staticFilesPath = 
                Path.GetFullPath(Path.Combine(GetScriptPath(), GetStaticFilesPath()));
            var fullPath = Path.GetFullPath(Path.Combine(staticFilesPath, path));

            if (!IsInDirectory(staticFilesPath, fullPath))
            {
                log.LogInformation($"{source} pathValue {pathValue}");
                throw new ArgumentException("Invalid path");
            }

            if (!File.Exists(fullPath))
            {
                log.LogInformation($"{source} {fullPath} does not exist as a file or directory");
                fullPath = Path.Combine(staticFilesPath, GetDefaultPage());
            }

            return fullPath;
        }

        public static string GetMimeType(string filePath)
        {
            var fileName = new FileInfo(filePath).Name;
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if(!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}