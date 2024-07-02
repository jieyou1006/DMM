using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GetFileName
{
    class Program
    {
        public static string ExceptFile = "EXCEL";  //排除的文件夾分號隔開
        public static string ExceptFileType = ".scc;.config";  //排除的文件類型分號隔開
        public static List<FileInfo> ListFileInfo = new List<FileInfo>();
        static void Main(string[] args)
        {
            DirectoryInfo dir = new DirectoryInfo("O:\\迅雷下载");
            GetChildDicsName(dir);
            Console.ReadLine();
        }

        public static DirectoryInfo[] GetChildDicsName(DirectoryInfo dir)
        {
            //FileInfo[] files = dir.GetFiles();
            DirectoryInfo[] childDirs = dir.GetDirectories();

            foreach (var item in childDirs)
            {
                Console.WriteLine(item + ",");
            }

            //var fileArray = files.Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden));  //排除隱藏文件

            //foreach (FileInfo file in fileArray)
            //{
            //    bool boolCheckExceptFile = false;
            //    bool boolCheckExceptFileType = false;
            //    foreach (var item in ExceptFile.Split(';'))
            //    {
            //        if (file.FullName.ToUpper().Contains(item))  //排除路徑
            //        {
            //            boolCheckExceptFile = true;
            //        }
            //    }
            //    if (boolCheckExceptFile)
            //    {
            //        continue;
            //    }

            //    foreach (var item in ExceptFileType.Split(';'))
            //    {
            //        if (file.Extension.ToString().ToLower() == item)  //排除文件擴展名
            //        {
            //            boolCheckExceptFileType = true;
            //        }
            //    }
            //    if (boolCheckExceptFileType)
            //    {
            //        continue;
            //    }

            //    ListFileInfo.Add(file);
            //}

            //递归获取子文件夹名
            //foreach (DirectoryInfo dirChild in childDirs)
            //{
            //    GetChildDicsName(dirChild);
            //}

            return childDirs;
        }
    }
}
