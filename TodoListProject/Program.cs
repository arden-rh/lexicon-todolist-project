/* Todo List */


/** TODO in this file
 * Implement Edit Todo functionality
 * 
 * 
 */

using TodoListProject;

JsonDataStore DataStore = new JsonDataStore();

List<Todo> CurrentTodos;
List<Project> CurrentProjects;

string UserInput;

// Load existing data
(CurrentTodos, CurrentProjects) = DataStore.LoadState();
TodoList Manager = new TodoList(CurrentTodos, CurrentProjects);


bool isRunning = true;

while (isRunning)
{
    int IncompleteTodos = Manager.GetIncompleteTodos().Count;
    int CompletedTodos = Manager.GetCompletedTodos().Count;
    ConsoleUI.DisplayWelcomeMessage(IncompleteTodos, CompletedTodos);
    // Menu
    ConsoleUI.DisplayMenu();
    int userChoice = ConsoleUI.GetUserMenuChoice();

    switch (userChoice)
    {
        case 1:
            Manager.GetListOfAllTodos();
            break;
        case 2:
            Manager.AddTodo();
            break;
        case 3:
            // Edit Todo
            ConsoleUI.DisplayEditMenu();
            int editChoice = ConsoleUI.GetUserMenuChoice();
            string TodoTitle;
            switch (editChoice)
            {
                case 1:
                    // Update Todo Details
                    break;
                case 2:
                    // Mark as Completed
                    Manager.GetListOfAllTodos();
                    TodoTitle = ConsoleUI.GetUserInput("Enter the title of the Todo to mark as completed: ");
                    Manager.MarkTodoAsCompleted(TodoTitle);
                    break;
                case 3:
                    // Remove Todo
                    Manager.GetListOfAllTodos();
                    TodoTitle = ConsoleUI.GetUserInput("Enter the title of the Todo to remove: ");
                    Manager.RemoveTodo(TodoTitle);
                    break;
                case 4:
                    // Return to Main Menu
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
            break;
        case 4:
            // Save and Quit
            DataStore.SaveState(CurrentTodos, CurrentProjects);
            isRunning = false;
            break;
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }
}

