namespace TaskManagerProject.Model;
using Enums;
using IdTools;
public class Task : IdContainer<Task>
{
    public Task(IdGiver freeIdsGiver, string taskInfo) : base(freeIdsGiver)
    {
        Info = taskInfo;
    }

    public string Info {get; init; }
    public ExecutionState State { get; set; } = ExecutionState.InProgress;
}