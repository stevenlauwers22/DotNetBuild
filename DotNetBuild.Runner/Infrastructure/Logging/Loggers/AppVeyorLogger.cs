using System;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace DotNetBuild.Runner.Infrastructure.Logging.Loggers
{
    // http://www.appveyor.com/docs/build-worker-api
    public class AppVeyorLogger
        : ILogger
    {
        public void Write(String message)
        {
            Console.WriteLine(message);

            SendBuildMessage(new BuildMessage { Message = message, Category = BuildMessage.BuildMessageCategory.Information });
        }

        public void WriteBlockStart(String message)
        {
            Console.WriteLine("START: " + message);
            Console.WriteLine("------------------");

            SendBuildMessage(new BuildMessage { Message = "START: " + message, Category = BuildMessage.BuildMessageCategory.Information });
        }

        public void WriteBlockEnd(String message)
        {
            Console.WriteLine("END: " + message);
            Console.WriteLine("------------------");

            SendBuildMessage(new BuildMessage { Message = "END: " + message, Category = BuildMessage.BuildMessageCategory.Information });
        }

        public void WriteError(String message, Exception exception)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(String.Empty);
            Console.Error.WriteLine(exception.Message);
            Console.Error.WriteLine(exception.StackTrace);
            Console.Error.WriteLine("------------------");
            Console.ResetColor();

            SendBuildMessage(new BuildMessage { Message = message, Category = BuildMessage.BuildMessageCategory.Error, Details = exception.StackTrace });
        }

        private static void SendBuildMessage(BuildMessage message)
        {
            try
            {
                using (var webClient = GetClient())
                {
                    webClient.UploadData("api/build/messages", "POST", SerializeToJson(message));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(message.Message);
                Console.Error.WriteLine("Error communicating AppVeyor Build Worker API: " + ex.Message);
            }
        }

        private static WebClient GetClient()
        {
            var webClient = new WebClient
            {
                BaseAddress = GetApiUrl()
            };

            webClient.Headers["Accept"] = "application/json";
            webClient.Headers["Content-type"] = "application/json";

            return webClient;
        }

        private static string GetApiUrl()
        {
            var apiUrl = Environment.GetEnvironmentVariable("APPVEYOR_API_URL");
            if (apiUrl != null)
                apiUrl = apiUrl.TrimEnd('/') + "/";

            return apiUrl;
        }

        private static byte[] SerializeToJson(object data)
        {
            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(data);
            return Encoding.UTF8.GetBytes(json);
        }

        public class BuildMessage
        {
            public String Message { get; set; }
            public BuildMessageCategory Category { get; set; }
            public String Details { get; set; }

            public enum BuildMessageCategory
            {
                Information,
                Warning,
                Error
            }
        }
    }
}