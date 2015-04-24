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
        public int nWidth;     // 表示雷区的宽度
        public int nHeight;        // 表示雷区的高度
        public int nMineCnt;       // 表示地雷的数目

        bool bMark;     // 表示是否使用标记
        bool bAudio;    // 表示是否使用音效

        public Form_Main()
        {
            InitializeComponent();

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
            PaintGame();
        }

        /// <summary>
        /// 绘制游戏区
        /// </summary>
        private void PaintGame()
        {
            Graphics g = this.CreateGraphics();     //  创建绘图句柄
            g.FillRectangle(Brushes.White, new Rectangle(0, 0, this.Width, this.Height));
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
                    g.FillRectangle(Brushes.SandyBrown, new Rectangle(nOffsetX + 34 * (i - 1) + 1, nOffsetY + 34 * (j - 1) + 1, 32, 32));   // 绘制雷区方块
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
            PaintGame();
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
    }
}