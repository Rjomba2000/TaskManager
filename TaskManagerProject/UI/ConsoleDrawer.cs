using TaskManagerProject.Model.Enums;

namespace TaskManagerProject.UI;
using Spectre.Console;
using Model;

public static class ConsoleDrawer
{
    public static void DrawAll(TaskManager taskManager)
    {
        DrawGroups(taskManager);
        foreach (Task task in taskManager.Tasks)
        {
            var root = new Tree(GetStyledTaskInfoString(task));
            DrawTasks(root, task);
            AnsiConsole.Write(root);
        }
    }
    
    private static void DrawGroups(TaskManager taskManager)
    {
        foreach (var group in taskManager.Groups)
        {
            var groupTable = new Table();
            groupTable.AddColumn(group.Key);
            foreach (Task task in group.Value)
            {
                var root = new Tree(GetStyledTaskInfoString(task));
                DrawTasks(root, task);
                groupTable.AddRow(root);
            }
            AnsiConsole.Write(groupTable);
        }
    }

    private static void DrawTasks(Tree parent, Task currentTask)
    {
        foreach (Task task in currentTask)
        {
            var child = new Tree(GetStyledTaskInfoString(task));
            parent.AddNode(child);
            DrawTasks(child, task);
        }
    }

    private static string GetStyledTaskInfoString(Task task)
    {
        var idString = "[[ID: " + task.Id + "]] ";
        var stateString = "";
        if (task.State == ExecutionState.Completed)
        {
            stateString = "[green][[completed]][/] ";
        }
        else
        {
            if ((task.CompletedTasks.Count != 0) || (task.InProgressTasks.Count != 0))
            {
                stateString = "[[" + task.CompletedTasks.Count + "/" +
                              (task.CompletedTasks.Count + task.InProgressTasks.Count) +
                              "]] ";
            }
        }

        string resultString = idString + stateString +  task.Info;
        
        // if (task.State == ExecutionState.Completed)
        // {
        //     resultString = "[green]" + resultString + "[/]";
        // }
        return resultString;
    }
}