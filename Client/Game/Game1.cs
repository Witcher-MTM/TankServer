using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using TankDLL;


namespace Client_Graphic
{
 

    public class Game1 : Game
    {
        private Menu menu;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Client client = new Client();
        private List<Sprite> TankSpriteList;
        private Sprite TankSprite;
        private bool KeyPressed;
        private List<Tank> tanks;
        private Rectangle tank;
        private Rectangle bullet_rectangle;
        private SpriteFont tankHP;
        private Wall wall;
        private bool SetTankID;
        private Button button;
        private SpriteFont textForMenu;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            KeyPressed = false;
            this.tanks = new List<Tank>();
            this.TankSpriteList = new List<Sprite>();
            this.SetTankID = false;
            this.wall = new Wall();
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
            wall.map.LoadMap();
            wall.InitMap();
             _spriteBatch = new SpriteBatch(GraphicsDevice);
            TankSprite = new Sprite(Content.Load<Texture2D>(@"Texure\tank"), new Tank(), Content.Load<Texture2D>(@"Texure\bullet"), 
                new Bullet());
            tankHP = Content.Load<SpriteFont>(@"LabelInfo\TankHP");
            wall.wallTexture = Content.Load<Texture2D>(@"Texure\wall");
            button = new Button(new Rectangle(100, 100, 300, 80), Content.Load<Texture2D>(@"Texure\MenuTexture\Button"));
            textForMenu = Content.Load<SpriteFont>(@"Texure\MenuTexture\MenuText");
            menu = new Menu(button, textForMenu);
            Rectangle tank = new Rectangle(TankSprite.tank.X, TankSprite.tank.Y, TankSprite.TankTexture.Width, TankSprite.TankTexture.Height);
            Rectangle bullet_rectangle = new Rectangle(TankSprite.tank.bullet.CoordX, TankSprite.tank.bullet.CoordY, 20, 20);
            client.SendInfo(TankSprite.tank);
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            if (menu.State == BtnState.Exit)
            {
                Exit();
            }
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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && menu.IsActive == false)
            {
                menu.IsActive = true;
            }
            if (TankSprite.tank.IsAlive)
            {
                TankMove();
            }

            menu.CathClick();
            SetID();
            BulletInter();
            BulletMove();
          
            if (TankSprite.tank.TankRespawn())
            {
                client.SendInfo(TankSprite.tank);
            }
            KeyPressed = false;
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            if (menu.IsActive)
            {
                menu.Draw(_spriteBatch);
            }
            else
            {
                DrawGame();
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        private void DrawGame()
        {
            wall.Draw(_spriteBatch);
            foreach (var item in TankSpriteList)
            {
                if (item.tank.IsAlive)
                {
                    _spriteBatch.Draw(item.TankTexture, new Rectangle(item.tank.X, item.tank.Y, item.TankTexture.Width, item.TankTexture.Height), null, new Color(item.tank.Color[0], item.tank.Color[1], item.tank.Color[2]), item.tank.Rotation, new Vector2(TankSprite.tank.TankRealWidth, TankSprite.tank.TankRealHeight), SpriteEffects.None, 0f);
                    _spriteBatch.DrawString(tankHP, $"{item.tank.HP}", new Vector2(item.tank.X - 20, item.tank.Y + 25), Color.White);
                }
                if (item.tank.bullet.IsActive)
                {
                    _spriteBatch.Draw(item.BulletTexture, new Rectangle(item.tank.bullet.CoordX, item.tank.bullet.CoordY, item.tank.bullet.Width, item.tank.bullet.Height), null, Color.White, item.tank.bullet.Rotation, new Vector2(item.BulletTexture.Width / 2f, item.BulletTexture.Height / 2f), SpriteEffects.None, 0f);
                }
            }
        }
        private void BulletMove()
        {

            if (TankSprite.tank.IsAlive == true)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && TankSprite.tank.bullet.CD == 0)
                {
                    TankSprite.tank.bullet.CoordY = TankSprite.tank.Y;
                    TankSprite.tank.bullet.CoordX = TankSprite.tank.X;
                    TankSprite.tank.bullet.bulletDirection = TankSprite.tank.tankDirection;
                    TankSprite.tank.bullet.CheckDirection();
                    TankSprite.tank.bullet.IsActive = true;
                    TankSprite.tank.bullet.CD = 60;
                }
                if (TankSprite.tank.bullet.IsActive)
                {
                    switch (TankSprite.tank.bullet.bulletDirection)
                    {
                        case Direction.UP:
                            {
                                TankSprite.tank.bullet.CoordY -= TankSprite.tank.bullet.Speed;
                            }
                            break;
                        case Direction.DOWN:
                            {
                                TankSprite.tank.bullet.CoordY += TankSprite.tank.bullet.Speed;
                            }
                            break;
                        case Direction.LEFT:
                            {
                                TankSprite.tank.bullet.CoordX -= TankSprite.tank.bullet.Speed;
                            }
                            break;
                        case Direction.RIGHT:
                            {
                                TankSprite.tank.bullet.CoordX += TankSprite.tank.bullet.Speed;
                            }
                            break;
                        default:
                            break;
                    }
                    client.SendInfo(TankSprite.tank);
                }
                if (TankSprite.tank.bullet.CD > 0)
                {
                    TankSprite.tank.bullet.CD--;
                }
            }
            else
            {
                TankSprite.tank.bullet.CD = 0;
                client.SendInfo(TankSprite.tank);
            }
        }
        private void TankMove()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W) && KeyPressed == false)
            {
                TankSprite.tank.tankDirection = Direction.UP;
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
                TankSprite.tank.Rotation = 15.7f;
                if (TankInterMap() && TankInterTank())
                {
                    TankSprite.tank.Y += TankSprite.tank.Speed;
                    KeyPressed = true;
                }
                client.SendInfo(TankSprite.tank);
            }
        }
        private void BulletInter()
        {
            bullet_rectangle = new Rectangle(TankSprite.tank.bullet.CoordX, TankSprite.tank.bullet.CoordY, TankSprite.tank.bullet.Width, TankSprite.tank.bullet.Height);
            tank = new Rectangle(TankSprite.tank.X, TankSprite.tank.Y, TankSprite.tank.TankRealWidth, TankSprite.tank.TankRealHeight);
            foreach (var item in TankSpriteList)
            {
                if (TankSprite.tank.TankID != item.tank.TankID)
                {
                    if (tank.Intersects(new Rectangle(item.tank.bullet.CoordX, item.tank.bullet.CoordY, item.tank.bullet.Width, item.tank.bullet.Height)))
                    {
                        TankSprite.tank.HP -= item.tank.bullet.Damage;
                        TankSprite.tank.CheckHP();
                        client.SendInfo(TankSprite.tank);
                        break;
                    }
                }
            }
            foreach (var item_2 in tanks)
            {
                if (TankSprite.tank.TankID != item_2.TankID)
                {
                    if (bullet_rectangle.Intersects(new Rectangle(item_2.X, item_2.Y, TankSprite.tank.TankRealWidth, TankSprite.tank.TankRealHeight)))
                    {
                        TankSprite.tank.bullet.CoordX = 2021;
                        TankSprite.tank.bullet.CoordY = 2021;
                        TankSprite.tank.bullet.IsActive = false;
                        client.SendInfo(TankSprite.tank);
                    }
                }
            }
            for (int i = 0; i <this.wall.map.IntMap.GetLength(0); i++)
            {
                for (int j = 0; j < this.wall.map.IntMap.GetLength(1); j++)
                {
                    if (this.wall.WallMap[i, j].IsActive == true)
                    {
                        if (bullet_rectangle.Intersects(this.wall.WallMap[i, j].rec))
                        {
                            if(i!=0 && i != this.wall.map.IntMap.GetLength(0)-1)
                            {
                                if(j!=0 && j != this.wall.map.IntMap.GetLength(1)-1)
                                {
                                    this.wall.WallMap[i, j].HP -= TankSprite.tank.bullet.Damage;
                                    this.wall.CheckStatus();
                                    if (this.wall.WallMap[i, j].HP <= 0)
                                    {
                                        this.wall.WallMap[i, j].IsActive = false;
                                    }

                                }
                            }
                            TankSprite.tank.bullet.CoordX = 2021;
                            TankSprite.tank.bullet.CoordY = 2021;
                            TankSprite.tank.bullet.IsActive = false;
                            client.SendInfo(TankSprite.tank);
                            break;
                        }
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
                    if (CheckDirectionTank().Intersects(new Rectangle(item.tank.X, item.tank.Y, TankSprite.tank.TankRealWidth, TankSprite.tank.TankRealHeight)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool TankInterMap()
        {
            for (int i = 0; i < this.wall.map.IntMap.GetLength(0); i++)
            {
                for (int j = 0; j < this.wall.map.IntMap.GetLength(1); j++)
                {
                    if (this.wall.WallMap[i, j].IsActive == true)
                    {
                        if (CheckDirectionTank().Intersects(this.wall.WallMap[i, j].rec))
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
                        tank = new Rectangle(TankSprite.tank.X, TankSprite.tank.Y - (TankSprite.tank.Speed + TankSprite.tank.TankRealHeight / 2), TankSprite.tank.TankRealWidth, TankSprite.tank.TankRealHeight);
                        break;
                    }
                case Direction.DOWN:
                    {
                        tank = new Rectangle(TankSprite.tank.X, TankSprite.tank.Y + TankSprite.tank.Speed, TankSprite.tank.TankRealWidth, TankSprite.tank.TankRealHeight);
                        break;
                    }
                case Direction.LEFT:
                    {
                        tank = new Rectangle(TankSprite.tank.X - TankSprite.tank.Speed, TankSprite.tank.Y, TankSprite.tank.TankRealWidth, TankSprite.tank.TankRealHeight);
                        break;
                    }
                case Direction.RIGHT:
                    {
                        tank = new Rectangle(TankSprite.tank.X + (TankSprite.tank.Speed + TankSprite.tank.TankRealWidth / 2), TankSprite.tank.Y, TankSprite.tank.TankRealWidth, TankSprite.tank.TankRealHeight);
                        break;
                    }
                default:
                    break;
            }
            return tank;
        }
        private void SetID()
        {
            if (SetTankID == false)
            {
                if (TankSprite.tank.TankID == 0)
                {
                    if (TankSpriteList.Count > 0)
                    {
                        TankSprite.tank.TankID = TankSpriteList.Count;
                        client.SendInfo(TankSprite.tank);
                        SetTankID = true;
                    }
                }
            }
        }
    }
}
