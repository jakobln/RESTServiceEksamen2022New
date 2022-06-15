using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTServiceNew
{
    public class Wind
    {
        public int Id { get; set; }
        public string Direction { get; set; }
        public int Speed { get; set; }

        public Wind()
        { }

        public Wind(int id, string direction, int speed)
        {
            Id = id;
            Direction = direction;
            Speed = speed;
        }

        public override string ToString()
        {
            //Simple string containing the property names and thier respective values
            return $"Id: {Id} - Direction: {Direction} - Speed: {Speed}";
        }
    }
}
