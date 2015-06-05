using FolderWatcher.Common.Events;
using FolderWatcher.Common.Model;
using FolderWatcher.Common.Services;

namespace FolderWatcher.Common.Plugins
{
    public abstract class PluginBase<TConfig>: IPlugin
        where TConfig : PluginConfigBase
    {
        protected readonly TConfig Config;
        protected IValueBag _valueBag;

        public PluginBase(TConfig config)
        {
            Config = config;
            Metadata = new PluginMetadata(config);
        }

        public PluginMetadata Metadata { get; }
        public virtual void OnFilesChange(FileSystemChangeSet fileSystemChangeSet, IValueBag valueBag)
        {
            if (fileSystemChangeSet.Added != null)
            {
                foreach (var fileChangeInfo in fileSystemChangeSet.Added)
                {
                    OnFileCreated(fileChangeInfo, valueBag);
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

        public virtual void OnFileCreated(FileChangeInfo file, IValueBag valueBag)
        {
        }

        public virtual void OnFileDeleted(string file)
        {
        }
    }
}