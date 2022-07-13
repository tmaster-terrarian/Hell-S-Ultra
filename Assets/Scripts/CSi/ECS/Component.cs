using System;
using System.Collections.Generic;
using System.Text;

namespace CSi.ECS
{
    class Component
    {
        public Entity entity;

        public virtual void Update(float gameTime) { }
    }
}
