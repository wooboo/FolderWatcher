using Microsoft.Practices.Prism.PubSubEvents;

namespace FolderWatcher.Services.Events
{
    public class FilesEvent : PubSubEvent<FileSystemChangeSet>
    {

    }
}