using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BFR0QN.Burger;

namespace BFR0QN
{
    internal class GameManager
    {
        public void Betolt()
        {
            Console.WriteLine("Siti Hamburgerezője!");
            Console.WriteLine("Új játék (uj) Meglévő betöltése (be)");
            string beolvas = Console.ReadLine();
            if (beolvas == "uj")
            {
                UjJatek();
            }
            else if (beolvas == "be")
            {
                //TODO
            }
            else
            {
                Console.WriteLine("Rossz kifejezés");
                Betolt();
            }
        }
        public void UjJatek()
        {
            Bevezetes();
        }
        public void Bevezetes()
        {
            Console.WriteLine("Ebben a játékban burgereket\nfogsz elkészíteni kérésekre. " +
                              "Mindig fog jönni egy vásárló, kér egy burgert\n" +
                              "és neked azt pontosan el kell\n" +
                              "tudnod készsíteni. Alapvetőn szavakat kell beütnöd egyesével," +
                              "ha nem tudod az adott burger felépítését,\n" +
                              "akkor használhatsz segítséget ?burgerNeve commanddal\n" +
                              "ami 3 mp megjeleníti az adott burger elkészítését\n" +
                              "Ok? Nyomj meg egy gombot...");
            string s = Console.ReadLine();
            if (s != null)
            {
                Console.Clear();
            }
        }
        public Hamburger kovetkezoSzint(int szint)
        {
            String[] pistiBurger = ["pistiBurger","zsemle", "marhahúspogácsa", "sajt", "ketchup"];
            String[] hamburger = ["hamburger","zsemle", "marhahúspogácsa", "uborka", "hagyma", "ketchup"];
            String[] sajtosMcRoyal = ["sajtosMcRoyal","zsemle", "marhahúspogácsa", "sajt", "sajt", "uborka", "hagyma", "ketchup", "mustár"];
            String[] sertesMcFarm = ["sertesMcFarm","zsemle", "marhahúspogácsa", "sajtszelet", "marhahúspogácsa", "jégsaláta", "hagyma", "paradicsom", "mustár"];
            Hamburger Burger;
            if (szint == 0) {
                Burger = feltolt(pistiBurger.Length, pistiBurger[0], pistiBurger);
            }
            if(szint == 1)
            {
                Burger = feltolt(hamburger.Length, hamburger[0], hamburger);
            }
            if (szint == 2)
            {
                Burger = feltolt(sajtosMcRoyal.Length, sajtosMcRoyal[0], sajtosMcRoyal);
            }
            else
            {
                Burger = feltolt(sajtosMcRoyal.Length, sertesMcFarm[0], sertesMcFarm);
            }
            return Burger;
        }
        private Hamburger feltolt(int elemszam,string nev, string[] koviBurger)
        {
            Hamburger burger = new Hamburger(elemszam, nev);
            for (int i = 1; i < elemszam; i++)
            {
                burger.Add(koviBurger[i]);
            }
            return burger;
        }
    }
}
