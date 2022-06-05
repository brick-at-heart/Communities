namespace BrickAtHeart.Communities.Models.Authorization
{
    public class RoleRight
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public RightState State { get; set; }

        public RoleRight(string name)
        {
            Name = name;
        }
    }
}