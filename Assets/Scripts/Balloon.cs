using System;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public int PlayerID;
    public ParticleSystem particleSystem;

    internal void PlayParticle()
    {
        particleSystem.transform.SetParent(null);
        particleSystem.gameObject.SetActive(true);
        particleSystem.Play();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
