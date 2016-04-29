using System;
using System.Collections.Generic;
using System.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ScrollingBackgroundSpace;

namespace CS2053Project
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        int screenWidth;
        int screenHeight;
        int framecount = 0;

        Texture2D backgroundTexture;
        Rectangle backgroundRectangle;
        private ScrollingBackground myBackground;

        PlayerObject playerObject;
        Bonus bonus;
        List<GameObject> gameObjectList = new List<GameObject>();
        List<ProtectFighter> protectObjectList = new List<ProtectFighter>();
        List<BombObject> bombObjectList = new List<BombObject>();
        List<BomberObject> bomberObjectList = new List<BomberObject>();
        List<AircraftCarrier> aircraftcarriers = new List<AircraftCarrier>();
        GameInfoObject gameInfoObject;
        GameInfoObject2 gameInfoObject2;
        GameInfoBetween gameInfoBetween;
        List<BattleShip> battleship;
        List<DestroyerObject1> destroyerobjcet1list = new List<DestroyerObject1>();

        GameController gameController;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screenWidth = graphics.GraphicsDevice.Viewport.Width;
            screenHeight = graphics.GraphicsDevice.Viewport.Height;

            playerObject = new PlayerObject(screenWidth, screenHeight);
            aircraftcarriers = new List<AircraftCarrier>();

            bonus = new Bonus(0,0);
            gameObjectList = new List<GameObject>();
            battleship = new List<BattleShip>();
            protectObjectList = new List<ProtectFighter>();
            bombObjectList = new List<BombObject>();
            bomberObjectList = new List<BomberObject>();
            destroyerobjcet1list = new List<DestroyerObject1>();
            gameInfoObject = new GameInfoObject(screenWidth, screenHeight-30);
            gameInfoObject2 = new GameInfoObject2(40, screenHeight/2-40);
            gameInfoBetween = new GameInfoBetween(0, screenHeight/2-20);

            gameController = new GameController(screenWidth, screenHeight, playerObject, gameObjectList, bomberObjectList, gameInfoObject, bombObjectList, gameInfoObject2, gameInfoBetween, destroyerobjcet1list, aircraftcarriers, protectObjectList, battleship, bonus);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //TODO: use this.Content to load your game content here
            //Texture2D background = Content.Load<Texture2D>("starfield");
            myBackground = new ScrollingBackground();
            backgroundTexture = this.Content.Load<Texture2D>("starfield");
           // backgroundRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
            myBackground.Load(GraphicsDevice, backgroundTexture);

            gameController.setTexture(this.Content.Load<Texture2D>("zerotype"));
            gameController.setTexture2(this.Content.Load<Texture2D>("explosion"));
            gameController.setTexture3(this.Content.Load<Texture2D>("Drillexplode"));

            gameController.setProtectTexture(this.Content.Load<Texture2D>("zerotype"));
            gameController.setBomberTexture(this.Content.Load<Texture2D>("JapBomber"));
            gameController.setDestroyer1Texture(this.Content.Load<Texture2D>("destroyer1"));
            gameController.setAircraftCarrierTexture(this.Content.Load<Texture2D>("aircraftcarrier"));
            gameController.setBattleShipTexture(this.Content.Load<Texture2D>("battleship"));

            gameController.setBomberHitTexture(this.Content.Load<Texture2D>("JapBomberhit"));
            gameController.setDestroyer1HitTexture(this.Content.Load<Texture2D>("destroyer1hit"));
            gameController.setAircraftCarrierHitTexture(this.Content.Load<Texture2D>("aircraftcarrierhit"));
            gameController.setBattleShipHitTexture(this.Content.Load<Texture2D>("battleshiphit"));

            playerObject.setTexture(this.Content.Load<Texture2D>("USfighter"));
            playerObject.setVelocity(new Vector2(3f, 3f));

            bonus.setFont(this.Content.Load<SpriteFont>("SpriteFont1"));
            gameInfoObject.setFont(this.Content.Load<SpriteFont>("SpriteFont1"));
            gameInfoBetween.setFont(this.Content.Load<SpriteFont>("SpriteFont1"));
            gameInfoObject2.setFont(this.Content.Load<SpriteFont>("SpriteFont2"));

            playerObject.setBulletTexture(this.Content.Load<Texture2D>("redball"));
            playerObject.setTorpedoTexture(this.Content.Load<Texture2D>("torpedo"));

            gameController.setBombTexture(this.Content.Load<Texture2D>("whiteball"));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            playerObject.update();
            foreach (BombObject b in bombObjectList) { b.update(); }
            foreach (BattleShip bs in battleship) { bs.update(); }
            foreach (ProtectFighter fl in protectObjectList) { fl.update(); }
            foreach (AircraftCarrier ac in aircraftcarriers) { ac.update(); }
            foreach (GameObject g in gameObjectList) { g.update(); }
            foreach (BomberObject g in bomberObjectList) { g.update(); }
            
            foreach (DestroyerObject1 d1 in destroyerobjcet1list) { d1.update(); }
          //  bonus.update(gameTime);
            gameInfoObject.update(gameTime);
            gameController.control();
      /*      if (gameController.getCarrier()!=null)
                gameController.getCarrier().update();*/
            // this is for the scrolling background;
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            myBackground.Update(elapsed * 100);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            //spriteBatch.Draw(backgroundTexture, backgroundRectangle, Color.White);
            myBackground.Draw(spriteBatch);
            if (!gameInfoBetween.getFinish()) 
            {
                gameInfoBetween.draw(spriteBatch);
            }
            else if (gameInfoObject.getWIN() || gameInfoObject.getLOSE())
            {
                if (gameInfoObject.getWIN())
                {
                    gameInfoObject2.setGameInfo("Perfect fight!" + "\nSeems we can"+"\n win the war, right?");
                    gameInfoObject2.draw(spriteBatch);
                }
                else 
                { 
                    gameInfoObject2.setGameInfo("Mission Failed");
                    gameInfoObject2.draw(spriteBatch);
                }
                
            }
            else
            {
                if (bonus.getShow())
                {
                        bonus.draw(spriteBatch);
                        framecount++;
                }
                if (bonus.getShow() && framecount == 30)
                {
                    framecount = 0;
                    bonus.setShow(false);
                }
                foreach (DestroyerObject1 b in destroyerobjcet1list)
                {
                    b.draw(spriteBatch);
                }
                foreach (BomberObject b in bomberObjectList)
                {
                    b.draw(spriteBatch);
                }
                foreach (BombObject b in bombObjectList)
                {
                    b.draw(spriteBatch);
                }
                foreach (AircraftCarrier ac in aircraftcarriers)
                {
                    ac.draw(spriteBatch);
                }
                foreach (ProtectFighter fl in protectObjectList)
                {
                    fl.draw(spriteBatch);
                }
                foreach (BattleShip bs in battleship)
                {
                    bs.draw(spriteBatch);
                }
                playerObject.draw(spriteBatch);
                foreach (GameObject g in gameObjectList)
                {
                    g.draw(spriteBatch);
                }
                gameInfoObject.draw(spriteBatch);
            }
                spriteBatch.End();
                base.Draw(gameTime);
            
        }
    }
}
