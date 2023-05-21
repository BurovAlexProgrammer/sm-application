﻿using smApplication.Scripts.Extension;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace sm_application.Scripts.Main.Game
{
    public class GizmoItem : MonoBehaviour
    {
        public float ShowTime = 1f;

        private void Start()
        {
            _ = DestroyAfter(ShowTime);
        }

        private async UniTask DestroyAfter(float time)
        {
            await time.WaitInSeconds();
            
            Destroy(gameObject);
        }
    }
}