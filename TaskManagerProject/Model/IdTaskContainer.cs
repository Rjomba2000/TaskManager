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

    public void DeleteTaskById(int deletedElementId)
    {
        foreach (Task task in this)
        {
            if (task.Id == deletedElementId)
            {
                if (_completedTasks.Contains(task))
                {
                    _completedTasks.Remove(task);
                }
                else
                {
                    _inProgressTasks.Remove(task);
                }
                return;
            }
            else
            {
                task.DeleteTaskById(deletedElementId);
            }
        }
    }

    public void CompleteTask(int taskId)
    {
        foreach (var task in _inProgressTasks)
        {
            if (task.Id == taskId)
            {
                task.State = ExecutionState.Completed;
            }
            else
            {
                task.CompleteTask(taskId);
                if ((task._inProgressTasks.Count == 0) && (task._completedTasks.Count != 0))
                {
                    task.State = ExecutionState.Completed;
                }
            }
        }

        Task? completedTask;
        while ((completedTask = _inProgressTasks.Find(task => task.State == ExecutionState.Completed)) != null)
        {
            _completedTasks.Add(completedTask);
            _inProgressTasks.Remove(completedTask);
        }
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