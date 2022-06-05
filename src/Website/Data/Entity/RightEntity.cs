namespace BrickAtHeart.Communities.Data.Entity
{
    public class RightEntity : IRightEntity
    {
        public long Id {get; set;}

        public string Name {get; set;}

        public bool? State {get; set;}

        public RightEntity(string name)
        {
            Name = name;
        }
    }
}