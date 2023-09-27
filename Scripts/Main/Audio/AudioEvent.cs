using UnityEngine;

namespace sm_application.Audio
{
    public abstract class AudioEvent : ScriptableObject
    {
        public abstract void Play(AudioSource audioSource);
    }
}
