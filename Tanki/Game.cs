using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanki
{
    internal class Game
    {
        Map map;
        
        public PlayerTankController playerTankController;
        public EnemyTankController enemyTankController;
        public void StartGame()
        {
            map = new Map();
            map.GenerateMap(20);

            
            playerTankController = new PlayerTankController(map);
            enemyTankController = new EnemyTankController(map);
            
            playerTankController.SetEnemyTankController(enemyTankController);
            enemyTankController.SetPlayerController(playerTankController);
        }

        public void Draw(Graphics g)
        {
            map.Draw(g);
            playerTankController.Draw(g);
            enemyTankController.Draw(g);
        }

        //players
        //turn

        //draw
        //nextturn
        //checkwin
    }
}
