using System.ComponentModel.Composition;
using System.IO;
using System.Threading.Tasks;

namespace FolderWatcher.Services
{
    [Export("delete", typeof (IFileActionPlugin))]
    public class DeleteFileActionPlugin : IFileActionPlugin
    {
        public Task<string> Execute(string file, params string[] args)
        {
            File.Delete(file);
            return Task.FromResult(string.Empty);
        }
    }
}