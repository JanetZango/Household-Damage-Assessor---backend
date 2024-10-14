namespace ACM.ViewModels.Services
{
    public class ClickatellServiceRequest
    {
        [JsonProperty("messages")]
        public List<ClickatellServiceRequestMessage> Messages { get; set; }
    }

    public class ClickatellServiceRequestMessage
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }
        [JsonProperty("to")]
        public string To { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
