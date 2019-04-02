using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewCity.Models
{
    public class HomeNews
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Type { get; set; }
        public string Img { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid Creator { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
