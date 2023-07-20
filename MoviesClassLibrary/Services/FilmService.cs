using MoviesClassLibrary.Data;
using MoviesClassLibrary.Models;
using MoviesClassLibrary.Models.Bases;
using MoviesClassLibrary.Services.Bases;

namespace MoviesClassLibrary.Services
{
    public class FilmService : IService
    {
        private YonetmenService _yonetmenService = new YonetmenService();

        public List<Film> FilmleriGetir()
        {
            return Veriler.Filmler;
        }

        public Kayit KayitGetir(int id)
        {
            List<Film> filmler = FilmleriGetir();
            return filmler.FirstOrDefault(f => f.Id == id);
        }

        public List<Film> FilmleriGetir(string adi)
        {
            List<Film> filmler = new List<Film>();
            List<Film> mevcutFilmler = FilmleriGetir();
            foreach (Film mevcutFilm in mevcutFilmler)
            {
                if (mevcutFilm.Adi.Contains(adi.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    filmler.Add(mevcutFilm);
                }
            }
            return filmler;
        }

        public Film FilmGetir(string adi, int id = 0)  // 0: yeni film ekleme , !0: mevcut film güncellleme
        {
            Film film = null;
            List<Film> mevcutFilmler = FilmleriGetir();
            for (int i = 0; i < mevcutFilmler.Count; i++)
            {
                if (id == 0 && mevcutFilmler[i].Adi.Equals(adi.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    film = mevcutFilmler[i];
                    break;
                }
                else if (id != mevcutFilmler[i].Id && mevcutFilmler[i].Adi.Equals(adi.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    film = mevcutFilmler[i];
                    break;
                }
            }
            return film;
        }

        public string FilmEkle(string adi, short yapimYili, decimal gisesi, int yonetmenId)
        {
            if (FilmGetir(adi) is not null)
            {
                return "Girilen ada sahip film bulunmamaktadır!";
            }

            Film film = new Film()
            {
                Id = ++Veriler.EnSonId,
                Adi = adi,
                YapimYili = yapimYili,
                Gisesi = gisesi,
                Yonetmeni = _yonetmenService.KayitGetir(yonetmenId) as Yonetmen
            };
            Veriler.Filmler.Add(film);
            return "Film başarıyla eklendi.";
        }

        public string FilmEkle(Film film)
        {
            if (FilmGetir(film.Adi) is not null)
                return "Girilen ada sahip film bulunmamaktadır!";
            film.Id = ++Veriler.EnSonId;
            Veriler.Filmler.Add(film);
            return "Film başarıyla eklendi.";
        }

        public string FilmGuncelle(Film film)
        {
            if(FilmGetir(film.Adi, film.Id) is not null)
                return "Girilen ada sahip film bulunmamaktadır!";
            Kayit mevcutKayit = KayitGetir(film.Id);
            if (mevcutKayit is null)
                return Veriler.KayitBulunamadiMesaji;
            Film mevcutFilm = mevcutKayit as Film;
            mevcutFilm.Adi = film.Adi.Trim();
            mevcutFilm.YapimYili = film.YapimYili;
            mevcutFilm.Gisesi = film.Gisesi;
            mevcutFilm.Yonetmeni = film.Yonetmeni;
            return "Film başarıyla güncellendi.";
        }

        public string FilmSil(int id)
        {
            Kayit mevcutFilm = KayitGetir(id);
            if (mevcutFilm is null)
                return Veriler.KayitBulunamadiMesaji;
            Veriler.Filmler.Remove((Film)mevcutFilm);
            return "Film başarıyla silindi.";
        }
    }
}
