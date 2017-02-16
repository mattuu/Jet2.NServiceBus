using NServiceBus;

namespace ServiceBus.Contract
{
    public class SendMessage : ICommand
    {
        public string Message { get; set; }
    }
}