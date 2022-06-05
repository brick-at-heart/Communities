namespace BrickAtHeart.Communities.Data.Entity
{
    public interface IRightEntity
    {
        public long Id {get; set;}

        public string Name {get; set;}

        public bool? State {get; set;}
    }
}