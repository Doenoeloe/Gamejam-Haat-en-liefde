using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoverEffect : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    public Color wantedColor;
    private Color originalColor;
    private Color Textcolor;

    void Start()
    {
       Textcolor = text.color;
       originalColor = text.color;
    }

    public void OnHoverEnter()
    {
        text.color = wantedColor;
        text.fontSize = 36;
    }

    public void OnHoverExit()
    {
        text.color = originalColor;
        text.fontSize = 24;
    }

}
