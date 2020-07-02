using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Server
{
    public static List<Player> Clients { get; private set; } = new List<Player>();

    public static void AddClient(Player client) {
        Clients.Add(client);
    }

    public static void LocalPlayerUpdatePosition(Vector3 position) {

    }

    public static void AllPlayerUpdatePosition(Vector3 position) {

    }
}