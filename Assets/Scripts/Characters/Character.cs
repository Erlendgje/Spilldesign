using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public Color color;

    public void Start()
    {
        ParticleSystem.MainModule newMain = GetComponentInChildren<ParticleSystem>().main;
        newMain.startColor = color;
    }

}
