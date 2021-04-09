using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fakebook.Profile.DataAccess;
using Fakebook.Profile.DataAccess.EntityModel;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Fakebook.Profile.UnitTests.ApiTests
{
    public class ContextFactory : IDisposable
    {
        private DbConnection _conn;
        private bool disposedValue;

        private DbContextOptions<ProfileDbContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<ProfileDbContext>().UseSqlite(_conn).Options;
        }

        public ProfileDbContext CreateContext()
        {
            
            if (_conn == null)
            {
                _conn = new SqliteConnection("DataSource=:memory:");
                _conn.Open();
            }

            DbContextOptions<ProfileDbContext> options = CreateOptions();
            using var context = new ProfileDbContext(options);
            context.Database.EnsureCreated();

            // add extra test seed data here (or, in each test method)

            return new ProfileDbContext(options);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _conn.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}