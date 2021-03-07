using System.Reflection;

using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;


namespace PaddlePower
{
	[BepInPlugin("ohmg.mods.paddlepower", "Paddle Power", "1.0.1")]
	public class PaddlePower : BaseUnityPlugin
	{
		private Harmony _harmony;
        public static ConfigEntry<bool> PluginEnabled;
        public static ConfigEntry<bool> DebugLoggingEnabled;

        public static ConfigEntry<string> ScalingMethod;
        public static ConfigEntry<string> CountingMethod;

        public static ConfigEntry<float> GeometricCoefficient;
        public static ConfigEntry<float> GeometricRatio;

        public static ConfigEntry<float> LinearBaseAmount;
        public static ConfigEntry<float> LinearAdditionalAmount;
        public static ConfigEntry<float> LinearMaximum;


        private void Awake()
        {
            PluginEnabled = Config.Bind("General", "enabled", true, "Enable paddle power scaling");
            DebugLoggingEnabled = Config.Bind("General", "debug_logging", false, "Print debug log messages to the console");

            ScalingMethod = Config.Bind("General", "scaling_method", "GEOMETRIC", "Method used for calculating additional player contributions. GEOMETRIC and LINEAR are valid options.");
            CountingMethod = Config.Bind("General", "counting_method", "ATTACHED", "What players to count as contributing to ship speed. ALL, ATTACHED, or SEATED are valid options.");

            GeometricCoefficient = Config.Bind("Geometric", "coefficient", 0.55f, "Geometric series function coefficient");
            GeometricRatio = Config.Bind("Geometric", "ratio", 0.875f, "Geometric series function common ratio");

            LinearBaseAmount = Config.Bind("Linear", "base_amount", 0.5f, "Paddle force contributed by the ship pilot");
            LinearAdditionalAmount = Config.Bind("Linear", "additional_amount", 0.25f, "Paddle force contributed by each additional player");
            LinearMaximum = Config.Bind("Linear", "maximum_bonus", 3f, "Maximum paddling force, regardless of player count");

            // Validate CountingMethod and ScalingMethod, overwrite with Attached and Geometric if a bad value is supplied
            if (!CountingMethod.Value.Equals("ALL", System.StringComparison.CurrentCultureIgnoreCase)
                && !CountingMethod.Value.Equals("ATTACHED", System.StringComparison.CurrentCultureIgnoreCase)
                && !CountingMethod.Value.Equals("SEATED", System.StringComparison.CurrentCultureIgnoreCase)) 
            {
                CountingMethod.Value = "ATTACHED";
                Debug.LogWarning("PaddlePower: Invalid counting_method configuration value, changing to ATTACHED");

            }

            if (!ScalingMethod.Value.Equals("GEOMETRIC", System.StringComparison.CurrentCultureIgnoreCase)
                && !ScalingMethod.Value.Equals("LINEAR", System.StringComparison.CurrentCultureIgnoreCase))
            {
                ScalingMethod.Value = "GEOMETRIC";
                Debug.LogWarning("PaddlePower: Invalid scaling_method configuration value, changing to GEOMETRIC");
            }

            _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }

        private void OnDestroy()
        {
            _harmony?.UnpatchAll();
        }
    }
}
