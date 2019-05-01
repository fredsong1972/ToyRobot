using System;
using ToyRobot.Models;
using Action = ToyRobot.Models.Action;

namespace ToyRobot.Services
{
    /// <summary>
    /// CommandFactory
    /// </summary>
    public class CommandFactory : ICommandFactory
    {
        /// <summary>
        /// Parsing string to Command
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Command Parse(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return null;
            string commandText = text.ToUpper().Trim();

            char[] delimiterChars = { ',', ' ' };
            string[] parts = commandText.Split(delimiterChars,StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
                return null;
            Command command = null;
            switch (parts[0])
            {
                case "PLACE":
                {
                    if (parts.Length < 4)
                        return null;
                    if (!Int32.TryParse(parts[1], out int xPosition))
                        return null;
                    if (!Int32.TryParse(parts[2], out int yPosition))
                        return null;
                    Direction facing;
                    if (!Enum.TryParse(parts[3], false, out facing))
                        return null;
                    command = new Command { Action = Action.PLACE, XPosition = xPosition, YPosition = yPosition, Facing = facing};
                        break;
                }
                case "MOVE":
                    command = new Command{Action = Action.MOVE};
                    break;
                case "LEFT":
                    command = new Command {Action = Action.LEFT};
                    break;
                case "RIGHT":
                    command = new Command {Action = Action.RIGHT};
                    break;
                case "REPORT":
                    command = new Command { Action = Action.REPORT };
                    break;
            }
           
            return command;
        }
    }
}
