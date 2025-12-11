/* Todo List */

/// <summary>
/// This is the main entry point for the Todo List application.
/// It initializes the data store, loads existing data, and starts the main application loop.
/// </summary>

using TodoListProject;

// Initialize Data Store
JsonDataStore DataStore = new JsonDataStore();

List<Todo> CurrentTodos;
List<Project> CurrentProjects;

// Load existing data
(CurrentTodos, CurrentProjects) = DataStore.LoadState();

// Initialize TodoListManager
TodoListManager Manager = new TodoListManager(CurrentTodos, CurrentProjects);

int NumberOfIncompleteTodos = Manager.GetIncompleteTodos().Count;
int NumberOfCompletedTodos = Manager.GetCompletedTodos().Count;
ConsoleUI.DisplayWelcomeMessage(NumberOfIncompleteTodos, NumberOfCompletedTodos);

bool isRunning = true;

/* Main loop */
while (isRunning)
{
    // Menu
    ConsoleUI.DisplayMainMenu();
    int userChoice = ConsoleUI.GetUserInputAsInt("Enter your choice: ");

    // Handle user choice for main menu
    switch (userChoice)
    {
        case 1:
            /* Show Todo List */
            Manager.GetListOfAllTodos("List of Todos (sorted by project and date)");

            // Return to List Menu after displaying chosen list
            while (true)
            {
                ConsoleUI.DisplayListMenu();
                int listChoice = ConsoleUI.GetUserInputAsInt("Enter your choice: ");

                // Handle user choice for list menu
                switch (listChoice)
                {
                    case 1:
                        // Show Todos sorted by date
                        Manager.GetListOfAllTodos("List of Todos (sorted by date)", true);
                        break;
                    case 2:
                        // Show Todos split by project
                        Utilities.PrintStatementInColor($"\nProjects", ConsoleColor.DarkGreen);
                        Utilities.PrintStatementInColor("=================================================", ConsoleColor.DarkGreen);

                        foreach (Project P in CurrentProjects)
                        {
                            ConsoleUI.DisplayProjectWithTodos(P, Manager);
                        }
                        break;
                    case 3:
                        // Show only incomplete Todos
                        var incompleteTodos = Manager.GetIncompleteTodos();
                        ConsoleUI.DisplayListOfTodos(incompleteTodos, "List of Incomplete Todos");
                        break;
                    case 4:
                        // Show only completed Todos
                        var completedTodos = Manager.GetCompletedTodos();
                        ConsoleUI.DisplayListOfTodos(completedTodos, "List of Completed Todos");
                        break;
                    case 5:
                        // Return to Main Menu
                        break;
                    default:
                        Utilities.PrintStatementInColor("\nInvalid choice. Please try again.", ConsoleColor.Red);
                        break;
                }
                // Break to Main Menu if user chose to return
                if (listChoice == 5)
                {
                    break;
                }
            }
            break;
        case 2:
            /* Add New Todo */
            Utilities.PrintStatementInColor("\n--- Add New Todo ---", ConsoleColor.DarkCyan);
            Utilities.PrintStatementInColor("Type 'Q' to cancel and return to the main menu.", ConsoleColor.Yellow);
            Console.WriteLine("-------------------------------------------------");

            while (true)
            {
                // Add Todo
                bool TodoIsAdded = Manager.AddTodo();

                if (!TodoIsAdded)
                {
                    break;
                }

                // Ask to add another Todo
                string AddAnotherInput = InputHelper.GetValidatedStringInput("\nWould you like to add another Todo? (Y/N): ", out bool isQuit);

                // Break if user chooses to quit or not add another
                if (isQuit || AddAnotherInput.Equals("N", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
            }
            break;
        case 3:
            /* Edit Todo */
            Manager.GetListOfAllTodos(showIds: true);
            ConsoleUI.DisplayEditMenu();
            int editChoice = ConsoleUI.GetUserInputAsInt("Enter your choice: ");
            int TodoId;

            // Handle user choice for edit menu
            switch (editChoice)
            {
                case 1:
                    // Update Todo Details
                    Utilities.PrintStatementInColor("\n--- Update Todo Details ---", ConsoleColor.DarkCyan);
                    TodoId = ConsoleUI.GetUserInputAsInt("Enter the ID of the Todo to update: ");
                    Manager.EditTodoDetails(TodoId);
                    break;
                case 2:
                    // Mark Todo as Completed
                    Utilities.PrintStatementInColor("\n--- Mark Todo as Completed ---", ConsoleColor.DarkCyan);
                    TodoId = ConsoleUI.GetUserInputAsInt("Enter the ID of the Todo to mark as completed: ");
                    Manager.MarkTodoAsCompleted(TodoId);
                    break;
                case 3:
                    // Remove Todo
                    Utilities.PrintStatementInColor("\n--- Remove Todo ---", ConsoleColor.DarkCyan);
                    TodoId = ConsoleUI.GetUserInputAsInt("Enter the ID of the Todo to remove: ");
                    Manager.RemoveTodo(TodoId);
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
            /* Save and Quit */
            // Save current state
            DataStore.SaveState(CurrentTodos, CurrentProjects);
            isRunning = false;
            break;
        default:
            /* Invalid choice */
            Utilities.PrintStatementInColor("\nInvalid choice. Please try again.", ConsoleColor.Red);
            break;
    }
}

