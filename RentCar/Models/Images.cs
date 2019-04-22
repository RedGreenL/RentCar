using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Models
{
    public class Images
    {
        [Key]
        public int Images_ID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        public float Size { get; set; }

        public int Car_ID { get; set; }

        public virtual Car Car { get; set; }
        //public virtual ICollection<Car> Car { get; set; }
    }
}
