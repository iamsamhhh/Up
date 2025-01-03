using System.Threading.Tasks;
using UnityEngine;
using Apple.GameKit;
using MyFramework;
using System.Linq;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System;
using Apple.GameKit.Leaderboards;

public class AppleGameCenter : MonoSingletonBaseAuto<AppleGameCenter>
{
    // byte[] Signature;
    // string TeamPlayerID;
    // byte[] Salt;
    // string PublicKeyUrl;
    // ulong Timestamp;
    async void Awake()
	{
		try
		{
			await UnityServices.InitializeAsync();
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
	}

    // Start is called before the first frame update
    async void Start()
    {
        if (EnvironmentConfig.environment.mode != EnvironmentMode.Developing)
            await Login();
    }

    public async Task Login()
    {
        if (!GKLocalPlayer.Local.IsAuthenticated)
        {
            var player = await GKLocalPlayer.Authenticate();
            Debug.Log($"GameKit Authentication: isAuthenticated => {player.IsAuthenticated}");

            // Grab the display name.
            var localPlayer = GKLocalPlayer.Local;
            Debug.Log($"Local Player: {localPlayer.DisplayName}");
            
            // Fetch the items.
            var fetchItemsResponse =  await GKLocalPlayer.Local.FetchItemsForIdentityVerificationSignature();

            // Signature = fetchItemsResponse.GetSignature();
            // TeamPlayerID = localPlayer.TeamPlayerId;
            // Debug.Log($"Team Player ID: {TeamPlayerID}");

            // Salt = fetchItemsResponse.GetSalt();
            // PublicKeyUrl = fetchItemsResponse.PublicKeyUrl;
            // Timestamp = fetchItemsResponse.Timestamp;

            // Debug.Log($"GameKit Authentication: signature => {Signature}");
            // Debug.Log($"GameKit Authentication: publickeyurl => {PublicKeyUrl}");
            // Debug.Log($"GameKit Authentication: salt => {Salt}");
            // Debug.Log($"GameKit Authentication: Timestamp => {Timestamp}");

            // await SignInWithAppleGameCenterAsync(Signature.ToString(), TeamPlayerID, PublicKeyUrl, Salt.ToString(), Timestamp);
        }
        else
        {
            Debug.Log("AppleGameCenter player already logged in.");
        }
    }

    // async Task SignInWithAppleGameCenterAsync(string signature, string teamPlayerId, string publicKeyURL, string salt, ulong timestamp)
    // {
    //     try
    //     {
    //         await AuthenticationService.Instance.SignInWithAppleGameCenterAsync(signature, teamPlayerId, publicKeyURL, salt, timestamp);
    //         Debug.Log("SignIn is successful.");
    //     }
    //     catch (AuthenticationException ex)
    //     {
    //         // Compare error code to AuthenticationErrorCodes
    //         // Notify the player with the proper error message
    //         Debug.LogException(ex);
    //     }
    //     catch (RequestFailedException ex)
    //     {
    //         // Compare error code to CommonErrorCodes
    //         // Notify the player with the proper error message
    //         Debug.LogException(ex);
    //     }
    // }

    public async void OpenAchievements(){
        // var achievements = await GKAchievementDescription.LoadAchievementDescriptions();

        // foreach (var a in achievements) 
        // {
        //     Debug.Log($"Achievement: {a.Identifier}");
        // }
        if (EnvironmentConfig.environment.mode != EnvironmentMode.Developing){
            var gameCenter = GKGameCenterViewController.Init(GKGameCenterViewController.GKGameCenterViewControllerState.Achievements);
            // await for user to dismiss...
            await gameCenter.Present();
        }
    }

    public async void OpenLeaderBoards(){
        // var achievements = await GKAchievementDescription.LoadAchievementDescriptions();

        // foreach (var a in achievements) 
        // {
        //     Debug.Log($"Achievement: {a.Identifier}");
        // }
        if (EnvironmentConfig.environment.mode != EnvironmentMode.Developing){
            var gameCenter = GKGameCenterViewController.Init(GKGameCenterViewController.GKGameCenterViewControllerState.Leaderboards);
            // await for user to dismiss...
            await gameCenter.Present();
        }
    }

    public async void ReportAchievement(GKAchievement achievement){
        if (EnvironmentConfig.environment.mode != EnvironmentMode.Developing)
            await GKAchievement.Report(achievement);
    }

    public async void SubmitNewScore(GKLeaderboard leaderboard, long score, long context){
        if (EnvironmentConfig.environment.mode != EnvironmentMode.Developing)
            await leaderboard.SubmitScore(score, context, GKLocalPlayer.Local);
    }

    public async Task<GKAchievement> GetAchievementAsync(string id){
        if (EnvironmentConfig.environment.mode == EnvironmentMode.Developing)
            return null;
        var achievements = await GKAchievement.LoadAchievements();

        // Only completed achievements are returned.
        var achievement = achievements.FirstOrDefault(a => a.Identifier == id);

        // If null, initialize it
        achievement ??= GKAchievement.Init(id);
        return achievement;
    }

    public async Task<GKLeaderboard> GetLeaderboardAsync(string id){
        if (EnvironmentConfig.environment.mode == EnvironmentMode.Developing)
            return null;
        var leaderboards = await GKLeaderboard.LoadLeaderboards(id);
        var leaderboard = leaderboards.FirstOrDefault();

        return leaderboard;
    }
}