using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Frogger
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
        Bitmap bitmap;
        Graphics display_graphics;
        long tick;
        private void button1_Click(object sender, EventArgs e)
        {
            toggleMenuButtons();
            in_menu = false;
            startGame();
        }

        private void toggleMenuButtons()
        {
            bool state = newGameButton.Visible;
            newGameButton.Visible = !state;
            buttonHint.Visible = !state;
            levelSelector.Visible = !state;
            level_easy.Visible = !state;
            level_medium.Visible = !state;
            level_hard.Visible = !state;
        }

        private void startGame()
        {
            bitmap = new Bitmap(1000, 1000);
            Graphics g = Graphics.FromImage(bitmap);

            int level_selected = levelSelector.Value;
            GameLevel game_level = (GameLevel) level_selected;

            game = new Game(g, game_level);
            
            Frog player = new Frog(game);
            game.current_frog = player;

            game.staticObjects.Add(new LilyPad(game, game.tiles_horizontal / 2));
            game.staticObjects.Add(new LilyPad(game, (game.tiles_horizontal / 3) - 1));
            game.staticObjects.Add(new LilyPad(game, game.tiles_horizontal * 2/3  + 1));
            game.to_win = 3;

            game.state = GameState.Running;
            timer1.Enabled = true;
            tick = 200;
        }

        
        private void timer1_Tick(object sender, EventArgs e)
        {
            label_lives_left.Text = "LIVES LEFT: " + (game.to_loose - game.dead_frogs).ToString();

            List<MovingGameObject> to_remove = new List<MovingGameObject>();

            if (game.state == GameState.Running)
            {
                for (int i = 0; i < game.spawning.Length; i++)
                {
                    string spawn_rule = game.spawning[i];
                    string[] rules = spawn_rule.Split(' ');
                    
                    string type = rules[0];
                    int frequency;
                    string where;

                    switch (type)
                    {
                        case "log":
                            frequency = int.Parse(rules[3]);
                            if (tick % frequency != 0) break;
                            where = rules[1];
                            short length = short.Parse(rules[2]);
                            
                            if (where == "left")
                            {
                                game.movingObjects.Add(new Log(game, 0-length, i, length));
                            } 
                            else
                            {
                                game.movingObjects.Add(new Log(game, game.tiles_horizontal - 1, i, length, true));
                            }
                            break;
                        case "car":
                            frequency = int.Parse(rules[2]);
                            if (tick % frequency != 0) break;
                            where = rules[1];
                            if (where == "left")
                            {
                                game.movingObjects.Add(new Car(game, -1, i));
                            }
                            else
                            {
                                game.movingObjects.Add(new Car(game, game.tiles_horizontal - 1, i, true));
                            }
                            break;
                        default:
                            break;
                    }
                }

                game.bg.draw(); 

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
                        Log log = (Log)obj;
                        if (tick % 4 == 0)
                        {

                            if (log.y == game.current_frog.y && (game.current_frog.x >= log.x && game.current_frog.x <= (log.x + log.length - 1)))
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

                foreach (var obj in game.staticObjects)
                {
                    obj.draw();
                }

                if (game.background_tiles[game.current_frog.y] == "river" && !game.current_frog.on_log)
                {
                    game.state = GameState.FrogDead;
                }

                game.current_frog.move();
                game.current_frog.draw();

                if (game.background_tiles[game.current_frog.y] == "river_end")
                {
                    bool dead = true;
                    foreach (var obj in game.staticObjects)
                    {
                        if (obj.GetType() == typeof(LilyPad))
                        {
                            if (obj.x == game.current_frog.x)
                            {
                                dead = false;
                            }
                        }
                    }
                    if (!dead)
                    {
                        game.state = GameState.FrogInHole;
                    } else
                    {
                        game.state = GameState.FrogDead;
                    }

                }


                foreach (var obj in game.movingObjects)
                {
                    if (obj.GetType() == typeof(Car))
                    {
                        if (obj.x == game.current_frog.x && obj.y == game.current_frog.y)
                        {
                            game.state = GameState.FrogDead;
                            break;
                        }
                    }
                }

                if (game.current_frog.x < 0 || game.current_frog.x >= game.tiles_horizontal)
                {
                    game.state = GameState.FrogDead;
                }
                else if (game.current_frog.y < 0 || game.current_frog.y >= game.tiles_vertical)
                {
                    game.state = GameState.FrogDead;
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
            if (game.state == GameState.FrogDead)
            {
                Frog new_frog = new Frog(game);
                game.current_frog = new_frog;
                game.dead_frogs++;
                if (game.dead_frogs == game.to_loose)
                {
                    game.state = GameState.Loss;
                } 
                else
                {
                    game.state = GameState.Running;
                }
            }
            if (game.state == GameState.FrogInHole)
            {
                game.staticObjects.Add(game.current_frog);
                Frog new_frog = new Frog(game);
                game.current_frog = new_frog;
                game.frog_wins++;
                if (game.frog_wins == game.to_win)
                {
                    game.state = GameState.Win;
                } 
                else
                {
                    game.state = GameState.Running;
                }
            }
            if (game.state == GameState.Win)
            {
                game.state = GameState.NotStarted;
                MessageBox.Show("GAME Won!\nSCORE: " + game.frog_wins.ToString());
                this.Refresh();
                toggleMenuButtons();
                in_menu = true;
            }
            if (game.state == GameState.Loss)
            {
                game.state = GameState.NotStarted;
                MessageBox.Show("GAME OVER!\nSCORE: " + game.frog_wins.ToString());
                this.Refresh();
                toggleMenuButtons();
                in_menu = true;
            }
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

        private void level_medium_Click(object sender, EventArgs e)
        {

        }
    }
}
