﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SamGame01.Collisions
{
    /// <summary>
    /// Class which is used to manage collisions between game objects
    /// </summary>
    public static class CollisionHelper
    {
        /// <summary>
        /// Detects collision between two BoundingCircles
        /// </summary>
        /// <param name="a">The first BoundingCircle</param>
        /// <param name="b">The second BoundingCircle</param>
        /// <returns>true for collision, false otherwise</returns>
        public static bool Collides(BoundingCircle a, BoundingCircle b)
        {
            return Math.Pow(a.Radius + b.Radius, 2) >=
                Math.Pow(a.Center.X - b.Center.X, 2) +
                Math.Pow(a.Center.Y - b.Center.Y, 2);
        }

        /// <summary>
        /// Detects collision between two BoundingRectangles
        /// </summary>
        /// <param name="a">The first BoundingRectangle</param>
        /// <param name="b">The second BoundingRectangle</param>
        /// <returns>True for collision, false otherwise</returns>
        public static bool Collides(BoundingRectangle a, BoundingRectangle b)
        {
            return !(a.Right < b.Left || a.Left > b.Right ||
                    a.Top > b.Bottom || a.Bottom < b.Top
                );
        }


        /// <summary>
        /// Detects collision between a rectangle and a circle
        /// </summary>
        /// <param name="c">the bounding circle</param>
        /// <param name="r">the bounding rectangle</param>
        /// <returns>True for collision, false otherwise</returns>
        public static bool Collides(BoundingCircle c, BoundingRectangle r)
        {
            float nearestX = MathHelper.Clamp(c.Center.X, r.Left, r.Right);
            float nearestY = MathHelper.Clamp(c.Center.Y, r.Top, r.Bottom);

            return Math.Pow(c.Radius, 2) >=
                Math.Pow(c.Center.X - nearestX, 2) +
                Math.Pow(c.Center.Y - nearestY, 2);
        }

        /// <summary>
        /// Detects collision between a rectangle and a circle
        /// </summary>
        /// <param name="r">the bounding rectangle</param>
        /// <param name="c">the bounding circle</param>
        /// <returns>True for collision, false otherwise</returns>
        public static bool Collides(BoundingRectangle r, BoundingCircle c) => Collides(c, r);
    }
}
