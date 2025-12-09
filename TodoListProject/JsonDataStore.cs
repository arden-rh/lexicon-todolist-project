using System.Collections.Generic;
using System.IO;
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
        public (List<Todo>, List<Project>) LoadState()
        {
            // Check if user data file exists
            if (File.Exists(UserDataFilePath))
            {
                return LoadFromFile(UserDataFilePath);
            }
            // If not, check for test data file
            else if (File.Exists(TestDataFilePath))
            {
                var testData = LoadFromFile(TestDataFilePath);
                SaveState(testData.Todos, testData.Projects);
                return testData;
            }
            // If neither file exists, return empty lists
            else
            {
                return (new List<Todo>(), new List<Project>());
            }
        }

        private (List<Todo> Todos, List<Project> Projects) LoadFromFile(string filePath)
        {

            string JsonString = File.ReadAllText(filePath);
            var SavedData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(JsonString);
            var Todos = JsonSerializer.Deserialize<List<Todo>>(SavedData["todos"].GetRawText());
            var Projects = JsonSerializer.Deserialize<List<Project>>(SavedData["projects"].GetRawText());
            return (Todos, Projects);
        }

        // Save the Todo list to a JSON file
        public void SaveState(List<Todo> Todos, List<Project> Projects)
        {
            var json = JsonSerializer.Serialize(new { Todos, Projects });
            File.WriteAllText(UserDataFilePath, json);
        }

    }
}
