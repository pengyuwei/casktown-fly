using System;
using DxVBLib;

namespace ToyshopGame
{
	/// <summary>
	/// CConstcs 的摘要说明。
	/// </summary>
	public class CConst
	{
		//屏幕信息常量
		public static RECT C_SCREENRECT=new RECT();
		public static int C_SCREENWIDTH=800;
		public static int C_SCREENHEIGHT=600;
		//RPG相关常量
		public static int C_RpgTalkBoxWidth=780;
		public static int C_RpgTalkBoxHeight=130;
		public static int C_RpgTalkBoxTop1=440;
		public static int C_RpgTalkBoxHeadX1=80;
		public static int C_RpgTalkBoxHeadY1=360;
		public static int C_RpgTalkBoxTop2=490;
		public static int C_RpgTalkBoxHeadX2=100;
		public static int C_RpgTalkBoxHeadY2=100;
		//子弹信息常量
		public static int C_BULLETTYPE_PLAYER1=0;
		public static int C_BULLETTYPE_PLAYER2=1;
		public static int C_BULLETTYPE_ENEMY=-5;
		//集合Key信息常量
		public static string C_SURFACEKEY_MAP="Map";
		public static string C_SURFACEKEY_PLAYER="Player";
		public static string C_SURFACEKEY_PLAYERHEAD="PlayerHead";
		public static string C_SURFACEKEY_PLAYER1="Player1";
		public static string C_SURFACEKEY_PLAYER2="Player2";
		public static string C_SURFACEKEY_PLAYERHEAD1="PlayerHead1";
		public static string C_SURFACEKEY_PLAYERHEAD2="PlayerHead2";
		public static string C_SURFACEKEY_ENEMY="Enemy";
		public static string C_SURFACEKEY_ENEMY1="Enemy1";
		public static string C_SURFACEKEY_ENEMY2="Enemy2";
		public static string C_SURFACEKEY_ENEMYBOSS="EnemyBoss";
		public static string C_SURFACEKEY_OBJECT="Object";
		public static string C_SURFACEKEY_OBJECT1="Object1";
		public static string C_SURFACEKEY_OBJECT2="Object2";
		public static string C_SURFACEKEY_BULLET1="Bullet1";
		public static string C_SURFACEKEY_BULLET2="Bullet2";
		public static string C_SURFACEKEY_WEAPON1="Weapon1";
		public static string C_SURFACEKEY_WEAPON2="Weapon2";
		public static string C_SURFACEKEY_EXPLODE="Explode";
		public static string C_SURFACEKEY_EXPLODE1="Explode1";
		public static string C_SURFACEKEY_EXPLODE2="Explode2";

		static CConst()
		{
			C_SCREENRECT.Left=0;
			C_SCREENRECT.Top=0;
			C_SCREENRECT.Bottom=C_SCREENHEIGHT;
			C_SCREENRECT.Right=C_SCREENWIDTH;
		}
		public CConst()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
	}
}
