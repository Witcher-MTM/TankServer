using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Client_Graphic
{
    public enum BtnState
    {
        Play,
        Settings,
        Exit
    }
    public class Menu
    {
        public bool IsActive { get; set; }
        public Button BtnPlay { get; set; }
        public Button BtnSettings { get; set; }
        public Button BtnExit { get; set; }
        public SpriteFont text { get; set; }
        public BtnState State { get; set; }
        public Menu(Button btn , SpriteFont text)
        {
            BtnPlay = new Button(new Rectangle(300, 300, 300, 80), btn.Texture);
            BtnSettings = new Button(new Rectangle(600, 300, 300, 80), btn.Texture);
            BtnExit = new Button(new Rectangle(900, 300, 300, 80), btn.Texture);
            this.text = text;
            IsActive = false;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(BtnPlay.Texture, BtnPlay.Rectangle, BtnPlay.color);
            spritebatch.Draw(BtnSettings.Texture, BtnSettings.Rectangle, BtnSettings.color);
            spritebatch.Draw(BtnExit.Texture, BtnExit.Rectangle, BtnExit.color);
            spritebatch.DrawString(text, "Play", new Vector2(420, 320), Color.White);
            spritebatch.DrawString(text, "Settings", new Vector2(690, 320), Color.White);
            spritebatch.DrawString(text, "Exit", new Vector2(1020, 320), Color.White);


        }
        public void CathClick()
        {
            if(IsActive == true)
            {
                var mouse = Mouse.GetState();
                if(new Rectangle(mouse.X, mouse.Y, 1, 1).Intersects(BtnPlay.Rectangle))
                {
                    BtnPlay.color = Color.LightGreen;
                }
                else
                {
                    BtnPlay.color = Color.Black;
                }
                if (new Rectangle(mouse.X, mouse.Y, 1, 1).Intersects(BtnSettings.Rectangle))
                {
                    BtnSettings.color = Color.LightGreen;
                }
                else
                {
                    BtnSettings.color = Color.Black;
                }
                if (new Rectangle(mouse.X, mouse.Y, 1, 1).Intersects(BtnExit.Rectangle))
                {
                    BtnExit.color = Color.LightGreen;
                }
                else
                {
                    BtnExit.color = Color.Black;
                }
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    if (new Rectangle(mouse.X, mouse.Y, 1, 1).Intersects(BtnPlay.Rectangle))
                    {
                        State = BtnState.Play;
                        IsActive = false;
                    }
                    if (new Rectangle(mouse.X, mouse.Y, 1, 1).Intersects(BtnSettings.Rectangle))
                    {
                        State = BtnState.Settings;
                    }
                    if (new Rectangle(mouse.X, mouse.Y, 1, 1).Intersects(BtnExit.Rectangle))
                    {
                        State = BtnState.Exit;
                    }
                }
            }
            
        }

    }
}
