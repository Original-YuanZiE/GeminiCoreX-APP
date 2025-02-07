using System;
using System.Collections.Generic;
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
using System.Diagnostics;
namespace GeminiCoreX
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : Page
    {
        public int volChange;
        public HomePage()
        {
            
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";//启动cmd
            p.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//是否显示程序窗口
            p.Start();//启动程序
            p.StandardInput.WriteLine("start ms-settings:");
            p.StandardInput.WriteLine("exit");
            string output = p.StandardOutput.ReadToEnd();//获取cmd窗口的输出信息，即便并无获取的需要也需要写这句话，不然程序会假死
            p.WaitForExit();//等待程序执行完
            p.Close();//退出进程
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";//启动cmd
            p.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//是否显示程序窗口
            p.Start();//启动程序
            p.StandardInput.WriteLine("start ms-settings:network-status");
            p.StandardInput.WriteLine("exit");
            string output = p.StandardOutput.ReadToEnd();//获取cmd窗口的输出信息，即便并无获取的需要也需要写这句话，不然程序会假死
            p.WaitForExit();//等待程序执行完
            p.Close();//退出进程
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            WebView webView = new WebView();
            webView.Show();
        }
    }
}
