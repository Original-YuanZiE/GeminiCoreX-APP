using System;
using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Toolkit.Uwp.Notifications;

namespace GeminiCoreX
{
    /// <summary>
    /// Splash.xaml 的交互逻辑
    /// </summary>
    public partial class Splash : Window
    {
        public Splash()
        {
            InitializeComponent();
            try
            {
                string str = Path.Combine(Environment.GetEnvironmentVariable("systemdrive")); //获取程序根目录：X:\xx\xx\
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(str + @"\TrustedSysLicense.xml"); // 加载XML文件

                XmlNode node = xmlDoc.SelectSingleNode("/root/OEM");
                string OEM = node.InnerText;
                node = xmlDoc.SelectSingleNode("/root/Key");
                string Key = node.InnerText;
                string OEMH = GetSHA256Hash(OEM + @"Original_License_16384");
                if (OEMH == Key)
                {
                    Init();
                }
                else
                {
                    new ToastContentBuilder()
                    .AddArgument("action", "viewConversation")
                    .AddArgument("conversationId", 9813)
                    .AddText("GeminiCoreX 拒绝启动")
                    .AddText("为了保证系统安全，默认状态下，GeminiCoreX 拒绝在未经验证的系统中启动；如需帮助，请联系元子鹅")
                    .Show();
                    this.Close();
                }
            } catch
            {
                new ToastContentBuilder()
                    .AddArgument("action", "viewConversation")
                    .AddArgument("conversationId", 9813)
                    .AddText("GeminiCoreX 拒绝启动")
                    .AddText("为了保证系统安全，默认状态下，GeminiCoreX 拒绝在未经验证的系统中启动；如需帮助，请联系元子鹅")
                    .Show();
                this.Close();
            }
        }
        public string CPUInfo;
        public string winV;
        public string RAMInfo;
        public string GPUInfo;
        public string PCName;
        public async void Init()
        {
            await Task.Run(async () =>
            {
                await Task.Delay(800);
                //获取CPU信息
                string CPUName = "";
                ManagementObjectSearcher mos = new ManagementObjectSearcher("Select * from Win32_Processor");//Win32_Processor  CPU处理器
                foreach (ManagementObject mo in mos.Get())
                {
                    CPUName = mo["Name"].ToString();
                }
                mos.Dispose();
                CPUInfo = CPUName;
            });
                SplashLog.Content = "已完成 CPU 名称获取";

            await Task.Run(async () =>
            {
                //获取Windows版本
                winV = Environment.OSVersion.VersionString;
            });
                SplashLog.Content = "已完成 Windows 版本获取";

            await Task.Run(async () =>
            {
                //获取RAM
                const int GB = 1024 * 1024 * 1024;
                long totalMemory = 0;

                using (var searcher = new ManagementObjectSearcher("SELECT Capacity FROM Win32_PhysicalMemory"))
                {
                    foreach (var memObject in searcher.Get())
                    {
                        totalMemory += Convert.ToInt64(memObject["Capacity"]);
                    }
                }
                long RAM = totalMemory / GB;
                RAMInfo = RAM.ToString() + "GB";
            });
                SplashLog.Content = "已完成 RAM 大小获取";

            await Task.Run(async () =>
            {
                //获取GPU信息
                ManagementObjectSearcher searcher1 = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
                foreach (ManagementObject item in searcher1.Get())
                {
                    GPUInfo += item["Name"].ToString() + "\n";
                }
            });
            SplashLog.Content = "已完成 GPU 名称获取";

            await Task.Run(async () =>
            {
                PCName = Environment.GetEnvironmentVariable("computername");
            });
            SplashLog.Content = "已完成 计算机名称获取";

            SplashLog.Content = "请稍等";
            string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase; //获取程序根目录：X:\xx\xx\
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(str + "PCInfo.xml"); // 加载XML文件
            
            // 修改节点的值
            XmlNode node = xmlDoc.SelectSingleNode("/root/CPU");
            node.InnerText = CPUInfo;
            node = xmlDoc.SelectSingleNode("/root/WinVer");
            node.InnerText = winV;
            node = xmlDoc.SelectSingleNode("/root/RAM");
            node.InnerText = RAMInfo;
            node = xmlDoc.SelectSingleNode("/root/GPU");
            node.InnerText = GPUInfo;
            node = xmlDoc.SelectSingleNode("/root/PCName");
            node.InnerText = PCName;
            // 保存修改后的XML文件
            xmlDoc.Save("PCInfo.xml");


            await Task.Delay(500);
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
            
        }

        static string GetSHA256Hash(string input)
        {
            // 使用SHA256加密算法
            using (SHA256 sha256 = SHA256.Create())
            {
                // 将字符串转换为字节数组
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                // 计算哈希值
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                // 将哈希值转换为十六进制字符串
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }


}
