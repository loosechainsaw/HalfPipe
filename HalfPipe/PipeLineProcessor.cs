using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HalfPipe
{
    public class PipeLineProcessor : IPipeLineProcessor
    {

        public PipeLineProcessor()
        {
            _pipeline = new List<IPipeLine>();
        }

        public void Append<T, U>(IPipeLine<T, U> pipe)
        {
            _pipeline.Add(pipe);
        }

        public async Task<T> Compute<T, U>(U initial)
        {
            var pipeLine = _pipeline.First();
            object result = await pipeLine.InvokeWith(initial);

            foreach (var pipe in _pipeline.Skip(1))
            {
                result = await pipe.InvokeWith(result);
            }

            return (T)result;
        }

        public async Task Process<U>(U initial)
        {
            var pipeLine = _pipeline.First();
            object result = await pipeLine.InvokeWith(initial);

            foreach (var pipe in _pipeline.Skip(1))
            {
                result = await pipe.InvokeWith(result);
            }

        }

        private readonly List<IPipeLine> _pipeline;
    }

    internal static class IPipeLineInvokeExtensions
    {
        public static dynamic InvokeWith(this IPipeLine pipe, object value)
        {
            return pipe.GetType().GetMethod("Process").Invoke(pipe, new[] { value });
        }

    }
}