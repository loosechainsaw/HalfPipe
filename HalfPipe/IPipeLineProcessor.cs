using System.Threading.Tasks;

namespace HalfPipe
{
    public interface IPipeLineProcessor
    {
        Task Process();
        Task<T> Compute<T>();
    }
}