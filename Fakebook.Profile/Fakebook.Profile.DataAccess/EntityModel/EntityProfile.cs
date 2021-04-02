using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fakebook.Profile.DataAccess.EntityModel
{
    /// <summary>
    /// Database model of a profile.
    /// </summary>
    [Table("Profile")]
    public class EntityProfile
    {
        /// <summary>
        /// The user's ID. Cannot be null.
        /// </summary>
        [Key]
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// The user's unique email. Cannot be null.
        /// </summary>
        [Required]
        [MaxLength(45),MinLength(5)]
        public string Email { get; set; }

        /// <summary>
        /// The user's followers. Can be null.
        /// </summary>
        public List<string> FollowerEmails { get; set; } 

        /// <summary>
        /// The user's followees. Can be null.
        /// </summary>
        public List<string> FollowingEmails { get; set; } 

        /// <summary>
        /// The Uri of the user's profile picture. Can be null.
        /// </summary>
        public Uri ProfilePictureUrl { get; set; }

        /// <summary>
        /// The user's first name. Cannot be null.
        /// </summary>
        [MaxLength(25),MinLength(2)]
        public string FirstName { get; set; }

        /// <summary>
        /// The user's last name, cannot be null.
        /// </summary>
        [Required]
        [MaxLength(25),MinLength(2)]
        public string LastName { get; set; }

        /// <summary>
        /// The user's phone number. can be up to 15 characters. (?) Can be null.
        /// </summary>
        [MaxLength(15),MinLength(10)]
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
    }
}