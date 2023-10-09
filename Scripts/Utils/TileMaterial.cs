using System;
using UnityEngine;

namespace sm_application.Scripts.Utils
{
    public class TileMaterial : MonoBehaviour
    {
        [SerializeField] private float tileX = 1;
        [SerializeField] private float tileY = 1;
        [SerializeField] private bool _tileYToZScale;
        
        void OnEnable()
        {
            SetTileSize();
        }

        private void OnValidate()
        {
            SetTileSize();
        }

        private void SetTileSize()
        {
            var material = GetComponent<Renderer>().material;
            // var mesh = GetComponent<MeshFilter>().mesh;
            var scaleX = transform.localScale.x;
            var scaleY = _tileYToZScale ? transform.localScale.z : transform.localScale.y;
            material.mainTextureScale = new Vector2( scaleX * tileX,  scaleY * tileY);
        }
    }
}