using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualMachine : MonoBehaviour
{
    private List<IProgram> installedPrograms;
    
    public IProgram ActiveProgram { get; set; }
    public Canvas computerCanvas;

    void Start() {
        // TODO: загрузка списка программ с сейв файла
        computerCanvas = transform.GetComponentInChildren<Canvas>();
    }

    public void Explode(int power) {
        
    }
}
