global using System.ComponentModel.DataAnnotations;
global using System.Reflection;
global using System.Text;

global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.Logging;

global using Proget.Messaging.RabbitMq.Subscribers;
global using Proget.Messaging.RabbitMq.Publishers;
global using Proget.Messaging.RabbitMq.Connection;
global using Proget.Messaging.RabbitMq.Channels;
global using Proget.Messaging.RabbitMq.Helpers;
global using Proget.Messaging.RabbitMq.Options;
global using Proget.Messaging.RabbitMq.Routing;
global using Proget.Web;

global using Humanizer;
global using RabbitMQ.Client;
global using RabbitMQ.Client.Events;