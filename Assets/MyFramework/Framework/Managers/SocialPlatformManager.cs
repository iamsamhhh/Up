using UnityEngine;
using System.Threading.Tasks;
using System.Collections;
using Apple.GameKit;
using Apple.GameKit.Leaderboards;


namespace MyFramework.SocialPlatforms
{
    public class SocialPlatformManager : MonoSingletonBaseAuto<SocialPlatformManager>{
        ECurrentSocialPlatform currentSocialPlatform;
    }

    public interface IAchievement
    {
        public Task Report();
    }

    public interface ILeaderboard{
        public Task SubmitScore(long score, long context);
    }

    public class Achievement{
        IAchievement achievement;

        public async void Report(){
            if (EnvironmentConfig.environment.mode != EnvironmentMode.Developing)
                await achievement.Report();
        }
    }
    public class GameCenterLeaderBoard : ILeaderboard {
        GKLeaderboard leaderboard;

        public async Task SubmitScore(long score, long context)
        {
            var player = GKLocalPlayer.Local;
            await leaderboard.SubmitScore(score, context, player);
        }
    }


    public class GameCenterAchievement : IAchievement {
        GKAchievement achievement;
        public async Task Report(){
            await GKAchievement.Report(achievement);
        }
    }
    public enum ECurrentSocialPlatform {
        GameCenter,
        Steam,
        Unity
    }
}
