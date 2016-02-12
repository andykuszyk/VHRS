using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHRS.ViewModel;

namespace VHRS.Tests.ViewModel
{
    [TestFixture]
    public class MainViewModelTests
    {
        [Test]
        public void RunRace_RunnerShouldWinWithin2PointsOfProbability()
        {
            var viewModel = new MainViewModel();
            viewModel.Runners.Clear();
            for (Int32 i = 0; i < 4; i++) viewModel.AddRunner();
            viewModel.Runners[0].Name = "Runner One";
            viewModel.Runners[0].Odds = "1/2";
            viewModel.Runners[1].Name = "Runner Two";
            viewModel.Runners[1].Odds = "2/1";
            viewModel.Runners[2].Name = "Runner Three";
            viewModel.Runners[2].Odds = "3/1";
            viewModel.Runners[3].Name = "Runner Four";
            viewModel.Runners[3].Odds = "8/1";

            Int64 runnerOneWinCount = 0;
            for(Int32 i = 0; i < 1000000; i++)
            {
                var winner = viewModel.RunRace();
                if (winner == viewModel.Runners[0]) runnerOneWinCount++;
            }

            runnerOneWinCount.Should().BeGreaterOrEqualTo(460000);
            runnerOneWinCount.Should().BeLessOrEqualTo(500000);
        }
    }
}
