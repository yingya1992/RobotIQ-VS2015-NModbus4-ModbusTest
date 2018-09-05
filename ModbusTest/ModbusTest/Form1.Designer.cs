namespace ModbusTest
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.StatusTB = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.ForReqTB = new System.Windows.Forms.TextBox();
            this.SpeReqTB = new System.Windows.Forms.TextBox();
            this.PosReqTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "状态查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // StatusTB
            // 
            this.StatusTB.Location = new System.Drawing.Point(171, 32);
            this.StatusTB.Multiline = true;
            this.StatusTB.Name = "StatusTB";
            this.StatusTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.StatusTB.Size = new System.Drawing.Size(324, 246);
            this.StatusTB.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(41, 143);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "运行手爪";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ForReqTB
            // 
            this.ForReqTB.Location = new System.Drawing.Point(50, 106);
            this.ForReqTB.Name = "ForReqTB";
            this.ForReqTB.Size = new System.Drawing.Size(100, 21);
            this.ForReqTB.TabIndex = 3;
            // 
            // SpeReqTB
            // 
            this.SpeReqTB.Location = new System.Drawing.Point(50, 79);
            this.SpeReqTB.Name = "SpeReqTB";
            this.SpeReqTB.Size = new System.Drawing.Size(100, 21);
            this.SpeReqTB.TabIndex = 3;
            // 
            // PosReqTB
            // 
            this.PosReqTB.Location = new System.Drawing.Point(50, 52);
            this.PosReqTB.Name = "PosReqTB";
            this.PosReqTB.Size = new System.Drawing.Size(100, 21);
            this.PosReqTB.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "PosReq";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "SpeReq";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "ForReq";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 386);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PosReqTB);
            this.Controls.Add(this.SpeReqTB);
            this.Controls.Add(this.ForReqTB);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.StatusTB);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox StatusTB;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox ForReqTB;
        private System.Windows.Forms.TextBox SpeReqTB;
        private System.Windows.Forms.TextBox PosReqTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

