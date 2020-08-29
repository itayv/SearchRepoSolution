using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SearchRepoApi.Services;

namespace SearchRepoApi.Controllers
{
    [Route("api/repository")]
    [ApiController]
    public class RepositoryController : ControllerBase
    {
        private readonly IGitHubService _gitHubService;
        private readonly IUserInfoService _userInfoService;
        private readonly IRepoDbService _repoDbService;
        private readonly IConfiguration _configuration;

        public RepositoryController(IGitHubService gitHubService,
            IUserInfoService userInfoService,
            IRepoDbService repoDbService,
            IConfiguration configuration)
        {
            _gitHubService = gitHubService;
            _userInfoService = userInfoService;
            _repoDbService = repoDbService;
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet()]
        public ActionResult<string[]> GetFavorites()
        {
            var accessToken = HttpContext.GetTokenAsync("access_token").Result;

            var userinfo = _userInfoService.GetUserInfo(_configuration.GetValue<string>("IdentityServer4Url"),
                accessToken).Result;

            string[] res = _repoDbService.GetFavoriteRepoitories(userinfo.name).Select(x => x.RepositoryName).ToArray();

            return Ok(res);
        }

        [Authorize]
        [HttpGet("search")]
        public ActionResult<string[]> Search([FromQuery] string name)
        {
            string[] res = _gitHubService.SearchRepository(name).Result.items.Select(x => x.full_name).ToArray();

            return Ok(res);
        }

        [Authorize]
        [HttpGet("saveFavorite")]
        public ActionResult<bool> saveFavorite([FromQuery] string repositoryName)
        {
            var accessToken = HttpContext.GetTokenAsync("access_token").Result;

            var userinfo = _userInfoService.GetUserInfo(_configuration.GetValue<string>("IdentityServer4Url"),
                accessToken).Result;

            _repoDbService.AddFavoriteRepository(userinfo.name, repositoryName);

            return Ok(true);
        }

    }
}
