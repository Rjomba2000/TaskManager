namespace TaskManager.Model;
using IdTools;

public class TaskManager
{
    public TaskManager()
    {
        _tasks = new IdContainer<Task>(_freeIdGiver);
    }

    public void AddTask(string taskInfo)
    {
        _tasks.AddElement(new Task(_freeIdGiver, taskInfo));
    }

    public void AddSubtask(int parentTaskId, string subtaskInfo)
    {
        var parentTask = (Task?)_tasks.FindById(parentTaskId);
        if (parentTask != null)
        {
            parentTask.AddSubtask(_freeIdGiver, subtaskInfo);
        }
        else
        {
            //error
        }
    }
    
    IdContainer<Task> _tasks;
    private IdGiver _freeIdGiver = new IdGiver();
}