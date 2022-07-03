namespace TaskManagerProject.Model;

public class Group : IdTaskContainer
{
    public void Remove(int deletedTaskId)
    {
        foreach (var task in _inProgressTasks)
        {
            if (task.Id == deletedTaskId)
            {
                _inProgressTasks.Remove(task);
                return;
            }
        }
        foreach (var task in _inProgressTasks)
        {
            if (task.Id == deletedTaskId)
            {
                _completedTasks.Remove(task);
                return;
            }
        }
    }
}