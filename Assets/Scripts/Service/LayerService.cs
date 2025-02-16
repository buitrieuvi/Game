using System;
using UnityEngine;


namespace Game.Service
{
    [Serializable]
    public class LayerService
    {
        public LayerMask GroundLayer;

        public bool ContainsLayer(LayerMask layerMask, int layer)
        {
            return (1 << layer & layerMask) != 0;
        }

        public bool IsGroundLayer(int layer)
        {
            return ContainsLayer(GroundLayer, layer);
        }
    }
}