using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text;

/// <summary>
/// Summary description for Layer1_new
///
/// </summary>



//public static class DataLayer
namespace DataLayer
{
    //Később ezt fogjuk majd használni a sima '0' betűzések helyett...
    //Kell hozzá egy konvertáló függvény is majd...
    //Az sql stringekbe majd a konvertáló függvénnyel illesztjük bele
    internal enum LiveStatus
    {
        Inactive = 0,
        Active = 1
    }

    public enum CommandType
    {
        None,
        Execute,
        List,
        GetRow,
        GetField
    }

    public class Technical
    {
        public static string[] ClientDataBaseLabels = new string[]
        {
            "id",
            "f_name",
            "l_name",
            "d_birth",
            "p_birth",
            "name_of_m",
            "address",
            "phone",
            "email"
        };

        public static string[] ClientParameterLabels = new string[]
        {
            "id",
            "firstName",
            "lastName",
            "dateOfBirth",
            "placeOfBirth",
            "nameOfMother",
            "address",
            "phone",
            "email"
        };

        public static string[] AccountDataBaseLabels = new string[]
        {
            "id",
            "acc_number",
            "acc_name",
            "acc_type",
            "curr",
            "sector_type",
            "balance",
            "date_of_op"
        };

        public static string[] AccountParameterLabels = new string[]
        {
            "id",
            "accountNumber",
            "accountName",
            "accountType",
            "currency",
            "sectorType",
            "balance",
            "dateOfOpening"
        };

        public static string[] RoleDataBaseLabelsLabels = new string[]
        {
            "client_id",
            "acc_id",
            "role"
        };

        public static string[] RoleParameterLabels = new string[]
        {
            "clientID",
            "accountID",
            "role"
        };

        public static string[] TransactionDataBaseLabelsLabels = new string[]
        {
            "id",
            "trans_type",
            "src_acc_num",
            "dst_acc_num",
            "acc_num",
            "src_name",
            "dst_name",
            "amount",
            "curr",
            "date",
            "time",
            "msg"
        };

        public static string[] TransactionParameterLabels = new string[]
        {
            "id",
            "transactionType",
            "sourceAccountNumber",
            "destinationAccountNumber",
            "accountNumber",
            "sourceName",
            "destinationName",
            "amount",
            "currency",
            "date",
            "time",
            "message"
        };

        public static string[] CardDataBaseLabelsLabels = new string[]
        {
            "id",
            "client_id",
            "acc_id",
            "type",
            "card_num",
            "curr",
            "deb_cred",
            "date_of_req",
            "date_ofexp"
        };

        public static string[] CardParameterLabels = new string[]
        {
            "id",
            "clientID",
            "accountID",
            "type",
            "cardNumber",
            "currency",
            "debitCredit",
            "dateOfRequest",
            "dateOfExpiration"
        };

        public static Dictionary<string, object> ConvertParameterNamesToDataBaseNames(Dictionary<string, object> parameters, string[] parameterNameList, string[] dataBaseNameList)
        {
            //Később ezt máshogy fogjuk megoldani
            Dictionary<string, object> result = new Dictionary<string, object>();
            for(int i = 0; (i < parameterNameList.Length && i < dataBaseNameList.Length); i++)
            {
                if (parameters.ContainsKey(parameterNameList[i]))
                {
                    result.Add(dataBaseNameList[i], parameters[parameterNameList[i]]);
                }
            }
            return result;
        }

        public static DataSet ConvertDataBaseNamesToParameterNames(DataSet ds, string[] dataBaseNameList, string[] parameterNameList)
        {
            DataSet result = new DataSet();
            result.Tables.Add(new DataTable());
            DataRow row = result.Tables[0].NewRow();
            for (int i = 0; (i < parameterNameList.Length && i < dataBaseNameList.Length); i++)
            {
                if (ds.Tables[0].Columns.Contains(dataBaseNameList[i]))
                {
                    result.Tables[0].Columns.Add(parameterNameList[i]);
                    row[parameterNameList[i]] = ds.Tables[0].Rows[0][dataBaseNameList[i]];
                }
            }
            result.Tables[0].Rows.Add(row);
            return result;
        }
    }

    public class Command
    {
        public CommandType Type { get; set; }

        public string SqlString { get; set; }

        public Dictionary<string, object> Parameters { get; set; }

        public Command()
        {
            Type = CommandType.None;
            SqlString = "";
            Parameters = new Dictionary<string, object>();
        }

        public Command(CommandType type)
        {
            Type = type;
            SqlString = "";
            Parameters = new Dictionary<string, object>();
        }

        public Command(string sqlString)
        {
            Type = CommandType.None;
            SqlString = sqlString;
            Parameters = new Dictionary<string, object>();
        }

        public Command(Dictionary<string, object> parameters)
        {
            Type = CommandType.None;
            SqlString = "";
            Parameters = new Dictionary<string, object>();
            //Nem biztos, hogy ez működni fog, mert ez egy sima paraméter, amit a semmiből csináltunk
            Parameters = parameters;
        }

        public Command(CommandType type, string sqlString)
        {
            Type = type;
            SqlString = sqlString;
            Parameters = new Dictionary<string, object>();
        }

        public Command(CommandType type, Dictionary<string, object> parameters)
        {
            Type = type;
            SqlString = "";
            Parameters = new Dictionary<string, object>();
            //Nem biztos, hogy ez működni fog, mert ez egy sima paraméter, amit a semmiből csináltunk
            Parameters = parameters;
        }

        public Command(string sqlString, Dictionary<string, object> parameters)
        {
            Type = CommandType.None;
            SqlString = sqlString;
            Parameters = new Dictionary<string, object>();
            //Nem biztos, hogy ez működni fog, mert ez egy sima paraméter, amit a semmiből csináltunk
            Parameters = parameters;
        }

        public Command(CommandType type, string sqlString, Dictionary<string, object> parameters)
        {
            Type = type;
            SqlString = sqlString;
            Parameters = new Dictionary<string, object>();
            //Nem biztos, hogy ez működni fog, mert ez egy sima paraméter, amit a semmiből csináltunk
            Parameters = parameters;
        }
    }

    public static class SqlEngine
    {
        private static void AddCmdParameters(Dictionary<string, object> parameters, ref SqlCommand cmd)
        {
            foreach (KeyValuePair<string, object> param_var in parameters)
            {
                cmd.Parameters.AddWithValue(("@" + param_var.Key), param_var.Value);
            }
        }

        public static object Execute(Command command)
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["JPMorganBankDB"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(command.SqlString, con);
            object result = false;
            try
            {
                //Betöltjük a cmd-be az aktuális parancshoz tartozó paramétereket
                if (command.Parameters != null)
                {
                    AddCmdParameters(command.Parameters, ref cmd);
                }

                //Kapcsolatnyitás
                con.Open();

                switch(command.Type)
                {
                    case CommandType.Execute:
                        result = cmd.ExecuteNonQuery().ToString();
                        break;
                    case CommandType.List:
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        result = new DataSet();
                        adapter.Fill((DataSet)result, "table");
                        break;
                    case CommandType.GetRow:
                        SqlDataReader reader;
                        result = new Dictionary<string, object>();
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            foreach (KeyValuePair<string, object> field in reader)
                            {
                                ((Dictionary<string, object>)result).Add(field.Key, field.Value);
                            }
                        }
                        reader.Close();
                        break;
                    case CommandType.GetField:
                        result = cmd.ExecuteScalar();
                        if(result == null)
                        {
                            //hibakezelés kéne majd ide!!!
                        }
                        break;
                    case CommandType.None:
                        //hibakezelés
                        result = false;
                        break;
                }
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
    }

    public static partial class Client
    {
        public static class UserMethods
        {
            //Sima create
            public static string CreateClientRecord(Dictionary<string, object> parameters)
            {
                StringBuilder sqlString = new StringBuilder();
                sqlString.Append("INSERT INTO Clients(f_name, l_name, d_birth, p_birth, name_of_m, address, phone, email) VALUES (@f_name, @l_name, @d_birth, @p_birth, @name_of_m, @address, @phone, @email); ");
                sqlString.Append("DECLARE @id int; SET @id = (SELECT SCOPE_IDENTITY()); SELECT @id AS id;");
                return (SqlEngine.Execute(new Command(CommandType.GetField, sqlString.ToString(), Technical.ConvertParameterNamesToDataBaseNames(parameters, Technical.ClientParameterLabels, Technical.ClientDataBaseLabels)))).ToString();
            }

            //Sima modify
            public static void ModifyClientRecord(Dictionary<string, object> parameters)
            {
                string sqlString = "UPDATE Clients SET f_name = @f_name, l_name = @l_name, d_birth = @d_birth, p_birth = @p_birth, name_of_m = @name_of_m, address = @address, phone = @phone, email = @email WHERE id = @id;";
                SqlEngine.Execute(new Command(CommandType.Execute, sqlString, Technical.ConvertParameterNamesToDataBaseNames(parameters, Technical.ClientParameterLabels, Technical.ClientDataBaseLabels)));
            }

            //Sima select
            public static DataSet SelectClientRecordById(Dictionary<string, object> parameters)
            {
                string sqlString = "SELECT * FROM Clients WHERE id = @id;";
                return Technical.ConvertDataBaseNamesToParameterNames((DataSet)SqlEngine.Execute(new Command(CommandType.List, sqlString, Technical.ConvertParameterNamesToDataBaseNames(parameters, Technical.ClientParameterLabels, Technical.ClientDataBaseLabels))),Technical.ClientDataBaseLabels, Technical.ClientParameterLabels);
            }
        }
    }

    public static partial class Account
    {
        public static class UserMethods
        {
            //Account és Role rekordot hoz létre, live=1, role='OWNER' ezekkel a default értékekkel hozza létre
            public static string CreateAccountAndRoleRecords(Dictionary<string, object> parameters)
            {
                StringBuilder sqlString = new StringBuilder();
                sqlString.Append("INSERT INTO Accounts (acc_number, acc_name, acc_type, curr, sector_type, balance, date_of_op, live) VALUES (@acc_number, @acc_name, @acc_type, @curr, @sector_type, @balance, @date_of_op, '1'); ");
                sqlString.Append("DECLARE @acc_id int; SET @acc_id = (SELECT SCOPE_IDENTITY()); ");
                sqlString.Append("INSERT INTO Roles (client_id, acc_id, role, live) VALUES (@client_id, @acc_id, 'OWNER', '1'); ");
                sqlString.Append("SELECT @acc_id AS id;");
                return (SqlEngine.Execute(new Command(CommandType.GetField, sqlString.ToString(), Technical.ConvertParameterNamesToDataBaseNames(parameters, Technical.AccountParameterLabels, Technical.AccountDataBaseLabels)))).ToString();
            }

            //A törlés helyett inaktiváljuk a számlát és az összes hozzá tartozó role rekordot
            //Később ehhez a másik felhasználó engedélye is kell!
            public static void InactivateAccountAndAllConnectedRoleRecords(Dictionary<string, object> parameters)
            {
                string sqlString = "UPDATE Roles SET live='0' WHERE acc_id = @acc_id; UPDATE Accounts SET live='0' WHERE id = @acc_id";
                SqlEngine.Execute(new Command(CommandType.Execute, sqlString, Technical.ConvertParameterNamesToDataBaseNames(parameters, Technical.AccountParameterLabels, Technical.AccountDataBaseLabels)));
            }

            //Adott client id-hoz tartozó összes számlát listázza, ami élő számla
            public static DataSet SelectActiveAccountRecordsByClientId(Dictionary<string, object> parameters)
            {
                string sqlString = "SELECT * FROM Accounts WHERE (id in (SELECT acc_id FROM Roles WHERE client_id = @client_id AND live='1')) AND live='1'";
                string client_id = parameters["clientId"].ToString(); //kivesszük, mert a név konvertálásnál ezt ő már nem fordítaná le...
                parameters = Technical.ConvertParameterNamesToDataBaseNames(parameters, Technical.AccountParameterLabels, Technical.AccountDataBaseLabels);
                parameters.Add("client_id", client_id); //visszatesszük, mert a név konvertálásnál nem rakná vissza alapból...
                return Technical.ConvertDataBaseNamesToParameterNames((DataSet)SqlEngine.Execute(new Command(CommandType.List, sqlString, parameters)), Technical.AccountDataBaseLabels, Technical.AccountParameterLabels);
            }
        }
    }

    public static partial class Role
    {
        public static class UserMethods
        {
            //Új role rekordot hoz létre, a live flag-et alapból '1'-re állítjuk
            public static void CreateRoleRecordByClientId(Dictionary<string, object> parameters)
            {
                string sqlString = "INSERT INTO Roles (client_id, acc_id, role, live) VALUES (@client_id, @acc_id, @role, '1');";
                SqlEngine.Execute(new Command(CommandType.Execute, sqlString, Technical.ConvertParameterNamesToDataBaseNames(parameters, Technical.RoleParameterLabels, Technical.RoleDataBaseLabelsLabels)));
            }

            public static void CreateRoleRecordByClientPersonalDatas(Dictionary<string, object> parameters)
            {
                string sqlString = "INSERT INTO Roles (client_id, acc_id, role, live) VALUES (@client_id, @acc_id, @role, '1');";
                SqlEngine.Execute(new Command(CommandType.Execute, sqlString, Technical.ConvertParameterNamesToDataBaseNames(parameters, Technical.RoleParameterLabels, Technical.RoleDataBaseLabelsLabels)));
            }

            //Role rekord módosítása, csak a role mezőt piszkáljuk
            public static void ModifyRoleRecord(Dictionary<string, object> parameters)
            {
                string sqlString = "UPDATE Roles SET role = @role WHERE client_id = @client_id AND acc_id = @acc_id;";
                SqlEngine.Execute(new Command(CommandType.Execute, sqlString, Technical.ConvertParameterNamesToDataBaseNames(parameters, Technical.RoleParameterLabels, Technical.RoleDataBaseLabelsLabels)));
            }

            //Törlés helyett, csak ha marad 1, aki aktív!!!
            public static void InactivateRoleRecord(Dictionary<string, object> parameters)
            {
                string sqlString = "UPDATE Roles SET live = '0' WHERE client_id = @client_id AND acc_id = @acc_id AND ((SELECT COUNT(client_id) FROM Roles WHERE acc_id = @acc_id AND live = '1') > 1);";
                SqlEngine.Execute(new Command(CommandType.Execute, sqlString, Technical.ConvertParameterNamesToDataBaseNames(parameters, Technical.RoleParameterLabels, Technical.RoleDataBaseLabelsLabels)));
            }

            //Minden role rekordot listáz, ami egy adott client összes számlájához hozzátartozik
            public static DataSet SelectAllActiveRoleRecordByClientId(Dictionary<string, object> parameters)
            {
                StringBuilder sqlString = new StringBuilder();
                sqlString.Append("SELECT Clients.id as client_id, Clients.f_name as f_name, Clients.l_name as l_name, Accounts.id as acc_id, Accounts.acc_number as acc_number, Accounts.acc_type as acc_type, Roles.role as role ");
                sqlString.Append("FROM Clients, Accounts, Roles WHERE Clients.id = Roles.client_id AND Accounts.id = Roles.acc_id AND ");
                sqlString.Append(" Roles.acc_id in (SELECT acc_id FROM Roles WHERE client_id = @client_id AND live = '1') AND Roles.live = '1';");
                return Technical.ConvertDataBaseNamesToParameterNames((DataSet)SqlEngine.Execute(new Command(CommandType.List, sqlString.ToString(), Technical.ConvertParameterNamesToDataBaseNames(parameters, Technical.RoleParameterLabels, Technical.RoleDataBaseLabelsLabels))), Technical.RoleDataBaseLabelsLabels, Technical.RoleParameterLabels);
            }
        }
    }

    public static partial class Transaction
    {
        public static class UserMethods
        {
            //Átutalási tranzakció megadása
            public static string CreateTransferTransaction(Dictionary<string, object> parameters)
            {
                //CheckSourceBalance --->
                //DecreaseBalance (source oldal)
                //IncreaseBalance (destination oldal)
                //CreateTransactionRecord (transtype - transfer)
                StringBuilder sqlString = new StringBuilder();
                sqlString.Append("DECLARE @src_balance int, @dst_balance int, @id int; ");
                sqlString.Append("SET @src_balance = (SELECT balance FROM Accounts WHERE acc_number = @src_acc_num); ");
                sqlString.Append("SET @dst_balance = (SELECT balance FROM Accounts WHERE acc_number = @dst_acc_num); ");
                sqlString.Append("IF((@src_balance - @amount) >= 0) ");
                sqlString.Append("BEGIN ");
                sqlString.Append("UPDATE Accounts SET balance = (@src_balance - @amount) WHERE acc_number = @src_acc_num; ");
                sqlString.Append("UPDATE Accounts SET balance = (@dst_balance + @amount) WHERE acc_number = @dst_acc_num; ");
                sqlString.Append("INSERT INTO Transactions(trans_type, src_acc_num, dst_acc_num, src_name, dst_name, amount, curr, date, time, msg) VALUES('transfer', @src_acc_num, @dst_acc_num, 'Mr G.', 'Mr Z.', @amount, 'HUF', '2018.01.01', '18:00', '***GIRO TRANSFER'); ");
                //"INSERT INTO Transactions (trans_type, src_acc_num, dst_acc_num, src_name, dst_name, amount, curr, date, time, msg) VALUES ('transfer', @src_acc_num, @dst_acc_num, @src_name, @dst_name, @amount, @curr, @date, @time, @msg); ";
                sqlString.Append("SET @id = (SELECT SCOPE_IDENTITY()); SELECT @id AS id; ");
                sqlString.Append("END ");
                return (SqlEngine.Execute(new Command(CommandType.GetField, sqlString.ToString(), Technical.ConvertParameterNamesToDataBaseNames(parameters, Technical.TransactionParameterLabels, Technical.TransactionDataBaseLabelsLabels)))).ToString();
            }


            //Készpénz befizetés
            public static string CreateCashInTransaction(Dictionary<string, object> parameters)
            {
                //IncreaseBalance (acc_num)
                //CreateTransactionRecord (dst-hez megy az acc_num és az src-hez a 99999....)
                //                          (a name-eket üresen lehet hagyni)
                //                          (transtype - CashIn)
                StringBuilder sqlString = new StringBuilder();
                sqlString.Append("UPDATE Accounts SET balance = ((SELECT balance FROM Accounts WHERE acc_number = @acc_num) + @amount) WHERE acc_number = @acc_num; ");
                sqlString.Append("INSERT INTO Transactions(trans_type, src_acc_num, dst_acc_num, src_name, dst_name, amount, curr, date, time, msg) VALUES ('cashin', '999999999999999999999999', @acc_num, 'Mr G.', 'Mr Z.', @amount, 'HUF', '2018.01.01', '18:00', '***GIRO TRANSFER'); ");
                sqlString.Append("DECLARE @id int; SET @id = (SELECT SCOPE_IDENTITY()); SELECT @id AS id; ");
                //"INSERT INTO Transactions (trans_type, src_acc_num, dst_acc_num, src_name, dst_name, amount, curr, date, time, msg) VALUES ('cashin', '999999999999999999999999', @acc_num, '', '', @amount, @curr, @date, @time, @msg); ";
                return (SqlEngine.Execute(new Command(CommandType.GetField, sqlString.ToString(), Technical.ConvertParameterNamesToDataBaseNames(parameters, Technical.TransactionParameterLabels, Technical.TransactionDataBaseLabelsLabels)))).ToString();
            }

            //Készpénz felvétel
            public static string CreateCashOutTransaction(Dictionary<string, object> parameters)
            {
                //CheckSourceBalance --->
                //DecreaseBalance (acc_num)
                //CreateTransactionRecord (src-hez megy az acc_num és a dst-hez a 99999....)
                //                          (a name-eket üresen lehet hagyni)
                //                          (transtype - CashOut)
                StringBuilder sqlString = new StringBuilder();
                sqlString.Append("DECLARE @balance int, @id int; ");
                sqlString.Append("SET @balance = (SELECT balance FROM Accounts WHERE acc_number = @acc_num); ");
                sqlString.Append("IF((@balance - @amount) >= 0) ");
                sqlString.Append("BEGIN ");
                sqlString.Append("UPDATE Accounts SET balance = (@balance - @amount) WHERE acc_number = @acc_num; ");
                sqlString.Append("INSERT INTO Transactions(trans_type, src_acc_num, dst_acc_num, src_name, dst_name, amount, curr, date, time, msg) VALUES('cashout', @acc_num, '999999999999999999999999', 'Mr G.', 'Mr Z.', '-' + @amount, 'HUF', '2018.01.01', '18:00', '***GIRO TRANSFER'); ");
                //"INSERT INTO Transactions (trans_type, src_acc_num, dst_acc_num, src_name, dst_name, amount, curr, date, time, msg) VALUES ('cashout', @acc_num, '999999999999999999999999', '', '', @amount, @curr, @date, @time, @msg); ";
                sqlString.Append("END ");
                return (SqlEngine.Execute(new Command(CommandType.GetField, sqlString.ToString(), Technical.ConvertParameterNamesToDataBaseNames(parameters, Technical.TransactionParameterLabels, Technical.TransactionDataBaseLabelsLabels)))).ToString();
            }


            //Minden tranzakciót listáz, ami az adott számlaszámhoz kapcsolódik - akár forrás akár cél oldalon található meg a számlaszám
            public static DataSet SelectTransactionRecordsByAccountNumber(Dictionary<string, object> parameters)
            {
                string sqlString = "SELECT * FROM Transactions WHERE src_acc_num = @acc_num OR dst_acc_num = @acc_num;";
                return Technical.ConvertDataBaseNamesToParameterNames((DataSet)SqlEngine.Execute(new Command(CommandType.List, sqlString, Technical.ConvertParameterNamesToDataBaseNames(parameters, Technical.TransactionParameterLabels, Technical.TransactionDataBaseLabelsLabels))), Technical.TransactionDataBaseLabelsLabels, Technical.TransactionParameterLabels);
            }
        }

        
    }

    public static partial class Card
    {
        private static Dictionary<string, object> ConvertCardParameterNames(Dictionary<string, object> parameters)
        {
            //Később ezt máshogy fogjuk megoldani
            Dictionary<string, object> result = new Dictionary<string, object>();
            if (parameters.ContainsKey("id"))
            {
                result.Add("id", (string)parameters["id"]);
            }
            if (parameters.ContainsKey("firstName"))
            {
                result.Add("f_name", (string)parameters["firstName"]);
            }
            if (parameters.ContainsKey("lastName"))
            {
                result.Add("l_name", (string)parameters["lastName"]);
            }
            if (parameters.ContainsKey("dateOfBirth"))
            {
                result.Add("d_birth", (DateTime)parameters["dateOfBirth"]);
            }
            if (parameters.ContainsKey("placeOfBirth"))
            {
                result.Add("p_birth", (string)parameters["placeOfBirth"]);
            }
            if (parameters.ContainsKey("nameOfMother"))
            {
                result.Add("name_of_m", (string)parameters["nameOfMother"]);
            }
            if (parameters.ContainsKey("address"))
            {
                result.Add("address", (string)parameters["address"]);
            }
            if (parameters.ContainsKey("phone"))
            {
                result.Add("phone", (string)parameters["phone"]);
            }
            if (parameters.ContainsKey("email"))
            {
                result.Add("email", (string)parameters["email"]);
            }
            return result;
        }

        private static DataSet UnconvertCardParameterNames(DataSet ds)
        {
            DataSet result = new DataSet();
            result.Tables.Add(new DataTable());
            DataRow row = result.Tables[0].NewRow();

            if (ds.Tables[0].Columns.Contains("id"))
            {
                result.Tables[0].Columns.Add("id");
                row["id"] = ds.Tables[0].Rows[0]["id"].ToString();
            }
            if (ds.Tables[0].Columns.Contains("f_name"))
            {
                result.Tables[0].Columns.Add("firstName");
                row["firstName"] = ds.Tables[0].Rows[0]["f_name"].ToString();
            }
            if (ds.Tables[0].Columns.Contains("l_name"))
            {
                result.Tables[0].Columns.Add("lastName");
                row["lastName"] = ds.Tables[0].Rows[0]["l_name"].ToString();
            }
            if (ds.Tables[0].Columns.Contains("d_birth"))
            {
                result.Tables[0].Columns.Add("dateOfBirth");
                row["dateOfBirth"] = (DateTime)ds.Tables[0].Rows[0]["d_birth"];
            }
            if (ds.Tables[0].Columns.Contains("p_birth"))
            {
                result.Tables[0].Columns.Add("placeOfBirth");
                row["placeOfBirth"] = ds.Tables[0].Rows[0]["p_birth"].ToString();
            }
            if (ds.Tables[0].Columns.Contains("name_of_m"))
            {
                result.Tables[0].Columns.Add("nameOfMother");
                row["nameOfMother"] = ds.Tables[0].Rows[0]["name_of_m"].ToString();
            }
            if (ds.Tables[0].Columns.Contains("address"))
            {
                result.Tables[0].Columns.Add("address");
                row["address"] = ds.Tables[0].Rows[0]["address"].ToString();
            }
            if (ds.Tables[0].Columns.Contains("phone"))
            {
                result.Tables[0].Columns.Add("phone");
                row["phone"] = ds.Tables[0].Rows[0]["phone"].ToString();
            }
            if (ds.Tables[0].Columns.Contains("email"))
            {
                result.Tables[0].Columns.Add("email");
                row["email"] = ds.Tables[0].Rows[0]["email"].ToString();
            }

            //...
            result.Tables[0].Rows.Add(row);
            return result;
        }

        public static class UserMethods
        {
            //Kártya record létrehozása, a live flag-et '1're állítja alapból
            public static void CreateCardRecord(Dictionary<string, object> parameters)
            {
                string sqlString = "INSERT INTO Cards (client_id, acc_id, type, card_num, curr, deb_cred, date_if_req, date_of_exp, live) VALUES (@client_id, @acc_id, @type, @card_num, @curr, @deb_cred, @date_if_req, @date_of_exp, '1')";
                SqlEngine.Execute(new Command(CommandType.Execute, sqlString, ConvertCardParameterNames(parameters)));
            }

            //Törlés helyett, id alapján
            public static void InactivateCardRecordById(Dictionary<string, object> parameters)
            {
                string sqlString = "UPDATE Cards SET live='0' WHERE id=@id";
                SqlEngine.Execute(new Command(CommandType.Execute, sqlString, ConvertCardParameterNames(parameters)));
            }

            //Törlés helyett, kártyaszám alapján
            public static void InactivateCardRecordByCardNumber(Dictionary<string, object> parameters)
            {
                string sqlString = "UPDATE Cards SET live='0' WHERE card_num=@card_num";
                SqlEngine.Execute(new Command(CommandType.Execute, sqlString, ConvertCardParameterNames(parameters)));
            }
        }
    }
}
