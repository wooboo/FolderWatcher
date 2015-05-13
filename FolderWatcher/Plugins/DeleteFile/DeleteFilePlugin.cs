using System;
using System.Threading.Tasks;
using FolderWatcher.Services;
using FolderWatcher.Watcher;

namespace FolderWatcher.Plugins.DeleteFile
{
    public class DeleteFilePlugin : IPlugin, IPeriodocalPlugin
    {
        private readonly DeleteFilePluginConfig _config;
        private readonly IFileSystemService _fileSystemService;

        public DeleteFilePlugin(DeleteFilePluginConfig config, IFileSystemService fileSystemService)
        {
            _config = config;
            _fileSystemService = fileSystemService;
        }

        public void OnFile(IFileSystemService fileSystemService, ChangedFile file)
        {
            file.PluginParts.Add(new DeleteFilePluginPart(this, file));
        }

        public void DelayedDelete(ChangedFile changedFile, TimeSpan fromMinutes)
        {
            _config.FileStates.Add(new FileState
            {
                CreateDate = DateTime.Now,
                Path = changedFile.FullPath,
                DeleteAfter = fromMinutes
            });
            _config.Save();
        }

        public Task Delete(ChangedFile changedFile)
        {
            return _fileSystemService.ForFile(changedFile.FullPath).Call("delete");
        }

        public void Sweep()
        {
            var fileStates = _config.FileStates;
            for (int i = 0; i < fileStates.Count; i++)
            {
                var deletion = fileStates[i];
                if (deletion.CreateDate + deletion.DeleteAfter <= DateTime.Now)
                {
                    _fileSystemService.ForFile(deletion.Path).Call("delete");
                    fileStates.RemoveAt(i);
                    i--;
                }
            }
            _config.Save();
        }
    }
}