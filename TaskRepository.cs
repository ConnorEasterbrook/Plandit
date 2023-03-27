using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Plandit.Models;

namespace Plandit
{
    public  class TaskRepository
    {
        private string _databasePath;
        private SQLiteConnection connection;

        public TaskRepository(string dbPath)
        {
            _databasePath = dbPath;
        }

        private void Initialize()
        {
            if(connection != null)
            {
                return;
            }

            connection = new SQLiteConnection(_databasePath);
            connection.CreateTable<TodoTask>();
        }

        public void AddTask(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }

            Initialize();
            int result = 0;

            result = connection.Insert(new TodoTask { TaskTitle = name });
            Console.WriteLine(result);
        }

        public List<TodoTask> GetTasks()
        {
            Initialize();

            return connection.Table<TodoTask>().ToList();
        }

        public TodoTask GetTask(int id)
        {
            return connection.Table<TodoTask>().FirstOrDefault(firstElement => firstElement.Id == id);
        }

        public int DeleteTask(int id)
        {
            return connection.Delete<TodoTask>(id);
        }

        public int SaveTask(TodoTask task)
        {
            if (task.Id != 0)
            {
                connection.Update(task);
                return task.Id;
            }
            else
            {
                return connection.Insert(task);
            }
        }
    }
}
