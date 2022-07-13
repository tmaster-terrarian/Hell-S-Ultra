using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CSi.ECS
{
    class Transform : Component
    {
        public Vector2 position = Vector2.zero;
        public Vector2 scale = Vector2.zero;
        public float layerDepth = 0;
        public float rotation = 0;

        public Transform()
        {
            TransformSystem.Register(this);
        }
    }
}
