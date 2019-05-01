namespace ToyRobot.Models
{
    public interface ICommand
    {
        Action Action { get; set; }
        int XPosition { get; set; }
        int YPosition { get; set; }
        Direction Facing { get; set; }
    }
}
