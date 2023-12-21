using System.Collections;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip Jump;
    public AudioClip Death;
    public AudioClip Rope;
    public AudioClip Ghost;
    public AudioClip Teleport;
    private AudioSource _playerAudio;
    private AudioSource _ropeAudio;

    private void Start()
    {
        _playerAudio = GetComponent<AudioSource>();
        _ropeAudio = FindObjectOfType<RopeAnimation>().GetComponent<AudioSource>();
    }

    public void PlayJump()
    {
        _playerAudio.PlayOneShot(Jump);
    }

    public void PlayDeath()
    {
        _playerAudio.PlayOneShot(Death);
    }

    public void PlayRope()
    {
        _ropeAudio.PlayOneShot(Rope);
    }

    public void PlayTeleportation()
    {
        _playerAudio.PlayOneShot(Teleport);
    }

    public void PlayGhost()
    {
        _playerAudio.PlayOneShot(Ghost);
    }
}
