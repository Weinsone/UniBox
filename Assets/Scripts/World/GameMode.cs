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
    public static GameModeList currentGameMode;

    public static Form[] CreatePlayerMenu() {
        Form[] menuForms;
        switch (currentGameMode) {
            case GameModeList.sandbox:
                menuForms = new Form[3];

                // Загрузка из XML всех данных для окна (интерфейса)
                // foreach (var menuWindow in menuWindows) {
                    
                // }

                // Временный костыль
                for (int i = 0; i < 3; i++) {
                    if (i == 0) {
                        menuForms[i] = Form.Initialize("Props", 225, 20, 850, 500);
                        menuForms[i].AddComponent("PropList", ComponentType.scrollView, 0, 0, 850, 500);
                        GameObject assetListObject = menuForms[i].GetFormComponent("PropList");

                        GameObject assetListContent = assetListObject.transform.GetChild(0).GetChild(0).gameObject;
                        GridLayoutGroup layout = assetListContent.AddComponent<GridLayoutGroup>();
                        layout.padding.left = 10; layout.padding.right = 10; layout.padding.top = 10; layout.padding.bottom = 10;
                        layout.childAlignment = TextAnchor.UpperCenter;
                        assetListContent.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;

                        UnityEngine.Object[] gameplayObjects = Resources.LoadAll("Objects");
                        GameObject previewButtonPerfab = (GameObject)Resources.Load("UI/ObjectPreview");
                        foreach (var gameplayObject in gameplayObjects) {
                            // Texture2D objectPreviewTexture = AssetPreview.GetAssetPreview(gameplayObject);

                            GameObject previewButton = MonoBehaviour.Instantiate(previewButtonPerfab, assetListContent.transform);
                            Image objectPreview = previewButton.GetComponent<Image>();
                            // objectPreview.sprite = Sprite.Create(AssetPreview.GetAssetPreview(gameplayObject), new Rect(0, 0, 128, 128), objectPreview.rectTransform.pivot);
                            objectPreview.sprite = Sprite.Create(Screenshot.CaptureIcon((GameObject)gameplayObject), new Rect(0, 0, 128, 128), objectPreview.rectTransform.pivot);
                            objectPreview.GetComponent<Button>().onClick.AddListener(() => ObjectTools.Spawn((GameObject)gameplayObject));
                        }
                    } else if (i == 1) {
                        menuForms[i] = Form.Initialize("Tools", 1200, 20, 200, 500);
                        GameObject buttonObject;

                        menuForms[i].AddComponent("MoverSelector", ComponentType.button, 10, 10, 180, default);
                        buttonObject = menuForms[i].GetFormComponent("MoverSelector");
                        buttonObject.transform.GetChild(0).GetComponent<Text>().text = "Move Tool";
                        buttonObject.GetComponent<Button>().onClick.AddListener(() => ObjectTools.SetTool(ToolList.mover));

                        menuForms[i].AddComponent("VertexSnapSelector", ComponentType.button, 10, 45, 180, default);
                        buttonObject = menuForms[i].GetFormComponent("VertexSnapSelector");
                        buttonObject.transform.GetChild(0).GetComponent<Text>().text = "Vertex Snap Tool";
                        buttonObject.GetComponent<Button>().onClick.AddListener(() => ObjectTools.SetTool(ToolList.vertexSnap));
                    } else {
                        menuForms[i] = Form.Initialize("AI", 10, 20, 200, 500);

                        GameObject buttonObject;

                        menuForms[i].AddComponent("BotFollower", ComponentType.button, 10, 10, 180, default);
                        buttonObject = menuForms[i].GetFormComponent("BotFollower");
                        buttonObject.transform.GetChild(0).GetComponent<Text>().text = "Bot follower";
                        buttonObject.GetComponent<Button>().onClick.AddListener(() => Server.AddBot(new Bot(Server.Bots.Count, "Bot" + Server.Bots.Count, BotBehaviorList.Behaviors.follower, ControllerList.Controllers.assistant, new Vector3(0, 0.5f, 0))));
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
