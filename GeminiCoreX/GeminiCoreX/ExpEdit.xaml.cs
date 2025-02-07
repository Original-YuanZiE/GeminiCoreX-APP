using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

namespace GeminiCoreX
{
    /// <summary>
    /// ExpEdit.xaml 的交互逻辑
    /// </summary>
    public partial class ExpEdit : Page
    {
        bool isDllInjected;
        bool UserChangeSettings;
        public ExpEdit()
        {
            InitializeComponent();
            Check();
        }

        public async void Check()
        {
            UserChangeSettings = false;

            ExpBGSW.IsEnabled = false;
            AcrylicSW.IsEnabled = false;
            await Task.Run(async () =>
            {
                isDllInjected = IsDllInjected(@"ExplorerBgTool.dll");
            });
            ExpBGSW.IsOn = isDllInjected;
            ExpBGSW.IsEnabled = true;
            await Task.Run(async () =>
            {
                isDllInjected = IsDllInjected(@"ExplorerBlurMica.dll");
            });
            AcrylicSW.IsOn = isDllInjected;
            AcrylicSW.IsEnabled = true;
            

            UserChangeSettings = true;
        }
        private void AcrylicSW_Toggled(object sender, RoutedEventArgs e)
        {
            if (UserChangeSettings)
            {
                DllRegSvr(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\ExpBlur\\ExplorerBlurMica.dll", !IsDllInjected("ExplorerBlurMica.dll"));
            }
        }

        private void ExpBGSW_Toggled(object sender, RoutedEventArgs e)
        {
            if (UserChangeSettings)
            {
                DllRegSvr(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\ExpEditor\\ExplorerBgTool.dll", !IsDllInjected("ExplorerBgTool.dll"));
            }
        }
        public static bool IsDllInjected(string dllName)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";//启动cmd
            p.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//是否显示程序窗口
            p.Start();//启动程序
            p.StandardInput.WriteLine("reg query HKLM\\SOFTWARE\\Classes\\CLSID /s /f \""+ dllName +"\"");
            p.StandardInput.WriteLine("exit");
            string output = p.StandardOutput.ReadToEnd();//获取cmd窗口的输出信息，即便并无获取的需要也需要写这句话，不然程序会假死
            p.WaitForExit();//等待程序执行完
            p.Close();//退出进程

            string searchString = @"REG_SZ";

            bool containsSearchString = output.Contains(searchString);

            return containsSearchString;
        }

        public void DllRegSvr(string dllPath,bool isReg)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";//启动cmd
            p.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//是否显示程序窗口
            p.Start();//启动程序
            if (isReg)
            {
                p.StandardInput.WriteLine("regsvr32 /s \"" + dllPath + "\"");
            }
            else
            {
                p.StandardInput.WriteLine("regsvr32 /s /u \"" + dllPath + "\"");
            }
            p.StandardInput.WriteLine("exit");
            string output = p.StandardOutput.ReadToEnd();//获取cmd窗口的输出信息，即便并无获取的需要也需要写这句话，不然程序会假死
            p.WaitForExit();//等待程序执行完
            p.Close();//退出进程
        }
        private void RestartExplorer()
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";//启动cmd
            p.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//是否显示程序窗口
            p.Start();//启动程序
            p.StandardInput.WriteLine("taskkill /f /im explorer.exe");
            p.StandardInput.WriteLine("exit");
            string output = p.StandardOutput.ReadToEnd();//获取cmd窗口的输出信息，即便并无获取的需要也需要写这句话，不然程序会假死
            p.WaitForExit();//等待程序执行完
            p.Close();//退出进程
            Process.Start(Path.Combine(Environment.GetEnvironmentVariable("windir"), "explorer.exe"));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RestartExplorer();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase; //获取程序根目录：X:\xx\xx\
            Process p = new Process();
            Process.Start("explorer.exe", "\"" + str + "ExpEditor\\Image");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase; //获取程序根目录：X:\xx\xx\
            Process p = new Process();
            Process.Start("explorer.exe", "\"" + str + "ExpBlur\\config.ini");
        }
    }

}
