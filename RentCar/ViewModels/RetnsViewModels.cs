using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace RentCar.Models
{
    public class RetnsViewModels
    {

        [Key]
        public int Rent_ID { get; set; }

        [Required]
        public int District_ID { get; set; }

        [Required]
        public int City_ID { get; set; }

        [Required]
        public int Car_ID { get; set; }

        [Required]
        public int Client_ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Brand { get; set; }

        [Required]
        [StringLength(20)]
        public string Model { get; set; }

        [Required]
        [DisplayName("Engine Capacity")]
        public int Engine { get; set; }

        [Required]
        [StringLength(20)]
        public string Color { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Surname { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        public int? AplicationUser_ID { get; set; }

        [Required]
        [StringLength(30)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Idnp { get; set; }

        public bool Driver { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartData { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndData { get; set; }

        public Addres Addres { get; set; }

        public int Addres_ID { get; set; }
        
        public double Price { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("District")]
        public string District { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("City")]
        public string City { get; set; }

        [Required]
        [StringLength(20)]
        public string Street { get; set; }

        [Required]
        [StringLength(20)]
        public string Flat { get; set; }

    }
}