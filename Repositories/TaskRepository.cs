using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Plandit.Models;

namespace Plandit.Repositories
{
    public class TaskRepository
    {
        private string _databasePath;
        private SQLiteAsyncConnection connection;

        public TaskRepository(string dbPath)
        {
            _databasePath = dbPath;
        }

        private async Task Initialize()
        {
            if (connection != null)
            {
                return;
            }

            connection = new SQLiteAsyncConnection(_databasePath);
            await connection.CreateTableAsync<TodoTask>();
        }

        public async Task AddTask(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            await Initialize();
            int result = 0;

            result = await connection.InsertAsync(new TodoTask { TaskTitle = name });
        }

        public async Task<List<TodoTask>> GetTasks()
        {
            await Initialize();

            return await connection.Table<TodoTask>().ToListAsync();
        }

        public async Task<TodoTask> GetTask(int id)
        {
            return await connection.Table<TodoTask>().FirstOrDefaultAsync(firstElement => firstElement.Id == id);
        }

        public async Task<int> DeleteTask(int id)
        {
            return await connection.DeleteAsync<TodoTask>(id);
        }
    }
}
