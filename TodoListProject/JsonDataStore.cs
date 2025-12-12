/* Json Data Store */

/// <summary>
/// Class <c>JsonDataStore</c> handles loading and saving the Todo list data to and from a JSON file.
/// </summary>

using System.Text.Json;

namespace TodoListProject
{
    public class JsonDataStore
    {
        // File path for storing the Todo list data
        private const string UserDataFilePath = "todo_data.json";
        // For testing purposes, and populating an empty data store
        private const string TestDataFilePath = "test_todo_data.json";

        // Load the list of Todos and Projects from a JSON file
        public (List<Todo> Todos, List<Project> Projects) LoadState()
        {

            // Check if user data file exists
            if (File.Exists(UserDataFilePath)) return LoadAndMap(UserDataFilePath);

            // If not, check for test data file
            else if (File.Exists(TestDataFilePath))
            {
                var TestData = LoadAndMap(TestDataFilePath);
                SaveState(TestData.Todos, TestData.Projects);
                return TestData;

            }
            // If neither file exists, return empty lists
            else return (new List<Todo>(), new List<Project>());
        }

        // Helper method to load and map data from a specified file
        private (List<Todo> Todos, List<Project> Projects) LoadAndMap(string filePath)
        {

            string JsonString = File.ReadAllText(filePath);
            ApplicationData? SavedData = JsonSerializer.Deserialize<ApplicationData>(JsonString);

            if (SavedData == null) return (new List<Todo>(), new List<Project>());
            
            // Map records to domain objects / current state lists
            List<Todo> CurrentTodos = SavedData.Todos.ConvertAll(todoRecord => Todo.FromRecord(todoRecord));
            List<Project> CurrentProjects = SavedData.Projects.ConvertAll(projectRecord => Project.FromRecord(projectRecord));

            return (CurrentTodos, CurrentProjects);
        }

        // Save the list of Todos and Projects to a JSON file
        public void SaveState(List<Todo> Todos, List<Project> Projects)
        {
            // Serialize the data to JSON and write to file
            List<TodoRecord> TodoRecords = Todos.ConvertAll(todo => todo.ToRecord());
            List<ProjectRecord> ProjectRecords = Projects.ConvertAll(project => project.ToRecord());

            var DataToSave = new ApplicationData(TodoRecords, ProjectRecords);
            var Options = new JsonSerializerOptions{ WriteIndented = true };

            string JsonString = JsonSerializer.Serialize(DataToSave, Options);
            // Write the JSON string to the user data file
            File.WriteAllText(UserDataFilePath, JsonString);
        }
    }
}
