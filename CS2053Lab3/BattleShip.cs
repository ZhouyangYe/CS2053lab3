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
    class BattleShip
    {
        int screenWidth;
        int screenHeight;
        Boolean count = false;
        int frameCounter = 0;


        Texture2D texture, texture2, blink;
        int width = 400;
        int height = 100;
        Rectangle box;
        Vector2 velocity;
        int health = 400;
        int framecount = 0;
        Boolean sink = false;
        Boolean destroy = false;

        Random random = new Random();
        Boolean hit = false;

        public Boolean getHit() { return hit; }

        public void setHit(Boolean hitIn) { hit = hitIn; }

        public void setDestroy(Boolean destroyIn) { destroy = destroyIn; }

        public Boolean getDestroy() { return destroy; }

        public BattleShip(int w, int h) { screenWidth = w; screenHeight = h; box = new Rectangle(0, 0, width, height); }

        public void setTexture(Texture2D t) { texture = t; }
        public void setCount(Boolean c) { count = c; }

        public void setTexture2(Texture2D t) { texture2 = t; }

        public void setTexture3(Texture2D t) { blink = t; }

        public void setFrameCounter(int f) { frameCounter = f; }

        public int getFrameCounter() { return frameCounter; }

        public void setLocation(Vector2 l) { box.X = (int)l.X; box.Y = (int)l.Y; }

        public void setVelocity(Vector2 v) { velocity = v; }

        public int getFrame() { return framecount; }
        public void reduceHealth(int reducehealth) 
        { 
            health = health - reducehealth;
            if (health <= 0)
            {
                health = 0;
                sink = true;
            }
        }
        public void setSink(Boolean sinkIn)
        {
            sink = sinkIn;
        }
        public Boolean getSink()
        {
            return sink;
        }
        public int getHealth() { return health; }
        public int getWidth() { return width; }
        public int getHeight() { return height; }
        public Rectangle getBox() { return box; }
        // Random random = new Random();/*different place statement may cause bomb dropped synchronized*/
        public void loadContent()
        {
            setLocation(new Vector2(random.Next(screenWidth / 4 - 200, screenWidth * 3 / 4 - 400), -100));
        }

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
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, box, Color.White);
        }
    }
}
