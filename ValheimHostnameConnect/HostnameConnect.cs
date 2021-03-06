using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace HostnameConnect
{
	[BepInPlugin("ohmg.mods.hostnameconnect", "Hostname Connect", "1.0.1")]
	public class HostnameConnect : BaseUnityPlugin
	{
		private Harmony _harmony;
        public static ConfigEntry<string> lastServerAddress;
        public static ConfigEntry<string> serverAddress;
        public static ConfigEntry<string> serverPort;
        public static ConfigEntry<bool> savePassword;
        public static ConfigEntry<string> serverPassword;

        private void Awake()
        {
            lastServerAddress = Config.Bind("General", "Last server address", "", "OLD. Use address and port config properties");
            serverAddress = Config.Bind("General", "address", "", "IP or hostname");
            serverPort = Config.Bind("General", "port", "2456", "Port");
            savePassword = Config.Bind("General", "save_password", true, "Save last used server password in this config file");
            serverPassword = Config.Bind("General", "password", "", "Server password");

            if (!string.IsNullOrEmpty(lastServerAddress.Value))
            {
                // Migrate from old config key
                string[] strArray = lastServerAddress.Value.Split(':');
                if (strArray.Length == 2)
                {
                    serverAddress.Value = strArray[0];
                    serverPort.Value = strArray[1];

                    lastServerAddress.Value = "";
                }
            }

            _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }

        private void OnDestroy()
        {
            _harmony?.UnpatchAll();
        }
    }
}
