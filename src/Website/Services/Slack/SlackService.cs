using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BrickAtHeart.Communities.Services.Slack
{
    public class SlackService : ISlackService
    {
        public SlackService( HttpClient httpClient,
                             ILogger<SlackService> logger )
        {
            this.httpClient = httpClient;
            this.logger = logger;
        }

        public async Task LookupUserByEmail(string email)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"/users.lookupByEmail?email={email}");
        }

        public async Task UpdateAppHome(string slackUserId)
        {
            var message = new
            {
                user_id = slackUserId,
                view = new
                {
                    type = "home",
                    blocks = new List<object>
                    {
                        new
                        {
                            type = "section",
                            text = new
                            {
                                type = "mrkdwn",
                                text = "*KLUG* Summary"
                            }
                        },
                        new
                        {
                            type = "divider"
                        }
                    }
                }
            };

            StringContent content = new StringContent(JsonSerializer.Serialize(message), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync("views.publish", content);
        }

        private readonly HttpClient httpClient;
        private readonly ILogger<SlackService> logger;
    }
}