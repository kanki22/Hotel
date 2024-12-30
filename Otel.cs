using System;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;


namespace Otel
{
    abstract class Personal
    {
        public string isim { get; set; }
        public string telefon { get; set; }
        public static int daireSayisi { get; set; } = 16;
        public static bool[] daireDurumu { get; set; } = new bool[daireSayisi];
        public int daireNumara { get; set; }

        public int gun;
        public int Gun
        {
            get { return gun; }
            set
            {
                if (value > 7)
                {
                    Console.WriteLine("7 gun ustu rezervasyon edemezsiniz");
                    return;
                }
                else
                {
                    gun = value;
                }
            }
        }

    }
    
         
        
    interface ITemizleme
    {
        void mutfakTemizle();
        void odaTemizle();
    }
    interface IPisirme 
    {
        void kahvalti();
        void oglenYemegi();
        void aksamYemegi();
    }
    class Temizleyici : ITemizleme
    {
        public void mutfakTemizle()
        {
            Console.WriteLine("temizleyici mutfagi temizliyor");
        }
        public void odaTemizle()
        {
            Console.Write("temizleyici odayi temizliyor");
        }
    }
    class Asci : IPisirme
    {
        public void kahvalti() 
        { 
            Console.WriteLine("Kahvalti hazirdir");
        }
        public void oglenYemegi() 
        { 
            Console.WriteLine("Oglen yemegi hazirdir");
        }
        public void aksamYemegi() 
        { 
            Console.WriteLine("Aksam yemegi hazirdir");
        }
    }
     
                
    class Odeme : Personal
    {
        private int fiyat { get; set; } = 2000;
        public int hesapla()
        {
            return fiyat *= Gun;    
        }
    }
    class Musteri : Odeme
    {
        public static Musteri[] musteriler { get; set; } = new Musteri[daireSayisi];
        public static int musteriSayisi { get; set; } = 0;

        public static void rezerv(int daireNumara, string isim, string telefon, int gun)
        {

            if ( daireNumara < 1 || daireNumara > 16 )
            {
                Console.WriteLine("Daire 1 - 16 arasinda olmasi gerekir");         //? eger yanlis numara girirldi
                return;
            }
            else if ( daireDurumu[daireNumara - 1] )
            {                             //? eger daire onceden alinmis
                Console.WriteLine("Daire doludur..");
                return;
            }
            else if ( gun > 7) 
            { 
                Console.WriteLine("7 gun ustu rezervasyon edemezsiniz");
                return;
            }
                    
            Oda musteri = new Oda( daireNumara, isim, telefon, gun );

            musteriler[musteriSayisi] = musteri;
            musteriSayisi++;

            daireDurumu[daireNumara - 1] = true;   //? Daireleri doldurma islemi
            daireSayisi--;                       //? daire alindiktan sonra daire sayisini azaltiyoruz
                    
            int fiyat = musteri.hesapla();
            Console.WriteLine("Toplam : " + fiyat + "TL");
            Console.WriteLine("Daire " + daireNumara + " basariyla " + isim + " tarafindan rezerv edilmistir.");
        }
     }

    class Oda : Musteri
    {

        public Oda( int daireNumara, string isim, string telefon, int gun )
        {                                             //? constructure daire rezervasyonu

            this.daireNumara = daireNumara;      //? isimler ayni oldu icin this keywordu kullaniyoruz
            this.isim = isim;            //? isimler ayni oldu icin this keywordu kullaniyoruz
            this.telefon = telefon;     //? isimler ayni oldu icin this keywordu kullaniyoruz
            this.gun = gun;
        }
        public static void goster()
        {

            Console.WriteLine("\nMusterilerin bilgileri\n");
            for ( int i = 0; i < musteriSayisi; i++ )
            {
                Console.WriteLine("\n"+(i + 1) + ". Musteri kayidi : \n");
                var musteri = musteriler[i];

                Console.WriteLine("Musteri Adi : " + musteri.isim + "\nMusteri cep telefonu : " + musteri.telefon);
                Console.WriteLine("Daire : " + musteri.daireNumara + "\nSure : " + musteri.gun + " gun");
            }
        }

        public static int bosDaireSayisi()
        {
            return daireSayisi;                     //? islemlerden sonra bos daire sayisi
        }

    }
    class Program
    {
        static void Main()
        {
            int sec;
            string isim;
            int gun;
            string telefon;
            int daire;


            while (true)
            {
                Console.Write("\nOtelimize Hosgeldiniz \n");

                Console.Write("isim soyisim : ");
                isim = Console.ReadLine();

                Console.Write("Cep telefon : ");
                telefon = Console.ReadLine();

                Console.WriteLine("Bos daire sayisi : " + Oda.bosDaireSayisi());

                Console.Write("Hangi daire istiyorsunuz : ");
                daire = Convert.ToInt32(Console.ReadLine());

                Console.Write("Kac gun : ");
                gun = Convert.ToInt32(Console.ReadLine());

                Oda.rezerv(daire, isim, telefon, gun);

                Console.WriteLine("Baska musteri eklemek istiyormusun? (evet/hayir)");
                if (Console.ReadLine() != "evet")
                {
                    break;
                }


            }

            Oda.goster();
            Oda.bosDaireSayisi();
            Console.WriteLine("---------------");

            Temizleyici obj = new Temizleyici();
            Asci obj2 = new Asci();
            Console.WriteLine("1.Temizleme  2.Yemek");
            sec = Convert.ToInt32(Console.ReadLine());
            if (sec == 1)
            {
                obj.mutfakTemizle();
            }
            else if (sec == 2) 
            {
                obj2.kahvalti();
            }
        }
    }
}
