namespace Proget.Messaging.RabbitMq.Helpers;

internal static class RabbitMqQueue
{
    public static void DeclareQueue(IModel channel, RabbitMqQueueOptions options, ILogger logger)
    {
        channel.QueueDeclare(options.Name, options.Durable, options.Exclusive, options.AutoDelete);
        channel.BasicQos(options.PrefetchSize, options.PrefetchCount, options.Global);

        if (options.Logger)
        {
            var logInfoMsg = string.Join(
                Environment.NewLine,
                "The Queue has been declared:",
                string.Format("name: '{0}'", options.Name),
                string.Format("durable: '{0}'", options.Durable),
                string.Format("exclusive: '{0}'", options.Exclusive),
                string.Format("autoDelete: '{0}'", options.AutoDelete),
                string.Format("prefetchSize: '{0}'", options.PrefetchSize),
                string.Format("prefetchCount: '{0}'", options.PrefetchCount),
                string.Format("global: '{0}'", options.Global)
            );
            logger.LogInformation("{Message}", logInfoMsg);
        }
       
        if (options.AutoBind)
        {
            channel.QueueBind(options.Name, options.BindExchange, options.BindRoutingKey);

            if (options.Logger)
            {
                var logInfoMsg = string.Format(
                    "Queue bind: {0} to exchange: {1} with routing-key: {2}",
                    options.Name, options.BindExchange, options.BindRoutingKey
                );
                logger.LogInformation("{Message}", logInfoMsg);
            }
        }
    }
}