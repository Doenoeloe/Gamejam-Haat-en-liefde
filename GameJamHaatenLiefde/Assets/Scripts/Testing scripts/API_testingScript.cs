using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using TMPro;


// ====== PROFIEL KLASSEN ======
[System.Serializable]

public class Profile
{
    public string Naam;
    public string AboutMe;
    public string LookingFor;
    public string[] MyInterests;
}

[System.Serializable]
public class ProfileList
{
    public Profile[] profiles;
}

// ====== RANDOM USER KLASSEN (AL BESTONDEN) ======
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
    [Header("UI Elements")]
    public RawImage targetImage;   // Voor de foto
    public TextMeshPro naamText;          // Sleep hier een UI Text in
    public TextMeshPro aboutMeText;
    public TextMeshPro lookingForText;
    public TextMeshPro interestsText;

    private ProfileList profileList;

    [SerializeField] GameObject canvasYorick;
    [SerializeField] GameObject canvasRuben;
    [SerializeField] GameObject canvasRinze;
    [SerializeField] GameObject canvasSimon;
    [SerializeField] GameObject canvasMohammed;

    RawImage canvasY;
    RawImage canvasRu;
    RawImage canvasS;
    RawImage canvasRi;
    RawImage canvasF;


    private void Start()
    {
        canvasY = canvasYorick.GetComponent<RawImage>();
        canvasRu = canvasRuben.GetComponent<RawImage>();
        canvasS = canvasSimon.GetComponent<RawImage>();
        canvasRi = canvasRinze.GetComponent<RawImage>();
        canvasF = canvasMohammed.GetComponent<RawImage>();

        canvasY.enabled = false;
        canvasRu.enabled = false;
        canvasRi.enabled = false;
        canvasS.enabled = false;
        canvasF.enabled = false;

        // 1. Profielen inladen
        LoadProfiles();

        // 2. Foto + random profiel laden
        StartCoroutine(GetRandomImage());
        ShowRandomProfile();
    }

    // ========== PROFIEL FUNCTIES ==========
    public void LoadProfiles()
    {

        string filePath = Path.Combine(Application.streamingAssetsPath, "Profiles.json");
        string json = File.ReadAllText(filePath);


        // Omdat het bestand een array is, moeten we een wrapper toevoegen
        json = "{ \"profiles\": " + json + " }";

        profileList = JsonUtility.FromJson<ProfileList>(json);
    }

    Profile randomProfile;
    public void ShowRandomProfile()
    {
        if (profileList == null || profileList.profiles.Length == 0)
        {
            Debug.LogError("Geen profielen gevonden!");
            return;
        }

        randomProfile = profileList.profiles[Random.Range(0, profileList.profiles.Length)];

        naamText.text = randomProfile.Naam;
        aboutMeText.text = randomProfile.AboutMe;
        lookingForText.text = randomProfile.LookingFor;
        interestsText.text = string.Join("\n", randomProfile.MyInterests);
    }

    // ========== AFBEELDING INLADEN ==========

    public void LoadRandomImage()
    {

        switch (randomProfile.Naam)
        {
            case "Yorick (18) – Steenbok – 1.95m":
                canvasY.enabled = true;
                break;
            case "Ruben (19) – Kreeft – 1.70m":
                canvasRu.enabled = true;
                break;
            case "Rinze (18) – vissen – 1.90m":
                canvasRi.enabled = true;
                break;
            case "Simon (18) – Steenbok – 1.79m":
                canvasS.enabled = true;
                break;
            case "Fatma fatume Mohammed (18) – Stier – 1.62m":
                canvasF.enabled = true;
                break;
            default:
                canvasY.enabled = false;
                canvasRu.enabled = false;
                canvasRi.enabled = false;
                canvasS.enabled = false;
                canvasF.enabled = false;
                StartCoroutine(GetRandomImage());
                ShowRandomProfile();
                break;
        }
    }

    IEnumerator GetRandomImage()
    {
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
