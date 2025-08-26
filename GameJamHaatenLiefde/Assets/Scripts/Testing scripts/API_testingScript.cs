using System.IO;
using System.Net.Http;
using System.Threading;
using System;
using UnityEngine;
using Unity.VisualScripting;

public class API_testingScript : MonoBehaviour
{
    public Button yourButton;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(GeneratePicture);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void GeneratePicture()
    {
        Console.WriteLine("Hello world!");


        var max = 1000;
        var url = "https://www.thispersondoesnotexist.com/image";


        using (var client = new HttpClient())
        {
            for (int i = 0; i < max; i++)
            {
                var guid = Guid.NewGuid().ToString();
                var fileInfo = new FileInfo($"{guid}.jpg");
                System.Console.WriteLine("downloading: " + guid);
                var result = await client.GetAsync(url);
                result.EnsureSuccessStatusCode();
                var ms = await result.Content.ReadAsStreamAsync();
                var fs = File.Create(fileInfo.FullName);
                ms.Seek(0, SeekOrigin.Begin);
                ms.CopyTo(fs);
                Thread.Sleep(1500);
            }
        }
    }
}
