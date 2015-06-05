using System;
using System.IO;
using System.Linq;
using FolderWatcher.Common.Events;
using FolderWatcher.Common.Model;
using FolderWatcher.Common.Plugins;
using FolderWatcher.Common.Services;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.VisualBasic.FileIO;

namespace FolderWatcher.Plugins.Files.Extract
{
    public class ExtractPlugin : PluginBase<ExtractPluginConfig>
    {

        public ExtractPlugin(ExtractPluginConfig config) : base(config)
        {
        }

        public override void OnFileCreated(FileChangeInfo file, IValueBag valueBag)
        {
            ExtractZipFile(file.FullPath, null, Path.Combine(Path.GetDirectoryName(file.FullPath), Path.GetFileNameWithoutExtension(file.FullPath)));
        }


        public void ExtractZipFile(string archiveFilenameIn, string password, string outFolder)
    {
        ZipFile zf = null;
        try
        {
            FileStream fs = File.OpenRead(archiveFilenameIn);
            zf = new ZipFile(fs);
            if (!String.IsNullOrEmpty(password))
            {
                zf.Password = password;     // AES encrypted entries are handled automatically
            }
            foreach (ZipEntry zipEntry in zf)
            {
                if (!zipEntry.IsFile)
                {
                    continue;           // Ignore directories
                }
                String entryFileName = zipEntry.Name;
                // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                // Optionally match entrynames against a selection list here to skip as desired.
                // The unpacked length is available in the zipEntry.Size property.

                byte[] buffer = new byte[4096];     // 4K is optimum
                Stream zipStream = zf.GetInputStream(zipEntry);

                // Manipulate the output filename here as desired.
                String fullZipToPath = Path.Combine(outFolder, entryFileName);
                string directoryName = Path.GetDirectoryName(fullZipToPath);
                if (directoryName.Length > 0)
                    Directory.CreateDirectory(directoryName);

                // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                // of the file, but does not waste memory.
                // The "using" will close the stream even if an exception occurs.
                using (FileStream streamWriter = File.Create(fullZipToPath))
                {
                    StreamUtils.Copy(zipStream, streamWriter, buffer);
                }
            }
            //var outFiles = Directory.GetFileSystemEntries(outFolder);
            //if (outFiles.Length == 1 && Directory.Exists(outFiles[0]))
            //{
            //    var directoryName = Path.GetFileName(outFiles[0]);
            //    var parentFolder = Path.GetDirectoryName(outFolder);
            //    Directory.Move(outFiles[0], Path.Combine(parentFolder, directoryName));
            //}
        }
        finally
        {
            if (zf != null)
            {
                zf.IsStreamOwner = true; // Makes close also shut the underlying stream
                zf.Close(); // Ensure we release resources
            }
        }
    }

}
}