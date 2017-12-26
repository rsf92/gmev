using UnityEngine;
using System.Collections;

public class LoadStartScene : MonoBehaviour
{
    public int timeToComplete = 3;
 
    // Use this for initialization
    void Start () {
        //Use this to Start progress
        StartCoroutine(RadialProgress(timeToComplete));
    }
 
    IEnumerator RadialProgress(float time)
    {
	while (true)
        {
            yield return new WaitForSeconds(time);
            print("WaitAndPrint " + Time.time);
        }
    }
}
