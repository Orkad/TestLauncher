using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace TestLauncher
{
    public class Util
    {
        /// <summary>
        ///     Recherche une application installée sur la machine courante
        /// </summary>
        /// <param name="applicationName"></param>
        /// <returns></returns>
        public static string GetApplicationPath(string applicationName)
        {
            string displayName;
            List<string> debugList = new List<string>();

            // CurrentUser
            var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string;
                if (applicationName.Equals(displayName, StringComparison.OrdinalIgnoreCase))
                {
                    return subkey.GetValue("InstallLocation") as string;
                }
                debugList.Add(displayName);
            }

            // LocalMachine_32
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string;
                if (applicationName.Equals(displayName, StringComparison.OrdinalIgnoreCase))
                {
                    return subkey.GetValue("InstallLocation") as string;
                }
                debugList.Add(displayName);
            }

            // LocalMachine_64
            key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string;
                if (applicationName.Equals(displayName, StringComparison.OrdinalIgnoreCase))
                {
                    return subkey.GetValue("InstallLocation") as string;
                }
                debugList.Add(displayName);
            }

            debugList.Sort();

            // Null si non trouvé
            return null;
        }

        /// <summary>
        ///    Récupère le chemin complet vers le dossier "Steam"
        /// </summary>
        /// <returns></returns>
        public static string GetSteamPath()
        {
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.OpenSubKey(@"Software\Valve\Steam");
            return regKey?.GetValue("SteamPath").ToString();
        }

        /// <summary>
        ///     Récupère le chemin complet vers le dossier "Arma 3"
        /// </summary>
        /// <param name="steamPath">chemin vers le dossier "Steam"</param>
        /// <returns></returns>
        public static string GetArma3Path(string steamPath)
        {
            string arma3Path = steamPath + "/steamapps/common/Arma 3";
            return !Directory.Exists(arma3Path) ? null : arma3Path;
        }

        /// <summary>
        ///     Récupère le chemin complet vers le dossier "TeamSpeak"
        /// </summary>
        /// <returns></returns>
        public static string GetTeamSpeakPath()
        {
            var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
            string str = key.GetValue(null) as string;
            return str;
        }
    }
}
