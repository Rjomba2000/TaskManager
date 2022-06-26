using System.Collections;
using TaskManagerProject.Model.Enums;

namespace TaskManagerProject.Model.IdTools;

public class TaskContainerEnumerator : IEnumerator
{
    private readonly List<IdTaskContainer> _completedTasks;
    private readonly List<IdTaskContainer> _inProgressTasks;
    private int _position = -1;

    public TaskContainerEnumerator(List<IdTaskContainer> completedTasks, List<IdTaskContainer> inProgressTasks)
    {
        _completedTasks = completedTasks;
        _inProgressTasks = inProgressTasks;
    }

    public object Current
    {
        get
        {
            if (_position == -1 || _position >= _completedTasks.Count + _inProgressTasks.Count)
                throw new ArgumentException();
            return _position < _inProgressTasks.Count
                ? _inProgressTasks[_position]
                : _completedTasks[_position - _inProgressTasks.Count];
        }
    }
    public bool MoveNext()
    {
        if (_position < _completedTasks.Count + _inProgressTasks.Count - 1)
        {
            _position++;
            return true;
        }
        else
            return false;
    }
    public void Reset() => _position = -1;

    public ExecutionState CurrentState()
    {
        if (_position == -1 || _position >= _completedTasks.Count + _inProgressTasks.Count)
            throw new ArgumentException();
        return _position < _inProgressTasks.Count
            ? ExecutionState.InProgress
            : ExecutionState.Completed;
    }
}