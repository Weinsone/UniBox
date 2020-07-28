using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveBehavior : IBotBehavior
{
    public IBot Root { get; set; }
    public AIManager Ai { get; set; }

    public PassiveBehavior(IBot root) {
        Root = root;
    }

    public void Checkup() {
        Debug.Log("Надеюсь меня никто не видит");
    }

    public void DailyRoutine() {

    }

    private void OnTargetFound() {
        Debug.Log("Ай мляяяяя");
    }

    private void OnTargetLost() {
        Debug.Log("Фух");
    }
}
