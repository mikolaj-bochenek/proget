namespace Proget.Messaging.RabbitMq.Helpers;

internal static class RabbitMqChannel
{
    public static void ConfigureChannel(IModel channel, RabbitMqChannelOptions options, ILogger logger)
    {
        if (options.BasicAck)
        {
            channel.ConfirmSelect();
            channel.BasicAcks += (sender, eventArgs) =>
            {
                var logInfoMsg = string.Format("Basic ACK for delivery-tag: '{0}'", eventArgs.DeliveryTag);
                logger.LogInformation("{Message}", logInfoMsg);
            };
        }

        if (options.ComplexAck)
        {
            channel.BasicReturn += (sender, eventArgs) =>
            {
                var logMsg = string.Format("Complex ACK for exchange: {0}. {1}", eventArgs.Exchange, eventArgs.ReplyText);

                if (eventArgs.ReplyCode is 200)
                {
                    logger.LogInformation("{Message}", logMsg);
                }
                else
                {
                    logger.LogError("{Message}", logMsg);
                }
            };
        }

    }
}