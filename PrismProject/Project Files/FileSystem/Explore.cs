using System.IO;

namespace PrismProject.System2.FileSystem
{
    class Explore
    {
        /// <summary>
        /// Lists a directory's contents.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>string array</returns>
        public static string[] ListPath(string path)
        {
            return Directory.GetFiles(path);
        }
        /// <summary>
        /// Make a directory
        /// </summary>
        /// <param name="path"></param>
        public static void MakeDirectory(string path)
        {
            Config.fs.CreateDirectory(System.Convert.ToString(path));
        }
        /// <summary>
        /// Make a file at the specified path
        /// </summary>
        /// <param name="path"></param>
        public static void MakeFile(string path, string FileName)
        {
            File.Create(path + FileName);
        }
        /// <summary>Read a file's contents</summary>
        /// <param name="path"></param>
        /// <returns>File's contents.</returns>
        public static string Read(string path)
        {
            return File.ReadAllText(path).ToString();
        }
        /// <summary>
        /// Write content to a file. Creates file if it doesnt exist.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="FileName"></param>
        /// <param name="contents"></param>
        public static void Write(string path, string FileName, string contents)
        {
            if (Exists(path))
            {
                File.WriteAllText(path, contents);
            }
            else
            {

                MakeFile(path, FileName);
                File.WriteAllText(path, contents);
            }
        }
        /// <summary>
        /// Check if a file exists
        /// </summary>
        /// <param name="path"></param>
        /// <returns>True or false</returns>
        public static bool Exists(string path)
        {
            if (File.Exists(path))
                return true;
            else
                return false;
        }
    }
}
