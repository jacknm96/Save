using System;

namespace AdventureGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Room[,] map = new Room[3, 3];
            for (int row = 0; row < 3; row++) { 
                for (int col = 0; col < 3; col++) { 
                    map[row, col] = new Room(); 
                } 
            }
            Player p = new Player();
            Enemy e = new Enemy(10, 27, 6);
            PowerUp pu = new PowerUp();
            map[1, 1].AddGameObject(p);
            map[2, 0].AddGameObject(e);
            map[0, 1].AddGameObject(pu);
            for (int row = 0; row < 3; row++) { 
                for (int col = 0; col < 3; col++) { 
                    map[row, col].Draw(); 
                } Console.WriteLine(); 
            }
            Console.ReadLine();
        }
    }

    // base game object class, that our characters and power-ups will derive from
    abstract public class GameObject
    {
        protected int xPosition;
        protected int yPosition;

        abstract public void Draw();
    }

    // character object, that our players and enemies will derive from.
    abstract public class Character : GameObject
    {
        protected int attack;
        protected int hp;
        protected int defense;

        // if character still has hp remaining, returns true. otherwise false;
        public bool IsAlive()
        {
            return hp > 0;
        }
    }

    // expands on our character class, defining the player
    public class Player : Character
    {
        // player is denoted as 'X' in game
        override public void Draw()
        {
            Console.Write("X");
        }

        // initializes our player to have 2 attack, 10 hp, and 3 defense
        public Player()
        {
            attack = 2;
            hp = 10;
            defense = 3;
        }
    }

    // expands on our character class, defining the enemy
    public class Enemy : Character
    {
        // initializes our enemy with default values
        public Enemy()
        {
            attack = 1;
            hp = 15;
            defense = 2;
        }

        // initializes our enemy with given values
        public Enemy(int atk, int health, int def)
        {
            attack = atk;
            hp = health;
            defense = def;
        }

        // enemies will be denoted with a 'O' if they have more than 10 health remaining, otherwise with an 'o'
        public override void Draw()
        {
            if (hp > 10)
            {
                Console.Write("O");
            } else
            {
                Console.Write("o");
            }
        }
    }

    // defines out power-up objects
    public class PowerUp : GameObject
    {
        // power-ups will be denoted with a '?'
        public override void Draw()
        {
            Console.Write("?");
        }
    }

    // defines what exists in a given room, if anything
    public class Room
    {
        protected GameObject[] objects = new GameObject[3]; // a room can hold up to 3 objects

        // adds a given object to the room
        public void AddGameObject(GameObject go)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] == null)
                {
                    objects[i] = go;
                    break;
                }
            }
        }

        // removes a given object from the room. repositions the objects in the array so that the front of the array is filled, if possible.
        // if the given object does not exist in the room, prints that the object doesn't exist
        public void RemoveGameObject(GameObject go)
        {
            bool found = false;
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] == go)
                {
                    if (i == 0) // shifts objects forward in the array so that "null" spaces appear at the end
                    {
                        objects[0] = objects[1];
                        objects[1] = objects[2];
                        objects[2] = null;
                    } else if (i == 1)
                    {
                        objects[1] = objects[2];
                        objects[2] = null;
                    } else
                    {
                        objects[2] = null;
                    }
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                Console.WriteLine("Object does not exist in this room.");
            }
        }

        // if the room is empty, it is denoted by a '_'. otherwise, it uses the draw function of the first object in the room.
        public void Draw()
        {
            if (objects[0] == null)
            {
                Console.Write("_");
            } else
            {
                objects[0].Draw();
            }
        }
    }
}
