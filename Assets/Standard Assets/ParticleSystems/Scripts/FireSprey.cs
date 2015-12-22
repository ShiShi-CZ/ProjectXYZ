using UnityEngine;
using System.Collections;

public class FireSprey : MonoBehaviour {
    public GameObject fire;
    public float Cas = 0;
	//Names like fajr and Cas are from my language (Czech), dont mind them


    // Use this for initialization
    void Start()
    {
        Light light = GetComponent<Light>();
        light.intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Light light = GetComponent<Light>();
        
        ParticleSystem fajr = GetComponentInParent<ParticleSystem>();
        if (fajr.isPlaying)
        {
            if(Cas == 0) //Noted time of the start of the particle system
            {
                Cas = Time.time+fajr.duration;
            }
            if (Time.time >= Cas)//When the duration is up, light goes off (priority over going on)
            {
                if (light.intensity > 0)
                {
                    light.intensity -= 0.05f;
                }
            }
			else//Light goes up to 2
            {
                if (light.intensity < 2)
                {
                    light.intensity += 0.10f;
                }
            }
        }
        else//If the particle system would end, but the light would not be fully off yet
        {
            if (light.intensity > 0)
            {
                light.intensity -= 0.20f;
            }
        }
    }
}
