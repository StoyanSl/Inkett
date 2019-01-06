namespace Inkett.ApplicationCore.Entitites
{
    public class Like:BaseEntity
    {
        public int ProfileId { get; set; }

        public Profile Profile { get; set; }
        
        public int TattooId { get; set; }

        public Tattoo Tattoo { get; set; }
    }
}
