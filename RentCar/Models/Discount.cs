using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentCar.Models
{
    public class Discount
    {
        [Key]
        public int Discount_ID { get; set; }
        
        public int Days { get; set; }
        public float Percent { get; set; }
    }
}