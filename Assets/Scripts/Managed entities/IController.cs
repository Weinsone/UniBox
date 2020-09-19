using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    float Speed { get; set; }
    Vector3 EyeLevel { get; set; }
    void ApplySettings(EntitySettings settings);
    void SetAnimation(string animationName);
    void Goto(Vector3 position, bool immediately);
    void Look(Vector3 direction);
    void Jump();
}
