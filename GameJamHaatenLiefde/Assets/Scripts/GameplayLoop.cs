using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayLoop : MonoBehaviour
{
    API_testingScript api;
    [SerializeField] Material paperColor;
    int condition;
    public int points;
    int rand;
    int rand2;
    System.Random random = new System.Random();
    private string[] horo;

    private char[] alph;

    private void Start()
    {
        api = GetComponent<API_testingScript>();
        paperColor.color = new Color(0.9450980392156862f, 0.9137254901960784f, 0.8235294117647058f, 1f);
        alph = new char[]
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
            'V',
            'W', 'X', 'Y', 'Z'
        };
        horo = new string[]
        {
            "Ram", "Stier", "Tweelingen", "Kreeft", "Leeuw", "Maagd", "Weegschaal", "Schorpioen", "Boogschutter",
            "Steenbok", "Waterman", "Vissen"
        };
        rand = random.Next(0, 11);
        rand2 = random.Next(0, 25);
        condition = random.Next(0, 1);
    }

    public void YesButton()
    {
        CheckForRequirements(true);
        paperColor.color = Color.green;
        StartCoroutine(WaitHalfSecond(0.5f));
    }
    public void NoButton()
    {
        CheckForRequirements(false);
        paperColor.color = Color.red;
        StartCoroutine(WaitHalfSecond(0.5f));
    }

    void ResetPaper()
    {
        CheckScore();
        api.ShowRandomProfile();
        api.LoadRandomImage();
        paperColor.color = new Color(0.9450980392156862f, 0.9137254901960784f, 0.8235294117647058f, 1f);
    }

    IEnumerator WaitHalfSecond(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ResetPaper();
    }

    void CheckForRequirements(bool pressed)
    {
        switch (condition)
        {
            case 0:

                bool win = api.naamText.text.Contains(horo[rand]);
                Debug.Log(horo[rand]);
                if (!(win == true && pressed == true))
                {
                    points++;
                    Debug.Log(points);
                }
                else if (!(win == false && pressed == false))
                {
                    points++;
                    Debug.Log(points);
                }

                break;
            case 1:
                char firstLetter = api.naamText.text.ToCharArray()[0];
                bool win2 = firstLetter == alph[rand2];
                if (!(win2 == true && pressed == true))
                {
                    points++;
                    Debug.Log(points);
                }
                else if (!(win2 == false && pressed == false))
                {
                    points++;
                    Debug.Log(points);
                }

                break;
        }
    }

    void CheckScore()
    {
        int numb;
        numb = random.Next(0, 2);

        if (points == 50)
        {
            if (numb == 1)
            {
                SceneManager.LoadScene(sceneName: "menu");
            }
            else
            {
                SceneManager.LoadScene(sceneName: "menu-day");
            }
        }
    }
}