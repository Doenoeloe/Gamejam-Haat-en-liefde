using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

// For JSON parsing (only picture field)
[System.Serializable]
public class RandomUserResponse
{
    public Result[] results;
}

[System.Serializable]
public class Result
{
    public Picture picture;
}

[System.Serializable]
public class Picture
{
    public string large;
}

public class API_testingScript : MonoBehaviour
{
    public RawImage targetImage; // Drag a UI RawImage here

    private void Start()
    {
        StartCoroutine(GetRandomImage());
    }
    // Hook this up to a Button OnClick
    public void LoadRandomImage()
    {
        StartCoroutine(GetRandomImage());
    }

    IEnumerator GetRandomImage()
    {
        // Step 1: Fetch JSON
        using (UnityWebRequest request = UnityWebRequest.Get("https://randomuser.me/api/"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string json = request.downloadHandler.text;
                RandomUserResponse response = JsonUtility.FromJson<RandomUserResponse>(json);

                if (response.results.Length > 0)
                {
                    string imageUrl = response.results[0].picture.large;
                    StartCoroutine(DownloadImage(imageUrl));
                }
            }
            else
            {
                Debug.LogError("Failed to fetch JSON: " + request.error);
            }
        }
    }

    // Step 2: Download image
    IEnumerator DownloadImage(string url)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                targetImage.texture = texture;
            }
            else
            {
                Debug.LogError("Failed to fetch image: " + request.error);
            }
        }
    }
}
