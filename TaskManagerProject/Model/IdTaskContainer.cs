using System.Collections;
using TaskManagerProject.Model.Enums;

namespace TaskManagerProject.Model;

public class IdTaskContainer : IEnumerable
{
    public IdTaskContainer(IdGiver freeIdsGiver)
    {
        _freeIdsGiver = freeIdsGiver;
        Id = _freeIdsGiver.GiveId();
    }
    
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

    private static void DeleteTaskInList(int deletedTaskId, List<Task> taskList)
    {
        foreach (var task in taskList)
        {
            if (task.Id == deletedTaskId)
            {
                taskList.Remove(task);
                return;
            }
            else
            {
                task.DeleteTaskById(deletedTaskId);
                TryChangeTaskState(task);
            }
        }
    }

    public void DeleteTaskById(int deletedTaskId)
    {
        DeleteTaskInList(deletedTaskId, _completedTasks);
        DeleteTaskInList(deletedTaskId, _inProgressTasks);
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

    ~IdTaskContainer()
    {
        _freeIdsGiver.KeepId(Id);
    }

    public void AddTask(Task task)
    {
        _inProgressTasks.Add(task);
    } 

    private IdGiver _freeIdsGiver;
    public int Id { get; init; }

    public IEnumerator GetEnumerator() => new TaskContainerEnumerator(_completedTasks, _inProgressTasks);
    private List<Task> _completedTasks = new List<Task>();
    public List<Task> CompletedTasks => _completedTasks;
    private List<Task> _inProgressTasks = new List<Task>();
    public List<Task> InProgressTasks => _inProgressTasks;
}