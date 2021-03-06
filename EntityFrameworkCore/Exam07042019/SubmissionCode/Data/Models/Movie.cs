﻿using Cinema.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Data.Models
{
    public class Movie
    {
        //•	Title – text with length[3, 20] (required)
        //•	Genre – enumeration of type Genre, with possible values(Action, Drama, Comedy, Crime, Western, Romance, Documentary, Children, Animation, Musical) (required)
        //•	Duration – TimeSpan(required)
        //•	Rating – double in the range[1, 10] (required)
        //•	Director – text with length[3, 20] (required)
        //•	Projections - collection of type Projection

        public Movie()
        {
            this.Projections = new List<Projection>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3),MaxLength(20)]
        public string Title { get; set; }

        [Required]
        public GenreType Genre { get; set; }

        [Required]
        public TimeSpan Duration { get; set; } 

        [Required]
        [Range(typeof(double),"1.00","10.00")]
        public double Rating { get; set; } 

        [Required]
        [MinLength(3),MaxLength(20)]
        public string Director { get; set; }

        public ICollection<Projection> Projections { get; set; }
    }
}
