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
    /// GPUList.xaml 的交互逻辑
    /// </summary>
    public partial class GPUList : Window
    {
        public GPUList()
        {
            InitializeComponent();
            string str = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase; //获取程序根目录：X:\xx\xx\
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(str + "PCInfo.xml"); // 加载XML文件

            XmlNode node = xmlDoc.SelectSingleNode("/root/GPU");
            LGPUInfo.Content = node.InnerText;
        }
    }
}
