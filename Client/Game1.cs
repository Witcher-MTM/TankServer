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
                   
                }
                client.SendInfo(TankSprite.tank);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A) && KeyPressed == false)
            {
                check++;
                TankSprite.tank.Rotation = -7.85f;
                if (TankSprite.tank.X - (TankSprite.tank.Speed + TankSprite.TankTexture.Height / 2) > 0)
                {
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
           
            Boost();

            BulletMove();
            KeyPressed = false;
            base.Update(gameTime);
           
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
            Rectangle tank_for_intersect = new Rectangle(TankSprite.tank.X, TankSprite.tank.Y, TankSprite.TankTexture.Width, TankSprite.TankTexture.Height);
            for (int i = 0; i < TankSpriteList.Count; i++)
            {
                try
                {
                    if (tank_for_intersect.Intersects(new Rectangle(TankSpriteList[i].tank.bullet.CoordX, TankSpriteList[i].tank.bullet.CoordY, 20, 20)))
                    {
                        TankSprite.tank.HP -= TankSpriteList[i].tank.bullet.Damage;
                        TankSpriteList[i].tank.bullet.CoordX = -10;
                        TankSpriteList[i].tank.bullet.CoordY = -10;
                        client.SendInfo(TankSprite.tank);
                    }
                }
                catch (System.Exception)
                {
                }
               
            }
            

            
            return false;
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
        private void BulletMove()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space)&&TankSprite.tank.CD==0)
            {
                TankSprite.tank.bullet.CoordY = TankSprite.tank.Y;
                TankSprite.tank.bullet.CoordX = TankSprite.tank.X;
                TankSprite.tank.bullet.Rotation = TankSprite.tank.Rotation;
                if(TankSprite.tank.bullet.Rotation == 0f)
                {
                    TankSprite.tank.bullet.CoordY -= 50;
                }
                else if(TankSprite.tank.bullet.Rotation == 15.7f)
                {
                    TankSprite.tank.bullet.CoordY += 50;
                }
                else if (TankSprite.tank.bullet.Rotation == -7.85f)
                {
                    TankSprite.tank.bullet.CoordX -= 50;
                }
                else if (TankSprite.tank.bullet.Rotation == 7.85f)
                {
                    TankSprite.tank.bullet.CoordX += 50;
                }
                TankSprite.tank.bullet.IsActive = true;
                TankSprite.tank.CD = 120;
            }
            if (TankSprite.tank.bullet.IsActive)
            {
                if (TankSprite.tank.bullet.Rotation == 0f)
                {
                    if (TankSprite.tank.bullet.CoordY >= -10 && !BulletCollision())
                    {
                        TankSprite.tank.bullet.CoordY -= TankSprite.tank.bullet.Speed;
                    }
                    else
                        TankSprite.tank.bullet.IsActive = false;
                    
                }
                else if (TankSprite.tank.bullet.Rotation == 15.7f)
                {
                    if (TankSprite.tank.bullet.CoordY <= _graphics.PreferredBackBufferHeight + 10 && !BulletCollision())
                    {
                        TankSprite.tank.bullet.CoordY += TankSprite.tank.bullet.Speed;
                    }
                    else
                        TankSprite.tank.bullet.IsActive = false;
                   
                }
                else if (TankSprite.tank.bullet.Rotation == -7.85f)
                {
                    if (TankSprite.tank.bullet.CoordX >= -10 && !BulletCollision())
                    {
                        TankSprite.tank.bullet.CoordX -= TankSprite.tank.bullet.Speed;
                    }
                    else
                        TankSprite.tank.bullet.IsActive = false;
                   
                }
                else if (TankSprite.tank.bullet.Rotation == 7.85f)
                {
                    if (TankSprite.tank.bullet.CoordX <= _graphics.PreferredBackBufferWidth + 10 && !BulletCollision())
                    {
                        TankSprite.tank.bullet.CoordX += TankSprite.tank.bullet.Speed;
                    }
                    else
                        TankSprite.tank.bullet.IsActive = false;
                   
                }
                client.SendInfo(TankSprite.tank);
            }
            if (TankSprite.tank.CD > 0)
            {
                TankSprite.tank.CD--;
            }
          
        }
    }
}
