using System;
using System.Collections.Generic;
using System.Data;


namespace NewBusinessLayer
{
    public static class CurrencySystemV1
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


    namespace BusinessLayerExceptions
    {
        public class InvalidTypeException : Exception
        {
            public InvalidTypeException() : base() { }
            public InvalidTypeException(string message) : base(message) { }
            public InvalidTypeException(string message, Exception inner) : base(message, inner) { }
        }

        public class MissingParameterException : Exception
        {
            public MissingParameterException() : base() { }
            public MissingParameterException(string message) : base(message) { }
            public MissingParameterException(string message, Exception inner) : base(message, inner) { }
        }
    }

    namespace Technical
    {
        public enum AccountType
        {
            None,
            Junior,
            Basic,
            Premium,
            Senior
        }
        public enum SectorType
        {
            None,
            Retail,
            Corporate
        }
        public enum CurrencyType
        {
            None = 0,
            HUF = 1,
            CHF = 2,
            EUR = 3,
            GBP = 4,
            USD = 5
        }
        public enum CashTransactionType
        {
            None,
            Deposit,
            WithDrawl
        }
        public enum CardProviderType
        {
            None,
            Maestro,
            MasterCard,
            VisaElectron,
            VisaClassic
        }
        public enum CardType
        {
            None,
            Debit,
            Credit
        }
        public enum RoleType
        {
            None,
            Owner,
            Trustee
        }

        public static class Convert
        {
            //Univerzális konvertáló függvények
            public static string UniversalConvertToString(object obj)
            {
                if (obj is AccountType)
                {
                    return AccountTypeToString((AccountType)obj);
                }
                else if (obj is SectorType)
                {
                    return SectorTypeToString((SectorType)obj);
                }
                else if (obj is CurrencyType)
                {
                    return CurrencyTypeToString((CurrencyType)obj);
                }
                else if (obj is CashTransactionType)
                {
                    return CashTransactionTypeToString((CashTransactionType)obj);
                }
                else if (obj is CardProviderType)
                {
                    return CardProviderTypeToString((CardProviderType)obj);
                }
                else if (obj is CardType)
                {
                    return CardTypeToString((CardType)obj);
                }
                else if (obj is RoleType)
                {
                    return RoleTypeToString((RoleType)obj);
                }
                else
                {
                    throw new BusinessLayerExceptions.InvalidTypeException();
                }
            }

            public static Enum UniversalConvertFromString(string obj, Enum type)
            {
                if (type is AccountType)
                {
                    return StringToAccountType(obj);
                }
                else if (type is SectorType)
                {
                    return StringToSectorType(obj);
                }
                else if (type is CurrencyType)
                {
                    return StringToCurrencyType(obj);
                }
                else if (type is CashTransactionType)
                {
                    return StringToCashTransactionType(obj);
                }
                else if (type is CardProviderType)
                {
                    return StringToCardProviderType(obj);
                }
                else if (type is CardType)
                {
                    return StringToCardType(obj);
                }
                else if (type is RoleType)
                {
                    return StringToRoleType(obj);
                }
                else
                {
                    throw new BusinessLayerExceptions.InvalidTypeException();
                }
            }

            //Konvertáló függvények
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
                    case "none":
                        return AccountType.None;
                    default:
                        throw new BusinessLayerExceptions.InvalidTypeException("Invalid AccountType!");
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
                    case AccountType.None:
                        return "none";
                    default:
                        throw new BusinessLayerExceptions.InvalidTypeException("Invalid AccountType!");
                }
            }

            public static SectorType StringToSectorType(string type)
            {
                switch (type)
                {
                    case "retail":
                        return SectorType.Retail;
                    case "corporate":
                        return SectorType.Corporate;
                    case "none":
                        return SectorType.None;
                    default:
                        throw new BusinessLayerExceptions.InvalidTypeException("Invalid SectorType!");
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
                    case SectorType.None:
                        return "none";
                    default:
                        throw new BusinessLayerExceptions.InvalidTypeException("Invalid SectorType!");
                }
            }

            public static CurrencyType StringToCurrencyType(string type)
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
                    case "none":
                        return CurrencyType.None;
                    default:
                        throw new BusinessLayerExceptions.InvalidTypeException("Invalid CurrencyType!");
                }
            }

            public static string CurrencyTypeToString(CurrencyType type)
            {
                switch (type)
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
                    case CurrencyType.None:
                        return "none";
                    default:
                        throw new BusinessLayerExceptions.InvalidTypeException("Invalid CurrencyType!");
                }
            }

            public static CashTransactionType StringToCashTransactionType(string type)
            {
                switch (type)
                {
                    case "deposit":
                        return CashTransactionType.Deposit;
                    case "withdrawl":
                        return CashTransactionType.WithDrawl;
                    case "none":
                        return CashTransactionType.None;
                    default:
                        throw new BusinessLayerExceptions.InvalidTypeException("Invalid CashTransactionType!");
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
                    case CashTransactionType.None:
                        return "none";
                    default:
                        throw new BusinessLayerExceptions.InvalidTypeException("Invalid CashTransactionType!");
                }
            }

            public static CardProviderType StringToCardProviderType(string type)
            {
                switch (type)
                {
                    case "maestro":
                        return CardProviderType.Maestro;
                    case "mastercard":
                        return CardProviderType.MasterCard;
                    case "visaelectron":
                        return CardProviderType.VisaElectron;
                    case "visaclassic":
                        return CardProviderType.VisaClassic;
                    case "none":
                        return CardProviderType.None;
                    default:
                        throw new BusinessLayerExceptions.InvalidTypeException("Invalid CardProviderType!");
                }
            }

            public static string CardProviderTypeToString(CardProviderType type)
            {
                switch (type)
                {
                    case CardProviderType.Maestro:
                        return "maestro";
                    case CardProviderType.MasterCard:
                        return "mastercard";
                    case CardProviderType.VisaElectron:
                        return "visaelectron";
                    case CardProviderType.VisaClassic:
                        return "visaclassic";
                    case CardProviderType.None:
                        return "none";
                    default:
                        throw new BusinessLayerExceptions.InvalidTypeException("Invalid CardProviderType!");
                }
            }

            public static CardType StringToCardType(string type)
            {
                switch (type)
                {
                    case "debit":
                        return CardType.Debit;
                    case "credit":
                        return CardType.Credit;
                    case "none":
                        return CardType.None;
                    default:
                        throw new BusinessLayerExceptions.InvalidTypeException("Invalid CardType!");
                }
            }

            public static string CardTypeToString(CardType type)
            {
                switch (type)
                {
                    case CardType.Debit:
                        return "debit";
                    case CardType.Credit:
                        return "credit";
                    case CardType.None:
                        return "none";
                    default:
                        throw new BusinessLayerExceptions.InvalidTypeException("Invalid CardType!");
                }
            }

            public static RoleType StringToRoleType(string type)
            {
                switch (type)
                {
                    case "owner":
                        return RoleType.Owner;
                    case "trustee":
                        return RoleType.Trustee;
                    case "none":
                        return RoleType.None;
                    default:
                        throw new BusinessLayerExceptions.InvalidTypeException("Invalid RoleType!");
                }
            }

            public static string RoleTypeToString(RoleType type)
            {
                switch (type)
                {
                    case RoleType.Owner:
                        return "owner";
                    case RoleType.Trustee:
                        return "trustee";
                    case RoleType.None:
                        return "none";
                    default:
                        throw new BusinessLayerExceptions.InvalidTypeException("Invalid RoleType!");
                }
            }
        }

        //ez később majd valami teljesen más osztályba fog menni...
        public static class BankFunctions
        {
            public static string GenerateRandomAccNumber()
            {
                string result = "18203129000000000000";
                Random r = new Random();
                for (int i = 0; i < 4; i++)
                {
                    result += (r.Next(0, 9)).ToString();
                }
                return result;
            }
        }
    }


    //Sima alaposztályok

    //Aktív és inaktív állapot az egyes osztályoknál
    interface ILive
    {
        bool Live { get; set; }
    }

    public abstract class Base
    {
        //Azért kell, hogy őt lehessen használni mindenre!!!!
        //Arra is, aminek nincs ID-je és ezért nem a Base-től származik.

        public Base()
        {

        }
    }

    public abstract class Base2 : Base
    {
        public string ID { get; set; }

        public Base2()
        {
            ID = null;
        }

        public Base2(string id)
        {
            ID = id;
        }


    }

    public class Client : Base2, ILive
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string NameOfMother { get; set; }
        public string Address { get; set; }
        //fontos a formátum!
        public string Phone { get; set; }
        //fontos a formátum!
        public string Email { get; set; }
        public bool Live { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public Client() : base()
        {
            FirstName = null;
            LastName = null;
            DateOfBirth = Convert.ToDateTime("1900.01.01").Date;
            PlaceOfBirth = null;
            NameOfMother = null;
            Address = null;
            Phone = null;
            Email = null;
            Live = false;
        }

        public Client(string firstName, string lastName, DateTime dateOfBirth, string placeOfBirth, string nameOfMother, string address, string phone, string email, bool live)
        : base()
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            PlaceOfBirth = placeOfBirth;
            NameOfMother = nameOfMother;
            Address = address;
            Phone = phone;
            Email = email;
            Live = live;
        }

        public Client(string id, string firstName, string lastName, DateTime dateOfBirth, string placeOfBirth, string nameOfMother, string address, string phone, string email, bool live)
        : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            PlaceOfBirth = placeOfBirth;
            NameOfMother = nameOfMother;
            Address = address;
            Phone = phone;
            Email = email;
            Live = live;
        }
    }

    public class Account : Base2, ILive
    {
        public string AccountNumber { get; set; }
        public Technical.AccountType AccountName { get; set; }
        public Technical.AccountType AccountType { get; set; }
        public Technical.CurrencyType Currency { get; set; }
        public Technical.SectorType SectorType { get; set; }
        public int Balance { get; set; }
        public DateTime DateOfOpening { get; set; }
        public DateTime DateOfClosing { get; set; }
        public bool Live { get; set; }

        public Account() : base()
        {
            AccountNumber = null;
            AccountName = Technical.AccountType.None;
            AccountType = Technical.AccountType.None;
            Currency = Technical.CurrencyType.None;
            SectorType = Technical.SectorType.None;
            Balance = 0;
            DateOfOpening = Convert.ToDateTime("1900.01.01").Date;
            DateOfClosing = Convert.ToDateTime("9999.12.31").Date;
            Live = false;
        }

        public Account(string accountNumber, Technical.AccountType accountName, Technical.AccountType accountType, Technical.CurrencyType currency, Technical.SectorType sectorType, int balance, DateTime dateOfOpening, DateTime dateOfClosing, bool live)
            : base()
        {
            AccountNumber = accountNumber;
            AccountName = accountName;
            AccountType = accountType;
            Currency = currency;
            SectorType = sectorType;
            Balance = balance;
            DateOfOpening = dateOfOpening;
            DateOfClosing = dateOfClosing;
            Live = live;
        }

        public Account(string id, string accountNumber, Technical.AccountType accountName, Technical.AccountType accountType, Technical.CurrencyType currency, Technical.SectorType sectorType, int balance, DateTime dateOfOpening, DateTime dateOfClosing, bool live)
            : base(id)
        {
            AccountNumber = accountNumber;
            AccountName = accountName;
            AccountType = accountType;
            Currency = currency;
            SectorType = sectorType;
            Balance = balance;
            DateOfOpening = dateOfOpening;
            DateOfClosing = dateOfClosing;
            Live = live;
        }
    }

    public class Role : Base, ILive
    {
        public string ClientID { get; set; }
        public Technical.RoleType Type { get; set; }
        public bool Live { get; set; }

        public Role()
        {
            ClientID = null;
            Type = Technical.RoleType.None;
            Live = false;
        }

        public Role(string clientID, Technical.RoleType type, bool live)
        {
            ClientID = clientID;
            Type = type;
            Live = live;
        }
    }

    public abstract class Transaction : Base2
    {
        public int Amount { get; set; }
        public Technical.CurrencyType Currency { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }

        public Transaction() : base()
        {
            Amount = 0;
            Currency = Technical.CurrencyType.None;
            Date = Convert.ToDateTime("1900.01.01").Date;
            Time = Convert.ToDateTime("00:00:00");
            Message = null;
        }

        public Transaction(int amount, Technical.CurrencyType currency, DateTime date, DateTime time, string msg)
        : base()
        {
            Amount = amount;
            Currency = currency;
            Date = date;
            Time = time;
            Message = msg;
        }

        public Transaction(string id, int amount, Technical.CurrencyType currency, DateTime date, DateTime time, string msg)
        : base(id)
        {
            Amount = amount;
            Currency = currency;
            Date = date;
            Time = time;
            Message = msg;
        }
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

        public Transfer(string sourceAccountNumber, string destinationAccountNumber, string sourceName, string destinationName, int amount, Technical.CurrencyType currency, DateTime date, DateTime time, string msg)
        : base(amount, currency, date, time, msg)
        {
            SourceAccountNumber = sourceAccountNumber;
            DestinationAccountNumber = destinationAccountNumber;
            SourceName = sourceName;
            DestinationName = destinationName;
        }

        public Transfer(string id, string sourceAccountNumber, string destinationAccountNumber, string sourceName, string destinationName, int amount, Technical.CurrencyType currency, DateTime date, DateTime time, string msg)
        : base(id, amount, currency, date, time, msg)
        {
            SourceAccountNumber = sourceAccountNumber;
            DestinationAccountNumber = destinationAccountNumber;
            SourceName = sourceName;
            DestinationName = destinationName;
        }
    }

    public class CashTransaction : Transaction
    {
        public Technical.CashTransactionType Type { get; set; }
        public string AccountNumber { get; set; }

        public CashTransaction() : base()
        {
            Type = Technical.CashTransactionType.None;
            AccountNumber = null;
        }

        public CashTransaction(Technical.CashTransactionType type, string accountNumber, int amount, Technical.CurrencyType currency, DateTime date, DateTime time, string message)
            : base(amount, currency, date, time, message)
        {
            Type = type;
            AccountNumber = accountNumber;
        }

        public CashTransaction(string id, Technical.CashTransactionType type, string accountNumber, int amount, Technical.CurrencyType currency, DateTime date, DateTime time, string message)
            : base(id, amount, currency, date, time, message)
        {
            Type = type;
            AccountNumber = accountNumber;
        }
    }

    public class Card : Base2, ILive
    {
        public Technical.CardProviderType ProviderType { get; set; }
        public string CardNumber { get; set; }
        public Technical.CurrencyType Currency { get; set; }
        public Technical.CardType Type { get; set; }
        public DateTime DateOfRequest { get; set; }
        public DateTime DateOfExpiration { get; set; }
        public bool Live { get; set; }

        public Card() : base()
        {
            ProviderType = Technical.CardProviderType.None;
            CardNumber = null;
            Currency = Technical.CurrencyType.None;
            Type = Technical.CardType.None;
            DateOfRequest = Convert.ToDateTime("1900.01.01").Date;
            DateOfExpiration = Convert.ToDateTime("9999.12.31").Date;
            Live = false;
        }

        public Card(Technical.CardProviderType providerType, string cardNumber, Technical.CurrencyType currency, Technical.CardType type, DateTime dateOfRequest, DateTime dateOfExpiration, bool live)
            : base()
        {
            ProviderType = providerType;
            CardNumber = cardNumber;
            Currency = currency;
            Type = type;
            DateOfRequest = dateOfRequest;
            DateOfExpiration = dateOfExpiration;
            Live = live;
        }

        public Card(string id, Technical.CardProviderType providerType, string cardNumber, Technical.CurrencyType currency, Technical.CardType type, DateTime dateOfRequest, DateTime dateOfExpiration, bool live)
            : base(id)
        {
            ProviderType = providerType;
            CardNumber = cardNumber;
            Currency = currency;
            Type = type;
            DateOfRequest = dateOfRequest;
            DateOfExpiration = dateOfExpiration;
            Live = live;
        }
    }




    //List osztályok

    public abstract class ListOfElements
    {
        protected List<Base> list = new List<Base>();

        public void Add(Base item)
        {
            if (list.GetType().GetProperty("Item").PropertyType == item.GetType())
            {
                list.Add(item);
            }
            else
            {
                throw new BusinessLayerExceptions.InvalidTypeException("Parameter type must be " + list.GetType().GetProperty("Item").PropertyType.ToString() + "!");
            }
        }

        public void Remove(Base item)
        {
            if (list.GetType().GetProperty("Item").PropertyType == item.GetType())
            {
                list.Remove(item);
            }
            else
            {
                throw new BusinessLayerExceptions.InvalidTypeException("Parameter type must be " + list.GetType().GetProperty("Item").PropertyType.ToString() + "!");
            }
        }

        public void Clear()
        {
            list.Clear();
        }

        public List<Base> GetList()
        {
            return list;
        }
    }

    public class Base2s : ListOfElements
    {
        protected new List<Base2> list = new List<Base2>();
    }

    public class Clients : ListOfElements
    {
        protected new List<Client> list = new List<Client>();
    }

    public class Accounts : ListOfElements
    {
        protected new List<Account> list = new List<Account>();
    }

    public class Roles : ListOfElements
    {
        protected new List<Role> list = new List<Role>();
    }

    public class Transactions : ListOfElements
    {
        protected new List<Transaction> list = new List<Transaction>();
    }

    public class Transfers : ListOfElements
    {
        protected new List<Transfer> list = new List<Transfer>();
    }

    public class CashTransactions : ListOfElements
    {
        protected new List<CashTransaction> list = new List<CashTransaction>();
    }

    public class Cards : ListOfElements
    {
        protected new List<Card> list = new List<Card>();
    }




    //Kiterjesztett osztályok

    //A lényeg itt az lenne, hogy automatikusan feltöli a dictionary-ből vagy datarow-ból az objectet
    //úgy, hogy kiszedi hogy milyen tulajdonságai vannak....
    //kifejezetten azokra a kulcsszavakra fog rákeresni, amik a tulajdonságnevek
    //és azzal a típussal fogja őket cast-olni, ami az adott tulajdonság típusa


    
    public class ExtendedDynamicClasses
    {

        public abstract class ExtendedBase
        {
            protected Base obj;

            public string TestFunc()
            {
                string result = "";
                Client car = new Client();



                foreach (var property in car.GetType().GetProperties())
                {
                    result += property.Name + " - ";
                    result += property.PropertyType + " | ";
                    result += property.GetValue(car, null) + " | ";

                }
                //return car;
                return result;
            }

            public virtual void ImportFromDictionary(Dictionary<string, object> parameters)
            {
                if (parameters != null)
                {
                    //végigmegyünk a tulajdonságokon
                    foreach(var property in obj.GetType().GetProperties())
                    {
                        //ha írható a tulajdonság és a kulcsnevek között is szerepel a tulajdonság neve
                        if(property.CanWrite && parameters.ContainsKey(property.Name))
                        {
                            //a property típusára konvertáljuk, a property nevével egyező kulcsnevű elemet a dictionary-ből
                            property.SetValue(obj, Technical.Convert.UniversalConvertFromString(parameters[property.Name], (Enum)property.PropertyType), null);

                            //1. kiszedjük az értéket a parameters-ből
                            object rawValue = parameters[property.Name];

                            //2. átkonvertáljuk az értéket
                            object convertedValue = new object();
                            convertedValue = Technical.Convert.UniversalConvertFromString(rawValue, )


                            //így nézne ki alapból
                            
                            //(SectorType)
                            Technical.Convert.StringToSectorType(rawValue.ToString());

                            //de itt a típust is le kell kérni még pluszban

                            //(property.PropertyType)
                            Technical.Convert.UniversalConvertFromString(rawValue.ToString(), (Enum)Enum.ToObject(property.PropertyType, rawValue));
                            //nem jó!!! nekünk itt csak a típus kell most!

                            Technical.Convert.UniversalConvertFromString(rawValue.ToString(), Enum.ToObject(property.PropertyType);

                            string a = "";
                            Enum.ToObject((a.GetType()) , property.PropertyType);

                            object b = new object();
                            Enum.Parse();



                            ///
                            /// typeof() lesz a megoldás!!!!!
                            /// 






                            //Nem mind enum!!!! isEnum-al kell majd megnézni!!!


                            //3. beállítjuk a tulajdonságot... megkapja a convertedValue értékét, ami már a helyes típusba lett konvertálva
                            property.SetValue(obj, convertedValue, null);


                            property.SetValue(obj, Technical.Convert.UniversalConvertFromString(parameters[property.Name].ToString(), (Enum)Enum.ToObject(property.PropertyType, parameters[""])), null);


                            Technical.SectorType sect = (Technical.SectorType)Enum.ToObject(property.PropertyType, parameters[""]);

                            var s = new ((property).PropertyType)();
                            sect = Enum.ToObject(property.PropertyType, parameters[""]);
                            Enum.ToObject(property.PropertyType, parameters[""]);
                        }

                        //prop.SetValue(item, Enum.ToObject(prop.PropertyType, row[prop.Name]), null);


                    }
                    

                    /// végig menni az egyes tulajdonságokon, amik-nek van set részük
                    /// parameters-ben megnézni, hogy szerepel e benne ilyen kulcs!
                    ///     ha nem szerepel, akkor kivétel
                    ///     ha szerepel, akkor abból töltsük fel, a megfelelő típussal


                    
                    if (parameters.ContainsKey("id"))
                    {
                        obj.ID = parameters["id"].ToString();
                    }
                    else
                    {
                        throw new BusinessLayerExceptions.MissingParameterException("The \"id\" parameter is missing!");
                    }
                    

                }

            }

            public virtual void ImportFromDataRow(DataRow parameters)
            {
                if (obj != null)
                {
                    if (!parameters.IsNull("id"))
                    {
                        obj.ID = parameters["id"].ToString();
                    }
                    else
                    {
                        throw new BusinessLayerExceptions.MissingParameterException("The \"id\" parameter is missing!");
                    }
                }
            }

            public virtual Dictionary<string, object> ExportToDictionary()
            {
                var dict = new Dictionary<string, object>();
                if (obj != null && obj.ID != null)
                {
                    dict.Add("id", obj.ID);
                }
                return dict;
            }

            public virtual DataRow ExportToDataRow()
            {
                var table = new DataTable(); //csak átmenetileg kell nekünk, így lehet csak megoldani a konstruktort a DataRow-nak
                table.Columns.Add("id");
                DataRow row = table.NewRow();
                if (obj != null && obj.ID != null)
                {
                    row["id"] = obj.ID;
                }
                return row;
            }

            public Base Send()
            {
                return obj;
            }

            public void Receive(Base o)
            {
                if (obj.GetType() == o.GetType())
                {
                    obj = o;
                }
            }
        }

        public class ExtendedClient : ExtendedBase
        {
            protected new Client obj;

            public override void ImportFromDictionary(Dictionary<string, object> parameters)
            {
                obj = new Client();
                base.ImportFromDictionary(parameters);
            }

            public override void ImportFromDataRow(DataRow parameters)
            {
                obj = new Client();
                base.ImportFromDataRow(parameters);
            }

            public override Dictionary<string, object> ExportToDictionary()
            {
                obj = new Client();
                return base.ExportToDictionary();
            }

            public override DataRow ExportToDataRow()
            {
                obj = new Client();
                return base.ExportToDataRow();
            }
        }
    }


    //Kiterjesztett osztályok
    public abstract class ExtendedBase
    {
        protected Base obj;

        public abstract void ImportFromDictionary(Dictionary<string, object> parameters);

        public abstract void ImportFromDataRow(DataRow parameters);

        public abstract Dictionary<string, object> ExportToDictionary();

        public abstract DataRow ExportToDataRow();

        public Base Send()
        {
            return obj;
        }

        public void Receive(Base o)
        {
            if (obj.GetType() == o.GetType())
            {
                obj = o;
            }
        }
    }

    public class ExtendedBase2 : ExtendedBase
    {
        protected new Base2 obj;

        public override void ImportFromDictionary(Dictionary<string, object> parameters)
        {
            if (obj != null)
            {
                if (parameters.ContainsKey("id"))
                {
                    obj.ID = parameters["id"].ToString();
                }
                else
                {
                    throw new BusinessLayerExceptions.MissingParameterException("The \"id\" parameter is missing!");
                }
            }
        }

        public override void ImportFromDataRow(DataRow parameters)
        {
            if (obj != null)
            {
                if (!parameters.IsNull("id"))
                {
                    obj.ID = parameters["id"].ToString();
                }
                else
                {
                    throw new BusinessLayerExceptions.MissingParameterException("The \"id\" parameter is missing!");
                }
            }
        }

        public override Dictionary<string, object> ExportToDictionary()
        {
            var dict = new Dictionary<string, object>();
            if (obj != null && obj.ID != null)
            {
                dict.Add("id", obj.ID);
            }
            return dict;
        }

        public override DataRow ExportToDataRow()
        {
            var table = new DataTable(); //csak átmenetileg kell nekünk, így lehet csak megoldani a konstruktort a DataRow-nak
            table.Columns.Add("id");
            DataRow row = table.NewRow();
            if (obj != null && obj.ID != null)
            {
                row["id"] = obj.ID;
            }
            return row;
        }
    }

    public class ExtendedClient : ExtendedBase2
    {
        protected new Client obj;

        public override void ImportFromDictionary(Dictionary<string, object> parameters)
        {
            obj = new Client();
            base.ImportFromDictionary(parameters);
            if (parameters.ContainsKey("firstName"))
            {
                obj.FirstName = parameters["firstName"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"firstName\" parameter is missing!");
            }
            if (parameters.ContainsKey("lastName"))
            {
                obj.LastName = parameters["lastName"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"lastName\" parameter is missing!");
            }
            if (parameters.ContainsKey("dateOfBirth"))
            {
                obj.DateOfBirth = Convert.ToDateTime(parameters["dateOfBirth"]).Date;
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"dateOfBirth\" parameter is missing!");
            }
            if (parameters.ContainsKey("placeOfBirth"))
            {
                obj.PlaceOfBirth = parameters["placeOfBirth"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"placeOfBirth\" parameter is missing!");
            }
            if (parameters.ContainsKey("nameOfMother"))
            {
                obj.NameOfMother = parameters["nameOfMother"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"nameOfMother\" parameter is missing!");
            }
            if (parameters.ContainsKey("address"))
            {
                obj.Address = parameters["address"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"address\" parameter is missing!");
            }
            if (parameters.ContainsKey("phone"))
            {
                obj.Phone = parameters["phone"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"phone\" parameter is missing!");
            }
            if (parameters.ContainsKey("email"))
            {
                obj.Email = parameters["email"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"email\" parameter is missing!");
            }
            if (parameters.ContainsKey("live"))
            {
                obj.Live = Convert.ToBoolean(parameters["live"]);
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"live\" parameter is missing!");
            }
        }

        public override void ImportFromDataRow(DataRow parameters)
        {
            obj = new Client();
            base.ImportFromDataRow(parameters);
            if (!parameters.IsNull("firstName"))
            {
                obj.FirstName = parameters["firstName"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"firstName\" parameter is missing!");
            }
            if (!parameters.IsNull("lastName"))
            {
                obj.LastName = parameters["lastName"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"lastName\" parameter is missing!");
            }
            if (!parameters.IsNull("dateOfBirth"))
            {
                obj.DateOfBirth = Convert.ToDateTime(parameters["dateOfBirth"]).Date;
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"dateOfBirth\" parameter is missing!");
            }
            if (!parameters.IsNull("placeOfBirth"))
            {
                obj.PlaceOfBirth = parameters["placeOfBirth"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"placeOfBirth\" parameter is missing!");
            }
            if (!parameters.IsNull("nameOfMother"))
            {
                obj.NameOfMother = parameters["nameOfMother"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"nameOfMother\" parameter is missing!");
            }
            if (!parameters.IsNull("address"))
            {
                obj.Address = parameters["address"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"address\" parameter is missing!");
            }
            if (!parameters.IsNull("phone"))
            {
                obj.Phone = parameters["phone"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"phone\" parameter is missing!");
            }
            if (!parameters.IsNull("email"))
            {
                obj.Email = parameters["email"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"email\" parameter is missing!");
            }
            if (!parameters.IsNull("live"))
            {
                obj.Live = Convert.ToBoolean(parameters["live"]);
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"live\" parameter is missing!");
            }
        }

        public override Dictionary<string, object> ExportToDictionary()
        {
            var dict = new Dictionary<string, object>();
            if (obj != null)
            {
                dict = base.ExportToDictionary();
                if (obj.FirstName != null)
                {
                    dict.Add("firstName", obj.FirstName);
                }
                if (obj.LastName != null)
                {
                    dict.Add("lastName", obj.LastName);
                }
                if (obj.DateOfBirth != null)
                {
                    dict.Add("dateOfBirth", obj.DateOfBirth);
                }
                if (obj.PlaceOfBirth != null)
                {
                    dict.Add("placeOfBirth", obj.PlaceOfBirth);
                }
                if (obj.NameOfMother != null)
                {
                    dict.Add("nameOfMother", obj.NameOfMother);
                }
                if (obj.Address != null)
                {
                    dict.Add("address", obj.Address);
                }
                if (obj.Phone != null)
                {
                    dict.Add("phone", obj.Phone);
                }
                if (obj.Email != null)
                {
                    dict.Add("email", obj.Email);
                }
                //Live-nál nem lehet "null" az érték, így nem tudjuk ellenőrizni
                dict.Add("live", obj.Live);
            }
            return dict;
        }

        public override DataRow ExportToDataRow()
        {
            var table = new DataTable(); //csak átmenetileg kell nekünk, így lehet csak megoldani a konstruktort a DataRow-nak
            DataRow row = table.NewRow();
            if (obj != null)
            {
                DataRow temp = base.ExportToDataRow();  //muszály így megoldani, hogy a DataRow működjön rendesen
                table.Columns.Add("id");
                row["id"] = temp["id"];
                if (obj.FirstName != null)
                {
                    table.Columns.Add("firstName");
                    row["firstName"] = obj.FirstName;
                }
                if (obj.LastName != null)
                {
                    table.Columns.Add("lastName");
                    row["lastName"] = obj.LastName;
                }
                if (obj.DateOfBirth != null)
                {
                    table.Columns.Add("dateOfBirth");
                    row["dateOfBirth"] = obj.DateOfBirth;
                }
                if (obj.PlaceOfBirth != null)
                {
                    table.Columns.Add("placeOfBirth");
                    row["placeOfBirth"] = obj.PlaceOfBirth;
                }
                if (obj.NameOfMother != null)
                {
                    table.Columns.Add("nameOfMother");
                    row["nameOfMother"] = obj.NameOfMother;
                }
                if (obj.Address != null)
                {
                    table.Columns.Add("address");
                    row["address"] = obj.Address;
                }
                if (obj.Phone != null)
                {
                    table.Columns.Add("phone");
                    row["phone"] = obj.Phone;
                }
                if (obj.Email != null)
                {
                    table.Columns.Add("email");
                    row["email"] = obj.Email;
                }
                //Live-nál nem lehet "null" az érték, így nem tudjuk ellenőrizni
                table.Columns.Add("live");
                row["live"] = obj.Live;
            }
            return row;
        }
    }

    public class ExtendedAccount : ExtendedBase2
    {
        protected new Account obj; //Újradefíniáljuk az obj-t

        public override void ImportFromDictionary(Dictionary<string, object> parameters)
        {
            obj = new Account();
            base.ImportFromDictionary(parameters);
            if (parameters.ContainsKey("accountNumber"))
            {
                obj.AccountNumber = parameters["accountNumber"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"accountNumber\" parameter is missing!");
            }
            if (parameters.ContainsKey("accountName"))
            {
                obj.AccountName = Technical.Convert.StringToAccountType(parameters["accountName"].ToString());
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"accountName\" parameter is missing!");
            }
            if (parameters.ContainsKey("accountType"))
            {
                obj.AccountType = Technical.Convert.StringToAccountType(parameters["accountType"].ToString());
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"accountType\" parameter is missing!");
            }
            if (parameters.ContainsKey("currency"))
            {
                obj.Currency = Technical.Convert.StringToCurrencyType(parameters["currency"].ToString());
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"currency\" parameter is missing!");
            }
            if (parameters.ContainsKey("sectorType"))
            {
                obj.SectorType = Technical.Convert.StringToSectorType(parameters["sectorType"].ToString());
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"sectorType\" parameter is missing!");
            }
            if (parameters.ContainsKey("balance"))
            {
                obj.Balance = int.Parse(parameters["balance"].ToString());
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"balance\" parameter is missing!");
            }
            if (parameters.ContainsKey("dateOfOpening"))
            {
                obj.DateOfOpening = Convert.ToDateTime(parameters["dateOfOpening"]).Date;
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"dateOfOpening\" parameter is missing!");
            }
            if (parameters.ContainsKey("dateOfClosing"))
            {
                obj.DateOfClosing = Convert.ToDateTime(parameters["dateOfClosing"]).Date;
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"dateOfClosing\" parameter is missing!");
            }
            if (parameters.ContainsKey("live"))
            {
                obj.Live = Convert.ToBoolean(parameters["live"]);
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"live\" parameter is missing!");
            }
        }

        public override void ImportFromDataRow(DataRow parameters)
        {
            obj = new Account();
            base.ImportFromDataRow(parameters);
            if (!parameters.IsNull("accountNumber"))
            {
                obj.AccountNumber = parameters["accountNumber"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"AccountNumber\" parameter is missing!");
            }
            if (!parameters.IsNull("accountName"))
            {
                obj.AccountName = Technical.Convert.StringToAccountType(parameters["accountName"].ToString());
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"AccountName\" parameter is missing!");
            }
            if (!parameters.IsNull("accountType"))
            {
                obj.AccountType = Technical.Convert.StringToAccountType(parameters["accountType"].ToString());
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"AccountType\" parameter is missing!");
            }
            if (!parameters.IsNull("currency"))
            {
                obj.Currency = Technical.Convert.StringToCurrencyType(parameters["currency"].ToString());
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"Currency\" parameter is missing!");
            }
            if (!parameters.IsNull("sectorType"))
            {
                obj.SectorType = Technical.Convert.StringToSectorType(parameters["sectorType"].ToString());
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"SectorType\" parameter is missing!");
            }
            if (!parameters.IsNull("balance"))
            {
                obj.Balance = int.Parse(parameters["balance"].ToString());
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"Balance\" parameter is missing!");
            }
            if (!parameters.IsNull("dateOfOpening"))
            {
                obj.DateOfOpening = Convert.ToDateTime(parameters["dateOfOpening"]).Date;
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"DateOfOpening\" parameter is missing!");
            }
            if (!parameters.IsNull("dateOfClosing"))
            {
                obj.DateOfClosing = Convert.ToDateTime(parameters["dateOfClosing"]).Date;
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"dateOfClosing\" parameter is missing!");
            }
            if (!parameters.IsNull("live"))
            {
                obj.Live = Convert.ToBoolean(parameters["live"]);
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"live\" parameter is missing!");
            }
        }

        public override Dictionary<string, object> ExportToDictionary()
        {
            var dict = new Dictionary<string, object>();
            if (obj != null)
            {
                dict = base.ExportToDictionary();
                if (obj.AccountNumber != null)
                {
                    dict.Add("accountNumber", obj.AccountNumber);
                }

                //Nem lehetnek "null"-ok
                dict.Add("accountName", Technical.Convert.AccountTypeToString(obj.AccountName));
                dict.Add("accountType", Technical.Convert.AccountTypeToString(obj.AccountType));
                dict.Add("currency", Technical.Convert.CurrencyTypeToString(obj.Currency));
                dict.Add("sectorType", Technical.Convert.SectorTypeToString(obj.SectorType));
                dict.Add("balance", obj.Balance);

                if (obj.DateOfOpening != null)
                {
                    dict.Add("dateOfOpening", obj.DateOfOpening);
                }
                if (obj.DateOfClosing != null)
                {
                    dict.Add("dateOfClosing", obj.DateOfClosing);
                }

                //Nem lehet "null"
                dict.Add("live", obj.Live);
            }
            return dict;
        }

        public override DataRow ExportToDataRow()
        {
            var table = new DataTable(); //csak átmenetileg kell nekünk, így lehet csak megoldani a konstruktort a DataRow-nak
            DataRow row = table.NewRow();
            if (obj != null)
            {
                DataRow temp = base.ExportToDataRow();  //muszály így megoldani, hogy a DataRow működjön rendesen
                table.Columns.Add("id");
                row["id"] = temp["id"];
                if (obj.AccountNumber != null)
                {
                    table.Columns.Add("accountNumber");
                    row["accountNumber"] = obj.AccountNumber;
                }

                //Nem lehetnek "null"-ok
                table.Columns.Add("accountName");
                row["accountName"] = Technical.Convert.AccountTypeToString(obj.AccountName);
                table.Columns.Add("accountType");
                row["accountType"] = Technical.Convert.AccountTypeToString(obj.AccountType);
                table.Columns.Add("currency");
                row["currency"] = Technical.Convert.CurrencyTypeToString(obj.Currency);
                table.Columns.Add("sectorType");
                row["sectorType"] = Technical.Convert.SectorTypeToString(obj.SectorType);
                table.Columns.Add("balance");
                row["balance"] = obj.Balance;

                if (obj.DateOfOpening != null)
                {
                    table.Columns.Add("dateOfOpening");
                    row["dateOfOpening"] = obj.DateOfOpening;
                }
                if (obj.DateOfClosing != null)
                {
                    table.Columns.Add("dateOfClosing");
                    row["dateOfClosing"] = obj.DateOfClosing;
                }

                //Nem lehet "null"
                table.Columns.Add("live");
                row["live"] = obj.Live;
            }
            return row;
        }
    }

    public class ExtendedRole : ExtendedBase
    {
        protected new Role obj; //Újradefíniáljuk az obj-t

        public override void ImportFromDictionary(Dictionary<string, object> parameters)
        {
            obj = new Role();
            if (parameters.ContainsKey("clientID"))
            {
                obj.ClientID = parameters["clientID"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"clientID\" parameter is missing!");
            }
            if (parameters.ContainsKey("type"))
            {
                obj.Type = Technical.Convert.StringToRoleType(parameters["type"].ToString());
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"type\" parameter is missing!");
            }
            if (parameters.ContainsKey("live"))
            {
                obj.Live = Convert.ToBoolean(parameters["live"]);
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"live\" parameter is missing!");
            }
        }

        public override void ImportFromDataRow(DataRow parameters)
        {
            obj = new Role();
            if (!parameters.IsNull("clientID"))
            {
                obj.ClientID = parameters["clientID"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"clientID\" parameter is missing!");
            }
            if (!parameters.IsNull("type"))
            {
                obj.Type = Technical.Convert.StringToRoleType(parameters["type"].ToString());
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"type\" parameter is missing!");
            }
            if (!parameters.IsNull("live"))
            {
                obj.Live = Convert.ToBoolean(parameters["live"]);
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"live\" parameter is missing!");
            }
        }

        public override Dictionary<string, object> ExportToDictionary()
        {
            var dict = new Dictionary<string, object>();
            if (obj != null)
            {
                if (obj.ClientID != null)
                {
                    dict.Add("clientID", obj.ClientID);
                }

                //Nem kaphatnak "null" értéket
                dict.Add("type", Technical.Convert.RoleTypeToString(obj.Type));
                dict.Add("live", obj.Live);
            }
            return dict;
        }

        public override DataRow ExportToDataRow()
        {
            var table = new DataTable(); //csak átmenetileg kell nekünk, így lehet csak megoldani a konstruktort a DataRow-nak
            DataRow row = table.NewRow();
            if (obj != null)
            {
                if (obj.ClientID != null)
                {
                    table.Columns.Add("clientID");
                    row["clientID"] = obj.ClientID;
                }
                table.Columns.Add("type");
                row["type"] = Technical.Convert.RoleTypeToString(obj.Type);
                table.Columns.Add("live");
                row["live"] = obj.Live;
            }
            return row;
        }
    }

    public class ExtendedTransaction : ExtendedBase2
    {
        protected new Transaction obj; //Újradefíniáljuk az obj-t

        public override void ImportFromDictionary(Dictionary<string, object> parameters)
        {
            if (obj != null)
            {
                base.ImportFromDictionary(parameters);
                if (parameters.ContainsKey("amount"))
                {
                    obj.Amount = int.Parse(parameters["amount"].ToString());
                }
                else
                {
                    throw new BusinessLayerExceptions.MissingParameterException("The \"amount\" parameter is missing!");
                }
                if (parameters.ContainsKey("currency"))
                {
                    obj.Currency = Technical.Convert.StringToCurrencyType(parameters["currency"].ToString());
                }
                else
                {
                    throw new BusinessLayerExceptions.MissingParameterException("The \"currency\" parameter is missing!");
                }
                if (parameters.ContainsKey("date"))
                {
                    obj.Date = Convert.ToDateTime(parameters["date"]).Date;
                }
                else
                {
                    throw new BusinessLayerExceptions.MissingParameterException("The \"date\" parameter is missing!");
                }
                if (parameters.ContainsKey("time"))
                {
                    obj.Time = Convert.ToDateTime(parameters["time"]);
                }
                else
                {
                    throw new BusinessLayerExceptions.MissingParameterException("The \"time\" parameter is missing!");
                }
                if (parameters.ContainsKey("message"))
                {
                    obj.Message = parameters["message"].ToString();
                }
                else
                {
                    throw new BusinessLayerExceptions.MissingParameterException("The \"message\" parameter is missing!");
                }
            }
        }

        public override void ImportFromDataRow(DataRow parameters)
        {
            if (obj != null)
            {
                base.ImportFromDataRow(parameters);
                if (!parameters.IsNull("amount"))
                {
                    obj.Amount = int.Parse(parameters["amount"].ToString());
                }
                else
                {
                    throw new BusinessLayerExceptions.MissingParameterException("The \"amount\" parameter is missing!");
                }
                if (!parameters.IsNull("currency"))
                {
                    obj.Currency = Technical.Convert.StringToCurrencyType(parameters["currency"].ToString());
                }
                else
                {
                    throw new BusinessLayerExceptions.MissingParameterException("The \"currency\" parameter is missing!");
                }
                if (!parameters.IsNull("date"))
                {
                    obj.Date = Convert.ToDateTime(parameters["date"]).Date;
                }
                else
                {
                    throw new BusinessLayerExceptions.MissingParameterException("The \"date\" parameter is missing!");
                }
                if (!parameters.IsNull("time"))
                {
                    obj.Time = Convert.ToDateTime(parameters["time"]);
                }
                else
                {
                    throw new BusinessLayerExceptions.MissingParameterException("The \"time\" parameter is missing!");
                }
                if (!parameters.IsNull("message"))
                {
                    obj.Message = parameters["message"].ToString();
                }
                else
                {
                    throw new BusinessLayerExceptions.MissingParameterException("The \"message\" parameter is missing!");
                }
            }
        }

        public override Dictionary<string, object> ExportToDictionary()
        {
            var dict = new Dictionary<string, object>();
            if (obj != null)
            {
                dict = base.ExportToDictionary();

                //Nem lehet "null" értékük
                dict.Add("amount", obj.Amount);
                dict.Add("currency", Technical.Convert.CurrencyTypeToString(obj.Currency));

                if (obj.Date != null)
                {
                    dict.Add("date", obj.Date);
                }
                if (obj.Time != null)
                {
                    dict.Add("time", obj.Time);
                }
                if (obj.Message != null)
                {
                    dict.Add("message", obj.Message);
                }
            }
            return dict;
        }

        public override DataRow ExportToDataRow()
        {
            var table = new DataTable(); //csak átmenetileg kell nekünk, így lehet csak megoldani a konstruktort a DataRow-nak
            DataRow row = table.NewRow();
            if (obj != null)
            {
                DataRow temp = base.ExportToDataRow();  //muszály így megoldani, hogy a DataRow működjön rendesen
                table.Columns.Add("id");
                row["id"] = temp["id"];

                //Nem lehetnek "null"-ok
                table.Columns.Add("amount");
                row["amount"] = obj.Amount;
                table.Columns.Add("currency");
                row["currency"] = Technical.Convert.CurrencyTypeToString(obj.Currency);

                if (obj.Date != null)
                {
                    table.Columns.Add("date");
                    row["date"] = obj.Date;
                }
                if (obj.Time != null)
                {
                    table.Columns.Add("time");
                    row["time"] = obj.Time;
                }
                if (obj.Message != null)
                {
                    table.Columns.Add("message");
                    row["message"] = obj.Message;
                }
            }
            return row;
        }
    }

    public class ExtendedTransfer : ExtendedTransaction
    {
        protected new Transfer obj; //Újradefíniáljuk az obj-t

        public override void ImportFromDictionary(Dictionary<string, object> parameters)
        {
            obj = new Transfer();
            base.ImportFromDictionary(parameters);
            if (parameters.ContainsKey("sourceAccountNumber"))
            {
                obj.SourceAccountNumber = parameters["sourceAccountNumber"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"sourceAccountNumber\" parameter is missing!");
            }
            if (parameters.ContainsKey("destinationAccountNumber"))
            {
                obj.DestinationAccountNumber = parameters["destinationAccountNumber"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"destinationAccountNumber\" parameter is missing!");
            }
            if (parameters.ContainsKey("sourceName"))
            {
                obj.SourceName = parameters["sourceName"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"sourceName\" parameter is missing!");
            }
            if (parameters.ContainsKey("destinationName"))
            {
                obj.DestinationName = parameters["destinationName"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"destinationName\" parameter is missing!");
            }
        }

        public override void ImportFromDataRow(DataRow parameters)
        {
            obj = new Transfer();
            base.ImportFromDataRow(parameters);
            if (!parameters.IsNull("sourceAccountNumber"))
            {
                obj.SourceAccountNumber = parameters["sourceAccountNumber"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"sourceAccountNumber\" parameter is missing!");
            }
            if (!parameters.IsNull("destinationAccountNumber"))
            {
                obj.DestinationAccountNumber = parameters["destinationAccountNumber"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"destinationAccountNumber\" parameter is missing!");
            }
            if (!parameters.IsNull("sourceName"))
            {
                obj.SourceName = parameters["sourceName"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"sourceName\" parameter is missing!");
            }
            if (!parameters.IsNull("destinationName"))
            {
                obj.DestinationName = parameters["destinationName"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"destinationName\" parameter is missing!");
            }
        }

        public override Dictionary<string, object> ExportToDictionary()
        {
            var dict = new Dictionary<string, object>();
            if (obj != null)
            {
                dict = base.ExportToDictionary();
                if (obj.SourceAccountNumber != null)
                {
                    dict.Add("sourceAccountNumber", obj.SourceAccountNumber);
                }
                if (obj.DestinationAccountNumber != null)
                {
                    dict.Add("destinationAccountNumber", obj.DestinationAccountNumber);
                }
                if (obj.SourceName != null)
                {
                    dict.Add("sourceName", obj.SourceName);
                }
                if (obj.DestinationName != null)
                {
                    dict.Add("destinationName", obj.DestinationName);
                }
            }
            return dict;
        }

        public override DataRow ExportToDataRow()
        {
            var table = new DataTable(); //csak átmenetileg kell nekünk, így lehet csak megoldani a konstruktort a DataRow-nak
            DataRow row = table.NewRow();
            if (obj != null)
            {
                DataRow temp = base.ExportToDataRow();  //muszály így megoldani, hogy a DataRow működjön rendesen
                table.Columns.Add("id");
                row["id"] = temp["id"];
                row["amount"] = temp["amount"];
                row["currency"] = temp["currency"];
                row["date"] = temp["date"];
                row["time"] = temp["time"];
                row["message"] = temp["message"];

                if (obj.SourceAccountNumber != null)
                {
                    table.Columns.Add("sourceAccountNumber");
                    row["sourceAccountNumber"] = obj.SourceAccountNumber;
                }
                if (obj.DestinationAccountNumber != null)
                {
                    table.Columns.Add("destinationAccountNumber");
                    row["destinationAccountNumber"] = obj.DestinationAccountNumber;
                }
                if (obj.SourceName != null)
                {
                    table.Columns.Add("sourceName");
                    row["sourceName"] = obj.SourceName;
                }
                if (obj.DestinationName != null)
                {
                    table.Columns.Add("destinationName");
                    row["destinationName"] = obj.DestinationName;
                }
            }
            return row;
        }
    }

    public class ExtendedCashTransaction : ExtendedTransaction
    {
        protected new CashTransaction obj; //Újradefíniáljuk az obj-t

        public override void ImportFromDictionary(Dictionary<string, object> parameters)
        {
            obj = new CashTransaction();
            base.ImportFromDictionary(parameters);
            if (parameters.ContainsKey("type"))
            {
                obj.Type = Technical.Convert.StringToCashTransactionType(parameters["type"].ToString());
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"type\" parameter is missing!");
            }
            if (parameters.ContainsKey("accountNumber"))
            {
                obj.AccountNumber = parameters["accountNumber"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"accountNumber\" parameter is missing!");
            }
        }

        public override void ImportFromDataRow(DataRow parameters)
        {
            obj = new CashTransaction();
            base.ImportFromDataRow(parameters);
            if (!parameters.IsNull("type"))
            {
                obj.Type = Technical.Convert.StringToCashTransactionType(parameters["type"].ToString());
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"type\" parameter is missing!");
            }
            if (!parameters.IsNull("accountNumber"))
            {
                obj.AccountNumber = parameters["accountNumber"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"accountNumber\" parameter is missing!");
            }
        }

        public override Dictionary<string, object> ExportToDictionary()
        {
            var dict = new Dictionary<string, object>();
            if (obj != null)
            {
                dict = base.ExportToDictionary();

                //Nem lehet "null"
                dict.Add("type", Technical.Convert.CashTransactionTypeToString(obj.Type));

                if (obj.AccountNumber != null)
                {
                    dict.Add("accountNumber", obj.AccountNumber);
                }
            }
            return dict;
        }

        public override DataRow ExportToDataRow()
        {
            var table = new DataTable(); //csak átmenetileg kell nekünk, így lehet csak megoldani a konstruktort a DataRow-nak
            DataRow row = table.NewRow();
            if (obj != null)
            {
                DataRow temp = base.ExportToDataRow();  //muszály így megoldani, hogy a DataRow működjön rendesen
                table.Columns.Add("id");
                row["id"] = temp["id"];
                row["amount"] = temp["amount"];
                row["currency"] = temp["currency"];
                row["date"] = temp["date"];
                row["time"] = temp["time"];
                row["message"] = temp["message"];

                //Nem lehetnek "null"-ok
                table.Columns.Add("transactionType");
                row["transactionType"] = Technical.Convert.CashTransactionTypeToString(obj.Type);

                if (obj.AccountNumber != null)
                {
                    table.Columns.Add("accountNumber");
                    row["accountNumber"] = obj.AccountNumber;
                }
            }
            return row;
        }
    }

    public class ExtendedCard : ExtendedBase2
    {
        protected new Card obj; //Újradefíniáljuk az obj-t

        public override void ImportFromDictionary(Dictionary<string, object> parameters)
        {
            obj = new Card();
            base.ImportFromDictionary(parameters);
            if (parameters.ContainsKey("providerType"))
            {
                obj.ProviderType = Technical.Convert.StringToCardProviderType(parameters["providerType"].ToString());
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"providerType\" parameter is missing!");
            }
            if (parameters.ContainsKey("cardNumber"))
            {
                obj.CardNumber = parameters["cardNumber"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"cardNumber\" parameter is missing!");
            }
            if (parameters.ContainsKey("currency"))
            {
                obj.Currency = Technical.Convert.StringToCurrencyType(parameters["currency"].ToString());
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"currency\" parameter is missing!");
            }
            if (parameters.ContainsKey("type"))
            {
                obj.Type = Technical.Convert.StringToCardType(parameters["type"].ToString());
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"type\" parameter is missing!");
            }
            if (parameters.ContainsKey("dateOfRequest"))
            {
                obj.DateOfRequest = Convert.ToDateTime(parameters["dateOfRequest"]).Date;
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"dateOfRequest\" parameter is missing!");
            }
            if (parameters.ContainsKey("dateOfExpiration"))
            {
                obj.DateOfExpiration = Convert.ToDateTime(parameters["dateOfExpiration"]).Date;
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"dateOfExpiration\" parameter is missing!");
            }
            if (parameters.ContainsKey("live"))
            {
                obj.Live = Convert.ToBoolean(parameters["live"]);
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"live\" parameter is missing!");
            }
        }

        public override void ImportFromDataRow(DataRow parameters)
        {
            obj = new Card();
            base.ImportFromDataRow(parameters);
            if (!parameters.IsNull("providerType"))
            {
                obj.ProviderType = Technical.Convert.StringToCardProviderType(parameters["providerType"].ToString());
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"providerType\" parameter is missing!");
            }
            if (!parameters.IsNull("cardNumber"))
            {
                obj.CardNumber = parameters["cardNumber"].ToString();
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"cardNumber\" parameter is missing!");
            }
            if (!parameters.IsNull("currency"))
            {
                obj.Currency = Technical.Convert.StringToCurrencyType(parameters["currency"].ToString());
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"currency\" parameter is missing!");
            }
            if (!parameters.IsNull("type"))
            {
                obj.Type = Technical.Convert.StringToCardType(parameters["type"].ToString());
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"type\" parameter is missing!");
            }
            if (!parameters.IsNull("dateOfRequest"))
            {
                obj.DateOfRequest = Convert.ToDateTime(parameters["dateOfRequest"]).Date;
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"dateOfRequest\" parameter is missing!");
            }
            if (!parameters.IsNull("dateOfExpiration"))
            {
                obj.DateOfExpiration = Convert.ToDateTime(parameters["dateOfExpiration"]).Date;
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"dateOfExpiration\" parameter is missing!");
            }
            if (!parameters.IsNull("live"))
            {
                obj.Live = Convert.ToBoolean(parameters["live"]);
            }
            else
            {
                throw new BusinessLayerExceptions.MissingParameterException("The \"live\" parameter is missing!");
            }
        }

        public override Dictionary<string, object> ExportToDictionary()
        {
            var dict = new Dictionary<string, object>();
            if (obj != null)
            {
                dict = base.ExportToDictionary();

                //Nem lehet "null"
                dict.Add("providerType", Technical.Convert.CardProviderTypeToString(obj.ProviderType));

                if (obj.CardNumber != null)
                {
                    dict.Add("cardNumber", obj.CardNumber);
                }

                //Nem lehetnek "null"-ok
                dict.Add("currency", Technical.Convert.CurrencyTypeToString(obj.Currency));
                dict.Add("type", Technical.Convert.CardTypeToString(obj.Type));

                if (obj.DateOfRequest != null)
                {
                    dict.Add("dateOfRequest", obj.DateOfRequest);
                }
                if (obj.DateOfExpiration != null)
                {
                    dict.Add("dateOfExpiration", obj.DateOfExpiration);
                }

                //Nem lehet "null"
                dict.Add("live", Convert.ToBoolean(obj.Live));
            }
            return dict;
        }

        public override DataRow ExportToDataRow()
        {
            var table = new DataTable(); //csak átmenetileg kell nekünk, így lehet csak megoldani a konstruktort a DataRow-nak
            DataRow row = table.NewRow();
            if (obj != null)
            {
                DataRow temp = base.ExportToDataRow();  //muszály így megoldani, hogy a DataRow működjön rendesen
                table.Columns.Add("id");
                row["id"] = temp["id"];

                //Nem lehet "null"
                table.Columns.Add("providerType");
                row["providerType"] = Technical.Convert.CardProviderTypeToString(obj.ProviderType);

                if (obj.CardNumber != null)
                {
                    table.Columns.Add("cardNumber");
                    row["cardNumber"] = obj.CardNumber;
                }

                //Nem lehetnek "null"-ok
                table.Columns.Add("currency");
                row["currency"] = Technical.Convert.CurrencyTypeToString(obj.Currency);
                table.Columns.Add("type");
                row["type"] = Technical.Convert.CardTypeToString(obj.Type);

                if (obj.DateOfRequest != null)
                {
                    table.Columns.Add("dateOfRequest");
                    row["dateOfRequest"] = obj.DateOfRequest;
                }
                if (obj.DateOfExpiration != null)
                {
                    table.Columns.Add("dateOfExpiration");
                    row["dateOfExpiration"] = obj.DateOfExpiration;
                }

                //Nem lehet "null"
                table.Columns.Add("live");
                row["live"] = Convert.ToBoolean(obj.Live);
            }
            return row;
        }
    }





    //Kiterjesztett List osztályok

    public abstract class ExtendedListOfElements
    {
        private List<ExtendedBase> list = new List<ExtendedBase>();

        public virtual void Add(ExtendedBase item)
        {
            list.Add(item);
        }

        public virtual void Remove(ExtendedBase item)
        {
            list.Remove(item);
        }

        public virtual void Clear()
        {
            list.Clear();
        }

        public virtual List<ExtendedBase> GetList()
        {
            return list;
        }

        public abstract void ImportFromDataTable(DataTable table);

        public abstract DataTable ExportToDataTable();

    }

    public abstract class ExtendedBase2s : ExtendedListOfElements
    {
        public override void Add(ExtendedBase item)
        {
            if (item is ExtendedBase2)
            {
                base.Add(item);
            }
            else
            {
                throw new BusinessLayerExceptions.InvalidTypeException("Parameter type must be ExtendedBase2!");
            }
        }

        public override void Remove(ExtendedBase item)
        {
            if (item is ExtendedBase2)
            {
                base.Remove(item);
            }
            else
            {
                throw new BusinessLayerExceptions.InvalidTypeException("Parameter type must be ExtendedBase2!");
            }
        }

        public override void Clear()
        {
            base.Clear();
        }

        public override List<ExtendedBase> GetList()
        {
            return base.GetList();
        }

        public override void ImportFromDataTable(DataTable table)
        {
            base.Clear();
            ExtendedBase2 eBase2;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i] != null)
                {
                    eBase2 = new ExtendedBase2();
                    eBase2.ImportFromDataRow(table.Rows[i]);
                    base.Add(eBase2);
                }
            }
        }

        public override DataTable ExportToDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("id");
            DataRow row;
            List<ExtendedBase> list = base.GetList();
            foreach (ExtendedBase temp in list)
            {
                if (temp != null)
                {
                    row = table.NewRow();
                    row = ((ExtendedBase2)temp).ExportToDataRow();
                    table.Rows.Add(row);
                }
            }
            return table;
        }
    }

    public class ExtendedClients : ExtendedBase2s
    {
        public override void Add(ExtendedBase item)
        {
            if (item is ExtendedClient)
            {
                base.Add(item);
            }
            else
            {
                throw new BusinessLayerExceptions.InvalidTypeException("Parameter type must be ExtendedClient!");
            }
        }

        public override void Remove(ExtendedBase item)
        {
            if (item is ExtendedClient)
            {
                base.Remove(item);
            }
            else
            {
                throw new BusinessLayerExceptions.InvalidTypeException("Parameter type must be ExtendedClient!");
            }
        }

        public override void ImportFromDataTable(DataTable table)
        {
            base.Clear();
            ExtendedClient eClient;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i] != null)
                {
                    eClient = new ExtendedClient();
                    eClient.ImportFromDataRow(table.Rows[i]);
                    base.Add(eClient);
                }
            }
        }

        public override DataTable ExportToDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("id");
            table.Columns.Add("firstName");
            table.Columns.Add("lastName");
            table.Columns.Add("dateOfBirth");
            table.Columns.Add("placeOfBirth");
            table.Columns.Add("nameOfMother");
            table.Columns.Add("address");
            table.Columns.Add("phone");
            table.Columns.Add("email");
            table.Columns.Add("live");
            DataRow row;
            List<ExtendedBase> list = base.GetList();
            foreach (ExtendedBase temp in list)
            {
                if (temp != null)
                {
                    row = table.NewRow();
                    row = ((ExtendedClient)temp).ExportToDataRow();
                    table.Rows.Add(row);
                }
            }
            return table;
        }
    }

    public class ExtendedAccounts : ExtendedBase2s
    {
        public override void Add(ExtendedBase item)
        {
            if (item is ExtendedAccount)
            {
                base.Add(item);
            }
            else
            {
                throw new BusinessLayerExceptions.InvalidTypeException("Parameter type must be ExtendedAccount!");
            }
        }

        public override void Remove(ExtendedBase item)
        {
            if (item is ExtendedAccount)
            {
                base.Remove(item);
            }
            else
            {
                throw new BusinessLayerExceptions.InvalidTypeException("Parameter type must be ExtendedAccount!");
            }
        }

        public override void ImportFromDataTable(DataTable table)
        {
            base.Clear();
            ExtendedAccount eAccount;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i] != null)
                {
                    eAccount = new ExtendedAccount();
                    eAccount.ImportFromDataRow(table.Rows[i]);
                    base.Add(eAccount);
                }
            }
        }

        public override DataTable ExportToDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("id");
            table.Columns.Add("accountNumber");
            table.Columns.Add("accountName");
            table.Columns.Add("accountType");
            table.Columns.Add("currency");
            table.Columns.Add("sectorType");
            table.Columns.Add("balance");
            table.Columns.Add("dateOfOpening");
            table.Columns.Add("dateOfClosing");
            table.Columns.Add("live");
            DataRow row;
            List<ExtendedBase> list = base.GetList();
            foreach (ExtendedBase temp in list)
            {
                if (temp != null)
                {
                    row = table.NewRow();
                    row = ((ExtendedAccount)temp).ExportToDataRow();
                    table.Rows.Add(row);
                }
            }
            return table;
        }
    }

    public class ExtendedRoles : ExtendedListOfElements
    {
        public override void Add(ExtendedBase item)
        {
            if (item is ExtendedRole)
            {
                base.Add(item);
            }
            else
            {
                throw new BusinessLayerExceptions.InvalidTypeException("Parameter type must be ExtendedRole!");
            }
        }

        public override void Remove(ExtendedBase item)
        {
            if (item is ExtendedRole)
            {
                base.Remove(item);
            }
            else
            {
                throw new BusinessLayerExceptions.InvalidTypeException("Parameter type must be ExtendedRole!");
            }
        }

        public override void ImportFromDataTable(DataTable table)
        {
            base.Clear();
            ExtendedRole eRole;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i] != null)
                {
                    eRole = new ExtendedRole();
                    eRole.ImportFromDataRow(table.Rows[i]);
                    base.Add(eRole);
                }
            }
        }

        public override DataTable ExportToDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("clientID");
            table.Columns.Add("type");
            table.Columns.Add("live");
            DataRow row;
            List<ExtendedBase> list = base.GetList();
            foreach (ExtendedBase temp in list)
            {
                if (temp != null)
                {
                    row = table.NewRow();
                    row = ((ExtendedRole)temp).ExportToDataRow();
                    table.Rows.Add(row);
                }
            }
            return table;
        }
    }

    public class ExtendedTransactions : ExtendedBase2s
    {
        public override void Add(ExtendedBase item)
        {
            if (item is ExtendedTransaction)
            {
                base.Add(item);
            }
            else
            {
                throw new BusinessLayerExceptions.InvalidTypeException("Parameter type must be ExtendedTransaction!");
            }
        }

        public override void Remove(ExtendedBase item)
        {
            if (item is ExtendedTransaction)
            {
                base.Remove(item);
            }
            else
            {
                throw new BusinessLayerExceptions.InvalidTypeException("Parameter type must be ExtendedTransaction!");
            }
        }

        public override void ImportFromDataTable(DataTable table)
        {
            base.Clear();
            ExtendedTransaction eTransaction;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i] != null)
                {
                    eTransaction = new ExtendedTransaction();
                    eTransaction.ImportFromDataRow(table.Rows[i]);
                    base.Add(eTransaction);
                }
            }
        }

        public override DataTable ExportToDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("id");
            table.Columns.Add("amount");
            table.Columns.Add("currency");
            table.Columns.Add("date");
            table.Columns.Add("time");
            table.Columns.Add("message");
            DataRow row;
            List<ExtendedBase> list = base.GetList();
            foreach (ExtendedBase temp in list)
            {
                if (temp != null)
                {
                    row = table.NewRow();
                    row = ((ExtendedTransaction)temp).ExportToDataRow();
                    table.Rows.Add(row);
                }
            }
            return table;
        }
    }

    public class ExtendedTransfers : ExtendedBase2s
    {
        public override void Add(ExtendedBase item)
        {
            if (item is ExtendedTransfer)
            {
                base.Add(item);
            }
            else
            {
                throw new BusinessLayerExceptions.InvalidTypeException("Parameter type must be ExtendedTransfer!");
            }
        }

        public override void Remove(ExtendedBase item)
        {
            if (item is ExtendedTransfer)
            {
                base.Remove(item);
            }
            else
            {
                throw new BusinessLayerExceptions.InvalidTypeException("Parameter type must be ExtendedTransfer!");
            }
        }

        public override void ImportFromDataTable(DataTable table)
        {
            base.Clear();
            ExtendedTransfer eTransfer;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i] != null)
                {
                    eTransfer = new ExtendedTransfer();
                    eTransfer.ImportFromDataRow(table.Rows[i]);
                    base.Add(eTransfer);
                }
            }
        }

        public override DataTable ExportToDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("id");
            table.Columns.Add("sourceAccountNumber");
            table.Columns.Add("destinationAccountNumber");
            table.Columns.Add("sourceName");
            table.Columns.Add("destinationName");
            table.Columns.Add("amount");
            table.Columns.Add("currency");
            table.Columns.Add("date");
            table.Columns.Add("time");
            table.Columns.Add("message");
            DataRow row;
            List<ExtendedBase> list = base.GetList();
            foreach (ExtendedBase temp in list)
            {
                if (temp != null)
                {
                    row = table.NewRow();
                    row = ((ExtendedTransfer)temp).ExportToDataRow();
                    table.Rows.Add(row);
                }
            }
            return table;
        }
    }

    public class ExtendedCashTransactions : ExtendedBase2s
    {
        public override void Add(ExtendedBase item)
        {
            if (item is ExtendedCashTransaction)
            {
                base.Add(item);
            }
            else
            {
                throw new BusinessLayerExceptions.InvalidTypeException("Parameter type must be ExtendedCashTransaction!");
            }
        }

        public override void Remove(ExtendedBase item)
        {
            if (item is ExtendedCashTransaction)
            {
                base.Remove(item);
            }
            else
            {
                throw new BusinessLayerExceptions.InvalidTypeException("Parameter type must be ExtendedCashTransaction!");
            }
        }

        public override void ImportFromDataTable(DataTable table)
        {
            base.Clear();
            ExtendedCashTransaction eCashTransaction;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i] != null)
                {
                    eCashTransaction = new ExtendedCashTransaction();
                    eCashTransaction.ImportFromDataRow(table.Rows[i]);
                    base.Add(eCashTransaction);
                }
            }
        }

        public override DataTable ExportToDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("id");
            table.Columns.Add("type");
            table.Columns.Add("accountNumber");
            table.Columns.Add("amount");
            table.Columns.Add("currency");
            table.Columns.Add("date");
            table.Columns.Add("time");
            table.Columns.Add("message");

            DataRow row;
            List<ExtendedBase> list = base.GetList();
            foreach (ExtendedBase temp in list)
            {
                if (temp != null)
                {
                    row = table.NewRow();
                    row = ((ExtendedCashTransaction)temp).ExportToDataRow();
                    table.Rows.Add(row);
                }
            }
            return table;
        }
    }

    public class ExtendedCards : ExtendedBase2s
    {
        public override void Add(ExtendedBase item)
        {
            if (item is ExtendedCard)
            {
                base.Add(item);
            }
            else
            {
                throw new BusinessLayerExceptions.InvalidTypeException("Parameter type must be ExtendedCard!");
            }
        }

        public override void Remove(ExtendedBase item)
        {
            if (item is ExtendedCard)
            {
                base.Remove(item);
            }
            else
            {
                throw new BusinessLayerExceptions.InvalidTypeException("Parameter type must be ExtendedCard!");
            }
        }

        public override void ImportFromDataTable(DataTable table)
        {
            base.Clear();
            ExtendedCard eCard;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i] != null)
                {
                    eCard = new ExtendedCard();
                    eCard.ImportFromDataRow(table.Rows[i]);
                    base.Add(eCard);
                }
            }
        }

        public override DataTable ExportToDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("id");
            table.Columns.Add("providerType");
            table.Columns.Add("cardNumber");
            table.Columns.Add("currency");
            table.Columns.Add("type");
            table.Columns.Add("dateOfRequest");
            table.Columns.Add("dateOfExpiration");
            table.Columns.Add("live");
            DataRow row;
            List<ExtendedBase> list = base.GetList();
            foreach (ExtendedBase temp in list)
            {
                if (temp != null)
                {
                    row = table.NewRow();
                    row = ((ExtendedCard)temp).ExportToDataRow();
                    table.Rows.Add(row);
                }
            }
            return table;
        }
    }


    //Komplex osztályok

    public class ComplexClient : Client
    {
        private Accounts accounts;
        private Cards cards;

        public ComplexClient() : base()
        {
            //
        }

        public ComplexClient(string id) : base()
        {
            ID = id;
        }

        public ComplexClient(string firstName, string lastName, DateTime dateOfBirth, string placeOfBirth, string nameOfMother, string address, string phone, string email, bool live)
            : base(firstName, lastName, dateOfBirth, placeOfBirth, nameOfMother, address, phone, email, live)
        {
            //
        }

        public ComplexClient(string id, string firstName, string lastName, DateTime dateOfBirth, string placeOfBirth, string nameOfMother, string address, string phone, string email, bool live)
            : base(id, firstName, lastName, dateOfBirth, placeOfBirth, nameOfMother, address, phone, email, live)
        {
            //
        }

        public void AddAccount(Base item)
        {
            accounts.Add(item);
        }

        public void RemoveAccount(Base item)
        {
            accounts.Remove(item);
        }

        public void AddCard(Base item)
        {
            cards.Add(item);
        }

        public void RemoveCard(Base item)
        {
            cards.Remove(item);
        }
    }

    public class ComplexAccount : Account
    {
        private Roles roles;
        private Cards cards;
        private Transactions transactions;

        public ComplexAccount() : base()
        {
            //
        }

        public ComplexAccount(string id) : base()
        {
            ID = id;
        }

        public ComplexAccount(string accountNumber, Technical.AccountType accountName, Technical.AccountType accountType, Technical.CurrencyType currency, Technical.SectorType sectorType, int balance, DateTime dateOfOpening, DateTime dateOfClosing, bool live)
            : base(accountNumber, accountName, accountType, currency, sectorType, balance, dateOfOpening, dateOfClosing, live)
        {
            //
        }

        public ComplexAccount(string id, string accountNumber, Technical.AccountType accountName, Technical.AccountType accountType, Technical.CurrencyType currency, Technical.SectorType sectorType, int balance, DateTime dateOfOpening, DateTime dateOfClosing, bool live)
            : base(id, accountNumber, accountName, accountType, currency, sectorType, balance, dateOfOpening, dateOfClosing, live)
        {
            //
        }

        public void AddRole(Base item)
        {
            roles.Add(item);
        }

        public void RemoveRole(Base item)
        {
            roles.Remove(item);
        }

        public void AddCard(Base item)
        {
            cards.Add(item);
        }

        public void RemoveCard(Base item)
        {
            cards.Remove(item);
        }

        public void AddTransaction(Base item)
        {
            transactions.Add(item);
        }

        public void RemoveTransaction(Base item)
        {
            transactions.Remove(item);
        }
    }

    public class UserInterface
    {
        //

        public void ListClientDatas()
        {

        }

        public void ListClientAccounts()
        {

        }

        public void ListClientCards()
        {

        }

        public void ListAccountDatas()
        {

        }

        public void ListAccountRoles()
        {

        }

        public void ListAccountCards()
        {

        }

        public void ListAccountTransactions()
        {

        }


        public void TestMethod()
        {
            ComplexClient client1 = new ComplexClient();
            ComplexAccount account1 = new ComplexAccount();
            Card card1 = new Card();
            Transfer transaction1 = new Transfer();
            Role role1 = new Role();

            client1.FirstName = "Peter";
            account1.Balance = 500000;
            card1.CardNumber = "1111 2222 3333 4444";
            transaction1.Amount = 100000;
            role1.Type = Technical.RoleType.Owner;

            //Nagyon fontos!!!! Mivel a ComplexAccount az Account-tól származik, így ide
            //ComplexAccount-ot is hozzáadhatunk!
            client1.AddAccount(account1);
            client1.AddCard(card1);

            account1.AddRole(role1);
            account1.AddCard(card1);
            account1.AddTransaction(transaction1);
        }
    }



    //Kiterjesztett komplex osztályok

    public class ComplexExtendedClient
    {
        private ExtendedClient client;
        private ExtendedAccounts accounts;
        private ExtendedCards cards;

        public ComplexExtendedClient()
        {
            client = new ExtendedClient();
            accounts = new ExtendedAccounts();
            cards = new ExtendedCards();
        }

        public ComplexExtendedClient(DataSet ds)
        {
            client = new ExtendedClient();
            accounts = new ExtendedAccounts();
            cards = new ExtendedCards();

            if (ds.Tables.Contains("Client"))
            {
                client.ImportFromDataRow(ds.Tables["Client"].Rows[0]);
            }

            if (ds.Tables.Contains("Account"))
            {
                accounts.ImportFromDataTable(ds.Tables["Account"]);
            }
            else if (ds.Tables.Contains("Accounts"))
            {
                accounts.ImportFromDataTable(ds.Tables["Accounts"]);
            }

            if (ds.Tables.Contains("Card"))
            {
                cards.ImportFromDataTable(ds.Tables["Card"]);
            }
            else if (ds.Tables.Contains("Cards"))
            {
                cards.ImportFromDataTable(ds.Tables["Cards"]);
            }
        }

        public void SetClient(ExtendedClient eClient)
        {
            client = eClient;
        }

        public void ClearClient()
        {
            client = new ExtendedClient();
        }

        public ExtendedClient ListClient()
        {
            return client;
        }

        public void AddAccount(ExtendedAccount eAccount)
        {
            accounts.Add(eAccount);
        }

        public void AddAccounts(ExtendedAccounts eAccounts)
        {
            accounts = eAccounts;
        }

        public void RemoveAccount(ExtendedAccount eAccount)
        {
            accounts.Remove(eAccount);
        }

        public void ClearAccounts()
        {
            accounts = new ExtendedAccounts();
        }

        public ExtendedAccounts ListAccounts()
        {
            return accounts;
        }

        public void AddCard(ExtendedCard eCard)
        {
            cards.Add(eCard);
        }

        public void AddCards(ExtendedCards eCards)
        {
            cards = eCards;
        }

        public void RemoveCard(ExtendedCard eCard)
        {
            cards.Remove(eCard);
        }

        public void ClearCards()
        {
            cards.Clear();
        }

        public ExtendedCards ListCards()
        {
            return cards;
        }

        //Ugyanaz, mint a konstruktor
        public void ImportFromDataSet(DataSet ds)
        {
            if (ds.Tables.Contains("Client"))
            {
                client.ImportFromDataRow(ds.Tables["Client"].Rows[0]);
            }

            if (ds.Tables.Contains("Account"))
            {
                accounts.ImportFromDataTable(ds.Tables["Account"]);
            }
            else if (ds.Tables.Contains("Accounts"))
            {
                accounts.ImportFromDataTable(ds.Tables["Accounts"]);
            }

            if (ds.Tables.Contains("Card"))
            {
                cards.ImportFromDataTable(ds.Tables["Card"]);
            }
            else if (ds.Tables.Contains("Cards"))
            {
                cards.ImportFromDataTable(ds.Tables["Cards"]);
            }
        }

        public DataSet ExportToDataSet()
        {
            DataSet result = new DataSet();
            DataTable table;

            if (client != null)
            {
                DataRow row;
                table = new DataTable("Client");
                table.Columns.Add("id");
                table.Columns.Add("firstName");
                table.Columns.Add("lastName");
                table.Columns.Add("dateOfBirth");
                table.Columns.Add("placeOfBirth");
                table.Columns.Add("nameOfMother");
                table.Columns.Add("address");
                table.Columns.Add("phone");
                table.Columns.Add("email");
                table.Columns.Add("live");
                row = table.NewRow();
                DataRow temp = client.ExportToDataRow();
                for (int i = 0; (i < temp.ItemArray.Length && i < row.ItemArray.Length); i++)
                {
                    row[i] = temp[i];
                }
                table.Rows.Add(row);
                result.Tables.Add(table);
            }

            if (accounts != null)
            {
                table = accounts.ExportToDataTable();
                table.TableName = "Accounts";
                result.Tables.Add(table);
            }

            if (cards != null)
            {
                table = cards.ExportToDataTable();
                table.TableName = "Cards";
                result.Tables.Add(table);
            }
            return result;
        }
    }

    public class ComplexExtendedAccount
    {
        private ExtendedAccount account;
        private ExtendedRoles roles;
        private ExtendedCards cards;
        private ExtendedTransactions transactions;

        public ComplexExtendedAccount()
        {
            account = new ExtendedAccount();
            roles = new ExtendedRoles();
            cards = new ExtendedCards();
            transactions = new ExtendedTransactions();
        }

        public ComplexExtendedAccount(DataSet ds)
        {
            account = new ExtendedAccount();
            roles = new ExtendedRoles();
            cards = new ExtendedCards();
            transactions = new ExtendedTransactions();

            if (ds.Tables.Contains("Account"))
            {
                account.ImportFromDataRow(ds.Tables["Account"].Rows[0]);
            }

            if (ds.Tables.Contains("Role"))
            {
                roles.ImportFromDataTable(ds.Tables["Role"]);
            }
            else if (ds.Tables.Contains("Roles"))
            {
                roles.ImportFromDataTable(ds.Tables["Roles"]);
            }

            if (ds.Tables.Contains("Card"))
            {
                cards.ImportFromDataTable(ds.Tables["Card"]);
            }
            else if (ds.Tables.Contains("Cards"))
            {
                cards.ImportFromDataTable(ds.Tables["Cards"]);
            }

            if (ds.Tables.Contains("Transaction"))
            {
                transactions.ImportFromDataTable(ds.Tables["Transaction"]);
            }
            else if (ds.Tables.Contains("Transactions"))
            {
                transactions.ImportFromDataTable(ds.Tables["Transactions"]);
            }
        }

        public void SetAccount(ExtendedAccount eAccount)
        {
            account = eAccount;
        }

        public void ClearAccount()
        {
            account = new ExtendedAccount();
        }

        public ExtendedAccount ListAccount()
        {
            return account;
        }

        public void AddRole(ExtendedRole eRole)
        {
            roles.Add(eRole);
        }

        public void AddRoles(ExtendedRoles eRoles)
        {
            roles = eRoles;
        }

        public void RemoveRole(ExtendedRole eRole)
        {
            roles.Remove(eRole);
        }

        public void ClearRoles()
        {
            roles = new ExtendedRoles();
        }

        public ExtendedRoles ListRoles()
        {
            return roles;
        }

        public void AddCard(ExtendedCard eCard)
        {
            cards.Add(eCard);
        }

        public void AddCards(ExtendedCards eCards)
        {
            cards = eCards;
        }

        public void RemoveCard(ExtendedCard eCard)
        {
            cards.Remove(eCard);
        }

        public void ClearCards()
        {
            cards.Clear();
        }

        public ExtendedCards ListCards()
        {
            return cards;
        }

        public void AddTransaction(ExtendedTransaction eTransaction)
        {
            transactions.Add(eTransaction);
        }

        public void AddTransactions(ExtendedTransactions eTransactions)
        {
            transactions = eTransactions;
        }

        public void RemoveTransaction(ExtendedCard eTransaction)
        {
            transactions.Remove(eTransaction);
        }

        public void ClearTransactions()
        {
            transactions.Clear();
        }

        public ExtendedTransactions ListTransactions()
        {
            return transactions;
        }

        public void ImportFromDataSet(DataSet ds)
        {
            if (ds.Tables.Contains("Account"))
            {
                account.ImportFromDataRow(ds.Tables["Account"].Rows[0]);
            }

            if (ds.Tables.Contains("Role"))
            {
                roles.ImportFromDataTable(ds.Tables["Role"]);
            }
            else if (ds.Tables.Contains("Roles"))
            {
                roles.ImportFromDataTable(ds.Tables["Roles"]);
            }

            if (ds.Tables.Contains("Card"))
            {
                cards.ImportFromDataTable(ds.Tables["Card"]);
            }
            else if (ds.Tables.Contains("Cards"))
            {
                cards.ImportFromDataTable(ds.Tables["Cards"]);
            }

            if (ds.Tables.Contains("Transaction"))
            {
                transactions.ImportFromDataTable(ds.Tables["Transaction"]);
            }
            else if (ds.Tables.Contains("Transactions"))
            {
                transactions.ImportFromDataTable(ds.Tables["Transactions"]);
            }
        }

        public DataSet ExportToDataSet()
        {
            DataSet result = new DataSet();
            DataTable table;

            if (account != null)
            {
                DataRow row;
                table = new DataTable("Account");
                table.Columns.Add("id");
                table.Columns.Add("accountNumber");
                table.Columns.Add("accountName");
                table.Columns.Add("accountType");
                table.Columns.Add("currency");
                table.Columns.Add("sectorType");
                table.Columns.Add("balance");
                table.Columns.Add("dateOfOpening");
                table.Columns.Add("dateOfClosing");
                table.Columns.Add("live");
                row = table.NewRow();
                DataRow temp = account.ExportToDataRow();
                for (int i = 0; (i < temp.ItemArray.Length && i < row.ItemArray.Length); i++)
                {
                    row[i] = temp[i];
                }
                table.Rows.Add(row);
                result.Tables.Add(table);
            }

            if (roles != null)
            {
                table = roles.ExportToDataTable();
                table.TableName = "Roles";
                result.Tables.Add(table);
            }

            if (cards != null)
            {
                table = cards.ExportToDataTable();
                table.TableName = "Cards";
                result.Tables.Add(table);
            }

            if (transactions != null)
            {
                table = transactions.ExportToDataTable();
                table.TableName = "Transactions";
                result.Tables.Add(table);
            }
            return result;
        }
    }

    public class UserInterfaceE
    {
        public void TestMethod()
        {
            ///Létre kell hozni a két complex osztályt
            ///Fel kell tölteni a mezőiket
            ///Össze kell kapcsolni őket

            //1.
            ComplexExtendedClient client = new ComplexExtendedClient();
            ComplexExtendedAccount account = new ComplexExtendedAccount();

            //2.
            //client.
        }

        public void ListClientDatas()
        {

        }

        public void ListClientAccounts()
        {

        }

        public void ListClientCards()
        {

        }

        public void ListAccountDatas()
        {

        }

        public void ListAccountRoles()
        {

        }

        public void ListAccountCards()
        {

        }

        public void ListAccountTransactions()
        {

        }
    }
}