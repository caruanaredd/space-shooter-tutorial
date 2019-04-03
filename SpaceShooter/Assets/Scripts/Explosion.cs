using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // A reference to the audio source on this object.
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
