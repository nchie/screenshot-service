
namespace Screenshot.Worker.MQ
{
    class MqConfiguration {
        public string Host { get; set; } = "localhost";
        public string ScreenshotRequestEndpoint { get; set; } = "submit-screenshot-request";
        public string ScreenshotSavedEndpoint { get; set; } = "screenshot-saved";

    }
}