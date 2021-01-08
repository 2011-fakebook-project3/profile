using Fakebook.Profile.DataAccess.Services.Interfaces;
namespace Fakebook.Profile.DataAccess.Services
{
    public class ProfileDbSettings : IProfileDbSettings
    {
        public string ProfilesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
