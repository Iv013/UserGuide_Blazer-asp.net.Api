using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using UserGuide.Client.Pages;
using UserGuide.Shared.Models;

namespace UserGuide.Client.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public List<UserData> ListUser { get; set; } = new List<UserData>();

        public UserServices(HttpClient http, NavigationManager navigationManager)

        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public async Task<UserData> GetSingleUser(int id)
        {
            var result = await _http.GetFromJsonAsync<UserData>($"api/User/{id}");

            if (result != null)
            {
                return  result;
            }
            throw new Exception("Пользовательне найден!");
          
        }

        public async Task GetUsers()
        {
            var result = await _http.GetFromJsonAsync<List<UserData>>("api/User");
            
            if (result != null)
            {
                ListUser = result;
            }
            
        }

        public async Task UpdateUser(UserData user)
        {
            var result = await _http.PutAsJsonAsync("api/User", user);
          //  var response = await result.Content.ReadFromJsonAsync<List<UserData>>();
            if (result != null)
            {
               // ListUser = response;
                _navigationManager.NavigateTo("/");
            }
        }

        public async Task DeleteActiveUser(int id)
        {
            var result = await _http.PutAsJsonAsync($"api/User/{id}", id);
          //  var response = await result.Content.ReadFromJsonAsync<List<UserData>>();
            if (result != null)
            {
              //  ListUser = response;
                _navigationManager.NavigateTo("/");
            }
        }

        public async Task CreateUser(UserData user)
        {
           
            var result = await _http.PostAsJsonAsync("api/User", user);
         //   var response = await result.Content.ReadFromJsonAsync<List<UserData>>();
         
              //  ListUser = response;
                _navigationManager.NavigateTo("/");
          
        }
    }
}
