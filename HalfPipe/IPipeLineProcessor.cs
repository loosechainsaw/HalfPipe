using System.Threading.Tasks;

namespace HalfPipe
{
    public interface IPipeLineProcessor
    {
        Task Process<U>(U initial);
        Task<T> Compute<T, U>(U initial);
    }
}