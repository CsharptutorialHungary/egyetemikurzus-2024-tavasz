using System.Data.SQLite;
using WHBNDL.Domain;
using System.Linq;

namespace WHBNDL.Database
{
    internal class MemoryDatabase
    {
        private SQLiteConnection _connection;

        public MemoryDatabase()
        {
            _connection = new SQLiteConnection("Data Source=:memory:");
            _connection.Open();

            InitializeSchema();
        }

        private void InitializeSchema()
        {
            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Questions (
                    Id INTEGER PRIMARY KEY,
                    QuestionText TEXT NOT NULL,
                    CorrectAnswer TEXT NOT NULL,
                    WrongAnswers TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS QuizResults (
                    Id INTEGER PRIMARY KEY,
                    CorrectAnswers INTEGER NOT NULL,
                    TotalQuestions INTEGER NOT NULL,
                    Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP
                );
            ";

            using (var command = new SQLiteCommand(createTableQuery, _connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public async Task SaveQuizResultAsync(int correctAnswers, int totalQuestions)
        {
            string saveResultQuery = @"
                INSERT INTO QuizResults (CorrectAnswers, TotalQuestions) 
                VALUES (@CorrectAnswers, @TotalQuestions);
            ";

            using (var command = new SQLiteCommand(saveResultQuery, _connection))
            {
                command.Parameters.AddWithValue("@CorrectAnswers", correctAnswers);
                command.Parameters.AddWithValue("@TotalQuestions", totalQuestions);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<Dictionary<int, List<QuizResult>>> ListQuizResultsAsync()
        {
            List<QuizResult> results = new List<QuizResult>();

            string query = "SELECT CorrectAnswers, TotalQuestions, Timestamp FROM QuizResults;";

            using (var command = new SQLiteCommand(query, _connection))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        int correctAnswers = Convert.ToInt32(reader["CorrectAnswers"]);
                        int totalQuestions = Convert.ToInt32(reader["TotalQuestions"]);
                        DateTime timestamp = Convert.ToDateTime(reader["Timestamp"]);

                        results.Add(new QuizResult(correctAnswers, totalQuestions, timestamp));
                    }
                }
            }

            var orderedGroupedResults = results
                .GroupBy(r => r.CorrectAnswers)
                .OrderByDescending(g => g.Key)
                .ToDictionary(g => g.Key, g => g.ToList());

            return orderedGroupedResults;
        }
        public async Task<QuizResult> GetBestQuizResultAsync()
        {
            string selectQuery = @"
                SELECT * FROM QuizResults;
                ";

            List<QuizResult> quizResults = new List<QuizResult>();

            using (var command = new SQLiteCommand(selectQuery, _connection))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        quizResults.Add(new QuizResult(
                        Convert.ToInt32(reader["CorrectAnswers"]),
                        Convert.ToInt32(reader["TotalQuestions"]),
                        Convert.ToDateTime(reader["Timestamp"])
                        ));
                    }
                }
            }

            if (!quizResults.Any())
            {
                return new QuizResult("No quiz results found.");
            }

            var bestResult = quizResults
                .OrderByDescending(q => (double)q.CorrectAnswers / q.TotalQuestions)
                .FirstOrDefault();

            return bestResult;
        }

        public void Close()
        {
            _connection.Close();
        }
    }
}
