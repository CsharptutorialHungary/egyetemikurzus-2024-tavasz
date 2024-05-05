using V2JQM3.Infrastructure;
using V2JQM3.UserInterface;

internal class Program
{
    /*
    Feladat leírás:
    RSS downloader - RSS fájl letöltése a hálózatról és a Headline címek megjelenítése számozva. 
    A user a szám megadásával a cikkre navigálódik.
        
    Nem kihagyható elemek:
        -Legyen benne kivételkezelés (try-catch)
        -Legalább a képenyőre írjon ki hibaüzeneteket
    Kötelezelő elemek:
        -adat olvasása fájlból szerializáció segítségével (pl.: Adat betöltés és/vagy mentés JSON/XML fájlból/fájlba)
        -legyen benne saját immutable type (pl.: record class)
        -legyen benne LINQ segítségével: szűrés (where), csoportosítás (group by), rendezés (order by), agregáció (Pl.: Min(), Max(), First(), FirstOrDefault, Average(), stb...) közül legalább kettő
        -legyen benne generikus kollekció (pl.: List, Stack, stb...)
        -legyen benne aszinkron rész (async és Task)
*/

    private static async Task Main(string[] args)
    {
        var ui = new Ui(new ReflectionCMDLoader(), new Host());
        ui.Run();
    }
}