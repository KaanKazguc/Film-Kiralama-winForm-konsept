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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            
            
        }
        //public static List<kullanici> tumKullaniciler = new List<kullanici>();
        
        private void button1_Click(object sender, EventArgs e)
        {
            List<kullanici> tumKullaniciler = Program.tumKullaniciler;


            if (textBox1.Text == "admin")
                if(textBox2.Text == "admin")
                {
                    Program.girisYapan = girisStateler.yonetici;
                    Close();
                }

            int i;
            for (i = 0; i<tumKullaniciler.Count; i++)
            {
                if(tumKullaniciler[i].kullanıcıAdı != null)
                if(tumKullaniciler[i].kullanıcıAdı ==textBox1.Text)
                {
                    if (tumKullaniciler[i].sifre ==textBox2.Text)
                    {
                        Program.giren = tumKullaniciler[i];
                        Program.girisYapan = girisStateler.kullanici;
                        Close();
                    }
                }
            }


            MessageBox.Show("Kullanıcı Adı Veya Şifre Hatalı");


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Program.girisYapan = girisStateler.yeniKullanici;
            Close();
        }
    }
}
