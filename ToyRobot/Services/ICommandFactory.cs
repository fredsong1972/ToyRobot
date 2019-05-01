using ToyRobot.Models;

namespace ToyRobot.Services
{
    public interface ICommandFactory
    {
        Command Parse(string text);
    }
}
