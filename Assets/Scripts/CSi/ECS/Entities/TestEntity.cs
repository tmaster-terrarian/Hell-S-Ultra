using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CSi.ECS
{
    class TestEntity : Entity
    {
        public TestEntity(/* Texture2D tex */)
        {
            // add a `Transform` component to store the character's position
            Transform transform = new Transform();
            transform.position = new Vector2(0, 0);
            AddComponent(transform);

            // add a `Sprite` component to store the character's texture
            Sprite sprite = new Sprite();
            // sprite.texture = tex;
            AddComponent(sprite);
        }
    }
}
