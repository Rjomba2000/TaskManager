namespace TaskManagerProject.Model;

public class TaskManager
{
    public void AddTask(string taskInfo)
    {
        _tasks.AddTask(new Task(_freeIdGiver, taskInfo));
    }

    public void AddSubtask(int parentTaskId, string subtaskInfo)
    {
        var parentTask = _tasks.FindById(parentTaskId);
        if (parentTask != null)
        {
            parentTask.AddTask(new Task(_freeIdGiver, subtaskInfo));
        }
        else
        {
            //error
        }
    }

    public void CompleteTask(int taskId)
    {
        Tasks.CompleteTask(taskId);
    }

    public void DeleteTask(int deletedElementId)
    {
        if (_tasks.FindById(deletedElementId) != null)
        {
            _tasks.DeleteTaskById(_freeIdGiver, deletedElementId);
        }
        else
        {
            //error
        }
    }

    public void CreateGroup(string groupName)
    {
        if (!_groups.TryAdd(groupName, new List<Task>()))
        {
            //error
        }
    }
    
    public void DeleteGroup(string groupName)
    {
        if (_groups.ContainsKey(groupName))
        {
            _groups.Remove(groupName);
        }
        else
        {
            //error
        }
    }
    
    public void AddToGroup(int taskId, string groupName)
    {
        if (!_groups.ContainsKey(groupName))
        {
            Task? lookingTask = null;
            foreach (Task task in _tasks)
            {
                if (task.Id == taskId)
                {
                    lookingTask = task;
                }
            }
            if (lookingTask != null)
            {
                _groups[groupName].Add(lookingTask);
            }
            else
            {
                //error
            }
        }
        else
        {
            //error
        }
    }

    public void DeleteFromGroup(int taskId, string groupName)
    {
        if (!_groups.ContainsKey(groupName))
        {
            var deletedTask = _groups[groupName].Find(task => task.Id == taskId);
            if (deletedTask != null)
            {
                _groups[groupName].Remove(deletedTask);
            }
            else
            {
                //error
            }
        }
        else
        {
            //error
        }
    }

    public Dictionary<string, List<Task>> Groups => _groups;
    private Dictionary<string, List<Task>> _groups = new Dictionary<string, List<Task>>();
    public IdTaskContainer Tasks => _tasks;
    private IdTaskContainer _tasks = new IdTaskContainer();
    private IdGiver _freeIdGiver = new IdGiver();
}