using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum GameModeList
{
    sandbox,
    walkthrough
}

public static class GameMode
{
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
                        menuForms[i].AddComponent("PropList", ComponentType.scrollView, 0, 0, 850, 500);
                        GameObject assetListObject = menuForms[i].GetFormComponent("PropList");

                        GameObject assetListContent = assetListObject.transform.GetChild(0).GetChild(0).gameObject;
                        GridLayoutGroup layout = assetListContent.AddComponent<GridLayoutGroup>();
                        layout.padding.left = 10; layout.padding.right = 10; layout.padding.top = 10; layout.padding.bottom = 10;
                        layout.childAlignment = TextAnchor.UpperCenter;
                        assetListContent.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;

                        UnityEngine.Object[] gameplayObjects = Resources.LoadAll("Objects");
                        GameObject objectPreviewPerfab = (GameObject)Resources.Load("UI/ObjectPreview");
                        foreach (var gameplayObject in gameplayObjects) {
                            // Texture2D objectPreviewTexture = AssetPreview.GetAssetPreview(gameplayObject);

                            Image objectPreview = MonoBehaviour.Instantiate(objectPreviewPerfab, assetListContent.transform).GetComponent<Image>();
                            objectPreview.sprite = Sprite.Create(AssetPreview.GetAssetPreview(gameplayObject), new Rect(0, 0, 128, 128), objectPreview.rectTransform.pivot);
                            objectPreview.GetComponent<Button>().onClick.AddListener(() => ObjectTools.Spawn((GameObject)gameplayObject));
                        }
                    } else {
                        menuForms[i] = Form.Initialize("Tools", 1050, 18, 200, 500);
                        GameObject buttonObject;

                        menuForms[i].AddComponent("MoverSelector", ComponentType.button, 10, 10, 180, default);
                        buttonObject = menuForms[i].GetFormComponent("MoverSelector");
                        buttonObject.transform.GetChild(0).GetComponent<Text>().text = "Move Tool";
                        buttonObject.GetComponent<Button>().onClick.AddListener(() => ObjectTools.SetTool(ToolList.mover));

                        menuForms[i].AddComponent("VertexSnapSelector", ComponentType.button, 10, 45, 180, default);
                        buttonObject = menuForms[i].GetFormComponent("VertexSnapSelector");
                        buttonObject.transform.GetChild(0).GetComponent<Text>().text = "Vertex Snap Tool";
                        buttonObject.GetComponent<Button>().onClick.AddListener(() => ObjectTools.SetTool(ToolList.vertexSnap));
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
