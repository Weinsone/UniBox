using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public GameObject PlayerModel { get; private set; }
    public Controller Controller { get; private set; }
    public int id;
    public string nickname;
    public Privileges privileges;

    public Player(int id, string nickname, Privileges privileges) {
        this.id = id;
        this.nickname = nickname;
        this.privileges = privileges;
        PlayerModel = MonoBehaviour.Instantiate((GameObject)Resources.Load("Controllers/Player"));
        Controller = PlayerModel.GetComponent<Controller>();
    }

    public GameObject GetController() {
        return PlayerModel;
    }
    public void SetController(string controllerName) {
        MonoBehaviour.Destroy(PlayerModel);
        PlayerModel = MonoBehaviour.Instantiate((GameObject)Resources.Load("Controllers/" + controllerName));
        Controller = PlayerModel.GetComponent<Controller>();
    }
}