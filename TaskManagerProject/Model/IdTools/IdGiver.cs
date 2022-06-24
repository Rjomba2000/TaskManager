namespace TaskManagerProject.Model.IdTools;

public class IdGiver
{
    public int GiveId()
    {
        if (freeIds.Count == 0)
        {
            _reservedId++;
            return _reservedId - 1;
        }
        else
        {
            return freeIds.Pop();
        }
    }

    public void KeepId(int id)
    {
        freeIds.Push(id);
    }

    private int _reservedId = 0;
    private Stack<int> freeIds = new Stack<int>();
}