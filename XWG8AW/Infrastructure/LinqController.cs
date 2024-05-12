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
        UserDeserializer allUserJson = new UserDeserializer();

        public async Task<String> BestScoreByPlayer(string user)
        {
            List<User> allUser = await allUserJson.UserDeserializeFromJson();

            var bestScore = allUser.Where(x => x.UserName == user).OrderByDescending(x => x.Score).FirstOrDefault();

            if (bestScore != null)
            {
                return bestScore.ToString();
            }

            return "Nem talalhato ilyen jatekos!";
        }

        public async Task<IOrderedEnumerable<User>> AllScore()
        {
            List<User> allUser = await allUserJson.UserDeserializeFromJson();

            IOrderedEnumerable<User> allScore = allUser.OrderByDescending(x => x.Score);

            if (allScore != null)
            {
                return allScore;
            }

            return null;
        }
    }
}
