using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Yame.Tools.Helper
{
    public static class DirectoryHelper
    {
        /// <summary>
        /// 刪除資料夾和所有檔案
        /// </summary>
        /// <param name="target_dir"></param>
        /// <returns></returns>
        public static bool DeleteDirectory(string target_dir)
        {
            if (Directory.Exists(target_dir) == false)
            {
                return true;
            }

            bool result = false;
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);
            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }
            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }
            Directory.Delete(target_dir, false);
            return result;
        }

        public static void DeleteFile(string target_dir)
        {
            if (File.Exists(target_dir))
            {
                File.Delete(target_dir);
            }
        }
    }
}
