using System;
using System.Globalization;

namespace TaskManagementSystem
{
    public class Program
    {
        private static readonly TaskManager taskManager = new TaskManager();

        public static void Main()
        {
            Console.WriteLine("\nWelcome to WeDesign Solutions Task Management System");

            while (true)
            {
                ShowMenu();
                HandleUserInput();
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("1. Add a new task");
            Console.WriteLine("2. Update an existing task");
            Console.WriteLine("3. Mark a task as completed");
            Console.WriteLine("4. View tasks by due date");
            Console.WriteLine("5. Display all tasks");
            Console.WriteLine("6. Exit");
            Console.Write("Please select an option: ");
        }

        private static void HandleUserInput()
        {
            switch (Console.ReadLine())
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    UpdateTask();
                    break;
                case "3":
                    MarkTaskAsCompleted();
                    break;
                case "4":
                    ViewTasksByDueDate();
                    break;
                case "5":
                    taskManager.DisplayAllTasks();
                    break;
                case "6":
                    if (ConfirmExit())
                    {
                        Environment.Exit(0);
                    }
                    break;
                default:
                    Console.WriteLine("Invalid option. Please select a valid option.");
                    break;
            }
        }

        private static void AddTask()
        {
            Console.Write("Enter Title: ");
            string? title = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(title))
            {
                Console.WriteLine("Title cannot be empty. Please try again.");
                return;
            }

            Console.Write("Enter Description: ");
            string? description = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(description))
            {
                Console.WriteLine("Description cannot be empty. Please try again.");
                return;
            }

            Console.Write("Enter Due Date (yyyy-mm-dd): ");
            string? dateInput = Console.ReadLine()?.Trim();

            if (!DateTime.TryParseExact(dateInput, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime dueDate))
            {
                Console.WriteLine("Invalid date format. Please enter the date in yyyy-mm-dd format.");
                return;
            }

            taskManager.AddTask(new Task(title, description, dueDate));
        }

        private static void UpdateTask()
        {
            Console.Write("Enter the title of the task to update: ");
            string? titleToUpdate = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(titleToUpdate))
            {
                Console.WriteLine("Title cannot be empty. Please try again.");
                return;
            }

            Console.Write("Enter new Title: ");
            string? newTitle = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(newTitle))
            {
                Console.WriteLine("New title cannot be empty. Please try again.");
                return;
            }

            // Check if the new title already exists in another task
            if (taskManager.DoesTaskExist(newTitle))
            {
                Console.WriteLine("A task with this title already exists. Please choose a different title.");
                return;
            }

            Console.Write("Enter new Description: ");
            string? newDescription = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(newDescription))
            {
                Console.WriteLine("New description cannot be empty. Please try again.");
                return;
            }

            Console.Write("Enter new Due Date (yyyy-mm-dd): ");
            string? dateInput = Console.ReadLine()?.Trim();
            if (!DateTime.TryParseExact(dateInput, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime newDueDate))
            {
                Console.WriteLine("Invalid date format. Please enter the date in yyyy-mm-dd format.");
                return;
            }

            taskManager.UpdateTask(titleToUpdate, new Task(newTitle, newDescription, newDueDate));
        }

        private static void MarkTaskAsCompleted()
        {
            Console.Write("Enter the title of the task to mark as completed: ");
            string? titleToComplete = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(titleToComplete))
            {
                Console.WriteLine("Title cannot be empty. Please try again.");
                return;
            }

            if (!taskManager.DoesTaskExist(titleToComplete))
            {
                Console.WriteLine("Task not found.");
                return;
            }

            if (taskManager.IsTaskAlreadyCompleted(titleToComplete))
            {
                Console.WriteLine("Task is already marked as completed.");
                return;
            }

            taskManager.MarkTaskAsCompleted(titleToComplete);
        }

        private static void ViewTasksByDueDate()
        {
            Console.Write("Enter Due Date to filter tasks (yyyy-mm-dd): ");
            string? dateInput = Console.ReadLine()?.Trim();

            if (!DateTime.TryParseExact(dateInput, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime filterDate))
            {
                Console.WriteLine("Invalid date format. Please enter the date in yyyy-mm-dd format.");
                return;
            }

            var filteredTasks = taskManager.GetTasksByDueDate(filterDate);
            if (filteredTasks.Count == 0)
            {
                Console.WriteLine("No tasks found for the specified date.");
            }
            else
            {
                Console.WriteLine("Tasks due on " + filterDate.ToString("yyyy-MM-dd") + ":");
                filteredTasks.ForEach(task => Console.WriteLine(task.ToString()));
            }
        }

        private static bool ConfirmExit()
        {
            Console.Write("Are you sure you want to exit? (y/n): ");
            string? response = Console.ReadLine()?.Trim().ToLower();
            return response == "y";
        }
    }
}
