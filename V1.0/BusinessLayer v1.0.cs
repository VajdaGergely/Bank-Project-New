using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Layer2
/// </summary>



//public static class BusinessLayer
namespace BusinessLayerOld
{
    //lehet hogy mivel private-nek akarjuk majd az ő eljárásait ezért namespace helyett, ez egy static class lesz majd...
    namespace Technical
    {
        //ha őket private-nek akarjuk, akkor kell egy külső public class...és azon belül kell elhelyezni őket
        public static class Currency
        {
            public enum CurrencyType
            {
                None = 0,
                HUF = 1,
                CHF = 2,
                EUR = 3,
                GBP = 4,
                USD = 5
                //ennél már több van!!! valahogy ezeket ki is kell pakolni majd külső helyre
                //valami xml fájlba...onnan fogjuk beolvasni mindig ha kell
                //és kell hozzá külön felület, ahol programból lehet bővíteni őket, törölni,
                //menedzselni (a többit is ugyanígy kell majd megcsinálni)

                //
            }

            public static string ConvertCurrencyTypeToString(CurrencyType type)
            {
                switch(type)
                {
                    case CurrencyType.HUF:
                        return "huf";
                    case CurrencyType.CHF:
                        return "chf";
                    case CurrencyType.EUR:
                        return "eur";
                    case CurrencyType.GBP:
                        return "gbp";
                    case CurrencyType.USD:
                        return "usd";
                    default:
                        return "none";
                }
            }

            public static CurrencyType ConvertStringToCurrencyType(string type)
            {
                switch (type)
                {
                    case "huf":
                        return CurrencyType.HUF;
                    case "chf":
                        return CurrencyType.CHF;
                    case "eur":
                        return CurrencyType.EUR;
                    case "gbp":
                        return CurrencyType.GBP;
                    case "usd":
                        return CurrencyType.USD;
                    default:
                        return CurrencyType.None;
                }
            }
        }

        public static class Card
        {
            public enum CardType
            {
                None,
                Maestro,
                MasterCard,
                VisaElectron,
                VisaClassic
            }
            //kellenek itt is az oda-vissza konvertáló függvények

            public static CardType StringToCardType(string type)
            {
                switch (type)
                {
                    case "maestro":
                        return CardType.Maestro;
                    case "mastercard":
                        return CardType.MasterCard;
                    case "visaelectron":
                        return CardType.VisaElectron;
                    case "visaclassic":
                        return CardType.VisaClassic;
                    default:
                        return CardType.None;
                }
            }

            public static string TransactionTypeToString(CardType type)
            {
                switch (type)
                {
                    case CardType.Maestro:
                        return "maestro";
                    case CardType.MasterCard:
                        return "mastercard";
                    case CardType.VisaElectron:
                        return "visaelectron";
                    case CardType.VisaClassic:
                        return "visaclassic";
                    default:
                        return "none";
                }
            }
        }

        //Már nem annyira aktuális...de lehet hogy később mégis jó lesz...
        //A CashTransaction kiütötte...
        public static class Transaction
        {
            public enum TransactionType
            {
                None,
                Transfer,
                CashIn,
                CashOut
            }

            public static TransactionType StringToTransactionType(string type)
            {
                switch (type)
                {
                    case "cashin":
                        return TransactionType.CashIn;
                    case "cashout":
                        return TransactionType.CashOut;
                    case "transfer":
                        return TransactionType.Transfer;
                    default:
                        return TransactionType.None;
                }
            }

            public static string TransactionTypeToString(TransactionType type)
            {
                switch (type)
                {
                    case TransactionType.CashIn:
                        return "cashin";
                    case TransactionType.CashOut:
                        return "cashout";
                    case TransactionType.Transfer:
                        return "transfer";
                    default:
                        return "none";
                }
            }
        }

        public static class CashTransaction
        {
            public enum CashTransactionType
            {
                None,
                Deposit,
                WithDrawl
            }

            public static CashTransactionType StringToCashTransactionType(string type)
            {
                switch (type)
                {
                    case "deposit":
                        return CashTransactionType.Deposit;
                    case "withdrawl":
                        return CashTransactionType.WithDrawl;
                    default:
                        return CashTransactionType.None;
                }
            }

            public static string CashTransactionTypeToString(CashTransactionType type)
            {
                switch (type)
                {
                    case CashTransactionType.Deposit:
                        return "deposit";
                    case CashTransactionType.WithDrawl:
                        return "withdrawl";
                    default:
                        return "none";
                }
            }
        }

        public class Account
        {
            public enum AccountType
            {
                None,
                Junior,
                Basic,
                Premium,
                Senior
            }

            public static AccountType StringToAccountType(string type)
            {
                switch (type)
                {
                    case "junior":
                        return AccountType.Junior;
                    case "basic":
                        return AccountType.Basic;
                    case "premium":
                        return AccountType.Premium;
                    case "senior":
                        return AccountType.Senior;
                    default:
                        return AccountType.None;
                }
            }

            public static string AccountTypeToString(AccountType type)
            {
                switch (type)
                {
                    case AccountType.Junior:
                        return "junior";
                    case AccountType.Basic:
                        return "basic";
                    case AccountType.Premium:
                        return "premium";
                    case AccountType.Senior:
                        return "senior";
                    default:
                        return "none";
                }
            }

            public enum SectorType
            {
                None,
                Retail,
                Corporate
            }

            public static SectorType StringToSectorType(string type)
            {
                switch (type)
                {
                    case "retail":
                        return SectorType.Retail;
                    case "corporate":
                        return SectorType.Corporate;
                    default:
                        return SectorType.None;
                }
            }

            public static string SectorTypeToString(SectorType type)
            {
                switch (type)
                {
                    case SectorType.Retail:
                        return "retail";
                    case SectorType.Corporate:
                        return "corporate";
                    default:
                        return "none";
                }
            }
        }
    }

    public static class CurrencySystem
    {
        private static double hufUsd = 0.003824;
        private static double chfUsd = 1.004208;
        private static double gbpUsd = 1.361656;
        private static double eurUsd = 1.200531;

        public static double HufUsd
        {
            get
            {
                return hufUsd;
            }
            private set
            {
                //
            }
        }
        public static double ChfUsd
        {
            get
            {
                return chfUsd;
            }
            private set
            {
                //
            }
        }
        public static double GbpUsd
        {
            get
            {
                return gbpUsd;
            }
            private set
            {
                //
            }
        }
        public static double EurUsd
        {
            get
            {
                return eurUsd;
            }
            private set
            {
                //
            }
        }
    }


    //#####################


    //Úgy kéne megcsinálni, hogy ezt ne kintről kelljen létrehozni!!!!
    //Az egész Client class-t ne lássák kívülről
    //Kéne egy sima static class, aminek csak az eljárásai vannak meg kifelé
    //és utána azok hozzák létre az object-eket itt bent

    //Az alapfunkciója a konvertálás.... alapvetően interface-el akartuk, de úgy nem működött
    //public, protected, abstract, override...ezekkel volt a gond...

    public abstract class Default
    {
        protected string ID { get; set; }

        protected abstract Dictionary<string, object> PackToDataLayer();
        protected abstract void UnpackFromDataLayer(DataSet ds);
        protected abstract DataSet PackToViewLayer();

        public Default()
        {
            ID = null;
        }

        public Default(string id)
        {
            ID = id;
        }
    }
    
    public class Client : Default
    {
        protected DateTime dateOfBirth;
        protected string FirstName { get; set; }
        protected string LastName { get; set; }
        protected DateTime DateOfBirth
        {
            get
            {
                return dateOfBirth;
            }

            set
            {
                //if (DateOfBirth.Year > 1900 && DateOfBirth.Year < 2000)
                //{
                dateOfBirth = value;
                //}
            }
        }
        protected string PlaceOfBirth { get; set; }
        protected string NameOfMother { get; set; }
        protected string Address { get; set; }
        //fontos a formátum!
        protected string Phone { get; set; }
        //fontos a formátum!
        protected string Email { get; set; }
        protected string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        //List-hez használjuk
        public Client() :base()
        {
            FirstName = null;
            LastName = null;
            DateOfBirth = Convert.ToDateTime("1900.01.01");
            PlaceOfBirth = null;
            NameOfMother = null;
            Address = null;
            Phone = null;
            Email = null;
        }

        //Create-nél használjuk.
        public Client(string firstName, string lastName, string dateOfBirth, string placeOfBirth, string nameOfMother, string address, string phone, string email)
        : base()
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = Convert.ToDateTime(dateOfBirth);
            PlaceOfBirth = placeOfBirth;
            NameOfMother = nameOfMother;
            Address = address;
            Phone = phone;
            Email = email;
        }

        //módosításhoz kell
        public Client(string id, string firstName, string lastName, string dateOfBirth, string placeOfBirth, string nameOfMother, string address, string phone, string email)
        : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = Convert.ToDateTime(dateOfBirth);
            PlaceOfBirth = placeOfBirth;
            NameOfMother = nameOfMother;
            Address = address;
            Phone = phone;
            Email = email;
        }

        protected override Dictionary<string, object> PackToDataLayer()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            if (ID != null)
            {
                result.Add("id", ID);
            }
            result.Add("firstName", FirstName);
            result.Add("lastName", LastName);
            result.Add("dateOfBirth", DateOfBirth);
            result.Add("placeOfBirth", PlaceOfBirth);
            result.Add("nameOfMother", NameOfMother);
            result.Add("address", Address);
            result.Add("phone", Phone);
            result.Add("email", Email);
            return result;
        }

        protected override void UnpackFromDataLayer(DataSet ds)
        {
            //megnézzük, hogy id tartozik e hozzá
            if (ds.Tables[0].Columns.Contains("id"))
            {
                //ha igen, akkor, tudjuk, hogy már ügyfél az illető
                ID = ds.Tables[0].Rows[0]["id"].ToString();
            }
            else
            {
                //ha nincs id, akkor pedig valószínűleg most hozzuk létre az ügyfelet, vagy simán egy
                //átmeneti ügyfél objektummal dolgozunk
            }

            //elvileg ez a függvény csak a datalayer-ből hívódik meg....
            //ha mégse csak onnan, akkor később átpiszkáljuk majd
            FirstName = ds.Tables[0].Rows[0]["firstName"].ToString();
            LastName = ds.Tables[0].Rows[0]["lastName"].ToString();
            DateOfBirth = Convert.ToDateTime(ds.Tables[0].Rows[0]["dateOfBirth"].ToString());
            PlaceOfBirth = ds.Tables[0].Rows[0]["placeOfBirth"].ToString();
            NameOfMother = ds.Tables[0].Rows[0]["nameOfMother"].ToString();
            Address = ds.Tables[0].Rows[0]["address"].ToString();
            Phone = ds.Tables[0].Rows[0]["phone"].ToString();
            Email = ds.Tables[0].Rows[0]["email"].ToString();
        }

        protected override DataSet PackToViewLayer()
        {
            DataSet result = new DataSet();
            result.Tables.Add(new DataTable());
            result.Tables[0].Columns.Add("id");
            result.Tables[0].Columns.Add("firstName");
            result.Tables[0].Columns.Add("lastName");
            //result.Tables[0].Columns.Add("fullName");
            result.Tables[0].Columns.Add("dateOfBirth");
            result.Tables[0].Columns.Add("placeOfBirth");
            result.Tables[0].Columns.Add("nameOfMother");
            result.Tables[0].Columns.Add("address");
            result.Tables[0].Columns.Add("phone");
            result.Tables[0].Columns.Add("email");

            DataRow row = result.Tables[0].NewRow();
            //row["fullName"] = FullName;
            row["id"] = ID;
            row["firstName"] = FirstName;
            row["lastName"] = LastName;
            //Ez itt nem megjelenítési formázás, hanem csak simán ezt az adatot akarjuk továbbítani,
            //Mert az idő nem kell nekünk ez itt csak Dátumot jelöl!!!!
            //Ha DateTime-ként küldenénk, akkor Time része is lenne, ami hiba forrás lehetne
            row["dateOfBirth"] = DateOfBirth.ToShortDateString();
            row["placeOfBirth"] = PlaceOfBirth;
            row["nameOfMother"] = NameOfMother;
            row["address"] = Address;
            row["phone"] = Phone;
            row["email"] = Email;
            //...
            result.Tables[0].Rows.Add(row);
            return result;
        }



        public string CreateClient()
        {
            //1. Konstruktor tölti fel a property-ket a paraméterekkel
            //2. Ellenőrzés, konvertálás, stb műveletek
            //3. Becsomagolás Dictionary<string, object> típusba
            //4. Átadjuk a DataLayer-nek
            //5. Visszajön egy sima string, ami az id-t tartalmazza és amit egyből tolunk tovább a View Layer-be
            return DataLayer.Client.UserMethods.CreateClientRecord(PackToDataLayer());
        }

        public DataSet GetClientData(string id)
        {
            //1. Az id-t Dictionary<string, object> típusba csomagoljuk
            //2. Elküldjük a Data Layer-nek
            //3. Visszajön egy DataSet
            //4. A DataSet-et property-kké konvertáljuk
            //5. Ellenőrzés, konvertálás, stb műveletek
            //6. Visszaalkítjuk a property-ket DataSet-é és elküldjük a View Layer-nek
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["id"] = id;
            UnpackFromDataLayer(DataLayer.Client.UserMethods.SelectClientRecordById(parameters));
            return PackToViewLayer();
        }

        public void SetClientData()
        {
            //1. Konstruktor tölti fel a property-ket a paraméterekkel
            //2. Ellenőrzés, konvertálás, stb műveletek
            //3. Becsomagolás Dictionary<string, object> típusba
            //4. Átadjuk a DataLayer-nek, és kész (nem ad vissza semmit)
            DataLayer.Client.UserMethods.ModifyClientRecord(PackToDataLayer());
        }

        public static void ChangeLoginName()
        {

        }

        public static void ChangePassword()
        {

        }

        public static void DeleteClient()
        {
            //Ilyen nem kell, mert őrizzük mindenképp az adatait és a tranzakcióit is...
        }

        //###
        //### Nem kellenek static eljárások.....mert itt egy csomó dolgot fogunk majd ellenőrizni,
        //### piszkálni, stb...
        //###
    }

    
    
    //A Role csak azért kell, hogy lehessen ilyen típusú tulajdonság az Accounts class-ban
    //Nem kezelünk kívülről semmilyen Role dolgot...
    public class Role
    {
        protected string ClientID { get; set; }
        protected string AccountID { get; set; }

        //ide kell a speciális role megoldás esetleg....
        protected string RoleType { get; set; }
        protected bool Live { get; set; }
    }

    public class Account : Default
    {
        protected DateTime dateOfOpening;

        protected Technical.Account.SectorType sectorType;
        protected string AccountNumber { get; set; }
        protected string AccountName { get; set; }
        protected string AccountType { get; set; }
        protected string Currency { get; set; }

        //Ehelyett már Technical.Account.AccountSectorType kell!!
        protected Technical.Account.SectorType SectorType { get; set; }
        protected int Balance { get; set; }
        protected DateTime DateOfOpening
        {
            get
            {
                return dateOfOpening;
            }
            set
            {
                dateOfOpening = value;
            }
        }

        //ez a megszűntetés miatt kell majd, ha inaktív számlákat is látni akarunk
        //alapból 9999.12.31 az értéke
        protected DateTime DateOfClosing { get; set; }

        protected List<Role> Roles { get; set; }
        protected bool Live { get; set; }

        //list
        public Account() :base()
        {
            AccountNumber = null;
            AccountName = null;
            AccountType = null;
            Currency = null;
            SectorType = Technical.Account.SectorType.None;
            Balance = 0;
            DateOfOpening = Convert.ToDateTime("1991.01.01");
            //DateOfClosing = null;
            //Live = null;
        }

        //számlanyitáshoz kell
        public Account(string accountType, string currency, string sectorType)
        :base()
        {
            AccountNumber = GenerateRandomAccNumber();
            AccountName = accountType;
            AccountType = accountType;
            Currency = currency;
            SectorType = Technical.Account.StringToSectorType(sectorType);
            Balance = 0;
            DateOfOpening = DateTime.Today;
            //DateOfClosing = null;
            //Live = null;
        }

        public Account(string accountType, string currency, string sectorType, int balance, DateTime dateOfOpening)
        :base ()
        {
            AccountNumber = GenerateRandomAccNumber();
            AccountName = accountType;
            AccountType = accountType;
            Currency = currency;
            SectorType = Technical.Account.StringToSectorType(sectorType);
            Balance = balance;
            DateOfOpening = dateOfOpening;
            //DateOfClosing = null;
            //Live = null;
        }

        private string GenerateRandomAccNumber()
        {
            string result = "18203129";
            Random r = new Random();
            for (int i = 0; i < 8; i++)
            {
                result += (r.Next(0, 9)).ToString();
            }

            result += "00000000";
            return result;
        }

        protected override Dictionary<string, object> PackToDataLayer()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            if (ID != null)
            {
                result.Add("id", ID);
            }
            result.Add("accountNumber", AccountNumber);
            result.Add("accountName", AccountName);
            result.Add("accountType", AccountType);
            result.Add("currency", Currency);
            result.Add("sectorType", Technical.Account.SectorTypeToString(SectorType));
            result.Add("balance", Balance);
            result.Add("dateOfOpening", DateOfOpening);
            return result;
        }

        protected override void UnpackFromDataLayer(DataSet ds)
        {
            //megnézzük, hogy id tartozik e hozzá
            if (ds.Tables[0].Columns.Contains("id"))
            {
                //ha igen, akkor, tudjuk, hogy már ügyfél az illető
                ID = ds.Tables[0].Rows[0]["id"].ToString();
            }
            else
            {
                //ha nincs id, akkor pedig valószínűleg most hozzuk létre az ügyfelet, vagy simán egy
                //átmeneti ügyfél objektummal dolgozunk
            }
            //elvileg ez a függvény csak a datalayer-ből hívódik meg....
            //ha mégse csak onnan, akkor később átpiszkáljuk majd
            AccountNumber = ds.Tables[0].Rows[0]["accountNumber"].ToString();
            AccountName = ds.Tables[0].Rows[0]["accountName"].ToString();
            AccountType = ds.Tables[0].Rows[0]["accountType"].ToString();
            Currency = ds.Tables[0].Rows[0]["currency"].ToString();
            SectorType = Technical.Account.StringToSectorType(ds.Tables[0].Rows[0]["sectorType"].ToString());
            Balance = int.Parse(ds.Tables[0].Rows[0]["balance"].ToString());
            DateOfOpening = Convert.ToDateTime(ds.Tables[0].Rows[0]["dateOfOpening"]);
        }

        protected override DataSet PackToViewLayer()
        {
            DataSet result = new DataSet();
            result.Tables.Add(new DataTable());
            result.Tables[0].Columns.Add("id");
            result.Tables[0].Columns.Add("accountNumber");
            result.Tables[0].Columns.Add("accountName");
            result.Tables[0].Columns.Add("accountType");
            result.Tables[0].Columns.Add("currency");
            result.Tables[0].Columns.Add("sectorType");
            result.Tables[0].Columns.Add("balance");
            result.Tables[0].Columns.Add("dateOfOpening");

            DataRow row = result.Tables[0].NewRow();
            row["id"] = ID;
            row["accountNumber"] = AccountNumber;
            row["accountName"] = AccountName;
            row["accountType"] = AccountType;
            row["currency"] = Currency;
            row["sectorType"] = Technical.Account.SectorTypeToString(SectorType);
            row["balance"] = Balance;
            row["dateOfOpening"] = DateOfOpening;

            result.Tables[0].Rows.Add(row);
            return result;
        }

        public string OpenAccount(string client_id)
        {
            Dictionary<string, object> parameters = PackToDataLayer();
            parameters["client_id"] = client_id;
            parameters["role"] = "OWNER";
            parameters["live"] = "1";
            return DataLayer.Account.UserMethods.CreateAccountAndRoleRecords(parameters);
        }


        public DataSet ListAccounts(string id)
        {
            //valahogy máshogy kéne majd csinálni, hogy ne itt keljen a Dictionary-t létrehozni...
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["clientId"] = id;
            UnpackFromDataLayer(DataLayer.Account.UserMethods.SelectActiveAccountRecordsByClientId(parameters));
            return PackToViewLayer();
        }

        public void CloseAccount(string client_id, string acc_id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["client_id"] = client_id;
            parameters["acc_id"] = acc_id;
            DataLayer.Account.UserMethods.InactivateAccountAndAllConnectedRoleRecords(parameters);
        }

        public void CreateRole(string client_id, string acc_id, string role)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["client_id"] = client_id;
            parameters["acc_id"] = acc_id;
            parameters["role"] = role;
            //DataLayer.Role.UserMethods.CreateRoleRecordByClientId(parameters);
        }

        public void ModifyRole(string client_id, string acc_id, string role)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["client_id"] = client_id;
            parameters["acc_id"] = acc_id;
            parameters["role"] = role;
            //DataLayer.Role.UserMethods.ModifyRoleRecord(parameters);
        }

        public void DeleteRole(string client_id, string acc_id)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["client_id"] = client_id;
            parameters["acc_id"] = acc_id;
            //DataLayer.Role.UserMethods.InactivateRoleRecord(parameters);
        }

        public DataSet ListRoles(string client_id)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["client_id"] = client_id;
            DataSet dataset = new DataSet();
            //DataLayer.Role.UserMethods.SelectAllActiveRoleRecordByClientId(parameters, ref dataset);
            return dataset;
        }

        //Meg kell oldani valahogy, közvetlenül ne lehessen meghívni, hanem csak a Transaction osztály metódusai hívhassák
        //Közvetlenül nem piszkáljuk az egyenleget....
        //Lehet, hogy akkor nem is ide kéne tenni...hanem a transaction-höz esetleg....
    }

    public abstract class Transaction : Default
    {
        protected int Amount { get; set; }
        protected Technical.Currency.CurrencyType Currency { get; set; }
        protected DateTime Date { get; set; }
        protected DateTime Time { get; set; }
        protected string Message { get; set; }

        public Transaction() :base()
        {
            Amount = 0;
            Currency = Technical.Currency.CurrencyType.None;
            Date = Convert.ToDateTime("1900.01.01");
            Time = Convert.ToDateTime("00:00:00");
            Message = null;
        }

        public Transaction(int amount, string curr, DateTime date, DateTime time, string msg)
        :base()
        {
            Amount = amount;
            Currency = Technical.Currency.ConvertStringToCurrencyType(curr);
            Date = date;
            Time = time;
            Message = msg;
        }

        public Transaction(string id, int amount, string curr, DateTime date, DateTime time, string msg)
        :base(id)
        {
            Amount = amount;
            Currency = Technical.Currency.ConvertStringToCurrencyType(curr);
            Date = date;
            Time = time;
            Message = msg;
        }

        protected override Dictionary<string, object> PackToDataLayer()
        {
            //csak az öröklődés miatt kell
            return new Dictionary<string, object>();
        }

        protected override void UnpackFromDataLayer(DataSet ds)
        {
            //csak az öröklődés miatt kell
        }

        protected override DataSet PackToViewLayer()
        {
            //csak az öröklődés miatt kell
            return new DataSet();
        }


        //Kérdés, hogy ezzel mi lesz??? mert ez egy közös függvény! És innen nem is tudjuk majd meghívni.
        //De legalább öröklik őt, szóval talán mégis meg lehet hívni majd
        public abstract DataSet ListTransactions(string acc_num);
    }

    public class Transfer : Transaction
    {
        public string SourceAccountNumber { get; set; }
        public string DestinationAccountNumber { get; set; }
        public string SourceName { get; set; }
        public string DestinationName { get; set; }

        public Transfer() : base()
        {
            SourceAccountNumber = null;
            DestinationAccountNumber = null;
            SourceName = null;
            DestinationName = null;
        }

        public Transfer(string sourceAccountNumber, string destinationAccountNumber, string sourceName, string destinationName, int amount, string currency, DateTime date, DateTime time, string msg)
        : base(amount, currency, date, time, msg)
        {
            SourceAccountNumber = sourceAccountNumber;
            DestinationAccountNumber = destinationAccountNumber;
            SourceName = sourceName;
            DestinationName = destinationName;
        }

        public Transfer(string id, string sourceAccountNumber, string destinationAccountNumber, string sourceName, string destinationName, int amount, string currency, DateTime date, DateTime time, string msg)
        : base(id, amount, currency, date, time, msg)
        {
            SourceAccountNumber = sourceAccountNumber;
            DestinationAccountNumber = destinationAccountNumber;
            SourceName = sourceName;
            DestinationName = destinationName;
        }

        protected override Dictionary<string, object> PackToDataLayer()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            if (ID != null)
            {
                result.Add("id", ID);
            }
            result.Add("sourceAccountNumber", SourceAccountNumber);
            result.Add("destinationAccountNumber", DestinationAccountNumber);
            result.Add("sourceName", SourceName);
            result.Add("destinationName", DestinationName);
            result.Add("amount", Amount);
            result.Add("currency", Technical.Currency.ConvertCurrencyTypeToString(Currency));
            result.Add("date", Date);
            result.Add("time", Time);
            result.Add("message", Message);
            return result;
        }

        protected override void UnpackFromDataLayer(DataSet ds)
        {
            //megnézzük, hogy id tartozik e hozzá
            if (ds.Tables[0].Columns.Contains("id"))
            {
                //ha igen, akkor, tudjuk, hogy már ügyfél az illető
                ID = ds.Tables[0].Rows[0]["id"].ToString();
            }
            else
            {
                //ha nincs id, akkor pedig valószínűleg most hozzuk létre az ügyfelet, vagy simán egy
                //átmeneti ügyfél objektummal dolgozunk
            }

            //elvileg ez a függvény csak a datalayer-ből hívódik meg....
            //ha mégse csak onnan, akkor később átpiszkáljuk majd
            SourceAccountNumber = ds.Tables[0].Rows[0]["sourceAccountNumber"].ToString();
            DestinationAccountNumber = ds.Tables[0].Rows[0]["destinationAccountNumber"].ToString();
            SourceName = ds.Tables[0].Rows[0]["sourceName"].ToString();
            DestinationName = ds.Tables[0].Rows[0]["destinationName"].ToString();
            Amount = int.Parse(ds.Tables[0].Rows[0]["amount"].ToString());
            Currency = Technical.Currency.ConvertStringToCurrencyType(ds.Tables[0].Rows[0]["currency"].ToString());
            Date = Convert.ToDateTime(ds.Tables[0].Rows[0]["date"]);
            Time = Convert.ToDateTime(ds.Tables[0].Rows[0]["time"]);
            Message = ds.Tables[0].Rows[0]["message"].ToString();
        }

        protected override DataSet PackToViewLayer()
        {
            DataSet result = new DataSet();
            result.Tables.Add(new DataTable());
            result.Tables[0].Columns.Add("id");
            result.Tables[0].Columns.Add("sourceAccountNumber");
            result.Tables[0].Columns.Add("destinationAccountNumber");
            result.Tables[0].Columns.Add("sourceName");
            result.Tables[0].Columns.Add("destinationName");
            result.Tables[0].Columns.Add("amount");
            result.Tables[0].Columns.Add("currency");
            result.Tables[0].Columns.Add("date");
            result.Tables[0].Columns.Add("time");
            result.Tables[0].Columns.Add("message");

            DataRow row = result.Tables[0].NewRow();
            row["id"] = ID;
            row["sourceAccountNumber"] = SourceAccountNumber;
            row["destinationAccountNumber"] = DestinationAccountNumber;
            row["sourceName"] = SourceName;
            row["destinationName"] = DestinationName;
            row["amount"] = Amount;
            row["currency"] = Technical.Currency.ConvertCurrencyTypeToString(Currency);
            row["date"] = Date;
            row["time"] = Time;
            row["message"] = Message;

            result.Tables[0].Rows.Add(row);
            return result;
        }

        public string CreateTransferTransaction()
        {
            //konstruktor tölti fel első lépésben, onnan tesszük át ide
            return DataLayer.Transaction.UserMethods.CreateTransferTransaction(PackToDataLayer());
        }

        //Módosítás, törlés nem kell

        public override DataSet ListTransactions(string accountNumber)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["accountNumber"] = accountNumber;
            UnpackFromDataLayer(DataLayer.Transaction.UserMethods.SelectTransactionRecordsByAccountNumber(parameters));
            return PackToViewLayer();
        }
    }

    public class CashTransaction : Transaction
    {
        public  Technical.CashTransaction.CashTransactionType Type { get; set; }
        public string AccountNumber { get; set; }

        public CashTransaction() :base()
        {
            Type = Technical.CashTransaction.CashTransactionType.None;
            AccountNumber = null;
        }

        public CashTransaction(string type, string accountNumber, int amount, string currency, DateTime date, DateTime time, string message)
            :base(amount, currency, date, time, message)
        {
            Type = Technical.CashTransaction.StringToCashTransactionType(type);
            AccountNumber = accountNumber;
        }

        public CashTransaction(string id, string type, string accountNumber, int amount, string currency, DateTime date, DateTime time, string message)
            : base(id, amount, currency, date, time, message)
        {
            Type = Technical.CashTransaction.StringToCashTransactionType(type);
            AccountNumber = accountNumber;
        }

        protected override Dictionary<string, object> PackToDataLayer()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            if (ID != null)
            {
                result.Add("id", ID);
            }
            result.Add("type", Technical .CashTransaction.CashTransactionTypeToString(Type));
            result.Add("accountNumber", AccountNumber);
            result.Add("amount", Amount);
            result.Add("currency", Technical.Currency.ConvertCurrencyTypeToString(Currency));
            result.Add("date", Date);
            result.Add("time", Time);
            result.Add("message", Message);
            return result;
        }

        protected override void UnpackFromDataLayer(DataSet ds)
        {
            //megnézzük, hogy id tartozik e hozzá
            if (ds.Tables[0].Columns.Contains("id"))
            {
                //ha igen, akkor, tudjuk, hogy már ügyfél az illető
                ID = ds.Tables[0].Rows[0]["id"].ToString();
            }
            else
            {
                //ha nincs id, akkor pedig valószínűleg most hozzuk létre az ügyfelet, vagy simán egy
                //átmeneti ügyfél objektummal dolgozunk
            }

            //elvileg ez a függvény csak a datalayer-ből hívódik meg....
            //ha mégse csak onnan, akkor később átpiszkáljuk majd

            //Lehet hogy lesz gond, mert a DataLayer még máshogy néz ki...
            Type = Technical.CashTransaction.StringToCashTransactionType(ds.Tables[0].Rows[0]["type"].ToString());
            Amount = (int)(ds.Tables[0].Rows[0]["amount"]);
            if(Type == Technical.CashTransaction.CashTransactionType.Deposit)
            {
                AccountNumber = ds.Tables[0].Rows[0]["dst_acc_num"].ToString();
            }
            else if (Type == Technical.CashTransaction.CashTransactionType.WithDrawl)
            {
                AccountNumber = ds.Tables[0].Rows[0]["src_acc_num"].ToString();
            }
            Currency = Technical.Currency.ConvertStringToCurrencyType(ds.Tables[0].Rows[0]["currency"].ToString());
            Date = Convert.ToDateTime(ds.Tables[0].Rows[0]["date"]);
            Time = Convert.ToDateTime(ds.Tables[0].Rows[0]["time"]);
            Message = ds.Tables[0].Rows[0]["message"].ToString();
        }

        protected override DataSet PackToViewLayer()
        {
            DataSet result = new DataSet();
            result.Tables.Add(new DataTable());
            result.Tables[0].Columns.Add("id");
            result.Tables[0].Columns.Add("type");
            result.Tables[0].Columns.Add("sourceAccountNumber");
            result.Tables[0].Columns.Add("destinationAccountNumber");
            result.Tables[0].Columns.Add("amount");
            result.Tables[0].Columns.Add("currency");
            result.Tables[0].Columns.Add("date");
            result.Tables[0].Columns.Add("time");
            result.Tables[0].Columns.Add("message");

            DataRow row = result.Tables[0].NewRow();
            row["id"] = ID;
            row["type"] = Technical.CashTransaction.CashTransactionTypeToString(Type);
            if (Type == Technical.CashTransaction.CashTransactionType.Deposit)
            {
                row["sourceAccountNumber"] = "";
                row["destinationAccountNumber"] = AccountNumber;
            }
            else if (Type == Technical.CashTransaction.CashTransactionType.WithDrawl)
            {
                row["destinationAccountNumber"] = "";
                row["sourceAccountNumber"] = AccountNumber;
            }
            row["amount"] = Amount;
            row["currency"] = Technical.Currency.ConvertCurrencyTypeToString(Currency);
            row["date"] = Date;
            row["time"] = Time;
            row["message"] = Message;

            result.Tables[0].Rows.Add(row);
            return result;
        }

        public string CreateDeposit(string acc_num)
        {
            //konstruktor tölti fel első lépésben, onnan tesszük át ide
            //az acc_num azért kell ide, mert nem akarjuk eldönteni, hogy source vagy destionation legyen
            //nem tudnánk utána a convertálásnál lefordítani
            return DataLayer.Transaction.UserMethods.CreateCashInTransaction(PackToDataLayer());
        }

        public string CreateWithdrawl(string acc_num)
        {
            //konstruktor tölti fel első lépésben, onnan tesszük át ide
            return DataLayer.Transaction.UserMethods.CreateCashOutTransaction(PackToDataLayer());
        }

        //Módosítás, törlés nem kell

        public override DataSet ListTransactions(string acc_num)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["acc_num"] = acc_num;
            UnpackFromDataLayer(DataLayer.Transaction.UserMethods.SelectTransactionRecordsByAccountNumber(parameters));
            return PackToViewLayer();
        }
    }


    //Lehet hogy a Card egy base class lesz
    //És lesz benne DebitCard meg CreditCard...
    //A CreditCard valamilyen "Credit class"-tól fog majd örökölni mindenféle dolgot...

    public class Card : Default
    {
        protected string ClientID { get; set; }
        protected string AccountID { get; set; }
        protected Technical.Card.CardType Type { get; set; }
        protected string CardNumber { get; set; }

        //Ez is ilyen dupla-mezős tulajdonság lesz
        protected Technical.Currency.CurrencyType Currency { get; set; }
        protected string CurrencyCode { get; set; }
        protected char DebitCredit { get; set; }

        protected DateTime DateOfRequest { get; set; }
        protected DateTime DateOfExpiration { get; set; }

        //át kell még írni őket...
        protected override Dictionary<string, object> PackToDataLayer()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            if (ID != null)
            {
                result.Add("id", ID);
            }
            result.Add("clientID", ClientID);
            result.Add("accountID", AccountID);
            result.Add("type", Type);
            result.Add("cardNumber", CardNumber);
            result.Add("currency", Currency);
            result.Add("debitCredit", DebitCredit);
            result.Add("dateOfRequest", DateOfRequest);
            result.Add("dateOfExpire", DateOfExpiration);
            return result;
        }

        protected override void UnpackFromDataLayer(DataSet ds)
        {
            //megnézzük, hogy id tartozik e hozzá
            if (ds.Tables[0].Columns.Contains("id"))
            {
                //ha igen, akkor, tudjuk, hogy már ügyfél az illető
                ID = ds.Tables[0].Rows[0]["id"].ToString();
            }
            else
            {
                //ha nincs id, akkor pedig valószínűleg most hozzuk létre az ügyfelet, vagy simán egy
                //átmeneti ügyfél objektummal dolgozunk
            }

            //elvileg ez a függvény csak a datalayer-ből hívódik meg....
            //ha mégse csak onnan, akkor később átpiszkáljuk majd
            ClientID = ds.Tables[0].Rows[0]["clientID"].ToString();
            AccountID = ds.Tables[0].Rows[0]["accountID"].ToString();
            Type = Technical.Card.StringToCardType(ds.Tables[0].Rows[0]["type"].ToString());
            CardNumber = ds.Tables[0].Rows[0]["cardNumber"].ToString();
            Currency = Technical.Currency.ConvertStringToCurrencyType(ds.Tables[0].Rows[0]["currency"].ToString());
            DebitCredit = (ds.Tables[0].Rows[0]["debitCredit"].ToString())[0];
            DateOfRequest = Convert.ToDateTime(ds.Tables[0].Rows[0]["dateOfRequest"]);
            DateOfExpiration = Convert.ToDateTime(ds.Tables[0].Rows[0]["dateOfExpire"]);
        }

        protected override DataSet PackToViewLayer()
        {
            DataSet result = new DataSet();
            result.Tables.Add(new DataTable());
            result.Tables[0].Columns.Add("id");
            result.Tables[0].Columns.Add("clientID");
            result.Tables[0].Columns.Add("accountID");
            result.Tables[0].Columns.Add("type");
            result.Tables[0].Columns.Add("cardNumber");
            result.Tables[0].Columns.Add("currency");
            result.Tables[0].Columns.Add("debitCredit");
            result.Tables[0].Columns.Add("dateOfRequest");
            result.Tables[0].Columns.Add("dateOfExpire");

            DataRow row = result.Tables[0].NewRow();
            row["id"] = ID;
            row["clientID"] = ClientID;
            row["accountID"] = AccountID;
            row["type"] = Type;
            row["cardNumber"] = CardNumber;
            row["currency"] = Currency;
            row["debitCredit"] = DebitCredit;
            row["dateOfRequest"] = DateOfRequest;
            row["dateOfExpire"] = DateOfExpiration;

            result.Tables[0].Rows.Add(row);
            return result;
        }

        public void CreateCard()
        {

        }

        public void ModifyCard()
        {
            //Ez egyelőre nem kell
        }

        public void DeleteCard()
        {

        }
    }

}
