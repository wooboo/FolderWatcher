using FolderWatcher.Common.Events;
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
        public virtual void OnFilesChange(FileSystemChangeSet fileSystemChangeSet)
        {
            if (fileSystemChangeSet.Added != null)
            {
                foreach (var fileChangeInfo in fileSystemChangeSet.Added)
                {
                    OnFileCreated(fileChangeInfo);
                }
            }
            if (fileSystemChangeSet.Deleted != null)
            {
                foreach (var path in fileSystemChangeSet.Deleted)
                {
                    OnFileDeleted(path);
                }
            }
        }

        public virtual void OnFileCreated(FileChangeInfo file)
        {
        }

        public virtual void OnFileDeleted(string file)
        {
        }
    }
}