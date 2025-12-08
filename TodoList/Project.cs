using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList
{
    public class Project
    {
        public string Name { get; set; }
        public List<Todo> Todos { get; set; }


        public Project(string name)
        {
            Name = name;
            Todos = new List<Todo>();
        }

        public void AddTodo(Todo todo)
        {
            Todos.Add(todo);
        }

        public void RemoveTodo(Todo todo) {
            Todos.Remove(todo); 
        }


    }
}
