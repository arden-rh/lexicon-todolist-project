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

int IncompleteTodos = Manager.GetIncompleteTodos().Count;
int CompletedTodos = Manager.GetCompletedTodos().Count;
ConsoleUI.DisplayWelcomeMessage(IncompleteTodos, CompletedTodos);

bool isRunning = true;

while (isRunning)
{
    // Menu
    ConsoleUI.DisplayMainMenu();
    int userChoice = ConsoleUI.GetUserMenuChoice();

    switch (userChoice)
    {
        case 1:
            Manager.GetListOfAllTodos();
            // Return to List Menu after displaying chosen list
            while (true)
            {
                ConsoleUI.DisplayListMenu();
                int listChoice = ConsoleUI.GetUserMenuChoice();
                switch (listChoice)
                {
                    case 1:
                        // Show Todos sorted by date
                        Manager.GetListOfAllTodos(true);
                        break;
                    case 2:
                        // Show Todos sorted by project and due date
                        Manager.GetListOfAllTodos();
                        break;
                    case 3:
                        // Show only incomplete Todos
                        var incompleteTodos = Manager.GetIncompleteTodos();
                        ConsoleUI.DisplayListOfTodos(incompleteTodos, "Incomplete Todos");
                        break;
                    case 4:
                        // Show only completed Todos
                        var completedTodos = Manager.GetCompletedTodos();
                        ConsoleUI.DisplayListOfTodos(completedTodos, "Completed Todos");
                        break;
                    case 5:
                        // Return to Main Menu
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                if (listChoice == 5)
                {
                    break;
                }
            }
            break;
        case 2:
            // Add New Todo
            while (true)
            {
                bool TodoIsAdded = Manager.AddTodo();

                if (!TodoIsAdded)
                {
                    break;
                }
                string AddAnotherInput = InputHelper.GetValidatedStringInput("\nWould you like to add another Todo? (Y/N): ", out bool isQuit);

                if (isQuit || AddAnotherInput.Equals("N", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
            }
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
                    TodoTitle = ConsoleUI.GetUserInput("\nEnter the title of the Todo to mark as completed: ");
                    Manager.MarkTodoAsCompleted(TodoTitle);
                    break;
                case 3:
                    // Remove Todo
                    Manager.GetListOfAllTodos();
                    TodoTitle = ConsoleUI.GetUserInput("\nEnter the title of the Todo to remove: ");
                    Manager.RemoveTodo(TodoTitle);
                    break;
                case 4:
                    // Return to Main Menu
                    break;
                default:
                    Console.WriteLine("\nInvalid choice. Please try again.");
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

