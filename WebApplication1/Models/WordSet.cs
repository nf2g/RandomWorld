using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class WordSet
    {
        [Key]
        public int Num { get; set; }
        public string Word { get; set; }
        public string Rand { get; set; }
        public string answer { get; set; }
    }
}