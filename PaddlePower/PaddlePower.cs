using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace PaddlePower
{
	[BepInPlugin("ohmg.mods.paddlepower", "Paddle Power", "1.0.0")]
	public class PaddlePower : BaseUnityPlugin
	{
		private Harmony _harmony;
        public static ConfigEntry<bool> PluginEnabled;
        public static ConfigEntry<float> Coefficient;
        public static ConfigEntry<float> Ratio;


        private void Awake()
        {
            PluginEnabled = Config.Bind("General", "enabled", true, "Enable paddle power scaling");
            Coefficient = Config.Bind("General", "coefficient", 0.5f, "Geometric series function coefficient");
            Ratio = Config.Bind("General", "ratio", 0.75f, "Geometric series function common ratio");

            _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }

        private void OnDestroy()
        {
            _harmony?.UnpatchAll();
        }
    }
}
