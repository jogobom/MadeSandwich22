using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using FSandwich;
using System.IO;
using Microsoft.FSharp.Core;

namespace jogobom.MadeSandwich;

public class SandwichTime
{
    [FunctionName("SandwichTime")]
    public async Task RunAsync([TimerTrigger("0 30 9 * * 1-5")]TimerInfo myTimer, ILogger log, ExecutionContext context)
    {
        log.LogInformation($"SandwichTime trigger function executed at: {DateTime.Now}");

        var seed = Environment.TickCount;
        var rand = new Random(seed);

        var reader = FSharpFunc<string,string>.FromConverter((string p)
            => File.ReadAllText(Path.Combine(context.FunctionAppDirectory, p)));

        var sandwich = Sandwich.make_sandwich(rand, reader);
        log.LogInformation($"Sandwich from seed {seed}: {sandwich.ToString()}");

        var description = Sandwich.describe(sandwich, rand, reader);
        
        var price = Sandwich.price(sandwich);
        log.LogInformation($"Calculated price to be {price} pence");

        var message = MessageBuilder.Build(description, price);

        log.LogInformation(message);
        await Slack.PostToFood(message);
    }
}
