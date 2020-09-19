using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ManagedEntity
{
    public Privileges privileges;
    public bool isUseComputer;
    public VirtualMachine usingComputer; // Компьютер, который использует игрок. Необходим для вывода формы, т.к в ее конструкторе не указывается инфа о компе, куда она должна быть выведена

    public Player(int id, string nickname, Privileges privileges, ControllerList.Controllers controllerName) : base(ControllerList.Assign(controllerName), ControllerList.Types.player) {
        base.Id = id;
        base.Name = nickname;
        this.privileges = privileges;
    }
}