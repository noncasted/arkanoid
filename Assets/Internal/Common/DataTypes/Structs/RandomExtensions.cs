﻿using UnityEngine;

namespace Internal
{
    public static class RandomExtensions
    {
        public static Vector2 RandomDirection()
        {
            return new Vector2(RandomNormalized(), RandomNormalized());
        }

        public static float RandomNormalized()
        {
            return Random.Range(-1f, 1f);
        }

        public static float RandomOne()
        {
            return Random.Range(0f, 1f);
        }

        public static int RandomPercent()
        {
            return Random.Range(0, 100);
        }

        public static Vector2 RandomPosition(float min, float max)
        {
            return new Vector2(Random.Range(min, max), Random.Range(min, max));
        }
    }
}