using System.Threading.Tasks;

namespace Lib.Generators
{
    public interface IDocumentGenerator
    {
        Task GenerateAsync<T>(T model, string viewFilePath, string outputFilePath);
    }
}
