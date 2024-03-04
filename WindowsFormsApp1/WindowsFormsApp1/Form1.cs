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
    public partial class Form1 : Form
    {
        int verilenPuan=0;
        
        public Form1()
        {
            InitializeComponent();
            tumYorumAlanlari.Add(new yorumAlanı(lable10,yorum10));
            tumYorumAlanlari.Add(new yorumAlanı(lable11,yorum11));
            tumYorumAlanlari.Add(new yorumAlanı(lable12,yorum12));
            tumYorumAlanlari.Add(new yorumAlanı(lable13,yorum13));
            tumYorumAlanlari.Add(new yorumAlanı(lable14,yorum14));
            tumYorumAlanlari.Add(new yorumAlanı(lable15,yorum15));
            tumYorumAlanlari.Add(new yorumAlanı(lable16,yorum16));
            tumYorumAlanlari.Add(new yorumAlanı(lable17,yorum17));
            tumYorumAlanlari.Add(new yorumAlanı(lable18,yorum18));
            tumYorumAlanlari.Add(new yorumAlanı(lable19,yorum19));
        }


       
        public static List<film> filmList = Program.filmList;
        public class yorumAlanı
        {
            public System.Windows.Forms.Label yorumuYazan { get; set; }
            public System.Windows.Forms.TextBox yazdigiYorum { get; set; }
            
            public yorumAlanı(System.Windows.Forms.Label yorumuYazan, System.Windows.Forms.TextBox yazdigiYorum)
            {
                this.yorumuYazan=yorumuYazan;
                this.yazdigiYorum=yazdigiYorum;
            }
        }
         List<yorumAlanı> tumYorumAlanlari = new List<yorumAlanı>();
        
       
        public film secilenFilm;

        kullanici girisYapan = Program.giren;
        public premium girisYapanP;
        public standart girisYapanS;

        public void secilenFilmiYazdir()
        {
            oyuncularTb.Text = String.Join(", ", secilenFilm.oyuncuList.ToArray());
            yonetmenAdi.Text = secilenFilm.yonetmen;
            filmTuru.Text = secilenFilm.tur;
            FilmAdi.Text = secilenFilm.ad;
            filmYili.Text = secilenFilm.yil;
            filmPuan.Text = String.Format("{0:0.0}", secilenFilm.puan);
            filmOySayisi.Text = '(' + secilenFilm.oySayisi.ToString() + ')';
            pictureBox1.ImageLocation = secilenFilm.posterUrl;
            int i;
            for (i = 0; i < secilenFilm.gelenYorumlar.Count && i < 10; i++)
            {
                tumYorumAlanlari[i].yorumuYazan.Text = secilenFilm.gelenYorumlar[i].yorumuYazan;
                tumYorumAlanlari[i].yorumuYazan.Visible = true;
                tumYorumAlanlari[i].yazdigiYorum.Text = secilenFilm.gelenYorumlar[i].yazdigiYorum;
                tumYorumAlanlari[i].yazdigiYorum.Visible = true;
            }
            for (; i < 10; i++)
            {
                tumYorumAlanlari[i].yorumuYazan.Visible = false;
                tumYorumAlanlari[i].yazdigiYorum.Visible = false;
            }
            verilenPuan = 0;
            birYildiz.Text = ikiYildiz.Text = ucYildiz.Text = dortYildiz.Text = besYildiz.Text = "⭐";

            kullaniciYorumuTb.Text = "";
            kullaniciYorumuTb.Enabled = true;
            birYildiz.Enabled = true;
            ikiYildiz.Enabled = true;
            ucYildiz.Enabled = true;
            dortYildiz.Enabled = true;
            besYildiz.Enabled = true;
            yorumYazmaTalep.Visible = true;

            for (i=0; i < girisYapan.yorumlari.Count; i++)
            {
               if (secilenFilm.filmId == girisYapan.yorumlari[i].yorumYaptigiFilm)
                {
                    kullaniciYorumuTb.Text = girisYapan.yorumlari[i].yorumu;

                    switch (girisYapan.yorumlari[i].verdigiPuan)
                    {
                        case 1:
                            birYildiz_Click(new object(),new EventArgs());
                            break;
                        case 2:
                            ikiYildiz_Click(new object(), new EventArgs());
                            break;
                        case 3:
                            ucYildiz_Click(new object(), new EventArgs());
                            break;
                        case 4:
                            dortYildiz_Click(new object(), new EventArgs());
                            break;
                        case 5:
                            besYildiz_Click(new object(), new EventArgs());
                            break;
                    }
                    kullaniciYorumuTb.Text = girisYapan.yorumlari[i].yorumu;
                    kullaniciYorumuTb.Enabled = false;
                    birYildiz.Enabled = false;
                    ikiYildiz.Enabled = false;
                    ucYildiz.Enabled = false;
                    dortYildiz.Enabled = false;
                    besYildiz.Enabled = false;
                    yorumYazmaTalep.Visible = false;
                }
            }

            tabControl1.SelectTab(1);
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == dataGridView1.Columns[6].Index && e.RowIndex >= 0 && e.RowIndex < filmList.Count)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                secilenFilm = filmList[Int32.Parse(selectedRow.Cells[0].Value.ToString())];
                secilenFilmiYazdir();
            }

            if (e.ColumnIndex == dataGridView1.Columns[7].Index && e.RowIndex >= 0 && e.RowIndex < filmList.Count)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                secilenFilm = filmList[Int32.Parse(selectedRow.Cells[0].Value.ToString())];
                girisYapan.izlenecekler.Add(secilenFilm);
            }
        }

        private void DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == dataGridView2.Columns[6].Index && e.RowIndex >= 0 && e.RowIndex < girisYapan.izlenecekler.Count)
            {
                DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];
                secilenFilm = filmList[Int32.Parse(selectedRow.Cells[0].Value.ToString())];
                secilenFilmiYazdir();
            }

            if (e.ColumnIndex == dataGridView2.Columns[7].Index && e.RowIndex >= 0 && e.RowIndex < girisYapan.izlenecekler.Count)
            {
                DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];
                secilenFilm = filmList[Int32.Parse(selectedRow.Cells[0].Value.ToString())];
                girisYapan.izlenecekler.Remove(secilenFilm);
                profileYonlendir(sender,e);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (radioButton1.Checked)
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
                String.Format("Ad like '%" + textBox1.Text + "%'");
            }

            else if (radioButton2.Checked)
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
                String.Format("Yönetmen like '%" + textBox1.Text + "%'");
            }

            else if (radioButton3.Checked)
            {
                (dataGridView1.DataSource as DataTable).DefaultView.RowFilter =
                String.Format("Tür like '%" + textBox1.Text + "%'");
            }


        }
        public DataTable table = new DataTable();
        public DataTable Table = new DataTable();
        public void Form1_Load(object sender, EventArgs e)
        {
            

            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Ad", typeof(string));
            table.Columns.Add("Yönetmen", typeof(string));
            table.Columns.Add("Tür", typeof(string));
            table.Columns.Add("Yayın Yılı", typeof(string));
            table.Columns.Add("Puan", typeof(string));

            Table.Columns.Add("Id", typeof(int));
            Table.Columns.Add("Ad", typeof(string));
            Table.Columns.Add("Yönetmen", typeof(string));
            Table.Columns.Add("Tür", typeof(string));
            Table.Columns.Add("Yayın Yılı", typeof(string));
            Table.Columns.Add("Puan", typeof(string));


            textBox2.Text= girisYapan.kullanıcıAdı;
            textBox3.Text= girisYapan.sifre;
            textBox4.Text= girisYapan.ad;
            textBox5.Text= girisYapan.soyad;
            textBox6.Text= girisYapan.tckno.ToString();
            textBox7.Text= girisYapan.dogumTarihi;
            if (girisYapan.cinsiyeti == kullanici.cinsiyet.kadin)
                comboBox1.SelectedIndex=0;
            if (girisYapan.cinsiyeti == kullanici.cinsiyet.erkek)
                comboBox1.SelectedIndex=1;
            if (girisYapan.cinsiyeti == kullanici.cinsiyet.digerOrNull)
                comboBox1.SelectedIndex=2;



            if (girisYapan.isPremium)
            {
                girisYapanP = new premium(girisYapan);
                textBox13.Text= girisYapanP.fiyatHesapla().ToString();
                label16.Text="Premium";
                button3.Visible=false;
            }
            else
            {
                girisYapanS = new standart(girisYapan);
                textBox13.Text= girisYapanS.fiyatHesapla().ToString();
                label16.Text="Standart";
            }

            if (!girisYapan.enSonFilmdenHaberiVarMi)
            {
                
                notifyIcon1.Icon= SystemIcons.Information;
                notifyIcon1.BalloonTipTitle="Merhaba "+girisYapan.ad;
                notifyIcon1.BalloonTipText= "Senin için yeni bir filmimiz var: "+ Program.yeniFilmAdi;
                notifyIcon1.ShowBalloonTip(500);
            }

            int i;
            for(i=0; i<filmList.Count;i++)
            {
                table.Rows.Add(filmList[i].filmId, filmList[i].ad, filmList[i].yonetmen, filmList[i].tur, filmList[i].yil, String.Format("{0:0.0}", filmList[i].puan));
            }
            dataGridView1.DataSource = table;
            dataGridView1.Columns[0].Visible = false;

            DataGridViewButtonColumn detay = new DataGridViewButtonColumn();
            detay.HeaderText = "Detay";
            detay.Text = "Detay";
            detay.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(detay);

            DataGridViewButtonColumn listeyeEkle = new DataGridViewButtonColumn();
            listeyeEkle.HeaderText = "Daha sonra izleye ekle";
            listeyeEkle.Text = "+";
            listeyeEkle.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(listeyeEkle);

            dataGridView1.CellContentClick += DataGridView_CellContentClick;

            pictureBox10.Click += playlisteYonlendir;
            pictureBox8.Click += playlisteYonlendir;
            pictureBox15.Click += playlisteYonlendir;
            
            pictureBox4.Click += anasayfayaYonlendir;
            pictureBox7.Click += anasayfayaYonlendir;
            pictureBox13.Click += anasayfayaYonlendir;
            
            pictureBox2.Click += profileYonlendir;
            pictureBox3.Click += profileYonlendir;
            pictureBox14.Click += profileYonlendir;

            pictureBox9.Click += yorumlaraYonlendir;

            pictureBox11.Click += detayaaYonlendir;
            dataGridView2.CellContentClick += DataGridView2_CellContentClick;
        }

        private void anasayfayaYonlendir(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }
        private void detayaaYonlendir(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }
        private void yorumlaraYonlendir(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
        }
        public void playlisteYonlendir(object sender, EventArgs e)
        {
            List<film>  enBegenilenler =filmList.OrderByDescending(film => film.puan).ToList();
            pictureBox16.ImageLocation = enBegenilenler[0].posterUrl;
            pictureBox17.ImageLocation = enBegenilenler[1].posterUrl;
            pictureBox18.ImageLocation = enBegenilenler[2].posterUrl;

            tabControl1.SelectTab(3);
        }

        private void profileYonlendir(object sender, EventArgs e)
        {
            
            dataGridView2.Columns.Clear();
            Table.Columns.Clear();
            Table.Rows.Clear();
            Table.Columns.Add("Id", typeof(int));
            Table.Columns.Add("Ad", typeof(string));
            Table.Columns.Add("Yönetmen", typeof(string));
            Table.Columns.Add("Tür", typeof(string));
            Table.Columns.Add("Yayın Yılı", typeof(string));
            Table.Columns.Add("Puan", typeof(string));

            DataGridViewButtonColumn detaylar = new DataGridViewButtonColumn();
            detaylar.HeaderText = "Detay";
            detaylar.Text = "Detay";
            detaylar.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn listedenCikar = new DataGridViewButtonColumn();
            listedenCikar.HeaderText = "Listeden Çıkart";
            listedenCikar.Text = "-";
            listedenCikar.UseColumnTextForButtonValue = true;

            dataGridView2.DataSource = Table;
            int i;
            for (i = 0; i < girisYapan.izlenecekler.Count; i++)
            {
                Table.Rows.Add(girisYapan.izlenecekler[i].filmId, girisYapan.izlenecekler[i].ad, girisYapan.izlenecekler[i].yonetmen, girisYapan.izlenecekler[i].tur, girisYapan.izlenecekler[i].yil, girisYapan.izlenecekler[i].puan);
            }
            dataGridView2.Columns.Add(detaylar);
            dataGridView2.Columns.Add(listedenCikar);
            dataGridView2.Columns[0].Visible = false;

            

            tabControl1.SelectTab(4);
        }


        private void birYildiz_Click(object sender, EventArgs e)
        {
            birYildiz.Text = "★";
            ikiYildiz.Text = ucYildiz.Text = dortYildiz.Text = besYildiz.Text = "⭐";
            verilenPuan = 1;
        }

        private void ikiYildiz_Click(object sender, EventArgs e)
        {
            birYildiz.Text = ikiYildiz.Text = "★";
            ucYildiz.Text = dortYildiz.Text = besYildiz.Text = "⭐";
            verilenPuan = 2;
        }

        private void ucYildiz_Click(object sender, EventArgs e)
        {
            birYildiz.Text = ikiYildiz.Text = ucYildiz.Text = "★";
            dortYildiz.Text = besYildiz.Text = "⭐";
            verilenPuan = 3;
        }

        private void dortYildiz_Click(object sender, EventArgs e)
        {
            birYildiz.Text = ikiYildiz.Text = ucYildiz.Text = dortYildiz.Text = "★";
            besYildiz.Text = "⭐";
            verilenPuan = 4;
        }

        private void besYildiz_Click(object sender, EventArgs e)
        {
            birYildiz.Text = ikiYildiz.Text = ucYildiz.Text = dortYildiz.Text = besYildiz.Text = "★";
            verilenPuan = 5;
        }

        private void yorumYazmaTalep_Click(object sender, EventArgs e)
        {
            if (girisYapan.isPremium && verilenPuan!=0)
            {
                girisYapanP.degerlendirYorumYap(secilenFilm, kullaniciYorumuTb.Text, verilenPuan);
                filmPuan.Text = String.Format("{0:0.0}", secilenFilm.puan);
                secilenFilm.oySayisi++;
                filmOySayisi.Text =  secilenFilm.oySayisi.ToString();
                int a = secilenFilm.gelenYorumlar.Count-1;
                tumYorumAlanlari[a].yorumuYazan.Text = secilenFilm.gelenYorumlar[a].yorumuYazan;
                tumYorumAlanlari[a].yorumuYazan.Visible = true;
                tumYorumAlanlari[a].yazdigiYorum.Text = secilenFilm.gelenYorumlar[a].yazdigiYorum;
                tumYorumAlanlari[a].yazdigiYorum.Visible= true;
                kullaniciYorumuTb.Enabled = false;
                birYildiz.Enabled = false;
                ikiYildiz.Enabled = false;
                ucYildiz.Enabled = false;
                dortYildiz.Enabled = false;
                besYildiz.Enabled = false;
                yorumYazmaTalep.Visible = false;
            }
            else if (girisYapan.isPremium && verilenPuan==0) MessageBox.Show("Verilen puan 0 olamaz");
            else MessageBox.Show("Yorum yapmak için hesabınızı premiuma yükseltin");
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            List<film> enBegenilenler = filmList.OrderByDescending(film => film.puan).ToList();
            secilenFilm = enBegenilenler[0];
            secilenFilmiYazdir();
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            List<film> enBegenilenler = filmList.OrderByDescending(film => film.puan).ToList();
            secilenFilm = enBegenilenler[1];
            secilenFilmiYazdir();
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            List<film> enBegenilenler = filmList.OrderByDescending(film => film.puan).ToList();
            secilenFilm = enBegenilenler[2];
            secilenFilmiYazdir();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length !=0)
                girisYapan.kullanıcıAdı=textBox2.Text;

            if (textBox3.Text.Length !=0)
                girisYapan.sifre=textBox3.Text;

            if (textBox4.Text.Length !=0)
                girisYapan.ad=textBox4.Text;

            if (textBox5.Text.Length !=0)
                girisYapan.soyad=textBox5.Text;

            if (textBox6.Text.Length !=0)
                girisYapan.tckno=long.Parse(textBox6.Text);

            if (textBox7.Text.Length !=0)
                girisYapan.dogumTarihi =textBox7.Text; 

            if (comboBox1.SelectedIndex==0)
                girisYapan.cinsiyeti = kullanici.cinsiyet.kadin;
            if (comboBox1.SelectedIndex==1)
                girisYapan.cinsiyeti = kullanici.cinsiyet.erkek;
            if (comboBox1.SelectedIndex==2)
                girisYapan.cinsiyeti = kullanici.cinsiyet.digerOrNull;

            MessageBox.Show("Değişiklikler kaydedildi");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 yukselt = new Form3();
            yukselt.Show();
            yukselt.Deactivate +=Yukselt_Deactivate;
        }

        private void Yukselt_Deactivate(object sender, EventArgs e)
        {
            if (Program.hesapYukseltilecekMi)
            {
                Program.hesapYukseltilecekMi=false;
                girisYapanS.hesapYukselt();
                label16.Text="Premium";
                girisYapanS=null;
                girisYapanP = new premium(girisYapan);
                textBox13.Text= girisYapanP.fiyatHesapla().ToString();
                button3.Visible=false;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Hesabınızı kapatmak istediğinize emin misiniz?", "Hesap kapatma", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                girisYapan.hesapKapat();
                Close();
            }
        }
    }

}