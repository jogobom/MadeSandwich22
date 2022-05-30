using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace jogobom.MadeSandwich;

public class SandwichTime
{
    [FunctionName("SandwichTime")]
    public async Task RunAsync([TimerTrigger("0 30 9 * * 1-5")]TimerInfo myTimer, ILogger log)
    {
        log.LogInformation($"SandwichTime trigger function executed at: {DateTime.Now}");

        var generator = new SandwichGenerator();

        var sandwich = generator.Generate();

        var message = MessageBuilder.Build(sandwich);
        log.LogInformation(message);

        await Slack.PostToFood(message);
    }
}
