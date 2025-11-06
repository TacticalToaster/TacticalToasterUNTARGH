using EFT;
using System.Collections.Generic;
using TacticalToasterUNTARGH.Components;

namespace TacticalToasterUNTARGH.Controllers
{
    public static class BotUntarManagerFactory
    {
        private static Dictionary<BotOwner, BotUntarManager> managers = new Dictionary<BotOwner, BotUntarManager>();

        public static BotUntarManager GetManager(this BotOwner botOwner)
        {
            if (managers.TryGetValue(botOwner, out var manager))
            {
                return manager;
            }
            return null;
        }

        public static BotUntarManager GetOrAddUntarManager(this BotOwner botOwner)
        {
            var manager = GetManager(botOwner);

            if (manager != null)
            {
                return manager;
            }

            manager = botOwner.GetPlayer.gameObject.GetOrAddComponent<BotUntarManager>();
            managers[botOwner] = manager;
            manager.Init(botOwner);

            return manager;
        }
    }
}
