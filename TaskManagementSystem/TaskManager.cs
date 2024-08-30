using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManagementSystem
{
    public class TaskManager
    {
        private readonly List<Task> tasks = new List<Task>();

        public void AddTask(Task newTask)
        {
            if (DoesTaskExist(newTask.Title))
            {
                Console.WriteLine("A task with this title already exists. Please choose a different title.");
                return;
            }

            tasks.Add(newTask);
            Console.WriteLine("Task added successfully!");
        }

        public void UpdateTask(string titleToUpdate, Task updatedTask)
        {
            var taskToUpdate = FindTaskByTitle(titleToUpdate);
            if (taskToUpdate == null)
            {
                Console.WriteLine("Task not found.");
                return;
            }

            taskToUpdate.Title = updatedTask.Title;
            taskToUpdate.Description = updatedTask.Description;
            taskToUpdate.DueDate = updatedTask.DueDate;
            Console.WriteLine("Task updated successfully!");
        }

        public void MarkTaskAsCompleted(string titleToComplete)
        {
            var taskToComplete = FindTaskByTitle(titleToComplete);
            if (taskToComplete == null)
            {
                Console.WriteLine("Task not found.");
                return;
            }

            taskToComplete.IsCompleted = true;
            Console.WriteLine("Task marked as completed!");
        }

        public List<Task> GetTasksByDueDate(DateTime dueDate) => tasks.Where(task => task.DueDate.Date == dueDate.Date).ToList();

        public void DisplayAllTasks()
        {
            if (!tasks.Any())
            {
                Console.WriteLine("No tasks available.");
                return;
            }

            Console.WriteLine("Task List:");
            tasks.ForEach(task => Console.WriteLine(task.ToString()));
        }

        public bool DoesTaskExist(string title) => tasks.Any(task => task.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

        public bool IsTaskAlreadyCompleted(string title)
        {
            var task = FindTaskByTitle(title);
            return task != null && task.IsCompleted;
        }

        private Task? FindTaskByTitle(string title) => tasks.FirstOrDefault(task => task.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }
}
