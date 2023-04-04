using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Plandit.Models;
using System.Diagnostics;

namespace Plandit.Repositories
{
    public class ProjectRepository
    {
        private string _databasePath;
        private SQLiteAsyncConnection connection;

        public ProjectRepository(string dbPath)
        {
            _databasePath = dbPath;
        }

        private async Task Initialize()
        {
            if(connection != null)
            {
                return;
            }

            connection = new SQLiteAsyncConnection(_databasePath);
            await connection.CreateTableAsync<ProjectModel>();
            await connection.CreateTableAsync<TodoTask>();
        }

        public async Task AddProject(ProjectModel project)
        {
            await Initialize();

            try
            {
                await connection.InsertAsync(new ProjectModel
                {
                    ProjectTitle = project.ProjectTitle,
                    ProjectDescription = project.ProjectDescription,
                    DateSpan = project.DateSpan,

                    Tasks = new List<TodoTask>()
                });

                Debug.WriteLine("Project added");
            }
            catch
            {
                Debug.WriteLine("Project not added");
                return;
            }
        }

        public async Task<List<ProjectModel>> GetProjects()
        {
            await Initialize();
            return await connection.Table<ProjectModel>().ToListAsync();
        }

        public async Task<ProjectModel> GetProject(int id)
        {
            return await connection.Table<ProjectModel>().FirstOrDefaultAsync(firstElement => firstElement.Id == id);
        }

        public async Task<int> DeleteProject(int id)
        {
            List<TodoTask> tasks = await GetTasks(id);

            try
            {

                foreach(TodoTask task in tasks)
                {
                    await DeleteTask(task.Id);
                    tasks.Remove(task);
                }

                return await connection.DeleteAsync<ProjectModel>(id);
            }
            catch
            {
                return 0;
            }
        }

        public async Task AddTask(string name, int projectID)
        {
            if(string.IsNullOrEmpty(name))
            {
                return;
            }

            await Initialize();
            await connection.InsertAsync(new TodoTask 
            { 
                ProjectId = projectID,
                TaskTitle = name 
            });
        }

        public async Task<List<TodoTask>> GetTasks(int projectID)
        {
            await Initialize();

            return await connection.Table<TodoTask>().Where(task => task.ProjectId == projectID).ToListAsync();
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
