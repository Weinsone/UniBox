using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour { // ща по жопе получишь. ой извените
    public static Client LocalPlayer { get; private set; }
    public static bool multiplayerMode;

    void Start() {
        LocalPlayer = new Client(Server.clients.Count, "Player", Privileges.admin);
        if (multiplayerMode) {
            // траханье с сокетами (мммм дельфи) (ахахах чую можно будет определить мой код по var'ам)
        }
    }
}