namespace FolderWatcher.Common.Plugins
{
    public class PluginMetadata
    {
        public PluginMetadata(PluginConfigBase config)
        {
            Rank = config.Rank;
            var fullName = config.GetName();
            FullName = fullName;
            Name = fullName.Split('.')[0];
            InstanceName = fullName.Split('.')[1];
        }
        public string Name { get; }
        public string InstanceName { get; }
        public string FullName { get; }
        public int Rank { get; }
    }
}