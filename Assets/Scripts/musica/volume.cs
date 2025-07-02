using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class volume : MonoBehaviour
{
    public Slider slider;
    public AudioMixer audioMixer;
    public string qual;
    public float valor;

    public void Deslizei()
    {
       audioMixer.SetFloat(qual,slider.value);
       PlayerPrefs.SetFloat(qual,slider.value);
    }

    public void Start()
    {
        valor = PlayerPrefs.GetFloat(qual, 0);
        slider.value = valor;
        audioMixer.SetFloat(qual, valor); 
    }
}
