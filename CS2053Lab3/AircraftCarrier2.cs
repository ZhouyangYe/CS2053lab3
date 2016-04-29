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
    class AircraftCarrier2
    {
        int screenWidth;
        int screenHeight;

       

        Texture2D texture;
        int width = 300;
        int height = 100;
        Rectangle box;
        Vector2 velocity;
        int health = 100;
        int framecount = 0;
        int specificframe;// the frame tells when the carrier should show up;
        float vy = 1;
        float vx = 1;
        Random random = new Random();


        public AircraftCarrier2(int w, int h) { screenWidth = w; screenHeight = h; box = new Rectangle(0, 0, width, height); }

        public void setTexture(Texture2D t) { texture = t; }

        public void setLocation(Vector2 l) { box.X = (int) l.X; box.Y = (int) l.Y; }
 
        public void setVelocity(Vector2 v) { velocity = v; }

        public int getFrame() { return framecount; }
        public void reduceHealth(int reducehealth) { health = health - reducehealth; }
        public int getHealth() { return health; }
        public int getWidth() { return width; }
        public int getHeight() { return height; }
        public Rectangle getBox() { return box; }
        public void setSpecificFrame(int frameIn) { specificframe = frameIn; }
       // Random random = new Random();/*different place statement may cause bomb dropped synchronized*/
        public void loadContent()
        {
            setLocation(new Vector2(random.Next(screenWidth / 4 - 200, screenWidth * 3 / 4 - 400), -100));
        }

        public void update()
        {
            framecount++;
            if (framecount < specificframe)
            { 
            }
            else if (framecount == specificframe)
            {
  //              setLocation(new Vector2(random.Next(screenWidth / 4-200, screenWidth * 3 / 4-400), -100));
            }
            else
            {
                    
                     if (box.Y <= screenHeight / 4-100)
                        {
                                vy = 1;
                        }

                        if (box.Y >= screenHeight / 2 - 100)
                        {
                            vy = -1;
                        }

                        if (box.X <= 0)
                        {
                            vx = 1;
                        }

                        if (box.X >= screenWidth-300)
                        {
                            vx = -1;
                        }
                
                
                setVelocity(new Vector2(vx, vy));
                box.X = box.X + (int)velocity.X;
                box.Y = box.Y + (int)velocity.Y;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, box, Color.White);
        }
    }
}
