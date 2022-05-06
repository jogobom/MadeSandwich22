using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;
using System.Text;
using System;

namespace jogobom.MadeSandwich
{
    internal static class Slack
    {
        private static readonly HttpClient Client = new();

        public static async Task PostToChrisP(string message)
        {
            await Post(message, Environment.GetEnvironmentVariable("SLACK_CHRISP"));
        }

        public static async Task PostToFood(string message)
        {
            await Post(message, Environment.GetEnvironmentVariable("SLACK_FOOD"));
        }

        private static async Task Post(string message, string hook)
        {
            var values = new Dictionary<string, string>
            {
                {"text", message},
                {"username", "MadeSandwich"}
            };

            var content = new StringContent(JsonSerializer.Serialize(values), Encoding.UTF8, "application/json");
            await Client.PostAsync(hook, content);
        }
    }   }
