using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MapDLL;
namespace Client_Graphic
{
    public class Wall
    {
        public bool IsActive { set; get; }
        public Rectangle rec { set; get; }
        public Map map { get; set; }
        public Wall[,] WallMap { get; set; }
        public Texture2D wallTexture { get; set; }
        public Wall()
        {
            map = new Map();
        }
        public Wall(Rectangle rect, bool active)
        {
            rec = rect;
            IsActive = active;
           
        }
        public void InitMap()
        {
            WallMap = new Wall[17, 32];
            for (int i = 0; i < map.IntMap.GetLength(0); i++)
            {
                for (int j = 0; j < map.IntMap.GetLength(1); j++)
                {
                    WallMap[i, j] = new Wall(new Rectangle(j * 50, i * 50, 50, 50), map.IntMap[i, j] == 'X' ? true : false);
                }
            }
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            for (int i = 0; i < this.map.IntMap.GetLength(0); i++)
            {
                for (int j = 0; j < this.map.IntMap.GetLength(1); j++)
                {
                    if (WallMap[i, j].IsActive == true)
                        _spriteBatch.Draw(wallTexture, new Vector2(WallMap[i, j].rec.X, WallMap[i, j].rec.Y), Color.Bisque);
                }
            }
        }
    }
}
