using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Plandit.Models
{
    [Table("projects")]
    public class ProjectModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100), Unique]
        public string ProjectTitle { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<TodoTask> Tasks { get; set; }
    }
}
