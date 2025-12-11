/* Project */

namespace TodoListProject
{
    public class Project
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public List<int> RelatedTodoIds { get; set; }

        // Constructor for creating a new Project
        public Project(int id, string name)
        {
            this.Id = id;
            this.Name = name;
            RelatedTodoIds = new List<int>();
        }

        // Constructor for creating a Project with all properties
        internal Project(int id, string name, List<int> relatedTodoIds)
        {
            this.Id = id;
            this.Name = name;
            this.RelatedTodoIds = relatedTodoIds;
        }

        // Mapping method to create a Project from data record
        public static Project FromRecord(ProjectRecord record)
        {
            return new Project(
                record.Id,
                record.Name,
                record.TodoIds
            );
        }

        // Mapping method to convert a Project to data record
        public ProjectRecord ToRecord()
        {
            return new ProjectRecord(
                Id,
                Name,
                RelatedTodoIds
            );
        }
    }
}
