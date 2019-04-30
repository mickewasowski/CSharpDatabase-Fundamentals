using System;
using System.Collections.Generic;
using System.Text;

namespace P03_FootballBetting.Data.Models
{
    public class Color
    {
        public Color()
        {
            this.HomeTeams = new List<Team>();
            this.AwayTeams = new List<Team>();
        }

        public int ColorId { get; set; }

        public string Name { get; set; }

        public ICollection<Team> HomeTeams { get; set; }
        public ICollection<Team> AwayTeams { get; set; }
    }
}
