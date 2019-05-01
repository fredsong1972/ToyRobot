using NSubstitute;
using TestStack.BDDfy;
using Xunit;
using ToyRobot.Models;
using ToyRobot.Services;

namespace ToyRobot.Test.Services
{
    public class RobotServiceTest
    {
        private readonly RobotService _subject;
        private readonly ICommandFactory _commandFactory;
        private readonly IRobot _robot;
        private ICommand _command;
        private int _yOriginalPosition;
        private int _xOriginalPosition;
        private Direction _originalFacing;
        
        public RobotServiceTest()
        {
            _commandFactory = Substitute.For<ICommandFactory>();
            _robot = new Robot {XPosition = 1, YPosition = 1, Facing = Direction.NORTH, Placed = false};
            _xOriginalPosition = _robot.XPosition;
            _yOriginalPosition = _robot.YPosition;
            _originalFacing = _robot.Facing;
            _subject = new RobotService(_robot, _commandFactory);
        }

        #region Facts
        [Fact]
        public void PlaceShouldSucceed()
        {
            this.Given(x => x.GivenPlace())
                .When(x => x.WhenCallRun())
                .Then(x => x.ThenShouldBePlaced())
                .BDDfy();
        }

        [Fact]
        public void PlaceInvalidPositionsShouldIgnore()
        {
            this.Given(x => x.GivenPlaceInvalidPositions())
                .When(x => x.WhenCallRun())
                .Then(x => x.ThenShouldIgnored())
                .BDDfy();
        }

        [Fact]
        public void MoveIgnoredIfRobotNotPlaced()
        {
            this.Given(x => x.GivenMoveNotPlacedRobot())
                .When(x => x.WhenCallRun())
                .Then(x => x.ThenShouldIgnored())
                .BDDfy();
        }

        [Fact]
        public void MoveSouthShouldDownOne()
        {
            this.Given(x => x.GivenMoveSouth())
                .When(x => x.WhenCallRun())
                .Then(x => x.ThenShouldDownOne())
                .BDDfy();
        }

        [Fact]
        public void MoveNorthShouldUpOne()
        {
            this.Given(x => x.GivenMoveNorth())
                .When(x => x.WhenCallRun())
                .Then(x => x.ThenShouldUpOne())
                .BDDfy();
        }

        [Fact]
        public void MoveWestShouldLeftOne()
        {
            this.Given(x => x.GivenMoveWest())
                .When(x => x.WhenCallRun())
                .Then(x => x.ThenShouldLeftOne())
                .BDDfy();
        }

        [Fact]
        public void MoveEastShouldRightOne()
        {
            this.Given(x => x.GivenMoveEast())
                .When(x => x.WhenCallRun())
                .Then(x => x.ThenShouldRightOne())
                .BDDfy();
        }

        [Fact]
        public void TurnLeftFromNorthShouldFaceWest()
        {
            this.Given(x => x.GivenTurnLeftFromNorth())
                .When(x => x.WhenCallRun())
                .Then(x => x.ThenShouldFacingWest())
                .BDDfy();
        }

        [Fact]
        public void TurnLeftFromSouthShouldFaceEast()
        {
            this.Given(x => x.GivenTurnLeftFromSouth())
                .When(x => x.WhenCallRun())
                .Then(x => x.ThenShouldFacingEast())
                .BDDfy();
        }

        [Fact]
        public void TurnLeftFromWestShouldFaceSouth()
        {
            this.Given(x => x.GivenTurnLeftFromWest())
                .When(x => x.WhenCallRun())
                .Then(x => x.ThenShouldFacingSouth())
                .BDDfy();
        }

        [Fact]
        public void TurnLeftFromEastShouldFaceNorth()
        {
            this.Given(x => x.GivenTurnLeftFromEast())
                .When(x => x.WhenCallRun())
                .Then(x => x.ThenShouldFacingNorth())
                .BDDfy();
        }

        [Fact]
        public void TurnRightFromNorthShouldFaceEast()
        {
            this.Given(x => x.GivenTurnRightFromNorth())
                .When(x => x.WhenCallRun())
                .Then(x => x.ThenShouldFacingEast())
                .BDDfy();
        }

        [Fact]
        public void TurnRightFromSouthShouldFaceWest()
        {
            this.Given(x => x.GivenTurnRightFromSouth())
                .When(x => x.WhenCallRun())
                .Then(x => x.ThenShouldFacingWest())
                .BDDfy();
        }

        [Fact]
        public void TurnRightFromWestShouldFaceNorth()
        {
            this.Given(x => x.GivenTurnRightFromWest())
                .When(x => x.WhenCallRun())
                .Then(x => x.ThenShouldFacingNorth())
                .BDDfy();
        }

        [Fact]
        public void TurnRightFromEastShouldFaceSouth()
        {
            this.Given(x => x.GivenTurnRightFromEast())
                .When(x => x.WhenCallRun())
                .Then(x => x.ThenShouldFacingSouth())
                .BDDfy();
        }

        [Fact]
        public void ReportShouldSucceed()
        {
            this.Given(x => x.GiveReport())
                .When(x => x.WhenCallRun())
                .Then(x => x.ThenItShouldBeSuccessful())
                .BDDfy();
        }
        #endregion

        #region Givens

        private void GivenPlace()
        {
            _command = new Command {Action = Action.PLACE, Facing = Direction.EAST, XPosition = 1, YPosition = 1};
            _commandFactory.Parse(Arg.Any<string>()).Returns(_command);
        }

        private void GivenPlaceInvalidPositions()
        {
            _command = new Command {Action = Action.PLACE, Facing = Direction.EAST, XPosition = -1, YPosition = 7};
            _commandFactory.Parse(Arg.Any<string>()).Returns(_command);
        }
        private void GivenMoveNotPlacedRobot()
        {
            _command = new Command {Action = Action.MOVE};
            _robot.Facing = Direction.SOUTH;
            _robot.Placed = false;
            _originalFacing = _robot.Facing;
            _commandFactory.Parse(Arg.Any<string>()).Returns(_command);
        }

        private void GivenMoveSouth()
        {
            _command = new Command {Action = Action.MOVE};
            _robot.Facing = Direction.SOUTH;
            _robot.Placed = true;
            _originalFacing = _robot.Facing;
            _commandFactory.Parse(Arg.Any<string>()).Returns(_command);
        }

        private void GivenMoveNorth()
        {
            _command = new Command {Action = Action.MOVE};
            _robot.Facing = Direction.NORTH;
            _robot.Placed = true;
            _originalFacing = _robot.Facing;
            _commandFactory.Parse(Arg.Any<string>()).Returns(_command);
        }

        private void GivenMoveWest()
        {
            _command = new Command {Action = Action.MOVE};
            _robot.Facing = Direction.WEST;
            _robot.Placed = true;
            _originalFacing = _robot.Facing;
            _commandFactory.Parse(Arg.Any<string>()).Returns(_command);
        }

        private void GivenMoveEast()
        {
            _command = new Command {Action = Action.MOVE};
            _robot.Facing = Direction.EAST;
            _robot.Placed = true;
            _originalFacing = _robot.Facing;
            _commandFactory.Parse(Arg.Any<string>()).Returns(_command);
        }

        private void GivenTurnLeftFromNorth()
        {
            _command = new Command {Action = Action.LEFT};
            _robot.Facing = Direction.NORTH;
            _robot.Placed = true;
            _originalFacing = _robot.Facing;
            _commandFactory.Parse(Arg.Any<string>()).Returns(_command);
        }

        private void GivenTurnLeftFromSouth()
        {
            _command = new Command {Action = Action.LEFT};
            _robot.Facing = Direction.SOUTH;
            _robot.Placed = true;
            _originalFacing = _robot.Facing;
            _commandFactory.Parse(Arg.Any<string>()).Returns(_command);
        }

        private void GivenTurnLeftFromWest()
        {
            _command = new Command {Action = Action.LEFT};
            _robot.Facing = Direction.WEST;
            _robot.Placed = true;
            _originalFacing = _robot.Facing;
            _commandFactory.Parse(Arg.Any<string>()).Returns(_command);
        }

        private void GivenTurnLeftFromEast()
        {
            _command = new Command {Action = Action.LEFT};
            _robot.Facing = Direction.EAST;
            _robot.Placed = true;
            _originalFacing = _robot.Facing;
            _commandFactory.Parse(Arg.Any<string>()).Returns(_command);
        }

        private void GivenTurnRightFromNorth()
        {
            _command = new Command {Action = Action.RIGHT};
            _robot.Facing = Direction.NORTH;
            _robot.Placed = true;
            _originalFacing = _robot.Facing;
            _commandFactory.Parse(Arg.Any<string>()).Returns(_command);
        }

        private void GivenTurnRightFromSouth()
        {
            _command = new Command {Action = Action.RIGHT};
            _robot.Facing = Direction.SOUTH;
            _robot.Placed = true;
            _originalFacing = _robot.Facing;
            _commandFactory.Parse(Arg.Any<string>()).Returns(_command);
        }

        private void GivenTurnRightFromWest()
        {
            _command = new Command {Action = Action.RIGHT};
            _robot.Facing = Direction.WEST;
            _robot.Placed = true;
            _originalFacing = _robot.Facing;
            _commandFactory.Parse(Arg.Any<string>()).Returns(_command);
        }

        private void GivenTurnRightFromEast()
        {
            _command = new Command {Action = Action.RIGHT};
            _robot.Facing = Direction.EAST;
            _robot.Placed = true;
            _originalFacing = _robot.Facing;
            _commandFactory.Parse(Arg.Any<string>()).Returns(_command);
        }

        private void GiveReport()
        {
            _command = new Command {Action = Action.REPORT};
            _robot.Placed = true;
            _commandFactory.Parse(Arg.Any<string>()).Returns(_command);
        }
        #endregion

        #region Whens

        private void WhenCallRun()
        {
            _subject.Run("test run");
        }
        #endregion

        #region Thens

        private void ThenShouldBePlaced()
        {
            Assert.True(_robot.Placed);
            Assert.Equal(_command.Facing, _robot.Facing);
            Assert.Equal(_command.XPosition, _robot.XPosition);
            Assert.Equal(_command.YPosition, _robot.YPosition);
        }

        private void ThenShouldIgnored()
        {
            Assert.Equal(_yOriginalPosition, _robot.YPosition);
            Assert.Equal(_xOriginalPosition, _robot.XPosition);
            Assert.Equal(_originalFacing, _robot.Facing);
        }
        private void ThenShouldDownOne()
        {
            Assert.Equal(_yOriginalPosition -1, _robot.YPosition);
        }

        private void ThenShouldUpOne()
        {
            Assert.Equal(_yOriginalPosition +1, _robot.YPosition);
        }

        private void ThenShouldLeftOne()
        {
            Assert.Equal(_xOriginalPosition -1, _robot.XPosition);
        }

        private void ThenShouldRightOne()
        {
            Assert.Equal(_xOriginalPosition +1, _robot.XPosition);
        }

        private void ThenShouldFacingNorth()
        {
            Assert.Equal(Direction.NORTH, _robot.Facing);
        }

        private void ThenShouldFacingSouth()
        {
            Assert.Equal(Direction.SOUTH, _robot.Facing);
        }

        private void ThenShouldFacingWest()
        {
            Assert.Equal(Direction.WEST, _robot.Facing);
        }

        private void ThenShouldFacingEast()
        {
            Assert.Equal(Direction.EAST, _robot.Facing);
        }

        private void ThenItShouldBeSuccessful()
        { }
        #endregion
    }
}
