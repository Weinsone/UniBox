using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualMachine : MonoBehaviour
{
    public List<IProgram> installedPrograms;
    
    public IProgram ActiveProgram { get; set; }
    public Canvas computerCanvas;

    void Start() {
        installedPrograms = new List<IProgram>();
        computerCanvas = transform.GetComponentInChildren<Canvas>();

        UpdateProgramList();
    }

    public void RunProgram(string programName) {
        foreach (var program in installedPrograms) {
            if (program.Name == programName) {
                program.Main();
            }
        }
    }

    public void UpdateProgramList() {
        PluginEngine.RefreshLibraries<IProgram>();
        foreach (IProgram program in PluginEngine.PluginsOrPrograms) {
            installedPrograms.Add(program);
            UpdateStartMenu(program.Name);
        }
    }

    private void UpdateStartMenu(string programName) {
        // Transform startMenuContent = transform.Find("Computer screen");
        Transform startMenuContent = transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0);
        Debug.Log(startMenuContent.name);
        GameObject startMenuItem = Instantiate((GameObject)Resources.Load("Virtual machine/Start Menu Item"));
        startMenuItem.transform.GetChild(0).GetComponent<Text>().text = programName;
        Form.AdaptiveAndShow(computerCanvas, startMenuItem.transform, startMenuContent, 0);
        // startMenuItem.transform.SetParent(startMenuContent);
    }

    public void Explode(int power) {
        
    }
}
