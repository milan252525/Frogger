using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Hra.Properties;

namespace Hra
{
    public enum GameState {NotStarted, Running, FrogInHole, FrogDead, Win, Loss};
    public enum CarColour {Green, Red, Yellow, Blue};
    public enum KeyPressed {up, down, left, right, none};
    class Game
    {
        public GameState state = GameState.NotStarted;
        public Graphics graphics;
        public int tile_size;
        public int tiles_vertical;
        public int tiles_horizontal;
        public int height;
        public int width;
        public string[] background_tiles;
        
        public byte dead_frogs;
        public byte frog_wins;

        public byte to_win;
        public byte to_loose;

        public Frog current_frog;

        public GameObject[,] map;

        public KeyPressed key_pressed;

        public List<MovingGameObject> movingObjects;
        public List<GameObject> staticObjects;

        public Background bg;

        public Random random;

        public Game(Graphics g)
        {
            graphics = g;
            tile_size = 64;
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
            tiles_vertical = background_tiles.Length;
            tiles_horizontal = 11;
            map = new GameObject[tiles_vertical, tiles_horizontal];
            height = tile_size * tiles_vertical;
            width = tile_size * tiles_horizontal;
            bg = new Background(this);
            movingObjects = new List<MovingGameObject>();
            staticObjects = new List<GameObject>();
            state = GameState.Running;
            key_pressed = KeyPressed.none;

            dead_frogs = 0;
            frog_wins = 0;

            to_win = 3;
            to_loose = 5;

            random = new Random();
        }

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
        private long tick;
        private short animation;
        public bool on_log;
        public Frog(Game game)
        {
            this.game = game;
            x = game.tiles_horizontal / 2;
            y = game.tiles_vertical - 1;
            tick = 0;
            animation = 0;
            on_log = false;
        }
        public override void move()
        {
            if (game.state == GameState.Running && game.key_pressed != KeyPressed.none)
            {
                switch (game.key_pressed)
                {
                    case KeyPressed.up:
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
            tick++;
            if (tick % 50 == 0)
            {
                animation++;
            }
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
