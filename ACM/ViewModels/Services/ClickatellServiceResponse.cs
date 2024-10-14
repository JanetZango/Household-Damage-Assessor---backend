namespace ACM.ViewModels.Services
{
    public class ClickatellServiceResponse
    {
        [JsonProperty("messages")]
        public List<ClickatellServiceResponseMessage> Messages { get; set; }
        [JsonProperty("error")]
        public ClickatellServiceResponseMessageError Error { get; set; }
    }

    public class ClickatellServiceResponseMessage
    {
        [JsonProperty("apiMessageId")]
        public string ApiMessageID { get; set; }
        [JsonProperty("accepted")]
        public bool Accepted { get; set; }
        [JsonProperty("to")]
        public string To { get; set; }
        [JsonProperty("error")]
        public ClickatellServiceResponseMessageError Error { get; set; }
    }

    public class ClickatellServiceResponseMessageError
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
