using System.Collections.ObjectModel;
using System.Text.Json;
using System.IO;
using Microsoft.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToDoList1.Models;
using ToDoList1.Services;

namespace ToDoList1.ViewModels
{
    public partial class TodoViewModel : ObservableObject
    {
        //private const string FileName = "todo_list.json";

        private readonly TodoDatabase _databaseService;
        public ObservableCollection<TodoItem> Items { get; set; } = new();

        public TodoViewModel()
        {
            _databaseService = new TodoDatabase();
            LoadData();
        }

        [RelayCommand]
        public void AddItem(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                var newItem = new TodoItem { Title = title};
                _databaseService.SaveItemAsync(newItem);
                LoadData();
            }
        }

        [RelayCommand]
        public void RemoveItem(TodoItem item)
        {
            var completedItems = Items.Where(item => item.IsCompleted).ToList();

            foreach (var task in completedItems)
            {
                Items.Remove(task);
                _databaseService.DeleteItemAsync(task);
            }
        }

        [RelayCommand]
        public void ToggleComplete(TodoItem item)
        {
            if (item != null)
            {
                item.IsCompleted = !item.IsCompleted;
                _databaseService.SaveItemAsync(item);
                LoadData();
            }
        }

        public async void LoadData()
        {
            var items = await _databaseService.GetItemsAsync();
            Items.Clear();
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }
    }
}
