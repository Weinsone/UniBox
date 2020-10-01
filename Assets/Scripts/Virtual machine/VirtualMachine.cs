using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualMachine : MonoBehaviour
{
    public List<IProgram> installedPrograms;

    public IProgram ActiveProgram { get; set; }
    public Canvas computerCanvas;

    private Transform startMenuViewport;

    void Start() {
        installedPrograms = new List<IProgram>();
        computerCanvas = transform.GetComponentInChildren<Canvas>();

        startMenuViewport = transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0);

        RefreshProgramList();
    }

    public void Explode(int power) {
        
    }

    public void RunProgram(string programName) {
        foreach (var program in installedPrograms) {
            if (program.Name == programName) {
                program.Main();
            }
        }
    }

    public void AddProgram(string programName) {
        PluginEngine.RefreshLibraries<IProgram>();
        foreach (IProgram program in PluginEngine.PluginsAndPrograms) {
            if (program.Name == programName) {
                installedPrograms.Add(program);
                AddItemToStartMenu(programName);
                
            }
        }
    }

    public void DeleteProgram(string programName) {

    }

    public static void GetCompiledProgram(string programName) {
        GameLevel.LocalPlayer.usingComputer.AddProgram(programName);
    }

    public void RefreshProgramList() {
        ClearStartMenu();
        PluginEngine.RefreshLibraries<IProgram>();
        foreach (IProgram program in PluginEngine.PluginsAndPrograms) {
            installedPrograms.Add(program);
            AddItemToStartMenu(program.Name);
        }
    }

    private void AddItemToStartMenu(string programName) {
        GameObject startMenuItem = Instantiate((GameObject)Resources.Load("Virtual machine/Start Menu Item"));
        startMenuItem.transform.GetChild(0).GetComponent<Text>().text = programName;
        Form.AdaptiveAndShow(computerCanvas, startMenuItem.transform, startMenuViewport, 1);
    }

    public void ClearStartMenu() {
        Transform startMenuContent = startMenuViewport.GetChild(0);
        for (int i = 0; i < startMenuContent.childCount; i++) {
            Debug.Log("Удаление " + startMenuContent.GetChild(i).GetChild(0).GetComponent<Text>().text);
            Destroy(startMenuContent.GetChild(i).gameObject);
        }
    }
}
