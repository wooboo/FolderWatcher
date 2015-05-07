namespace WpfApplication1.Watcher
{
    public interface IPlugin
    {
        void Init(object settings);
        string OnFile(string path);
    }
}