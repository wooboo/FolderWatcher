using FolderWatcher.Common.Model;

namespace FolderWatcher.Common.Plugins
{
    public abstract class PluginBase<TConfig>: IPlugin
        where TConfig : PluginConfigBase
    {
        protected readonly TConfig Config;

        public PluginBase(TConfig config)
        {
            Config = config;
            Metadata = new PluginMetadata(config);
        }

        public PluginMetadata Metadata { get; }
        public abstract void OnFileCreated(FileChangeInfo file);

        public virtual void OnFileDeleted(FileChangeInfo file)
        {
        }
    }
}