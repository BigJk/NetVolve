using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetVolveLib;
using NetVolveLib.Evolver;
using NetVolveLib.Grid;
using NetVolveLib.Mars;
using NetVolveLib.Parameters;
using NetVolveLib.Redcode;
using NetVolveLib.Redcode.Enums;
using NetVolveLib.Redcode.Lines;
using NetVolveLib.Utility;

namespace NetVolveGUI
{
    public partial class mainFrm : Form
    {
        public mainFrm()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private static Parameter Paras = ParameterLoader.FromFile("settings.cfg");
        private static int Threads = 7;

        private Grid grid;
        private GridWarrior[] Warriors;
        private GridWarrior SelectedWarrior;

        private delegate void UpdateDel();
        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("save.bin"))
                grid = GridSerializer.Load("save.bin");
            else
                grid  = new Grid(Paras);
            for (int i = 0; i < 20; i++)
            {
                lstRank.Items.Add(new ListViewItem(new string[] { "", (i + 1).ToString(), " - ", " - ", " - ", " - ", " - " })
                {
                    UseItemStyleForSubItems = false
                });
            }
            pnGrid.Refresh();
            Task.Factory.StartNew(new Action(() =>
            {
                while (true)
                {
                    try
                    {
                        Thread.Sleep(100);
                        Invoke(new UpdateDel(UpdatePanel));
                    }
                    catch { }
                }
            }));
            Task.Factory.StartNew(new Action(() =>
            {
                while (true)
                {
                    try
                    {
                        Thread.Sleep(2000);
                        Invoke(new UpdateDel(UpdateList));
                    }
                    catch { }
                }
            }));
            grid.StartAsync(Threads);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            int rec = pnGrid.Width / grid.Size;
            Bitmap bmp = new Bitmap(pnGrid.Width, pnGrid.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {

                for (int y = 0; y < grid.Size; y++)
                {
                    for (int x = 0; x < grid.Size; x++)
                    {
                        using (SolidBrush b = new SolidBrush(grid.Cells[y, x].Owner.Color))
                        {
                            g.FillRectangle(b, new Rectangle(x * rec, y * rec, rec, rec));
                        }
                    }
                }
                e.Graphics.DrawImage(bmp, 0, 0);
            }

        }
        private void UpdatePanel()
        {
            lblTotalWarriors.Text = "Warriors: " + grid.Warriors.Count;
            pnGrid.Refresh();
        }

        private void UpdateList()
        {
            lstRank.BeginUpdate();
            GridWarrior[] warriors = grid.GetWarriors(20);
            for (int i = 0; i < 20; i++)
            {
                if (i < warriors.Count())
                {
                    lstRank.Items[i].SubItems[0].BackColor = warriors[i].Color;
                    lstRank.Items[i].SubItems[2].Text = warriors[i].Warrior.Name;
                    lstRank.Items[i].SubItems[3].Text = warriors[i].Warrior.Author;
                    lstRank.Items[i].SubItems[4].Text = warriors[i].Wins.ToString();
                    lstRank.Items[i].SubItems[5].Text = warriors[i].Lose.ToString();
                    lstRank.Items[i].SubItems[6].Text = warriors[i].OwnedCells.Count.ToString();
                }
                else
                {
                    lstRank.Items[i].SubItems[0].BackColor = Color.Black;
                    lstRank.Items[i].SubItems[2].Text = " - ";
                    lstRank.Items[i].SubItems[3].Text = " - ";
                    lstRank.Items[i].SubItems[4].Text = " - ";
                    lstRank.Items[i].SubItems[5].Text = " - ";
                    lstRank.Items[i].SubItems[6].Text = " - ";
                }
            }
            Warriors = warriors;
            lstRank.EndUpdate();
        }

        private void lstRank_DoubleClick(object sender, EventArgs e)
        {
            if(lstRank.SelectedIndices.Count != 1) return;
            SelectedWarrior = Warriors[lstRank.SelectedIndices[0]];
            lblWarriorName.Text = Warriors[lstRank.SelectedIndices[0]].Warrior.Name;
            lblWarriorFields.Text = "Fields: " + Warriors[lstRank.SelectedIndices[0]].OwnedCells.Count.ToString();
            lblWarriorWins.Text = "Wins: " + Warriors[lstRank.SelectedIndices[0]].Wins.ToString();
            lblWarriorLose.Text = "Lose: " + Warriors[lstRank.SelectedIndices[0]].Lose.ToString();
            pnSelectedWarrior.BackColor = Warriors[lstRank.SelectedIndices[0]].Color;
            txtWarrior.Text = Warriors[lstRank.SelectedIndices[0]].Warrior.ToString().Replace("\n", Environment.NewLine);
            lstCode.Items.Clear();
            foreach (WarriorLine w in Warriors[lstRank.SelectedIndices[0]].Warrior.CodeLines)
            {
                lstCode.Items.Add(new ListViewItem(new string[]
                {
                    Enum.GetName(typeof (Instructors), w.Instructor),
                    Enum.GetName(typeof (Modifiers), w.Modifier),
                    AddressingModesHelper.GetString(w.AddressingMode1), w.Number1.ToString(),
                    AddressingModesHelper.GetString(w.AddressingMode2), w.Number2.ToString()
                }));
            }
        }

        private void btnExBin_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("export")) Directory.CreateDirectory("export");
            SelectedWarrior.Warrior.Save("export\\" + SelectedWarrior.Warrior.Name + ".bin");
        }

        private void btnExRed_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("export")) Directory.CreateDirectory("export");
            File.WriteAllText("export\\" + SelectedWarrior.Warrior.Name + ".red", SelectedWarrior.Warrior.ToString());
        }

        private void mainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GridSerializer.Save("save.bin", grid);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GridSerializer.Save("backup\\" + DateTime.Now.ToShortTimeString().Replace(":","_") + "save.bin", grid);
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pauseToolStripMenuItem.Text == "Pause")
            {
                grid.StopAsync();
                pauseToolStripMenuItem.Text = "Resume";
            }
            else
            {
                grid.StartAsync(Threads);
                pauseToolStripMenuItem.Text = "Pause";
            }
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grid.StopAsync();
            Paras = ParameterLoader.FromFile("settings.cfg");
            grid = new Grid(Paras);
            grid.StartAsync(Threads);
        }

        private void loadFromToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grid.StopAsync();
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    grid = GridSerializer.Load(ofd.FileName);
                }
            }
            grid.StartAsync(Threads);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grid.StopAsync();
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    GridSerializer.Save(sfd.FileName, grid);
                }
            }
            grid.StartAsync(Threads);
        }
    }
}
