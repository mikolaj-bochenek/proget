namespace Proget.Messaging;

internal sealed class MessagingOptionsBuilder : IMessagingOptionsBuilder
{
    public IServiceCollection Services { get; }

    public MessagingOptionsBuilder(IServiceCollection services)
    {
        Services = services;
    }
    
}