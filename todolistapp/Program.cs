using System;
using System.Collections.Generic;
class TaskItem
{
    public string Description { get; set; }
    public bool IsCompleted { get; set; }

    public TaskItem(string description)
    {
        Description = description;
        IsCompleted = false;
    }

    public void MarkComplete()
    {
        IsCompleted = true;
    }

    public override string ToString()
    {
        return $"{Description} - Tamamlandi mi: {IsCompleted}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<TaskItem> tasks = new List<TaskItem>();
        string input;

        do
        {
            Console.WriteLine("Yeni bir görev eklemek için '1', görevleri görüntülemek için '2', çikmak için 'q' tuşlayin.");
            input = Console.ReadLine();

            if (input == "1")
            {
                Console.Write("Görev tanimini girin: ");
                string description = Console.ReadLine();
                tasks.Add(new TaskItem(description));
            }
            else if (input == "2")
            {
                Console.WriteLine("Görevler Listesi:");
                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {tasks[i]}");
                }

                Console.WriteLine("Bir görevi tamamlandi olarak işaretlemek için görev numarasini girin:");
                int taskNumber;
                if (int.TryParse(Console.ReadLine(), out taskNumber) && taskNumber > 0 && taskNumber <= tasks.Count)
                {
                    tasks[taskNumber - 1].MarkComplete();
                    Console.WriteLine("Görev tamamlandi olarak işaretlendi.");
                }
            }

        } while (input != "q");
    }
}