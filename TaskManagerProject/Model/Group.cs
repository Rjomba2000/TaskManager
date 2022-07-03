namespace TaskManagerProject.Model;

public class Group : IdTaskContainer
{
    public void Remove(int deletedTaskId)
    {
        foreach (var task in InProgressTasks)
        {
            if (task.Id == deletedTaskId)
            {
                InProgressTasks.Remove(task);
                return;
            }
        }
        foreach (var task in InProgressTasks)
        {
            if (task.Id == deletedTaskId)
            {
                CompletedTasks.Remove(task);
                return;
            }
        }
    }
}