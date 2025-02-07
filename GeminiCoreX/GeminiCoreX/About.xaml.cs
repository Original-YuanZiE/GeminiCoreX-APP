using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using Windows.Devices.Radios;
using System.Management;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Xml;

namespace GeminiCoreX
{
    /// <summary>
    /// About.xaml 的交互逻辑
    /// </summary>
    public partial class About : Page
    {
        public About()
        {
            InitializeComponent();
            SetIMG();
            string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase; //获取程序根目录：X:\xx\xx\
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(str + "PCInfo.xml"); // 加载XML文件

            XmlNode node = xmlDoc.SelectSingleNode("/root/CPU");
            LCPUInfo.Content = node.InnerText;
            node = xmlDoc.SelectSingleNode("/root/WinVer");
            LWinVer.Content = node.InnerText;
            node = xmlDoc.SelectSingleNode("/root/RAM");
            LRAMInfo.Content = node.InnerText + " RAM";
            node = xmlDoc.SelectSingleNode("/root/PCName");
            LPCName.Content = node.InnerText;

            try
            {
                xmlDoc = new XmlDocument();
                xmlDoc.Load(Environment.GetEnvironmentVariable("SystemDrive") + "\\GeminiCore\\GeminiCoreX\\Version.xml"); // 加载XML文件
                node = xmlDoc.SelectSingleNode("/root/OSVer");
                LYZEVer.Content = node.InnerText;
            } catch 
            {
                LYZEVer.Content = "未检测到 GeminiCore 组件,读取失败";
                new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText("发生非致命错误")
                .AddText("未检测到 GeminiCore 组件!")
                .Show();
            }
        }
/*
        public string CPUInfo;
        public string winV;
        public string RAMInfo;
        public string GPUInfo;
        public async void Init()
        {
            await Task.Run(() =>
            {
                //获取CPU信息
                string CPUName = "";
                ManagementObjectSearcher mos = new ManagementObjectSearcher("Select * from Win32_Processor");//Win32_Processor  CPU处理器
                foreach (ManagementObject mo in mos.Get())
                {
                    CPUName = mo["Name"].ToString();
                }
                mos.Dispose();
                CPUInfo = CPUName;

                //获取Windows版本
                winV = Environment.OSVersion.VersionString;

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

                //获取GPU信息
                ManagementObjectSearcher searcher1 = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
                foreach (ManagementObject item in searcher1.Get())
                {
                    GPUInfo = item["Name"].ToString();
                }
            });
        }
*/

        //获取桌面壁纸路径
        public class WallpaperHelper
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            private static extern int SystemParametersInfo(int uAction, int uParam, StringBuilder lpvParam, int fuWinIni);

            private const int SPI_GETDESKWALLPAPER = 0x0073;

            public static string GetDesktopWallpaperPath()
            {
                StringBuilder wallpaperPath = new StringBuilder(260);
                if (SystemParametersInfo(SPI_GETDESKWALLPAPER, wallpaperPath.Capacity, wallpaperPath, 0) > 0)
                {
                    return wallpaperPath.ToString();

                }
                else
                {
                    throw new Exception("无法获取桌面壁纸路径");
                }
            }
        }

        public void SetIMG()
        {
            try
            {
                string wallpaperPath = WallpaperHelper.GetDesktopWallpaperPath();
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(wallpaperPath);
                bitmapImage.EndInit();
                WIMG.Source = bitmapImage;
            }
            catch (Exception ex)
            {
                new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText("发生非致命错误")
                .AddText(ex.Message)
                .Show();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GPUList gPUList = new GPUList();
            gPUList.Show();
        }
    }
    
}
