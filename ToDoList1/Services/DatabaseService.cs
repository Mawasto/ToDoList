using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList1.Models;

namespace ToDoList1.Services
{
    internal class TodoDatabase
    {
        private readonly SQLiteAsyncConnection _connection;

        public TodoDatabase()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "todo.db");
            _connection = new SQLiteAsyncConnection(dbPath);
            _connection.CreateTableAsync<TodoItem>().Wait();
        }
        public Task<List<TodoItem>> GetItemsAsync()
        {
            return _connection.Table<TodoItem>().ToListAsync();
        }

        public Task<int> SaveItemAsync(TodoItem item)
        {
            return _connection.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(TodoItem item)
        {
            return _connection.DeleteAsync(item);
        }
    }
}
