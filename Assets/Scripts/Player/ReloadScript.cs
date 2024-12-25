using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReloadScript : MonoBehaviour
{
    [SerializeField] private Shoot shoot;

    public void ReloadEnd()
    {
        shoot.bullets = shoot.maxBullets;
        shoot.reloadText.text = "";
        shoot.reloading = false;
    }

    public void ReloadSound()
    {
        shoot.audioSource.PlayOneShot(shoot.reload);
    }
}
