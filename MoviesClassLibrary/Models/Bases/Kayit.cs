namespace MoviesClassLibrary.Models.Bases
{
    public abstract class Kayit
    {
        public int Id { get; set; }
        public DateTime OlusturulmaTarihi { get; set; }

        protected Kayit(int id) : this()
        {
            Id = id;
        }

        protected Kayit()
        {
            OlusturulmaTarihi = DateTime.Now;
        }
    }
}
