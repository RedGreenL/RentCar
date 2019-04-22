namespace RentCar.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rents
    {
        [Key]
        public int Rent_ID { get; set; }

        public int? Client_ID { get; set; }

        public int Car_ID { get; set; }

        public int? Operator_ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartData { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndData { get; set; }

        [Required]
        public double Price { get; set; }

        public bool Driver { get; set; }

        public virtual Car Car { get; set; }

        public virtual Client Client { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
