using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicHub.Data.Models
{
    public class Album
    {
        public Album()
        {
            this.Songs = new List<Song>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(40,MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate  { get; set; }

        public int ProducerId { get; set; }
        public Producer Producer { get; set; }

        public ICollection<Song> Songs { get; set; }

        public decimal Price
        {
            get
            {
                return CalculatePrice(Songs);
            }
        }

        public decimal CalculatePrice(ICollection<Song> Songs)
        {
            decimal sum = 0;

            foreach (var s in Songs)
            {
                sum += s.Price;
            }

            return sum;
        }
    }
}
