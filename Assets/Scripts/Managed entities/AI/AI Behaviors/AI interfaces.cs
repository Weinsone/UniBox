using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBotBase
{
    IBotBehavior Behavior { get; set; }
    int Id { get; set; }
    string Name { get; set; }
    int Speed { get; set; }
    int Remembrance { get; set; } // время с секундах, где он будет помнить что с кем-то сражался (не то что сржался, скорее видел). По истечению этого значения пойдет по своим делам (0 - злопамятный и будет вечно помнить)
    // поле с анимацией

    void MakeSound(); // пердёж
}

public interface IBotBehavior {
    void Checkup(); // Момент простоя (ну там ходить кругами, либо просто тупить в стену с анимацией)

    void OnTargetFound();
    void OnTargetLost();
}
