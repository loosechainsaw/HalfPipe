using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace HalfPipe.UnitTests
{
    [TestFixture]
    public class HalfPipeTests
    {
        [Test]
        public async Task ComposingASimplePipelineShouldReturnTheCorrectResult()
        {
            var pipeLine = new PipeLineProcessor();
            pipeLine.Append(new Number1PipeLine());
            pipeLine.Append(new Number2PipeLine());

            var result = await pipeLine.Compute<int, Unit>(Unit.Instance);
            Assert.AreEqual(7, result);
        }

        [Test]
        public async Task ComposingASimplePipelineShouldReturnTheCorrectResultIfLongRunningOperation()
        {
            var pipeLine = new PipeLineProcessor();
            pipeLine.Append(new Number1LongRunningPipeLine());
            pipeLine.Append(new Number2LongRunningPipeLine());

            var result = await pipeLine.Compute<int, Unit>(Unit.Instance);
            Assert.AreEqual(11, result);
        }

    }

    public class Number1LongRunningPipeLine : IPipeLine<int, Unit>
    {
        public Task<int> Process(Unit input)
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(500);
                return 5;
            });
        }
    }

    public class Number2LongRunningPipeLine : IPipeLine<int, int>
    {
        public Task<int> Process(int input)
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(500);
                return 6 + input;
            });
        }
    }

    public class Number1PipeLine : IPipeLine<int, Unit>
    {
        public Task<int> Process(Unit input)
        {
            return Task.FromResult(3);
        }
    }

    public class Number2PipeLine : IPipeLine<int, int>
    {
        public Task<int> Process(int input)
        {
            return Task.FromResult(4 + input);
        }
    }


}
