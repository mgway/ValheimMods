using System;
using HarmonyLib;
using System.Net;

namespace ValheimHostnameConnect
{
    [HarmonyPatch(typeof(ZSteamMatchmaking), "QueueServerJoin")]
    public static class ZSteamMatchmakingPatch
    {
        private static void Prefix(ref string addr)
        {
            string[] strArray = addr.Split(':');

            try
            {
                IPAddress.Parse(strArray[0]);

                // Save IP for later
                HostnameConnect.lastServerAddress.Value = addr;
            } catch (FormatException)
            {
                // May be a hostname. Sync call
                var ipEntry = Dns.GetHostByName(strArray[0]);
                if (ipEntry.AddressList.Length > 0)
                {
                    // Save hostname for later
                    HostnameConnect.lastServerAddress.Value = addr;

                    addr = ipEntry.AddressList[0] + ":" + strArray[1];
                }
            }
        }
    }
}
