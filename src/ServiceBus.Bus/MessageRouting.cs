using NServiceBus;

namespace ServiceBus.Bus
{
    public class MessageRouting
    {
        public void Configure(RoutingSettings routingSettings)
        {
            // Specify the routing for a specific type
            //routingSettings.RouteToEndpoint(typeof(SendMessage), "SomeEndpoint");
        }
    }
}