using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fakebook.Profile.DataAccess.EntityModel
{
    /// <summary>
    /// Database model of a profile. 
    /// </summary>
    public class EntityProfile
    {
        /// <summary>
        /// The primary key, the user's email.
        /// </summary>
        [Key]
        [Column(name: nameof(Email))]
        public string Email { get; set; }

        /// <summary>
        /// The Uri of the user's profile picture. Can be null.
        /// </summary>
        public Uri ProfilePictureUrl { get; set; }

        /// <summary>
        /// The user's first name. Cannot be null.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The user's last name, cannot be null.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The user's phone number. can be upto 15 characters. (?) Can be null. 
        /// </summary>
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The user's birthdate. Can't be null.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// The user's current status message, can be null.
        /// </summary>
        public string Status { get; set; }
    }
}
