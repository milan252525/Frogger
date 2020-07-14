//Frogger - zápočtový program
//Milan Abrahám
//NPRG031 Programování 2

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Frogger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //disable resizing
            FormBorderStyle = FormBorderStyle.FixedSingle;
            //window icon
            Icon = Frogger.Properties.Resources.game_icon;
        }

        Game game;
        bool in_menu = true;
        /*
        everything gets drawn on invisible bitmap first, then the bitmap is copied 
        onto second graphics in the window to prevent
        too many drawings resulting in bad blinking effect
        */
        Bitmap bitmap;
        Graphics display_graphics;
        //timer ticks since start
        long tick;

        //start game button
        private void button1_Click(object sender, EventArgs e)
        {
            toggleMenuButtons();
            in_menu = false;
            startGame();
        }

        //change visibilty of all main menu components
        private void toggleMenuButtons()
        {
            bool state = newGameButton.Visible;
            newGameButton.Visible = !state;
            buttonHint.Visible = !state;
            level_slider.Visible = !state;
            level_easy.Visible = !state;
            level_medium.Visible = !state;
            level_hard.Visible = !state;
            level_random.Visible = !state;
            level_select.Visible = !state;
            picture_game_logo.Visible = !state;

            label_lives_left.Visible = state;
        }

        private void startGame()
        {
            //create new empty bitmap and load it into Graphics
            bitmap = new Bitmap(1000, 1000);
            Graphics g = Graphics.FromImage(bitmap);

            //get level from main menu slider
            int level_selected = level_slider.Value;
            //get corresponding enum value
            GameLevel game_level = (GameLevel) level_selected;
            //create new game object
            game = new Game(g, game_level);
            
            //create new frog and set it as currently active one
            Frog player = new Frog(game);
            game.current_frog = player;

            //add 3 lilypads into the playing field, one in the middle, two into thirds
            game.staticObjects.Add(new LilyPad(game, game.tiles_horizontal / 2));
            game.staticObjects.Add(new LilyPad(game, (game.tiles_horizontal / 3) - 1));
            game.staticObjects.Add(new LilyPad(game, game.tiles_horizontal * 2/3  + 1));
            //set required amount of frogs on lilypads to win
            game.to_win = 3;
            //start the game
            game.state = GameState.Running;
            timer1.Enabled = true;
            tick = 0;
        }

        
        private void timer1_Tick(object sender, EventArgs e)
        {
            //set remaining lives to label
            label_lives_left.Text = "LIVES LEFT: " + (game.to_loose - game.dead_frogs).ToString();

            //prepare a list of objects to delete (objects that got out of map)
            //cant remove them immidietally because of foreach loops
            List<MovingGameObject> to_remove = new List<MovingGameObject>();

            if (game.state == GameState.Running)
            {
                //spawn items based on set rules
                for (int i = 0; i < game.spawning.Length; i++)
                {
                    //process rules
                    string spawn_rule = game.spawning[i];
                    string[] rules = spawn_rule.Split(' ');
                    
                    string type = rules[0];
                    //each object has a frequency in the rule, frequency means the object will spawn every n-th tick
                    int frequency;
                    //left or right
                    string where;

                    switch (type)
                    {
                        case "log":
                            frequency = int.Parse(rules[3]);
                            //not time to spawn
                            if (tick % frequency != 0) break;
                            where = rules[1];
                            //length of the log
                            short length = short.Parse(rules[2]);
                            
                            if (where == "left")
                            {
                                //spawn new log on left (negative x, so it doesn't appear all at once)
                                game.movingObjects.Add(new Log(game, 0-length, i, length));
                            } 
                            else
                            {
                                //spawn new log on right, last argument meaning movement in opposite direction
                                game.movingObjects.Add(new Log(game, game.tiles_horizontal - 1, i, length, true));
                            }
                            break;
                        case "car":
                            frequency = int.Parse(rules[2]);
                            if (tick % frequency != 0) break;
                            where = rules[1];
                            if (where == "left")
                            {
                                //spawn new car on left
                                game.movingObjects.Add(new Car(game, -1, i));
                            }
                            else
                            {
                                //spawn new car on right, last argument meaning movement in opposite direction
                                game.movingObjects.Add(new Car(game, game.tiles_horizontal - 1, i, true));
                            }
                            break;
                        //if incorrect or empty rule has been passed don't do anything
                        default:
                            break;
                    }
                }

                //draw game background
                game.bg.draw();

                //for detection if frog is on the log
                game.current_frog.on_log = false;

                //process every moving object
                foreach (var obj in game.movingObjects)
                {
                    //car
                    if (obj.GetType() == typeof(Car))
                    {   
                        //car moves every 4th tick
                        if (tick % 4 == 0)
                        {
                            //move car
                            obj.move();
                            //if car got out of screen, prepare it for removal
                            if (obj.x == game.tiles_horizontal - 1 || obj.x == -1)
                            {
                                to_remove.Add(obj);
                            }
                        }
                        //draw car
                        obj.draw();
                    }
                    //log
                    else if (obj.GetType() == typeof(Log))
                    {
                        //convert MovingObject to Log because MovingObject doesn't have length attribute
                        Log log = (Log)obj;
                        //log moves every 4th tick
                        if (tick % 4 == 0)
                        {
                            //detect frog on the log
                            if (log.y == game.current_frog.y && (game.current_frog.x >= log.x && game.current_frog.x <= (log.x + log.length - 1)))
                            {
                                //move frog in the same direction
                                if (log.moving_left)
                                {
                                    game.current_frog.move_left();
                                }
                                else
                                {
                                    game.current_frog.move_right();
                                }

                            }
                            //move log
                            log.move();
                            //if log got out of screen, prepare it for removal
                            if (log.x == game.tiles_horizontal || log.x + log.length == -1)
                            {
                                to_remove.Add(log);
                            }
                        }
                        //detect frog on the log, duplicate because this needs to happen every tick
                        if (log.y == game.current_frog.y && (game.current_frog.x >= log.x && game.current_frog.x <= (log.x + log.length - 1)))
                            game.current_frog.on_log = true;
                        //draw log
                        log.draw();
                    }
                    //move and draw any other object, currently not any
                    else
                    {
                        obj.move();
                        obj.draw();
                    }
                }
                //draw every static object - lilypads, not active frogs
                foreach (var obj in game.staticObjects)
                {
                    obj.draw();
                }

                //kill frog that is in river but not on log
                if (game.background_tiles[game.current_frog.y] == "river" && !game.current_frog.on_log)
                {
                    game.state = GameState.FrogDead;
                }

                //move and draw active frog
                game.current_frog.move();
                game.current_frog.draw();

                //if frog reached last tile, check if it died - fell into water or won - landed on lilypad
                if (game.background_tiles[game.current_frog.y] == "river_end")
                {
                    bool dead = true;
                    //go through every statuc object, check only lilypads
                    foreach (var obj in game.staticObjects)
                    {
                        if (obj.GetType() == typeof(LilyPad))
                        {
                            //if there is frog on the lilypad, its not dead
                            if (obj.x == game.current_frog.x)
                            {
                                dead = false;
                            }
                        }
                    }
                    if (!dead)
                    {
                        //frog on lilypad
                        game.state = GameState.FrogOnLilypad;
                    } else
                    {
                        //dead
                        game.state = GameState.FrogDead;
                    }

                }

                //go through every moving object, check if any car hit frog
                foreach (var obj in game.movingObjects)
                {
                    if (obj.GetType() == typeof(Car))
                    {
                        if (obj.x == game.current_frog.x && obj.y == game.current_frog.y)
                        {
                            //kill the frog
                            game.state = GameState.FrogDead;
                            //no need to continue
                            break;
                        }
                    }
                }

                //if frog got out of screen (only possible on log), kill it
                //checking x coordinate
                if (game.current_frog.x < 0 || game.current_frog.x >= game.tiles_horizontal)
                {
                    game.state = GameState.FrogDead;
                }
                //checking y coordinate
                else if (game.current_frog.y < 0 || game.current_frog.y >= game.tiles_vertical)
                {
                    game.state = GameState.FrogDead;
                }

                //finally remove every object that should be removed
                foreach (var obj in to_remove)
                {
                    game.movingObjects.Remove(obj);
                }

                //create new Graphics on the screen
                display_graphics = CreateGraphics();
                //display prepared bitmap
                display_graphics.DrawImage(bitmap, 0, 0);
                //reset pressed key, results in better movement
                game.key_pressed = KeyPressed.none;
                //increase tick counter
                tick++;
            }
            //frog died
            if (game.state == GameState.FrogDead)
            {
                //increase dead frog counter
                game.dead_frogs++;
                //if amount is reached game is over
                if (game.dead_frogs == game.to_loose)
                {
                    game.state = GameState.Loss;
                }
                //make a new frog
                else
                {
                    Frog new_frog = new Frog(game);
                    game.current_frog = new_frog;
                    game.state = GameState.Running;
                }
            }
            //frog got onto lilypad
            if (game.state == GameState.FrogOnLilypad)
            {
                //make the frog static
                game.staticObjects.Add(game.current_frog);
                game.frog_wins++;
                //if all lilypads are full, game is won
                if (game.frog_wins == game.to_win)
                {
                    game.state = GameState.Win;
                } 
                else
                {
                    //create a new frog
                    Frog new_frog = new Frog(game);
                    game.current_frog = new_frog;
                    game.state = GameState.Running;
                }
            }
            //player won, game over
            if (game.state == GameState.Win)
            {
                //stop the game
                game.state = GameState.NotStarted;
                //display popup window
                MessageBox.Show("GAME WON!");
                //delete grpahics, restart the window into main menu
                this.Refresh();
                //show all menu buttons
                toggleMenuButtons();
                in_menu = true;
            }
            //player lost, game over
            if (game.state == GameState.Loss)
            {
                //stop the game
                game.state = GameState.NotStarted;
                //display popup window
                MessageBox.Show("GAME OVER!");
                //delete grpahics, restart the window into main menu
                this.Refresh();
                //show all menu buttons
                toggleMenuButtons();
                in_menu = true;
            }
        }

        //storing currently pressed key
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //ignore when game isn't running
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

        private void buttonHint_Click(object sender, EventArgs e)
        {
            //create new Form containing how to play guide
            Form2 howtoplay = new Form2();
            //disable resizing
            howtoplay.FormBorderStyle = FormBorderStyle.FixedSingle;
            //set windows icon
            howtoplay.Icon = Frogger.Properties.Resources.game_icon;
            howtoplay.Show();
        }
    }
}
