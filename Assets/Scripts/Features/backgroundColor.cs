using UnityEngine;
using System.Collections;

//This class is responsible for gradually changing the color of the background.
public class backgroundColor : MonoBehaviour {

    Renderer backgroundRenderer;

    //Color array - these are the colors that will be cycled through
    //dark blue, cyan, green, grey, dark red, pink, purple
    private Color[] colorList= new Color[]{ new Color(0.11f, 0.06f, 0.38f, 0.3f), new Color(0,1,1,0.3f) ,
    new Color(0, 0.34f, 0.26f, 0.3f), new Color(0.5f, 0.5f, 0.5f, 0.3f), new Color(0.50f, 0.07f, 0.07f, 0.3f), new Color(0.88f, 0.56f, 0.82f, 0.3f), new Color(0.29f, 0.16f, 0.82f, 0.3f)};
    private float time=0;
    private int counter=0;
    [Header("Transition time(seconds)")]
    public float duration = 10;
  
    void Start () {
        backgroundRenderer = GetComponent<Renderer>();
 	}

   
    void Update () {
        //change background color gradually
        backgroundRenderer.material.color = Color.Lerp(colorList[counter], colorList[(counter+1)%colorList.Length], time);
        if (time < 1)
        {
            time += Time.deltaTime / duration;
        }else //Resets and moves on to next color
        {
            time = 0;
            counter = (counter + 1) % colorList.Length;
        }
       
    }

  
}
