using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BFR0QN
{
    public class GameManager
    {
        private static GameManager _instance;
        private int _level;
        private List<Food> foods;
        private Dictionary<string, int> saveList;
        private GameManager()
        {
            foods = ReadJsonFile.ReadJsonFileToList("Etelek.json");
        }
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }
        public int Level {get{ return _level;} }
        public async Task Load()
        {
            saveList = await ReadJsonFile.LoadTheGame();
            Console.WriteLine("Siti Étteremje!");
            Console.WriteLine("Új játék (uj) Meglévő betöltése (be)");
            string read = Console.ReadLine();
            if (read == "uj")
            {
                _level = 1;
                Presentation();
            }
            else if (read == "be")
            {
                LoadSavedGame();
            }
            else
            {
                Console.WriteLine("Rossz kifejezés");
                Load();
            }
        }
        public void LoadSavedGame()
        {
            if (saveList.Count > 0)
            {
                foreach (var save in saveList)
                {
                    Console.WriteLine(save.Key);
                }
                Console.Write("Az alábbiak közül melyiket szeretnéd betölteni? : ");
                string nev = Console.ReadLine();
                if (saveList.Keys.Contains(nev))
                {
                    _level = saveList[nev];
                }
                else
                {
                    Console.WriteLine("Nem megfelelően írtad be!");
                    LoadSavedGame();
                }
            }
            else
            {
                Console.WriteLine("Jelenleg nincs megkezdett játék!");
                Load();
            }
        }
        public void Presentation()
        {
            Console.WriteLine("Ebben a játékban ételeket\nfogsz elkészíteni kérésekre. " +
                              "Mindig fog jönni egy vásárló, kér egy kaját\n" +
                              "és neked azt pontosan el kell\n" +
                              "tudnod készsíteni. Alapvetőn szavakat kell beütnöd egyesével," +
                              "ha nem tudod az adott éltel felépítését,\n" +
                              "akkor használhatsz segítséget ?etel commanddal\n" +
                              "ami 3 mp megjeleníti az adott étel összetevőit\n" +
                              "Ok? Nyomj meg egy gombot...");
            string s = Console.ReadLine();
            if (s != null)
            {
                Console.Clear();
            }
        }
        public Food NextLevel(int level)
        {
            Food currentFood = foods.FirstOrDefault(x => x.Level == level);
            return currentFood;
        }
        public String AvarageFood()
        {
            int[] foodArray=new int[foods.Count];
            int i = 0;
            foreach (var food in foods)
            {
                foodArray[i]=food.Kcal; i++;
            }
            double avarage=foodArray.Average();
            String summaryAvarageFood = $"Az ételek átlagosan {avarage} kalóriával rendelkeznek";
            return summaryAvarageFood;
        }
        public void Help(String[] currentFood)
        {
            for (int k = 0; k < currentFood.Length; k++)
            {
                int isLastFood = k + 1;
                if (isLastFood == currentFood.Length)
                {
                    Console.Write(currentFood[k]);
                }
                else
                {
                    Console.Write(currentFood[k] + " ");
                }
            }
            Thread.Sleep(3000);
            Console.Clear();
        }
        public bool IsSave(string saveName, int currentLevel)
        {
            string name = saveName.Trim();
            if (name == null || name.Length == 0)
            {
                Console.WriteLine("Nem adtál meg nevet");
                return false;

            }
            if (saveList.Keys.Contains(saveName))
            {
                Console.Write("Van már ilyen mentés, Szeretnéd felülirni? igen (i), nem (n) : ");
                string doYouWantToOverride = Console.ReadLine();
                if (doYouWantToOverride == "i")
                {
                    saveList[saveName] = currentLevel;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                saveList.Add(saveName, currentLevel); 
                return true;
            }
        }
        public void CreateJsonFileToSaveTheGame() {
            SaveTheGameToJson.Save(saveList);
        }
    }
}
