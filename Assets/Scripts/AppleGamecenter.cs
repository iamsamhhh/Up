using System.Threading.Tasks;
using UnityEngine;
using Apple.GameKit;
using MyFramework;

public class AppleGameCenter : MonoSingletonBaseAuto<AppleGameCenter>
{
    byte[] Signature;
    string TeamPlayerID;
    byte[] Salt;
    string PublicKeyUrl;
    ulong Timestamp;

    // Start is called before the first frame update
    async void Start()
    {
        await Login();
    }

    public async Task Login()
    {
        if (!GKLocalPlayer.Local.IsAuthenticated)
        {
            // Perform the authentication.
            var player = await GKLocalPlayer.Authenticate();
            Debug.Log($"GameKit Authentication: player {player}");

            // Grab the display name.
            var localPlayer = GKLocalPlayer.Local;
            Debug.Log($"Local Player: {localPlayer.DisplayName}");

            // Fetch the items.
            var fetchItemsResponse =  await GKLocalPlayer.Local.FetchItemsForIdentityVerificationSignature();

            Signature = fetchItemsResponse.GetSignature();
            TeamPlayerID = localPlayer.TeamPlayerId;
            Debug.Log($"Team Player ID: {TeamPlayerID}");

            Salt = fetchItemsResponse.GetSalt();
            PublicKeyUrl = fetchItemsResponse.PublicKeyUrl;
            Timestamp = fetchItemsResponse.Timestamp;

            Debug.Log($"GameKit Authentication: signature => {Signature}");
            Debug.Log($"GameKit Authentication: publickeyurl => {PublicKeyUrl}");
            Debug.Log($"GameKit Authentication: salt => {Salt}");
            Debug.Log($"GameKit Authentication: Timestamp => {Timestamp}");
        }
        else
        {
            Debug.Log("AppleGameCenter player already logged in.");
        }
    }

    public async void OpenAchievements(){
        var achievements = await GKAchievementDescription.LoadAchievementDescriptions();

        foreach (var a in achievements) 
        {
            Debug.Log($"Achievement: {a.Identifier}");
        }
        var gameCenter = GKGameCenterViewController.Init(GKGameCenterViewController.GKGameCenterViewControllerState.Achievements);
        // await for user to dismiss...
        await gameCenter.Present();
    }
}