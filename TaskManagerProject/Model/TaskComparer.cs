using TaskManagerProject.Model.Enums;

namespace TaskManagerProject.Model;
using IdTools;

public class TaskComparer : IComparer<IdTaskContainer>
{
    public int Compare(IdTaskContainer? container1, IdTaskContainer? container2)
    {
        if ((container1 is null) || (container2 is null))
        {
            return 0;
        }
        else
        {
            var (task1, task2) = ((Task)container1, (Task)container2);
            return (task1.State == ExecutionState.Completed) && (task2.State == ExecutionState.InProgress)
                ? 1
                : -1;
        }
    }
}