using System.Collections;

namespace TaskManagerProject.Model.IdTools;

public class IdTaskContainer : IEnumerable
{
    public IdTaskContainer(IdGiver freeIdsGiver)
    {
        _freeIdsGiver = freeIdsGiver;
        Id = _freeIdsGiver.GiveId();
    }
    
    public IdTaskContainer? FindById(int lookingId)
    {
        foreach (IdTaskContainer container in this)
        {
            if (container.Id == lookingId)
            {
                return container;
            }
            var lookingContainer = container.FindById(lookingId);
            if (lookingContainer != null)
            {
                return lookingContainer;
            }
        }
        return null;
    }

    public void DeleteTaskById(int deletedElementId)
    {
        foreach (IdTaskContainer container in this)
        {
            if (container.Id == deletedElementId)
            {
                if (_completedTasks.Contains(container))
                {
                    _completedTasks.Remove(container);
                }
                else
                {
                    _inProgressTasks.Remove(container);
                }
                return;
            }
            else
            {
                container.DeleteTaskById(deletedElementId);
            }
        }
    }

    public void CompleteTask(int taskId)
    {
        foreach (IdTaskContainer container in this)
        {
            if (container.Id == taskId)
            {
                _completedTasks.Add(container);
                _inProgressTasks.Remove(container);
                return;
            }
            else
            {
                container.CompleteTask(taskId);
            }
        }
    }

    ~IdTaskContainer()
    {
        _freeIdsGiver.KeepId(Id);
    }

    public void AddTask(IdTaskContainer element)
    {
        _inProgressTasks.Add(element);
    } 

    private IdGiver _freeIdsGiver;
    public int Id { get; init; }

    public IEnumerator GetEnumerator() => new TaskContainerEnumerator(_completedTasks, _inProgressTasks);
    private List<IdTaskContainer> _completedTasks = new List<IdTaskContainer>();
    public List<IdTaskContainer> CompletedTasks => _completedTasks;
    private List<IdTaskContainer> _inProgressTasks = new List<IdTaskContainer>();
    public List<IdTaskContainer> InProgressTasks => _inProgressTasks;
}