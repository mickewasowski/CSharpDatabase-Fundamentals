namespace MyApp.Core.Interfaces
{
    public interface ICommandInterpreter
    {
        string Read(string[] inputArgs);
    }
}
