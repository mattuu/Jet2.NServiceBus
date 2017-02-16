using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using ServiceBus.Contract;

namespace ServiceBus.Bus
{
    public class Receiver : IHandleMessages<SendMessage>
    {
        private static readonly ILog logger = LogManager.GetLogger<Receiver>();

        public Task Handle(SendMessage message, IMessageHandlerContext context)
        {
            logger.Info($"Received message: {message.Message}");

            return Task.CompletedTask;
        }
    }
}