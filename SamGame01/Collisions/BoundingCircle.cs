﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace SamGame01.Collisions
{
    /// <summary>
    /// A struct representing circular bounds
    /// </summary>
    public struct BoundingCircle
    {
        /// <summary>
        /// The center of the BounduingCircle
        /// </summary>
        public Vector2 Center;

        /// <summary>
        /// The radius of the BoundingCircle
        /// </summary>
        public float Radius;

        /// <summary>
        /// Constructs a new BoundingCircle
        /// </summary>
        /// <param name="center">the center</param>
        /// <param name="radius">the radius</param>
        public BoundingCircle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        /// <summary>
        /// Tests for a collision between this and another BoundingCircle
        /// </summary>
        /// <param name="other">The other BoundingCirlce</param>
        /// <returns>True for collision, false otherwise</returns>
        public bool CollidesWith(BoundingCircle other)
        {
            return CollisionHelper.Collides(this, other);
        }

        /// <summary>
        /// Tests for a collision between this and a BoundingRectangle
        /// </summary>
        /// <param name="other">The BoundingRectangle</param>
        /// <returns>True for collision, false otherwise</returns>
        public bool CollidesWith(BoundingRectangle other)
        {
            return CollisionHelper.Collides(this, other);
        }

    }
}
