using Life;
using System;
using UnityEngine;
using Life.Naos;
using Mirror;
using Life.Network;
using Life.Network.Systems;
using Life.UI;
using Life.API;
using Life.Behaviours;
using Life.Config;
using Life.ServerCreationSystem;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Discord581
{
    public class Discord581 : Plugin
    {
        private Config config;

        public Discord581(IGameAPI api) : base(api)
        {

        }

        public override void OnPluginInit()
        {
            base.OnPluginInit();

            UnityEngine.Debug.Log("Discord581 Initialisé avec succès.");

            string directoryPath = pluginsPath + "/Discord581";

            string configFilePath = directoryPath + "/config.json";

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (!File.Exists(configFilePath))
            {
                Config defaultConfig = new Config { DiscordLink = "Votre lien discord" };

                string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(defaultConfig, Newtonsoft.Json.Formatting.Indented);

                File.WriteAllText(configFilePath, jsonContent);
            }

            config = Newtonsoft.Json.JsonConvert.DeserializeObject<Config>(File.ReadAllText(configFilePath));

            new SChatCommand("/discord", "Lien Discord", "/discord", (Action<Player, string[]>)((player, arg) =>
            {
                player.SendText($"<color=#e8472a>[DISCORD]</color> Le lien du discord est : {config.DiscordLink}");

                player.Notify("SUCCES", "Le lien discord vous a été envoyer avec succès.", Life.NotificationManager.Type.Success);

            })).Register();
        }

        public class Config
        {
            public string DiscordLink { get; set; }
        }
    }
}
