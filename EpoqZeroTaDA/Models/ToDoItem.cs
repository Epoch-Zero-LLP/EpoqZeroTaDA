using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EpoqZeroTaDA.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpoqZeroTaDA.Models
{
    public partial class ToDoItem : ObservableObject
    {
        public string UserId { get; set; }
        public string _id { get; set; }

        [ObservableProperty]
        string title;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        DateTime endDate = DateTime.Today;

        public long EndDateInEpochTime {
            get
            {
                var dateWithOffset = new DateTimeOffset(endDate).ToUniversalTime();
                long timestamp = dateWithOffset.ToUnixTimeMilliseconds();
                return timestamp;
            }
        }

        [ObservableProperty]
        string status;

        [ObservableProperty]
        string remarks;

    }
}
