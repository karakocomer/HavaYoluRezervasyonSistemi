using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportReservationSystem
{
    class Program
    {
        static string[] koltuklar = new string[20];
        static void Main(string[] args)
        {
            do
            {
                StartReservationApp();
            } while (true);
        }
        static void StartReservationApp()
        {
            string sinif = SinifSec();
            bool rezerveEdildiMi;
            int koltukNo;
            do
            {
                string secim = "";
                do
                {
                    secim = "";
                    koltukNo = BosKoltuklariListele(sinif);
                    if (koltukNo == -1)
                    {
                        if (sinif == "business")
                        {
                            Console.WriteLine("----------------------------------------------------------------------");
                            Console.WriteLine("Economy Class bölümünde boş koltukları görmek ister misiniz? (E/H)");
                            secim = Console.ReadKey().KeyChar.ToString();
                            if (secim.ToLower() == "e")
                            {
                                sinif = "economy";
                            }
                        }
                        else
                        {
                            Console.WriteLine("----------------------------------------------------------------------");
                            Console.WriteLine("Business Class bölümünde boş koltukları görmek ister misiniz? (E/H)");
                            secim = Console.ReadKey().KeyChar.ToString();
                            if (secim.ToLower() == "e")
                            {

                                sinif = "business";
                            }
                        }
                    }
                    else if (!String.IsNullOrEmpty(koltuklar[koltukNo - 1]))
                    {
                        if (sinif == "business")
                        {
                            Console.WriteLine("----------------------------------------------------------------------");
                            Console.WriteLine("Business Class bölümündeki {0} numaralı koltuğu daha önce {1} isimli yolcuya rezerve ettiniz\nLütfen boş koltuklardan birini seçiniz.", koltukNo, koltuklar[koltukNo - 1]);
                        }
                        else
                        {
                            Console.WriteLine("----------------------------------------------------------------------");
                            Console.WriteLine("Economy Class bölümündeki {0} numaralı koltuğu daha önce {1} isimli yolcuya rezerve ettiniz\nLütfen boş koltuklardan birini seçiniz.", koltukNo, koltuklar[koltukNo - 1]);
                        }
                    }
                } while ((koltukNo == -1 && secim.ToLower() == "e") || (koltukNo >= 0 && !String.IsNullOrEmpty(koltuklar[koltukNo - 1])));
                if (secim.ToLower() == "h")
                {
                    Console.WriteLine("----------------------------------------------------------------------");
                    Console.WriteLine("Bir sonraki uçuşlar 4 saat sonradır.");
                    return;
                }
                string yolcuAdi = YolcununAdiniAl(koltukNo);
                rezerveEdildiMi = RezerveEt(sinif, koltukNo, yolcuAdi);
                if (rezerveEdildiMi)
                {
                    Console.WriteLine("----------------------------------------------------------------------");
                    Console.WriteLine("Devam etmek için bir tuşa basınız.");
                    Console.WriteLine("----------------------------------------------------------------------");
                    Console.ReadKey(true);
                }
            } while (rezerveEdildiMi == false);
        }
        static string YolcununAdiniAl(int koltukNo)
        {
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("Seçilen koltuk numarası: {0}", koltukNo);
            Console.WriteLine("----------------------------------------------------------------------");
            Console.Write("Lütfen yolcunun Adını ve Soyadını giriniz: ");
            return Console.ReadLine();
        }
        static string SinifSec()
        {
            string tus = "";
            Console.WriteLine("1. Business Class bölümü için 1 tuşuna basın.");
            Console.WriteLine("2. Economy Class bölümü için 2 tuşuna basın.");
            Console.WriteLine("----------------------------------------------------------------------");
            do
            {
                tus = Console.ReadKey(true).KeyChar.ToString();
            } while (tus != "1" && tus != "2");
            if (tus == "1")
            {
                return "business";
            }
            else
            {
                return "economy";
            }
        }
        static int BosKoltuklariListele(string sinif)
        {
            if (sinif == "business")
            {
                bool bosKoltukVarMi = false;
                Console.WriteLine("Business Class bölümünde kalan boş koltuklar:");
                for (int i = 0; i < 8; i++)
                {
                    if (String.IsNullOrEmpty(koltuklar[i]))
                    {
                        Console.WriteLine("- {0}", i + 1);
                        bosKoltukVarMi = true;
                    }
                }
                Console.WriteLine("----------------------------------------------------------------------");
                if (bosKoltukVarMi == false)
                {
                    Console.WriteLine("----------------------------------------------------------------------");
                    Console.WriteLine("Seçtiğiniz Business Class bölümünde boş koltuk kalmamıştır.");
                    return -1;
                }
            }
            else if (sinif == "economy")
            {
                bool bosKoltukVarMi = false;
                Console.WriteLine("Economy Class bölümünde kalan boş koltuklar:");
                for (int i = 8; i < 20; i++)
                {
                    if (String.IsNullOrEmpty(koltuklar[i]))
                    {
                        Console.WriteLine("- {0}", i + 1);
                        bosKoltukVarMi = true;
                    }
                }
                if (bosKoltukVarMi == false)
                {
                    Console.WriteLine("----------------------------------------------------------------------");
                    Console.WriteLine("Seçtiğiniz Economy Class bölümünde boş koltuk kalmamıştır.");
                    return -1;
                }
            }
            string koltukNoAsText = "";
            bool sayiMi = false;
            int koltukNo;
            do
            {
                koltukNoAsText = Console.ReadLine();
                sayiMi = Int32.TryParse(koltukNoAsText, out koltukNo);
            } while (sayiMi == false);
            return koltukNo;
        }
        static bool RezerveEt(string sinif, int koltukNo, string yolcuAdi)
        {
            if (sinif == "business" && koltukNo > 0 && koltukNo <= 8)
            {
                if (String.IsNullOrEmpty(koltuklar[koltukNo - 1]))
                {
                    koltuklar[koltukNo - 1] = yolcuAdi;
                    Console.WriteLine("----------------------------------------------------------------------");
                    Console.WriteLine("Business Class bölümündeki {0} numaralı koltuğu {1} isimli yolcuya rezerve ettiniz.", koltukNo, yolcuAdi);
                    return true;
                }
                else
                {
                    Console.WriteLine("----------------------------------------------------------------------");
                    Console.WriteLine("{0} numaralı koltuğu daha önce \"{1}\" adlı kişiye rezerve ettiniz.", koltukNo, koltuklar[koltukNo - 1]);
                    Console.WriteLine("Lütfen boş koltuklardan birisini seçiniz.");
                    Console.WriteLine("----------------------------------------------------------------------");
                    return false;
                }
            }
            else if (sinif == "economy" && koltukNo > 8 && koltukNo <= 20)
            {
                if (String.IsNullOrEmpty(koltuklar[koltukNo - 1]))
                {
                    koltuklar[koltukNo - 1] = yolcuAdi;
                    Console.WriteLine("----------------------------------------------------------------------");
                    Console.WriteLine("Economy Class bölümündeki {0} numaralı koltuğu {1} isimli yolcuya rezerve ettiniz.", koltukNo, yolcuAdi);
                    return true;
                }
                else
                {
                    Console.WriteLine("----------------------------------------------------------------------");
                    Console.WriteLine("{0} numaralı koltuğu daha önce \"{1}\" adlı kişiye rezerve ettiniz.", koltukNo, koltuklar[koltukNo - 1]);
                    Console.WriteLine("Lütfen boş koltuklardan birisini seçiniz.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("----------------------------------------------------------------------");
                Console.WriteLine("Geçersiz sınıf veya koltuk numarası girildi. Lütfen tekrar deneyiniz.");
                return false;
            }
        }
    }
}
