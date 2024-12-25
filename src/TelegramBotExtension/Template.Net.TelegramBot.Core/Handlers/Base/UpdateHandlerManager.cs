using System.Data;
using System.Reflection;
using Telegram.Bot.Types.Enums;

namespace Template.Net.TelegramBot.Core.Handlers.Base
{
    /// <summary>
    /// A factory that takes all classes implementing <see cref="IApplicationUpdateHandler"/>
    /// </summary>
    public class UpdateHandlerManager
    {
        private readonly Dictionary<UpdateType, IApplicationUpdateHandler> _handlersCache = new();
        private readonly IServiceProvider _serviceProvider;

        public UpdateHandlerManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var applicationTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.GetInterfaces().Any(type => type == typeof(IApplicationUpdateHandler)));

            foreach (var type in applicationTypes)
            {
                var updateHandlerAttribute = type.GetCustomAttribute<UpdateHandlerAttribute>();
                if (updateHandlerAttribute == null)
                {
                    continue;
                }

                if (_handlersCache.ContainsKey(updateHandlerAttribute.UpdateType))
                {
                    throw new InvalidConstraintException($"Repeating the command name: {updateHandlerAttribute.UpdateType}");
                }

                _handlersCache.Add(updateHandlerAttribute.UpdateType, (IApplicationUpdateHandler)serviceProvider.GetService(type)!);
            }
        }

        /// <summary>
        /// Returns a handler implementing <see cref="IApplicationUpdateHandler"/>
        /// </summary>
        /// <param name="updateType"></param>
        /// <returns></returns>
        public IApplicationUpdateHandler? GetHandler(UpdateType updateType)
        {
            return _handlersCache.GetValueOrDefault(updateType);
        }
    }
}
