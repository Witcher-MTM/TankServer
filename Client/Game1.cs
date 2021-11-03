using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
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
        public Texture2D MapTexture { get; set; }
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
        private Texture2D wall;
        private Rectangle tank;
        private Rectangle bullet_rectangle;
        private SpriteFont tankHP;

        public static class Map
        {
            public static char[,] IntMap { set; get; }
            public static Wall[,] WallMap { set; get; }

            static Map()
            {
                IntMap = new char[16, 10]{
                    {'X','X','X','X','X','X','X','X','X','X'},
                    {'X',' ',' ',' ',' ',' ',' ',' ',' ','X'},
                    {'X',' ',' ',' ',' ',' ',' ',' ',' ','X'},
                    {'X',' ',' ',' ',' ',' ',' ',' ',' ','X'},
                    {'X',' ',' ',' ',' ',' ',' ',' ',' ','X'},
                    {'X',' ',' ',' ',' ',' ',' ',' ',' ','X'},
                    {'X',' ',' ',' ',' ',' ',' ',' ',' ','X'},
                    {'X',' ',' ',' ',' ',' ',' ',' ',' ','X'},
                    {'X',' ',' ',' ',' ',' ',' ',' ',' ','X'},
                    {'X',' ',' ',' ',' ',' ',' ',' ',' ','X'},
                    {'X',' ',' ',' ',' ',' ',' ',' ',' ','X'},
                    {'X',' ',' ',' ',' ',' ',' ',' ',' ','X'},
                    {'X',' ',' ',' ',' ',' ',' ',' ',' ','X'},
                    {'X',' ',' ',' ',' ',' ',' ',' ',' ','X'},
                    {'X',' ',' ',' ',' ',' ',' ',' ',' ','X'},
                    {'X','X','X','X','X','X','X','X','X','X'},
                };
                WallMap = new Wall[16, 10];
                for (int i = 0; i < IntMap.GetLength(0); i++)
                {
                    for (int j = 0; j < IntMap.GetLength(1); j++)
                    {
                        WallMap[i, j] = new Wall(new Rectangle(i * 50, j * 50, 50, 50), IntMap[i, j] == 'X' ? true : false);
                    }
                }
            }
        }
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
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();
            client.Connect();
            base.Initialize();
        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            TankSprite = new Sprite(Content.Load<Texture2D>(@"Texure\tank"), new Tank(), Content.Load<Texture2D>(@"Texure\bullet"), new Bullet());
            tankHP = Content.Load<SpriteFont>(@"LabelInfo\TankHP");
            wall = Content.Load<Texture2D>(@"Texure\wall");
            Rectangle tank = new Rectangle(TankSprite.tank.X, TankSprite.tank.Y, TankSprite.TankTexture.Width, TankSprite.TankTexture.Height);
            Rectangle bullet_rectangle = new Rectangle(TankSprite.tank.bullet.CoordX, TankSprite.tank.bullet.CoordY, 20, 20);
            client.SendInfo(TankSprite.tank);
        }


        int check = 0;

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            TankSpriteList.Clear();

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
                TankSprite.tank.tankDirection = Direction.UP;
                check++;
                TankSprite.tank.Rotation = 0f;
                if (TankInterMap() && TankInterTank())
                {
                    TankSprite.tank.Y -= TankSprite.tank.Speed;
                    KeyPressed = true;
                }


                client.SendInfo(TankSprite.tank);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A) && KeyPressed == false)
            {
                TankSprite.tank.tankDirection = Direction.LEFT;
                check++;
                TankSprite.tank.Rotation = -7.85f;
                if (TankInterMap() && TankInterTank())
                {
                    TankSprite.tank.X -= TankSprite.tank.Speed;
                    KeyPressed = true;
                }

                client.SendInfo(TankSprite.tank);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) && KeyPressed == false)
            {
                TankSprite.tank.tankDirection = Direction.RIGHT;
                check++;
                TankSprite.tank.Rotation = 7.85f;
                if (TankInterMap() && TankInterTank())
                {
                    TankSprite.tank.X += TankSprite.tank.Speed;
                    KeyPressed = true;
                }

                client.SendInfo(TankSprite.tank);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) && KeyPressed == false)
            {
                TankSprite.tank.tankDirection = Direction.DOWN;
                check++;
                TankSprite.tank.Rotation = 15.7f;
                if (TankInterMap() && TankInterTank())
                {
                    TankSprite.tank.Y += TankSprite.tank.Speed;
                    KeyPressed = true;
                }

                client.SendInfo(TankSprite.tank);

            }

            Boost();
            if (TankSprite.tank.TankID == 0)
            {
                if (TankSpriteList.Count > 0)
                {
                    TankSprite.tank.TankID = TankSpriteList.Count;
                    client.SendInfo(TankSprite.tank);
                }
            }
            BulletMove();

            KeyPressed = false;
            base.Update(gameTime);

        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            drawWalls();
            foreach (var item in TankSpriteList)
            {
                _spriteBatch.Draw(item.TankTexture, new Rectangle(item.tank.X, item.tank.Y, item.TankTexture.Width, item.TankTexture.Height), null, new Color(item.tank.Color[0], item.tank.Color[1], item.tank.Color[2]), item.tank.Rotation, new Vector2(item.TankTexture.Width / 2f, item.TankTexture.Height / 2f), SpriteEffects.None, 0f);
                if (item.tank.bullet.IsActive)
                {
                    _spriteBatch.Draw(item.BulletTexture, new Rectangle(item.tank.bullet.CoordX, item.tank.bullet.CoordY, 20, 20), null, Color.White, item.tank.bullet.Rotation, new Vector2(item.BulletTexture.Width / 2f, item.BulletTexture.Height / 2f), SpriteEffects.None, 0f);
                }
            }
            _spriteBatch.DrawString(tankHP, $"{TankSprite.tank.HP}", new Vector2(TankSprite.tank.X-20, TankSprite.tank.Y + 25), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
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
            BulletInter();
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && TankSprite.tank.CD == 0)
            {
                TankSprite.tank.bullet.CoordY = TankSprite.tank.Y;
                TankSprite.tank.bullet.CoordX = TankSprite.tank.X;
                TankSprite.tank.bullet.Rotation = TankSprite.tank.Rotation;
                CheckDirectionBullet();
                TankSprite.tank.bullet.IsActive = true;
                TankSprite.tank.CD = 120;
            }
            if (TankSprite.tank.bullet.IsActive)
            {
                if (TankSprite.tank.bullet.Rotation == 0f)
                {
                    if (TankSprite.tank.bullet.CoordY >= -10)
                    {
                        TankSprite.tank.bullet.CoordY -= TankSprite.tank.bullet.Speed;
                    }
                    else
                        TankSprite.tank.bullet.IsActive = false;
                }
                else if (TankSprite.tank.bullet.Rotation == 15.7f)
                {
                    if (TankSprite.tank.bullet.CoordY <= _graphics.PreferredBackBufferHeight + 10)
                    {
                        TankSprite.tank.bullet.CoordY += TankSprite.tank.bullet.Speed;
                    }
                    else
                        TankSprite.tank.bullet.IsActive = false;
                }
                else if (TankSprite.tank.bullet.Rotation == -7.85f)
                {
                    if (TankSprite.tank.bullet.CoordX >= -10)
                    {
                        TankSprite.tank.bullet.CoordX -= TankSprite.tank.bullet.Speed;
                    }
                    else
                        TankSprite.tank.bullet.IsActive = false;
                }
                else if (TankSprite.tank.bullet.Rotation == 7.85f)
                {
                    if (TankSprite.tank.bullet.CoordX <= _graphics.PreferredBackBufferWidth + 10)
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
        private void drawWalls()
        {
            for (int i = 0; i < Map.IntMap.GetLength(0); i++)
            {
                for (int j = 0; j < Map.IntMap.GetLength(1); j++)
                {
                    if (Map.WallMap[i, j].IsActive == true)
                        _spriteBatch.Draw(wall, new Vector2(Map.WallMap[i, j].rec.X, Map.WallMap[i, j].rec.Y), Color.Bisque);
                }
            }
        }
        private void BulletInter()
        {
            bullet_rectangle = new Rectangle(TankSprite.tank.bullet.CoordX, TankSprite.tank.bullet.CoordY, 20, 20);
            for (int i = 0; i < Map.IntMap.GetLength(0); i++)
            {
                for (int j = 0; j < Map.IntMap.GetLength(1); j++)
                {
                    if (Map.WallMap[i, j].IsActive == true)
                    {
                        if (bullet_rectangle.Intersects(Map.WallMap[i, j].rec))
                        {
                            TankSprite.tank.bullet.CoordX = 2021;
                            TankSprite.tank.bullet.CoordY = 2021;
                            client.SendInfo(TankSprite.tank);
                            TankSprite.tank.bullet.IsActive = false;
                        }
                    }
                }
            }
            foreach (var item in TankSpriteList)
            {
                if (TankSprite.tank.TankID != item.tank.TankID)
                {
                    if (tank.Intersects(new Rectangle(item.tank.bullet.CoordX, item.tank.bullet.CoordY, 20, 20)))
                    {
                        TankSprite.tank.HP -= item.tank.bullet.Damage;
                        CheckStatus();
                        client.SendInfo(TankSprite.tank);
                    }
                }
            }
            foreach (var item_2 in tanks)
            {
                if (TankSprite.tank.TankID != item_2.TankID)
                {
                    if (bullet_rectangle.Intersects(new Rectangle(item_2.X, item_2.Y, TankSprite.TankTexture.Width, TankSprite.TankTexture.Height)))
                    {
                        TankSprite.tank.bullet.CoordX = 2021;
                        TankSprite.tank.bullet.CoordY = 2021;
                        TankSprite.tank.bullet.IsActive = false;
                        client.SendInfo(TankSprite.tank);
                    }
                }
            }

        }

        private bool TankInterTank()
        {
            foreach (var item in TankSpriteList)
            {
                if (item.tank.TankID != TankSprite.tank.TankID)
                {
                    if (CheckDirectionTank().Intersects(new Rectangle(item.tank.X, item.tank.Y, item.TankTexture.Width, item.TankTexture.Height)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool TankInterMap()
        {
            for (int i = 0; i < Map.IntMap.GetLength(0); i++)
            {
                for (int j = 0; j < Map.IntMap.GetLength(1); j++)
                {
                    if (Map.WallMap[i, j].IsActive == true)
                    {
                        if (CheckDirectionTank().Intersects(Map.WallMap[i, j].rec))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private Rectangle CheckDirectionTank()
        {
            switch (TankSprite.tank.tankDirection)
            {
                case Direction.UP:
                    {
                        tank = new Rectangle(TankSprite.tank.X, TankSprite.tank.Y - (TankSprite.tank.Speed + TankSprite.TankTexture.Height / 2), TankSprite.TankTexture.Width - 20, TankSprite.TankTexture.Height - 20);
                        break;
                    }
                case Direction.DOWN:
                    {
                        tank = new Rectangle(TankSprite.tank.X, TankSprite.tank.Y + TankSprite.tank.Speed, TankSprite.TankTexture.Width, TankSprite.TankTexture.Height);
                        break;
                    }
                case Direction.LEFT:
                    {
                        tank = new Rectangle(TankSprite.tank.X - (TankSprite.tank.Speed + TankSprite.TankTexture.Width / 2), TankSprite.tank.Y - 20, TankSprite.TankTexture.Width, TankSprite.TankTexture.Height);
                        break;
                    }
                case Direction.RIGHT:
                    {
                        tank = new Rectangle(TankSprite.tank.X + TankSprite.tank.Speed, TankSprite.tank.Y, TankSprite.TankTexture.Width, TankSprite.TankTexture.Height);
                        break;
                    }
                default:
                    break;
            }
            return tank;
        }
        private void CheckDirectionBullet()
        {
            switch (TankSprite.tank.tankDirection)
            {
                case Direction.UP:
                    {
                        TankSprite.tank.bullet.CoordY -= 40;
                        break;
                    }
                case Direction.DOWN:
                    {
                        TankSprite.tank.bullet.CoordY += 40;
                        break;
                    }
                case Direction.LEFT:
                    {
                        TankSprite.tank.bullet.CoordX -= TankSprite.tank.bullet.Speed + 10;
                        break;
                    }
                case Direction.RIGHT:
                    {
                        TankSprite.tank.bullet.CoordX += 40;
                        break;
                    }
                default:
                    break;
            }
        }
        private void CheckStatus()
        {


            if (TankSprite.tank.HP == 75)
            {

                TankSprite.tank.Color[0] = TankSprite.tank.Color[0] - 15;
                TankSprite.tank.Color[1] = TankSprite.tank.Color[1] - 15;
                TankSprite.tank.Color[2] = TankSprite.tank.Color[2] - 15;
            }
            else if (TankSprite.tank.HP == 50)
            {

                TankSprite.tank.Color[0] = TankSprite.tank.Color[0] - 15;
                TankSprite.tank.Color[1] = TankSprite.tank.Color[1] - 15;
                TankSprite.tank.Color[2] = TankSprite.tank.Color[2] - 15;

            }
            else if (TankSprite.tank.HP == 25)
            {

                TankSprite.tank.Color[0] = TankSprite.tank.Color[0] - 15;
                TankSprite.tank.Color[1] = TankSprite.tank.Color[1] - 15;
                TankSprite.tank.Color[2] = TankSprite.tank.Color[2] - 15;
            }
            else if (TankSprite.tank.HP <= 0)
            {

                TankSprite.tank.X = 4044;
                TankSprite.tank.Y = 4044;
            }

        }
    }
}
