/* Data Records */

using System.Text.Json.Serialization;

namespace TodoListProject
{
    public record ApplicationData(
        [property: JsonPropertyName("todos")] List<TodoRecord> Todos,
        [property: JsonPropertyName("projects")] List<ProjectRecord> Projects
    );

    public record TodoRecord(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("title")] string Title,
        [property: JsonPropertyName("dueDate")] DateOnly DueDate,
        [property: JsonPropertyName("isCompleted")] bool IsCompleted,
        [property: JsonPropertyName("projectName")] string ParentProjectName
    );

    public record ProjectRecord(
        [property: JsonPropertyName("id")] int Id,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("todoIds")] List<int> TodoIds
    );
}
