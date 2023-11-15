using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Fatura_Kalem
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.Write("Seçiminizi yapın: \n\n");
			Menu();

			static void Menu()
			{
				Console.WriteLine("----------------------------------------------");
				Console.WriteLine("1. Fatura Oluştur");
				Console.WriteLine("2. Kesilen Faturalar");
				Console.WriteLine("----------------------------------------------\n\n");
				Console.WriteLine("Lütfen seçmek istediğin menünün numarasını yaz");
				Console.WriteLine("Örnek fatura oluşturmak için '1'");
				string menu = Console.ReadLine();
				Console.Clear();

				if (menu == "1")
				{
					Console.WriteLine("Fatura Oluştur işlemi seçtiniz.");
					FaturaOlustur();
				}
				else if (menu == "2")
				{

					Console.WriteLine("Kesilen Faturalar işlemi seçtiniz.");
					Console.Write("Bakmak istediğiniz Fatura için Müşteri/ Şirket adı seçiniz : \n\n");
					List<string> faturalar = Directory.GetFiles("C:\\Fatura").ToList<string>();
					int count = 1;
					foreach (string s in faturalar)
					{
						Console.WriteLine(count + " - " + s);
						count++;
					}
					int siraNo = int.Parse(Console.ReadLine());
					Console.Clear();

					string dosyaAdi = faturalar[siraNo - 1];
					KesilenFaturaOkuma(dosyaAdi);
					string oku = File.ReadAllText(dosyaAdi);
					Console.WriteLine(oku);


				}
				else
				{
					Console.WriteLine("Geçersiz seçim yaptınız.");
				}
			}
			static void FaturaOlustur()
			{
				Console.Clear();
				Console.WriteLine("--------------------------------------------------------------------------------");
				Random rnd = new Random();
				int faturaNo = rnd.Next(100000000, 999999999);
				string tarih = DateTime.Now.ToString("dd-MM-yyyy");
				Console.WriteLine("Müşteri/ Şirket adı giriniz");
				string musteri = Console.ReadLine();
				Console.WriteLine("Adres bilgisi giriniz");
				string adres = Console.ReadLine();
				Console.WriteLine("Vergi numarası giriniz");
				string vergiNo = Console.ReadLine();
				Console.Clear();
				Console.WriteLine("--------------------------------------------------------------------------------");
				Console.WriteLine("Kaç kalem hizmet olacak");
				int hizmetKalem = int.Parse(Console.ReadLine());
				for (int i = 0; i < hizmetKalem; i++)
				{
					Console.WriteLine($"Kalem hizmet sayısı : {hizmetKalem}");
					Console.WriteLine($"{i + 1}. Hizmet\n");
					Console.Write("Hizmet Adı : ");
					string hizmetAdi = Console.ReadLine();
					Console.Write("Hizmet Adedi : ");
					int hizmetAdet = int.Parse(Console.ReadLine());
					Console.Write("Birim Fiyatı : ");
					double birimFiyat = double.Parse(Console.ReadLine());
					#region islem
					Console.WriteLine("--------------------------------------------------------------------------------");

					double topFiyat = hizmetAdet * birimFiyat;
					Console.Write($"Toplam Fiyatı = {topFiyat}\n");
					Console.WriteLine("--------------------------------------------------------------------------------");

					double kdv = topFiyat * 18 / 100;
					Console.Write($"KDV (%18) = {kdv}\n");

					Console.WriteLine("--------------------------------------------------------------------------------");
					double genelTop = kdv + topFiyat;
					Console.Write($"Genel Toplam = {genelTop}\n");
					#endregion
					string faturaNumarası = faturaNo.ToString();
					string birimF = birimFiyat.ToString();
					string toplamF = topFiyat.ToString();
					string genelF = genelTop.ToString();
					string metin = $"Fatura Numarası : {faturaNumarası}\nTarih : {tarih}\nMüşteri/ Şirket Adı : {musteri}\n" +
						$"Adres : {adres}\nVergi Numarası : {vergiNo}\n----------------------------------\n" +
						$"Hizmet Adı : {hizmetAdi}\n Hizmet Adedi : {hizmetAdet}\nBirim Fiyat : {birimF}\n----------------------------------\n" +
						$"Toplam Fiyat : {toplamF}\nGenel Toplam : {genelF}\n";
					KesilenFaturaYazdirma($"c:\\Fatura\\{musteri}.txt", metin);
				}
				Console.WriteLine("Fatura Oluşturuldu");
				Thread.Sleep(2000); //console ekranında 2 saniye bekletir
				Console.Clear();
			}
			static void KesilenFaturaOkuma(string path)
			{
				File.ReadAllText(path);
			}
			static void KesilenFaturaYazdirma(string path, string deger)
			{
				File.AppendAllText(path, deger);
			}
		}
	}
}