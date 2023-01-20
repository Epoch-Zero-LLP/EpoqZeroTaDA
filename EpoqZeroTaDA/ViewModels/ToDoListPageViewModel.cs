using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EpoqZeroTaDA.Models;
using CommunityToolkit.Mvvm.Input;
using EpoqZeroTaDA.Views;
using System.Net.Http.Json;
using Mopups.Services;
using EpoqZeroTaDA.Popups;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.Maui.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EpoqZeroTaDA.ViewModels
{
    [QueryProperty(nameof(CurrentlyFocusedUser), "currentuser")]
    public partial class ToDoListPageViewModel : ObservableObject
    {
        [ObservableProperty]
        string version;

        [ObservableProperty]
        ToDoItem newToDoItem;

        [ObservableProperty]
        User newUser;

        [ObservableProperty]
        string myCurrentState;

        [ObservableProperty]
        User currentlyFocusedUser;

        [ObservableProperty]
        string typeOfTaskDisplayed;

        //ToDoItem OriginaltoDoItem;
        //ToDoItem TempToDoItem;

        string TaskApi = "https://api.epoqzerotodo.tk/api/tasks";
        HttpClient Client;

        public ObservableCollection<ToDoItem> ToDoItems { get; set; }

        public ToDoListPageViewModel()
        {
            NewToDoItem = new ToDoItem();
            //TempToDoItem = new ToDoItem();
            ToDoItems = new ObservableCollection<ToDoItem>();
            //OriginaltoDoItem = new ToDoItem();
            MyCurrentState = "Loading";

            Version = AppInfo.Current.VersionString;
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [RelayCommand]
        async void ShowPopup()
        {
            await MopupService.Instance.PushAsync(new AlertPopup("Aswin", "Aswin is irritating", "Correct"));
        }

        [RelayCommand]
        void CompletedTasksButtonClicked()
        {
            MyCurrentState = "Loading";
            ToDoItems.Clear();
            DisplayTasksViaApi("Closed");
        }

        [RelayCommand]
        void AllTasksButtonClicked()
        {
            MyCurrentState = "Loading";
            ToDoItems.Clear();
            DisplayTasksViaApi("All");
        }

        [RelayCommand]
        void PendingTasksButtonClicked()
        {
            MyCurrentState = "Loading";
            ToDoItems.Clear();
            DisplayTasksViaApi("Open");
        }
        public async void AddTaskViaApi()
        {
            NewToDoItem.UserId = CurrentlyFocusedUser._id;
            var myContent = JsonConvert.SerializeObject(NewToDoItem);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await Client.PostAsync(TaskApi, byteContent);
            if (response.IsSuccessStatusCode)
            {
                await MopupService.Instance.PushAsync(new AlertPopup("Success", "Task added successfully", "OK"));
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            //NewToDoItem = new ToDoItem();
        }
        public async void ModifyTaskViaApi()
        {
            string ModifyTaskApi = System.String.Format("{0}/{1}", TaskApi,NewToDoItem._id);
            var myContent = JsonConvert.SerializeObject(NewToDoItem);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await Client.PatchAsync(ModifyTaskApi, byteContent);
            if (response.IsSuccessStatusCode)
            {
                await MopupService.Instance.PushAsync(new AlertPopup("Success", "Task modified successfully", "OK"));
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            //NewToDoItem = new ToDoItem();
        }

        public async void DeleteTaskViaApi()
        {
            string DeleteTaskApi = System.String.Format("{0}/{1}", TaskApi, NewToDoItem._id);
            HttpResponseMessage response = await Client.DeleteAsync(DeleteTaskApi);
            if (response.IsSuccessStatusCode)
            {
                await MopupService.Instance.PushAsync(new AlertPopup("Success", "Task deleted successfully", "OK"));
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            //NewToDoItem = new ToDoItem();
        }
        public async void DisplayTasksViaApi(string TaskStatus="Open")
        {
            string GetTasksApi = "";
            TypeOfTaskDisplayed = TaskStatus;
            if (TaskStatus == "All")
            {
                GetTasksApi = GetTasksApi + System.String.Format("{0}?Sort=EndDateInEpochTime&UserId={1}", TaskApi, CurrentlyFocusedUser._id);
            }
            else
            {
                GetTasksApi = GetTasksApi + System.String.Format("{0}?Sort=EndDateInEpochTime&UserId={1}&Status={2}", TaskApi, CurrentlyFocusedUser._id, TaskStatus);
            }
            HttpResponseMessage response = await Client.GetAsync(GetTasksApi);
            if (response.IsSuccessStatusCode)
            {
                List<ToDoItem> ToDoItemsTempList = await response.Content.ReadFromJsonAsync<List<ToDoItem>>();
                ToDoItems.Clear();
                foreach (ToDoItem toDoItemTemp in ToDoItemsTempList)
                {
                    ToDoItems.Add(toDoItemTemp);
                }
                MyCurrentState = "Success";
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        //void SaveOriginalToDoItem(ToDoItem tempToDoItem, ToDoItem toDoItem)
        //{
        //    tempToDoItem.Title = toDoItem.Title;
        //    tempToDoItem.Description = toDoItem.Description;
        //    tempToDoItem.EndDate = toDoItem.EndDate;
        //    tempToDoItem.Status = toDoItem.Status;
        //    tempToDoItem.Remarks = toDoItem.Remarks;
        //    tempToDoItem._id = toDoItem._id;
        //    tempToDoItem.UserId = toDoItem.UserId;
        //}

        //public void AddToDoItem()
        //{
        //    if (System.String.IsNullOrEmpty(NewToDoItem.Title))
        //    {
        //        return;
        //    }
        //    else if (System.String.IsNullOrEmpty(NewToDoItem.Description))
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        ToDoItems.Add(NewToDoItem);
        //        NewToDoItem = new ToDoItem();
        //    }
        //}


        //public void DeleteToDoItem(ToDoItem e)
        //{
        //    ToDoItems.Remove(e);

        //}   

        [RelayCommand]
        void GoToAddToDoItemPage(ToDoItem toDoItem)
        {
            NewToDoItem = new ToDoItem();
            Shell.Current.GoToAsync(nameof(AddToDoItemPage));
        }

        [RelayCommand]
        void GoToModifyToDoItemPage(ToDoItem toDoItem)
        {
            if (toDoItem != null)
            {
                //SaveOriginalToDoItem(OriginaltoDoItem, toDoItem);
                //TempToDoItem = toDoItem;
                NewToDoItem = toDoItem;
            }
            Shell.Current.GoToAsync(nameof(ModifyToDoItemPage));
        }



        [RelayCommand]
        async void Save()
        {
            if (System.String.IsNullOrEmpty(NewToDoItem.Title))
            {
                await MopupService.Instance.PushAsync(new AlertPopup("Mandatory field!", "Please enter a value for Title", "OK"));
            }
            else if (System.String.IsNullOrEmpty(NewToDoItem.Status))
            {
                await MopupService.Instance.PushAsync(new AlertPopup("Mandatory field!", "Please choose a value for Status", "OK"));
            }
            else 
            {
                AddTaskViaApi();
                //AddToDoItem();
                Shell.Current.GoToAsync("..");
            }
        }


        [RelayCommand]
        async void SaveModified()
        {
            if (System.String.IsNullOrEmpty(NewToDoItem.Title))
            {
                await MopupService.Instance.PushAsync(new AlertPopup("Mandatory field!", "Please enter a value for Title", "OK"));
            }
            else if (System.String.IsNullOrEmpty(NewToDoItem.Status))
            {
                await MopupService.Instance.PushAsync(new AlertPopup("Mandatory field!", "Please choose a value for Status", "OK"));
            }
            else
            {
                //SaveOriginalToDoItem(TempToDoItem, NewToDoItem);
                ModifyTaskViaApi();
                //NewToDoItem = new ToDoItem();
                Shell.Current.GoToAsync("..");
            }
        }

        [RelayCommand]
        async Task Cancel()
        {
            //SaveOriginalToDoItem(NewToDoItem, OriginaltoDoItem);
            NewToDoItem = new ToDoItem();
            //await Shell.Current.GoToAsync(nameof(ToDoListpage));
            Shell.Current.GoToAsync("..");

        }

        [RelayCommand]
        async Task Delete()
        {
            //DeleteToDoItem(TempToDoItem);
            bool DeleteTaskAnswer = await App.Current.MainPage.DisplayAlert("CONFIRM", "Do you want to delete this task?", "Yes", "No");
            if (DeleteTaskAnswer)
            {
                DeleteTaskViaApi();
            }
            //await Shell.Current.GoToAsync(nameof(ToDoListpage));
            Shell.Current.GoToAsync("..");
            //var sdfg = await Cancel();
        }
    }
}
