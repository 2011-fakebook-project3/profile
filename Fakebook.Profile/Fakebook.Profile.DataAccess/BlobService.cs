using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakebook.Profile.DataAccess
{
    /// <summary>
    /// Gets data from/to azure.
    /// </summary>
    class BlobService : IBlobService
    {
        //todo: replace with the real type
        private string _Client = null;

        public BlobService(string client){
            _Client = client;
        }
    }
}
