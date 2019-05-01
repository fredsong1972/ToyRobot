using Xunit;
using TestStack.BDDfy;
using ToyRobot.Services;
using ToyRobot.Models;

namespace ToyRobot.Test.Services
{
    public class CommandFactoryTest
    {
        private string _testInput;
        private Command _testResult;
        private readonly CommandFactory _subject;

        public CommandFactoryTest()
        {
            _subject = new CommandFactory();
        }

        #region Facts
        [Fact]
        public void ParsingWrongFormatInputShouldReturnNull()
        {
            this.Given(x=>x.GivenWrongFormatInput())
                .When(x=>x.WhenParseCalled())
                .Then(x=>x.ThenItShouldBeNull())
                .BDDfy();
        }

        [Fact]
        public void ParsingPlaceCommandShouldSucceed()
        {
            this.Given(x=>x.GivenPlaceCommandInput())
                .When(x=>x.WhenParseCalled())
                .Then(x=>x.ThenItShouldBePlaceCommand())
                .BDDfy();
        }

        [Fact]
        public void ParsingIncorrectPlaceCommandShouldReturnNull()
        {
            this.Given(x=>x.GivenIncorrectPlaceCommandInput())
                .When(x=>x.WhenParseCalled())
                .Then(x=>x.ThenItShouldBeNull())
                .BDDfy();
        }

        [Fact]
        public void ParsingPlaceCommandIncorrectFacingShouldReturnNull()
        {
            this.Given(x=>x.GivenPlaceCommandIncorrectFacingInput())
                .When(x=>x.WhenParseCalled())
                .Then(x=>x.ThenItShouldBeNull())
                .BDDfy();
        }

        [Fact]
        public void ParsingMoveCommandShouldSucceed()
        {
            this.Given(x=>x.GivenMoveCommandInput())
                .When(x=>x.WhenParseCalled())
                .Then(x=>x.ThenItShouldBeMoveCommand())
                .BDDfy();
        }

        [Fact]
        public void ParsingLeftCommandShouldSucceed()
        {
            this.Given(x=>x.GivenLeftCommandInput())
                .When(x=>x.WhenParseCalled())
                .Then(x=>x.ThenItShouldBeLeftCommand())
                .BDDfy();
        }

        [Fact]
        public void ParsingRightCommandShouldSucceed()
        {
            this.Given(x=>x.GivenRightCommandInput())
                .When(x=>x.WhenParseCalled())
                .Then(x=>x.ThenItShouldBeRightCommand())
                .BDDfy();
        }

        [Fact]
        public void ParsingReportCommandShouldSucceed()
        {
            this.Given(x=>x.GivenReportCommandInput())
                .When(x=>x.WhenParseCalled())
                .Then(x=>x.ThenItShouldBeReportCommand())
                .BDDfy();
        }
        #endregion

        #region Givens
        private void GivenWrongFormatInput()
        {
            _testInput = "ROLLBACK";
        }
        private void GivenPlaceCommandInput()
        {
            _testInput = "PLACE 1,2,EAST";
        }

        private void GivenIncorrectPlaceCommandInput()
        {
            _testInput = "PLACE 1,2";
        }

        private void GivenPlaceCommandIncorrectFacingInput()
        {
            _testInput = "PLACE 1,2, SOUTHEASTER";
        }

        private void GivenMoveCommandInput()
        {
            _testInput = "MOVE";
        }

        private void GivenLeftCommandInput()
        {
            _testInput = "LEFT";
        }

        private void GivenRightCommandInput()
        {
            _testInput = "RIGHT";
        }

        private void GivenReportCommandInput()
        {
            _testInput = "REPORT";
        }

        #endregion

        #region Whens

        private void WhenParseCalled()
        {
            _testResult = _subject.Parse(_testInput);
        }
        #endregion

        #region Thens
        private void ThenItShouldBePlaceCommand()
        {
            Assert.Equal(Action.PLACE, _testResult.Action);
        }

        private void ThenItShouldBeMoveCommand()
        {
            Assert.Equal(Action.MOVE, _testResult.Action);
        }

        private void ThenItShouldBeLeftCommand()
        {
            Assert.Equal(Action.LEFT, _testResult.Action);
        }

        private void ThenItShouldBeRightCommand()
        {
            Assert.Equal(Action.RIGHT, _testResult.Action);
        }

        private void ThenItShouldBeReportCommand()
        {
            Assert.Equal(Action.REPORT, _testResult.Action);
        }

        private void ThenItShouldBeNull()
        {
            Assert.Null(_testResult);
        }
        #endregion
    }
}
