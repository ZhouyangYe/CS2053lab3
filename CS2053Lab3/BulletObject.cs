using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CS2053Project
{
    class BulletObject
    {
        Texture2D texture;
        Texture2D texture2;
        int width = 5;
        int height = 5;
        int frameCount = 0;
        Rectangle box;
        Vector2 velocity;
        Boolean collide = false;

        public void setTexture(Texture2D t) { texture = t; box = new Rectangle(0, 0, width, height); }

        public void setLocation(Vector2 l) { box.X = (int)l.X; box.Y = (int)l.Y; }

        public void setTexture2(Texture2D t) { texture2 = t; }

        public int getFrame() { return frameCount; }

        public void setCollide(Boolean b) { collide = b; }

        public void setVelocity(Vector2 v) { velocity = v; }

        public Rectangle getBox() { return box; }

        public void update()
        {
            box.X = box.X + (int)velocity.X;
            box.Y = box.Y + (int)velocity.Y;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            spriteBatch.Draw(texture, box, null, Color.White, 0f, origin, SpriteEffects.None, 0f);
        }
    }
}