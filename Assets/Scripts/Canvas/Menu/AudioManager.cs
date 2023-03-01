using UnityEngine;

public static class AudioManager
{
    public static AudioSource PlayAudio(AudioClip clip)
    {
        AudioSource audioSource =
            new GameObject(string.Concat("Sound - ", clip),
            typeof(AudioSource)).GetComponent<AudioSource>();

        if (clip == null)
            Debug.LogError("Audioclip reference not found");

        audioSource.clip = clip;

        audioSource.Play();

        Object.Destroy(audioSource.gameObject, clip.length);

        return audioSource;
    }
}
