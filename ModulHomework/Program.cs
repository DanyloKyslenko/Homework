using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<string[]> taskList = new List<string[]>();

        while (true)
        {
            Console.Write("\n Enter command: ");
            Console.Write("\n \"add-item\" - add new task \n \"remove-item\" - remove task \n \"mark-as\" - mark item as сompleted or not implemented \n \"show\" - show tasks and status \n \"exit\" - close program \n");
            string command = Console.ReadLine();

            if (command == "add-item")
            {
                Console.Write("Enter task: ");
                string task = Console.ReadLine();
                AddItem(taskList, task);
            }
            else if (command == "remove-item")
            {
                Console.Write("Enter task: ");
                string task = Console.ReadLine();
                RemoveItem(taskList, task);
            }
            else if (command == "mark-as")
            {
                Console.Write("Enter status (1 - сompleted, 0 - not implemented): ");
                int status = int.Parse(Console.ReadLine());
                Console.Write("Enter task: ");
                string task = Console.ReadLine();
                string date = "";
                if (status == 1)
                {
                    Console.Write("Enter the execution date (or leave it blank for the current date): ");
                    date = Console.ReadLine();
                }
                MarkAs(taskList, status, task, date);
            }
            else if (command == "show")
            {
                Console.Write("Enter status (1 - completed, 0 - not implemented, * - all): ");
                string statusInput = Console.ReadLine();
                int status = (statusInput == "*") ? -1 : int.Parse(statusInput);
                Show(taskList, status);
            }
            else if (command == "exit")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid command. Please try again.");
            }
        }
    }

    static void AddItem(List<string[]> taskList, string task)
    {
        string taskLower = task.ToLower();
        foreach (var item in taskList)
        {
            if (taskLower == item[0].ToLower())
            {
                Console.WriteLine("An task with such description already exists");
                return;
            }
        }
        taskList.Add(new string[] { task, "0", "" });
    }

    static void RemoveItem(List<string[]> taskList, string task)
    {
        if (task == "*")
        {
            taskList.Clear();
        }
        else
        {
            string taskLower = task.ToLower();
            taskList.RemoveAll(item => taskLower == item[0].ToLower());
        }
    }

    static void MarkAs(List<string[]> taskList, int status, string task, string date)
    {
        string taskLower = task.ToLower();
        foreach (var item in taskList)
        {
            if (taskLower == item[0].ToLower())
            {
                item[1] = status.ToString();
                if (status == 1)
                {
                    if (string.IsNullOrWhiteSpace(date))
                    {
                        item[2] = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    }
                    else
                    {
                        if (DateTime.TryParse(date, out DateTime executionDate))
                        {
                            item[2] = executionDate.ToString("yyyy-MM-dd HH:mm");
                        }
                        else
                        {
                            Console.WriteLine("The date format is incorrect. Current date will be used.");
                            item[2] = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                        }
                    }
                }
                else
                {
                    item[2] = "";
                }
                return;
            }
        }
    }


    static void Show(List<string[]> taskList, int status)
    {
        foreach (var task in taskList)
        {
            if (status == -1 || task[1] == status.ToString())
            {
                string statusStr = task[1] == "1" ? "Completed" : "not implemented";
                Console.WriteLine($"Task: {task[0]}, Status: {statusStr}, Runtime: {task[2]}");
            }
        }
    }
}