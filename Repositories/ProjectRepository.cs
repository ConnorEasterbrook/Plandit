using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Plandit.Models;

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
            if (connection != null)
            {
                return;
            }

            connection = new SQLiteAsyncConnection(_databasePath);
            await connection.CreateTableAsync<ProjectModel>();
        }

        public async Task AddProject(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            await Initialize();
            int result = 0;
            result = await connection.InsertAsync(new ProjectModel { ProjectTitle = name });
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
            return await connection.DeleteAsync<ProjectModel>(id);
        }
    }
}
