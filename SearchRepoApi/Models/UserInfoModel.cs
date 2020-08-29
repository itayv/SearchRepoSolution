using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchRepoApi.Models
{
    public class UserInfoModel
    {
        public string sub { get; set; }
        public string name { get; set; }
        public string preferred_username { get; set; }
    }
}
