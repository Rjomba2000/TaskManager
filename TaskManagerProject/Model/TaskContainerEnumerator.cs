using System.Collections;
using TaskManagerProject.Model.Enums;

namespace TaskManagerProject.Model;

public class TaskContainerEnumerator : IEnumerator
{
    private readonly List<Task> _completedTasks;
    private readonly List<Task> _inProgressTasks;
    private int _position = -1;

    public TaskContainerEnumerator(List<Task> completedTasks, List<Task> inProgressTasks)
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