using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hra
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        Game game;
        bool in_menu = true;
        Bitmap bitmap = new Bitmap(1000, 1000);
        Graphics display_graphics;
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;

            Graphics g = Graphics.FromImage(bitmap);
            game = new Game(g);
            in_menu = false;
            Frog player = new Frog(game);
            game.current_frog = player;

            timer1.Enabled = true;
        }
        long tick = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tick % 15 == 0)
            {
                game.movingObjects.Add(new Car(game, -1, 6));
            }
            if (tick % 20 == 0)
            {
                game.movingObjects.Add(new Car(game, game.tiles_horizontal-1, 7, true));
            }
            if (tick % 25 == 0)
            {
                game.movingObjects.Add(new Log(game, -2, 1, 2));
            }
            if (tick % 35 == 0)
            {
                game.movingObjects.Add(new Car(game, game.tiles_horizontal - 1, 5, true));
                game.movingObjects.Add(new Log(game, -3, 3, 3));
                game.movingObjects.Add(new Log(game, game.tiles_horizontal - 1, 2, 4, true));
            }
            game.bg.draw();
            List<MovingGameObject> to_remove = new List<MovingGameObject>();
            
            game.current_frog.on_log = false;
            foreach (var obj in game.movingObjects)
            {
                if (obj.GetType() == typeof(Car))
                {
                    if (tick % 4 == 0)
                    {
                        obj.move();
                        if (obj.x == game.tiles_horizontal - 1 || obj.x == -1)
                        {
                            to_remove.Add(obj);
                        }
                    }
                    obj.draw();
                }
                else if (obj.GetType() == typeof(Log))
                {
                    Log log = (Log) obj;
                    if (tick % 4 == 0)
                    {
                        
                        if (log.y == game.current_frog.y && (game.current_frog.x >= log.x && game.current_frog.x <= (log.x + log.length-1)))
                        {
                            if (log.moving_left)
                            {
                                game.current_frog.move_left();
                            }
                            else
                            {
                                game.current_frog.move_right();
                            }
                            
                        }
                        log.move();
                        if (log.x == game.tiles_horizontal || log.x + log.length == -1)
                        {
                            to_remove.Add(log);
                        }
                    }
                    if (log.y == game.current_frog.y && (game.current_frog.x >= log.x && game.current_frog.x <= (log.x + log.length - 1)))
                        game.current_frog.on_log = true;
                    log.draw();
                }
                else
                {
                    obj.move();
                    obj.draw();
                }
            }

            if (game.background_tiles[game.current_frog.y] == "river" && !game.current_frog.on_log)
            {
                timer1.Stop();
            }
            
            game.current_frog.move();
            game.current_frog.draw();
          
                

            foreach (var obj in game.movingObjects)
            {
                if (obj.GetType() == typeof(Car)) 
                {
                    if (obj.x == game.current_frog.x && obj.y == game.current_frog.y)
                    {
                        timer1.Stop();
                    }
                }
            }

            if (game.current_frog.x < 0 || game.current_frog.x >= game.tiles_horizontal)
            {
                timer1.Stop();
            }
            if (game.current_frog.y < 0 || game.current_frog.y >= game.tiles_vertical)
            {
                timer1.Stop();
            }

            foreach (var obj in to_remove)
            {
                game.movingObjects.Remove(obj);
            }
    
            display_graphics = CreateGraphics();
            display_graphics.DrawImage(bitmap, 0, 0);
            game.key_pressed = KeyPressed.none;
            tick++;
        }
        
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (in_menu)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
            if (keyData == Keys.Up)
            {
                game.key_pressed = KeyPressed.up;
                return true;
            }
            if (keyData == Keys.Down)
            {
                game.key_pressed = KeyPressed.down;
                return true;
            }
            if (keyData == Keys.Left)
            {
                game.key_pressed = KeyPressed.left;
                return true;
            }
            if (keyData == Keys.Right)
            {
                game.key_pressed = KeyPressed.right;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //game.key_pressed = KeyPressed.none;
        }
    }
}
