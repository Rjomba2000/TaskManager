namespace TaskManagerProject.Model.IdTools;

public class IdContainer<T>
{
    public IdContainer(IdGiver freeIdsGiver)
    {
        _freeIdsGiver = freeIdsGiver;
        Id = _freeIdsGiver.GiveId();
    }
    
    public IdContainer<T>? FindById(int lookingId)
    {
        foreach (var element in _elements)
        {
            if (element.Id == lookingId)
            {
                return element;
            }
            IdContainer<T>? lookingContainer = element.FindById(lookingId);
            if (lookingContainer != null)
            {
                return lookingContainer;
            }
        }
        return null;
    }

    public void DeleteElement(int deletedElementId)
    {
        foreach (var element in _elements)
        {
            if (element.Id == deletedElementId)
            {
                _elements.Remove(element);
                return;
            }
            else
            {
                element.DeleteElement(deletedElementId);
            }
        }
    }

    ~IdContainer()
    {
        _freeIdsGiver.KeepId(Id);
    }

    public void AddElement(IdContainer<T> element)
    {
        _elements.Add(element);
    } 

    private IdGiver _freeIdsGiver;
    public int Id { get; init; }
    private List<IdContainer<T>> _elements = new List<IdContainer<T>>();
    public List<IdContainer<T>> Elements => _elements;
}