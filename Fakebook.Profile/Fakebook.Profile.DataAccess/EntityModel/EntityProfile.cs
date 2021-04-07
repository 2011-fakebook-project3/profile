using System;
using System.Collections.Generic;
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
        /// The primary key.
        /// </summary>
        [Key]
        [Column(name: nameof(Id))]
        public int Id { get; set; }

        /// <summary>
        /// The users email address.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// The Uri of the user's profile picture. Can be null.
        /// </summary>
        public Uri ProfilePictureUrl { get; set; }

        /// <summary>
        /// The user's first name. Cannot be null.
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// The user's last name, cannot be null.
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// The user's phone number. can be up to 15 characters. (?) Can be null.
        /// </summary>
        [MinLength(10), MaxLength(15)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The user's birthdate. Can't be null.
        /// </summary>
        [Required]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// The user's current status message, can be null.
        /// </summary>
        [MaxLength(300)]
        public string Status { get; set; }

        /// <summary>
        /// List of users this user is following
        /// </summary>
        public virtual IList<Follow> Following { get; set; }

        /// <summary>
        /// List of users following this user
        /// </summary>
        public virtual IList<Follow> Followers { get; set; }
    }
}
