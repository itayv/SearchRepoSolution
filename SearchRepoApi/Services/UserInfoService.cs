using IdentityModel.Client;
using SearchRepoApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SearchRepoApi.Services
{
    public interface IUserInfoService
    {
        public Task<UserInfoModel> GetUserInfo(string address, string accessToken);
    }

    public class UserInfoService : IUserInfoService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserInfoService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;   
        }

        public async Task<UserInfoModel> GetUserInfo(string address,string accessToken)
        {
            HttpClient client = _httpClientFactory.CreateClient();

            DiscoveryDocumentResponse discoveryDocument = await client.GetDiscoveryDocumentAsync(address);

            UserInfoResponse response = await client.GetUserInfoAsync(new UserInfoRequest
            {
                Address = discoveryDocument.UserInfoEndpoint,
                Token = accessToken
            });

            UserInfoModel userInfoModel = JsonSerializer.Deserialize<UserInfoModel>(response.Raw);

            return userInfoModel;
        }
    }
}
