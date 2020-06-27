using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client
{
    private GameObject playerModel;
    public int id;
    public string nickname;
    public Privileges privileges;

    public Client(int id, string nickname, Privileges privileges) {
        this.id = id;
        this.nickname = nickname;
        this.privileges = privileges;
        playerModel = MonoBehaviour.Instantiate((GameObject)Resources.Load("Controllers/Player"));
    }

    public GameObject GetController() {
        return playerModel;
    }
    public void SetController(string controllerName) {
        MonoBehaviour.Destroy(playerModel);
        playerModel = MonoBehaviour.Instantiate((GameObject)Resources.Load("Controllers/" + controllerName));
    }
}