using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameModeList
{
    sandbox,
    walkthrough
}

public static class GameMode
{
    public static Form test;
    public static GameModeList currentGameMode;

    public static Form[] CreatePlayerMenu() {
        Form[] menuForms;
        switch (currentGameMode) {
            case GameModeList.sandbox:
                menuForms = new Form[2];

                // Загрузка из XML всех данных для окна (интерфейса)
                // foreach (var menuWindow in menuWindows) {
                    
                // }

                // Временный костыль
                for (int i = 0; i < 2; i++) {
                    if (i == 0) {
                        menuForms[i] = Form.Initialize("Props", 175, 18, 850, 500);
                        test = menuForms[i];
                        menuForms[i].AddComponent("PropList", ComponentType.scrollView);
                        GameObject assetListObject = menuForms[i].GetFormComponent("PropList");
                        menuForms[i].SetComponentPosition("PropList", 0, 0);
                        menuForms[i].SetComponentSize("PropList", 850, 500);
                        assetListObject.transform.GetChild(0).GetChild(0).gameObject.AddComponent<GridLayoutGroup>();
                    } else {
                        menuForms[i] = Form.Initialize("Tools", 1050, 18, 200, 500);
                    }
                    menuForms[i].gameObject.SetActive(false);
                }

                return menuForms;
            // case GameModeList.walkthrough:
            //     return menuWindows;
        }
        return default;
    }

    public static Form[] CreatePlayerQuickMenu() {
        return null;
    }
}
