using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveBehavior : IBotBehavior
{
    public void Checkup() {
        Debug.Log("Да где же этот артефакт едрить его в корень");
    }

    public void OnTargetFound() {
        Debug.Log("Ебать, вот ты где");
    }

    public void OnTargetLost() {
        Debug.Log("Опять артефакты искать");
    }
}
