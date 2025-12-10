/* Json Data Store */

using System.Text.Json;

namespace TodoListProject
{
    public class JsonDataStore
    {
        // File path for storing the Todo list data
        private const string UserDataFilePath = "todo_data.json";
        // For testing purposes, and populating an empty data store
        private const string TestDataFilePath = "test_todo_data.json";

        // Load the Todo list from a JSON file
        public (List<Todo> Todos, List<Project> Projects) LoadState()
        {

            // Check if user data file exists
            if (File.Exists(UserDataFilePath))
            {
                 return LoadAndMap(UserDataFilePath);
            }
            // If not, check for test data file
            else if (File.Exists(TestDataFilePath))
            {
                var testData = LoadAndMap(TestDataFilePath);
                SaveState(testData.Todos, testData.Projects);
                return testData;

            }
            // If neither file exists, return empty lists
            else
            {
                return (new List<Todo>(), new List<Project>());
            }
        }

        private (List<Todo> Todos, List<Project> Projects) LoadAndMap(string filePath)
        {

            string JsonString = File.ReadAllText(filePath);
            ApplicationData? SavedData = JsonSerializer.Deserialize<ApplicationData>(JsonString);

            if (SavedData == null)
            {
                return (new List<Todo>(), new List<Project>());
            }
            // Map records to domain objects
            List<Todo> currentTodos = SavedData.Todos.ConvertAll(todoRecord => Todo.FromRecord(todoRecord));
            List<Project> currentProjects = SavedData.Projects.ConvertAll(projectRecord => Project.FromRecord(projectRecord));

            return (currentTodos, currentProjects);
        }

        // Save the Todo list to a JSON file
        public void SaveState(List<Todo> Todos, List<Project> Projects)
        {

            // Serialize the data to JSON and write to file
            List<TodoRecord> todoRecords = Todos.ConvertAll(todo => todo.ToRecord());
            List<ProjectRecord> projectRecords = Projects.ConvertAll(project => project.ToRecord());

            var dataToSave = new ApplicationData(todoRecords, projectRecords);
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            string JsonString = JsonSerializer.Serialize(dataToSave, options);
            File.WriteAllText(UserDataFilePath, JsonString);
        }

    }
}
