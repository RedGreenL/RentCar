using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RentCar.Repositories;

namespace RentCar.Models
{
    public class ClientViewModels
    {
        [Key]
        public int Client_ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string Surname { get; set; }

        [Required]
        [StringLength(30)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(13)]
        public string Idnp { get; set; }

        public int? Addres_ID { get; set; }

        [Required]
        [Phone]
        [MaxLength(9)]
        [MinLength(9)]
        public string Phone { get; set; }
        
        [Required]
        public int District_ID { get; set; }

        [Required]
        public int City_ID { get; set; }

        [StringLength(20)]
        public string Street { get; set; }

        [Required]
        [MaxLength(50)]
        public string Flat { get; set; }

    }
}