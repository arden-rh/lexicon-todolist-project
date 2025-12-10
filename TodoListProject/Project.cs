using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace TodoListProject
{
    public class Project
    {
        public int Id { get; }
        public string Name { get; set; }
        public List<int> RelatedTodoIds { get; set; }

        // Constructor for creating a new Project
        public Project(string name)
        {
            Id = new Random().Next(1, 100000); // Simple random ID generation
            Name = name;
            RelatedTodoIds = new List<int>();
        }

        // Constructor for creating a Project with all properties
        internal Project(int id, string name, List<int> relatedTodoIds)
        {
            Id = id;
            Name = name;
            RelatedTodoIds = relatedTodoIds;
        }

        // Mapping method to create a Project from data record
        public static Project FromRecord(ProjectRecord record)
        {
            return new Project(
                record.Id,
                record.Name,
                record.TodoIds
            );
        }

        // Mapping method to convert a Project to data record
        public ProjectRecord ToRecord()
        {
            return new ProjectRecord(
                Id,
                Name,
                RelatedTodoIds
            );
        }
    }
}
