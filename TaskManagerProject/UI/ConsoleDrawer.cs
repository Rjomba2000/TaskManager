namespace TaskManagerProject.UI;
using Spectre.Console;
using Model;

public static class ConsoleDrawer
{
    public static void DrawAllTasks(TaskManager taskManager)
    {
        foreach (var container in taskManager.Tasks.Elements)
        {
            var task = (Task)container;
            var root = new Tree(task.Id + " " + task.Info);
            DrawTasks(root, task);
            AnsiConsole.Write(root);
        }
    }

    private static void DrawTasks(Tree parent, Task currentTask)
    {
        foreach (var container in currentTask.Elements)
        {
            var task = (Task)container;
            var child = new Tree(task.Id + " " + task.Info);
            parent.AddNode(child);
            DrawTasks(child, task);
        }
    }
}