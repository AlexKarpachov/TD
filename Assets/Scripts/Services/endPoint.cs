using System.Collections.Generic;
using UnityEngine;

public class endPoint : MonoBehaviour
{
    [SerializeField] ParticleSystem enemyInsideVFX;
    [SerializeField] AudioClip enemyInsideSFX;
    [SerializeField] AudioSource audioSource;

    List<string> enemyTags = new List<string> { "red enemy", "blue enemy", "RedSwordman", "BlueSwordman" };


    private void OnTriggerEnter(Collider other)
    {
        if (enemyTags.Contains(other.gameObject.tag))
        {
            enemyInsideVFX.Play();
            audioSource.PlayOneShot(enemyInsideSFX);
        }
    }
}
