using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using Microsoft.Toolkit.Uwp.Notifications;

namespace TranslucentSMAliveKeeper
{
    public partial class Service1 : ServiceBase
    {
        WqlEventQuery query;
        ManagementEventWatcher watcher;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            query = new WqlEventQuery("SELECT * FROM __InstanceCreationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_Process' AND TargetInstance.Name = 'explorer.exe'");
            watcher = new ManagementEventWatcher(query);
            watcher.EventArrived += new EventArrivedEventHandler(OnExplorerRestart);
            watcher.Start();
        }

        protected override void OnStop()
        {
            watcher.Stop();
            
        }

        private async static void OnExplorerRestart(object sender, EventArrivedEventArgs e)
        {
            await Task.Delay(2000);
            Process p = new Process();
            p.StartInfo.FileName = Environment.GetEnvironmentVariable("systemdrive") + @"\GeminiCore\GeminiCoreX\Main\TranslucentSM\start.exe";
            p.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//是否显示程序窗口
            p.Start();//启动程序
            string output = p.StandardOutput.ReadToEnd();//获取cmd窗口的输出信息，即便并无获取的需要也需要写这句话，不然程序会假死
            p.WaitForExit();//等待程序执行完
            p.Close();//退出进程
        }
    }
}
