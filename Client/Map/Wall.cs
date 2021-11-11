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
        public int HP { get; set; }
        public Color color { get; set; }
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
            HP = 125;
            color = Color.White;

        }
        public void InitMap()
        {
            WallMap = new Wall[17, 32];
            for (int i = 0; i < map.IntMap.GetLength(0); i++)
            {
                for (int j = 0; j < map.IntMap.GetLength(1); j++)
                {
                    WallMap[i, j] = new Wall(new Rectangle(j * 50, i * 50, 58, 65), map.IntMap[i, j] == 'X' ? true : false);
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
                    {
                        _spriteBatch.Draw(wallTexture, new Vector2(WallMap[i, j].rec.X, WallMap[i, j].rec.Y), WallMap[i,j].color);
                    }
                }
            }
        }
        public void CheckStatus()
        {
            for (int i = 0; i < this.map.IntMap.GetLength(0); i++)
            {
                for (int j = 0; j < this.map.IntMap.GetLength(1); j++)
                {
                    if (WallMap[i, j].IsActive)
                    {
                        if (WallMap[i, j].HP == 100)
                        {
                            WallMap[i, j].color = new Color(243, 178, 178);
                        }
                        if (WallMap[i, j].HP == 75)
                        {
                            WallMap[i, j].color = new Color(222, 118, 118);
                        }
                        if (WallMap[i, j].HP == 50)
                        {
                            WallMap[i, j].color = new Color(114, 44, 44);
                        }
                        if (WallMap[i, j].HP == 25)
                        {
                            WallMap[i, j].color = new Color(73, 20, 20);
                        }
                    }
                    
                }
            }
        }
    }
}
