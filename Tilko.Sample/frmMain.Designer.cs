namespace Tilko.Sample
{
	partial class frmMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this._txtAPI_KEY = new System.Windows.Forms.TextBox();
			this._txtRSA_PUBLIC_KEY = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this._txtRSA_PUBLIC_KEY);
			this.groupBox1.Controls.Add(this._txtAPI_KEY);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(944, 172);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "① API 키 입력";
			// 
			// _txtAPI_KEY
			// 
			this._txtAPI_KEY.Location = new System.Drawing.Point(119, 19);
			this._txtAPI_KEY.Name = "_txtAPI_KEY";
			this._txtAPI_KEY.Size = new System.Drawing.Size(477, 20);
			this._txtAPI_KEY.TabIndex = 0;
			// 
			// _txtRSA_PUBLIC_KEY
			// 
			this._txtRSA_PUBLIC_KEY.Location = new System.Drawing.Point(119, 45);
			this._txtRSA_PUBLIC_KEY.Multiline = true;
			this._txtRSA_PUBLIC_KEY.Name = "_txtRSA_PUBLIC_KEY";
			this._txtRSA_PUBLIC_KEY.Size = new System.Drawing.Size(477, 104);
			this._txtRSA_PUBLIC_KEY.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(75, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "API 키";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(101, 26);
			this.label2.TabIndex = 3;
			this.label2.Text = "서버용 RSA 공개키\r\n(BASE64)";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(944, 646);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "틸코 API 샘플";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox _txtRSA_PUBLIC_KEY;
		private System.Windows.Forms.TextBox _txtAPI_KEY;
	}
}

