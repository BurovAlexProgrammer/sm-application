using UnityEngine;

namespace sm_application.Scripts.Main.Audio
{
    public abstract class AudioEvent : ScriptableObject
    {
        public abstract void Play(AudioSource audioSource);
    }
}
