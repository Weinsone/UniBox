using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/*
    Ядерный полигон испытаний
*/
public class Kek : MonoBehaviour
{
    public static GameObject helper, pathHelper;
    public IBot bot;

    void Start() {
        helper = GameObject.Find("Helper");
        pathHelper = GameObject.Find("Path helper");
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.L)) {
            bot = Server.AddBot(new Bot(Server.Bots.Count, "Bot" + Server.Bots.Count, BotBehaviorList.Behaviors.follower, ControllerList.Controllers.assistant, new Vector3(0, 0.5f, 0)));
        }
    }

    // public Transform target;
    // public IEnumerator RunningAround(IBot bot) {
    //     while (true) {
    //         bot.Goto(target.position, false);
    //         yield return null;
    //     }
    // }
}
