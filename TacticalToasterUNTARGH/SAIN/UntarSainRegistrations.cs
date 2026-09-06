using MoreBotsServer.Interop;
using MoreBotsServer.Models;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;

namespace UntarServer.SAIN;

[Injectable(TypePriority = OnLoadOrder.Preload + 2)]
public sealed class UntarSainRegistrations(SainInteropRegistration sainInterop) : IOnLoad
{
    private const int RiflemanId = 1170;
    private const int SquadLeaderId = 1171;
    private const int MarksmanId = 1172;
    private const int OfficerId = 1173;

    public Task OnLoadAsync(CancellationToken cancellationToken)
    {
        RegisterUntar();
        return Task.CompletedTask;
    }

    private void RegisterUntar()
    {
        sainInterop.RegisterBotType(CreateRegistration(
            wildSpawnType: RiflemanId,
            botDbKey: "followeruntar",
            name: "UNTAR Follower",
            description: "An UNTAR grunt.",
            difficultyModifier: 0.5f));

        sainInterop.RegisterBotType(CreateRegistration(
            wildSpawnType: SquadLeaderId,
            botDbKey: "bossuntarlead",
            name: "UNTAR Squad Leader",
            description: "An UNTAR squad leader.",
            difficultyModifier: 0.66f));

        sainInterop.RegisterBotType(CreateRegistration(
            wildSpawnType: MarksmanId,
            botDbKey: "followeruntarmarksman",
            name: "UNTAR Marksman",
            description: "An UNTAR marksman.",
            difficultyModifier: 0.5f));

        sainInterop.RegisterBotType(CreateRegistration(
            wildSpawnType: OfficerId,
            botDbKey: "bossuntarofficer",
            name: "UNTAR Officer",
            description: "An UNTAR officer.",
            difficultyModifier: 0.7f));
    }

    private static MoreBotsSainBotTypeRegistration CreateRegistration(
        int wildSpawnType,
        string botDbKey,
        string name,
        string description,
        float difficultyModifier)
    {
        return new MoreBotsSainBotTypeRegistration
        {
            WildSpawnType = wildSpawnType,
            BotDbKey = botDbKey,
            Name = name,
            Description = description,
            Section = "UNTAR",
            DifficultyModifier = difficultyModifier,

            BrainsToApply =
            [
                "PMC",
                "ExUsec",
            ],

            LayersToRemove =
            [
                "Request",
                "KnightFight",
                "PmcBear",
                "PmcUsec",
                "StationaryWS",
                "ExURequest",
                "Utility peace",
            ],
        };
    }
}