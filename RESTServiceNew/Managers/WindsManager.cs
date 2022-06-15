using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTServiceNew.Managers
{
    public class WindsManager
    {
        //A simple int to keep track of ID's
        private static int _nextID = 1;

        private static List<Wind> _data = new List<Wind>()
        {
            new Wind { Id = _nextID++, Direction = "North", Speed = 1},
            new Wind { Id = _nextID++, Direction = "East", Speed = 0},
            new Wind { Id = _nextID++, Direction = "South", Speed = 1},
            new Wind { Id = _nextID++, Direction = "West", Speed = 2},
            new Wind { Id = _nextID++, Direction = "North", Speed = 0}
        };

        //Returns all winds in the List, in a new List, if the all the parameters is null (or 0 for int values, default value)
        //if the substring is not null, the it returns all items that has a name containing the substring
        //the filter is case-insensitive
        public List<Wind> GetAll(string substring, int minimumSpeed)
        {
            List<Wind> result = new List<Wind>(_data);
            if (substring != null)
            {
                result = result.FindAll(wind => wind.Direction.Contains(substring, StringComparison.OrdinalIgnoreCase));
            }
            if (minimumSpeed != 0)
            {
                result = result.FindAll(wind => wind.Speed >= minimumSpeed);
            }

            return result;
        }

        //Filter function to return all items having a speed between minSpeed and maxSpeed
        public List<Wind> GetAllBetweenSpeed(int minSpeed, int maxSpeed)
        {
            List<Wind> result = new List<Wind>(_data);
            result = result.FindAll(wind => wind.Speed >= minSpeed && wind.Speed <= maxSpeed);
            return result;
        }

        //Adds an object to the list, and assign a unique ID
        public int Add(Wind newData)
        {
            newData.Id = _nextID++;
            _data.Add(newData);
            return newData.Id;
        }
    }
}