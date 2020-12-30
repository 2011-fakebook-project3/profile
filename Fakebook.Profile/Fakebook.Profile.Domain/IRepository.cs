using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakebook.Profile.Domain
{
    /// <summary>
    /// Interface for data retrieval repository.
    /// </summary>
    public interface IRepository
    {

        public Task<DomainProfile> GetProfileAsync(string email);
    }
}
