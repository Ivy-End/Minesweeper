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
        int[] px = new int[] { 1, -1, 0, 0, 1, -1, 1, -1 };   // 四方向x坐标偏移量
        int[] py = new int[] { 0, 0, 1, -1, 1, 1, -1, -1 };   // 四方向y坐标偏移量

        public int nWidth;     // 表示雷区的宽度
        public int nHeight;        // 表示雷区的高度
        public int nMineCnt;       // 表示地雷的数目

        bool bMark;     // 表示是否使用标记
        bool bAudio;    // 表示是否使用音效

        bool bMouseLeft;    // 鼠标左键是否被按下
        bool bMouseRight;   // 鼠标右键是否被按下

        bool bGame; // 游戏是否结束

        int[,] pMine = new int[MAX_WIDTH, MAX_HEIGHT];  // 第一类数据
        int[,] pState = new int[MAX_WIDTH, MAX_HEIGHT]; // 第二类数据

        Point MouseFocus;   // 高亮点记录


        System.Media.SoundPlayer soundTick; // 计时
        System.Media.SoundPlayer soundBomb; // 爆炸

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

            // 初始化音频信息
            soundTick = new System.Media.SoundPlayer(Properties.Resources.Tick);
            soundBomb = new System.Media.SoundPlayer(Properties.Resources.Bomb);

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
                    if(pState[i, j] != 1)   // 未点开
                    {
                        // 绘制背景
                        if (i == MouseFocus.X && j == MouseFocus.Y)  // 是否为高亮点
                        {
                            g.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.SandyBrown)), new Rectangle(nOffsetX + 34 * (i - 1) + 1, nOffsetY + 34 * (j - 1) + 1, 32, 32));
                        }
                        else
                        {
                            g.FillRectangle(Brushes.SandyBrown, new Rectangle(nOffsetX + 34 * (i - 1) + 1, nOffsetY + 34 * (j - 1) + 1, 32, 32));   // 绘制雷区方块
                        }
                        // 绘制标记
                        if(pState[i, j] == 2)
                        {
                            g.DrawImage(Properties.Resources.Flag, nOffsetX + 34 * (i - 1) + 1 + 4, nOffsetY + 34 * (j - 1) + 1 + 2);   // 绘制红旗
                        }
                        if(pState[i, j] == 3)
                        {
                            g.DrawImage(Properties.Resources.Doubt, nOffsetX + 34 * (i - 1) + 1 + 4, nOffsetY + 34 * (j - 1) + 1 + 2);   // 绘制问号
                        }
                    }
                    else if(pState[i, j] == 1)  // 点开
                    {
                        // 绘制背景
                        if(MouseFocus.X == i && MouseFocus.Y == j)
                        {
                            g.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.LightGray)), new Rectangle(nOffsetX + 34 * (i - 1) + 1, nOffsetY + 34 * (j - 1) + 1, 32, 32));
                        }
                        else
                        {
                            g.FillRectangle(Brushes.LightGray, new Rectangle(nOffsetX + 34 * (i - 1) + 1, nOffsetY + 34 * (j - 1) + 1, 32, 32));
                        }
                        // 绘制数字
                        if(pMine[i, j] > 0)
                        {
                            Brush DrawBrush = new SolidBrush(Color.Blue);    // 定义钢笔
                            // 各个地雷数目的颜色
                            if (pMine[i, j] == 2) { DrawBrush = new SolidBrush(Color.Green); }
                            if (pMine[i, j] == 3) { DrawBrush = new SolidBrush(Color.Red); }
                            if (pMine[i, j] == 4) { DrawBrush = new SolidBrush(Color.DarkBlue); }
                            if (pMine[i, j] == 5) { DrawBrush = new SolidBrush(Color.DarkRed); }
                            if (pMine[i, j] == 6) { DrawBrush = new SolidBrush(Color.DarkSeaGreen); }
                            if (pMine[i, j] == 7) { DrawBrush = new SolidBrush(Color.Black); }
                            if (pMine[i, j] == 8) { DrawBrush = new SolidBrush(Color.DarkGray); }
                            SizeF Size = g.MeasureString(pMine[i, j].ToString(), new Font("Consolas", 16));
                            g.DrawString(pMine[i, j].ToString(), new Font("Consolas", 16), DrawBrush, nOffsetX + 34 * (i - 1) + 1 + (32 - Size.Width) / 2, nOffsetY + 34 * (j - 1) + 1 + (32 - Size.Height) / 2);
                        }
                        // 绘制地雷
                        if(pMine[i, j] == -1)
                        {
                            g.DrawImage(Properties.Resources.Mine, nOffsetX + 34 * (i - 1) + 1 + 4, nOffsetY + 34 * (j - 1) + 1 + 2);   // 绘制地雷
                        }
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
            bGame = false;  // 游戏暂未结束
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
            if(bAudio)
            {
                soundTick.Play();   // 播放
            }
            Label_Timer.Text = Convert.ToString(Convert.ToInt32(Label_Timer.Text) + 1); // 自增1秒
        }

        private void Form_Main_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)   // 鼠标左键被按下
            {
                bMouseLeft = true;
            }
            if(e.Button == MouseButtons.Right)  // 鼠标右键被按下
            {
                bMouseRight = true;
            }
        }

        private void Form_Main_MouseUp(object sender, MouseEventArgs e)
        {
            if ((MouseFocus.X == 0 && MouseFocus.Y == 0) || bGame) // 不在地雷区域或游戏结束
            {
                return; // 不做任何处理
            }

            if(bMouseLeft && bMouseRight)   // 左右键同时按下
            {
                if(pState[MouseFocus.X, MouseFocus.Y] == 1 && pMine[MouseFocus.X, MouseFocus.Y] > 0)    // 为数字区域
                {
                    int nFlagCnt = 0, nDoubtCnt = 0, nSysCnt = pMine[MouseFocus.X, MouseFocus.Y];   // 记录红旗数目，问号数目，九宫格地雷数目
                    for(int i = 0; i < 8; i++)
                    {
                        // 获取偏移量
                        int x = MouseFocus.X + dx[i];
                        int y = MouseFocus.Y + dy[i];
                        if(pState[x, y] == 2)   // 红旗
                        {
                            nFlagCnt++;
                        }
                        if(pState[x, y] == 3)   // 问号
                        {
                            nDoubtCnt++;
                        }
                        if(nFlagCnt == nSysCnt || nFlagCnt + nDoubtCnt == nSysCnt) // 打开九宫格
                        {
                            bool bFlag = OpenMine(MouseFocus.X, MouseFocus.Y);
                            if (!bFlag) // 周围有地雷
                            {
                                // 结束游戏 
                                GameLost();
                            }
                        }
                    }
                }
            }
            else if(bMouseLeft) // 左键被按下
            {
                if(pMine[MouseFocus.X, MouseFocus.Y] != -1)
                {
                    if(pState[MouseFocus.X, MouseFocus.Y] == 0)
                    {
                        dfs(MouseFocus.X, MouseFocus.Y);
                    }
                }
                else
                {
                    // 地雷，游戏结束
                    GameLost();
                }
            }
            else if(bMouseRight)    // 右键被按下
            {
                if(bMark)   // 可以使用标记
                {
                    if(pState[MouseFocus.X, MouseFocus.Y] == 0) // 未点开
                    {
                        if (Convert.ToInt32(Label_Mine.Text) > 0)
                        {
                            pState[MouseFocus.X, MouseFocus.Y] = 2; // 红旗
                            Label_Mine.Text = Convert.ToString(Convert.ToInt32(Label_Mine.Text) - 1);   // 剩余地雷数目减1
                        }
                    }
                    else if(pState[MouseFocus.X, MouseFocus.Y] == 2) // 红旗
                    {
                        pState[MouseFocus.X, MouseFocus.Y] = 3; // 问号
                        Label_Mine.Text = Convert.ToString(Convert.ToInt32(Label_Mine.Text) + 1);   // 剩余地雷数目加1
                    }
                    else if(pState[MouseFocus.X, MouseFocus.Y] == 3) // 问号
                    {
                        pState[MouseFocus.X, MouseFocus.Y] = 0;  // 未点开
                    }
                }
            }
            this.Refresh();
            GameWin();
            bMouseLeft = bMouseRight = false;
        }

        private void dfs(int sx, int sy)
        {
            pState[sx, sy] = 1; // 访问该点
            for(int i = 0; i < 8; i++)
            {
                // 获取相邻点的坐标
                int x = sx + px[i];
                int y = sy + py[i];
                if(x >= 1 && x <= nWidth && y >= 1 && y <= nHeight && 
                    pMine[x, y] != -1 && pMine[sx, sy] == 0 && 
                    (pState[x, y] == 0 || pState[x, y] == 3)) // 不是地雷，处于地雷区域，且未点开，或者标记为问号
                {
                    dfs(x, y);  // 访问该点
                }
            }
        }

        private bool OpenMine(int sx, int sy)
        {
            bool bFlag = true;  // 默认周围无雷
            for (int i = 0; i < 8; i++)
            {
                // 获取偏移量
                int x = MouseFocus.X + dx[i];
                int y = MouseFocus.Y + dy[i];
                if (pState[x, y] == 0)   // 问号
                {
                    pState[x, y] = 1;   // 打开
                    if(pMine[x, y] != -1)   // 无地雷
                    {
                        dfs(x, y);
                    }
                    else    // 有地雷
                    {
                        bFlag = false;
                        break;
                    }
                }
            }
            return bFlag;
        }

        private void GameLost()
        {
            for(int i = 1; i <= nWidth; i++)
            {
                for(int j = 1; j <= nHeight; j++)
                {
                    if(pMine[i, j] == -1 && (pState[i, j] == 0 || pState[i, j] == 3))   // 未点开或者标记为问号的地雷
                    {
                        pState[i, j] = 1;   // 点开该地雷
                    }
                }
            }
            if(bAudio)
            {
                soundBomb.Play();
            }
            Timer_Main.Enabled = false; // 停止计时
            bGame = true;
        }

        private void GameWin()
        {
            int nCnt = 0;   // 用户标记红旗数目、问号数目、以及无标记未点开区域总数
            for(int i = 1; i <= nWidth; i++)
            {
                for(int j = 1; j <= nHeight; j++)
                {
                    if(pState[i ,j] == 0 || pState[i, j] == 2 || pState[i, j] == 3) // 对应无标记未点开区域、红旗区域、问号区域
                    {
                        nCnt++;
                    }
                }
            }
            if(nCnt == nMineCnt)    // 胜利条件
            {
                Timer_Main.Enabled = false; // 关闭计时器
                MessageBox.Show(String.Format("游戏胜利，耗时：{0} 秒", Label_Timer.Text), "提示", MessageBoxButtons.OK);
                // 更新记录
                if(nWidth == 10 && nHeight == 10 && nMineCnt == 10) // 初级
                {
                    if(Properties.Settings.Default.Beginner > Convert.ToInt32(Label_Timer.Text))    // 更新记录
                    {
                        Properties.Settings.Default.Beginner = Convert.ToInt32(Label_Timer.Text);
                        Properties.Settings.Default.Save();
                    }
                }
                else if(nWidth == 16 && nHeight == 16 && nMineCnt == 40)    // 中级
                {
                    if (Properties.Settings.Default.Intermediate > Convert.ToInt32(Label_Timer.Text))    // 更新记录
                    {
                        Properties.Settings.Default.Intermediate = Convert.ToInt32(Label_Timer.Text);
                        Properties.Settings.Default.Save();
                    }
                }
                else if(nWidth == 30 && nHeight == 16 && nMineCnt == 99)    // 高级
                {
                    if (Properties.Settings.Default.Expert > Convert.ToInt32(Label_Timer.Text))    // 更新记录
                    {
                        Properties.Settings.Default.Expert = Convert.ToInt32(Label_Timer.Text);
                        Properties.Settings.Default.Save();
                    }
                }
                bGame = true;
            }
        }
    }
}