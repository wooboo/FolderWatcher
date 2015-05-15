using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FolderWatcher.Plugins.Delay;
using FolderWatcher.Services;
using FolderWatcher.Services.Events;
using FolderWatcher.Watcher;
using Microsoft.Practices.ServiceLocation;

namespace FolderWatcher.Plugins.Delay
{
    public class DelayPlugin : IPlugin, IPeriodocalPlugin
    {
        private readonly DelayPluginConfig _config;
        private readonly IFileSystemService _fileSystemService;

        public DelayPlugin(DelayPluginConfig config, IFileSystemService fileSystemService)
        {
            _config = config;
            _fileSystemService = fileSystemService;
        }

        public void OnFile(FileSystemItem file)
        {
            _config.FileStates.Add(new FileState
            {
                CreateDate = DateTime.Now,
                Path = file,
                DelayAfter = _config.DelayDelay
            });
            _config.Save();
        }


        public void Sweep()
        {
            var fileStates = _config.FileStates;
            for (int i = 0; i < fileStates.Count; i++)
            {
                var deletion = fileStates[i];
                if (deletion.CreateDate + deletion.DelayAfter <= DateTime.Now)
                {
                    var file = deletion.Path;

                    var factories = ServiceLocator.Current.GetAllInstances<IPluginFactory>();
                            foreach (var pluginFactory in factories)
                            {
                                    if (FitsMask(_config.Plugin, pluginFactory.Name + ".*.json"))
                                    {
                                        //TODO: niez³e spaghetti :D
                                        foreach (
                                            var plugin in
                                                pluginFactory.CreatePlugins(_config.GetPath("delay", _config.Plugin)))
                                        {
                                            plugin.OnFile(file);
                                        }
                        }

                    }








                    //TODO: odpalanie wybranych pluginów
                   // _fileSystemService.ForFile(deletion.Path).Call("Delay");








                    fileStates.RemoveAt(i);
                    i--;
                }
            }
            _config.Save();
        }
        private bool FitsMask(string sFileName, string sFileMask)
        {
            Regex mask = new Regex(sFileMask.Replace(".", "[.]").Replace("*", ".*").Replace("?", "."));
            return mask.IsMatch(sFileName);
        }
    }
}