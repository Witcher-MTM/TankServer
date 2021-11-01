using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using TankDLL;

namespace Client_Graphic
{
    public class Sprite
    {


        public Texture2D TankTexture { get; set; }
        public Texture2D BulletTexture { get; set; }
        public Tank tank { get; set; }
        public Sprite(Texture2D textureT, Tank tank, Texture2D textureB, Bullet bullet)
        {
            this.TankTexture = textureT;
            this.tank = tank;
            this.BulletTexture = textureB;
            this.tank.bullet = bullet;
        }


    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Client client = new Client();
        private List<Sprite> TankSpriteList;
        private Sprite TankSprite;
        private bool KeyPressed;
        private List<Tank> tanks;
      

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            KeyPressed = false;
            this.tanks = new List<Tank>();
            this.TankSpriteList = new List<Sprite>();

        }

        protected override void Initialize()
        {
            client.Connect();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            TankSprite = new Sprite(Content.Load<Texture2D>(@"Texure\tank"), new Tank(), Content.Load<Texture2D>(@"Texure\bullet"), new Bullet());
            client.SendInfo(TankSprite.tank);
        }


        int check = 0;

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            TankSpriteList.Clear();
            Window.Title = $"Send {check}";

            try
            {
                tanks = JsonSerializer.Deserialize<List<Tank>>(client.GetInfo().ToString());
            }
            catch (System.Exception)
            {
            }

            for (int i = 0; i < tanks.Count; i++)
            {
                TankSpriteList.Add(new Sprite(Content.Load<Texture2D>(@"Texure\tank"), tanks[i], Content.Load<Texture2D>(@"Texure\bullet"), tanks[i].bullet));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.W) && KeyPressed == false)
            {
                check++;
                TankSprite.tank.Rotation = 0f;
                if (TankSprite.tank.Y - (TankSprite.tank.Speed + TankSprite.TankTexture.Height/2) > 0)
                {
                    TankSprite.tank.Y -= TankSprite.tank.Speed;
                    KeyPressed = true;
                    client.SendInfo(TankSprite.tank);
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A) && KeyPressed == false)
            {
                check++;
                TankSprite.tank.Rotation = -7.85f;
                if (TankSprite.tank.X - (TankSprite.tank.Speed + TankSprite.TankTexture.Height/2) > 0) {
                    TankSprite.tank.X -= TankSprite.tank.Speed;
                    KeyPressed = true;
                }
                client.SendInfo(TankSprite.tank);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) && KeyPressed == false)
            {
                check++;
                TankSprite.tank.Rotation = 7.85f;
                if (TankSprite.tank.X+(TankSprite.tank.Speed + TankSprite.TankTexture.Height/2) <_graphics.PreferredBackBufferWidth)
                {
                    TankSprite.tank.X += TankSprite.tank.Speed;
                    KeyPressed = true;
                }
                client.SendInfo(TankSprite.tank);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) && KeyPressed == false)
            {
                check++;
                TankSprite.tank.Rotation = 15.7f;
                if (TankSprite.tank.Y+(TankSprite.tank.Speed+TankSprite.TankTexture.Height/2) <= _graphics.PreferredBackBufferHeight)
                {
                    TankSprite.tank.Y += TankSprite.tank.Speed;
                    KeyPressed = true;
                }
                client.SendInfo(TankSprite.tank);
            }
            Shoot();
            Boost();

            Task.Factory.StartNew(() => {
                while (CoulDown())
                {
                    Thread.Sleep(20);
                }
            });
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
        private void Shoot()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space)&&TankSprite.tank.CD == 0)
            {
                TankSprite.tank.CD = 80;
                TankSprite.tank.bullet.CoordY = TankSprite.tank.Y;
                TankSprite.tank.bullet.CoordX = TankSprite.tank.X;
                TankSprite.tank.bullet.Rotation = TankSprite.tank.Rotation;

                Task.Factory.StartNew(() =>
                {

                    if (TankSprite.tank.Rotation == 0f)
                    {
                        while (TankSprite.tank.bullet.CoordY > 0)
                        {
                            Thread.Sleep(10);
                            TankSprite.tank.bullet.CoordY -= TankSprite.tank.bullet.Speed;
                            client.SendInfo(TankSprite.tank);

                            if (BulletCollision())
                            {
                                break;
                            }
                        }
                        TankSprite.tank.bullet.CoordX = -10;
                        TankSprite.tank.bullet.CoordY = -10;
                        client.SendInfo(TankSprite.tank);
                    }
                    else if (TankSprite.tank.Rotation == 15.7f)
                    {
                        while (TankSprite.tank.bullet.CoordY < _graphics.PreferredBackBufferHeight)
                        {
                            Thread.Sleep(10);
                            TankSprite.tank.bullet.CoordY += TankSprite.tank.bullet.Speed;
                            client.SendInfo(TankSprite.tank);
                            if (BulletCollision())
                            {
                                break;
                            }
                        }
                        TankSprite.tank.bullet.CoordX = -10;
                        TankSprite.tank.bullet.CoordY = -10;
                        client.SendInfo(TankSprite.tank);
                      
                    }

                    else if (TankSprite.tank.Rotation == -7.85f)
                    {
                        while (TankSprite.tank.bullet.CoordX > 0)
                        {
                            Thread.Sleep(10);
                            TankSprite.tank.bullet.CoordX -= TankSprite.tank.bullet.Speed;
                            client.SendInfo(TankSprite.tank);
                            if (BulletCollision())
                            {

                                break;
                            }
                           
                        }
                        TankSprite.tank.bullet.CoordX = -10;
                        TankSprite.tank.bullet.CoordY = -10;
                        client.SendInfo(TankSprite.tank);
                    }
                    else if (TankSprite.tank.Rotation == 7.85f)
                    {
                        while (TankSprite.tank.bullet.CoordX < _graphics.PreferredBackBufferWidth)
                        {
                            Thread.Sleep(10);
                            TankSprite.tank.bullet.CoordX += TankSprite.tank.bullet.Speed;
                            client.SendInfo(TankSprite.tank);
                            if (BulletCollision())
                            {
                                break;
                            }
                        }
                        TankSprite.tank.bullet.CoordX = -10;
                        TankSprite.tank.bullet.CoordY = -10;
                        client.SendInfo(TankSprite.tank);
                    }


                });
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            foreach (var item in TankSpriteList)
            {
                _spriteBatch.Draw(item.TankTexture, new Rectangle(item.tank.X, item.tank.Y, item.TankTexture.Width, item.TankTexture.Height), null, new Color(item.tank.Color[0], item.tank.Color[1], item.tank.Color[2]), item.tank.Rotation, new Vector2(item.TankTexture.Width / 2f, item.TankTexture.Height / 2f), SpriteEffects.None, 0f);
                
              
              _spriteBatch.Draw(item.BulletTexture, new Rectangle(item.tank.bullet.CoordX, item.tank.bullet.CoordY, 20, 20), null, Color.White, item.tank.bullet.Rotation, new Vector2(item.BulletTexture.Width / 2f, item.BulletTexture.Height / 2f), SpriteEffects.None, 0f);

                
                

            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }


        private bool BulletCollision()
        {
            Rectangle bullet = new Rectangle(TankSprite.tank.bullet.CoordX, TankSprite.tank.bullet.CoordY, 20, 20);

            foreach (var item in TankSpriteList)
            {
                if (item.tank.X != TankSprite.tank.X && item.tank.Y != TankSprite.tank.Y)
                {
                   
                    if (bullet.Intersects(new Rectangle(item.tank.X, item.tank.Y, item.TankTexture.Width, item.TankTexture.Height)))
                    {
                        item.tank.HP -= item.tank.bullet.Damage;
                        item.tank.bullet.CoordX = -10;
                        item.tank.bullet.CoordY = -10;
                        return true;
                    }
                }
            }
            return false;
        }
        private bool CoulDown()
        {
            bool check = false;
            if (TankSprite.tank.CD > 0)
            {
                TankSprite.tank.CD--;
                check = false;
            }
            else
            {
                check = true;
            }
            return check;
            
        }
    }
}
