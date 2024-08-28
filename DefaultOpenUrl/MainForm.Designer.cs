namespace DefaultOpenUrl
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btn_SetOpenUrl = new System.Windows.Forms.Button();
            this.btn_CopyUrl = new System.Windows.Forms.Button();
            this.btn_OpenInEdge = new System.Windows.Forms.Button();
            this.btn_OpenInEdgeInPrivate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_SetOpenUrl
            // 
            this.btn_SetOpenUrl.Location = new System.Drawing.Point(26, 45);
            this.btn_SetOpenUrl.Name = "btn_SetOpenUrl";
            this.btn_SetOpenUrl.Size = new System.Drawing.Size(92, 37);
            this.btn_SetOpenUrl.TabIndex = 0;
            this.btn_SetOpenUrl.Text = "注册程序";
            this.btn_SetOpenUrl.UseVisualStyleBackColor = true;
            this.btn_SetOpenUrl.Click += new System.EventHandler(this.btn_SetOpenUrl_Click);
            // 
            // btn_CopyUrl
            // 
            this.btn_CopyUrl.Enabled = false;
            this.btn_CopyUrl.Location = new System.Drawing.Point(124, 45);
            this.btn_CopyUrl.Name = "btn_CopyUrl";
            this.btn_CopyUrl.Size = new System.Drawing.Size(92, 37);
            this.btn_CopyUrl.TabIndex = 1;
            this.btn_CopyUrl.Text = "复制链接";
            this.btn_CopyUrl.UseVisualStyleBackColor = true;
            this.btn_CopyUrl.Click += new System.EventHandler(this.btn_CopyUrl_Click);
            // 
            // btn_OpenInEdge
            // 
            this.btn_OpenInEdge.Enabled = false;
            this.btn_OpenInEdge.Location = new System.Drawing.Point(222, 45);
            this.btn_OpenInEdge.Name = "btn_OpenInEdge";
            this.btn_OpenInEdge.Size = new System.Drawing.Size(92, 37);
            this.btn_OpenInEdge.TabIndex = 2;
            this.btn_OpenInEdge.Text = "使用Edge打开";
            this.btn_OpenInEdge.UseVisualStyleBackColor = true;
            this.btn_OpenInEdge.Click += new System.EventHandler(this.btn_OpenInEdge_Click);
            // 
            // btn_OpenInEdgeInPrivate
            // 
            this.btn_OpenInEdgeInPrivate.Enabled = false;
            this.btn_OpenInEdgeInPrivate.Location = new System.Drawing.Point(320, 45);
            this.btn_OpenInEdgeInPrivate.Name = "btn_OpenInEdgeInPrivate";
            this.btn_OpenInEdgeInPrivate.Size = new System.Drawing.Size(92, 37);
            this.btn_OpenInEdgeInPrivate.TabIndex = 3;
            this.btn_OpenInEdgeInPrivate.Text = "Edge无痕模式";
            this.btn_OpenInEdgeInPrivate.UseVisualStyleBackColor = true;
            this.btn_OpenInEdgeInPrivate.Click += new System.EventHandler(this.btn_OpenInEdgeInPrivate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 120);
            this.Controls.Add(this.btn_OpenInEdgeInPrivate);
            this.Controls.Add(this.btn_OpenInEdge);
            this.Controls.Add(this.btn_CopyUrl);
            this.Controls.Add(this.btn_SetOpenUrl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DefaultOpenUrl";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_SetOpenUrl;
        private System.Windows.Forms.Button btn_CopyUrl;
        private System.Windows.Forms.Button btn_OpenInEdge;
        private System.Windows.Forms.Button btn_OpenInEdgeInPrivate;
    }
}

