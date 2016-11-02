using System.Threading.Tasks;

namespace HalfPipe
{

    public interface IPipeLine
    {
    }

    public interface IPipeLine<T, U> : IPipeLine
    {
        Task<T> Process(U input);
    }
}
