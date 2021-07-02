namespace OrderAPI.Settings
{
    public class ServiceBusSettings
    {
        public string ConnectionString { get; set; }
        public string CheckoutMessageTopic { get; set; }
        public string SubscriptionCheckOut { get; set; }
        public string OrderPaymentProcessTopics { get; set; }
        public string OrderUpdatePaymentResultTopic { get; set; }
    }
}
