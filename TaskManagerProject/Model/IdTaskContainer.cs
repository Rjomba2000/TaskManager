using System.Collections;
using TaskManagerProject.Model.Enums;

namespace TaskManagerProject.Model;

public class IdTaskContainer : IEnumerable
{
    public IEnumerator GetEnumerator() => new TaskContainerEnumerator(CompletedTasks, InProgressTasks);
    public readonly List<Task> CompletedTasks = new List<Task>();
    public readonly List<Task> InProgressTasks = new List<Task>();

    public Task? FindById(int lookingId)
    {
        foreach (Task task in this)
        {
            if (task.Id == lookingId)
            {
                return task;
            }
            var lookingTask = task.FindById(lookingId);
            if (lookingTask != null)
            {
                return lookingTask;
            }
        }
        return null;
    }

    private void DeleteTaskInList(IdGiver idGiver, int deletedTaskId, List<Task> taskList)
    {
        foreach (var task in taskList)
        {
            if (task.Id == deletedTaskId)
            {
                idGiver.KeepId(task.Id);
                task.FreeAllIds(idGiver);
                taskList.Remove(task);
                return;
            }
            else
            {
                task.DeleteTaskById(idGiver, deletedTaskId);
                TryChangeTaskState(task);
            }
        }
    }

    public void DeleteTaskById(IdGiver idGiver, int deletedTaskId)
    {
        DeleteTaskInList(idGiver, deletedTaskId, CompletedTasks);
        DeleteTaskInList(idGiver, deletedTaskId, InProgressTasks);
        TryMoveSubtasksToCompletedList(this);
    }

    public void CompleteTask(int taskId)
    {
        foreach (var task in InProgressTasks)
        {
            if (task.Id == taskId)
            {
                task.State = ExecutionState.Completed;
                CompleteAllSubtasks(task);
            }
            else
            {
                task.CompleteTask(taskId);
                TryChangeTaskState(task);
            }
        }

        TryMoveSubtasksToCompletedList(this);
    }

    private static void TryChangeTaskState(Task task)
    {
        if ((task.InProgressTasks.Count == 0) && (task.CompletedTasks.Count != 0))
        {
            task.State = ExecutionState.Completed;
        }
    }

    private static void TryMoveSubtasksToCompletedList(IdTaskContainer container)
    {
        Task? completedTask;
        while ((completedTask = container.InProgressTasks.Find(task => task.State == ExecutionState.Completed)) != null)
        {
            container.CompletedTasks.Add(completedTask);
            container.InProgressTasks.Remove(completedTask);
        }
    }
    
    private static void CompleteAllSubtasks(IdTaskContainer task)
    {
        foreach (var subtask in task.InProgressTasks)
        {
            subtask.State = ExecutionState.Completed;
            CompleteAllSubtasks(subtask);
        }

        TryMoveSubtasksToCompletedList(task);
    }

    private void FreeAllIds(IdGiver idGiver)
    {
        foreach (Task task in this)
        {
            idGiver.KeepId(task.Id);
            task.FreeAllIds(idGiver);
        }
    }

    public void AddTask(Task task)
    {
        if (task.State == ExecutionState.InProgress)
        {
            InProgressTasks.Add(task);
        }
        else
        {
            CompletedTasks.Add(task);
        }
    }

    public Task? FindInRoot(int lookingId)
    {
        foreach (Task task in this)
        {
            if (lookingId == task.Id)
            {
                return task;
            }
        }

        return null;
    }
}