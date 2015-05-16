using FolderWatcher.Services.Events;
using FolderWatcher.Watcher;

namespace FolderWatcher.Plugins
{
    public abstract class PluginBase<TConfig>: IPlugin
        where TConfig : PluginConfigBase
    {
        protected readonly TConfig Config;

        public PluginBase(TConfig config)
        {
            Config = config;
            Name = new PluginName(config.GetName());
        }

        public PluginName Name { get; }
        public abstract void OnFileCreated(FileChangeInfo file);

        public virtual void OnFileDeleted(FileChangeInfo file)
        {
        }
    }
}