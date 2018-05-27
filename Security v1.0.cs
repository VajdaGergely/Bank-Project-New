using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

/// <summary>
/// Summary description for Security
/// </summary>
//public static class Security
namespace Security
{
    public enum RoleType
    {
        None = 0,
        User = 1,
        Admin = 2,
        System = 3
    }

    public class Client
    {
        //noneCode = '0';
        //userCode = '1';
        //adminCode = '2';

        //A RoleName és RoleCode párhumzamos működnek egymással.
        //Ha bármelyik értéke megváltozik, akkor hozzáigazítjuk a másik értékét is.
        //Az értékeik párban vannak.

        public string ClientId { get; set; }
        public RoleType Role { get; set; }
        

        public Client()
        {
            ClientId = "";
            Role = RoleType.None;
            //A RoleCode-ot nem kell beállítanunk már, mert a kettő automatikusan szinkronizálja egymást
        }

        public Client(string clientId)
        {
            ClientId = clientId;
            Role = RoleType.None;
        }

        public Client(RoleType roleName)
        {
            ClientId = "";
            Role = roleName;
        }

        public Client(string clientId, RoleType roleName)
        {
            ClientId = clientId;
            Role = roleName;
        }
    }

    public class Security
    {
        private static readonly string connectionString = WebConfigurationManager.ConnectionStrings["JPMorganBankDB"].ConnectionString;

        private static char ConvertRoleTypeToRoleCode(RoleType role)
        {
            switch (role)
            {
                case RoleType.None:
                    return '0';
                case RoleType.User:
                    return '1';
                case RoleType.Admin:
                    return '2';
                case RoleType.System:
                    return '3';
                default:
                    return '0';
            }
        }

        public static RoleType ConvertRoleCodeToRoleType(char role)
        {
            switch (role)
            {
                case '0':
                    return RoleType.None;
                case '1':
                    return RoleType.User;
                case '2':
                    return RoleType.Admin;
                case '3':
                    return RoleType.System;
                default:
                    return RoleType.None;
            }
        }


        //Lecsekkolj a lognevet és jelszót - client_id és role-t ad vissza vagy hibát
        private static Dictionary<string, string> CheckLognameAndPassword(string logname, string password)
        {
            string sqlString = "SELECT client_id, role FROM Log WHERE logname = @logname AND pass = @password";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sqlString, con);
            SqlDataReader reader;
            Dictionary<string, string> result = new Dictionary<string, string>();

            try
            {
                cmd.Parameters.AddWithValue("@logname", logname);
                cmd.Parameters.AddWithValue("@password", password);
                con.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    result["client_id"] = reader["client_id"].ToString();
                    result["role"] = reader["role"].ToString();

                }
                else
                {
                    result["err"] = "Hibás felhasználónév vagy jelszó!";
                }
            }
            catch
            {
                result = new Dictionary<string, string>();
                result["err"] = "Ismeretlen hiba!";
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        //Nem naplózással kapcsolatos "Log", hanem autentikációval kapcsolatos
        private static string CreateLogRecord(string client_id, string logname, string password, string role)
        {
            string sqlString = "INSERT INTO Log (client_id, logname, pass, role) VALUES (@client_id, @logname, @password, @role);";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sqlString, con);
            string result = "";
            try
            {
                cmd.Parameters.AddWithValue("@client_id", client_id);
                cmd.Parameters.AddWithValue("@logname", logname);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@role", role);
                con.Open();
                result = cmd.ExecuteNonQuery().ToString();
            }
            catch (Exception err)
            {
                result = err.Message;
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        //Ha sikeres a bejelentkezés, akkor egy User objektumot ad vissza
        //(Ha sikertelen akkor is csak akkor üres lesz)
        public static Client Logon(string logname, string pass)
        {
            Dictionary<string, string> result = CheckLognameAndPassword(logname, pass);
            //Ha létezik a lognév + jelszó -> az ügyfél sikeresen hitelesítetette magát
            if (result.ContainsKey("client_id") && result.ContainsKey("role"))
            {
                //vissza küldünk egy client objectet
                return new Client(result["client_id"], ConvertRoleCodeToRoleType(result["role"].ToCharArray()[0]));
            }
            else
            {
                //visszaküldünk egy üres client objectet
                return new Client();
            }
        }

        public static void Registration(string client_id, string logName, string password, Client client)
        {
            CreateLogRecord(client_id, logName, password, ConvertRoleTypeToRoleCode(client.Role).ToString());
        }
    }
}