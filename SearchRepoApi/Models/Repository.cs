using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchRepoApi.Models
{
    public class Repository
    {
        public int total_count { get; set; }
        public bool incomplete_results { get; set; }
        public List<RepositoryItem> items { get; set; }
    }
}
