using TaskManagerProject.Model.Enums;

namespace TaskManagerProject.Model;
using IdTools;

public class TaskManager
{
    public TaskManager()
    {
        _tasks = new IdTaskContainer(_freeIdGiver);
    }

    public void AddTask(string taskInfo)
    {
        _tasks.AddTask(new Task(_freeIdGiver, taskInfo));
    }

    public void AddSubtask(int parentTaskId, string subtaskInfo)
    {
        var parentTask = (Task?)_tasks.FindById(parentTaskId);
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
            _tasks.DeleteTaskById(deletedElementId);
        }
        else
        {
            //error
        }
    }

    public IdTaskContainer Tasks => _tasks;
    private IdTaskContainer _tasks;
    private IdGiver _freeIdGiver = new IdGiver();
}