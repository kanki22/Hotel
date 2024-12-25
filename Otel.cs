using System;
using System.ComponentModel;

namespace Otel{

   class Program{
        abstract class Rezervasyon{
            public string isim {get; set;}
            public string telefon {get; set;}
            public static int daireSayisi {get; set;} = 16;
            public static bool[] daireDurumu {get; set;} = new bool[daireSayisi];
            public int daireNumara {get; set;}
            public int gun;
            public int Gun {
               get{ return gun; } 

               set{ 
                  if( value > 7 ){
                     Console.WriteLine("7 gun ustu rezervasyon edemezsiniz");
                     return;
                  }
                  else{
                     gun = value;
                  }
               
               }
               
            
            }
            
        }
        class Odeme : Rezervasyon{   
            public int fiyat {get; set;} = 2000;
            public int hesapla(){ 
               return fiyat *= Gun;
            }

            

        }
        class Kisi : Odeme{
            public static Kisi[] musteriler {get; set;} = new Kisi[daireSayisi];
            public static int musteriSayisi {get; set;} = 0;

            public static void rezerv (int daireNumara, string isim, string telefon, int gun){

                  if (daireNumara < 1 || daireNumara > 16){
                     Console.WriteLine("Daire 1 - 16 arasinda olmasi gerekir");         //? eger yanlis numara girirldi
                     return;
                  }
                  else if ( daireDurumu[ daireNumara-1 ] ){                             //? eger daire onceden alinmis
                     Console.WriteLine("Daire doludur..");
                     return;
                  }
                  else if ( gun > 7 ){
                     Console.WriteLine("7 gun ustu rezervasyon edemezsiniz");
                     return;
                  }
                  
                  Oda musteri = new Oda(daireNumara, isim, telefon, gun);
                  
                  musteriler[musteriSayisi] = musteri;
                  musteriSayisi++;

                  daireDurumu[daireNumara-1] = true;   //? Daireleri doldurma islemi
                  daireSayisi--;                       //? daire alindiktan sonra daire sayisini azaltiyoruz

                  int fiyat = musteri.hesapla();       //? fiyat hesaplama
                  Console.WriteLine("Toplam : "+fiyat+"TL");
                  Console.WriteLine("Daire "+daireNumara+" basariyla "+isim+" tarafindan rezerv edilmistir.");
            }


        }
        
        class Oda : Kisi{
            
            public Oda(int daireNumara, string isim, string telefon, int gun){                                             //? constructure daire rezervasyonu
               
               this.daireNumara = daireNumara;      //? isimler ayni oldu icin this keywordu kullaniyoruz
               this.isim = isim;            //? isimler ayni oldu icin this keywordu kullaniyoruz
               this.telefon = telefon;            //? isimler ayni oldu icin this keywordu kullaniyoruz
               this.gun = gun;
            }
            public static void goster(){
   
               Console.WriteLine("\nMusterilerin bilgileri\n");
               for(int i=0; i<musteriSayisi; i++){
                  Console.WriteLine("\n"+(i+1)+". Musteri kayidi : \n");
                  var musteri = musteriler[i];

                  Console.WriteLine("Musteri Adi : "+musteri.isim+"\nMusteri cep telefonu : "+musteri.telefon);
                  Console.WriteLine("Daire : "+musteri.daireNumara+"\nSure : "+musteri.gun+" gun");
               }
            }
            
            public static int bosDaireSayisi(){
               return daireSayisi;                                                //? islemlerden sonra bos daire sayisi
            }

        }
        
    
        static void Main(){

            string isim;
            int gun;
            string telefon;
            int daire;
            
            
            while( true ){
               Console.Write("\nOtelimize Hosgeldiniz \n");

               Console.Write("isim soyisim : ");
               isim = Console.ReadLine();

               Console.Write("Cep telefon : ");
               telefon = Console.ReadLine();

               Console.WriteLine("Bos daire sayisi : "+Oda.bosDaireSayisi() );

               Console.Write("Hangi daire istiyorsunuz : ");
               daire = Convert.ToInt32(Console.ReadLine());

               Console.Write("Kac gun : ");
               gun = Convert.ToInt32(Console.ReadLine());

               
               Oda.rezerv (daire,isim,telefon,gun);

               Console.WriteLine("Baska musteri kaydetmek istiyormusun? (evet/hayir)");
               if ( Console.ReadLine() != "evet"){
                  break;
               }
               
             
            }
            Oda.goster();
            Oda.bosDaireSayisi();

            //Console.WriteLine(musteri1.Gun+" gundur");
            //Console.WriteLine("fiyat = "+musteri1.Fiyat);


        }
        
   }
}