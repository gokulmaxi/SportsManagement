using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsManagement
{
    internal class SportsMangementConsole
    {
        private SqlConnection conn;
        public SportsMangementConsole() {
            conn = new SqlConnection("Data Source=localhost;Initial Catalog=assementDb;Integrated Security=True;Encrypt=False");
            conn.Open();
            while (true)
            {
            giveMainPrompt();
            var opt = Console.ReadLine();
                Console.Clear();
                switch (opt)
                {
                    case "a":
                        viewSports();
                        break;
                    case "b":
                        Console.WriteLine("Enter your Sports Name");
                        string input = Console.ReadLine();
                        AddSport(input);
                        break;
                    case "c":
                        deleteSport();
                        break;
                    case "d":
                        viewTournaments();
                        break;
                    case "e":
                        AddTournament();
                        break;
                    case "f":
                        deleteTournament();
                        break;
                    case "g":
                        viewScoreCards();
                        break;
                    case "h":
                        AddScoreCard();
                        break;
                    case "i":
                        UpdateScoreCard();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }
            }
        }
        public static void giveMainPrompt()
        {
            Console.WriteLine(@"Enter any option
                a) View Sports
                b) Add sports
                c) Delete Sport
                d) View Tournament
                e) Add Tournament
                f) Delete Tournament
                g) View score card
                h) add score card
                i) update score card
                0) Exit");
        }
        public void viewSports()
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM sports";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("ID " + reader.GetInt32(0) + "  name:  " + reader.GetString(1));
            }
            reader.Close();
        }
        public void AddSport(string sports)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = $"insert into sports values('{sports}')";
            cmd.ExecuteReader().Close();
        }
        public void deleteSport()
        {
            viewSports();
            Console.WriteLine("Enter the Id of the sport you want to delete");
            string id= Console.ReadLine();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = $"DELETE FROM sports where sportsId= {id}";
            cmd.ExecuteReader().Close();

        }
        public void viewTournaments()
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT tournaments.tournementID ,tournaments.tournamentName,sports.sportsName " +
                "FROM tournaments INNER JOIN sports ON tournaments.sportsID=sports.sportsId";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("ID " + reader.GetInt32(0) + " Tournament name:  " + 
                    reader.GetString(1)+ " Sport Name "+reader.GetString(2));
            }
            reader.Close();
        }
        public void AddTournament()
        {
            string eventType = "Group";
            SqlCommand cmd = conn.CreateCommand();
            Console.WriteLine("Enter the tournament name");
            string tournamentName = Console.ReadLine();
            viewSports();
            Console.WriteLine("Enter the sports Id of your tournament");
            string sportId = Console.ReadLine();
            Console.WriteLine("Is this a group event[Y/n]");
            string eventTypeBool = Console.ReadLine();
            if(eventTypeBool == "n" || eventTypeBool == "N")eventType= "Individual";
            cmd.CommandText = $"insert into tournaments values('{tournamentName}','{sportId}','{eventType}')";
            cmd.ExecuteReader().Close();
            conn.Close();
        }
        public void deleteTournament()
        {
            viewTournaments();
            Console.WriteLine("Enter the Id of the tournament you want to delete");
            string id= Console.ReadLine();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = $"DELETE FROM tournaments where tournementID= {id}";
            cmd.ExecuteReader().Close();
        }
        public void viewScoreCards()
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT scoreCardID,MatchName,teamAScore,teamBScore,winner FROM scoreCard";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("ID " + reader.GetInt32(0) + " Match name:  " + reader.GetString(1)+ 
                    " Team A Score: "+reader.GetInt32(2)+" Team B Score: "+reader.GetInt32(3)+
                    " Winner: "+ reader.GetString(4));
            }
            reader.Close();
        }
        public void AddScoreCard()
        {
            SqlCommand cmd = conn.CreateCommand();
            Console.WriteLine("Enter the match name");
            string matchName = Console.ReadLine();
            viewTournaments();
            Console.WriteLine("Enter the Tournament Id of your match");
            string tournamentId= Console.ReadLine();
            cmd.CommandText = $"insert into scoreCard values('{matchName}','0','0','Draw','{tournamentId}')";
            cmd.ExecuteReader().Close();
            conn.Close();
        }
        public void UpdateScoreCard()
        {
            SqlCommand cmd = conn.CreateCommand();
            viewScoreCards();
            string winner = "Draw";
            Console.WriteLine("Enter the id of Score card to update");
            string id = Console.ReadLine();
            Console.WriteLine("Enter the score of team A and Team B seperated by space ex: [ 1 0 ]");
            string score = Console.ReadLine();
            int TeamAScore =Int32.Parse( score.Split(" ")[0]);
            int TeamBScore = Int32.Parse(score.Split(" ")[1]);
            if (TeamAScore != TeamBScore) winner = TeamAScore > TeamBScore ? "Team A" : "Team B";
            cmd.CommandText = $"UPDATE scoreCard set teamAScore = '{TeamAScore}',teamBScore = '{TeamBScore}'" +
                $",winner='{winner}' WHERE scoreCardID = '{id}'";
            cmd.ExecuteReader().Close();
        }
        public void deleteScoreCard()
        {
            viewScoreCards();
            Console.WriteLine("Enter the Id of the ScoreCard you want to delete");
            string id= Console.ReadLine();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = $"DELETE FROM scoreCard where scoreCardID= {id}";
            cmd.ExecuteReader().Close();
        }
    }
}
