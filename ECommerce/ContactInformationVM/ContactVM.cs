using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerce.ContactInformationVM
{
    public class ContactVM
    {
        public int ContactID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string Comment { get; set; }

    }
}