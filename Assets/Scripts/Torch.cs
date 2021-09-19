using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Torch : Interactive
{
    private Torchelight torchLight;
    private AudioSource torchSound;

    public float intensity;

    void Start()
    {
        torchLight = GetComponent<Torchelight>();
        torchSound = GetComponent<AudioSource>();
        status = true;
        torchSound.Play();
    }
    
    public override void Switch()
    {
        Debug.Log("Torch status switching: " + !status);
        status = !status;
        if (status)
            torchSound.Play();
        else
            torchSound.Stop();

        torchLight.IntensityLight = status ? intensity : 0;
    }
}
