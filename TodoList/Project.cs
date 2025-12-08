using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList
{
    public class Project
    {
        public string Name { get; set; }
        public List<Todo> Todos { get; set; }


        public Project(string name, Todo firstTodo)
        {

            if (firstTodo == null)
            {
                throw new ArgumentNullException(nameof(firstTodo), "First todo cannot be null");
            }

            Name = name;
            Todos = new List<Todo> { firstTodo };
          
        }

        //
        public void AddTodo(Todo todo)
        {
            if (todo != null)
            {
                Todos.Add(todo);
            }
        }

        //
        public void RemoveTodo(Todo todo) {
            if (todo != null)
            {
                Todos.Remove(todo);
            }
        }

        //


    }
}
