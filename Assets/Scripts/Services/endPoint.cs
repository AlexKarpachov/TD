using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endPoint : MonoBehaviour
{
    [SerializeField] ParticleSystem enemyInsideVFX;
    [SerializeField] AudioClip enemyInsideSFX;

    AudioSource audioSource;
    List<string> enemyTags = new List<string> {"red enemy", "blue enemy", "RedSwordman", "BlueSwordman" };

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemyTags.Contains(other.gameObject.tag))
        {
            enemyInsideVFX.Play();
            audioSource.PlayOneShot(enemyInsideSFX);
        }
    }
}
