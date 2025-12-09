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

        public List<Todo> Todos { get; set; }

        public Project(string name)
        {
            Id = new Random().Next(1, 100000); // Simple random ID generation
            Name = name;
            Todos = new List<Todo>();
        }
    }
}
