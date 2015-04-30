using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form_Main : Form
    {
        const int MAX_WIDTH = 64;   // 最大宽度
        const int MAX_HEIGHT = 32;  // 最大高度
        int[] dx = new int[] { -1, 0, 1, -1, 1, -1, 0, 1 };   // x坐标偏移量
        int[] dy = new int[] { 1, 1, 1, 0, 0, -1, -1, -1 };   // y坐标偏移量

        public int nWidth;     // 表示雷区的宽度
        public int nHeight;        // 表示雷区的高度
        public int nMineCnt;       // 表示地雷的数目

        bool bMark;     // 表示是否使用标记
        bool bAudio;    // 表示是否使用音效

        int[,] pMine = new int[MAX_WIDTH, MAX_HEIGHT];  // 第一类数据
        int[,] pState = new int[MAX_WIDTH, MAX_HEIGHT]; // 第二类数据

        Point MouseFocus;   // 高亮点记录

        public Form_Main()
        {
            InitializeComponent();
            
            this.DoubleBuffered = true; // 开启双缓冲

            // 初始化游戏参数
            nWidth = Properties.Settings.Default.Width;
            nHeight = Properties.Settings.Default.Height;
            nMineCnt = Properties.Settings.Default.MineCnt;

            // 初始化
            bMark = Properties.Settings.Default.Mark;
            bAudio = Properties.Settings.Default.Audio;
            markMToolStripMenuItem.Checked = bMark;
            audioMToolStripMenuItem.Checked = bAudio;

            UpdateSize();
            SelectLevel();            
        }

        /// <summary>
        /// 游戏参数设置
        /// </summary>
        /// <param name="Width">雷区宽度</param>
        /// <param name="Height">雷区高度</param>
        /// <param name="MineCnt">地雷数目</param>
        private void SetGame(int Width, int Height, int MineCnt)
        {
            nWidth = Width;
            nHeight = Height;
            nMineCnt = MineCnt;
            UpdateSize();
        }

        /// <summary>
        /// 设置游戏等级为初级
        /// </summary>
        private void SetGameBeginner()
        {
            SetGame(10, 10, 10);
        }

        /// <summary>
        /// 设置游戏等级为中级
        /// </summary>
        private void SetGameIntermediate()
        {
            SetGame(16, 16, 40);
        }

        /// <summary>
        /// 设置游戏等级为高级
        /// </summary>
        private void SetGameExpert()
        {
            SetGame(30, 16, 99);
        }

        /// <summary>
        /// 选择对应的游戏等级
        /// </summary>
        private void SelectLevel()
        {
            if (nWidth == 10 && nHeight == 10 && nMineCnt == 10)
            {
                beginnerBToolStripMenuItem.Checked = true;
                intermediateIToolStripMenuItem.Checked = false;
                expertEToolStripMenuItem.Checked = false;
                settingSToolStripMenuItem.Checked = false;
            }
            else if (nWidth == 16 && nHeight == 16 && nMineCnt == 40)
            {
                beginnerBToolStripMenuItem.Checked = false;
                intermediateIToolStripMenuItem.Checked = true;
                expertEToolStripMenuItem.Checked = false;
                settingSToolStripMenuItem.Checked = false;
            }
            else if (nWidth == 30 && nHeight == 16 && nMineCnt == 99)
            {
                beginnerBToolStripMenuItem.Checked = false;
                intermediateIToolStripMenuItem.Checked = false;
                expertEToolStripMenuItem.Checked = true;
                settingSToolStripMenuItem.Checked = false;
            }
            else
            {
                beginnerBToolStripMenuItem.Checked = false;
                intermediateIToolStripMenuItem.Checked = false;
                expertEToolStripMenuItem.Checked = false;
                settingSToolStripMenuItem.Checked = true;
            }
        }

        private void Form_Main_Paint(object sender, PaintEventArgs e)
        {
            PaintGame(e.Graphics);
        }

        /// <summary>
        /// 绘制游戏区
        /// </summary>
        private void PaintGame(Graphics g)
        {
            g.Clear(Color.White);   // 清空绘图区
            // 我们需要是雷区在用户显示的区域上下左右保持6px的偏移量，使得整体看起来更加协调
            int nOffsetX = 6;   // X方向偏移量
            int nOffsetY = 6 + MenuStrip_Main.Height;   // Y方向偏移量
            for (int i = 1; i <= nWidth; i++)    // 绘制行
            {
                for (int j = 1; j <= nHeight; j++)   // 绘制列
                {
                    // 第一个参数为笔刷，这里采用内置笔刷SandyBronw
                    // 第二个参数为方块的参数，这里采用左上角坐标以及长宽的形式给出
                    // 34表示每个雷区的大小，再加上偏移量就是我们当前雷区的起始位置，由于要空出1px的间隔，因此还需要加1
                    // 由此可以的到每个方块在雷区中的位置，然后利用循环绘制出来

                    if(i == MouseFocus.X && j == MouseFocus.Y)  // 是否为高亮点
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.SandyBrown)), new Rectangle(nOffsetX + 34 * (i - 1) + 1, nOffsetY + 34 * (j - 1) + 1, 32, 32));
                    }
                    else
                    {
                        g.FillRectangle(Brushes.SandyBrown, new Rectangle(nOffsetX + 34 * (i - 1) + 1, nOffsetY + 34 * (j - 1) + 1, 32, 32));   // 绘制雷区方块
                    }
                    
                }
            }
        }

        /// <summary>
        /// 自动更新窗口大小
        /// </summary>
        private void UpdateSize()
        {
            int nOffsetX = this.Width - this.ClientSize.Width;  // 包含了窗口标题栏以及上下边框的高度
            int nOffsetY = this.Height - this.ClientSize.Height;    // 包含了左右边框的宽度
            int nAdditionY = MenuStrip_Main.Height + TableLayoutPanel_Main.Height;  // 包含了菜单栏以及下方显示信息栏的高度
            this.Width = 12 + 34 * nWidth + nOffsetX;   // 设置窗口高度，34为每个雷区的高度，12为上下总空隙（6px+6px），再加上偏移量
            this.Height = 12 + 34 * nHeight + nAdditionY + nOffsetY;    // 设置窗口宽度，同理
            newGameNToolStripMenuItem_Click(new object(), new EventArgs()); // 调用新建游戏函数
        }

        private void beginnerBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nWidth = 10; nHeight = 10; nMineCnt = 10;
            SelectLevel();
            UpdateSize();
        }

        private void intermediateIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nWidth = 16; nHeight = 16; nMineCnt = 40;
            SelectLevel();
            UpdateSize();
        }

        private void expertEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nWidth = 30; nHeight = 16; nMineCnt = 99;
            SelectLevel();
            UpdateSize();
        }

        private void exitXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure to exit the game?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// 系统关于对话框（API）
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="szApp">标题文本</param>
        /// <param name="szOtherStuff">内容文本</param>
        /// <param name="hIcon">图标句柄</param>
        /// <returns></returns>
        [DllImport("shell32.dll")]
        public extern static int ShellAbout(IntPtr hWnd, string szApp, string szOtherStuff, IntPtr hIcon);

        private void aboutAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShellAbout(this.Handle, "Minesweeper", "A minesweeper game using CSharp language.", this.Icon.Handle);
        }

        private void markMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            markMToolStripMenuItem.Checked = bMark = !bMark;
        }

        private void audioMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            audioMToolStripMenuItem.Checked = bAudio = !bAudio;
        }

        private void settingSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Setting Setting = new Form_Setting(this);  // 将本身作为参数传递过去
            Setting.ShowDialog();
            UpdateSize();
        }

        private void rankRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Rank Rank = new Form_Rank();
            Rank.ShowDialog();
        }

        private void newGameNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 以下两行清空数组
            Array.Clear(pMine, 0, pMine.Length);
            Array.Clear(pState, 0, pState.Length);
            // 重置高亮点
            MouseFocus.X = 0; MouseFocus.Y = 0;
            // 初始化地雷数据
            Random Rand = new Random();
            for(int i = 1; i <= nMineCnt; )  // 地雷总数
            {
                // 随机地雷坐标(x, y)
                int x = Rand.Next(nWidth) + 1; 
                int y = Rand.Next(nHeight) + 1;
                if (pMine[x, y] != -1)
                {
                    pMine[x, y] = -1; i++;
                }
            }
            for(int i = 1; i <= nWidth; i++)    // 枚举宽度
            {
                for(int j = 1; j <= nHeight; j++)   // 枚举高度
                {
                    if(pMine[i, j] != -1)   // 不是地雷，显示周围地雷数
                    {
                        for(int k = 0; k < 8; k++)  // 八个方向拓展
                        {
                            if(pMine[i + dx[k], j + dy[k]] == -1)   // 找到地雷
                            {
                                pMine[i, j]++;  // 地雷数自增
                            }
                        }
                    }
                }
            }
            Label_Mine.Text = nMineCnt.ToString();  // 显示地雷数目
            Label_Timer.Text = "0"; // 计时器清零
            Timer_Main.Enabled = true;  // 启动计时器计时 
        }

        private void Form_Main_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.X < 6 || e.X > 6 + nWidth * 34 || 
                e.Y < 6 + MenuStrip_Main.Height || 
                e.Y > 6 + MenuStrip_Main.Height + nHeight * 34) // 不在地雷区域
            {
                MouseFocus.X = 0; MouseFocus.Y = 0;
            }
            else
            {
                int x = (e.X - 6) / 34 + 1; // 获取x位置
                int y = (e.Y - MenuStrip_Main.Height - 6) / 34 + 1; // 获取y位置
                MouseFocus.X = x; MouseFocus.Y = y; // 设置当前高亮点
            }
            this.Refresh();    // 重绘雷区
        }

        private void Timer_Main_Tick(object sender, EventArgs e)
        {
            Label_Timer.Text = Convert.ToString(Convert.ToInt32(Label_Timer.Text) + 1); // 自增1秒
        }
    }
}