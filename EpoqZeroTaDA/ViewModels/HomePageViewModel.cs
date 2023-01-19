using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EpoqZeroTaDA.Models;
using EpoqZeroTaDA.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EpoqZeroTaDA.ViewModels
{
    public partial class HomePageViewModel : ObservableObject
    {
        string UsersApiUrl = "https://api.epoqzerotodo.tk/api/users";
        HttpClient Client;


        [ObservableProperty]
        string myCurrentState;

        public ObservableCollection<User> NewUsers { get; set; }

        [RelayCommand]
        public void GoToToDoListPage(User CurrentUser)
        {
            Shell.Current.GoToAsync(nameof(ToDoListpage), new Dictionary<string, object>
            {
                { "currentuser", CurrentUser}
            });
        }

        public HomePageViewModel() 
        {
            NewUsers = new ObservableCollection<User>();
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            AccessUserApi();
            MyCurrentState = "Loading";
        }

        public async void AccessUserApi()
        {
            HttpResponseMessage response = await Client.GetAsync(UsersApiUrl);
            if (response.IsSuccessStatusCode)
            {
                List<User> users = await response.Content.ReadFromJsonAsync<List<User>>();
                foreach(User user in users)
                {
                    //user.ImageUrl = "profileimage";
                    NewUsers.Add(user);
                    //await Task.Delay(2000);
                }
                MyCurrentState = "Success";
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }
    }
}
