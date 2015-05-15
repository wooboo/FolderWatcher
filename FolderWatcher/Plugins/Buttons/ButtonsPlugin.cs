using System;
using System.Threading.Tasks;
using FolderWatcher.Services;
using FolderWatcher.Services.Events;
using FolderWatcher.Watcher;

namespace FolderWatcher.Plugins.Buttons
{
    public class ButtonsPlugin : IPlugin
    {
        private readonly ButtonsPluginConfig _config;
        private readonly IFileSystemService _fileSystemService;

        public ButtonsPlugin(ButtonsPluginConfig config, IFileSystemService fileSystemService)
        {
            _config = config;
            _fileSystemService = fileSystemService;
        }

        public void OnFile(FileSystemItem file)
        {
            //file.PluginParts.Add(new ButtonsPluginPart(this, file));
        }
    }
}