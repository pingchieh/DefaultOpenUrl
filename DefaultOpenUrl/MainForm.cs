using System;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DefaultOpenUrl
{
    public partial class MainForm : Form
    {
        private readonly string url;

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(string url)
            : this()
        {
            this.url = url;
            if (!string.IsNullOrEmpty(url))
            {
                btn_CopyUrl.Enabled = true;
                btn_OpenInEdge.Enabled = true;
                btn_OpenInEdgeInPrivate.Enabled = true;
                btn_SetOpenUrl.Enabled = false;
            }
        }

        /// <summary>
        /// 当按钮设置打开URL被点击时，注册应用程序的协议、能力和注册表项。
        /// </summary>
        /// <param name="sender">触发事件的对象。</param>
        /// <param name="e">包含事件数据的 EventArgs 对象。</param>
        private void Btn_SetOpenUrl_Click(object sender, EventArgs e)
        {
            string appName = Assembly.GetExecutingAssembly().GetName().Name;
            string appPath = Assembly.GetExecutingAssembly().Location;

            RegisterProtocol(appName, appPath, "http");
            RegisterProtocol(appName, appPath, "https");

            RegisterApplicationCapabilities(appName, appPath);
            RegisterApplicationInRegistry(appName);
        }

        /// <summary>
        /// 在注册表中为指定的应用程序注册特定协议。
        /// </summary>
        /// <param name="appName">应用程序名称。</param>
        /// <param name="appPath">应用程序路径。</param>
        /// <param name="protocol">要注册的协议。</param>
        private void RegisterProtocol(string appName, string appPath, string protocol)
        {
            using (
                RegistryKey protocolKey = Registry.CurrentUser.CreateSubKey(
                    $@"SOFTWARE\Classes\{appName}.{protocol}"
                )
            )
            {
                protocolKey.SetValue("", $"URL:{protocol.ToUpper()}");
                protocolKey.SetValue("URL Protocol", "");
                using (RegistryKey commandKey = protocolKey.CreateSubKey(@"shell\open\command"))
                {
                    commandKey.SetValue("", $"\"{appPath}\" \"%1\"");
                }
            }
        }

        /// <summary>
        /// 注册应用程序的功能，包括设置应用程序的名称、图标、描述和URL关联。
        /// </summary>
        /// <param name="appName">应用程序的名称。</param>
        /// <param name="appPath">应用程序的路径。</param>
        private void RegisterApplicationCapabilities(string appName, string appPath)
        {
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
        }

        /// <summary>
        /// 在注册表中注册应用程序。
        /// </summary>
        /// <param name="appName">应用程序的名称。</param>
        private void RegisterApplicationInRegistry(string appName)
        {
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
        /// 当点击复制URL按钮时触发的事件处理程序。
        /// 如果URL不为空，将其复制到剪贴板并询问是否关闭程序。
        /// 如果URL为空，则显示一条消息提示URL为空。
        /// </summary>
        /// <param name="sender">触发事件的对象。</param>
        /// <param name="e">包含事件数据的EventArgs对象。</param>
        private void Btn_CopyUrl_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(url))
            {
                Clipboard.SetText(url);
                if (
                    MessageBox.Show($"URL已复制到剪贴板,是否关闭程序?\n {url}", "提示", MessageBoxButtons.YesNo)
                    == DialogResult.Yes
                )
                {
                    Close();
                }
            }
            else
            {
                MessageBox.Show("URL为空");
            }
        }

        /// <summary>
        /// 处理按钮点击事件，询问用户是否在Microsoft Edge中打开URL。
        /// </summary>
        /// <param name="sender">触发事件的对象。</param>
        /// <param name="e">包含事件数据的EventArgs。</param>
        private void Btn_OpenInEdge_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show($"是否要在Microsoft Edge中打开URL?\n{url}", "提示", MessageBoxButtons.YesNo)
                == DialogResult.No
            )
            {
                return;
            }
            System.Diagnostics.Process.Start("msedge.exe", url);
            Close();
        }

        /// <summary>
        /// 当用户点击按钮时，在隐私模式下使用Edge浏览器打开指定的URL。
        /// </summary>
        /// <param name="sender">触发事件的对象。</param>
        /// <param name="e">包含事件数据的EventArgs对象。</param>
        private void Btn_OpenInEdgeInPrivate_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show($"是否要在隐私模式下打开URL?\n{url}", "提示", MessageBoxButtons.YesNo)
                == DialogResult.No
            )
            {
                return;
            }
            System.Diagnostics.Process.Start("msedge.exe", $"--inprivate {url}");
            Close();
        }
    }
}
