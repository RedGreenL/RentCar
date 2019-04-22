using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentCar.ViewModels
{
    public class UserViewModels
    {
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(20)]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string UserId { get; set; }
        public int  DistrictId { get; set; }
        public int CityId { get; set; }
        public string Flat { get; set; }
        public string Street { get; set; }

    }
}