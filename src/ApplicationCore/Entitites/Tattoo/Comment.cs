namespace Inkett.ApplicationCore.Entitites
{
    public class Comment:BaseEntity
    {
        public string Text { get; set; }

        public int ProfileId { get; set; }

        public Profile Profile { get; set; }

        public int TattooId { get; set; }

        public Tattoo Tattoo { get; set; }
    }
}
