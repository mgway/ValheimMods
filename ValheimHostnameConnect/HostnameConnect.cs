using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace ValheimHostnameConnect
{
	[BepInPlugin("ohmg.mods.hostnameconnect", "Hostname Connect", "1.0.0")]
	public class HostnameConnect : BaseUnityPlugin
	{
		private Harmony _harmony;
        public static ConfigEntry<string> lastServerAddress;

        private void Awake()
        {
            lastServerAddress = Config.Bind("General", "Last server address", "", "Last server IP/Hostname direct connected to");
            _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }

        private void OnDestroy()
        {
            _harmony?.UnpatchAll();
        }
    }
}
