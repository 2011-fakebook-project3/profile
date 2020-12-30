using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Fakebook.Profile.DataAccess.MongoModels
{
    class MongoProfile
    {
        [BsonId]
        public string Email { get; set; }

        public Uri ProfilePictureUrl { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public string Status { get; set; }

    }
}
