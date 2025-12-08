/* Todo List */

using TodoListProject;

string UserInput;
TodoList ListOfTodos = new TodoList();
int IncompleteTodos = ListOfTodos.GetIncompleteTodos().Count;
int CompletedTodos = ListOfTodos.GetCompletedTodos().Count;


do
{
    Console.WriteLine("Welcome to the Todo List Application!");
    Console.WriteLine($"You have {IncompleteTodos} tasks to do and {CompletedTodos} completed tasks.");
    // Menu
    Console.WriteLine("Pick an option:");
    Console.WriteLine("(1) Show Todo List (by date or project");
    Console.WriteLine("(2) Add New Todo");
    Console.WriteLine("(3) Edit Todo (update, mark as completed, remove)");
    Console.WriteLine("(4) Save and Quit");
    break;

}
while (true);
