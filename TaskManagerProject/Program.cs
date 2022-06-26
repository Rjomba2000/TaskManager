using TaskManagerProject.Model;
using TaskManagerProject.UI;

TaskManager manager = new TaskManager();
manager.AddTask("sosat");
manager.AddTask("lezat");
manager.AddSubtask(1, "srat");
manager.AddSubtask(3, "srat2");
manager.AddSubtask(3, "srat3");
manager.CompleteTask(4);
manager.CompleteTask(1);
ConsoleDrawer.DrawAllTasks(manager);

