using MoviesClassLibrary.Models;
using System.Globalization;

namespace MoviesClassLibrary.Data
{
    public static class Veriler
    {
        public static string KayitBulunamadiMesaji => "Kayıt bulunamadı.";
        public static string HataMesaji => "İşlem sırasında hata meydana geldi!";
        public static int EnSonId { get; set; } = 0;
        public static List<Film> Filmler { get; set; }
        public static List<Yonetmen> Yonetmenler { get; set; }

        static Veriler()
        {
            Yonetmenler = new List<Yonetmen>()
            {
                new Yonetmen("James","Cameron", DateTime.Parse("16.08.1954", new CultureInfo("tr-TR")), true, ++EnSonId),
                new Yonetmen("Guy","Ritchie", DateTime.Parse("10.09.1968", new CultureInfo("tr-TR")), false, ++EnSonId)   // new DateTime(1968, 9, 10)
            };

            Filmler = new List<Film>()
            {
                new Film()
                {
                    Id = ++EnSonId,
                    Adi = "Avatar",
                    YapimYili = 2009,
                    Gisesi = 1000000,
                    Yonetmeni = Yonetmenler.FirstOrDefault()
                },
                new Film()
                {
                    Id = ++EnSonId,
                    Adi = "Sherlock Holmes",
                    YapimYili = 2009,
                    Gisesi = 2000000,
                    Yonetmeni = Yonetmenler.LastOrDefault()
                }
            };
        }
        public static string KayitSayisiMesajiGetir(int kayitSayisi) => kayitSayisi == 0 ? KayitBulunamadiMesaji : kayitSayisi + " kayıt bulundu.";       
    }
}
