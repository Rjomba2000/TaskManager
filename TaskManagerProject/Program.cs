using TaskManagerProject.Model;
using TaskManagerProject.UI;

TaskManager manager = new TaskManager();
manager.AddTask("sosat");
manager.AddTask("lezat");
manager.AddSubtask(0, "srat");
manager.AddSubtask(2, "srat2");
manager.AddSubtask(2, "srat3");
manager.AddSubtask(2, "srat4");
manager.AddSubtask(0, "sosat2");
manager.CompleteTask(5);
manager.CompleteTask(4);
manager.CreateGroup("SUPERTASKS");
manager.CreateGroup("SMALLTASKS");
manager.AddToGroup(1, "SMALLTASKS");
manager.AddToGroup(0, "SUPERTASKS");
ConsoleDrawer.DrawAll(manager);
while (true)
{
    
}
