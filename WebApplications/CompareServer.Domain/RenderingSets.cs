using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CompareServer.Domain
{
    public class RenderingSets
    {

        public static List<string> GetList()
        {
            string[] files = Directory.GetFiles(GetRSPath());
            if (files.Length <= 0)
            {
                throw new Exception("Rendering Sets not found");
            }

            return files.Where(file => Path.GetExtension(file).ToLower() == ".set")
                        .Select(file => Path.GetFileName(file))
                        .OrderBy(file => file)
                        .ToList();        
        }

        public static int Count()
        {
            return GetList().Count;
        }

        public static void SaveFile(string fileName, Stream stream)
        {
            string path = GetRSPath();
            fileName = Path.Combine(path, fileName);
            string[] files = Directory.GetFiles(path);
            if (files.Contains(fileName))
            {
                throw new Exception(string.Format("File name {0} allready exists", Path.GetFileName(fileName)));
            }

            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            using (FileStream fs = File.Create(fileName))
            {
                fs.Write(buffer, 0, buffer.Length);
            }
        }

        public static void DeleteFile(string fileName)
        {
            string path = Path.Combine(GetRSPath(), fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                throw new Exception("File not found");
            }
        }


        /* Dummy func */
        private static string GetRSPath()
        {
            return AppConfig.RendersetPath;
        }
    }
}
