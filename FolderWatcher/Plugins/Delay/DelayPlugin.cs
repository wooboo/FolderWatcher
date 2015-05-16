using System;
using System.Collections.Generic;
using System.Linq;
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
    public class DelayPlugin : PluginBase<DelayPluginConfig>
    {
        readonly DispatcherTimer _dispatcherTimer;
        private IPlugin _plugin;
        public DelayPlugin(DelayPluginConfig config) : base(config)
        {
            _plugin = LoadPlugin();
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += dispatcherTimer_Tick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            _dispatcherTimer.Start();

        }
        private IPlugin LoadPlugin()
        {
            var pluginManager = ServiceLocator.Current.GetInstance<PluginManager>();
            return pluginManager.LoadAllPlugins(Config.GetPath(Config.GetName()), new[] { Config.Plugin}).Single();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Sweep();
        }

        public override void OnFileCreated(FileChangeInfo file)
        {
            Config.FileStates.Add(new FileState
            {
                CreateDate = DateTime.Now,
                File = file,
                DelayAfter = Config.DelayDelay
            });
            Config.Save();
        }

        public override void OnFileDeleted(FileChangeInfo file)
        {
            var state = Config.FileStates.SingleOrDefault(o => o.File.FullPath == file.FullPath);
            if (state != null)
            {
                Config.FileStates.Remove(state);
            }
            Config.Save();
        }


        public void Sweep()
        {
            var fileStates = Config.FileStates;
            for (int i = 0; i < fileStates.Count; i++)
            {
                var deletion = fileStates[i];
                if (deletion.CreateDate + deletion.DelayAfter <= DateTime.Now)
                {
                    var file = deletion.File;
                    _plugin.OnFileCreated(file);

                    fileStates.RemoveAt(i);
                    i--;
                }
            }
            Config.Save();
        }

    }
}