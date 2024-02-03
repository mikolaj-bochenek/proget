namespace Proget.Messaging.Channels;

internal interface ISubscribersChannel
{
    ChannelReader<IMessageSubscription> Reader { get; }
    ChannelWriter<IMessageSubscription> Writer { get; }
}