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
                        menuForms[i].AddComponent("PropList", ComponentType.scrollView);
                        GameObject assetListObject = menuForms[i].GetFormComponent("PropList");
                        menuForms[i].SetComponentPosition("PropList", 0, 0);
                        menuForms[i].SetComponentSize("PropList", 850, 500);

                        GameObject assetListContent = assetListObject.transform.GetChild(0).GetChild(0).gameObject;
                        GridLayoutGroup layout = assetListContent.AddComponent<GridLayoutGroup>();
                        layout.padding.left = 10; layout.padding.right = 10; layout.padding.top = 10; layout.padding.bottom = 10;
                        layout.childAlignment = TextAnchor.UpperCenter;
                        assetListContent.AddComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;

                        UnityEngine.Object[] gameplayObjects = Resources.LoadAll("Objects");
                        GameObject objectPreviewPerfab = (GameObject)Resources.Load("UI/ObjectPreview");
                        foreach (var gameplayObject in gameplayObjects) {
                            Texture2D objectPreviewTexture = AssetPreview.GetAssetPreview(gameplayObject);

                            Image objectPreview = MonoBehaviour.Instantiate(objectPreviewPerfab, assetListContent.transform).GetComponent<Image>();
                            objectPreview.sprite = Sprite.Create(objectPreviewTexture, new Rect(0, 0, objectPreviewTexture.width, objectPreviewTexture.height), objectPreview.rectTransform.pivot);
                            objectPreview.GetComponent<Button>().onClick.AddListener(() => ObjectTools.Spawn((GameObject)gameplayObject));
                        }
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
