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
            : this()
        {
            this.url = url;
            if (url != "")
            {
                btn_CopyUrl.Enabled = true;
                btn_OpenInEdge.Enabled = true;
                btn_OpenInEdgeInPrivate.Enabled = true;
                btn_SetOpenUrl.Enabled = false;
            }
        }

        /// <summary>
        /// 处理按钮点击事件，注册应用程序的 http 和 https 协议，并将其与应用程序功能关联。
        /// </summary>
        /// <param name="sender">触发事件的对象</param>
        /// <param name="e">包含事件数据的 EventArgs 对象</param>
        private void Btn_SetOpenUrl_Click(object sender, EventArgs e)
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

        /// <summary>
        /// 当点击"CopyUrl"按钮时处理的事件。如果URL不为空，将其复制到剪贴板并显示一条消息。如果URL为空，则显示一条错误消息。
        /// </summary>
        /// <param name="sender">触发事件的对象。</param>
        /// <param name="e">包含事件数据的EventArgs对象。</param>
        private void Btn_CopyUrl_Click(object sender, EventArgs e)
        {
            if (url != "")
            {
                Clipboard.SetText(url);
                if (
                    MessageBox.Show("URL已复制到剪贴板,是否关闭程序?\n " + url, "提示", MessageBoxButtons.YesNo)
                    == DialogResult.Yes
                )
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("URL为空");
            }
        }

        /// <summary>
        /// 处理按钮点击事件，使用Microsoft Edge打开指定的URL。
        /// </summary>
        /// <param name="sender">触发事件的对象。</param>
        /// <param name="e">包含事件数据的EventArgs对象。</param>
        private void Btn_OpenInEdge_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show("是否要在Microsoft Edge中打开URL?\n" + url, "提示", MessageBoxButtons.YesNo)
                == DialogResult.No
            )
            {
                return;
            }
            System.Diagnostics.Process.Start("msedge.exe", url);
            this.Close();
        }

        /// <summary>
        /// 在Microsoft Edge的隐私模式下打开URL的事件处理程序。
        /// </summary>
        /// <param name="sender">触发事件的对象。</param>
        /// <param name="e">包含事件数据的EventArgs对象。</param>
        private void Btn_OpenInEdgeInPrivate_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show("是否要在隐私模式下打开URL?\n" + url, "提示", MessageBoxButtons.YesNo)
                == DialogResult.No
            )
            {
                return;
            }
            System.Diagnostics.Process.Start("msedge.exe", $"--inprivate {url}");
            this.Close();
        }
    }
}
