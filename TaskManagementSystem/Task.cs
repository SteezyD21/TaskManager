namespace TaskManagementSystem
{
    public class Task
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; } = false;

        public Task(string title, string description, DateTime dueDate)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
        }

        public override string ToString()
        {
            return $"Title: {Title}\nDescription: {Description}\nDue Date: {DueDate:yyyy-MM-dd}\nCompleted: {(IsCompleted ? "Yes" : "No")}\n";
        }
    }
}
