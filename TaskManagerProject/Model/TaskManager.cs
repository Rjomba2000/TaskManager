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
        if (!_groups.TryAdd(groupName, new Group()))
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
        if (_groups.ContainsKey(groupName))
        {
            Task? lookingTask = _tasks.FindInRoot(taskId);
            if (lookingTask != null)
            {
                _groups[groupName].AddTask(lookingTask);
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
        if (_groups.ContainsKey(groupName))
        {
            if (_groups[groupName].FindInRoot(taskId) != null)
            {
                _groups[groupName].Remove(taskId);
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

    public Dictionary<string, Group> Groups => _groups;
    private Dictionary<string, Group> _groups = new Dictionary<string, Group>();
    public IdTaskContainer Tasks => _tasks;
    private IdTaskContainer _tasks = new IdTaskContainer();
    private IdGiver _freeIdGiver = new IdGiver();
}