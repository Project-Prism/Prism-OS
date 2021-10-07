using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

namespace PrismProject.Functions.Tests
{
    class Zip
    {
        public static void Extract(string ZipPath, string Password, string OutputFolder)
        { // Password is optional
            FileStream fs = File.OpenRead(ZipPath);
            ZipFile file = new ZipFile(fs);
            try
            {
                if (!string.IsNullOrEmpty(Password))
                {
                    file.Password = Password;
                }

                foreach (ZipEntry zipEntry in file)
                {
                    if (!zipEntry.IsFile)
                    {
                        // Ignore directories
                        continue;
                    }

                    // 4K is optimum
                    byte[] buffer = new byte[4096];
                    Stream zipStream = file.GetInputStream(zipEntry);

                    string fullZipToPath = Path.Combine(OutputFolder, zipEntry.Name);
                    string directoryName = Path.GetDirectoryName(fullZipToPath);

                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(directoryName);
                    }

                    using (FileStream streamWriter = File.Create(fullZipToPath))
                    {
                        StreamUtils.Copy(zipStream, streamWriter, buffer);
                    }
                }
            }
            finally
            {
                if (file != null)
                {
                    file.IsStreamOwner = true; // Makes close also shut the underlying stream
                    file.Close(); // Ensure we release resources
                }
            }
        }
        public static void compress(string DirectoryPath, string OutputFilePath, int CompressionLevel = 9)
        {
            try
            {

                string[] filenames = Directory.GetFiles(DirectoryPath);

            using (ZipOutputStream OutputStream = new ZipOutputStream(File.Create(OutputFilePath)))
            {
                OutputStream.SetLevel(CompressionLevel);

                byte[] buffer = new byte[4096];

                foreach (string file in filenames)
                {
                    ZipEntry entry = new ZipEntry(Path.GetFileName(file));

                    entry.DateTime = DateTime.Now;
                    OutputStream.PutNextEntry(entry);

                    using (FileStream fs = File.OpenRead(file))
                    {
                        int sourceBytes;

                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            OutputStream.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                }

                OutputStream.Finish();
                OutputStream.Close();

                Console.WriteLine("Files successfully compressed");
            }
        }
        catch (Exception ex)
        {
            // No need to rethrow the exception as for our purposes its handled.
            Console.WriteLine("Exception during processing {0}", ex);
        }
    }

        public static void list(string Zip)
        {
            ZipFile file = null;
            FileStream fs = File.OpenRead(Zip);
            file = new ZipFile(fs);
        }
    }
}
