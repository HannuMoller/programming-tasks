using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace WpfSkiJumping
{
    public class TournamentUtilities
    {

        private static MySqlConnection GetConnection()
        {
            var connectionString = "Server=localhost;Database=ekoodi;Uid=mariadmin;Pwd=data_admin;";
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        private static void CloseConnection(MySqlConnection connection)
        {
            connection.Close();
        }

        /// <summary>
        /// Fetch data for all athletes in the database
        /// </summary>
        /// <returns></returns>
        public static List<Athlete> GetAthletes()
        {
            var athletes = new List<Athlete>();

            var connection = GetConnection();

            var sql = "SELECT id,name,country FROM athletes ORDER BY id";
            var cmd = new MySqlCommand(sql, connection);
            var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                athletes.Add(new Athlete(rdr.GetInt16(0), rdr.GetString(1), rdr.GetString(2)));
            }

            rdr.Close();

            CloseConnection(connection);

            return athletes;
        }

        /// <summary>
        /// Fetch data for all hills in the database
        /// </summary>
        /// <returns></returns>
        public static List<Hill> GetHills()
        {
            var hills = new List<Hill>();

            var connection = GetConnection();

            var sql = "SELECT name,city,kpoint,hspoint,longest_jump,lowest_gate,highest_gate,gate_gap,gate_factor,wind_factor_head,wind_factor_tail,factor_type FROM hills ORDER BY name";
            var cmd = new MySqlCommand(sql, connection);
            var rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                hills.Add(new Hill(rdr.GetString(0), rdr.GetString(1), rdr.GetInt16(2), rdr.GetInt16(3), rdr.GetFloat(4), rdr.GetInt16(5), rdr.GetInt16(6), rdr.GetFloat(7), rdr.GetFloat(8), rdr.GetFloat(9), rdr.GetFloat(10), rdr.GetChar(11)));
            }

            rdr.Close();

            CloseConnection(connection);

            return hills;
        }


    }
}
