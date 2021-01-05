using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Fakebook.Profile.DataAccess.EntityModel
{
    class ProfileDbContext : DbContext
    {

        public ProfileDbContext([NotNull] DbContextOptions options) :
            base(options)
        { }

        public DbSet<EntityProfile> StorageProfiles { get; set; }

        public DbSet<EntityFollow> Follows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            throw new NotImplementedException();
        }
    }
}
