using System;
using System.Collections.Generic;

namespace lab5
{
    public class Building
    {
        public string Name { get; set; }
        public int YearBuilt { get; set; }
        public double Height { get; set; }
        public List<string> Purposes { get; set; }

        public Building()
        {
            Name = "Невідома споруда";
            YearBuilt = 2000;
            Height = 0;
            Purposes = new List<string>();
        }

        public Building(string name, int yearBuilt, double height, List<string> purposes)
        {
            Name = name;
            YearBuilt = yearBuilt;
            Height = height;
            Purposes = purposes ?? new List<string>();
        }

        public void AddPurpose(string purpose)
        {
            if (!string.IsNullOrEmpty(purpose))
                Purposes.Add(purpose);
        }

        public bool RemovePurpose(string purpose)
        {
            return Purposes.Remove(purpose);
        }

        public int GetAge()
        {
            return DateTime.Now.Year - YearBuilt;
        }
    }
}