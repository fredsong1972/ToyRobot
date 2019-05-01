namespace ToyRobot.Models
{
    public interface IRobot
    {
        int XPosition { get; set; }
        int YPosition { get; set; }
        Direction Facing { get; set; }
        bool Placed { get; set; }
    }
}
