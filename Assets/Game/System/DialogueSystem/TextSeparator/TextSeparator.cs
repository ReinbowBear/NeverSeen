﻿using UnityEngine;
using UnityEngine.UI;

public class TextSeparator : MonoBehaviour 
{
    public string text;
    public Font font;
    public Material material;

    void Start()
    {
        for (int i = 0; i < text.Length; i++){
            GameObject newGO = new GameObject(text[i].ToString());
            newGO.transform.SetParent(transform);

            Text myText = newGO.AddComponent<Text>();

            RectTransform parentTransform = GetComponentInParent<RectTransform>();
            myText.text = text[i].ToString();
            myText.alignment = TextAnchor.LowerCenter;
            myText.font = font;
            myText.material = material;
            //myText.GetComponent<RectTransform>().localPosition = new Vector3(parentTransform.localPosition.x + (i*17) , parentTransform.localPosition.y, myText.rectTransform.localPosition.z);
            myText.GetComponent<RectTransform>().localPosition = new Vector3(i * 17 , 0, 0);
            myText.fontSize = 40;
            myText.color = new Color(1, 0, 0, 1);

            //newGO.AddComponent<ShakeText>();
            newGO.AddComponent<WaveText>();
            newGO.GetComponent<WaveText>().Offset = 0.15f * i;
        }
    }
}
