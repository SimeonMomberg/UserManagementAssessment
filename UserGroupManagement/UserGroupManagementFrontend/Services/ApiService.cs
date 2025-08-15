using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UserGroupManagement.MVC.Models;

namespace UserGroupManagement.MVC.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task<List<UserViewModel>> GetUsersAsync()
        {
            var users = await _httpClient.GetFromJsonAsync<List<ApiUserDto>>("api/users");

            if (users == null)
                return new List<UserViewModel>();

            return users.Select(u => new UserViewModel
            {
                Id = u.Id,
                Name = u.Name,
                GroupIds = u.Groups?.Select(g => g.Id).ToList() ?? new List<int>(),
                Groups = u.Groups?.Select(g => g.Name).ToList() ?? new List<string>()
            }).ToList();
        }


        public async Task<UserViewModel?> GetUserAsync(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<ApiUserDto>($"api/users/{id}");
            if (user == null) return null;

            return new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                GroupIds = user.Groups?.Select(g => g.Id).ToList() ?? new List<int>(),
                Groups = user.Groups?.Select(g => g.Name).ToList() ?? new List<string>()
            };
        }


        public async Task<bool> CreateUserAsync(UserViewModel user)
        {
            var dto = new ApiUserDto
            {
                Id = user.Id,
                Name = user.Name,
                Groups = user.GroupIds.Select(id => new ApiGroupDto { Id = id }).ToList()
            };

            var response = await _httpClient.PostAsJsonAsync("api/users", dto);
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> UpdateUserAsync(UserViewModel user)
        {
            var dto = new ApiUserDto
            {
                Id = user.Id,
                Name = user.Name,
                Groups = user.GroupIds.Select(id => new ApiGroupDto { Id = id }).ToList()
            };

            var response = await _httpClient.PutAsJsonAsync($"api/users/{user.Id}", dto);
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/users/{id}");
            return response.IsSuccessStatusCode;
        }


        public async Task<int> GetUserCountAsync()
        {
            return await _httpClient.GetFromJsonAsync<int>("api/users/count");
        }

        public async Task<List<GroupUserCountViewModel>> GetUsersPerGroupCountAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<GroupUserCountViewModel>>("api/users/group-count");
            return result ?? new List<GroupUserCountViewModel>();
        }
    }


    public class ApiUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<ApiGroupDto>? Groups { get; set; }
    }

    public class ApiGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
