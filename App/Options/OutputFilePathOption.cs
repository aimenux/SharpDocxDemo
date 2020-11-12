using McMaster.Extensions.CommandLineUtils;

namespace App.Options
{
    public class OutputFilePathOption : CommandOption
    {
        public OutputFilePathOption() : base("-o|--out", CommandOptionType.SingleValue)
        {
            Description = "Name of output report file";
        }
    }
}
