using System;
using System.Linq;
using System.Windows.Threading;
using FolderWatcher.Common.Events;
using FolderWatcher.Common.Model;
using FolderWatcher.Common.Plugins;
using FolderWatcher.Common.Services;
using FolderWatcher.Core.Services;
using Microsoft.Practices.ServiceLocation;

namespace FolderWatcher.Plugins.Delay
{
    public class DelayPlugin : PluginBase<DelayPluginConfig>
    {
        private readonly DispatcherTimer _dispatcherTimer;
        private readonly IPlugin _plugin;

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
            return pluginManager.LoadAllPlugins(Config.GetPath(Config.GetName()), new[] {Config.Plugin}).Single();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Sweep();
        }

        public override void OnFileCreated(FileChangeInfo file, IValueBag valueBag)
        {
            Config.FileStates.Add(new FileState
            {
                CreateDate = DateTime.Now,
                File = file,
                DelayAfter = Config.DelayDelay,
                ValueBag = valueBag
            });
            Config.Save();
        }

        public override void OnFileDeleted(string file)
        {
            var state = Config.FileStates.SingleOrDefault(o => o.File.FullPath == file);
            if (state != null)
            {
                Config.FileStates.Remove(state);
            }
            Config.Save();
        }

        public void Sweep()
        {
            var fileStates = Config.FileStates.Where(deletion=> deletion.CreateDate + deletion.DelayAfter <= DateTime.Now).ToList();
            _plugin.OnFilesChange(new FileSystemChangeSet()
            {
                Added = fileStates.Select(o=>o.File)
            }, fileStates.FirstOrDefault()?.ValueBag);

            foreach (var toDelete in fileStates)
            {
                Config.FileStates.Remove(toDelete);

            }
            Config.Save();
        }
    }
}