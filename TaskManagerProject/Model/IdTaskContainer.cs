using System.Collections;
using TaskManagerProject.Model.Enums;

namespace TaskManagerProject.Model;

public class IdTaskContainer : IEnumerable
{
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
        DeleteTaskInList(idGiver, deletedTaskId, _completedTasks);
        DeleteTaskInList(idGiver, deletedTaskId, _inProgressTasks);
        TryMoveSubtasksToCompletedList(this);
    }

    public void CompleteTask(int taskId)
    {
        foreach (var task in _inProgressTasks)
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
        if ((task._inProgressTasks.Count == 0) && (task._completedTasks.Count != 0))
        {
            task.State = ExecutionState.Completed;
        }
    }

    private static void TryMoveSubtasksToCompletedList(IdTaskContainer container)
    {
        Task? completedTask;
        while ((completedTask = container._inProgressTasks.Find(task => task.State == ExecutionState.Completed)) != null)
        {
            container._completedTasks.Add(completedTask);
            container._inProgressTasks.Remove(completedTask);
        }
    }
    
    private static void CompleteAllSubtasks(IdTaskContainer task)
    {
        foreach (var subtask in task._inProgressTasks)
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
            _inProgressTasks.Add(task);
        }
        else
        {
            _completedTasks.Add(task);
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

    public IEnumerator GetEnumerator() => new TaskContainerEnumerator(_completedTasks, _inProgressTasks);
    protected List<Task> _completedTasks = new List<Task>();
    public List<Task> CompletedTasks => _completedTasks;
    protected List<Task> _inProgressTasks = new List<Task>();
    public List<Task> InProgressTasks => _inProgressTasks;
}