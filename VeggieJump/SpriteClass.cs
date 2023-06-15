//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  The MIT License (MIT)

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2D
{
    class SpriteClass
    {
        // experiment with this value to make the collision detection more or less forgiving
        const float HITBOXSCALE = .5f; 

        // sprite texture
        public Texture2D texture
        {
            get;
        }

        // x coordinate of the center of the sprite
        public float x
        {
            get;
            set;
        }

        // y coordinate of the center of the sprite
        public float y
        {
            get;
            set;
        }

        // Angle of the sprite around central axis
        public float angle
        {
            get;
            set;
        }

        // Rate of change of x per second
        public float dX
        {
            get;
            set;
        }

        // Rate of change of y per second
        public float dY
        {
            get;
            set;
        }

        // Rate of change of angle per second
        public float dA
        {
            get;
            set;
        }

        // Scale of the texture, where 1 is its true size
        public float scale
        {
            get;
            set;
        }

        // Constructor
        public SpriteClass(GraphicsDevice graphicsDevice, string textureName, float scale)
        {
            this.scale = scale;

            // Load the specified texture
            var stream = TitleContainer.OpenStream(textureName);
            texture = Texture2D.FromStream(graphicsDevice, stream);
        }

        // Update the position and angle of the sprite based on each rate of change
        // and the time elapsed
        public void Update(float elapsedTime)
        {
            this.x += this.dX * elapsedTime;
            this.y += this.dY * elapsedTime;
            this.angle += this.dA * elapsedTime;
        }

        // Draw the sprite with the given SpriteBatch
        public void Draw(SpriteBatch spriteBatch)
        {
            // Determine the position vector of the sprite
            Vector2 spritePosition = new Vector2(this.x, this.y);

            // Draw the sprite
            spriteBatch.Draw(texture, spritePosition, null, 
                Color.White, this.angle, 
                new Vector2(texture.Width / 2, texture.Height / 2), 
                new Vector2(scale, scale),
                SpriteEffects.None, 0f);
        }

        // Detect collision between two rectangular sprites
        public bool RectangleCollision(SpriteClass otherSprite)
        {
            if (this.x + this.texture.Width * this.scale * HITBOXSCALE / 2 
                < otherSprite.x - otherSprite.texture.Width * otherSprite.scale / 2) 
                return false;

            if (this.y + this.texture.Height * this.scale * HITBOXSCALE / 2 
                < otherSprite.y - otherSprite.texture.Height * otherSprite.scale / 2) 
                return false;

            if (this.x - this.texture.Width * this.scale * HITBOXSCALE / 2 
                > otherSprite.x + otherSprite.texture.Width * otherSprite.scale / 2) 
                return false;

            if (this.y - this.texture.Height * this.scale * HITBOXSCALE / 2 
                > otherSprite.y + otherSprite.texture.Height * otherSprite.scale / 2) 
                return false;
            return true;
        }

    }
}
