using MoviesClassLibrary.Models.Bases;

namespace MoviesClassLibrary.Models
{
    public class Film : Kayit
    {
        public string Adi { get; set; }
        public short YapimYili { get; set; }
        public decimal Gisesi { get; set; }
        public Yonetmen Yonetmeni { get; set; }
    }
}
