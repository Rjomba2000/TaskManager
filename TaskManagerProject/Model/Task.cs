namespace TaskManagerProject.Model;
using Enums;
public class Task : IdTaskContainer
{
    public Task(IdGiver freeIdsGiver, string taskInfo) : base(freeIdsGiver)
    {
        Info = taskInfo;
    }

    public string Info {get; init; }
    public ExecutionState State { get; set; } = ExecutionState.InProgress;
}