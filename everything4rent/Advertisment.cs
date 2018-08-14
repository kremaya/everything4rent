﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace everything4rent
{
    class Advertisment
    {
        static string cs = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + @"\Database1.mdf;";

        public static int add(string username,string date, string name, string type, string recieve, string from, string to, int cancle, string policy)
        {
            SqlConnection con;
            SqlCommand cmd;

            string qry = "insert into Advertisments (username, [date],name,[type],recieve,[from],[to],cancle,[policy]) values('" + username + "','" + date + "','" + name + "','" + type + "','" + recieve + "','" + from + "','" + to + "',"+cancle+",'" + policy + "');";
            con = new SqlConnection(cs);

            con.Open();
            cmd = new SqlCommand(qry, con);
            cmd.ExecuteNonQuery();


            qry = "select advertismentID from Advertisments where username='"+username+"' AND [date]='"+date+ "' AND name='"+name+"' AND [type]='"+type+ "' AND recieve='"+recieve+ "' AND [from]='"+from+ "' AND [to]='"+to+ "' AND cancle="+cancle+ " AND [policy]='"+policy+"';";

            cmd = new SqlCommand(qry, con);
            SqlDataReader dataReader = cmd.ExecuteReader();

            int ans = 0;
            if (dataReader.HasRows)
            {
                dataReader.Read();
                ans=(int)dataReader.GetValue(0);               
            }

            dataReader.Close();
            cmd.Dispose();
            con.Close();

            return ans;
        }

        public static string validation(string name, string title , string recieve, string from, string to)
        {
            string errors = "";
            if (name.Length ==0)
                errors += "You have to insert name\n";

            if (title=="Lending"|| title=="Donation")
            {
                try{Convert.ToDouble(recieve);}
                catch (Exception){errors += "Price must be a number\n";}
            }


            try
            {
                DateTime d1 = Convert.ToDateTime(from);
                DateTime d2 = Convert.ToDateTime(to);
                if (d1.CompareTo(d2) >= 0)
                    errors += "Start date is later that end date\n";

            }
            catch (Exception)
            {
                errors += "Invalid date\n";
            }


            return errors;
        }

        public static void allAdv(ListView list,string username)
        {
            list.Items.Clear();

            SqlConnection con;
            SqlCommand cmd;
            SqlDataReader dataReader;
            string qry = "select advertismentID,date ,name,type,recieve from Advertisments where username='"+username+"';";
            con = new SqlConnection(cs);

            con.Open();
            cmd = new SqlCommand(qry, con);
            dataReader = cmd.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    ListViewItem l = new ListViewItem((string)dataReader.GetValue(2));
                    l.SubItems.Add((string)dataReader.GetValue(1));
                    l.SubItems.Add((string)dataReader.GetValue(3));
                    l.SubItems.Add((string)dataReader.GetValue(4));
                    l.SubItems.Add(""+(int)dataReader.GetValue(0));
                    list.Items.Add(l);
                }
            }

            dataReader.Close();
            cmd.Dispose();
            con.Close();

            
        }

        public static List<string> getAdv(int advID)
        {
            List<string> ans= new List<string>();

            SqlConnection con;
            SqlCommand cmd;
            SqlDataReader dataReader;
            string qry = "select date ,name,[type],recieve, [from],[to],cancle,[policy] from Advertisments where advertismentID=" + advID + ";";
            con = new SqlConnection(cs);

            con.Open();
            cmd = new SqlCommand(qry, con);
            dataReader = cmd.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    ans.Add((string)dataReader.GetValue(0));
                    ans.Add((string)dataReader.GetValue(1));
                    ans.Add((string)dataReader.GetValue(2));
                    ans.Add((string)dataReader.GetValue(3));
                    ans.Add((string)dataReader.GetValue(4));
                    ans.Add((string)dataReader.GetValue(5));
                    ans.Add("" + (int)dataReader.GetValue(6));
                    ans.Add((string)dataReader.GetValue(7));
                }
            }

            dataReader.Close();
            cmd.Dispose();
            con.Close();

            return ans;
        }

        public static void update(int advID, string date, string name, string type, string recieve, string from, string to, int cancle, string policy)
        {
            SqlConnection con;
            SqlCommand cmd;

            string qry = "update  Advertisments set [date]='"+date+"',name='"+name+"',[type]='"+ type+"', recieve='"+ recieve+"',[from]='"+ from+"',[to]='"+to+"',cancle="+ cancle+",[policy]='"+ policy+ "' where advertismentID="+advID+";";
            con = new SqlConnection(cs);

            con.Open();
            cmd = new SqlCommand(qry, con);
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            con.Close();
        }

        public static List<string> searchAll(List<string> val)
        {
            string qry = "select * from Products join Advertisments on Products.advertismentID=Advertisments.advertismentID where ";
            if (val[0] == "true")
                qry += "";

            return null;
        }
    }
}
