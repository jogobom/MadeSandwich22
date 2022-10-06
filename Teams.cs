// Copyright © 2022 Waters Corporation. All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MessageCards;

namespace jogobom.MadeSandwich;

internal static class Teams
{
    private static readonly HttpClient Client = new();

    public static async Task Post(string message)
    {
        var webhookUrl = Environment.GetEnvironmentVariable("TEAMS_WEBHOOK");
        if (!string.IsNullOrWhiteSpace(webhookUrl))
        {
            await Post(message, webhookUrl);
        }
    }

    private static async Task Post(string message, string hook)
    {
        var messageCard = new MessageCard("Random sandwich");
        messageCard.Text = message;

        var content = new StringContent(messageCard.ToJson(), Encoding.UTF8, "application/json");
        await Client.PostAsync(hook, content);
    }
}