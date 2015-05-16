using System.IO;
using Newtonsoft.Json;

namespace FolderWatcher.Plugins
{
    public abstract class PluginConfigBase
    {
        private readonly string _path;

        public PluginConfigBase(string path)
        {
            _path = path;
        }

        public virtual bool Load()
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
            else
            {
                Save();
                return false;
            }
        }

        public virtual void Save()
        {
            var content = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(_path, content);
        }

        public string GetPath(params string[] parts)
        {
            return Path.Combine(Path.GetDirectoryName(_path), Path.Combine(parts));
        }

        public string GetName()
        {
            return Path.GetFileNameWithoutExtension(_path);
        }
    }
}