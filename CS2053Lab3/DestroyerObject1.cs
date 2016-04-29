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
    class DestroyerObject1
    {
        int screenWidth;
        int screenHeight;
        Boolean count = false;
        int frameCounter = 0;


        Texture2D texture, texture2, blink;
        int width = 200;
        int height = 70;
        Rectangle box;
        Vector2 velocity;
        int health = 200;
        Boolean destroyed = false;
        Boolean hit = false;

        public Boolean getHit() { return hit; }

        public void setHit(Boolean hitIn) { hit = hitIn; }

        public void setDestroy(Boolean destroyIn) { destroyed = destroyIn; }

        public Boolean getDestroy() { return destroyed; }
        public DestroyerObject1(int w, int h) { screenWidth = w; screenHeight = h; box = new Rectangle(0, 0, width, height); }

        public void reduceHealth(int reduce) { health = health - reduce; }
        public int getHealth() { return health; }
        public void setTexture(Texture2D t) { texture = t; }

        public void setCount(Boolean c) { count = c; }

        public void setTexture2(Texture2D t) { texture2 = t; }

        public void setTexture3(Texture2D t) { blink = t; }

        public void setFrameCounter(int f) { frameCounter = f; }

        public int getFrameCounter() { return frameCounter; }

        public void setLocation(Vector2 l) { box.X = (int)l.X; box.Y = (int)l.Y; }

        public void setVelocity(Vector2 v) { velocity = v; }

        public int getWidth() { return width; }
        public int getHeight() { return height; }

        public Rectangle getBox() { return box; }
        // Random random = new Random();/*different place statement may cause bomb dropped synchronized*/
        public void update()
        {
            if (count && frameCounter % 12 == 0)
            {
                texture = texture2;

            }

            if (count && (frameCounter + 6) % 10 == 0)
            {
                texture = blink;

            }
            box.X = box.X + (int)velocity.X;
            box.Y = box.Y + (int)velocity.Y;
            if (count)
            {
                frameCounter++;
                //frameCounter2++;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, box, Color.White);
        }
    }
}
