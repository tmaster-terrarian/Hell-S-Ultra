using System;
using System.Collections.Generic;
using System.Text;

namespace CSi.ECS
{
    class BaseSystem<T> where T : Component
    {
        public static List<T> components = new List<T>();

        public static void Register(T component)
        {
            components.Add(component);
        }

        public static void Update(float gameTime)
        {
            foreach (T component in components)
            {
                component.Update(gameTime);
            }
        }
    }

    class TransformSystem : BaseSystem<Transform>{}
}
