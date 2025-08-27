using UnityEngine;

public class MaterialAnimated : MonoBehaviour
{
    int currentIndex = 0; //keeps track of where we are in the list
    public Material[] materialList; //list of where the materials are stored
    public int framesPerSecond; //how many material changes should there be every second?
    float duration;
    float interval;
    public GameObject affectedBrush;
    float nextUpdate;

    void Start()
    {
        CalculateDuration(framesPerSecond);

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextUpdate)
        {
            nextUpdate += interval;
            if (currentIndex >= materialList.Length - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }
            Material material = materialList[currentIndex];
            
            //apply the new material to the brushwork
            affectedBrush.GetComponent<Renderer>().material = material;

        }
    }

    void CalculateDuration(int _fps)
    {
        //get list length
        int length = materialList.Length;
        duration = length / framesPerSecond;
        interval = duration / length;
    }
}


