using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Plandit.Models
{
    [Table("tasks")]
    public class TodoTask
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(ProjectModel))]
        public int ProjectId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public ProjectModel Parent { get; set; }

        [MaxLength(100), Unique]
        public string TaskTitle { get; set; }
        /*public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime CompletedAt { get; set; }
        public DateTime DeadlineAt { get; set; }*/
    }
}
