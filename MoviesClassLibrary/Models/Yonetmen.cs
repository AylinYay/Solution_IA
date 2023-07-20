using MoviesClassLibrary.Models.Bases;

namespace MoviesClassLibrary.Models
{
    public class Yonetmen : Kayit
    {
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public DateTime DogumTarihi { get; set; }
        public bool EmekliMi { get; set; }

        public Yonetmen(string adi, string soyadi, DateTime dogumTarihi, bool emekliMi, int id) : base(id)
        {
            Adi = adi;
            Soyadi = soyadi;
            DogumTarihi = dogumTarihi;
            EmekliMi = emekliMi;
        }


    }
}