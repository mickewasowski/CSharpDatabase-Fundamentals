using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusicHub.DataProcessor.ImportDtos
{
   public class ImportWriterDto
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }

        [RegularExpression(@"^[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+$")]
        public string Pseudonym { get; set; }
    }
}
