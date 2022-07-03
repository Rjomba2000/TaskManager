namespace TaskManagerProject.Model;
using Enums;
public class Task : IdTaskContainer
{
    public Task(IdGiver freeIdsGiver, string taskInfo)
    {
        Id = freeIdsGiver.GiveId();
        Info = taskInfo;
    }

    public int Id { get; init; }
    public string Info {get; init; }
    public ExecutionState State { get; set; } = ExecutionState.InProgress;
}