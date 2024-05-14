using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFR0QN
{
    public static class GameManagerTests
    {
        //having Convention - ClassName_MethodName_ExceptedResult
        public static void GameManager_NextLevel_ReturnFood()
        {
            try
            {
                //Arrange
                int level = 1;
                GameManager gameManager = GameManager.Instance;
                //Act
                Food result=gameManager.NextLevel(level);
                //Assert
                if(result.Name == "pistiHotdog")
                {
                    Console.WriteLine("PASSED: GameManager_NextLevel_ReturnFood()");
                }
                else
                {
                    Console.WriteLine("Failed: GameManager_NextLevel_ReturnFood()");
                }

            }catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void GameManager_AvarageFood_ReturnString()
        {
            try
            {
                //Arrange
                GameManager gameManager = GameManager.Instance;
                String text = "Az ételek átlagosan 294 kalóriával rendelkeznek";
                //Act
                String result=gameManager.AvarageFood();
                //Assert
                if (result==text)
                {
                    Console.WriteLine("PASSED: GameManager_AvarageFood()");
                }
                else
                {
                    Console.WriteLine("Failed: GameManager_AvarageFood()");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void GameManager_Help()
        {
            try
            {
                //Arrange
                GameManager gameManager = GameManager.Instance;
                String text = "Az ételek átlagosan 294 kalóriával rendelkeznek";
                //Act
                String result = gameManager.AvarageFood();
                //Assert
                if (result == text)
                {
                    Console.WriteLine("PASSED: GameManager_AvarageFood()");
                }
                else
                {
                    Console.WriteLine("Failed: GameManager_AvarageFood()");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
