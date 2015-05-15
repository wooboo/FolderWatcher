using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Threading;
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
        readonly DispatcherTimer _dispatcherTimer;

        public DelayPlugin(DelayPluginConfig config)
        {
            _config = config;
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += dispatcherTimer_Tick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            _dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Sweep();
        }
        public void OnFileCreated(FileChangeInfo file)
        {
            _config.FileStates.Add(new FileState
            {
                CreateDate = DateTime.Now,
                Path = file,
                DelayAfter = _config.DelayDelay
            });
            _config.Save();
        }

        public void OnFileDeleted(FileChangeInfo file)
        {
            //TODO: remove from FileStates if there is
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
                                plugin.OnFileCreated(file);
                            }
                        }

                    }

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