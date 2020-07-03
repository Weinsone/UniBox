using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emeaya : ManagedEntity, IBotBase
{
    public IBotBehavior Behavior { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public int Speed { get; set; }
    public int Remembrance { get; set; }

    public Emeaya(int id, string name, BotBehaviorList.Behaviors behavior) : base("Player") {
        Id = id;
        Name = name;
        Behavior = BotBehaviorList.Assign(behavior);
    }

    public void MakeSound() {
        
    }
}
