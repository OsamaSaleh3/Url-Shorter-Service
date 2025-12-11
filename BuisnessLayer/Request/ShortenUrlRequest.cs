using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BuisnessLayer.Request
{
    public class ShortenUrlRequest
    {
        [Required]
        public string LongUrl { get; set; }= string.Empty;
    }
}
