using Microsoft.Practices.Prism.PubSubEvents;

namespace FolderWatcher.Common.Events
{
    public class FilesEvent : PubSubEvent<FileSystemChangeSet>
    {

    }
}