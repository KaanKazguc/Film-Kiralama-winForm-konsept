using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private DataTable table= new DataTable();
        public Form2()
        {
            InitializeComponent();
        }

        List<film> filmList = Program.filmList;
        string[] oyuncular;
        bool filmYeniMi;
        film secilenFilm = new film();
        private void Form2_Load(object sender, EventArgs e)
        {
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Ad", typeof(string));
            table.Columns.Add("Yönetmen", typeof(string));
            table.Columns.Add("Tür", typeof(string));
            table.Columns.Add("Yayın Yılı", typeof(string));
            table.Columns.Add("Puan", typeof(string));

            

            int i;
            for (i = 0; i < filmList.Count; i++)
            {
                table.Rows.Add(filmList[i].filmId, filmList[i].ad, filmList[i].yonetmen, filmList[i].tur, filmList[i].yil, filmList[i].puan);
            }
            dataGridView1.DataSource = table;
            dataGridView1.Columns[0].Visible = false;
        }

        public void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex == filmList.Count)
            {
                filmYeniMi = true;
                secilenFilm = new film();
            }
            if (e.RowIndex >= 0 && e.RowIndex != filmList.Count)
            {

                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                secilenFilm = filmList[Int32.Parse(row.Cells[0].Value.ToString())];
                filmYeniMi = false;

            }
            if (e.RowIndex >= 0)
            {
                textBox1.Text = secilenFilm.filmId.ToString();
                textBox2.Text = secilenFilm.ad;
                textBox3.Text = secilenFilm.yonetmen;
                textBox4.Text = secilenFilm.tur;
                textBox5.Text = secilenFilm.yil;
                textBox11.Text = secilenFilm.puan.ToString();
                textBox12.Text = secilenFilm.oySayisi.ToString();
                oyuncular = secilenFilm.oyuncuList.ToArray();
                textBox13.Text = String.Join(", ", oyuncular);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i;

            if (filmYeniMi) 
            {
                secilenFilm.filmId = Int32.Parse(textBox1.Text);
                secilenFilm.puan = Int32.Parse(textBox11.Text);
                secilenFilm.oySayisi = Int32.Parse(textBox12.Text);
            }
            else secilenFilm = filmList[Int32.Parse(textBox1.Text)];

            secilenFilm.ad = textBox2.Text;
            secilenFilm.yonetmen = textBox3.Text;
            secilenFilm.tur = textBox4.Text;
            secilenFilm.yil = textBox5.Text;
            secilenFilm.oyuncuList=oyuncular.ToList();
            secilenFilm.posterUrl = textBox15.Text;

            if (filmYeniMi)
            {
                filmList.Add(secilenFilm);
                Program.yeniFilmAdi=secilenFilm.ad;
                for (i=0;i< Program.tumKullaniciler.Count; i++)
                {
                    Program.tumKullaniciler[i].enSonFilmdenHaberiVarMi=false;
                }
            }

            table.Rows.Clear();
            
            for (i = 0; i < filmList.Count; i++)
            {
                table.Rows.Add(filmList[i].filmId, filmList[i].ad, filmList[i].yonetmen, filmList[i].tur, filmList[i].yil, filmList[i].puan);
            }
            dataGridView1.DataSource = table;
            dataGridView1.Columns[0].Visible = false;
        }

        private void oyuncuEkle_Click(object sender, EventArgs e)
        {
            oyuncular=oyuncular.Concat( new string[] {textBox14.Text}).ToArray();
            textBox13.Text = String.Join(", ", oyuncular);
            textBox14.Text = "";
        }

        private void oyuncuTemizle_Click(object sender, EventArgs e)
        {
            oyuncular = new string[] {};
            textBox13.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog(); 
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                textBox15.Text = ofd.FileName;
            }
        }
    }
}
