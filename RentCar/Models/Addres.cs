namespace RentCar.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Addres
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Addres()
        {
            Client = new HashSet<Client>();
            ApplicationUsers = new HashSet<ApplicationUser>();
        }

        [Key]
        public int Addres_ID { get; set; }

        public int? District_ID { get; set; }

        public int? City_ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Street { get; set; }
        [Required]
        [StringLength(20)]
        public string Flat { get; set; }

        public virtual City City { get; set; }

        public virtual District District { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Client> Client { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
