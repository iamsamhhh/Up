using UnityEngine;
using System.Threading.Tasks;
using System.Collections;
using Apple.GameKit;


namespace MyFramework.SocialPlatforms
{
    public class SocialPlatformManager : MonoSingletonBaseAuto<SocialPlatformManager>{
        ECurrentSocialPlatform currentSocialPlatform;
    }

    public interface IAchievement
    {
        public Task Report();
    }

    public class Achievement{
        IAchievement achievement;

        public async void Report(){
            if (EnvironmentConfig.environment.mode != EnvironmentMode.Developing)
                await achievement.Report();
        }
    }
    public class LeaderBoard {

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
