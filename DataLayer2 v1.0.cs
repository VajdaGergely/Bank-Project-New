using System.Collections.Generic;
using System.Data;

/// <summary>
/// Summary description for DataLayer2
/// </summary>

namespace DataLayer
{
    public static partial class Client
    {
        //*
        public static class AdminMethods
        {
            //Összes Client rekord listázása
            public static void SelectAllClientRecords(ref DataSet dataset)
            {
                string sqlString = "SELECT * FROM Clients";
                Command command = new Command(CommandType.List, sqlString);
                //SqlEngine.Execute(command, ref dataset);
            }
        }

        private static class SystemMethods
        {
            //Teljesen kitörli az adott Client rekordot
            public static void DeleteClient(Dictionary<string, string> parameters)
            {
                //Az Account és a Role recordot is törölni kell
                //string sqlString = "DELETE FROM Clients WHERE id=@id; ";
                //Command command = new Command(CommandType.Execute, sqlString, parameters);
                //SqlEngine.Execute(command);
            }


            //Speciális select method
            public static void SelectClientRecordBy(Dictionary<string, string> parameters, ref DataSet dataset)
            {
                string sqlString = "SELECT * FROM Clients WHERE ";
                foreach (KeyValuePair<string, string> field in parameters)
                {
                    sqlString += string.Format("{0}=@{0} AND", field.Key);
                }
                sqlString = sqlString.TrimEnd(new char[] { 'D', 'N', 'A', ' ' });
                Command command = new Command(CommandType.List, sqlString);
                //SqlEngine.Execute(command, ref dataset);
            }
        }
    }

    public static partial class Account
    {
        public static class AdminMethods
        {
            //Adott client id-hoz tartozó összes számlát listázza, ami nem élő számla számla
            public static void SelectInactiveAccountRecordsByClientId(Dictionary<string, string> parameters, ref DataSet dataset)
            {
                //string sqlString = "SELECT * FROM Accounts WHERE (id in (SELECT acc_id FROM Roles WHERE client_id = @client_id AND live='0')) AND live='0'";
                //Command command = new Command(CommandType.List, sqlString, parameters);
                //SqlEngine.Execute(command, ref dataset);
            }

            //Adott client id-hoz tartozó összes számlát listázza, aktívot és inaktívot is
            public static void SelectAllAccountRecordsByClientId(Dictionary<string, string> parameters, ref DataSet dataset)
            {
                //string sqlString = "SELECT * FROM Accounts WHERE id in (SELECT acc_id FROM Roles WHERE client_id = @client_id)";
                //Command command = new Command(CommandType.List, sqlString, parameters);
                //SqlEngine.Execute(command, ref dataset);
            }


            //Számla keresése account id alapján
            public static void SelectAccountRecordByAccountId(Dictionary<string, string> parameters, ref DataSet dataset)
            {
                //string sqlString = "SELECT * FROM Accounts WHERE id=@id";
                //Command command = new Command(CommandType.List, sqlString, parameters);
                //SqlEngine.Execute(command, ref dataset);
            }

            //Összes aktív számlát listázza
            public static void SelectAllActiveAccountRecords(ref DataSet dataset)
            {
                //string sqlString = "SELECT * FROM Accounts";
                //Command command = new Command(CommandType.List, sqlString);
                //SqlEngine.Execute(command, ref dataset);
            }

            //Összes inaktív számlát listázza
            public static void SelectAllInactiveAccountRecords(ref DataSet dataset)
            {
                //string sqlString = "SELECT * FROM Accounts";
                //Command command = new Command(CommandType.List, sqlString);
                //SqlEngine.Execute(command, ref dataset);
            }


            //Összes számlát listázza aktívot és inaktívot is
            public static void SelectAllAccountRecords(ref DataSet dataset)
            {
                //string sqlString = "SELECT * FROM Accounts";
                //Command command = new Command(CommandType.List, sqlString);
                //SqlEngine.Execute(command, ref dataset);
            }
        }

        private static class SystemMethods
        {
            public static void MofidyAccount(Dictionary<string, string> parameters)
            {
                //Nem módosítunk számlát egyelőre
            }

            //Account és Role rekordot hoz létre, a role-t dinamikusan kell megadni, a live='1' default érték
            public static void CreateAccountAndRoleRecordsExtra(Dictionary<string, string> parameters)
            {
                string sqlString = "INSERT INTO Accounts (acc_number, acc_name, acc_type, curr, ret_corp, balance, date_of_op, live) VALUES (@acc_number, @acc_name, @acc_type, @curr, @ret_corp, @balance, @date_of_op, '1'); ";
                sqlString += "INSERT INTO Roles (client_id, acc_id, role, live) VALUES (@client_id, (SELECT id FROM Accounts WHERE acc_number=@acc_number), @role, '1')";
                //Command command = new Command(CommandType.Execute, sqlString, parameters);
                //SqlEngine.Execute(command);
            }

            //Újra aktívvá tehetjük az inaktivált számlát és a hozzá tartozó role-okat
            public static void ActivateAccountAndConnectedRoleRecords(Dictionary<string, string> parameters)
            {
                //string sqlString = "UPDATE Accounts SET live='1' WHERE id=@acc_id; UPDATE Roles SET live='1' WHERE acc_id=acc_id";
                //Command command = new Command(CommandType.Execute, sqlString, parameters);
                //SqlEngine.Execute(command);
            }

            //Account és role rekordok végleges törlése account number alapján
            public static void DeleteAccountAndConnectedRoleRecordsByAccountNumber(Dictionary<string, string> parameters)
            {
                //Az Account és a Role recordot is törölni kell
                string sqlString = "DELETE FROM Roles WHERE acc_id=(SELECT id FROM Accounts WHERE acc_number = @acc_number); ";
                sqlString += "DELETE FROM Accounts WHERE id=(SELECT id FROM Accounts WHERE acc_number = @acc_number)";
                //Command command = new Command(CommandType.Execute, sqlString, parameters);
                //SqlEngine.Execute(command);
            }

            //Account és role rekordok végleges törlése account id alapján
            public static void DeleteAccountAndConnectedRoleRecordsByAccountId(Dictionary<string, string> parameters)
            {
                //Az Account és a Role recordot is törölni kell
                string sqlString = "DELETE FROM Roles WHERE acc_id=@acc_id; ";
                sqlString += "DELETE FROM Accounts WHERE id=@acc_id";
                //Command command = new Command(CommandType.Execute, sqlString, parameters);
                //SqlEngine.Execute(command);
            }

            //Speciális select metódus
            public static void SelectAccountRecordBy(Dictionary<string, string> parameters, ref DataSet dataset)
            {
                string sqlString = "SELECT * FROM Accounts WHERE ";
                foreach (KeyValuePair<string, string> field in parameters)
                {
                    sqlString += string.Format("{0}=@{0} AND", field.Key);
                }
                sqlString = sqlString.TrimEnd(new char[] { 'D', 'N', 'A', ' ' });
                Command command = new Command(CommandType.List, sqlString);
                //SqlEngine.Execute(command, ref dataset);
            }

            //Egyenleg növelése - nem csinálunk ilyet, ez csak tranzakció része lehet
            public static void IncreaseBalance(Dictionary<string, string> parameters)
            {
                //le kell kérni mindenképp a balance-t először
                //aztán megnövelni
                //->ezt mind egy sql string-en belül tesszük meg 
                //string sqlString = "UPDATE Accounts SET balance=(SELECT balance FROM Accounts WHERE id=@id) + @amount WHERE id=@id";
                //Command command = new Command(CommandType.Execute, sqlString, parameters);
                //SqlEngine.Execute(command);
            }

            //Egyenleg csökkentése - nem csinálunk ilyet, ez csak tranzakció része lehet
            public static void DecreaseBalance(Dictionary<string, string> parameters)
            {
                //le kell kérni mindenképp a balance-t először
                //aztán megnövelni
                //->ezt mind egy sql string-en belül tesszük meg 
                //string sqlString = "UPDATE Accounts SET balance=(SELECT balance FROM Accounts WHERE id=@id) - @amount WHERE id=@id";
                //Command command = new Command(CommandType.Execute, sqlString, parameters);
                //SqlEngine.Execute(command);
            }

            //Egyenleg lekérése
            public static int GetBalance(Dictionary<string, string> parameters)
            {
                //string sqlString = "SELECT balance FROM Accounts WHERE acc_number=@src_acc_num";
                //Command command = new Command(CommandType.GetField, sqlString, parameters);
                string balance = "";
                //SqlEngine.Execute(command, ref balance);
                return int.Parse(balance);
            }
        }
    }

    public static partial class Role
    {
        public static class AdminMethods
        {
            //Minden role rekordot listáz, ami egy adott client összes számlájához hozzátartozik - aktív és inaktív is
            public static void SelectAllRoleRecordByClientId(Dictionary<string, string> parameters, ref DataSet dataset)
            {
                string sqlString = "SELECT Clients.id as client_id, Clients.f_name as f_name, Clients.l_name as l_name, Accounts.id as acc_id, Accounts.acc_number as acc_number, Accounts.acc_type as acc_type, Roles.role as role";
                sqlString += " FROM Clients, Accounts, Roles WHERE Clients.id = Roles.client_id AND Accounts.id = Roles.acc_id AND";
                sqlString += " Roles.client_id = @client_id";
                //túl bonyolítottuk...
                //sqlString += " Roles.acc_id in (SELECT acc_id FROM Roles WHERE client_id = @client_id)";
                //Command command = new Command(CommandType.List, sqlString, parameters);
                //SqlEngine.Execute(command, ref dataset);
            }
        }

        private static class SystemMethods
        {
            //Újraaktiválás, ilyet alapból nem csinálunk...
            public static void ActivateRoleRecord(Dictionary<string, string> parameters)
            {
                //string sqlString = "UPDATE Roles SET live='1' WHERE client_id=@client_id AND acc_id=@id";
                //Command command = new Command(CommandType.Execute, sqlString, parameters);
                //SqlEngine.Execute(command);
            }

            //Végleges törlés
            public static void Delete(Dictionary<string, string> parameters)
            {
                //Csak akkor törölhetünk Role-t hogyha legalább 2 van!
                //string sqlString = "DELETE FROM Roles WHERE client_id=@client_id AND acc_id=@acc_id";
                //Command command = new Command(CommandType.Execute, sqlString, parameters);
                //SqlEngine.Execute(command);
            }

            //Összes role rekordot listázza active és inactive-okat is
            public static void SelectAllRoleRecords(ref DataSet dataset)
            {
                string sqlString = "SELECT * FROM Roles";
                Command command = new Command(CommandType.List, sqlString);
                //SqlEngine.Execute(command, ref dataset);
            }


            //Speciális method
            public static void SelectRoleRecordBy(Dictionary<string, string> parameters, ref DataSet dataset)
            {
                string sqlString = "SELECT * FROM Roles WHERE ";
                foreach (KeyValuePair<string, string> field in parameters)
                {
                    sqlString += string.Format("{0}=@{0} AND", field.Key);
                }
                sqlString = sqlString.TrimEnd(new char[] { 'D', 'N', 'A', ' ' });
                Command command = new Command(CommandType.List, sqlString);
                //SqlEngine.Execute(command, ref dataset);
            }
        }
    }

    public static partial class Transaction
    {
        public static class AdminMethods
        {
            //id alapján keresünk rá tranzakcióra
            public static void SelectTransactionRecordById(Dictionary<string, string> parameters)
            {
                //string sqlString = "SELECT * FROM Transactions WHERE id=@id";
                //Command command = new Command(CommandType.List, sqlString, parameters);
                //SqlEngine.Execute(command);
            }

            //Összes tranzakció listázása
            public static void SelectAllTransactionRecords(ref DataSet dataset)
            {
                //string sqlString = "SELECT * FROM Transactions";
                //Command command = new Command(CommandType.List, sqlString);
                //SqlEngine.Execute(command, ref dataset);
            }
        }

        private static class SystemMethods
        {
            //Létrehoz egy transaction record-ot (nem használjuk, mert sima típus nélküli tranzaciót nem hozunk létre
            public static void CreateTransactionRecord(Dictionary<string, string> parameters)
            {
                //string sqlString = "INSERT INTO Transactions (trans_type, src_acc_num, dst_acc_num, src_name, dst_name, amount, curr, date, time, msg) VALUES (@trans_type, @src_acc_num, @dst_acc_num, @src_name, @dst_name, @amount, @curr, @date, @time, @msg)";
                //Command command = new Command(CommandType.Execute, sqlString, parameters);
                //SqlEngine.Execute(command);
            }

            public static void MofidyTransactionRecord(Dictionary<string, string> parameters)
            {
                //Nem módosítunk tranzakciót...
            }

            //Nem törlünk tranzakciót
            public static void DeleteTransactionRecord(Dictionary<string, string> parameters)
            {
                //Az Account és a Role recordot is törölni kell
                //string sqlString = "DELETE FROM Transactions WHERE id=@id; ";
                //Command command = new Command(CommandType.Execute, sqlString, parameters);
                //SqlEngine.Execute(command);
            }

            //speciális method
            public static void SelectTransactionRecordBy(Dictionary<string, string> parameters, ref DataSet dataset)
            {
                string sqlString = "SELECT * FROM Transactions WHERE ";
                foreach (KeyValuePair<string, string> field in parameters)
                {
                    sqlString += string.Format("{0}=@{0} AND", field.Key);
                }
                sqlString = sqlString.TrimEnd(new char[] { 'D', 'N', 'A', ' ' });
                //Command command = new Command(CommandType.List, sqlString);
                //SqlEngine.Execute(command, ref dataset);
            }
        }
    }

    public static partial class Card
    {
        public static class AdminMethods
        {
            //Összes aktív kártya listázása
            public static void SelectAllActiveCardRecords(ref DataSet dataset)
            {
                //string sqlString = "SELECT * FROM Cards WHERE live='1'";
                //Command command = new Command(CommandType.List, sqlString);
                //SqlEngine.Execute(command, ref dataset);
            }

            //Összes inaktív kártya listázása
            public static void SelectAllInactiveCardRecords(ref DataSet dataset)
            {
                //string sqlString = "SELECT * FROM Cards WHERE live='0'";
                //Command command = new Command(CommandType.List, sqlString);
                //SqlEngine.Execute(command, ref dataset);
            }

            //Összes kártya listázása
            public static void SelectAllRecords(ref DataSet dataset)
            {
                //string sqlString = "SELECT * FROM Cards";
                //Command command = new Command(CommandType.List, sqlString);
                //SqlEngine.Execute(command, ref dataset);
            }

            //kártya listázása id alapján
            public static void SelectCardRecordById(Dictionary<string, string> parameters)
            {
                //string sqlString = "SELECT * FROM Cards WHERE id=@id";
                //Command command = new Command(CommandType.List, sqlString, parameters);
                //SqlEngine.Execute(command);
            }

            //kártya listázása kártyaszám alapján
            public static void SelectRecordByCardNumber(Dictionary<string, string> parameters, ref DataSet dataset)
            {
                //string sqlString = "SELECT * FROM Cards WHERE card_num=@card_num";
                //Command command = new Command(CommandType.List, sqlString, parameters);
                //SqlEngine.Execute(command, ref dataset);
            }
        }

        private static class SystemMethods
        {
            //Alapból kártyát nem módosítunk
            public static void ModifyCardRecord(Dictionary<string, string> parameters)
            {
                //Nem módosítunk tranzakciót...
            }

            //Alapból kártyát nem törlünk
            public static void DeleteCardRecord(Dictionary<string, string> parameters)
            {
                //string sqlString = "DELETE FROM Cards WHERE id=@id";
                //Command command = new Command(CommandType.Execute, sqlString, parameters);
                //SqlEngine.Execute(command);
            }

            //Speciális method
            public static void SelectCardRecordBy(Dictionary<string, string> parameters, ref DataSet dataset)
            {
                string sqlString = "SELECT * FROM Cards WHERE ";
                foreach (KeyValuePair<string, string> field in parameters)
                {
                    sqlString += string.Format("{0}=@{0} AND", field.Key);
                }
                sqlString = sqlString.TrimEnd(new char[] { 'D', 'N', 'A', ' ' });
                Command command = new Command(CommandType.List, sqlString);
                //SqlEngine.Execute(command, ref dataset);
            }
        }
    }
}
    
    