using System.Collections.Generic;

namespace FolderWatcher.Common.Services
{
    public interface IValueBag
    {
        IDictionary<string, string> Values { get; } 
    }
}