namespace Fakebook.Profile.DataAccess.Services.Interfaces
{
    public interface IProfileDbSettings
    {
        public string ProfilesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}