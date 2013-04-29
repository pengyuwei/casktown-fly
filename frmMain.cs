using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ToyshopGame
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
		public CGameFrame CGame; 
		private int mintTimeCount;
		private System.Windows.Forms.Timer tmrAutoEnd;
		private System.ComponentModel.IContainer components;

		public frmMain(CGameFrame cGame)
		{
			CGame=cGame;
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.tmrAutoEnd = new System.Windows.Forms.Timer(this.components);
			// 
			// tmrAutoEnd
			// 
			this.tmrAutoEnd.Enabled = true;
			this.tmrAutoEnd.Interval = 1000;
			this.tmrAutoEnd.Tick += new System.EventHandler(this.tmrAutoEnd_Tick);
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(296, 160);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "DirectXForm";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
			this.Load += new System.EventHandler(this.frmMain_Load);

		}
		#endregion

		/// <summary>
		/// Ӧ�ó��������ڵ㡣
		/// </summary>
		[STAThread]
		static void Main() 
		{
			//Application.Run(new frmMain());
		}

		private void frmMain_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode==Keys.Escape)
			{
				CGame.Running =false;
				//break;
			}
		}

		private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			CGame.Running =false;
		}

		private void frmMain_Load(object sender, System.EventArgs e)
		{
			object a=new object();

			mintTimeCount=0;
			tmrAutoEnd.Enabled=false;
			this.Refresh(); 
			this.WindowState=FormWindowState.Maximized;
		}

		private void tmrAutoEnd_Tick(object sender, System.EventArgs e)
		{
			mintTimeCount++;
			if (mintTimeCount>30)
			{
				tmrAutoEnd.Enabled=false;  
				CGame.Running =false;
			}
		}
	}
}
