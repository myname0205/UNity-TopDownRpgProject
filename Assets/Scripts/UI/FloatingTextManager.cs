using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingTextObject> floatingTexts = new List<FloatingTextObject>();

    private void Update()
    {
        foreach(FloatingTextObject txt in floatingTexts)
        {
            txt.UpdateFloatingText();
        }
    }

    public void Show(string message, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingTextObject floatingText = GetFloatingText();

        floatingText.txt.text = message;
        floatingText.txt.fontSize = fontSize;
        floatingText.txt.color = color;
        
        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position); //Transfer world space to screen space
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.Show();
    }

    private FloatingTextObject GetFloatingText()
    {
        FloatingTextObject txt = floatingTexts.Find(t => !t.isActive);

        if(txt == null)
        {
            txt = new FloatingTextObject();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform);
            txt.txt = txt.go.GetComponent<TextMeshProUGUI>();

            floatingTexts.Add(txt);
        }

        return txt;
    }
}
