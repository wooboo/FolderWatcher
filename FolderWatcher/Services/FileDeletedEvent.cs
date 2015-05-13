using Microsoft.Practices.Prism.PubSubEvents;

namespace FolderWatcher.Services
{
    public class FileDeletedEvent : PubSubEvent<string>
    {
        
    }
}