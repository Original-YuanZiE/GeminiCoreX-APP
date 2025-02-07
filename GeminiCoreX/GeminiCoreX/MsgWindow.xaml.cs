using Microsoft.VisualBasic;
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
using System.Windows.Shapes;
using System.Xml;

namespace GeminiCoreX
{
    /// <summary>
    /// MsgWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MsgWindow : Window
    {
        public string Title;
        public string Text;
        public MsgWindow()
        {
            InitializeComponent();
            string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase; //获取程序根目录：X:\xx\xx\
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(str + "MsgSend.xml"); // 加载XML文件

            XmlNode node = xmlDoc.SelectSingleNode("/root/MsgTitle");
            MTitle.Content = node.InnerText;
            node = xmlDoc.SelectSingleNode("/root/MsgText");
            MText.Text = node.InnerText;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase; //获取程序根目录：X:\xx\xx\
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(str + "MsgSend.xml"); // 加载XML文件

            // 修改节点的值
            XmlNode node = xmlDoc.SelectSingleNode("/root/Value");
            node.InnerText = "false";
            // 保存修改后的XML文件
            xmlDoc.Save("MsgSend.xml");
            //MsgWindow msgWindow = new MsgWindow();
            //msgWindow.Close();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase; //获取程序根目录：X:\xx\xx\
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(str + "MsgSend.xml"); // 加载XML文件

            // 修改节点的值
            XmlNode node = xmlDoc.SelectSingleNode("/root/Value");
            node.InnerText = "true";
            // 保存修改后的XML文件
            xmlDoc.Save("MsgSend.xml");
            //MsgWindow msgWindow = new MsgWindow();
            //msgWindow.Close();
            this.Close();
        }
    }
}
