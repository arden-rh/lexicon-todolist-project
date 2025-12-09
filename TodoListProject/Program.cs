/* Todo List */

using TodoListProject;

string UserInput;
TodoList ListOfTodos = new TodoList();
int IncompleteTodos = ListOfTodos.GetIncompleteTodos().Count;
int CompletedTodos = ListOfTodos.GetCompletedTodos().Count;


do
{
    ConsoleUI.DisplayWelcomeMessage(IncompleteTodos, CompletedTodos);
    // Menu
    ConsoleUI.DisplayMenu();
    int userChoice = ConsoleUI.GetUserChoice();

    switch (userChoice)
    {
        case 1:
            ListOfTodos.GetAllTodos();
            break;
        case 2:
            ListOfTodos.AddTodo();
            break;
        case 3:
            // Edit Todo
            break;
        case 4:
            // Save and Quit
            break;
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }

}
while (true);
