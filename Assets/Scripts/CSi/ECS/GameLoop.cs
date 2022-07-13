using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace CSi.ECS
{
    public class GameLoop : MonoBehaviour
    {
        static void Main(string[] args)
        {
            var game = new Game();

            // main game loop
            while (true)
            {
                game.Update(0.01f); // assume the proper frame delta is passed
            }
        }
    }

    class Game
    {
        public void Update(float gameTime)
        {
            TransformSystem.Update(gameTime);
            SpriteSystem.Update(gameTime);
        }
    }
}
