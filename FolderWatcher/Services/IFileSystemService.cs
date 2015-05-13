namespace FolderWatcher.Services
{
    public interface IFileSystemService
    {
        FileAction ForFile(string file);
    }
}