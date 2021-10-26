using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading;
using TankDLL;

namespace Client_Graphic
{
    public class Sprite {


        public Texture2D texture { get; set; }
        public Tank tank { get; set; }
        public Sprite(Texture2D texture, Tank tank)
        {
            this.texture = texture;
            this.tank = tank;
        }

    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Client client = new Client();
        private Sprite TankSprite;
        private bool KeyPressed;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            KeyPressed = false;
        }

        protected override void Initialize()
        {

           
            client.Connect();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            TankSprite = new Sprite(Content.Load<Texture2D>(@"Texure\tank"), new Tank());
          
        }


        int check = 0;

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Boost();
            if (Keyboard.GetState().IsKeyDown(Keys.W)&& KeyPressed == false)
            {
                check++;
                TankSprite.tank.Y -= TankSprite.tank.Speed;
                TankSprite.tank.Rotation = 0f;
                KeyPressed = true;
                client.SendInfo(TankSprite.tank);
               

            }
            if (Keyboard.GetState().IsKeyDown(Keys.A)&& KeyPressed == false)
            {
                check++;
                TankSprite.tank.X -= TankSprite.tank.Speed;
                TankSprite.tank.Rotation = -7.85f;
                KeyPressed = true;
                client.SendInfo(TankSprite.tank);

            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) && KeyPressed == false)
            {
                check++;
                TankSprite.tank.X += TankSprite.tank.Speed;
                TankSprite.tank.Rotation = 7.85f;
                KeyPressed = true;
                client.SendInfo(TankSprite.tank);

            }
            if (Keyboard.GetState().IsKeyDown(Keys.S)&& KeyPressed == false)
            {
                check++;
                TankSprite.tank.Y += TankSprite.tank.Speed;
                TankSprite.tank.Rotation = 15.7f;
                KeyPressed = true;
                client.SendInfo(TankSprite.tank);

            }
           
            Window.Title = $"Send {check}";
            Thread.Sleep(10);
            KeyPressed = false;
            base.Update(gameTime);
        }
        private void Boost()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
            {
                TankSprite.tank.Speed = 6;
            }
            else
            {
                TankSprite.tank.Speed = 3;
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(TankSprite.texture, new Rectangle(TankSprite.tank.X, TankSprite.tank.Y, TankSprite.texture.Width, TankSprite.texture.Height), null,new Color(TankSprite.tank.Color[0], TankSprite.tank.Color[1], TankSprite.tank.Color[2]), TankSprite.tank.Rotation, new Vector2(TankSprite.texture.Width / 2f, TankSprite.texture.Height / 2f), SpriteEffects.None, 0f);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
