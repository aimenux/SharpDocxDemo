using SharpDocx;
using System.Threading.Tasks;

namespace Lib.Generators
{
    public class DocumentGenerator : IDocumentGenerator
    {
        public Task GenerateAsync<T>(T model, string viewFilePath, string outputFilePath)
        {
            #if DEBUG
            Ide.Start(viewFilePath, outputFilePath, model);
            #else
            var document = DocumentFactory.Create(viewFilePath, model);
            document.Generate(outputFilePath);
            #endif
            return Task.CompletedTask;
        }
    }
}
