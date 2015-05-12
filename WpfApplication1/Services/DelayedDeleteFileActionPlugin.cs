using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FolderWatcher.Services
{
    [Export(typeof(IDelayedActionPlugin))]
    [Export("delayed_delete", typeof(IFileActionPlugin))]
    public class DelayedDeleteFileActionPlugin : IFileActionPlugin, IDelayedActionPlugin
    {
        public IList<DelayedFileDeletion> List { get; set; }
        public DelayedDeleteFileActionPlugin()
        {
            if (File.Exists(@"DelayedDeleteFileActionPlugin.json"))
            {
                List =
                    JsonConvert.DeserializeObject<List<DelayedFileDeletion>>(
                        File.ReadAllText(@"DelayedDeleteFileActionPlugin.json"));
            }
            else
            {
                List = new List<DelayedFileDeletion>();
            }
        }
        public Task<string> Execute(string file, params object[] args)
        {

            List.Add(new DelayedFileDeletion()
            {
                OrderDate = DateTime.Now,
                Path = file,
                Delay = (TimeSpan)args[0]
            });
            File.WriteAllText(@"DelayedDeleteFileActionPlugin.json",JsonConvert.SerializeObject(List));
            return Task.FromResult(string.Empty);
        }

        public void Sweep()
        {
            for (int i = 0; i < List.Count; i++)
            {
                var deletion = List[i];
                if (deletion.OrderDate + deletion.Delay <= DateTime.Now)
                {
                    File.Delete(deletion.Path);
                    List.RemoveAt(i);
                    i--;
                }
            }
            File.WriteAllText(@"DelayedDeleteFileActionPlugin.json", JsonConvert.SerializeObject(List));
            
        }
    }
}