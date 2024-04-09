namespace DownloadManager.Infrastructure
{
    /// <summary>
    /// Definiálja a felhasználói input, illetve a kiíratásokhoz használt metódusokat.
    /// </summary>
    public interface IHost
    {
        /// <summary>
        /// Beolvas egy sort a felhasználótól.
        /// </summary>
        /// <returns>Felhasználói bemenet</returns>
        string ReadLine();

        /// <summary>
        /// Kiíratja a felhasználónak az átadott üzenetet sortöréssel a végén.
        /// </summary>
        /// <param name="message">Kimenő üzenet</param>
        void WriteLine(string message);

        /// <summary>
        /// Kiíratja a felhasználónak az átadott üzenetet sortörés nélkül.
        /// </summary>
        /// <param name="message">Kimenő üzenet</param>
        void Write(string message);

        /// <summary>
        /// Befejezi a program működését.
        /// </summary>
        void Exit();
    }
}