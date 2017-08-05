using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;
using System.Diagnostics;

namespace RSAProjectSSIYLİ
{
    class Program
    {
     
        static Random random = new Random();
        private static int sifrelimetin;
        private static int dekriptmetin;

        private static int a, b;
        
        static void Main(string[] args)
        {
            
            Console.WriteLine("Gönderilecek mesajı giriniz(Sayı):");
            int mesaj = Int32.Parse(Console.ReadLine());
            {
                Stopwatch watch = new Stopwatch();
                // P ve Q rastgele üretilen iki asal sayıdır.
                watch.Start();
                //for (int i = 0; i < 1000000; i++)
                //{
                    byte[] asalsayi = BölünemezSayi();
                     byte p = asalsayi[random.Next(1, asalsayi.Length)],
                      q = asalsayi[random.Next(1, asalsayi.Length)];
        //n değeri hesaplanır.
                int n = p * q;
                //totient fonksiyonu hesaplanır.φ(n) = (p-1)(q-1)
                int totient = (p - 1) * (q - 1);
                int e = 2;
                int sayac;
                while (e < totient)
                {
                    sayac = GCDAlgoritması(e, totient);
                    if (sayac == 1)
                        break;
                    else
                        e++;
                //}
                int z = e;//totient fonksiyonu ile aralarında asal olan 1 den büyük bir sayı bulunur.
                //de ≡ 1 mod(n) olacak şekilde d sayısı bulunur 
                int d = ExtendedAlgoritması(z, totient);
                  
                    //c = m^emodn bulunur.

                   sifrelimetin = ModveÜsAlma(mesaj, z, n);
                    //m = cd mod n
                dekriptmetin = ModveÜsAlma(sifrelimetin, d, n);

            }
          
                Console.WriteLine("RSA İle Şifrelenmiş Metin:{0}", sifrelimetin);
                Console.WriteLine("RSA İle Şifrelenmiş Metin:{0}", dekriptmetin);
                watch.Stop();
                Console.WriteLine("Hesaplama Süresi: {0},{1},{2}", watch.Elapsed.Minutes, watch.Elapsed.Seconds, watch.Elapsed.Milliseconds);
                

            }
            Console.WriteLine("Diğer algoritmayı hesaplamak için bir tuşa basınız:");
            Console.ReadKey();

            //-ELGAMAL ŞİFRELEME-
            Stopwatch watch_bir = new Stopwatch();
            watch_bir.Start();
            for (int i = 0; i < 1000000; i++)
            {
                byte[] asalsayi1 = BölünemezSayi();
                byte y = asalsayi1[random.Next(1, asalsayi1.Length)];
                
                int G = random.Next(1, (y - 1));
                int X = random.Next(1, 50);

                int Y = ModveÜsAlma(G, X, y);
                int k = random.Next(1, (y - 2));
                a = ModveÜsAlma(G, k, y);
                int bdegeri = ModveÜsAlma(Y, k, y);
                b = mesaj * bdegeri;
            }
           
            Console.WriteLine("Elgamal İle Şifrelenmiş Çift:{0},{1}", a, b);
            watch_bir.Stop();
            Console.WriteLine("Hesaplama Süresi: {0},{1},{2}", watch_bir.Elapsed.Minutes, watch_bir.Elapsed.Seconds, watch_bir.Elapsed.Milliseconds);
            Console.ReadKey();
        }

        static int ModveÜsAlma(int deger, int üs, int modunualma)
        {
            int sonuc = deger;
            for (int i = 0; i < üs - 1; i++)
            {
                sonuc = (sonuc * deger) % modunualma;
            }
            return sonuc;
        }
        static int ExtendedAlgoritması(int a, int b)
        {
            int  b0 = b, t, q;
            int x0 = 0, x1 = 1;
            if (b == 1) return 1;
            while (a > 1)
            {
                
                q = (a / b);
                t = b; b = a % b; a = t;
                t = x0; x0 = x1 - q * x0; x1 = t;
            }
            if (x1 < 0) x1 += b0;
            return x1;
        }


        static int  GCDAlgoritması(int numara1, int numara2)
        {
            int kalan;

            while (numara2 != 0)
            {
                kalan = numara1 % numara2;
                numara1 = numara2;
                numara2 = kalan;
            }

            return numara1;
        }

        static private byte[] BölünemezSayi()
        {
            List<byte> notDivideable = new List<byte>();


            for (int x = 2; x < 256; x++)
            {
                int n = 0;
                for (int y = 1; y <= x; y++)
                {
                    if (x % y == 0)
                        n++;
                }

                if (n <= 2)
                    notDivideable.Add((byte)x);
            }
            return notDivideable.ToArray();
        }
    }
}
   



