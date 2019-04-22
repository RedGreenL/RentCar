using System.ComponentModel;

namespace RentCar.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Car")]
    public partial class Car
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Car()
        {
            Rents = new HashSet<Rents>();
            Images = new HashSet<Images>();

        }

        [Key]
        public int Car_ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Brand { get; set; }

        [Required]
        [StringLength(20)]
        public string Model { get; set; }

        public int? Category_ID { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [StringLength(20)]
        public string Color { get; set; }

        [Required]
        [DisplayName("Engine Capacity")]
        public int Engine { get; set; }

        public int? GearType_ID { get; set; }

        public int? FuelType_ID { get; set; }

        [Required]
        public double Price { get; set; }
    
        //public int Images_ID { get; set; }

        public virtual Category Category { get; set; }

        public virtual FuelType FuelType { get; set; }

        public virtual GearType GearType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Images> Images { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rents> Rents { get; set; }
      
    }
    public enum CarColors
    {
        White,
        Silver,
        Black,
        Grey,
        Blue,
        Red,
        Brown,
        Green,
        Yellow,
        Purple,
    }
}
