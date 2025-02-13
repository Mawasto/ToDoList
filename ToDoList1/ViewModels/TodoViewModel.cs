using System.Collections.ObjectModel;
using System.Text.Json;
using System.IO;
using Microsoft.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToDoList1.Models;

namespace ToDoList1.ViewModels
{
    public partial class TodoViewModel : ObservableObject
    {
        private const string FileName = "todo_list.json";

        public ObservableCollection<TodoItem> Items { get; set; } = new();

        public TodoViewModel()
        {
            LoadData();
        }

        [RelayCommand]
        public void AddItem(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                Items.Add(new TodoItem { Title = title });
                Console.WriteLine("dodwanie");
                SaveData();
            }
        }

        [RelayCommand]
        public void RemoveItem(TodoItem item)
        {
            var completedItems = Items.Where(item => item.IsCompleted).ToList();

            foreach (var task in completedItems)
            {
                Items.Remove(task);
            }

            SaveData();
        }

        [RelayCommand]
        public void ToggleComplete(TodoItem item)
        {
            if (item != null)
            {
                item.IsCompleted = !item.IsCompleted;
                SaveData();
            }
        }

        private void LoadData()
        {
            try
            {
                var path = Path.Combine(FileSystem.AppDataDirectory, FileName);
                if (File.Exists(path))
                {
                    var json = File.ReadAllText(path);
                    var items = JsonSerializer.Deserialize<ObservableCollection<TodoItem>>(json);
                    if (items != null)
                        Items = new ObservableCollection<TodoItem>(items);
                }
            }
            catch { }
        }

        private void SaveData()
        {
            try
            {
                var path = Path.Combine(FileSystem.AppDataDirectory, FileName);
                var json = JsonSerializer.Serialize(Items);
                File.WriteAllText(path, json);
            }
            catch { }
        }
    }
}
