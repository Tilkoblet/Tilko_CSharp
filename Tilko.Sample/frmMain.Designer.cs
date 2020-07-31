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
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this._txtAPI_KEY = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this._txtCertPassword = new System.Windows.Forms.TextBox();
			this._txtIdentityNumber = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this._btnFind = new System.Windows.Forms.Button();
			this._txtCertPath = new System.Windows.Forms.TextBox();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label8 = new System.Windows.Forms.Label();
			this._cmbEndPoint = new System.Windows.Forms.ComboBox();
			this._btnOK = new System.Windows.Forms.Button();
			this._btnCancel = new System.Windows.Forms.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this._txtResult = new System.Windows.Forms.TextBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this._txtAPI_KEY);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(733, 69);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "① API 키 입력";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.Color.Red;
			this.label3.Location = new System.Drawing.Point(116, 42);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(437, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "홈페이지(http://tilko.net) > 내정보 > API KEY 목록에서 API 키를 확인하고 입력해 주세요.";
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
			// _txtAPI_KEY
			// 
			this._txtAPI_KEY.Location = new System.Drawing.Point(119, 19);
			this._txtAPI_KEY.Name = "_txtAPI_KEY";
			this._txtAPI_KEY.Size = new System.Drawing.Size(600, 20);
			this._txtAPI_KEY.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this._txtCertPassword);
			this.groupBox2.Controls.Add(this._txtIdentityNumber);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this._btnFind);
			this.groupBox2.Controls.Add(this._txtCertPath);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox2.Location = new System.Drawing.Point(0, 69);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(733, 125);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "② 공인인증서 정보 입력";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(26, 88);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(87, 13);
			this.label7.TabIndex = 9;
			this.label7.Text = "인증서 비밀번호";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(40, 62);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(73, 13);
			this.label6.TabIndex = 8;
			this.label6.Text = "주민등록번호";
			// 
			// _txtCertPassword
			// 
			this._txtCertPassword.Location = new System.Drawing.Point(119, 85);
			this._txtCertPassword.Name = "_txtCertPassword";
			this._txtCertPassword.PasswordChar = '*';
			this._txtCertPassword.Size = new System.Drawing.Size(228, 20);
			this._txtCertPassword.TabIndex = 7;
			// 
			// _txtIdentityNumber
			// 
			this._txtIdentityNumber.Location = new System.Drawing.Point(119, 59);
			this._txtIdentityNumber.MaxLength = 13;
			this._txtIdentityNumber.Name = "_txtIdentityNumber";
			this._txtIdentityNumber.PasswordChar = '*';
			this._txtIdentityNumber.Size = new System.Drawing.Size(228, 20);
			this._txtIdentityNumber.TabIndex = 6;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.ForeColor = System.Drawing.Color.Red;
			this.label5.Location = new System.Drawing.Point(116, 43);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(540, 13);
			this.label5.TabIndex = 5;
			this.label5.Text = "공인인증서는 대부분 C:\\Users\\[사용자명]\\AppData\\LocalLow\\NPKI\\yessign\\USER 폴더에 있습니다.";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(48, 22);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(65, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "인증서 경로";
			// 
			// _btnFind
			// 
			this._btnFind.Location = new System.Drawing.Point(644, 17);
			this._btnFind.Name = "_btnFind";
			this._btnFind.Size = new System.Drawing.Size(75, 23);
			this._btnFind.TabIndex = 1;
			this._btnFind.Text = "찾   기...";
			this._btnFind.UseVisualStyleBackColor = true;
			this._btnFind.Click += new System.EventHandler(this._btnFind_Click);
			// 
			// _txtCertPath
			// 
			this._txtCertPath.Location = new System.Drawing.Point(119, 19);
			this._txtCertPath.Name = "_txtCertPath";
			this._txtCertPath.ReadOnly = true;
			this._txtCertPath.Size = new System.Drawing.Size(519, 20);
			this._txtCertPath.TabIndex = 0;
			this._txtCertPath.DoubleClick += new System.EventHandler(this._txtCertPath_DoubleClick);
			// 
			// folderBrowserDialog1
			// 
			this.folderBrowserDialog1.Description = "인증서 폴더 찾기";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Controls.Add(this._cmbEndPoint);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox3.Location = new System.Drawing.Point(0, 194);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(733, 61);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "③ EndPoint 선택";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(63, 22);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(50, 13);
			this.label8.TabIndex = 3;
			this.label8.Text = "EndPoint";
			// 
			// _cmbEndPoint
			// 
			this._cmbEndPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._cmbEndPoint.FormattingEnabled = true;
			this._cmbEndPoint.Location = new System.Drawing.Point(119, 19);
			this._cmbEndPoint.Name = "_cmbEndPoint";
			this._cmbEndPoint.Size = new System.Drawing.Size(600, 21);
			this._cmbEndPoint.TabIndex = 0;
			// 
			// _btnOK
			// 
			this._btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._btnOK.Location = new System.Drawing.Point(563, 17);
			this._btnOK.Name = "_btnOK";
			this._btnOK.Size = new System.Drawing.Size(75, 23);
			this._btnOK.TabIndex = 3;
			this._btnOK.Text = "확   인";
			this._btnOK.UseVisualStyleBackColor = true;
			this._btnOK.Click += new System.EventHandler(this._btnOK_Click);
			// 
			// _btnCancel
			// 
			this._btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._btnCancel.Location = new System.Drawing.Point(644, 17);
			this._btnCancel.Name = "_btnCancel";
			this._btnCancel.Size = new System.Drawing.Size(75, 23);
			this._btnCancel.TabIndex = 4;
			this._btnCancel.Text = "취   소";
			this._btnCancel.UseVisualStyleBackColor = true;
			this._btnCancel.Click += new System.EventHandler(this._btnCancel_Click);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this._txtResult);
			this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox4.Location = new System.Drawing.Point(0, 255);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(733, 151);
			this.groupBox4.TabIndex = 5;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "④ 데이터 결과";
			// 
			// _txtResult
			// 
			this._txtResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this._txtResult.Location = new System.Drawing.Point(3, 16);
			this._txtResult.Multiline = true;
			this._txtResult.Name = "_txtResult";
			this._txtResult.ReadOnly = true;
			this._txtResult.Size = new System.Drawing.Size(727, 132);
			this._txtResult.TabIndex = 0;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this._btnOK);
			this.groupBox5.Controls.Add(this._btnCancel);
			this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox5.Location = new System.Drawing.Point(0, 406);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(733, 52);
			this.groupBox5.TabIndex = 6;
			this.groupBox5.TabStop = false;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this._btnCancel;
			this.ClientSize = new System.Drawing.Size(733, 458);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "틸코 API 샘플";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox _txtAPI_KEY;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button _btnFind;
		private System.Windows.Forms.TextBox _txtCertPath;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox _txtCertPassword;
		private System.Windows.Forms.TextBox _txtIdentityNumber;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button _btnOK;
		private System.Windows.Forms.Button _btnCancel;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox _cmbEndPoint;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.TextBox _txtResult;
	}
}

