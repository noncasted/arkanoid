﻿using System;
using UnityEngine;

namespace NaughtyAttributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class CurveRangeAttribute : DrawerAttribute
    {
        public Vector2 Min { get; private set; }
        public Vector2 Max { get; private set; }
        public EColor Color { get; private set; }

        public CurveRangeAttribute()
        {
            Min = Vector2.zero;
            Max = Vector2.one;
            Color = EColor.Clear;
        }
        
        public CurveRangeAttribute(Vector2 min, Vector2 max, EColor color = EColor.Clear)
        {
            Min = min;
            Max = max;
            Color = color;
        }
        
        public CurveRangeAttribute(float height)
        {
            Min = Vector2.zero;
            Max = new Vector2(1f, height);
            Color = EColor.Clear;
        }


        public CurveRangeAttribute(EColor color)
            : this(Vector2.zero, Vector2.one, color)
        {
        }

        public CurveRangeAttribute(float minX, float minY, float maxX, float maxY, EColor color = EColor.Clear)
            : this(new Vector2(minX, minY), new Vector2(maxX, maxY), color)
        {
        }
    }
}
