using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using ServiceBus.Contract;

namespace ServiceBus.Console2
{
    class Program
    {
        private static readonly ILog logger = LogManager.GetLogger<Program>();

        static void Main(string[] args)
        {
            Console.Title = "Console 2";

            AsyncMain().GetAwaiter().GetResult();
        }

        static async Task AsyncMain()
        {
            var endpointConfiguration = new EndpointConfiguration("Console 2");

            var transport = endpointConfiguration.UseTransport<MsmqTransport>();

            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                                                 .ConfigureAwait(false);
            await RunLoop(endpointInstance);

            await endpointInstance.Stop()
                                  .ConfigureAwait(false);
        }

        static async Task RunLoop(IEndpointInstance endpointInstance)
        {
            while (true)
            {
                logger.Info("Press 'Enter' to send message, or 'Q' to quit.");

                var key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        // Instantiate the command
                        var command = new SendMessage
                        {
                            Message = Guid.NewGuid().ToString()
                        };

                        logger.Info($"Sending message to Console 1: {command.Message}");

                        await endpointInstance.Send("Console 1", command)
                                              .ConfigureAwait(false);

                        break;
                    case ConsoleKey.Q:
                        return;
                }
            }
        }
    }
}