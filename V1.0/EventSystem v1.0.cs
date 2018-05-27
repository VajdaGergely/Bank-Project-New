using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EventSystem
/// </summary>


public class Account
{
    //Ezt módosítja minden eljárás
    //Közvetlenül kívülről nem lehet hozzáférni
    //Később lehet, hogy a tulajdonság, hogy lehet egy belső változó is, ahol ellenőrizhetjük a ki és bemenetet (így csak egy helyen kell) (pl ha nem lehet nulla)
    private int Balance { get; set; }

    public Account()
    {
        Balance = 0;
    }

    public Account(int b)
    {
        Balance = b;
    }

    public void BankFee(int fee)
    {
        Balance -= fee;
    }

    public void CashDeposit(int cash)
    {
        Balance += cash;
    }

    public void CashWithdrawl(int cash)
    {
        Balance -= cash;
    }

    public int GetBalance()
    {
        return Balance;
    }
}

public static class BankFeeSystem
{
    public static int AccountFee { get; set; }
}

public static class TimeSystem
{
    private static bool locked = false;
    private static DateTime startTime;
    private static DateTime today;

    public static DateTime StartTime
    {
        get
        {
            return startTime;
        }

        private set
        {
            startTime = value;
        }
    }

    public static DateTime Today
    {
        get
        {
            return today;
        }

        private set
        {
            today = value;
        }
    }

    public static void SetStartTime(string start)
    {
        if (!locked)
        {
            //StartTime = DateTime.ParseExact(start, "dd/mm/yyyy", new CultureInfo("en-US", false));
            StartTime = Convert.ToDateTime(start);
            Today = StartTime;
            locked = true;
        }
    }

    public static void JumpForwardInTime(string type, int count)
    {
        switch (type)
        {
            case "day":
                Today = Today.AddDays(count);
                break;
            case "month":
                Today = Today.AddMonths(count);
                break;
            case "year":
                Today = Today.AddYears(count);
                break;
            default:
                break;
        }
    }

    public static void JumpForwardInTime(string date)
    {
        //Today = DateTime.ParseExact(date, "dd/mm/yyyy", new CultureInfo("en-US", false));
        Today = Convert.ToDateTime(date);
    }
}

public enum StatusType
{
    Live,
    Executed
}

public abstract class TimeEvent
{
    public int ID { get; set; }
    public DateTime Date { get; set; }
    public StatusType Status { get; set; }
    public string Comment { get; set; }

    public TimeEvent()
    {
        ID = 0;
        //Date = DateTime.ParseExact("01/01/1900", "dd/mm/yyyy", new CultureInfo("hu-HU", false)); 
        Date = Convert.ToDateTime("01/01/1900");
        Status = StatusType.Live;
        Comment = "";
    }

    public TimeEvent(int id, string date, string comment)
    {
        ID = id;
        Date = Convert.ToDateTime(date);
        Status = StatusType.Live;
        Comment = comment;
    }

    public abstract void ExecuteEvent();
}

public class BankFeeEvent : TimeEvent
{
    public int Fee { get; set; }

    public BankFeeEvent() : base()
    {
        Fee = 0;
    }

    public BankFeeEvent(int id, string date, string comment, int fee) : base(id, date, comment)
    {
        Fee = fee;
    }

    public delegate void DelegateType(int a);
    public List<DelegateType> delegateList = new List<DelegateType>();

    public void AssignNewItemToEvent(ref Account account)
    {
        //create new item
        DelegateType del = null;
        delegateList.Add(del);
        //assign
        delegateList[delegateList.Count - 1] = account.BankFee;
    }

    public override void ExecuteEvent()
    {
        foreach (DelegateType d in delegateList)
        {
            d(Fee);
        }
    }
}

public static class EventSystem
{
    //események listája
    private static List<TimeEvent> timeEvents = new List<TimeEvent>();

    //új esemény hozzáadása, bármilyen származtatott osztályt tartalmazhat
    public static void AddEvent(TimeEvent timeEvent)
    {
        timeEvents.Add(timeEvent);
    }

    //kiválogatjuk az aktuális event-eket, amiket végre kell hajtani. (Lehet a neve Run() CheckEvents() stb...)
    public static void Run()
    {
        List<TimeEvent> result = new List<TimeEvent>();
        //Egyelőre csak simán végigmegyünk rajta elejétől a végéig. Nem optimalizálunk és nem törölgetünk bele
        foreach (TimeEvent timeEvent in timeEvents)
        {
            if (timeEvent.Status == StatusType.Live && timeEvent.Date <= TimeSystem.Today)
            {
                //Státusz átállítása
                timeEvent.Status = StatusType.Executed;

                //hozzáadjuk az eseményeket a listához, amit végre akarunk hajtani.
                result.Add(timeEvent);
            }
        }
        //végrehajtjuk az event-eket
        ExecuteEvents(result);
    }

    //Végrehajtjuk az egyes event-eket. Mindegyiknek a saját örökölt ExecuteEvent metódusa fut le a saját belső beállításaival...
    private static void ExecuteEvents(List<TimeEvent> events)
    {
        foreach (TimeEvent x in events)
        {
            x.ExecuteEvent();
        }
    }

    /*
    //Test List Function (figyeltünk a padding-re is...)
    private static void ListEvents(List<TimeEvent> events)
    {
        string id = "";
        string date = "";
        string status = "";
        string fee = "";
        string comment = "";
        foreach (TimeEvent timeEvent in events)
        {
            //padding...
            id = View.ID(timeEvent.ID.ToString());
            date = View.Date(timeEvent.Date.ToShortDateString());
            status = View.Status(timeEvent.Status.ToString());
            fee = View.Fee(BankFeeSystem.AccountFee.ToString());
            comment = timeEvent.Comment;
            Console.WriteLine(id + " | " + date + " | " + status + " | " + fee + " | " + comment);
        }
    }

    public static void ListAllEvents()
    {
        ListEvents(timeEvents);
    }


    public static void ListActualEvents()
    {
        List<TimeEvent> result = new List<TimeEvent>();
        foreach (TimeEvent timeEvent in timeEvents)
        {
            if (timeEvent.Status == StatusType.Live && timeEvent.Date <= TimeSystem.Today)
            {
                result.Add(timeEvent);
            }
        }
        ListEvents(result);
    }
    */
}

//########################################

/*
public static class View
{
    public static string ID(string id)
    {
        if (int.Parse(id) < 10)
        {
            id = " " + id;
        }
        return id;
    }

    public static string Date(string date)
    {
        if (date.Length == 8) //d és h helyére is kell kiegészítő 0
        {
            date = date.Insert(0, "0");
            date = date.Insert(3, "0");
        }
        else if (date.Length == 9) //d vagy h helyére kell kiegészítő 0 - megnézzük, hogy melyikhez
        {
            if (date.Substring(1, 1) == "/") //a 2. karakter a / jel -> az elejére kell 0 0
            {
                date = date.Insert(0, "0");
            }
            else if (date.Substring(2, 1) == "/") //a 3. karakter a / jel -> a közepére kell a 0
            {
                date = date.Insert(3, "0");
            }
        }
        return date;
    }

    public static string Status(string status)
    {
        if (status == "Live")
        {
            status += "    ";
        }
        return status;
    }

    public static string Fee(string fee)
    {
        for (int i = fee.Length; i < 5; i++)
        {
            fee = " " + fee;
        }
        return fee;
    }
}
*/

/*
public class Program
{
    public static void PrintHeader(Account account, int section, string availableCommands)
    {
        Console.WriteLine("BankTime System v1.0 Console");
        Console.WriteLine(TimeSystem.Today.ToShortDateString() + "		Balance: " + account.GetBalance() + "	AccountFee: " + BankFeeSystem.AccountFee);
        Console.WriteLine("Section" + section.ToString() + "	" + availableCommands);
        Console.WriteLine();
    }

    //a program váza...
    public static void UserInterface(Account account)
    {
        string cmd = "";
        int section = 1;
        string availableCommands = "Available commands: ";

        do
        {
            //init
            if (section == 1)
            {
                availableCommands = "Available commands: set fee, start, exit";
            }
            else if (section == 2)
            {
                availableCommands = "Available commands: jump, list, run, exit";
            }

            PrintHeader(account, section, availableCommands);
            cmd = Console.ReadLine();

            if (section == 1) //section 1
            {
                switch (cmd)
                {
                    //case "get balance":
                    //	Console.WriteLine("Balance: " + account.GetBalance());
                    //	break;
                    case "set fee":
                        try
                        {
                            BankFeeSystem.AccountFee = int.Parse(Console.ReadLine());
                        }
                        catch { }
                        break;
                    //case "get fee":
                    //	Console.WriteLine("Account fee: " + BankFeeSystem.AccountFee);
                    //	break;
                    case "start":
                        //tovább lépünk a section2-ra
                        section = 2;
                        break;
                    case "exit":
                        //finalization...
                        break;
                    default:
                        Console.WriteLine("Wrong command!");
                        break;
                }
            }
            else if (section == 2) //section 2
            {
                switch (cmd)
                {
                    //case "get balance":
                    //	Console.WriteLine(account.GetBalance());
                    //	break;
                    //case "get fee":
                    //	Console.WriteLine(BankFeeSystem.AccountFee);
                    //	break;
                    case "jump":
                        Console.WriteLine("Type and Count");
                        TimeSystem.JumpForwardInTime(Console.ReadLine(), int.Parse(Console.ReadLine()));
                        break;
                    case "jump to date":
                        Console.WriteLine("Type a date to jump");
                        TimeSystem.JumpForwardInTime(Console.ReadLine());
                        break;
                    case "list all":
                        EventSystem.ListAllEvents();
                        break;
                    case "list actual":
                        EventSystem.ListActualEvents();
                        break;
                    case "run":
                        EventSystem.Run();
                        break;
                    case "exit":
                        //finalization...
                        break;
                    case "shell script1": //saját script hogy ne kelljen beütni mindent százszor...
                        EventSystem.ListAllEvents(); //list all
                        TimeSystem.JumpForwardInTime("6/1/2002"); // jump to date
                        EventSystem.ListActualEvents(); //list actual
                        EventSystem.Run(); //run
                        EventSystem.ListAllEvents(); //list all
                        break;
                    default:
                        Console.WriteLine("Wrong command!");
                        break;
                }
            }
            Console.WriteLine();
            Console.WriteLine("-------------------------");
            Console.WriteLine();
        }
        while (cmd != "exit");
    }

    public static void Main()
    {
        //Account
        Account account = new Account(10000);
        Account account2 = new Account(7500);

        //BankFeeEvent
        BankFeeEvent bankFeeEvent;

        //BankFeeSystem
        BankFeeSystem.AccountFee = 400;

        //TimeSystem
        TimeSystem.SetStartTime("01/01/2000");
        DateTime startDate = Convert.ToDateTime("01/01/2002");

        for (int i = 1; i <= 15; i++)
        {
            bankFeeEvent = new BankFeeEvent(i, (startDate.AddMonths(i)).ToShortDateString(), "***Monthly Fee", BankFeeSystem.AccountFee);
            bankFeeEvent.AssignNewItemToEvent(ref account);
            bankFeeEvent.AssignNewItemToEvent(ref account2);
            EventSystem.AddEvent(bankFeeEvent);
        }



        UserInterface(account);
        Console.WriteLine("Program has executed...");

        Console.WriteLine(account.GetBalance());
        Console.WriteLine(account2.GetBalance());
    }
}
}
*/

