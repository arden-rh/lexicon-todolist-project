/* Todo List */

using TodoListProject;

string UserInput;
TodoList ListOfTodos = new TodoList();
int IncompleteTodos = ListOfTodos.GetIncompleteTodos().Count;
int CompletedTodos = ListOfTodos.GetCompletedTodos().Count;

