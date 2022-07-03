using TaskManagerProject.Model;
using TaskManagerProject.UI;

TaskManager manager = new TaskManager();
manager.AddTask("do something");
manager.AddTask("do something else");
manager.AddSubtask(0, "do something smaller");
manager.AddSubtask(2, "small task1");
manager.AddSubtask(2, "small task2");
manager.AddSubtask(2, "small task3");
manager.AddSubtask(0, "another small task");
manager.CompleteTask(5);
manager.CompleteTask(4);
manager.CreateGroup("MAIN_TASKS");
manager.CreateGroup("ANOTHER_TASKS");
manager.AddToGroup(1, "ANOTHER_TASKS");
manager.AddToGroup(0, "MAIN_TASKS");
ConsoleDrawer.DrawAll(manager);