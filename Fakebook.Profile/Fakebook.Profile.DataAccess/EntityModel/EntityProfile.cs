using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Fakebook.Profile.DataAccess.EntityModel
{
    public class EntityProfile
    {
        public EntityProfile ShallowCopy()
        {
            return (EntityProfile)this.MemberwiseClone();
        }

        [Key]
        [Column(name: nameof(Email))]
        public string Email { get; set; }

        public Uri ProfilePictureUrl { get; set;}

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public string Status { get; set; }
    }
}
