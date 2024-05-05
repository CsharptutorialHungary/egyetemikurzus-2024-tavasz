using V2JQM3.Infrastructure;
using V2JQM3.UserInterface;
using System;
using System.IO;
using System.Net.Http;
using System.Diagnostics;
using System.Xml;
using System.Collections;
using System.Xml.Linq;
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

    példa rss url-ek:
    https://prohardver.hu/hirfolyam/hirek/rss.xml
    https://itcafe.hu/hirfolyam/hirek/rss.xml
    https://mobilarena.hu/hirfolyam/hirek/rss.xml
    https://gamepod.hu/hirfolyam/hirek/rss.xml
*/

    private static async Task Main(string[] args)
    {
        var ui = new Ui(new ReflectionCMDLoader(), new Host());
        ui.Run();
    }
}