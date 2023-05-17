using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Tanki
{
    internal class PlayerTankController
    {
        Tank playerTank;
        Map map;
        EnemyTankController enemyController;
        bool isAlive;
        public PlayerTankController(Map map)
        {
            this.map = map;
            this.playerTank = new Tank(new MediumTankBody(), map.FindPositionForTank(0, 1), Image.FromFile("C:\\Users\\admin\\source\\repos\\Tanki\\Tanki\\images\\player.png"));
            isAlive = true;
        }
        //alive
        //position



        public bool Move(int x, int y)
        {
            map.UnmarkAllCells();

            if ((playerTank.GetPosition().x + x < 0) || (playerTank.GetPosition().x + x > map.GetMapSize() - 1)) { return false; }  //check if tank is going to be outside of the map
            if ((playerTank.GetPosition().y + y < 0) || (playerTank.GetPosition().y + y > map.GetMapSize() - 1)) { return false; }

            if (playerTank.GetPosition().x + x == enemyController.GetPosition().x && playerTank.GetPosition().y + y == enemyController.GetPosition().y) return false;

            if (map.ParkingAllowed(playerTank.GetPosition().x + x, playerTank.GetPosition().y + y)) 
            {
                playerTank.Move(x, y);
                enemyController.MakeMove();
                return true;
            }
            else { return false; }
        }

        public bool Shoot(int x, int y, IProjectile projectile)
        {
            map.UnmarkAllCells();
            if (x == 0) 
            {
                for (int i = playerTank.GetPosition().y + y; i != playerTank.GetPosition().y + (y * projectile.Range); i += y)
                {
                    map.MarkCell(playerTank.GetPosition().x, i);
                    if (enemyController.GetPosition() == new Position(playerTank.GetPosition().x, i)) enemyController.GetHit(projectile);
                }
            }
            else if (y == 0)
            {
                for (int i = playerTank.GetPosition().x + x; i != playerTank.GetPosition().x + (x * projectile.Range); i += x)
                {
                    map.MarkCell(i, playerTank.GetPosition().y);
                    if (enemyController.GetPosition() == new Position(i, playerTank.GetPosition().y)) enemyController.GetHit(projectile);
                }
            }
            enemyController.MakeMove();
            return true;
        }
        //hit

        public void GetHit(int damage)
        {
            isAlive = playerTank.GetHit(damage);
        }

        public Position GetPosition() { return playerTank.GetPosition(); }

        public void SetEnemyTankController(EnemyTankController enemyController)
        {
            this.enemyController = enemyController;
        }
        public void Draw(Graphics g)
        {
            playerTank.Draw(g);

        }
    }

    internal class EnemyTankController
    {
        Tank enemyTank;
        Map map;
        bool isAlive;
        PlayerTankController playerController;
        public EnemyTankController(Map map)
        {
            this.map = map;
            this.enemyTank = new Tank(new HeavyTankBody(), map.FindPositionForTank(map.GetMapSize() - 1, -1), Image.FromFile("C:\\Users\\admin\\source\\repos\\Tanki\\Tanki\\images\\enemy.png"));
            isAlive = true;
        }

        public bool MakeMove()
        {
            //shoot
            if (playerController.GetPosition().x == enemyTank.GetPosition().x && Abs(playerController.GetPosition().y-enemyTank.GetPosition().y) < 5)
            {//shoot verticaly
                Shoot(0, GetDirection(playerController.GetPosition().y - enemyTank.GetPosition().y));
                return true;
            }
            if (playerController.GetPosition().y == enemyTank.GetPosition().y && Abs(playerController.GetPosition().x - enemyTank.GetPosition().x) < 5)
            { //shoot horizontaly
                Shoot(GetDirection(playerController.GetPosition().x - enemyTank.GetPosition().x), 0);
                return true;
            }
            

            
            if (Abs(playerController.GetPosition().x - enemyTank.GetPosition().x) > Abs(playerController.GetPosition().y - enemyTank.GetPosition().y))
            { //move on x
                if (Move(GetDirection(playerController.GetPosition().x - enemyTank.GetPosition().x), 0))
                {
                    return true;
                }
            }
            //move on y
            if (Move(0, GetDirection(playerController.GetPosition().y - enemyTank.GetPosition().y))) return true;

            return false;
        }

        bool Shoot(int x, int y)
        {

            map.UnmarkAllCells();
            if (x == 0)
            {
                for (int i = enemyTank.GetPosition().y + y; i != enemyTank.GetPosition().y + (4 * y); i += y)
                {
                    map.MarkCell(enemyTank.GetPosition().x, i);
                    if (playerController.GetPosition() == new Position(enemyTank.GetPosition().x, i)) playerController.GetHit(15);
                }
            }
            else if (y == 0)
            {
                for (int i = enemyTank.GetPosition().x + x; i != enemyTank.GetPosition().x + (4 * x); i += x)
                {
                    map.MarkCell(i, enemyTank.GetPosition().y);
                    if (playerController.GetPosition() == new Position(i, enemyTank.GetPosition().y)) playerController.GetHit(15);
                }
            }
            return true;
        }

        bool Move(int x, int y)
        {
            if ((enemyTank.GetPosition().x + x < 0) || (enemyTank.GetPosition().x + x > map.GetMapSize() - 1)) { return false; }  //check if tank is going to be outside of the map
            if ((enemyTank.GetPosition().y + y < 0) || (enemyTank.GetPosition().y + y > map.GetMapSize() - 1)) { return false; }

            if ((enemyTank.GetPosition().x + x == playerController.GetPosition().x && enemyTank.GetPosition().y + y == playerController.GetPosition().y)) { return false; } //check if tanks are colliding

            if (map.ParkingAllowed(enemyTank.GetPosition().x + x, enemyTank.GetPosition().y + y))
            {
                enemyTank.Move(x, y);
                return true;
            }
            else { return false; }
        }

        public void GetHit(IProjectile projectile)
        {
            isAlive = enemyTank.GetHit(projectile.Damage);
        }

        public Position GetPosition() { return enemyTank.GetPosition(); }

        public void Draw(Graphics g)
        {
            enemyTank.Draw(g);
        }

        public void SetPlayerController(PlayerTankController playerController)
        {
            this.playerController = playerController;
        }
        
        int GetDirection(int difference)
        {
            if (difference >= 0) return 1;
            else return -1;
        }

        int Abs(int value)
        {
            if (value >= 0) return value;
            else return -value;
        }
    }

    internal class Tank
    {
        ITankBody body;
        int health;
        Position position;
        Image image;

        public Tank(ITankBody Body, Position position, Image image)
        {
            this.body = Body;
            this.health = body.Durability;
            this.position = position;
            this.image = image;
        }
        //ammmo


        //health
        //position
        //speed


        public void Move(int x, int y)
        {
            position.x += x;
            position.y += y;
        }


        public bool GetHit(int damage)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("health {0}, damage {1}, body durability {2}, helth/durabilit {3}", health, damage, body.Durability, health/body.Durability));
            health -= damage;
            //if (health <= 0) { return false; }
            //else { return true; }
            return health > 0;
        }
        
        //shoot

        //reload()
        
        public Position GetPosition() { return position; }

        public void Draw(Graphics g)
        {
            g.DrawImage(image, position.x * 50, position.y * 50, 50, 50);
            g.FillRectangle(new SolidBrush(Color.Red), position.x * 50 + 3, position.y * 50+ 3, 44, 6);
            g.FillRectangle(new SolidBrush(Color.Lime), position.x * 50 + 3, position.y * 50 + 3, (int)((float)44 * ((float)health/(float)body.Durability)), 6);
        }
    }
}
