using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Me.SendMail;
using Microsoft.Graph.Models;

namespace MsGraphClient;

public class MsGraphClient
{
    private readonly GraphServiceClient _client;

    public MsGraphClient(string clientId, Action<string, string>? deviceCodeCallback = null, string cacheName = "MsGraphClient")
    {
        var options = new DeviceCodeCredentialOptions
        {
            ClientId = clientId,
            TenantId = "consumers",
            TokenCachePersistenceOptions = new TokenCachePersistenceOptions { Name = cacheName },
            DeviceCodeCallback = (code, _) =>
            {
                if (deviceCodeCallback is not null)
                    deviceCodeCallback(code.VerificationUri.ToString(), code.UserCode);
                else
                {
                    Console.Error.WriteLine($"=== GRAPH AUTH ===");
                    Console.Error.WriteLine($"Otvori: {code.VerificationUri}");
                    Console.Error.WriteLine($"Unesi kod: {code.UserCode}");
                    Console.Error.WriteLine($"==================");
                }
                return Task.CompletedTask;
            }
        };

        _client = new GraphServiceClient(new DeviceCodeCredential(options),
            ["https://graph.microsoft.com/Mail.Send", "https://graph.microsoft.com/User.Read"]);
    }

    public async Task<GraphResult> SendEmailAsync(
        string toEmail,
        string toName,
        string subject,
        string body,
        bool isHtml = false,
        CancellationToken ct = default)
    {
        try
        {
            var message = new Message
            {
                Subject = subject,
                Body = new ItemBody
                {
                    ContentType = isHtml ? BodyType.Html : BodyType.Text,
                    Content = body
                },
                ToRecipients =
                [
                    new Recipient { EmailAddress = new EmailAddress { Address = toEmail, Name = toName } }
                ]
            };

            await _client.Me.SendMail.PostAsync(new SendMailPostRequestBody
            {
                Message = message,
                SaveToSentItems = false
            }, cancellationToken: ct);

            return GraphResult.Ok();
        }
        catch (Exception ex)
        {
            return GraphResult.Fail(ex.ToString());
        }
    }

}
