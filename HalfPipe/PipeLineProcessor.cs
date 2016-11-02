using System.Collections.Generic;
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

        public async Task<T> Compute<T>()
        {
            object result = Unit.Instance;

            foreach (dynamic pipe in _pipeline)
            {
                result = await pipe.Process(result);
            }

            return (T) result;
        }

        public async Task Process()
        {
            object result = Unit.Instance;

            foreach (dynamic pipe in _pipeline)
            {
                result = await pipe.Process(result);
            }

        }

        private readonly List<IPipeLine> _pipeline;
    }
}