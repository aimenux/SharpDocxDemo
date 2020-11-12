using App.Options;
using Lib.Builders;
using Lib.Generators;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.Commands
{
    public class DocumentGeneratorCommand : CommandLineApplication, ICommand
    {
        private const string ViewFilePath = @".\Views\Company.cs.docx";

        private readonly OutputFilePathOption _outputFilePathOption;
        private readonly IModelBuilder _builder;
        private readonly IDocumentGenerator _generator;
        private readonly ILogger _logger;

        public DocumentGeneratorCommand(
            OutputFilePathOption outputFilePathOption,
            IModelBuilder builder,
            IDocumentGenerator generator,
            ILogger logger)
        {
            Name = "DocxGen";
            Description = "Generate docs based on templates";
            Options.Add(outputFilePathOption);
            OnExecuteAsync(ExecuteAsync);

            _outputFilePathOption = outputFilePathOption;
            _builder = builder;
            _generator = generator;
            _logger = logger;
        }

        public async Task<int> ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                var model = _builder.BuildCompany();
                var outputFilePath = _outputFilePathOption.Value();
                await _generator.GenerateAsync(model, ViewFilePath, outputFilePath);
                _logger.LogInformation("Output file {outputFilePath} was generated successfully.", outputFilePath);
                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error has occured: {ex}", ex);
                return -1;
            }
        }
    }
}
