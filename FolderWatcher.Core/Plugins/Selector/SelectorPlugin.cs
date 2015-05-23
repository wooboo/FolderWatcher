using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FolderWatcher.Common.Events;
using FolderWatcher.Common.Model;
using FolderWatcher.Common.Plugins;
using FolderWatcher.Core.Services;
using Microsoft.Practices.ServiceLocation;

namespace FolderWatcher.Core.Plugins.Selector
{
    public class SelectorPlugin : PluginBase<SelectorPluginConfig>
    {
        private IDictionary<string, IPlugin> _plugins;

        public SelectorPlugin(SelectorPluginConfig config) : base(config)
        {
            _plugins = LoadPlugins();
        }
        private IDictionary<string, IPlugin> LoadPlugins()
        {
            var dict= new Dictionary<string, IPlugin>();
            var pluginManager = ServiceLocator.Current.GetInstance<PluginManager>();
            var plugins = pluginManager.LoadAllPlugins(Config.GetPath(Config.GetName()), Config.Masks.Values.Distinct()).ToList();
            
            foreach (var mask in Config.Masks)
            {
                dict[mask.Key] = plugins.Single(o => o.Metadata.FullName == mask.Value);
            }
            return dict;
        }
        private IEnumerable<FileChangeInfo> GetFilesOfMask(IEnumerable<FileChangeInfo> files,string mask)
        {
            return files.Where(o => FitsMask(o.FullPath, mask));
        }

        public override void OnFilesChange(FileSystemChangeSet fileSystemChangeSet)
        {
            foreach (var masks in Config.Masks)
            {
                _plugins[masks.Key].OnFilesChange(new FileSystemChangeSet() {Added = GetFilesOfMask(fileSystemChangeSet.Added, masks.Key).ToList()});
            }
        }

        private bool FitsMask(string sFileName, string sFileMask)
        {
            Regex mask = new Regex(sFileMask.Replace(".", "[.]").Replace("*", ".*").Replace("?", "."));
            return mask.IsMatch(sFileName);
        }
    }
}