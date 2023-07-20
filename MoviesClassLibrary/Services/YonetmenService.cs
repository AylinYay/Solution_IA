using MoviesClassLibrary.Data;
using MoviesClassLibrary.Models;
using MoviesClassLibrary.Models.Bases;
using MoviesClassLibrary.Services.Bases;

namespace MoviesClassLibrary.Services
{
    public class YonetmenService : IService
    {
        public List<Yonetmen> YonetmenleriGetir()
        {
            return Veriler.Yonetmenler;
        }

        public Kayit KayitGetir(int id)
        {
            List<Yonetmen> yonetmenler = YonetmenleriGetir();
            return yonetmenler.FirstOrDefault(y => y.Id == id);
        }
    }
}
