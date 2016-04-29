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
    class PlayerObject
    {
        int screenWidth;
        int screenHeight;

        Texture2D texture;
        int width = 50;
        int height = 50;
        int level =1;
        Rectangle box;
        Vector2 velocity;

        float moveVelocity = 2f;

        Random random = new Random();

        List<BulletObject> bulletObjectList = new List<BulletObject>();
        List<Torpedo> torpedoList = new List<Torpedo>();
        Texture2D bulletTexture;
        Texture2D bulletTexture2;
        Texture2D torpedoTexture;
        Boolean fireKeyDown = false;
        Boolean fireTorpedoDown = false;
        Boolean rotateKeyDown = false;
        Boolean jumpKeydown = false;
        Boolean canFire = true;
        Boolean hit = false;
        int torpedoNum = 10;

        public PlayerObject(int w, int h) 
        { 
            screenWidth = w; screenHeight = h; 
            box = new Rectangle(screenWidth/2-width/2, screenHeight-height, width, height);
        }

        public void setTexture(Texture2D t) { texture = t; }

        public void passlevel() { level++; }

        public void addTorpedo()
        {
            torpedoNum++;
            if (torpedoNum>=10)
            {
                torpedoNum = 10;
            }
        }
        public void sethit(Boolean hitIn) { hit = hitIn; }

        public Boolean gethit() { return hit; }

        public void setVelocity(Vector2 v) { velocity = v; }

        public void setStopFire(Boolean input) { canFire = input; }

        public Rectangle getBox() { return box; }

        public void reduceTorpedo() 
        {
            torpedoNum--;
            if (torpedoNum <= 0)
            {
                torpedoNum = 0;
            }
        }

        public void setBox() { box = new Rectangle(screenWidth / 2 - width / 2, screenHeight - height, width, height);}//in new level, set the player to the default location;

        public void setBulletTexture(Texture2D bt) { bulletTexture = bt; }
        public void setBulletTexture2(Texture2D bt) { bulletTexture2 = bt; }
        public List<BulletObject> getBulletObjectList() { return bulletObjectList; }
        public void setTorpedoTexture(Texture2D t) { torpedoTexture = t; }
        public List<Torpedo> getTorpedoList() { return torpedoList; }

        public void update()
        {
            KeyboardState kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Left))
            {
                if (box.X >= 0)              
                {
                    box.X = box.X - (int)moveVelocity;
                }
            }
            if (kstate.IsKeyDown(Keys.Right))
            {
                if (box.X + width <= screenWidth)
                {
                    box.X = box.X + (int)moveVelocity;
                }
            }
            if (kstate.IsKeyDown(Keys.Up))
            {
                if (box.Y >= 0)
                {
                    box.Y = box.Y - (int)moveVelocity;
                }
            }
            if (kstate.IsKeyDown(Keys.Down))
            {
                if (box.Y + height<= screenHeight)
                {
                    box.Y = box.Y + (int)moveVelocity;
                }
            }

            if (kstate.IsKeyUp(Keys.F) && fireKeyDown)
            {
              //  if (canFire)
              //  {
                    BulletObject bullet1 = new BulletObject();
                    bullet1.setTexture(bulletTexture);
                    Vector2 origin1 = new Vector2(texture.Width / 2, texture.Height / 2);
                    bullet1.setLocation(new Vector2(box.X + width / 4, box.Y + height / 2));
                    Vector2 bulletVelocity1 = new Vector2(0f,-10f) ;//10 * (float)Math.Cos(rotationAngle), 10 * (float)Math.Sin(rotationAngle));
                    bullet1.setVelocity(bulletVelocity1);
                    bulletObjectList.Add(bullet1);

                    BulletObject bullet2 = new BulletObject();
                    bullet2.setTexture(bulletTexture);
                    Vector2 origin2 = new Vector2(texture.Width / 2, texture.Height / 2);
                    bullet2.setLocation(new Vector2(box.X + width*3 / 4, box.Y + height / 2));
                    Vector2 bulletVelocity2 = new Vector2(0f, -10f);//10 * (float)Math.Cos(rotationAngle), 10 * (float)Math.Sin(rotationAngle));
                    bullet2.setVelocity(bulletVelocity2);
                    bulletObjectList.Add(bullet2);
              //  }
                fireKeyDown = false;
            }
            if (kstate.IsKeyDown(Keys.F))
            {
                fireKeyDown = true;
            }


            if (level > 1 && torpedoNum > 0)
            {
                if (kstate.IsKeyUp(Keys.D) && fireTorpedoDown)
                {
                    Torpedo torpedo = new Torpedo();
                    torpedo.setTexture(torpedoTexture);
                    Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
                    torpedo.setLocation(new Vector2(box.X + width / 2, box.Y + height / 2));
                    Vector2 torpedoVelocity = new Vector2(0f, -3f);
                    torpedo.setVelocity(torpedoVelocity);
                    torpedoList.Add(torpedo);
                    fireTorpedoDown = false;
                }
                if (kstate.IsKeyDown(Keys.D))
                {
                    fireTorpedoDown = true;
                }
            }
            List<BulletObject> removeList = new List<BulletObject>();
            List<Torpedo> removeTorpedoList = new List<Torpedo>();
            foreach (BulletObject b in bulletObjectList)
            {
                Rectangle bulletBox = b.getBox();
                if (bulletBox.X < 0 || bulletBox.X > screenWidth || bulletBox.Y < 0 || bulletBox.Y > screenHeight)
                { removeList.Add(b); }
                else { b.update(); }
            }
            foreach (BulletObject b in removeList)
            { bulletObjectList.Remove(b); }

            foreach (Torpedo t in torpedoList)
            {
                Rectangle torpedoBox = t.getBox();
                if (torpedoBox.X < 0 || torpedoBox.X > screenWidth || torpedoBox.Y < 0 || torpedoBox.Y > screenHeight)
                { removeTorpedoList.Add(t); }
                else { t.update(); }
            }
            foreach (Torpedo t in removeTorpedoList)
            { torpedoList.Remove(t); }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            Vector2 position = new Vector2(box.X + width/2, box.Y + height/2);
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            Vector2 scale = new Vector2((float)width / texture.Width, (float)height / texture.Height);
            float levelDepth = 0f;
            spriteBatch.Draw(texture, position,null,Color.White,0f, origin, scale, SpriteEffects.None, levelDepth);

            foreach (BulletObject b in bulletObjectList) { b.draw(spriteBatch); }
            if (level > 1)
            {
                foreach (Torpedo t in torpedoList) { t.draw(spriteBatch); }
            }
        }
    }
}