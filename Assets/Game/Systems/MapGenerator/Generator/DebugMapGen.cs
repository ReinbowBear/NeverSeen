using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugMapGen : MonoBehaviour
{
    private RectTransform layoutRoot;

    private int width = 256;
    private int height = 256;

    void Start()
    {
        CreateCanvas();

        var settings = new WorldSettings();
        var world = new WorldNoise(settings);

        ShowNoise(nameof(world.ContinentNoise), world.ContinentNoise);
        ShowNoise(nameof(world.DetailNoise), world.DetailNoise);
        ShowNoise(nameof(world.WarpNoise), world.WarpNoise);
        ShowNoise(nameof(world.MoistureNoise), world.MoistureNoise);
        ShowNoise(nameof(world.TemperatureNoise), world.TemperatureNoise);
        ShowNoise(nameof(world.FinalHeightNoise), world.FinalHeightNoise);
    }

    public void ShowNoise(string noiseName, INoise noise, float scale = 1f)
    {
        Texture2D texture = GetImage(noiseName);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float nx = x / (float)width * scale;
                float ny = y / (float)height * scale;

                float value = noise.GetHeight(nx, ny);
                value = Mathf.Clamp01(value);

                texture.SetPixel(x, y, new Color(value, value, value));
            }
        }

        texture.Apply();
    }


    private void CreateCanvas()
    {
        GameObject canvasObj = new GameObject("AutoCanvas");
        Canvas newCanvas = canvasObj.AddComponent<Canvas>();
        newCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

        canvasObj.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasObj.AddComponent<GraphicRaycaster>();


        GameObject layoutObj = new GameObject("LayoutContainer");
        layoutObj.transform.SetParent(canvasObj.transform, false);

        RectTransform rect = layoutObj.AddComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;


        GridLayoutGroup grid = layoutObj.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(width, height);
        grid.spacing = new Vector2(10, 10);
        grid.startAxis = GridLayoutGroup.Axis.Horizontal;
        grid.childAlignment = TextAnchor.MiddleCenter;

        layoutRoot = rect;
    }

    private Texture2D GetImage(string noiseName)
    {
        var imageObj = new GameObject(noiseName);
        imageObj.transform.SetParent(layoutRoot);

        var rect = imageObj.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(width, height);

        var image = imageObj.AddComponent<RawImage>();
        var texture = new Texture2D(width, height);
        image.texture = texture;


        var textObj = new GameObject("Text");
        textObj.transform.SetParent(imageObj.transform);

        var textRect = textObj.AddComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        var text = textObj.AddComponent<TextMeshProUGUI>();
        text.text = noiseName;
        text.fontSize = 22;
        text.alignment = TextAlignmentOptions.Center;

        return texture;
    }
}
