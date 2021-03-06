using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace AutoSneak
{
	[BepInPlugin("ohmg.mods.autosneak", "Auto Sneak", "1.0.0")]
	public class AutoSneak : BaseUnityPlugin
	{
		private Harmony _harmony;
        public static ConfigEntry<bool> PluginEnabled;

        private void Awake()
        {
            PluginEnabled = Config.Bind("General", "enabled", true, "Enable autorun when sneaking");
            _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }

        private void OnDestroy()
        {
            _harmony?.UnpatchAll();
        }
    }
}
