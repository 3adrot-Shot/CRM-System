using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Table
{
    internal class PostgreSQL
    {
        static public NpgsqlConnection PGSQL = null;
        static string ConPSQL = $"Server={Properties.Settings.Default.ParamServer};Port={Properties.Settings.Default.ParamPort};User Id={Properties.Settings.Default.ParamUserId};Password={Properties.Settings.Default.Password};Database={Properties.Settings.Default.Database};";
        public static bool Connect()
        {
            try
            {
                PGSQL = new NpgsqlConnection(connectionString: ConPSQL);
                PGSQL.Open();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static void Close()
        {
            PGSQL.Close();
        }
        public static string Auth(string TableName, string Login, string Password)
        {
            Close();
            var cmd = new NpgsqlCommand();
            if (PGSQL.State != System.Data.ConnectionState.Open) {
                Connect();
            }
            cmd.Connection = PGSQL;
            cmd.CommandText = $"SELECT id, last_name, first_name, patronymic, email, avatar_image_id, permissions, active FROM \"public\".\"user\" WHERE \"login\" = '" +@Login+"' AND \"password\" = '"+@Password+"' LIMIT 2;";
            int sums = 0;
            try
            {
                NpgsqlDataReader reader = cmd.ExecuteReader();
                string[] saveParams = new string[8];
                while (reader.Read())
                {
                    sums++;
                    for (int i = 0; i< saveParams.Length; i++)
                    {
                        saveParams[i] = $"{reader[i]}";
                    }
                }
                if (sums == 1)
                {
                    Properties.Settings.Default.user_AuthId = "" + saveParams[0];
                    Properties.Settings.Default.user_last_name = "" + saveParams[1];
                    Properties.Settings.Default.user_first_name = "" + saveParams[2];
                    Properties.Settings.Default.user_patronymic = "" + saveParams[3];
                    Properties.Settings.Default.user_email = "" + saveParams[4];
                    Properties.Settings.Default.user_avatar_image_id = "" + saveParams[5];
                    Properties.Settings.Default.user_permissions = "" + saveParams[6];
                    Properties.Settings.Default.user_active = "" + saveParams[7];
                    Properties.Settings.Default.Save();
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
            catch (Exception ex)
            {
                return "error";
            }
        }
        public static string GetProjectList(string id)
        {

            return null;
        }
    }
}
