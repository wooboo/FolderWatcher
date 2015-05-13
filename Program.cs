using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var fw = new FolderWatcher(@"~\Downloads");
            fw.Start();

            Console.ReadLine();
            fw.Stop();
        }

    }
    public class FolderWatcher
    {
        private string _path;
        FileSystemWatcher _fsw; 
        public FolderWatcher(string path)
        {

            _path = Environment.ExpandEnvironmentVariables(path.Replace("~", "%USERPROFILE%"));
            _fsw = new FileSystemWatcher(_path);
            _fsw.Error += _fsw_Error;
            _fsw.Deleted += _fsw_Deleted;
            _fsw.Created += _fsw_Created;
            _fsw.Changed += _fsw_Changed;
        }

        void _fsw_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.Name);
        }


        void _fsw_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.Name);
        }

        void _fsw_Deleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine(e.Name);
        }

        void _fsw_Error(object sender, ErrorEventArgs e)
        {
            Console.WriteLine(e.GetException());
        }
        public void Start()
        {
            _fsw.EnableRaisingEvents = true;
        }
        public void Stop()
        {
            _fsw.EnableRaisingEvents = false;
        }
    }
}
