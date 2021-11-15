using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using WinFormsApp1;
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
        public List<Button> MenuBtns { get; set; }
        public Button BtnSettings { get; set; }
        public Button BtnExit { get; set; }
        public SpriteFont text { get; set; }
        public BtnState State { get; set; }
        public Menu(Button btn, SpriteFont text)
        {
            MenuBtns = new List<Button>();
            MenuBtns.Add(new Button(new Rectangle(300, 300, 300, 80), btn.Texture, "Play"));
            MenuBtns.Add(new Button(new Rectangle(600, 300, 300, 80), btn.Texture, "Settings"));
            MenuBtns.Add(new Button(new Rectangle(900, 300, 300, 80), btn.Texture, "Exit"));
            this.text = text;
            IsActive = false;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (var item in MenuBtns)
            {
                spritebatch.Draw(item.Texture, item.Rectangle, item.color);
            }
            spritebatch.DrawString(text, "Play", new Vector2(420, 320), Color.White);
            spritebatch.DrawString(text, "Settings", new Vector2(690, 320), Color.White);
            spritebatch.DrawString(text, "Exit", new Vector2(1020, 320), Color.White);


        }
        public void CathClick()
        {
            if (IsActive == true)
            {
                var mouse = Mouse.GetState();
                foreach (var item in MenuBtns)
                {
                    if (new Rectangle(mouse.X, mouse.Y, 1, 1).Intersects(item.Rectangle))
                    {
                        item.color = Color.LightGreen;
                    }
                    else
                    {
                        item.color = Color.Black;
                    }
                }
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    foreach (var item in MenuBtns)
                    {
                        if (new Rectangle(mouse.X, mouse.Y, 1, 1).Intersects(item.Rectangle))
                        {
                            if (item.name == "Play")
                            {
                                State = BtnState.Play;
                                LoginForm.StartLogin();
                                if (LoginForm.IsLogin)
                                {
                                    IsActive = false;
                                }

                            }
                            else if (item.name == "Settings")
                            {
                                State = BtnState.Settings;
                            }
                            else if (item.name == "Exit")
                            {
                                State = BtnState.Exit;
                            }
                        }
                       
                    }
                   
                }

            }

        }

    }
}
