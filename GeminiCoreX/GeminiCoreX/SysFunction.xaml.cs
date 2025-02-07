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
//using System.Windows.Shapes;
using System.IO;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Win32;
using System.Xml.Linq;
using System.Xml;
using Windows.ApplicationModel;

namespace GeminiCoreX
{
    /// <summary>
    /// SysFunction.xaml 的交互逻辑
    /// </summary>
    public partial class SysFunction : Page
    {
        public bool UserChangeArrow;
        public bool UserChangeSecond;
        public SysFunction()
        {
            InitializeComponent();
            DetectArrowStatus();
            DetectSecondStatus();
        }


        private void DetectArrowStatus()
        {
            bool isShortcutArrowHidden = IsShortcutArrowHidden();
            UserChangeArrow = false;
            ShortCutSW.IsOn = !isShortcutArrowHidden;
            UserChangeArrow = true;
        }

        private bool IsShortcutArrowHidden()
        {
            // 检查注册表中是否设置了快捷方式箭头隐藏的值
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Shell Icons");
            if (key != null)
            {
                object value = key.GetValue("29");
                key.Close(); // 关闭注册表键
                if (value != null && value.ToString() == "%systemroot%\\System32\\imageres.dll,197")
                {
                    return false; // 箭头被隐藏
                }
            }
            return true; // 箭头可见或注册表键不存在
        }

        private void SetShortcutArrowVisibility(bool visible)
        {
            // 打开注册表项
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Shell Icons", true);
            if (key == null)
            {
                MessageBox.Show("无法访问注册表键。请以管理员身份运行程序。");
                return;
            }
            // 设置或删除注册表项
            if (visible)
            {
                key.DeleteValue("29", false);
            }
            else
            {
                key.SetValue("29", "%systemroot%\\System32\\imageres.dll,197");
            }
            key.Close();

            // 重启explorer进程以应用更改
            RestartExplorer();
        }

        private void RestartExplorer()
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";//启动cmd
            p.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = false;//是否显示程序窗口
            p.Start();//启动程序
            p.StandardInput.WriteLine("taskkill /f /im explorer.exe");
            p.StandardInput.WriteLine("exit");
            string output = p.StandardOutput.ReadToEnd();//获取cmd窗口的输出信息，即便并无获取的需要也需要写这句话，不然程序会假死
            p.WaitForExit();//等待程序执行完
            p.Close();//退出进程
            Process.Start(Path.Combine(Environment.GetEnvironmentVariable("windir"), "explorer.exe"));
        }

        private void ShortCutSW_Toggled(object sender, RoutedEventArgs e)
        {
            if (UserChangeArrow == true)
            {
                string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase; //获取程序根目录：X:\xx\xx\
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(str + "MsgSend.xml"); // 加载XML文件

                // 修改节点的值
                XmlNode node = xmlDoc.SelectSingleNode("/root/MsgTitle");
                node.InnerText = "警告";
                node = xmlDoc.SelectSingleNode("/root/MsgText");
                node.InnerText = "修改此项设置将会重启文件资源管理器，这会导致你的文件夹窗口全部关闭并终止复制任务，并且文件资源管理器重启后一段时间任务栏才会重新弹出\n继续吗？";
                node = xmlDoc.SelectSingleNode("/root/Value");
                node.InnerText = "false";
                // 保存修改后的XML文件
                xmlDoc.Save("MsgSend.xml");
                MsgWindow msgWindow = new MsgWindow();
                msgWindow.ShowDialog();

                xmlDoc.Load(str + "MsgSend.xml"); // 加载XML文件
                node = xmlDoc.SelectSingleNode("/root/Value");
                if (node.InnerText == "true")
                {
                    // 读取当前的快捷方式小箭头显示状态
                    bool isShortcutArrowHidden = IsShortcutArrowHidden();

                    // 切换状态
                    SetShortcutArrowVisibility(!isShortcutArrowHidden);

                    
                }
                // 更新按钮以反映当前状态
                DetectArrowStatus();
            }
        }

        private void TabletTBSW_Click(object sender, RoutedEventArgs e)
        {
            // 打开注册表项
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\PriorityControl", true);
            key.SetValue("ConvertibleSlateMode", "1");
            key.SetValue("ConvertibleSlateMode", "0");
            key.Close();
            key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer", true);
            key.SetValue("TabletPostureTaskbar", "1", Microsoft.Win32.RegistryValueKind.DWord);
            key.Close();
            key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\PriorityControl", true);
            key.SetValue("ConvertibleSlateMode", "1");
            key.Close();
        }

        private async void ActiveWinSW_Click(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";//启动cmd
            p.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//是否显示程序窗口
            p.Start();//启动程序
            p.StandardInput.WriteLine("slmgr /ipk VK7JG-NPHTM-C97JM-9MPGT-3V66T");
            await Task.Delay(1000);
            //p.StandardInput.WriteLine("slmgr /skms kms.03k.org");
            //await Task.Delay(1000);
            p.StandardInput.WriteLine("copy /y \"" + System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "GenuineTicket\\Professional.xml\" \"%SystemDrive%\\ProgramData\\Microsoft\\Windows\\ClipSVC\\GenuineTicket\"");
            p.StandardInput.WriteLine("clipup -v -o");
            p.StandardInput.WriteLine("slmgr /ato");
            await Task.Delay(1000);
            p.StandardInput.WriteLine("exit");
            string output = p.StandardOutput.ReadToEnd();//获取cmd窗口的输出信息，即便并无获取的需要也需要写这句话，不然程序会假死
            p.WaitForExit();//等待程序执行完
            p.Close();//退出进程
        }

        private void TaskBarSecondSW_Toggled(object sender, RoutedEventArgs e)
        {
            if (UserChangeSecond == true)
            {
                bool isSecondShow = IsSecondShow();
                if (isSecondShow == true)
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
                    key.SetValue("ShowSecondsInSystemClock", "0", Microsoft.Win32.RegistryValueKind.DWord);
                    key.Close();
                }
                else
                {
                    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", true);
                    key.SetValue("ShowSecondsInSystemClock", "1", Microsoft.Win32.RegistryValueKind.DWord);
                    key.Close();
                }
                DetectSecondStatus();
            }
        }

        private void DetectSecondStatus()
        {
            bool isSecondShow = IsSecondShow();
            UserChangeSecond = false;
            TaskBarSecondSW.IsOn = isSecondShow;
            UserChangeSecond = true;
        }
        private bool IsSecondShow()
        {
            // 检查注册表中是否设置了快捷方式箭头隐藏的值
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced");
            if (key != null)
            {
                object value = key.GetValue("ShowSecondsInSystemClock");
                key.Close(); // 关闭注册表键
                if (value != null && value.ToString() == "1")
                {
                    return true; // 秒钟显示
                }
            }
            return false; // 秒钟不显示或注册表键不存在
        }
    }
}
