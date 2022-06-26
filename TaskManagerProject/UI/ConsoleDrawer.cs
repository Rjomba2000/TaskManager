using TaskManagerProject.Model.Enums;
using TaskManagerProject.Model.IdTools;

namespace TaskManagerProject.UI;
using Spectre.Console;
using Model;

public static class ConsoleDrawer
{
    public static void DrawAllTasks(TaskManager taskManager)
    {
        var taskList = taskManager.Tasks.Elements;
        taskList.Sort(new TaskComparer());
        
        foreach (var container in taskList)
        {
            var task = (Task)container;
            var root = new Tree(GetStyledTaskInfoString(task));
            DrawTasks(root, task);
            AnsiConsole.Write(root);
        }
    }

    private static void DrawTasks(Tree parent, Task currentTask)
    {
        var taskList = currentTask.Elements;
        taskList.Sort(new TaskComparer());
        
        foreach (var container in taskList)
        {
            var task = (Task)container;
            var child = new Tree(GetStyledTaskInfoString(task));
            parent.AddNode(child);
            DrawTasks(child, task);
        }
    }

    private static string GetStyledTaskInfoString(Task task)
    {
        string styledString = task.Id + " " + task.Info;
        if (task.State == ExecutionState.Completed)
        {
            styledString = "[green]" + styledString + "[/]";
        }
        return styledString;
    }
}