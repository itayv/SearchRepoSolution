using Microsoft.EntityFrameworkCore;
using SearchRepoApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchRepoApi.DbContexts
{
    public class RepoDbContext : DbContext
    {
        public RepoDbContext(DbContextOptions<RepoDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FavoriteRepository> FavoriteRepositories { get; set; }

    }
}
