using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchTimeDilation : MonoBehaviour
{
    public float SlowDownAmount = 0.05f; //amount to be slowed down 
    public float SlowDownTime = 2f; //time it takes for slow down.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale += (1f/SlowDownTime) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    public void WitchTime(){
        Time.timeScale = SlowDownAmount;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
}
