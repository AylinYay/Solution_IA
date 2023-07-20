using MoviesClassLibrary.Data;
using MoviesClassLibrary.Models;
using MoviesClassLibrary.Models.Bases;
using MoviesClassLibrary.Services;
using System.Globalization;
using System.Threading.Channels;

namespace MoviesConsoleApp
{
    internal class Program
    {
        private static YonetmenService _yonetmenService = new YonetmenService();
        private static FilmService _filmService = new FilmService();

        static void Main(string[] args)
        {
            string giris = MenuGetir();

            while (giris != "0")
            {
                switch (giris)
                {
                    case "1":
                        FilmleriListele();
                        break;
                    case "2":
                        AdaGoreFilmleriListele();
                        break;
                    case "3":
                        IdyeGoreFilmleriListele();
                        break;
                    case "4":
                        FilmEkle();
                        break;
                    case "5":
                        FilmGuncelle();
                        break;
                    case "6":
                        FilmSil();
                        break;
                }
                giris = MenuGetir();
            }
        }

        

        private static string MenuGetir()
        {
            Console.WriteLine("\nİşlem Seçiniz\n" +
                "0: Çıkış\n" +
                "1: Tüm filmleri listele\n" +
                "2: Ada göre filmleri listele\n" +
                "3: ID'ye göre filmi göster\n" +
                "4: Film ekle\n" +
                "5: Film güncelle\n" +
                "6: Film sil");
            return Console.ReadLine().Trim();      
        }

        private static void Yazdir(Film film)
        {
            Console.WriteLine($"\nID: {film.Id}\n" +
                $"Adı: {film.Adi}\n" +
                $"Yapım Yılı: {film.YapimYili}\n" +
                $"Gişesi: {film.Gisesi.ToString("N2", new CultureInfo("tr-TR"))} TL\n" +
                $"Yönetmeni: {film.Yonetmeni.Adi} {film.Yonetmeni.Soyadi}\n" +
                $"Oluşturulma Tarihi: {film.OlusturulmaTarihi.ToString("dd.MM.yyyy HH:mm")}");
        }

        private static void Yazdir(List<Film> filmler)
        {
            Console.WriteLine("\n" + Veriler.KayitSayisiMesajiGetir(filmler.Count));
            foreach (Film film in filmler)
            {
                Yazdir(film);
            }
        }

        private static void Yazdir(List<Yonetmen> yonetmenler)
        {
            foreach (var yonetmen in yonetmenler)
            {
                Console.WriteLine("\nYönetmen\n" +
                    $"Yönetmen ID: {yonetmen.Id}\n" +
                    $"Adı: {yonetmen.Adi}\n" +
                    $"Soyadı: {yonetmen.Soyadi}\n" +
                    $"Doğum Tarihi: {yonetmen.DogumTarihi.ToString("dd.MM.yyyy")}\n" +
                    $"Durumu: {(yonetmen.EmekliMi ? "Emekli" : "Çalışıyor")}\n" +
                    $"Oluşturulma Tarihi: {yonetmen.OlusturulmaTarihi.ToString("dd.MM.yyyy HH:mm")}");
            }
        }

        private static void FilmleriListele()
        {
            Yazdir(_filmService.FilmleriGetir());
        }

        private static void AdaGoreFilmleriListele()
        {
            Console.Write("\nFilm adı: ");
            string giris = Console.ReadLine().Trim();
            Yazdir(_filmService.FilmleriGetir(giris));
        }

        private static void IdyeGoreFilmleriListele()
        {
            try
            {
                Console.Write("ID: ");
                int filmId = int.Parse(Console.ReadLine());
                Film film = (Film)_filmService.KayitGetir(filmId);
                if (film != null)
                    Yazdir(film);
                else
                    Console.WriteLine(Veriler.KayitBulunamadiMesaji);
            }
            catch
            {
                Console.WriteLine(Veriler.HataMesaji);
            }
        }
        private static Film FilmOlustur(int id = 0)
        {
            if (id > 0)
            {
                Kayit mevcutFilm = _filmService.KayitGetir(id);
                if (mevcutFilm is null)
                {
                    Console.WriteLine(Veriler.KayitBulunamadiMesaji);
                    return null;
                }
            }

            Console.Write("Film Adı: ");
            string adi = Console.ReadLine().Trim();
            Console.Write("Yapım Yılı: ");
            short yapimYili = Convert.ToInt16(Console.ReadLine());
            Console.Write("Gişesi: ");
            decimal gisesi = Convert.ToDecimal(Console.ReadLine(), new CultureInfo("tr-TR"));

            Yazdir(_yonetmenService.YonetmenleriGetir());
            Console.Write("Yönetmen ID: ");
            int yonetmenId = int.Parse(Console.ReadLine());

            Yonetmen yonetmen = (Yonetmen)_yonetmenService.KayitGetir(yonetmenId);
            if (yonetmen is null)
            {
                Console.WriteLine(Veriler.KayitBulunamadiMesaji);
                return null;
            }

            return new Film()
            {
                Id = id,
                Adi = adi,
                YapimYili = yapimYili,
                Gisesi = gisesi,
                Yonetmeni = yonetmen
            };
        }

        private static void FilmEkle()
        {
            try
            {
                Film film = FilmOlustur();
                _filmService.FilmEkle(film);
            }
            catch 
            {
                Console.WriteLine(Veriler.HataMesaji);
            }
        }

        private static void FilmGuncelle()
        {
            try
            {
                FilmleriListele();
                Console.Write("\nGüncellenecek film ID: ");
                int filmId = int.Parse(Console.ReadLine());
                Film film = FilmOlustur(filmId);
                Console.WriteLine(_filmService.FilmGuncelle(film));
            }
            catch 
            {
                Console.WriteLine(Veriler.HataMesaji);
            }
        }

        private static void FilmSil()
        {
            try
            {
                FilmleriListele();
                Console.Write("Silinecek Film Id: ");
                int filmId = int.Parse(Console.ReadLine());

                Console.WriteLine(_filmService.FilmSil(filmId));
            }
            catch 
            {
                Console.WriteLine(Veriler.HataMesaji);
            }
        }
    }
}