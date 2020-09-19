using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Server
{
    public static bool isHost = true;
    public static List<Player> Clients { get; private set; } = new List<Player>();
    public static List<IBot> Bots { get; private set; } = new List<IBot>();
    private static List<Transform> targets = new List<Transform>();
    public static List<Transform> Targets {
        get {
            if (isHost) {
                return targets;
            } else {
                // магия с запросом данных из сервера
                return null;
            }
        }
    }

    public static void AddClient(Player client) {
        Clients.Add(client);
        targets.Add(client.EntityGameObject.transform);
    }

    public static void AddBot(IBot bot) {
        Bots.Add(bot);
        targets.Add(bot.EntityGameObject.transform);
    }

    public static void RemoveClient(Player client) {
        Clients.Remove(client);
        targets.Remove(client.EntityGameObject.transform);
    }

    public static void RemoveBot(IBot bot) {
        Bots.Remove(bot);
        targets.Remove(bot.EntityGameObject.transform);
    }

    public static void CreateConnection() {

    }

    public static void LocalPlayerUpdatePosition() {

    }

    public static void AllPlayerUpdatePosition() { // Отправка всем клиентам координат игроков

    }
}