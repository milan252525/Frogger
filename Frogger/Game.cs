//Frogger - zápočtový program
//Milan Abrahám
//NPRG031 Programování 2

using System;
using System.Collections.Generic;
using System.Drawing;
using Frogger.Properties;

namespace Frogger
{
    //possible game states
    public enum GameState {NotStarted, Running, FrogOnLilypad, FrogDead, Win, Loss};
    //car colours
    public enum CarColour {Green, Red, Yellow, Blue};
    //pressed arrow key
    public enum KeyPressed {up, down, left, right, none};
    //game levels
    public enum GameLevel {Normal, Highway, DeepRiver, Random};
    class Game
    {
        //game state and level
        public GameState state;
        public GameLevel level;
        //graphics everythign gets drawn on
        public Graphics graphics;
        //size of one square
        public int tile_size;
        //amount of tiles
        public int tiles_vertical;
        public int tiles_horizontal;
        //size of game in pixels
        public int height;
        public int width;
        //level tile and spawning rules
        public string[] background_tiles;
        public string[] spawning;
        
        //win and loss counter
        public byte dead_frogs;
        public byte frog_wins;

        //required amounts to win and loose
        public byte to_win;
        public byte to_loose;

        //active frog
        public Frog current_frog;
        //pressed key to move the frog
        public KeyPressed key_pressed;
        //lists of objects
        public List<MovingGameObject> movingObjects;
        public List<GameObject> staticObjects;
        //background object - handles drawing the background
        public Background bg;
        //random number generator
        public Random random;

        //game constructor
        public Game(Graphics g, GameLevel l)
        {
            level = l;
            random = new Random();
            //set spawning rules based on level
            switch (level)
            {
                case GameLevel.Normal:
                    //handles what background is drawn
                    background_tiles = new string[] {
                        "river_end",
                        "river",
                        "river",
                        "river",
                        "beach",
                        "road",
                        "road",
                        "road",
                        "grass"
                    };
                    //handles spawning
                    //type of object | where to spawn it | (log length) | frequency (in game ticks) 
                    spawning = new string[]
                    {
                        "",
                        "log left 3 23",
                        "log right 2 19",
                        "log left 4 27",
                        "",
                        "car right 24",
                        "car left 12",
                        "car right 16",
                        ""
                    };
                    break;
                case GameLevel.DeepRiver:
                    background_tiles = new string[] {
                        "river_end",
                        "river",
                        "river",
                        "river",
                        "river",
                        "river",
                        "river",
                        "river",
                        "beach"
                    };
                    spawning = new string[]
                    {
                        "",
                        "log left 2 32",
                        "log right 4 36",
                        "log left 3 35",
                        "log right 5 44",
                        "log right 2 16",
                        "log left 4 32",
                        "log right 3 36",
                        ""
                    };
                    break;
                case GameLevel.Highway:
                    background_tiles = new string[] {
                        "river_end",
                        "road",
                        "road",
                        "road",
                        "road",
                        "road",
                        "road",
                        "road",
                        "grass"
                    };
                    spawning = new string[]
                    {
                        "",
                        "car left 24",
                        "car right 12",
                        "car left 16",
                        "car left 20",
                        "car right 12",
                        "car right 24",
                        "car left 20",
                        ""
                    };
                    break;
                //random level
                case GameLevel.Random:
                    background_tiles = new string[9];
                    spawning = new string[9];
                    //last and first tile have to be always same
                    //last - lilypads
                    //first - safe ground, beach or grass
                    background_tiles[0] = "river_end";
                    spawning[0] = "";
                    if (random.Next(0,2) < 1)
                        background_tiles[8] = "grass";
                    else
                        background_tiles[8] = "beach";
                    spawning[8] = "";

                    //randomly generate rest of the tiles
                    for (int i = 1; i < background_tiles.Length - 1; i++)
                    {
                        int rand = random.Next(0, 10);
                        //40% road
                        if (rand < 4)
                        {
                            background_tiles[i] = "road";
                            string spawn = "car ";
                            //where to spawn it
                            if (random.Next(0, 2) < 1)
                                spawn += "left ";
                            else
                                spawn += "right ";
                            //frequency 12 - 32 ticks
                            spawn += (random.Next(3, 9) * 4).ToString();
                            spawning[i] = spawn;
                        }
                        //40% river
                        else if (rand < 9)
                        {
                            background_tiles[i] = "river";
                            string spawn = "log ";
                            //where to spawn it
                            if (random.Next(0, 2) < 1)
                                spawn += "left ";
                            else
                                spawn += "right ";
                            //length 2 - 4
                            spawn += (random.Next(2, 5)).ToString();
                            spawn += " ";
                            //frequency 20 - 32 ticks
                            spawn += (random.Next(5, 9) * 4).ToString();
                            spawning[i] = spawn;
                        }
                        //20% safe land, grass or beach (50% each)
                        else
                        {
                            if (random.Next(0, 2) < 1)
                                background_tiles[i] = "grass";
                            else
                                background_tiles[i] = "beach";
                            spawning[i] = "";
                        }
                    }
                    break;
                default:
                    break;
            }

            //set all attributes
            graphics = g;
            //tiles has 64 pixels because of pixelart assets
            tile_size = 64;
            //9 wide, get from spawning rules
            tiles_vertical = background_tiles.Length;
            //11 long
            tiles_horizontal = 11;
            //calculate size of window
            height = tile_size * tiles_vertical;
            width = tile_size * tiles_horizontal;
            //create new background
            bg = new Background(this);
            movingObjects = new List<MovingGameObject>();
            staticObjects = new List<GameObject>();
            state = GameState.NotStarted;
            key_pressed = KeyPressed.none;

            dead_frogs = 0;
            frog_wins = 0;
            //3 frogs on lilypads to win, 5 lives
            to_win = 3;
            to_loose = 5;
        }

        //check if active frog is at coordinates
        public bool isFrogAt(int x, int y)
        {
            foreach (var obj in this.staticObjects)
            {
                if (obj.GetType() == typeof(Frog))
                {
                    if (obj.x == x && obj.y == y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
    abstract class GameObject
    {
        public Game game;
        public int x;
        public int y;
        public abstract void draw();
    }

    abstract class MovingGameObject : GameObject
    {
        public abstract void move();
    }

    class Background : GameObject
    {
        public Background(Game game)
        {
            this.game = game;
        }
        public override void draw()
        {
            //go through all tiles, and draw corresponding background
            for (int i = 0; i < game.tiles_vertical; i++)
            {
                string type = game.background_tiles[i];
                switch (type)
                {
                    case "grass":
                        for (int j = 0; j < game.tiles_horizontal; j++)
                        {
                            game.graphics.DrawImage(Resources.grass, game.tile_size * j, game.tile_size * i);
                        }
                        break;
                    case "river":
                        for (int j = 0; j < game.tiles_horizontal; j++)
                        {
                            game.graphics.DrawImage(Resources.river, game.tile_size * j, game.tile_size * i);
                        }
                        break;
                    //same as river, but had to be named diffently
                    case "river_end":
                        for (int j = 0; j < game.tiles_horizontal; j++)
                        {
                            game.graphics.DrawImage(Resources.river, game.tile_size * j, game.tile_size * i);
                        }
                        break;
                    case "beach":
                        for (int j = 0; j < game.tiles_horizontal; j++)
                        {
                            game.graphics.DrawImage(Resources.beach, game.tile_size * j, game.tile_size * i);
                        }
                        break;
                    case "road":
                        for (int j = 0; j < game.tiles_horizontal; j++)
                        {
                            game.graphics.DrawImage(Resources.road, game.tile_size * j, game.tile_size * i);
                        }
                        break;
                }
            }  
        }
    }
    class Frog : MovingGameObject
    {
        //counts game ticks, used for animation
        private long tick;
        private short animation;
        public bool on_log;
        public Frog(Game game)
        {
            this.game = game;
            //starting position bottom in the middle
            x = game.tiles_horizontal / 2;
            y = game.tiles_vertical - 1;
            tick = 0;
            animation = 0;
            on_log = false;
        }
        public override void move()
        {
            //moved based on pressed arrow key
            if (game.state == GameState.Running && game.key_pressed != KeyPressed.none)
            {
                //checking if frog wont go out of screen
                switch (game.key_pressed)
                {
                    case KeyPressed.up:
                        //check for frog above - two frogs cant be on one lilypad
                        if (y - 1 >= 0 && !game.isFrogAt(x, y-1))
                            y -= 1;
                        break;
                    case KeyPressed.down:
                        if (y + 1 <= game.tiles_vertical - 1)
                            y += 1;
                        break;
                    case KeyPressed.left:
                        if (x - 1 >= 0)
                            x -= 1;
                        break;
                    case KeyPressed.right:
                        if (x + 1 <= game.tiles_horizontal - 1)
                            x += 1;
                        break;
                    default:
                        break;
                }
            }
        }
        //methods used by logs to move frog
        public void move_right()
        {
            x += 1;
        }

        public void move_left()
        {
            x -= 1;
        }

        public override void draw()
        {
            //handles animation (tongue)
            tick++;
            //start animation every 50th tick
            if (tick % 50 == 0)
            {
                animation++;
            }
            //animation started, draw corresponding image
            if (animation > 0)
            {
                if (animation < 5)
                {
                    game.graphics.DrawImage(Resources.frog_tongue1, game.tile_size * x, game.tile_size * y);
                }
                else if (animation < 10)
                {
                    game.graphics.DrawImage(Resources.frog_tongue2, game.tile_size * x, game.tile_size * y);
                }
                else if (animation < 15)
                {
                    game.graphics.DrawImage(Resources.frog_tongue1, game.tile_size * x, game.tile_size * y);
                }
                animation++;
                //stop the animation
                if (animation == 15)
                {
                    animation = 0;
                }
            }
            else
            {
                game.graphics.DrawImage(Resources.frog, game.tile_size * x, game.tile_size * y);
            }
        }
    }
  
    class Car: MovingGameObject
    {
        bool moving_left;
        CarColour colour;
        public Car(Game game, int x, int y, bool opposite=false)
        {
            this.game = game;
            this.x = x;
            this.y = y;
            moving_left = opposite;
            //generate random car colour
            Array values = Enum.GetValues(typeof(CarColour));
            colour = (CarColour)values.GetValue(game.random.Next(values.Length));
        }
        public override void move()
        {
            if (moving_left)
            {
                x -= 1;
            }
            else
            {
                x += 1;
            }
        }

        public override void draw()
        {
            if (moving_left)
            {
                //different colours
                switch (colour)
                {
                    case CarColour.Green:
                        game.graphics.DrawImage(Resources.car_green_left, game.tile_size * x, game.tile_size * y);
                        break;
                    case CarColour.Red:
                        game.graphics.DrawImage(Resources.car_red_left, game.tile_size * x, game.tile_size * y);
                        break;
                    case CarColour.Yellow:
                        game.graphics.DrawImage(Resources.car_yellow_left, game.tile_size * x, game.tile_size * y);
                        break;
                    case CarColour.Blue:
                        game.graphics.DrawImage(Resources.car_green_left, game.tile_size * x, game.tile_size * y);
                        break;
                }
            }
            else
            {
                switch (colour)
                {
                    case CarColour.Green:
                        game.graphics.DrawImage(Resources.car_green, game.tile_size * x, game.tile_size * y);
                        break;
                    case CarColour.Red:
                        game.graphics.DrawImage(Resources.car_red, game.tile_size * x, game.tile_size * y);
                        break;
                    case CarColour.Yellow:
                        game.graphics.DrawImage(Resources.car_yellow, game.tile_size * x, game.tile_size * y);
                        break;
                    case CarColour.Blue:
                        game.graphics.DrawImage(Resources.car_blue, game.tile_size * x, game.tile_size * y);
                        break;
                }
                
            }
        }
    }
    class Log : MovingGameObject
    {
        public bool moving_left;
        public short length;

        //log stores only position of left most tile
        public Log(Game game, int x, int y, short length, bool opposite = false)
        {
            this.game = game;
            this.x = x;
            this.y = y;
            this.length = length;
            moving_left = opposite;
        }
        public override void move()
        {
            if (moving_left)
            {
                x -= 1;
            }
            else
            {
                x += 1;
            }
        }

        public override void draw()
        {
            //draw log tile by tile, sides are different
            for (int i = 0; i < this.length; i++)
            {
                if (i == 0)
                {
                    game.graphics.DrawImage(Resources.log_left, game.tile_size * x, game.tile_size * y);
                }
                else if (i == this.length - 1)
                {
                    game.graphics.DrawImage(Resources.log_right, game.tile_size * (x + i), game.tile_size * y);
                }
                else
                {
                    game.graphics.DrawImage(Resources.log_middle, game.tile_size * (x + i), game.tile_size * y);
                }
                
            }
        }
    }

    class LilyPad : GameObject
    {
        public LilyPad(Game game, int x)
        {
            this.x = x;
            this.y = 0;
            this.game = game;
        }
        public override void draw()
        {
            game.graphics.DrawImage(Resources.lilypad, game.tile_size * x, game.tile_size * y);
        }
    }
}
