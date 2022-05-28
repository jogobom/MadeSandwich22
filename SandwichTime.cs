using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using FSandwich;

namespace jogobom.MadeSandwich;

public class SandwichTime
{
    [FunctionName("SandwichTime")]
    public async Task RunAsync([TimerTrigger("0 30 9 * * 1-5")]TimerInfo myTimer, ILogger log)
    {
        log.LogInformation($"SandwichTime trigger function executed at: {DateTime.Now}");

        var rand = new Random();

        var sandwich = Sandwich.make_sandwich(rand);

        var description = Sandwich.describe(sandwich, rand);
        var price = Sandwich.price(sandwich);

        var message = MessageBuilder.Build(description, price);

        log.LogInformation(message);
        await Slack.PostToFood(message);
    }
}
