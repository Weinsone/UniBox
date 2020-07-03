using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ManagedEntity
{
    public int id;
    public string nickname;
    public Privileges privileges;

    public Player(int id, string nickname, Privileges privileges) : base("Player") {
        this.id = id;
        this.nickname = nickname;
        this.privileges = privileges;
    }
}