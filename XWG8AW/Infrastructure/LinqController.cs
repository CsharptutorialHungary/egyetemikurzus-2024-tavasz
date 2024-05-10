using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using XWG8AW.Domain;

namespace XWG8AW.Infrastructure
{
    internal class LinqController
    {
        public async Task<String> BestScore(string user)
        {
            UserDeserializer allUserJson = new UserDeserializer();

            List<User> allUser = await allUserJson.UserDeserializeFromJson();

            var bestScore = allUser.Where(x => x.UserName == user).GroupBy(x => x.UserName).OrderBy(x => x).FirstOrDefault();

            if (bestScore != null)
            {
                return bestScore.ToString();
            }

            return "Nem található ilyen játékos!";
        }
    }
}
