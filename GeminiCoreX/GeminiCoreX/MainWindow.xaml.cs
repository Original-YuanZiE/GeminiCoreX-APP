using Microsoft.Toolkit.Uwp.Notifications;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using System.Xml;

namespace GeminiCoreX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            HomePage homePage = new HomePage();
            Frame1.Navigate(homePage);
            LVI1.IsSelected = true;
            try
            {
                string str = Path.Combine(Environment.GetEnvironmentVariable("systemdrive")); //获取程序根目录：X:\xx\xx\
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(str + @"\TrustedSysLicense.xml"); // 加载XML文件

                XmlNode node = xmlDoc.SelectSingleNode("/root/OEM");
                string OEM = node.InnerText;
                if (OEM == "General OEM Auth")
                {
                    new ToastContentBuilder()
                    .AddArgument("action", "viewConversation")
                    .AddArgument("conversationId", 9813)
                    .AddText("系统验证失败")
                    .AddText("正在使用通用密钥启动 GeminiCoreX，如导致系统损坏，后果自负；")
                    .Show();
                    this.Title = "运行在未经验证的系统，不保证安全性！";
                    
                }
                else
                {
                    
                }
            }
            catch
            {
            }
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage();
            Frame1.Navigate(homePage);
        }

        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            About about = new About();
            Frame1.Navigate(about);
        }

        private void ListViewItem_Selected_2(object sender, RoutedEventArgs e)
        {
            SysFunction sysFunction = new SysFunction();
            Frame1.Navigate(sysFunction);
        }

        private void ListViewItem_Selected_3(object sender, RoutedEventArgs e)
        {
            Theme theme = new Theme();
            Frame1.Navigate(theme);
        }
    }
}