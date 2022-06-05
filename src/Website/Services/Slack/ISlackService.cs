using System.Threading.Tasks;

namespace BrickAtHeart.Communities.Services.Slack
{
    public interface ISlackService
    {
        Task UpdateAppHome(string slackUserId);
    }
}