using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Server
{
    public static List<GameObject> clients = new List<GameObject>();
    public static void AddClient(GameObject client) => clients.Add(client);

    
}