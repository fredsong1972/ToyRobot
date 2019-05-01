namespace ToyRobot.Models
{
    public enum Direction
    {
        NORTH,
        SOUTH,
        EAST,
        WEST
    }

    public class Robot : IRobot
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public Direction Facing { get; set; }
        public bool Placed { get; set; }
    }
}
