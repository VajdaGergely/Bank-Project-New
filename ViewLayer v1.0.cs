using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

/// <summary>
/// Summary description for ViewLayer
/// </summary>
/// 

namespace Technical
{
    public static class View
    {
        public static DataSet ClientView1 (DataSet ds)
        {
            //alapból ilyen, úgy tűnik...
            //ds.Tables[0].Rows[0]["dateOfBirth"] = ((DateTime)ds.Tables[0].Rows[0]["dateOfBirth"]).ToShortDateString();

            ds.Tables[0].Rows[0]["phone"] = string.Format("+36{0}{1}/{2}{3}{4}-{5}{6}{7}{8}", ds.Tables[0].Rows[0]["phone"].ToString().ToCharArray().Select(c => c.ToString()).ToArray());
            //ds.Tables[0].Rows[0]["email"] = ds.Tables[0].Rows[0]["email"];
            return ds;
        }

        public static DataSet AccountView1 (DataSet ds)
        {
            //ds.Tables[0].Rows[0]["accountNumber"] = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}-{8}{9}{10}{11}{12}{13}{14}{15}-{16}{17}{18}{19}{20}{21}{22}{23}", ds.Tables[0].Rows[0]["accountNumber"].ToString().ToCharArray().Select(c => c.ToString()).ToArray());

            int i = (ds.Tables[0].Rows[0]["balance"].ToString().Length) - 1;
            while (i > 0)
            {
                if(i > 2)
                {
                    ds.Tables[0].Rows[0]["balance"] = ds.Tables[0].Rows[0]["balance"].ToString().Insert(i - 2, ",");
                }
                    i -= 2;
            }
            ds.Tables[0].Rows[0]["balance"] += " " + ds.Tables[0].Rows[0]["currency"];

            //ds.Tables[0].Rows[0]["balance"] = string.Format("", ds.Tables[0].Rows[0]["balance"].ToString().ToCharArray().Select(c => c.ToString()).ToArray());

            ds.Tables[0].Rows[0]["dateOfOpening"] = (Convert.ToDateTime(ds.Tables[0].Rows[0]["dateOfOpening"].ToString())).ToShortDateString();
            return ds;
        }

        public static DataSet TransactionView1 (DataSet ds)
        {
            return ds;
        }
    }
}

public static class ViewLayer
{
    public static DataSet GetClientData(string id)
    {
        BusinessLayerOld.Client client = new BusinessLayerOld.Client();
        return Technical.View.ClientView1(client.GetClientData(id));
    }

    public static void SetClientData(string id, string firstName, string lastName, string dateOfBirth, string placeOfBirth, string nameOfMother, string address, string phone, string email)
    {
        BusinessLayerOld.Client client = new BusinessLayerOld.Client(id, firstName, lastName, dateOfBirth, placeOfBirth, nameOfMother, address, phone, email);
        client.SetClientData();
    }

    public static void CreateClient (string firstName, string lastName, string dateOfBirth, string placeOfBirth, string nameOfMother, string address, string phone, string email)
    {
        BusinessLayerOld.Client client = new BusinessLayerOld.Client(firstName, lastName, dateOfBirth, placeOfBirth, nameOfMother, address, phone, email);
        client.CreateClient();
    }

    public static void OpenAccount(string clientId, string accountType, string currency, string sectorType)
    {

    }

    public static DataSet ListAccounts(string id)
    {
        var account = new BusinessLayerOld.Account();
        return Technical.View.AccountView1(account.ListAccounts(id));
    }

    public static void CloseAccount(string client_id, string acc_id)
    {

    }

    public static void CreateRole(string client_id, string acc_id, string role)
    {

    }
    public static void ModifyRole(string client_id, string acc_id, string role)
    {

    }
    public static void DeleteRole(string client_id, string acc_id)
    {

    }
    public static void ListRoles(string client_id)
    {

    }
    public static void CreateCashInTransaction(string type, string acc_num, string amount, string curr, string date, string time, string msg)
    {

    }
    public static void CreateCashOutTransaction(string type, string acc_num, string amount, string curr, string date, string time, string msg)
    {

    }
    public static void CreateTransferTransaction(string type, string src_acc_num, string dst_acc_num, string src_name, string dst_name, string amount, string curr, string date, string time, string msg)
    {

    }
    public static DataSet ListTransactions(string accountNumber)
    {
        var transfer = new BusinessLayerOld.Transfer();
        return Technical.View.TransactionView1(transfer.ListTransactions(accountNumber));
    }
}