using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciProje
{
    internal class Program
    {
       
            static List<Personel> personelList = new List<Personel>();

            static void Main(string[] args)
            {
                Menu();
                Console.ReadLine();
            }
            static void Menu()
            {
                Console.WriteLine("1- Personel Listesi Oluştur ");
                Console.WriteLine("2- Personel Listesi Göster");
                Console.WriteLine("3- Silme İşlemi");
                Console.WriteLine("4- Güncelleme İşlemi");
                Console.WriteLine("5- Ekleme İşlemi");
                Console.WriteLine("6- Detaylar");
            string secim = Console.ReadLine();
                if (secim == "1")
                {
                    CreateList();
              
                }
                else if(secim == "2")
                {
            
                perList(personelList);
                Clear();

                 }
                else if(secim == "3")
                {
                PersonelDelete();
                }
                else if(secim == "4")
                {
                PersonelUpdate();
                }
                else if (secim == "5")
                {
                PersonelAdd();
                }
                else if (secim == "6")
                {
                PersonelDetails();
                }
            Menu();
            }
            static void Clear()
            {
            Console.ReadLine();
            Console.Clear();
            }
            static void PersonelDelete()
            {
            Console.WriteLine("Silinecek Personel ID:");
            int silinecekId = Convert.ToInt32(Console.ReadLine());
            Personel silinecekPersonel = personelList.Where(x => x.id == silinecekId).FirstOrDefault();
            personelList.Remove(silinecekPersonel);
            perList(personelList);

            }
            static void PersonelUpdate()
            {
            Console.WriteLine("Güncellenecek Personel ID:");
            int guncelId = Convert.ToInt32(Console.ReadLine());
            Personel guncelPersonel = personelList.Where(x => x.id == guncelId).FirstOrDefault();
            Console.WriteLine("İsim Giriniz");
            string isim =Console.ReadLine();
            Console.WriteLine("Soyad Giriniz");
            string soyad = Console.ReadLine();
            guncelPersonel.ad = isim;
            guncelPersonel.soyad = soyad;
            perList(personelList);

            } 
            
            static void PersonelAdd()
            {
            Personel yPersonel = new Personel();
            Console.WriteLine("İsim Giriniz");
            string isim = Console.ReadLine();
            Console.WriteLine("Soyad Giriniz");
            string soyad = Console.ReadLine();
            yPersonel.id = personelList.Max(x => x.id) + 1;
            yPersonel.ad = isim;
            yPersonel.soyad = soyad;
            personelList.Add(yPersonel);
            perList(personelList);
    
            }
            
            static void PersonelDetails()
            {
            Console.WriteLine("Personel ID:");
            int Id = Convert.ToInt32(Console.ReadLine());
            Personel secilenPersonel = personelList.Where(x => x.id == Id).FirstOrDefault();
            Console.WriteLine("     Detaylar   ");
            Console.WriteLine("****************");
            Console.WriteLine(secilenPersonel.Unvan() + "-"+secilenPersonel.yas());
            Console.WriteLine("Adres:");
            var aa = secilenPersonel.AdresAl();
            foreach (var item in secilenPersonel.AdresAl())
            {
                Console.WriteLine(item);
            }
           
            Console.WriteLine("****************");
            Clear();

        }
 
        static void perList(List<Personel> ls)
            {
                Console.WriteLine("id      ad      soyad      maas  ");
                Console.WriteLine("*********************************");
                foreach (var item in ls)
                {
                    Console.WriteLine($"{item.id} {item.ad} {item.soyad} {item.maas}");
                }
            ToplamAl(ls);
            }
        static void ToplamAl(List<Personel>ls)
        {
           // 1.yol
           // int toplamMaas = 0;
           // int ortMaas = 0;
           // int toplamKisi = 0;
           // int toplamErkek = 0;
           // int toplamKadin = 0;
           // int toplamErkekMaas = 0;
           // int toplamKadinMaas = 0;
           // foreach(var item in ls)
           // {
              //  toplamKisi++;
              //  toplamMaas += item.maas;

              //  if(item.cinsiyet == "E")
              //   {
                   // toplamErkek++;
                   // toplamErkekMaas +=item.maas;

               /// }
               // else
               // {
                    // toplamKadin++;
                    // toplamKadinMaas+=item.maas;
              // }

           // }
         
            //ortMaas = toplamMaas / toplamKisi;

            //2.yol Lampda Expression
            //8 foreach yaratıldığı için 1.yola göre daha düşük performansta çalışır.
           int toplamKisi = ls.Count;
           int toplamErkek=ls.Where(x => x.cinsiyet == "E").Count();
           int toplamKadin = ls.Where(x => x.cinsiyet == "K").Count();
           int toplamErkekMaas = ls.Where(x => x.cinsiyet == "E").Sum(x => x.maas);
           int toplamKadinMaas = ls.Where(x => x.cinsiyet == "K").Sum(x => x.maas);
           int toplamMaas = ls.Sum(x => x.maas);
                //  foreach (var item in ls)
                //  {
                //  toplamKisi++;
                //  toplamMaas += item.maas;
                //  }
           double ortMaas = ls.Average(x => x.maas);
           double ortKadinMaas=ls.Where(x => x.cinsiyet == "K").Average(x => x.maas);

            Console.WriteLine($"Toplam Kisi: {toplamKisi}");
            Console.WriteLine($"Toplam Erkek:{toplamErkek}");
            Console.WriteLine($"Toplam Kadın:{toplamKadin}");
            Console.WriteLine($"Toplam Maas:{toplamMaas }");
            Console.WriteLine($"Ortalama Maas:{ortMaas}");
        }
            static void CreateList()
            {
                Random random = new Random();
                for (int i = 1; i < 21; i++)
                {
                    Personel personel = new Personel();
                    personel.ad = FakeData.NameData.GetFirstName();
                    personel.id = i;
                    personel.soyad = FakeData.NameData.GetSurname();
                    personel.cadde = FakeData.PlaceData.GetStreetName();
                    int yas = random.Next(20, 50);
                    personel.dogumTarih = DateTime.Now.AddYears(yas * -1);
                    int cns = random.Next(0, 2);
                    if (cns == 0)
                        personel.cinsiyet = "E";
                    else
                        personel.cinsiyet = "K";
                    personel.ilce = FakeData.PlaceData.GetCountry();
                    personel.sehir = FakeData.PlaceData.GetCity();
                    personel.kapiNo = random.Next(1, 130);
                    personel.maas = random.Next(3000, 50000);
                    personelList.Add(personel);
                }
            }
        }

    }
