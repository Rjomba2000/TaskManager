using TaskManagerProject.Model;
using TaskManagerProject.UI;

TaskManager manager = new TaskManager();
manager.AddTask("sosat");
manager.AddTask("lezat");
manager.AddSubtask(1, "srat");
manager.AddSubtask(3, "srat2");
manager.AddSubtask(3, "srat3");
manager.AddSubtask(3, "srat4");
manager.AddSubtask(1, "sosat2");
manager.CompleteTask(4);
manager.CompleteTask(2);
manager.CompleteTask(6);
manager.CompleteTask(5);
ConsoleDrawer.DrawAllTasks(manager);

