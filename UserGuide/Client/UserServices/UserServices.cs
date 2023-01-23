using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using UserGuide.Shared.Models;

namespace UserGuide.Client.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public IJSRuntime _jSRuntime ;

        public List<UserData> ListUser { get; set; } = new List<UserData>();

        public UserServices(HttpClient http, NavigationManager navigationManager, IJSRuntime jSRuntime)

        {
            _http = http;
            _navigationManager = navigationManager;
         _jSRuntime = jSRuntime;    
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
            await ShowMessage(await _http.PutAsJsonAsync("api/User", user));   
        }

        public async Task DeleteActiveUser(int id)
        {
            await ShowMessage(await _http.PutAsJsonAsync($"api/User/{id}", id));
        }

        public async Task CreateUser(UserData user)
        {
           await ShowMessage( await _http.PostAsJsonAsync("api/User", user));  
        }

        private async Task ShowMessage(HttpResponseMessage? result)
        {
            var response = await result.Content.ReadFromJsonAsync<ServiceResponse>();
            await _jSRuntime.InvokeVoidAsync("ShowToastr", response.Success, response.Message);
            _navigationManager.NavigateTo("/");
        }
    }
}
