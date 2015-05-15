using System.IO;
using Newtonsoft.Json;

namespace FolderWatcher.Plugins.DeleteFile
{
    public abstract class PluginConfigBase
    {
        private readonly string _path;

        public PluginConfigBase(string path)
        {
            _path = path;
        }

        public bool TryLoad()
        {
            if (File.Exists(_path))
            {
                var content = File.ReadAllText(_path);
                if (!string.IsNullOrWhiteSpace(content))
                {
                    JsonConvert.PopulateObject(content, this);
                }
                return true;
            }
            return false;
        }

        public void Save()
        {
            var content = JsonConvert.SerializeObject(this);
            File.WriteAllText(_path, content);
        }

        public string GetPath(params string[] parts)
        {
            return Path.Combine(Path.GetDirectoryName(_path), Path.Combine(parts));
        }
    }
}