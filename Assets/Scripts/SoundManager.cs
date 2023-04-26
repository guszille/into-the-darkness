using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] collisionClipArray;

    private void Start()
    {
        MainCharacterController.Instance.OnGrounded += MainCharacter_OnGrounded;
    }

    private void MainCharacter_OnGrounded(object sender, System.EventArgs args)
    {
        PlayGroundedSound(transform.position);
    }

    private void PlayGroundedSound(Vector3 position, float volume = 1f)
    {
        AudioClip audioClip = collisionClipArray[Random.Range(0, collisionClipArray.Length)];

        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}
