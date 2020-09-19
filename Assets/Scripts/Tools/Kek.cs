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
    void Start() {
        
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.L)) {
            Server.AddBot(new Bot(Server.Bots.Count, "Bot" + Server.Bots.Count, BotBehaviorList.Behaviors.follower, ControllerList.Controllers.assistant, 45, 50));
        }
    }

    public Transform target;
    public IEnumerator RunningAround(IBot bot) {
        while (true) {
            bot.Goto(target.position, false);
            yield return null;
        }
    }
}
