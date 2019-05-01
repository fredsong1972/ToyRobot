namespace ToyRobot.Models
{
    public enum Action
    {
        PLACE,
        MOVE,
        LEFT,
        RIGHT,
        REPORT
    }

    public class Command : ICommand
    {
        public Action Action { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public Direction Facing { get; set; }
    }
}
