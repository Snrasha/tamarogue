// <copyright file="Tile.cs" company="Audino">
// Copyright (c) Audino
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using RogueElements;

namespace RogueElements.Examples
{
    public class RTile : ITile
    {
        public RTile()
        {
            this.ID = BaseMap.WALL_TERRAIN_ID;
        }

        public RTile(int id)
        {
            this.ID = id;
        }

        protected RTile(RTile other)
        {
            this.ID = other.ID;
        }

        public int ID { get; set; }

        public ITile Copy() => new RTile(this);

        public bool TileEquivalent(ITile other)
        {
            if (!(other is RTile tile))
                return false;
            return tile.ID == this.ID;
        }
    }
}
