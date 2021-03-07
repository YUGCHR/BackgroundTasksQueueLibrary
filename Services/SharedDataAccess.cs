using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CachingFramework.Redis.Contracts;
using CachingFramework.Redis.Contracts.Providers;
using Microsoft.Extensions.Logging;
using BackgroundTasksQueue.Library.Models;

namespace BackgroundTasksQueue.Library.Services
{
    public interface ISharedDataAccess
    {
        public Task SetStartConstants(EventKeyNames eventKeysSet, string checkTokenFetched);
        public Task<EventKeyNames> FetchAllConstants();
    }

    public class SharedDataAccess : ISharedDataAccess
    {
        
        private readonly ILogger<SharedDataAccess> _logger;
        private readonly ICacheProviderAsync _cache;
        private readonly IKeyEventsProvider _keyEvents;

        public SharedDataAccess(
            ILogger<SharedDataAccess> logger,
            ICacheProviderAsync cache,
            IKeyEventsProvider keyEvents)
        {
            _logger = logger;
            _cache = cache;
            _keyEvents = keyEvents;
        }

        private const string StartConstantKey = "constants";
        private const string StartConstantField = "all";
        private const string CheckToken = "tt-tt-tt";

        public async Task SetStartConstants(EventKeyNames eventKeysSet, string checkTokenFetched)
        {
            if (checkTokenFetched == CheckToken)
            {
                await _cache.SetHashedAsync<EventKeyNames>(StartConstantKey, StartConstantField, eventKeysSet,
                    eventKeysSet.EventKeyBackReadinessTimeDays);

                _logger.LogInformation(55050, "SetStartConstants set constants (EventKeyFrom for example = {0}) in key {1}.", eventKeysSet.EventKeyFrom, "constants");
            }
            else
            {
                _logger.LogError(55070, "SetStartConstants try to set constants unsuccessfully.");
            }
        }

        public async Task<EventKeyNames> FetchAllConstants()
        {
            // 
            return await _cache.GetHashedAsync<EventKeyNames>(StartConstantKey, StartConstantField);
        }
    }
}