using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Fakebook.Profile.DataAccess.BlobModel

{
    public class BlobProfile
    {
        [BsonId]
        public string Email { get; set; }

        [BsonIgnoreIfDefault]
        public Uri ProfilePictureUrl { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [BsonIgnoreIfNull]
        public string PhoneNumber { get; set; }

        [BsonIgnoreIfDefault]
        public DateTime BirthDate { get; set; }

        [BsonIgnoreIfNull]
        public string Status { get; set; }

    }
}
