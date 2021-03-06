using HarmonyLib;

namespace HostnameConnect
{
    [HarmonyPatch(typeof(FejdStartup), "OnJoinIPOpen")]
    public static class FejdStartupPatch
    {
        private static void Postfix(FejdStartup __instance)
        {
            if (!string.IsNullOrEmpty(HostnameConnect.serverAddress.Value))
            {
                __instance.m_joinIPAddress.text = HostnameConnect.serverAddress.Value + ":" + HostnameConnect.serverPort.Value;
            }
        }
    }
}
