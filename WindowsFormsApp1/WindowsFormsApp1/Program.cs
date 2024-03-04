using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
   
    public class yorum
        {
            public string yorumuYazan { get; set; }
            public string yazdigiYorum { get; set; }

            public yorum(string yorumuYazan, string yazdigiYorum)
            {
                this.yazdigiYorum = yazdigiYorum;
                this.yorumuYazan = yorumuYazan;
            }
        }
    public class yaptigiYorum
    {
        public int yorumYaptigiFilm;
        public int verdigiPuan;
        public string yorumu;

        public yaptigiYorum(int yorumYaptigiFilm, int verdigiPuan, string yorumu)
        {
            this.yorumYaptigiFilm = yorumYaptigiFilm;
            this.verdigiPuan = verdigiPuan;
            this.yorumu = yorumu;
        }
    }
    public class film
    {
        public int filmId { get; set; }
        public string ad { get; set; }
        public string yonetmen { get; set; }
        public string tur { get; set; }
        public string yil { get; set; }
        public double puan { get; set; }
        public int oySayisi { get; set; } = 0;

        public string posterUrl { get; set; }
        public List<yorum> gelenYorumlar = new List<yorum>();


        public List<string> oyuncuList = new List<string>();
        public film(string ad, string yonetmen, string tur, string yil, double puan, int oySayisi, string[] oyuncular, string posterUrl)
        {
            filmId = Program.filmList.Count;
            this.ad = ad;
            this.yonetmen = yonetmen;
            this.tur = tur;
            this.yil = yil;
            this.puan = puan;
            this.oySayisi = oySayisi;
            oyuncuList.AddRange(oyuncular);
            this.posterUrl = posterUrl;
            
        }
        public film()
        {
            filmId = Program.filmList.Count;
            this.puan = 0;
            this.oySayisi = 0;
        }
        
    }

    public class kullanici
    {
        public kullanici baseKullanici;
        public string kullanıcıAdı { get; set; }
        public string sifre { get; set; }
        public string ad { get; set; }
        public string soyad { get; set; }
        public long tckno { get; set; }
        public string dogumTarihi { get; set; }

        public bool enSonFilmdenHaberiVarMi = true;

        public List<film> izlenecekler = new List<film>();

        public List<yaptigiYorum> yorumlari = new List<yaptigiYorum>();

        public cinsiyet cinsiyeti;

        public enum cinsiyet
        {
            kadin,
            erkek,
            digerOrNull
        }
        public bool isPremium { get; set; } = false;

        public virtual double fiyatHesapla()
        {
            return 100;
        }
        public void hesapKapat()
        {
            this.kullanıcıAdı = null;
        }

        public kullanici(string kullanıcıAdı, string sifre, string ad, string soyad, long tckno, string dogumTarihi, cinsiyet cinsiyeti, bool isPremium)
        {
            this.kullanıcıAdı = kullanıcıAdı;
            this.sifre = sifre;
            this.ad = ad;
            this.soyad = soyad;
            this.tckno = tckno;
            this.dogumTarihi = dogumTarihi;
            this.cinsiyeti = cinsiyeti;
            this.isPremium = isPremium;
        }
        public kullanici(){}
    }

    public class standart : kullanici
    {
        public void hesapYukselt()
        {
            baseKullanici.isPremium=true;
        }
        public standart(kullanici baseKullanici)
        {
            this.baseKullanici = baseKullanici;
        }
    }

    public class premium : kullanici
    {

        public void degerlendirYorumYap(film filmi, string yaptigiYorum, int verdigiPuan)
        {
            filmi.gelenYorumlar.Add(new yorum(baseKullanici.ad, yaptigiYorum));
            filmi.puan = (filmi.puan * filmi.oySayisi + verdigiPuan) / (filmi.oySayisi + 1);
            baseKullanici.yorumlari.Add(new yaptigiYorum(filmi.filmId, verdigiPuan, yaptigiYorum));
        }
        public override double fiyatHesapla()
        {
            return 1.25 * base.fiyatHesapla();
        }

        public premium(kullanici baseKullanici)
        {
            this.baseKullanici = baseKullanici;
        }
    }

        public enum girisStateler
        {
            yok,
            yonetici,
            kullanici,
            yeniKullanici
        }
    
    
    

    internal static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        public static List<film> filmList = new List<film>();
        public static List<kullanici> tumKullaniciler = new List<kullanici>();
        public static kullanici giren;
        public static girisStateler girisYapan;
        public static bool hesapYukseltilecekMi = false;
        public static string yeniFilmAdi;

        [STAThread]
        static void Main()
        {
            string[] oyuncular;

            oyuncular = new string[] { "Tom Hanks", "ali" };
            filmList.Add(new film("Forrest Gump", "Robert Zemeckis", "Dram", "1994", 4.4, 80, oyuncular, "https://upload.wikimedia.org/wikipedia/tr/b/bb/Forrest_Gump_%28film%2C_1994%29.jpg"));
            filmList[0].gelenYorumlar.Add(new yorum("abdulkadir", "izleyince beyninden vurulmuşa döndüm"));
            filmList[0].gelenYorumlar.Add(new yorum("hasan", "keşke bu adamla tanışşam"));

            oyuncular = new string[] { "Tom Hanks", "veli" };
            filmList.Add(new film("Dövüş Kulübü", "David Fincher", "Dram", "1997", 4.3, 14, oyuncular, "https://upload.wikimedia.org/wikipedia/tr/a/ab/D%C3%B6v%C3%BC%C5%9F_Kul%C3%BCb%C3%BC_film_afi%C5%9Fi.jpg"));
            filmList[1].gelenYorumlar.Add(new yorum("abdurrahman", "Ha?"));
            filmList[1].gelenYorumlar.Add(new yorum("hasan", "keşke bu adamla tanışşam"));

            oyuncular = new string[] { "Leonardo DiCaprio", "Tom Hardy", "Ellen Page" };
            filmList.Add(new film("Inception", "Christopher Nolan", "Bilim Kurgu", "2010", 4.9, 90, oyuncular, "https://upload.wikimedia.org/wikipedia/tr/9/94/Ba%C5%9Flang%C4%B1%C3%A7.jpg"));
            filmList[3].gelenYorumlar.Add(new yorum("elif", "harika bir film"));
            filmList[3].gelenYorumlar.Add(new yorum("ayse", "çok etkileyici"));

            oyuncular = new string[] { "Keanu Reeves", "Carrie-Anne Moss", "Hugo Weaving" };
            filmList.Add(new film("The Matrix", "Lana Wachowski", "Aksiyon", "1999", 4.7, 85, oyuncular, "https://upload.wikimedia.org/wikipedia/tr/thumb/3/36/Matrix-film.jpg/330px-Matrix-film.jpg"));
            filmList[4].gelenYorumlar.Add(new yorum("mehmet", "Matrix gerçek değilmiş!"));
            filmList[4].gelenYorumlar.Add(new yorum("sibel", "Neo'nun kıyafetleri harika."));

            oyuncular = new string[] { "Johnny Depp", "Orlando Bloom", "Keira Knightley" };
            filmList.Add(new film("Pirates of the Caribbean: The Curse of the Black Pearl", "Gore Verbinski", "Aksiyon", "2003", 4.5, 75, oyuncular, "https://upload.wikimedia.org/wikipedia/tr/thumb/0/0e/Pirates_of_the_Caribbean_movie.jpg/330px-Pirates_of_the_Caribbean_movie.jpg"));
            filmList[5].gelenYorumlar.Add(new yorum("selin", "Jack Sparrow karakteri muhteşem!"));
            filmList[5].gelenYorumlar.Add(new yorum("ali", "Denizdeki efektler harika."));

            oyuncular = new string[] { "Leonardo DiCaprio", "Joseph Gordon-Levitt", "Ellen Page" };
            filmList.Add(new film("Shutter Island", "Martin Scorsese", "Dram", "2010", 4.8, 95, oyuncular, "https://upload.wikimedia.org/wikipedia/tr/b/b2/Zindan_Adas%C4%B1_film_posteri.jpg"));
            filmList[6].gelenYorumlar.Add(new yorum("ahmet", "Büyük bir twist!"));
            filmList[6].gelenYorumlar.Add(new yorum("zeynep", "Sonu çok şaşırtıcı."));

            oyuncular = new string[] { "Tim Robbins", "Morgan Freeman", "Bob Gunton" };
            filmList.Add(new film("The Shawshank Redemption", "Frank Darabont", "Drama", "1994", 4.9, 95, oyuncular, "https://upload.wikimedia.org/wikipedia/tr/thumb/e/e6/Esaretin-bedeli.jpg/330px-Esaretin-bedeli.jpg"));
            filmList[7].gelenYorumlar.Add(new yorum("emine", "Unutulmaz bir hapishane hikayesi."));
            filmList[7].gelenYorumlar.Add(new yorum("can", "Mükemmel bir film!"));


            oyuncular = new string[] { "Jim Carrey", "Kate Winslet", "Elijah Wood" };
            filmList.Add(new film("Eternal Sunshine of the Spotless Mind", "Michel Gondry", "Drama", "2004", 4.7, 82, oyuncular, "https://upload.wikimedia.org/wikipedia/tr/thumb/4/41/Sil_ba%C5%9Ftan.jpg/330px-Sil_ba%C5%9Ftan.jpg"));
            filmList[8].gelenYorumlar.Add(new yorum("merve", "Unutulmaz bir aşk hikayesi!"));
            filmList[8].gelenYorumlar.Add(new yorum("onur", "Sıradışı bir film."));

            oyuncular = new string[] { "Robert Downey Jr.", "Chris Evans", "Scarlett Johansson" };
            filmList.Add(new film("The Avengers", "Joss Whedon", "Aksiyon", "2012", 4.6, 96, oyuncular, "https://upload.wikimedia.org/wikipedia/tr/d/d8/Avengers_Sonsuzluk_Sava%C5%9F%C4%B1.jpg"));
            filmList[9].gelenYorumlar.Add(new yorum("gizem", "Harika bir kahraman filmi!"));
            filmList[9].gelenYorumlar.Add(new yorum("tolga", "Efektler çok etkileyici."));

            oyuncular = new string[] { "Harrison Ford", "Sean Connery", "Denholm Elliott" };
            filmList.Add(new film("Indiana Jones and the Last Crusade", "Steven Spielberg", "Aksiyon", "1989", 4.8, 90, oyuncular, "https://upload.wikimedia.org/wikipedia/tr/thumb/1/16/Kamcili_Adam_poster.jpeg/330px-Kamcili_Adam_poster.jpeg"));
            filmList[10].gelenYorumlar.Add(new yorum("irem", "Indiana Jones serisinin en iyisi!"));
            filmList[10].gelenYorumlar.Add(new yorum("emre", "Harika bir macera filmi."));

            oyuncular = new string[] { "Tom Cruise", "Kelly McGillis", "Val Kilmer" };
            filmList.Add(new film("Top Gun", "Tony Scott", "Aksiyon", "1986", 4.5, 88, oyuncular, "https://upload.wikimedia.org/wikipedia/tr/thumb/3/34/Tgun.jpg/330px-Tgun.jpg"));
            filmList[11].gelenYorumlar.Add(new yorum("burak", "Maverick'in hikayesi unutulmaz!"));
            filmList[11].gelenYorumlar.Add(new yorum("nur", "Jet sahneleri çok etkileyici."));

            oyuncular = new string[] { "Matt Damon", "Robin Williams", "Ben Affleck" };
            filmList.Add(new film("Good Will Hunting", "Gus Van Sant", "Drama", "1997", 4.8, 92, oyuncular, "https://upload.wikimedia.org/wikipedia/tr/1/17/Good_Will_Hunting_%28film%2C_1997%29.jpg"));
            filmList[12].gelenYorumlar.Add(new yorum("sema", "Harika bir duygusal film!"));
            filmList[12].gelenYorumlar.Add(new yorum("cengiz", "Matt Damon mükemmel performans sergiliyor."));

            oyuncular = new string[] { "Heath Ledger", "Christian Bale", "Aaron Eckhart" };
            filmList.Add(new film("The Dark Knight", "Christopher Nolan", "Aksiyon", "2008", 4.9, 94, oyuncular, "https://upload.wikimedia.org/wikipedia/tr/thumb/4/4d/Kara_%C5%9E%C3%B6valye_TR_film_afi%C5%9Fi.jpg/330px-Kara_%C5%9E%C3%B6valye_TR_film_afi%C5%9Fi.jpg"));
            filmList[13].gelenYorumlar.Add(new yorum("aslı", "Joker karakteri unutulmaz."));
            filmList[13].gelenYorumlar.Add(new yorum("mert", "Batman serisinin en iyisi!"));

            oyuncular = new string[] { "Russell Crowe", "Joaquin Phoenix", "Connie Nielsen" };
            filmList.Add(new film("Gladiator", "Ridley Scott", "Aksiyon", "2000", 4.7, 90, oyuncular, "https://upload.wikimedia.org/wikipedia/tr/8/8d/Gladiator_ver1.jpg"));
            filmList[14].gelenYorumlar.Add(new yorum("gizem", "Savaş sahneleri müthiş!"));
            filmList[14].gelenYorumlar.Add(new yorum("onur", "Russell Crowe harika bir performans sergiliyor."));

            oyuncular = new string[] { "Denzel Washington", "Russell Crowe", "Chiwetel Ejiofor" };
            filmList.Add(new film("American Gangster", "Ridley Scott", "Dram", "2007", 4.6, 88, oyuncular, "https://upload.wikimedia.org/wikipedia/tr/9/9f/American_Gangster_poster.jpg"));
            filmList[15].gelenYorumlar.Add(new yorum("eda", "Mafya filmleri arasında en iyilerden biri!"));
            filmList[15].gelenYorumlar.Add(new yorum("yusuf", "Denzel Washington'un performansı harika."));

            oyuncular = new string[] { "Liam Neeson", "Ben Kingsley", "Ralph Fiennes" };
            filmList.Add(new film("Schindler's List", "Steven Spielberg", "Drama", "1993", 4.8, 90, oyuncular, "https://upload.wikimedia.org/wikipedia/tr/3/38/Schindler%27s_List_movie.jpg"));
            filmList[16].gelenYorumlar.Add(new yorum("selma", "Tarihi bir başyapıt."));
            filmList[16].gelenYorumlar.Add(new yorum("murat", "İzleyince derin düşündüren bir film."));

            oyuncular = new string[] { "Tom Hanks", "Matt Damon", "Tom Sizemore" };
            filmList.Add(new film("Saving Private Ryan", "Steven Spielberg", "Drama", "1998", 4.8, 92, oyuncular, "https://upload.wikimedia.org/wikipedia/tr/thumb/d/d4/Er-Ryani-kurtarmak-poster.png/330px-Er-Ryani-kurtarmak-poster.png"));
            filmList[17].gelenYorumlar.Add(new yorum("ayşe", "Savaş filmleri arasında en iyilerden biri!"));
            filmList[17].gelenYorumlar.Add(new yorum("ali", "Duygusal bir film."));


            oyuncular = new string[] { "Marlon Brando", "Al Pacino", "James Caan" };
            filmList.Add(new film("The Godfather", "Francis Ford Coppola", "Suç", "1972", 4.9, 98, oyuncular, "https://upload.wikimedia.org/wikipedia/tr/7/7c/1972_yap%C4%B1m%C4%B1_Baba_film_afi%C5%9Fi.jpg"));
            filmList[18].gelenYorumlar.Add(new yorum("ayşe", "Sinema tarihindeki en iyi filmlerden biri."));
            filmList[18].gelenYorumlar.Add(new yorum("ali", "Mafya temalı başyapıt."));


            oyuncular = new string[] { "Denzel Washington", "Ethan Hawke", "Scott Glenn" };
            filmList.Add(new film("Training Day", "Antoine Fuqua", "Suç", "2001", 4.6, 88, oyuncular, "https://m.media-amazon.com/images/I/71d12bCw7DL._AC_SX679_.jpg"));
            filmList[19].gelenYorumlar.Add(new yorum("elif", "Denzel Washington'un muazzam performansı!"));
            filmList[19].gelenYorumlar.Add(new yorum("murat", "Gerilim dolu bir film."));

            oyuncular = new string[] { "Matt Damon", "Christian Bale", "Jon Bernthal" };
            filmList.Add(new film("Ford v Ferrari", "James Mangold", "Aksiyon", "2019", 4.5, 92, oyuncular, "https://upload.wikimedia.org/wikipedia/tr/thumb/e/e2/Asfalt%C4%B1n_Krallar%C4%B1_2019_afi%C5%9Fi.jpg/330px-Asfalt%C4%B1n_Krallar%C4%B1_2019_afi%C5%9Fi.jpg"));
            filmList[20].gelenYorumlar.Add(new yorum("eda", "Harika bir yarış filmi!"));
            filmList[20].gelenYorumlar.Add(new yorum("ahmet", "Oscar ödüllü bir yapım."));

            oyuncular = new string[] { "Tom Hanks", "veli" };
            filmList.Add(new film("Yıldızlar Arası", "Christopher Nolan", "Bilim Kurgu", "2014", 4.1, 20, oyuncular, "https://upload.wikimedia.org/wikipedia/tr/b/bc/Interstellar_film_poster.jpg"));





            tumKullaniciler.Add(new kullanici("mahoni06", "1234a", "mahmut", "iflaz", 11111111110, "12/12/2002", kullanici.cinsiyet.erkek, true));
            tumKullaniciler[0].izlenecekler.Add(filmList[0]);

            tumKullaniciler.Add(new kullanici("kaanKZ", "123456", "Kaan", "Kazguc", 39961528874, "04/06/2003", kullanici.cinsiyet.erkek, false));

            tumKullaniciler.Add(new kullanici("selinAydin", "selinpassword", "Selin", "Aydın", 12345678901, "22/08/1995", kullanici.cinsiyet.kadin, true));
            tumKullaniciler[2].izlenecekler.Add(filmList[2]);

            tumKullaniciler.Add(new kullanici("aliB", "parola9999", "Ali", "Bakır", 87654321012, "15/03/1988", kullanici.cinsiyet.erkek, false));
            tumKullaniciler[3].izlenecekler.Add(filmList[3]);

            tumKullaniciler.Add(new kullanici("edaC", "sifre4567", "Eda", "Can", 56473829103, "10/05/2000", kullanici.cinsiyet.kadin, true));
            tumKullaniciler[4].izlenecekler.Add(filmList[4]);

            tumKullaniciler.Add(new kullanici("galacticVoyager", "GalacticTravel", "Galactic", "Voyager", 98765432104, "08/11/1997", kullanici.cinsiyet.erkek, false));
            tumKullaniciler[5].izlenecekler.Add(filmList[5]);

            tumKullaniciler.Add(new kullanici("serendipitySeeker", "Serendipity", "Serendipity", "Seeker", 34567891205, "30/09/1985", kullanici.cinsiyet.kadin, true));
            tumKullaniciler[6].izlenecekler.Add(filmList[6]);

            tumKullaniciler.Add(new kullanici("zenMaster", "ZenMaster1", "Zen", "Master", 56789012306, "18/02/1996", kullanici.cinsiyet.erkek, false));
            tumKullaniciler[7].izlenecekler.Add(filmList[7]);

            tumKullaniciler.Add(new kullanici("lunarPhoenix", "LunarFire", "Lunar", "Phoenix", 12345678907, "25/07/1980", kullanici.cinsiyet.kadin, true));
            tumKullaniciler[8].izlenecekler.Add(filmList[8]);

            tumKullaniciler.Add(new kullanici("quantumPioneer", "QuantumPioneer", "Quantum", "Pioneer", 87654321008, "02/04/1992", kullanici.cinsiyet.erkek, false));
            tumKullaniciler[9].izlenecekler.Add(filmList[9]);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);




            
            do
            {
                girisYapan = girisStateler.yok;

                Application.Run(new Form4());

                if(girisYapan == girisStateler.yonetici)
                    Application.Run(new Form2());

                if (girisYapan == girisStateler.kullanici)
                    Application.Run(new Form1());
                if (girisYapan == girisStateler.yeniKullanici)
                    Application.Run(new Form5());
            } while (girisYapan != girisStateler.yok);
        }
    }
}
