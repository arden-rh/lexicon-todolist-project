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

bool isRunning = true;

while (isRunning)
{
    ConsoleUI.DisplayWelcomeMessage(IncompleteTodos, CompletedTodos);
    // Menu
    ConsoleUI.DisplayMenu();
    int userChoice = ConsoleUI.GetUserChoice();

    switch (userChoice)
    {
        case 1:
            Manager.GetAllTodos();
            break;
        case 2:
            Manager.AddTodo();
            break;
        case 3:
            // Edit Todo
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

