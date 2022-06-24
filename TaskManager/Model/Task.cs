namespace TaskManager.Model;
using Enums;
using IdTools;
public class Task : IdContainer<Task>
{
    public Task(IdGiver freeIdsGiver, string taskInfo) : base(freeIdsGiver)
    {
        Info = taskInfo;
    }

    public void AddSubtask(IdGiver freeIdsGiver, string subtaskInfo)
    {
        AddElement(new Task(freeIdsGiver, subtaskInfo));
    }

    public void DeleteSubtask(int taskId)
    {
        
    }

    public string Info {get; init; }
    private ExecutionState State { get; set; } = ExecutionState.InProgress;
}