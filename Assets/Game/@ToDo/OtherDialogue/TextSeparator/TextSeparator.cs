using UnityEngine;
using UnityEngine.UI;

public class TextSeparator : MonoBehaviour 
{
    public RectTransform root;
    public string text;
    public Font font;
    public Material material;

    void Start()
    {
        for (int i = 0; i < text.Length; i++)
        {
            GameObject letterObj = new GameObject(text[i].ToString());
            letterObj.transform.SetParent(root);

            Text myText = letterObj.AddComponent<Text>();

            myText.text = text[i].ToString();
            myText.alignment = TextAnchor.MiddleCenter;
            myText.font = font;
            myText.material = material;
            myText.GetComponent<RectTransform>().localPosition = new Vector3(i * 17 , 0, 0);
            myText.fontSize = 40;
            myText.color = new Color(1, 0, 0, 1);

            letterObj.AddComponent<ShakeText>();
            letterObj.AddComponent<WaveText>();
            letterObj.GetComponent<WaveText>().Offset = 0.15f * i;
        }
    }
}
