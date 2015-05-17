namespace FolderWatcher.Common.Services
{
    public interface IFolder {
        string Name { get; }
        string FullPath { get; }
    }
}