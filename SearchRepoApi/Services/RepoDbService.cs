using SearchRepoApi.DbContexts;
using SearchRepoApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchRepoApi.Services
{
    public interface IRepoDbService
    {
        void AddFavoriteRepository(string username, string repositoryname);
        List<FavoriteRepository> GetFavoriteRepoitories(string userName);
    }

    public class RepoDbService : IRepoDbService
    {
        private readonly RepoDbContext _context;

        public RepoDbService(RepoDbContext context)
        {
            _context = context;
        }

        public void AddFavoriteRepository(string username, string repositoryname)
        {
            if(!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(repositoryname))
            {

                if (_context.FavoriteRepositories
                    .Where(x => x.UserName == username && x.RepositoryName == repositoryname).FirstOrDefault() == null)
                {
                    FavoriteRepository newRepo = new FavoriteRepository { RepositoryName = repositoryname, UserName = username };
                    _context.FavoriteRepositories.Add(newRepo);

                    _context.SaveChanges();
                }
            }
        }

        public List<FavoriteRepository> GetFavoriteRepoitories(string userName)
        {
            return _context.FavoriteRepositories.Where(x => x.UserName == userName).ToList();
        }
    }
}
