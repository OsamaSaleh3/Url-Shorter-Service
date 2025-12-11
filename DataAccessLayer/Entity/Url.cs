using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLayer.Entity;

public class Url
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string LongUrl { get; set; }= string.Empty;
    [Required]
    public string ShortCode { get; set; }=string.Empty;
    [Required]
    public DateTime CreatedAt { get; set; }= DateTime.Now;
}
