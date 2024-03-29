using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace sm_application
{
    [CreateAssetMenu(menuName = "Custom/GameColors")]
    public class GameColors : ScriptableObject
    {
        [SerializeField] public GameColor[] colors;

        public GameColor GetRandom()
        {
            var colorIndex = Random.Range(0, colors.Length);
            return colors[colorIndex];
        }
    }

    [Serializable]
    public class GameColor
    {
        public string name;
        public Material material;
        public Color color;
    }
}