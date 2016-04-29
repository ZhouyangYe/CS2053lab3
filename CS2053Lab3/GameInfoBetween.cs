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
    class GameInfoBetween
    {
        int screenWidth;
        int screenHeight;
        String gameInfo;
        SpriteFont font;
        Vector2 location;
        Boolean finish = false;
        int level = 1;

        public GameInfoBetween(int w, int h) { screenWidth = w; screenHeight = h; location = new Vector2(screenWidth/2, screenHeight/2); }
        public void setFont(SpriteFont t) { font = t; }
        public void setGameInfo(String info) { gameInfo = info; }
        public Boolean getFinish() //if the playe wanna watch the information page between levels; 
        {
            return finish;
        }
        public void setFinish(Boolean finishIn)
        {
            finish = finishIn;
        }

        public void setLevel(int levelIn)
        {
            level = levelIn;
        }

        public int getLevel()
        {
            return level;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, gameInfo, location, Color.Orange);
        }
    }
   
}
