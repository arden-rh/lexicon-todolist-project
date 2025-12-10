/* Todo List */

namespace TodoListProject
{
    public class TodoList
    {
        public List<Todo> Todos { get; private set; }
        public List<Project> Projects { get; private set; }

        public TodoList(List<Todo> todos, List<Project> projects)
        {
            Todos = todos ?? new List<Todo>();
            Projects = projects ?? new List<Project>();
        }

        // Get all Todo items
        public void GetListOfAllTodos(bool sortByDate = false, bool showIds = false)
        {
            List<Todo> sortedTodos;

            // Sort todos by project name and due date
            if (sortByDate)
            {
                sortedTodos = Todos.OrderBy(todo => todo.DueDate).ToList();
            }
            else
            {
                sortedTodos = Todos.OrderBy(todo => todo.ParentProjectName).ThenBy(todo => todo.DueDate).ToList();
            }

            if (sortedTodos.Count == 0)
            {
                Utilities.PrintStatementInColor("No Todos to display.", ConsoleColor.Yellow);
                return;
            }

            if (showIds)
            {
                ConsoleUI.DisplayListOfTodos(sortedTodos, "List of Todos:", showIds: true);
                return;
            }

            ConsoleUI.DisplayListOfTodos(sortedTodos, "List of Todos:");

        }

        // Get all incomplete Todo items
        public List<Todo> GetIncompleteTodos()
        {
            return Todos.Where(todo => !todo.IsCompleted).ToList();
        }

        // Get all completed Todo items
        public List<Todo> GetCompletedTodos()
        {
            return Todos.Where(todo => todo.IsCompleted).ToList();
        }

        /* Add a new Todo item to the list */
        public bool AddTodo()
        {

            bool isQuit;
            Console.WriteLine("\nAdd a new Todo");
            Utilities.PrintStatementInColor("Type 'Q' at any time to return to the main menu.", ConsoleColor.Yellow);
            Console.WriteLine("---------------------------------------------------");

            string ProjectName;

            if (Projects != null)
            {
                Console.WriteLine($"List of current projects: ");
                foreach (Project project in Projects)
                {
                    Console.WriteLine($"- {project.Name}");
                }
                ProjectName = InputHelper.GetValidatedStringInput("Enter a new project name for the new Todo or one of the ones listed above", out isQuit);
                if (isQuit)
                {
                    return false;
                }
            }
            else
            {
                ProjectName = InputHelper.GetValidatedStringInput("Enter the project name of the new Todo", out isQuit);
                if (isQuit)
                {
                    return false;
                }
            }

            string Title = InputHelper.GetValidatedStringInput("Enter the title of the new Todo", out isQuit);
            if (isQuit)
            {
                return false;
            }
            DateOnly DueDate = InputHelper.GetValidatedDateInput("Enter the due date of the new Todo", out isQuit);
            if (isQuit)
            {
                return false;
            }

            Project ChosenProject = Projects.FirstOrDefault(p => p.Name.Equals(ProjectName, StringComparison.OrdinalIgnoreCase));

            Project TargetProject;

            if (ChosenProject != null)
            {
                TargetProject = ChosenProject;
            }
            else
            {
                TargetProject = new Project(ProjectName);
                Projects.Add(TargetProject);
                Utilities.PrintStatementInColor($"New project '{ProjectName}' created.", ConsoleColor.DarkGreen);
            }

            // Create the new Todo
            Todo NewTodo = new Todo(Title, DueDate, TargetProject.Name);

            // Link the new Todo to the Project
            TargetProject.RelatedTodoIds.Add(NewTodo.Id);

            // Add the new Todo to the Todo list
            Todos.Add(NewTodo);

            Utilities.PrintStatementInColor($"Todo '{Title}' added successfully.", ConsoleColor.Green);
            return true;
        }


        /* Mark a Todo item as completed */
        public bool MarkTodoAsCompleted(int todoId)
        {
            Todo? Todo = Todos.FirstOrDefault(t => t.Id == todoId);

            if (Todo == null)
            {
                Utilities.PrintStatementInColor("Todo not found.", ConsoleColor.Red);
                Utilities.PrintStatementInColor("Returning to main menu...", ConsoleColor.Yellow);
                return false;
            }
            if (Todo.IsCompleted)
            {
                Utilities.PrintStatementInColor("Todo is already marked as completed.", ConsoleColor.Yellow);
                Utilities.PrintStatementInColor("Returning to main menu...", ConsoleColor.Yellow);
                return false;
            }
            Todo.MarkAsCompleted();
            Utilities.PrintStatementInColor("Todo marked as completed successfully.", ConsoleColor.Green);
            return true;
        }
        // ------------------------------------------------------ NOT FINISHED ------------------------------------------------------
        /* Edit a Todo item */
        public void EditTodoDetails(int todoId)
        {
            // Find all todos matching the given title (case-insensitive)
            Todo TodoToEdit = Todos.FirstOrDefault(t => t.Id == todoId);

            if (TodoToEdit == null)
            {
                Utilities.PrintStatementInColor("Todo not found. Todo removal cancelled.", ConsoleColor.Red);
                Utilities.PrintStatementInColor("Returning to main menu...", ConsoleColor.Yellow);
                return;
            }

            // Get new details for the Todo
            Console.WriteLine("Enter new details for the Todo (leave blank to keep current value)");
            Utilities.PrintStatementInColor("Type 'Q' at any time to return to the main menu.", ConsoleColor.Yellow);
            string NewTitle = InputHelper.GetValidatedStringInput("Enter the new title for the Todo", TodoToEdit.Title, out bool isQuit);
            if (isQuit) return;

            DateOnly NewDueDate = InputHelper.GetValidatedDateInput("Enter the new due date for the Todo", TodoToEdit.DueDate, out isQuit);
            if (isQuit) return;
            
            bool NewIsCompleted = InputHelper.GetValidatedBoolInput("Is the Todo completed? (true/false)", TodoToEdit.IsCompleted, out isQuit);
            if (isQuit) return;

            string NewProjectName = InputHelper.GetValidatedStringInput("Enter the new project name for the Todo", TodoToEdit.ParentProjectName, out isQuit);
            if (isQuit) return;

            if (NewProjectName != TodoToEdit.ParentProjectName)
            {
                // Update the related todo IDs in the old project
                Project? OldProject = Projects.FirstOrDefault(p => p.Name == TodoToEdit.ParentProjectName);

                if (OldProject != null)
                {
                    OldProject.RelatedTodoIds.Remove(TodoToEdit.Id);
                }

                // Update the related todo IDs in the new project
                Project? NewProject = Projects.FirstOrDefault(p => p.Name == NewProjectName);

                if (NewProject == null)
                {
                    NewProject = new Project(NewProjectName);
                    Projects.Add(NewProject);
                    Utilities.PrintStatementInColor($"New project '{NewProjectName}' created.", ConsoleColor.DarkGreen);
                }

                NewProject.RelatedTodoIds.Add(TodoToEdit.Id);
            }

            // Update the Todo details
            TodoToEdit.EditTodo(newTitle: NewTitle, newDueDate: NewDueDate, isCompleted: NewIsCompleted, newProjectName: NewProjectName);

            Utilities.PrintStatementInColor("Todo edited successfully.", ConsoleColor.Green);
            return;
        }


        /* Remove a Todo item from the list */
        public void RemoveTodo(int todoId)
        {
            // Find the todo matching the id
            Todo TodoToRemove = Todos.FirstOrDefault(t => t.Id == todoId);

            if (TodoToRemove == null)
            {
                Utilities.PrintStatementInColor("Todo not found. Todo removal cancelled.", ConsoleColor.Red);
                Utilities.PrintStatementInColor("Returning to main menu...", ConsoleColor.Yellow);
                return;
            }

            // Confirm removal with the user
            Console.WriteLine("Is this the todo you want to remove?");
            Console.WriteLine($"Title: {TodoToRemove.Title}, Due Date: {TodoToRemove.DueDate}, Project: {TodoToRemove.ParentProjectName}, Completed: {TodoToRemove.IsCompleted}");
            string Confirmation = InputHelper.GetValidatedStringInput("(Y)es / (N)o: ", out bool isQuit);

            // User chose to not remove the todo or quit
            if (isQuit || Confirmation.Equals("N", StringComparison.OrdinalIgnoreCase) || Confirmation.Equals("NO", StringComparison.OrdinalIgnoreCase))
            {
                Utilities.PrintStatementInColor("Todo removal cancelled", ConsoleColor.Red);
                Utilities.PrintStatementInColor("Returning to main menu...", ConsoleColor.Yellow);
                return;
            }

            // User confirmed removal
            if (Confirmation.Equals("Y", StringComparison.OrdinalIgnoreCase) || Confirmation.Equals("YES", StringComparison.OrdinalIgnoreCase))
            {
                if (Projects != null)
                {
                    Project? ParentProject = Projects.FirstOrDefault(p => p.Name == TodoToRemove.ParentProjectName);

                    // Remove the todo ID from the parent project's related todo IDs
                    if (ParentProject != null)
                    {
                        ParentProject.RelatedTodoIds.Remove(TodoToRemove.Id);

                        // If the parent project has no more related todos, remove the project as well
                        if (ParentProject.RelatedTodoIds.Count == 0)
                        {
                            Projects.Remove(ParentProject);
                            Utilities.PrintStatementInColor($"Project '{ParentProject.Name}' has no more related todos and has been removed.", ConsoleColor.Yellow);
                        }
                    }
                }

                // Remove the todo from the list
                Todos.Remove(TodoToRemove);
                Utilities.PrintStatementInColor("Todo removed successfully.", ConsoleColor.Green);
            }
        }
    }
}
