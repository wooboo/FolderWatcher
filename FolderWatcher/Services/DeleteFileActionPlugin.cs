using System.ComponentModel.Composition;
using System.IO;
using System.Threading.Tasks;
using FolderWatcher.Services.Events;
using Microsoft.Practices.Prism.PubSubEvents;

namespace FolderWatcher.Services
{
    [Export(typeof(IFileActionPlugin))]
    public class DeleteFileActionPlugin : IFileActionPlugin
    {
        private readonly IEventAggregator _eventAggregator;
        [ImportingConstructor]
        public DeleteFileActionPlugin(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public string Name { get; } = "delete";

        public Task<string> Execute(string file, params object[] args)
        {
            File.Delete(file);
            _eventAggregator.GetEvent<FileDeletedEvent>().Publish(file);
            return Task.FromResult(string.Empty);
        }
    }
}