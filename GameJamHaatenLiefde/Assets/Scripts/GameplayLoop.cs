using UnityEngine;
using System.Collections;

public class GameplayLoop : MonoBehaviour
{
    API_testingScript api;
    [SerializeField] Material paperColor;
    private void Start()
    {
        api = GetComponent<API_testingScript>();
        paperColor.color = new Color(0.9450980392156862f, 0.9137254901960784f, 0.8235294117647058f, 1f);
    }
    public void YesButton()
    {
        paperColor.color = Color.green;
        StartCoroutine(WaitHalfSecond(0.5f));
    }
    public void NoButton()
    {
        paperColor.color = Color.red;
        StartCoroutine(WaitHalfSecond(0.5f));
    }
    void ResetPaper()
    {
        api.LoadRandomImage();
        paperColor.color = new Color(0.9450980392156862f, 0.9137254901960784f, 0.8235294117647058f, 1f);
    }
    IEnumerator WaitHalfSecond(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ResetPaper();
    }

}
