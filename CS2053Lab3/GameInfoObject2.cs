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
    class GameInfoObject2
    {
        int screenWidth;
        int screenHeight;
        String gameInfo;
        SpriteFont font;
        Vector2 location;

        public GameInfoObject2(int w, int h) { screenWidth = w; screenHeight = h; location = new Vector2(screenWidth/2, screenHeight/2); }
        public void setFont(SpriteFont t) { font = t; }
        public void setGameInfo(String info) { gameInfo = info; }
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, gameInfo, location, Color.Orange);
        }
    }
   
}
