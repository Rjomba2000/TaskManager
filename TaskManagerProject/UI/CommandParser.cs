namespace TaskManagerProject.UI;

public class CommandParser
{
    private string _command;
    
    public CommandParser(string command)
    {
        _command = command.Trim();
    }

    public string GetStringParameter()
    {
        _command = _command.Trim() + " ";
        var stringParameter = _command.Substring(0, _command.IndexOf(' '));
        _command = _command.Substring(_command.IndexOf(' '));
        return stringParameter;
    }
    
    public int GetIntParameter()
    {
        _command = _command.Trim() + " ";
        var parameter = _command.Substring(0, _command.IndexOf(' '));
        _command = _command.Substring(_command.IndexOf(' '));
        return Int32.Parse(parameter);
    }

    public string GetOthers()
    {
        var result =  _command.Trim();
        _command = "";
        return result;
    }

    public bool IsEmpty()
    {
        _command = _command.Trim();
        return _command.Length == 0 ? true : false;
    }
}