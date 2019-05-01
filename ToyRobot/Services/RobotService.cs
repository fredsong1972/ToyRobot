using System;
using System.Collections.Generic;
using System.Text;
using ToyRobot.Models;
using Action = ToyRobot.Models.Action;

namespace ToyRobot.Services
{
    /// <summary>
    /// Robot Service 
    /// </summary>
    public class RobotService : IRobotService
    {
        private const int XLeft = 0;
        private const int YBottom = 0;

        private const int XRight = 5;
        private const int YTop = 5;

        private readonly IRobot _robot;
        private readonly ICommandFactory _commandFactory;
        /// <summary>
        /// Inject robot, commandFactory in constructor
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="commandFactory"></param>
        public RobotService(IRobot robot, ICommandFactory commandFactory)
        {
            _robot = robot;
            _commandFactory = commandFactory;
        }
        /// <summary>
        /// Run input command string
        /// </summary>
        /// <param name="text"></param>
        public void Run(string text)
        {
            var command = _commandFactory.Parse(text);
            if (command != null)
                RunCommand(command);
        }

        /// <summary>
        /// Run command
        /// </summary>
        /// <param name="command"></param>
        private void RunCommand(ICommand command)
        {
            if (!_robot.Placed && command.Action != Action.PLACE)
                return;
            switch (command.Action)
            {
                case Action.PLACE:
                    Place(command.XPosition, command.YPosition, command.Facing);
                    break;
                case Action.MOVE:
                    Move();
                    break;
                case Action.LEFT:
                    Left();
                    break;
                case Action.RIGHT:
                    Right();
                    break;
                case Action.REPORT:
                    Report();
                    break;
            }
        }
        /// <summary>
        /// Validate position to make sure robot not fall off
        /// </summary>
        /// <param name="xPosition"></param>
        /// <param name="yPosition"></param>
        /// <returns></returns>
        private bool ValidatePosition(int xPosition, int yPosition)
        {
            if ((xPosition < XLeft) || (yPosition < YBottom))
                return false;
            if ((xPosition > XRight) || (yPosition > YTop))
                return false;
            return true;
        }
        /// <summary>
        /// Place
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="facing"></param>
        private void Place(int x, int y, Direction facing)
        {
            if (!ValidatePosition(x, y))
                return;
            _robot.XPosition = x;
            _robot.YPosition = y;
            _robot.Facing = facing;
            _robot.Placed = true;
        }
        /// <summary>
        /// Move
        /// </summary>
        private void Move()
        {
            var x = _robot.XPosition;
            var y = _robot.YPosition;
            switch (_robot.Facing)
            {
                case Direction.NORTH:
                    y++;
                    break;
                case Direction.SOUTH:
                    y--;
                    break;
                case Direction.EAST:
                    x++;
                    break;
                case Direction.WEST:
                    x--;
                    break;
            }

            if (ValidatePosition(x, y))
            {
                _robot.XPosition = x;
                _robot.YPosition = y;
            }
        }

        /// <summary>
        /// Turn left
        /// </summary>
        private void Left()
        {
            switch (_robot.Facing)
            {
                case Direction.NORTH:
                    _robot.Facing = Direction.WEST;
                    break;
                case Direction.SOUTH:
                    _robot.Facing = Direction.EAST;
                    break;
                case Direction.EAST:
                    _robot.Facing = Direction.NORTH;
                    break;
                case Direction.WEST:
                    _robot.Facing = Direction.SOUTH;
                    break;
            }
        }
        /// <summary>
        /// Turn right
        /// </summary>
        private void Right()
        {
            switch (_robot.Facing)
            {
                case Direction.NORTH:
                    _robot.Facing = Direction.EAST;
                    break;
                case Direction.SOUTH:
                    _robot.Facing = Direction.WEST;
                    break;
                case Direction.EAST:
                    _robot.Facing = Direction.SOUTH;
                    break;
                case Direction.WEST:
                    _robot.Facing = Direction.NORTH;
                    break;
            }
        }
        /// <summary>
        /// Report position
        /// </summary>
        private void Report()
        {
            Console.WriteLine($"Output: {_robot.XPosition},{_robot.YPosition}, {_robot.Facing}");
        }
    }
}
