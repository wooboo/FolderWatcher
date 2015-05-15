using Microsoft.Practices.Prism.PubSubEvents;

namespace FolderWatcher.Services.Events
{
    public class FileDeletedEvent : PubSubEvent<string>
    {
        
    }
}