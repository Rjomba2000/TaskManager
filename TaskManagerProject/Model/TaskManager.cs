using TaskManagerProject.Model.Enums;

namespace TaskManagerProject.Model;
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
            parentTask.AddElement(new Task(_freeIdGiver, subtaskInfo));
        }
        else
        {
            //error
        }
    }

    public void CompleteTask(int taskId)
    {
        var task = (Task?)_tasks.FindById(taskId);
        if (task != null)
        {
            task.State = ExecutionState.Completed;
        }
        else
        {
            //error
        }
    }

    public void DeleteTask(int deletedElementId)
    {
        if (_tasks.FindById(deletedElementId) != null)
        {
            _tasks.DeleteElement(deletedElementId);
        }
        else
        {
            //error
        }
    }
    
    private IdContainer<Task> _tasks;
    private IdGiver _freeIdGiver = new IdGiver();
}