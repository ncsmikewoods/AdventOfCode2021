using System.Collections.Generic;
using System.Linq;

namespace Day09
{
    public class Basin
    {
        public List<Location> Locations { get; set; } = new();

        public bool HasLocation(int x, int y)
        {
            return Locations.Any(loc => loc.X == x && loc.Y == y);
        }

        public void AddLocation(int x, int y)
        {
            Locations.Add(new Location{X = x, Y = y});
        }
    }
}