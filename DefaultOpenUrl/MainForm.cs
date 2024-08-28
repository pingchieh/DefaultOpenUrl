using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DefaultOpenUrl
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        string url = "";

        public MainForm(string url)
        {
            this.url = url;
            InitializeComponent();
            if (url != "")
            {
                btn_CopyUrl.Enabled = true;
                btn_OpenInEdge.Enabled = true;
                btn_OpenInEdgeInPrivate.Enabled = true;
                btn_SetOpenUrl.Enabled = false;
            }
        }

        private void btn_SetOpenUrl_Click(object sender, EventArgs e)
        {
            // 自动获取程序的名称和路径
            string appName = Assembly.GetExecutingAssembly().GetName().Name;
            string appPath = Assembly.GetExecutingAssembly().Location;

            // 注册 http 协议
            using (
                RegistryKey httpKey = Registry.CurrentUser.CreateSubKey(
                    $@"SOFTWARE\Classes\{appName}.http"
                )
            )
            {
                httpKey.SetValue("", "URL:HTTP");
                httpKey.SetValue("URL Protocol", "");
                using (RegistryKey commandKey = httpKey.CreateSubKey(@"shell\open\command"))
                {
                    commandKey.SetValue("", $"\"{appPath}\" \"%1\"");
                }
            }

            // 注册 https 协议
            using (
                RegistryKey httpsKey = Registry.CurrentUser.CreateSubKey(
                    $@"SOFTWARE\Classes\{appName}.https"
                )
            )
            {
                httpsKey.SetValue("", "URL:HTTPS");
                httpsKey.SetValue("URL Protocol", "");
                using (RegistryKey commandKey = httpsKey.CreateSubKey(@"shell\open\command"))
                {
                    commandKey.SetValue("", $"\"{appPath}\" \"%1\"");
                }
            }

            // 注册程序的功能和协议关联
            using (
                RegistryKey capabilitiesKey = Registry.CurrentUser.CreateSubKey(
                    $@"SOFTWARE\Classes\{appName}\Capabilities"
                )
            )
            {
                capabilitiesKey.SetValue("ApplicationName", appName);
                capabilitiesKey.SetValue("ApplicationIcon", $"{appPath},0");
                capabilitiesKey.SetValue("ApplicationDescription", $"{appName} - Custom Browser");

                using (RegistryKey urlAssocKey = capabilitiesKey.CreateSubKey("UrlAssociations"))
                {
                    urlAssocKey.SetValue("http", $"{appName}.http");
                    urlAssocKey.SetValue("https", $"{appName}.https");
                }
            }

            // 注册到 RegisteredApplications
            using (
                RegistryKey regAppsKey = Registry.CurrentUser.CreateSubKey(
                    @"SOFTWARE\RegisteredApplications"
                )
            )
            {
                regAppsKey.SetValue(appName, $@"SOFTWARE\Classes\{appName}\Capabilities");
            }
        }

        private void btn_CopyUrl_Click(object sender, EventArgs e)
        {
            if (url != "")
            {
                Clipboard.SetText(url);
                MessageBox.Show("URL已复制到剪贴板: " + url);
            }
            else
            {
                MessageBox.Show("URL为空");
            }
        }

        private void btn_OpenInEdge_Click(object sender, EventArgs e)
        {
            try
            {
                // 使用Microsoft Edge打开URL
                System.Diagnostics.Process.Start("msedge.exe", url);
                Console.WriteLine("已使用Edge打开URL。");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"打开Edge时出错: {ex.Message}");
            }
        }

        private void btn_OpenInEdgeInPrivate_Click(object sender, EventArgs e)
        {
            try
            {
                // 使用Microsoft Edge隐私模式打开URL
                System.Diagnostics.Process.Start("msedge.exe", $"--inprivate {url}");
                Console.WriteLine("已使用Edge隐私模式打开URL。");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"打开Edge时出错: {ex.Message}");
            }
        }
    }
}
