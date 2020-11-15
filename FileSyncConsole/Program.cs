using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileSyncConsole
{
    class Program
    {
        public static string ExecType = "push";  //程序執行方式push,get
        public static string path = "";  //程序執行目錄
        public static string UserName = "job";
        public static string PassWord = "foxconn123456...";  //寫入log的文本
        public static string ExceptFile = "EXCEL";  //排除的文件夾分號隔開
        public static string ExceptFileType = ".scc;.config";  //排除的文件類型分號隔開
        public static string path1 = "";  //文件同步源路徑
        public static string path2 = "";  //文件同步目標路徑
        public static List<FileInfo> ListFileInfo1 = new List<FileInfo>();
        public static List<FileInfo> ListFileInfo2 = new List<FileInfo>();
        public static void Main(string[] args)
        {
            Start();
        }

        public static void Start()
        {
            WriteMessage("{0}啟動");
            WriteMessage("{0}開始讀取路徑");
            path = GetPath().Replace("\\", "/");
            WriteMessage("{0}開始讀取配置文件");
            string strConfigPath = GetPath().Replace("\\", "/") + "Config.txt";

            if (!File.Exists(strConfigPath))  //檢測將要生成的文件是否存在
            {
                WriteMessage("{0}配置文件不存在");
                return;
            }

            try
            {
                FileStream fs = new FileStream(strConfigPath, FileMode.Open);
                // "GB2312"用于显示中文字符，写其他的，中文会显示乱码
                StreamReader reader = new StreamReader(fs, UnicodeEncoding.GetEncoding("GB2312"));
                ArrayList ConfigList = new ArrayList();
                // 一行一行读取
                string strLine = string.Empty;
                while ((strLine = reader.ReadLine()) != null)
                {
                    ConfigList.Add(strLine);
                }

                if (ConfigList.Count == 0)
                {
                    WriteMessage("{0}配置文件為空");
                    return;
                }
                else if (ConfigList.Count < 4)
                {
                    WriteMessage("{0}配置文件不完整");
                    return;
                }
                else
                {
                    ExecType = ConfigList[1].ToString().Split('=')[1].Trim();
                    UserName = ConfigList[2].ToString().Split('=')[1].Trim();
                    PassWord = ConfigList[3].ToString().Split('=')[1].Trim();
                    ExceptFile = ConfigList[4].ToString().Split('=')[1].Trim();
                    ExceptFileType = ConfigList[5].ToString().Split('=')[1].Trim();
                    path1 = ConfigList[6].ToString().Split('=')[1].Trim();
                    path2 = ConfigList[7].ToString().Split('=')[1].Trim();
                }

                //关闭文件流
                fs.Close();
            }
            catch (Exception ex)
            {
                WriteMessage("{0}配置文件讀取錯誤：" + ex.ToString());
                return;
            }

            if (string.IsNullOrEmpty(path1) || string.IsNullOrEmpty(path2))
            {
                WriteMessage("{0}源路徑和目標路徑不能為空");
                return;
            }


            DirectoryInfo dir1;
            DirectoryInfo dir2;
            if (ExecType == "push")
            {
                dir1 = new DirectoryInfo(path1);
                dir2 = new DirectoryInfo(path2);
            }
            else  //ExecType == "get"
            {
                dir1 = new DirectoryInfo(path2);
                dir2 = new DirectoryInfo(path1);
            }

            if (!dir1.Exists)
            {
                WriteMessage("{0}源路徑不存在，无法同步");
                return;
            }

            if (!dir2.Exists)
            {
                WriteMessage("{0}目标路徑不存在");
                WriteMessage("{0}创建目标路径");
                dir2.Create();
                WriteMessage("{0}目标路徑创建成功");
            }

            ListFileInfo1.Clear();
            ListFileInfo2.Clear();

            //if (!ExecCmd(path2))
            //{
            //    WriteMessage("{0}賬號密碼登陸命令執行不成功");
            //    return;
            //}

            WriteMessage("{0}登陸成功");

            GetChildDicsName(dir1, 1);
            GetChildDicsName(dir2, 2);
            WriteMessage("{0}獲取文件路徑成功");
            bool boolIsChange = false;
            try
            {
                //同步本地新文件和修改過的文件到目標地址
                foreach (var item1 in ListFileInfo1)
                {
                    bool boolFileExists = false;
                    foreach (var item2 in ListFileInfo2)
                    {
                        if (item1.FullName.Replace(ExecType == "push" ? path1 : path2, "") == item2.FullName.Replace(ExecType == "push" ? path2 : path1, ""))
                        {
                            boolFileExists = true;
                            if (item1.LastWriteTimeUtc != item2.LastWriteTimeUtc)
                            {
                                boolIsChange = true;
                                try
                                {
                                    //修改過的文件
                                    WriteMessage("{0}同步" + item1.FullName);
                                    File.Copy(item1.FullName, item2.FullName, true);  //複製文件並覆蓋
                                }
                                catch (Exception ex)
                                {
                                    WriteMessage("{0}同步失敗：" + ex.ToString());
                                }

                            }
                            break;
                        }
                    }

                    if (!boolFileExists)
                    {
                        boolIsChange = true;
                        try
                        {
                            //需要新增的文件
                            string strAimPath = item1.FullName.Replace(ExecType == "push" ? path1 : path2, ExecType == "push" ? path2 : path1);
                            WriteMessage("{0}新增" + strAimPath);
                            DirectoryInfo dirinfo = new DirectoryInfo(Path.GetDirectoryName(strAimPath));  //獲取路徑
                            if (!dirinfo.Exists)  //路徑不存在則創建
                            {
                                dirinfo.Create();
                            }

                            File.Copy(item1.FullName, strAimPath);
                        }
                        catch (Exception ex)
                        {
                            WriteMessage("{0}新增失敗：" + ex.ToString());
                        }
                    }
                }

                //刪除目標地址有而本地沒有的文件
                foreach (var item2 in ListFileInfo2)
                {
                    bool boolFileExists = false;
                    foreach (var item1 in ListFileInfo1)
                    {
                        if (item2.FullName.Replace(ExecType == "push" ? path2 : path1, "") == item1.FullName.Replace(ExecType == "push" ? path1 : path2, ""))
                        {
                            boolFileExists = true;
                        }
                    }

                    if (!boolFileExists)
                    {
                        boolIsChange = true;
                        try
                        {
                            //目標地址需要刪除的文件
                            WriteMessage("{0}刪除" + item2.FullName);
                            item2.Delete();
                        }
                        catch (Exception ex)
                        {
                            WriteMessage("{0}刪除失敗：" + ex.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteMessage("{0}錯誤：" + ex.ToString());
            }

            if (!boolIsChange)
            {
                WriteMessage("{0}沒有需要同步的文件");
            }

            WriteMessage("{0}結束\r\n");

            Thread.Sleep(1000);
            for (int i = 300; i > 0; i--)
            {
                //Console.WriteLine("{0}发布文件同步工具，请不要关闭", i);
                Thread.Sleep(1000);
            }

            Start();
        }

        public static DirectoryInfo[] GetChildDicsName(DirectoryInfo dir, int type)
        {
            FileInfo[] files = dir.GetFiles();
            DirectoryInfo[] childDirs = dir.GetDirectories();

            var fileArray = files.Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden));  //排除隱藏文件

            foreach (FileInfo file in fileArray)
            {
                bool boolCheckExceptFile = false;
                bool boolCheckExceptFileType = false;
                foreach (var item in ExceptFile.Split(';'))
                {
                    if (file.FullName.ToUpper().Contains(item))  //排除路徑
                    {
                        boolCheckExceptFile = true;
                    }
                }
                if (boolCheckExceptFile)
                {
                    continue;
                }

                foreach (var item in ExceptFileType.Split(';'))
                {
                    if (file.Extension.ToString().ToLower() == item)  //排除文件擴展名
                    {
                        boolCheckExceptFileType = true;
                    }
                }
                if (boolCheckExceptFileType)
                {
                    continue;
                }

                if (type == 1)
                {
                    ListFileInfo1.Add(file);
                }
                else
                {
                    ListFileInfo2.Add(file);
                }
            }

            foreach (DirectoryInfo dirChild in childDirs)
            {
                GetChildDicsName(dirChild, type);
            }

            return childDirs;
        }

        private static bool ExecCmd(string strPathTemp)
        {
            string strCmdText = "";
            strCmdText = string.Format("net use {0} /user:{1} {2}", strPathTemp, UserName, PassWord);

            try
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;//不显示程序窗口
                p.Start();//启动程序

                //向cmd窗口发送输入信息
                p.StandardInput.WriteLine(strCmdText + "&exit");

                p.StandardInput.AutoFlush = true;
                //p.StandardInput.WriteLine("exit");
                //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
                //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令



                //获取cmd窗口的输出信息
                string output = p.StandardOutput.ReadToEnd();

                //StreamReader reader = p.StandardOutput;
                //string line=reader.ReadLine();
                //while (!reader.EndOfStream)
                //{
                //    str += line + "  ";
                //    line = reader.ReadLine();
                //}

                p.WaitForExit();//等待程序执行完退出进程
                p.Close();

                Console.WriteLine(output);

                return true;
            }
            catch (Exception ex)
            {
                WriteMessage("{0}錯誤：" + ex.ToString());
                return false;
            }
        }

        public static void WriteMessage(string message)
        {
            string strText = string.Format(message, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " ");
            Console.WriteLine(strText);
            FileStream fs = File.Open(path + "log.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(strText);
            sw.Close();
            fs.Close();
        }

        private static string GetPath()
        {
            string str = Environment.CurrentDirectory;
            string[] sArray = str.Split(new char[1] { '\\' });
            string strConfigPath = "";
            for (int i = 0; i < sArray.Length; i++)
            {
                strConfigPath += sArray[i] + "\\";
            }

            return strConfigPath;
        }
    }
}
