using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sapper
{
    public partial class Form1 : Form
    {
        SapperGame sapperGame;
        public Form1()
        {
            InitializeComponent();
            easyToolStripMenuItem.Click += EasyToolStripMenuItem_Click;
        }

        private void EasyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            //ShowData();
        }

        void RefreshGrid()
        {
            dataGridView1.ColumnCount = sapperGame.N;
            dataGridView1.RowCount = sapperGame.N;
            for(int i=0;i<sapperGame.N;i++)
            {
                dataGridView1.Rows[i].Height = 40;
                dataGridView1.Columns[i].Width = 40;
            }
            Height = 50 * sapperGame.N + 40;
            Width = 50 * sapperGame.N + 40;
        }

        void ShowData()
        {
            for (int col = 0; col < sapperGame.N; col++)
                for (int row = 0; row < sapperGame.N; row++)
                    dataGridView1[col, row].Value = sapperGame.Area[row, col].ToString();

        }

        void DrawZero (int col, int row)
        {
            if (col < 0 || row < 0 || col > sapperGame.N - 1 || row > sapperGame.N - 1)
                return;
            if (sapperGame.Area[row,col]!=0)
            {
                dataGridView1[col, row].Style.BackColor = Color.Green;
                dataGridView1[col, row].Value = sapperGame.Area[row,col].ToString();
            }
            if (dataGridView1[col, row].Style.BackColor == Color.Green ||
                dataGridView1[col, row].Style.BackColor == Color.LightYellow)
                return;
            else
            {
                dataGridView1[col, row].Style.BackColor = Color.LightYellow;
            }
            DrawZero(col+1, row );
            DrawZero(col-1, row );
            
            DrawZero(col-1, row - 1);
            DrawZero(col+1, row - 1);
            DrawZero(col, row - 1);
            
            DrawZero(col-1, row + 1);
            DrawZero(col+1, row + 1);
            DrawZero(col, row + 1);
        }

        bool Winner()
        {
            int count = 0;
            for (int col = 0; col < sapperGame.N; col++)
                for (int row = 0; row < sapperGame.N; row++)
                {
                    if(dataGridView1[col,row].Style.BackColor==Color.White)
                    {
                        count++;
                    }
                }
                    return count==sapperGame.MinesCount;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(sapperGame.IsEnd==false)
            { 
                int col = e.ColumnIndex;
                int row = e.RowIndex;

                if(sapperGame.Area[row,col]>10)
                {
                    dataGridView1[col, row].Style.BackColor = Color.Red;
                    sapperGame.IsEnd = true;
                    MessageBox.Show("Вас размотало на мине!");
                }
                else if (sapperGame.Area[row,col]!=0)
                {
                    dataGridView1[col, row].Style.BackColor = Color.Green;
                    dataGridView1[col, row].Value = sapperGame.Area[row, col].ToString();
                }
                else
                {
                    DrawZero(col, row);
                    dataGridView1[col, row].Style.BackColor = Color.LightYellow;
                }
                if(!sapperGame.IsEnd&&Winner())
                {
                    MessageBox.Show("Победили!");
                    sapperGame.IsEnd = true;
                }
                dataGridView1.ClearSelection();
            }
        }

        void NewGme()
        {
            for (int col=0;col<sapperGame.N;col++)
                for(int row = 0; row < sapperGame.N; row++)
                {
                    dataGridView1[col, row].Style.BackColor = Color.White;
                    dataGridView1[col, row].Value = "";
                }
        }

        private void easyToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            sapperGame = new SapperGame(10,5);
            RefreshGrid();
            NewGme();
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sapperGame = new SapperGame(20, 25);
            RefreshGrid();
            NewGme();
        }
    }
}
