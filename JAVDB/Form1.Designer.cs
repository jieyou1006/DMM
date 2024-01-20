namespace JAVDB
{
    partial class Form1
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
            if (disposing && (components != null))
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
            this.but_ok1 = new System.Windows.Forms.Button();
            this.but_ok2 = new System.Windows.Forms.Button();
            this.textBoxCode = new System.Windows.Forms.TextBox();
            this.textBoxUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSearchUrl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxResult = new System.Windows.Forms.ListBox();
            this.lbl_Count = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // but_ok1
            // 
            this.but_ok1.Location = new System.Drawing.Point(878, 116);
            this.but_ok1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.but_ok1.Name = "but_ok1";
            this.but_ok1.Size = new System.Drawing.Size(120, 45);
            this.but_ok1.TabIndex = 0;
            this.but_ok1.Text = "OK";
            this.but_ok1.UseVisualStyleBackColor = true;
            this.but_ok1.Click += new System.EventHandler(this.but_ok1_Click);
            // 
            // but_ok2
            // 
            this.but_ok2.Location = new System.Drawing.Point(878, 282);
            this.but_ok2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.but_ok2.Name = "but_ok2";
            this.but_ok2.Size = new System.Drawing.Size(120, 45);
            this.but_ok2.TabIndex = 1;
            this.but_ok2.Text = "OK";
            this.but_ok2.UseVisualStyleBackColor = true;
            this.but_ok2.Click += new System.EventHandler(this.but_ok2_Click);
            // 
            // textBoxCode
            // 
            this.textBoxCode.Location = new System.Drawing.Point(170, 74);
            this.textBoxCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxCode.Multiline = true;
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.Size = new System.Drawing.Size(595, 206);
            this.textBoxCode.TabIndex = 2;
            // 
            // textBoxUrl
            // 
            this.textBoxUrl.Location = new System.Drawing.Point(170, 291);
            this.textBoxUrl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxUrl.Name = "textBoxUrl";
            this.textBoxUrl.Size = new System.Drawing.Size(595, 28);
            this.textBoxUrl.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 129);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "从CODE开始：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 296);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "从影片主页开始：";
            // 
            // textBoxSearchUrl
            // 
            this.textBoxSearchUrl.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.textBoxSearchUrl.Location = new System.Drawing.Point(170, 33);
            this.textBoxSearchUrl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxSearchUrl.Name = "textBoxSearchUrl";
            this.textBoxSearchUrl.Size = new System.Drawing.Size(595, 28);
            this.textBoxSearchUrl.TabIndex = 6;
            this.textBoxSearchUrl.Text = "https://avmoo.site/cn/search/";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "CODE搜索URL：";
            // 
            // listBoxResult
            // 
            this.listBoxResult.FormattingEnabled = true;
            this.listBoxResult.ItemHeight = 18;
            this.listBoxResult.Location = new System.Drawing.Point(18, 346);
            this.listBoxResult.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listBoxResult.Name = "listBoxResult";
            this.listBoxResult.Size = new System.Drawing.Size(1147, 994);
            this.listBoxResult.TabIndex = 8;
            // 
            // lbl_Count
            // 
            this.lbl_Count.AutoSize = true;
            this.lbl_Count.Location = new System.Drawing.Point(786, 138);
            this.lbl_Count.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Count.Name = "lbl_Count";
            this.lbl_Count.Size = new System.Drawing.Size(35, 18);
            this.lbl_Count.TabIndex = 9;
            this.lbl_Count.Text = "123";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1185, 1347);
            this.Controls.Add(this.lbl_Count);
            this.Controls.Add(this.listBoxResult);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxSearchUrl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxUrl);
            this.Controls.Add(this.textBoxCode);
            this.Controls.Add(this.but_ok2);
            this.Controls.Add(this.but_ok1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "GetFromJAVDB";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button but_ok1;
        private System.Windows.Forms.Button but_ok2;
        private System.Windows.Forms.TextBox textBoxCode;
        private System.Windows.Forms.TextBox textBoxUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSearchUrl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxResult;
        private System.Windows.Forms.Label lbl_Count;
    }
}

