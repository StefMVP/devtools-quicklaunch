using AppQuickLaunch.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Windows;

namespace AppQuickLaunch
{
    public class ConfigCache : IConfigCache
    {
        private QuickLaunchConfig _config;
        private readonly IMemoryCache _memoryCache;
        private const string ConfigKey = "ConfigKey";
        private MemoryCacheEntryOptions _options;
        private readonly IConfigComponent _configComponent;

        public ConfigCache(IMemoryCache memoryCache, IConfigComponent configComponent)
        {
            _configComponent = configComponent;
            _options = new MemoryCacheEntryOptions();
            _options.SetAbsoluteExpiration(TimeSpan.FromMinutes(600000));
            _memoryCache = memoryCache;
            SetConfig();
        }

        public QuickLaunchConfig GetConfig()
        {
            return _config;
        }

        public void SetConfig()
        {
            try
            {
                if (_memoryCache.TryGetValue(ConfigKey, out QuickLaunchConfig config))
                {
                    _config = config;
                }
                else
                {
                    _configComponent.CreateConfigIfDoesntExists();
                    _config = _configComponent.LoadConfig();
                    _memoryCache.CreateEntry(ConfigKey);
                    _memoryCache.Set(ConfigKey, _config, _options);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}