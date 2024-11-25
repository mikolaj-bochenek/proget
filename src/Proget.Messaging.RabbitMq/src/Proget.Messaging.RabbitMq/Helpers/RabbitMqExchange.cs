namespace Proget.Messaging.RabbitMq.Helpers;

internal static class RabbitMqExchange
{
    public static void DeclareExchange(IModel channel, RabbitMqExchangeOptions options, ILogger logger)
    {
        if (options.Declare)
        {
            channel.ExchangeDeclare(
                options.Name,
                options.Type.Humanize(LetterCasing.LowerCase),
                options.Durable,
                options.AutoDelete,
                options.Arguments
            );

            if (options.Logger)
            {
                var logInfoMsg = string.Join(
                    Environment.NewLine,
                    "The Exchange has been declared:",
                    string.Format("name: '{0}'", options.Name),
                    string.Format("type: '{0}'", options.Type),
                    string.Format("durable: '{0}'", options.Durable),
                    string.Format("autoDelete: '{0}'", options.AutoDelete),
                    string.Format("declaredBy: '{0}'", "Publisher")
                );
                
                logger.LogInformation("{Message}", logInfoMsg);
            }
        }
    }
}