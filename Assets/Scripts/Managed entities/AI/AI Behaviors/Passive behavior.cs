using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveBehavior : IBotBehavior
{
    public void Checkup() {
        Debug.Log("Надеюсь меня никто не видит");
    }

    public void OnTargetFound() {
        Debug.Log("Эээ, баклан");
    }

    public void OnTargetLost() {
        Debug.Log("Фух");
    }
}
