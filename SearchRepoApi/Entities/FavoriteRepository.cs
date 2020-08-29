using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SearchRepoApi.Entities
{
    public class FavoriteRepository
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(300)]
        public string RepositoryName { get; set; }
    }
}
