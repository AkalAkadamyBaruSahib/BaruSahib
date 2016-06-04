using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.IO;
using System.Configuration;

namespace DAL
{
    public class DalAccessUtility
    {
        /// <summary>
        /// connnection property.
        /// </summary>
        static SqlConnection _Con;
        public static SqlConnection Connection
        {
            get
            {
                if (_Con == null)
                {
                    _Con = new SqlConnection(ConnectionString);
                }
                return _Con;
            }
        }

        /// <summary>
        /// Connection string property.
        /// </summary>
        static string _ConnectionString;
        public static string ConnectionString
        {
            get
            {
                if (_ConnectionString == null)
                {
                    _ConnectionString = Convert.ToString(ConfigurationManager.ConnectionStrings["ConnectionString"]);
                }
                return _ConnectionString;
            }
        }

        /// <summary>
        /// Connection state property
        /// </summary>
        public ConnectionState ConnectionState
        {
            get
            {
                return ConnectionState.Open;
            }
        }

        string _imagefolder;
        public string Imagefolder
        {
            get
            {
                return _imagefolder;
            }
            set
            {
                _imagefolder = value;
            }
        }


        int _LoginPass;
        public int LoginPass
        {
            get
            {
                return _LoginPass;
            }
            set
            {
                _LoginPass = value;
            }
        }


        /// <summary>
        /// This is for fetching data from database when Parameters passing as argument.
        /// </summary>
        /// <param name="SpName"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>

        public static DataSet GetDataInDataSet(string StoredProc, Hashtable Parameters)
        {
            DataSet Ds = new DataSet();
            try
            {
                SqlParameter Param;
                SqlCommand Cmd = new SqlCommand();
                SqlDataAdapter Adap;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = StoredProc;
                Cmd.Connection = Connection;
                Param = Cmd.Parameters.Add("return value", SqlDbType.Int);
                Param.Direction = ParameterDirection.ReturnValue;
                foreach (DictionaryEntry Entry in Parameters)
                {
                    Cmd.Parameters.AddWithValue("@" + Entry.Key, Entry.Value);
                }
                if (!(Connection.State == ConnectionState.Open))
                {
                    Connection.Open();
                }
                Adap = new SqlDataAdapter(Cmd);
                Adap.Fill(Ds);
                int RowEffect = (int)Param.Value;
            }
            catch (Exception ex)
            {
                //LogMessage(ex.Message, ex.StackTrace, "Error"); 
                string msg = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return Ds;
        }   

        /// <summary>
        /// This is for Fetching data from database when no argument is passing.
        /// </summary>
        /// <param name="SpName"></param>
        /// <returns></returns>
        public static DataSet GetDataInDataSet(string StoredProc)
        {
            DataSet Ds = new DataSet();
            try
            {
                SqlCommand Cmd;
                SqlDataAdapter Adap;
                Cmd = new SqlCommand(StoredProc, Connection);
                if (!(Connection.State == ConnectionState.Open))
                {
                    Connection.Open();
                }
                Adap = new SqlDataAdapter(Cmd);
                Adap.Fill(Ds);
            }
            catch (Exception ex)
            {
                //GetDataInDataSet(StoredProc);
                //string error = ex.Message.Replace("'", "");
                //ExecuteNonQuery("INSERT INTO ERRORLOG VALUES (NULL,'" + ex.StackTrace + "',GETDATE())");
                
                //string mess = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return Ds;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="StoredProc"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public static int SetDataByExecuteNonQuery(string StoredProc, Hashtable Parameters)
        {
            int RowsAffected = 0;
            try
            {
                SqlParameter Param;
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = StoredProc;
                Cmd.Connection = Connection;
                if (!(Connection.State == ConnectionState.Open))
                {
                    Connection.Open();
                }
                Param = Cmd.Parameters.Add("return value", SqlDbType.Int);
                Param.Direction = ParameterDirection.ReturnValue;
                foreach (DictionaryEntry Entry in Parameters)
                {
                    Cmd.Parameters.AddWithValue("@" + Entry.Key, Entry.Value);
                }
                Cmd.ExecuteNonQuery();
                RowsAffected = (int)Param.Value;
            }
            catch (Exception ex)
            {
                
                RowsAffected = 10001;
                string mess = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return RowsAffected;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="StoredProc"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        ///
        public static string Encrypt(string strEncrypted)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
            string encryptedConnectionString = Convert.ToBase64String(b);
            return encryptedConnectionString;
        }
        public static string Decrypt(string encrString)
        {
            byte[] b = Convert.FromBase64String(encrString);
            string decryptedConnectionString = System.Text.ASCIIEncoding.ASCII.GetString(b);
            return decryptedConnectionString;
        }
        public static Int64 ExecuteNonQuery(string StoredProc)
        {
            Int64 RowsAffected = 0;
            try
            {

                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandText = StoredProc;
                Cmd.Connection = Connection;
                if (!(Connection.State == ConnectionState.Open))
                {
                    Connection.Open();
                }

                RowsAffected = Cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //string error = ex.Message.Replace("'", "");
                //ExecuteNonQuery("INSERT INTO ERRORLOG VALUES (NULL,'" + ex.StackTrace + "',GETDATE())");

                RowsAffected = 10001;
                string mess = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return RowsAffected;
        }



        public static int SetDataByExecuteNonQueryCmd(string StoredProc, Hashtable Parameters, SqlCommand Cmd)
        {
            int RowsAffected = 0;
            //try
            //{
            SqlParameter Param;
            //SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = StoredProc;
            //Cmd.Connection = Connection;

            //Connection.BeginTransaction();

            //if (!(Connection.State == ConnectionState.Open))
            //{
            //    Connection.Open();
            //}
            Cmd.Parameters.Clear();
            Param = Cmd.Parameters.Add("return value", SqlDbType.Int);
            Param.Direction = ParameterDirection.ReturnValue;
            foreach (DictionaryEntry Entry in Parameters)
            {
                Cmd.Parameters.AddWithValue("@" + Entry.Key, Entry.Value);
            }
            RowsAffected = Cmd.ExecuteNonQuery();
            RowsAffected = (int)Param.Value;
            //}
            //catch (Exception ex)
            //{
            //    string mess = ex.Message;
            //}
            //finally
            //{
            //    //Connection.Close();
            //}
            return RowsAffected;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="StoredProc"></param>
        /// <param name="ds"></param>
        public static void UpdateData(string StoredProc, DataSet ds)
        {
            try
            {
                SqlDataAdapter Adap;
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = StoredProc;
                Cmd.Connection = Connection;
                if (!(Connection.State == ConnectionState.Open))
                {
                    Connection.Open();
                }
                Adap = new SqlDataAdapter(Cmd);
                Adap.Update(ds);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="StoredProc"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public static int GetDataInScaler(string StoredProc, Hashtable Parameters)
        {
            string strData = "";
            int RowEffect = -1;
            try
            {
                SqlParameter Param;
                SqlCommand Cmd;
                Cmd = new SqlCommand();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = StoredProc;
                Cmd.Connection = Connection;
                Param = Cmd.Parameters.Add("return value", SqlDbType.Int);
                Param.Direction = ParameterDirection.ReturnValue;
                foreach (DictionaryEntry Entry in Parameters)
                {
                    if (Entry.Value == null)
                    {
                        Cmd.Parameters.AddWithValue("@" + Entry.Key, DBNull.Value);
                    }
                    else
                    {
                        Cmd.Parameters.AddWithValue("@" + Entry.Key, Entry.Value);
                    }
                }
                if (!(Connection.State == ConnectionState.Open))
                {
                    Connection.Open();
                }
                strData = Convert.ToString(Cmd.ExecuteScalar());
                RowEffect = (int)Param.Value;
            }
            catch (Exception ex)
            {
                string mess = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return RowEffect;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="StoredProc"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public static Guid GetUserId(string StoredProc, Hashtable Parameters)
        {
            SqlParameter Param;
            SqlCommand Cmd;
            Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = StoredProc;
            Cmd.Connection = Connection;
            Param = Cmd.Parameters.Add("return value", SqlDbType.Int);
            Param.Direction = ParameterDirection.ReturnValue;
            foreach (DictionaryEntry Entry in Parameters)
            {
                Cmd.Parameters.AddWithValue("@" + Entry.Key, Entry.Value);
            }
            if (!(Connection.State == ConnectionState.Open))
            {
                Connection.Open();
            }
            Guid UserId = new Guid(Convert.ToString(Cmd.ExecuteScalar()));
            Connection.Close();
            return UserId;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        private static object GetConverter(Guid UserId)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="StoredProc"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public static SqlDataReader GetDataInDataReader(string StoredProc, Hashtable Parameters)
        {
            SqlParameter Param;
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = StoredProc;
            Cmd.Connection = Connection;
            Param = Cmd.Parameters.Add("return value", SqlDbType.Int);
            Param.Direction = ParameterDirection.ReturnValue;
            foreach (DictionaryEntry Entry in Parameters)
            {
                Cmd.Parameters.AddWithValue("@" + Entry.Key, Entry.Value);
            }
            if (!(Connection.State == ConnectionState.Open))
            {
                Connection.Open();
            }
            SqlDataReader dr = Cmd.ExecuteReader();
            return dr;
        }
        public static int IsQueryStringSecure(string TexttoValidate)
        {
            string TextVal;

            TextVal = TexttoValidate;

            int IsSecure = 1;

            //Build an array of characters that need to be filter.
            string[] strDirtyQueryString = { "@@", "'", "xp_", ";", "--", "<", ">", "script", "iframe", "delete", "drop", "exec", "or" };

            //Loop through all items in the array
            foreach (string item in strDirtyQueryString)
            {
                if (TextVal.IndexOf(item) != -1)
                {
                    IsSecure = 0;
                }
            }

            return IsSecure;
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="StoredProc"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public static int UpdateDataByExecuteNonQuery(string StoredProc, Hashtable Parameters)
        {
            int RowAffected = 0;
            try
            {
                SqlParameter Param;
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = StoredProc;
                Cmd.Connection = Connection;
                Param = Cmd.Parameters.Add("return value", SqlDbType.Int);
                Param.Direction = ParameterDirection.ReturnValue;
                foreach (DictionaryEntry Entry in Parameters)
                {
                    Cmd.Parameters.AddWithValue("@" + Entry.Key, Entry.Value);
                }
                if (!(Connection.State == ConnectionState.Open))
                {
                    Connection.Open();
                }
                RowAffected = Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return RowAffected;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="StoredProc"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public static int DeleteDataByExecuteNonQuery(string StoredProc, Hashtable Parameters)
        {
            int RowAffected = 0;
            try
            {
                SqlParameter Param;
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = StoredProc;
                Cmd.Connection = Connection;
                Param = Cmd.Parameters.Add("return value", SqlDbType.Int);
                Param.Direction = ParameterDirection.ReturnValue;
                foreach (DictionaryEntry Entry in Parameters)
                {
                    Cmd.Parameters.AddWithValue("@" + Entry.Key, Entry.Value);
                }
                if (!(Connection.State == ConnectionState.Open))
                {
                    Connection.Open();
                }
                RowAffected = Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            finally
            {
                Connection.Close();
            }
            return RowAffected;
        }
        public string BuildPager(int TotalCount, int curPage, int PageCounterLimit, int NumberfRowsInpage, string ALID)
        {
            int start = 0;
            int endn = 0;
            int Counter = 1;
            int lastpage = 0;
            //TotalRec.Text = " (<i>Total Records: " & TotalCount & ")</i>"
            if ((TotalCount / NumberfRowsInpage) == (TotalCount / NumberfRowsInpage))
            {
                lastpage = TotalCount / NumberfRowsInpage;
            }
            else
            {
                lastpage = (TotalCount / NumberfRowsInpage) + 1;
            }


            if (curPage + PageCounterLimit >= lastpage)
            {
                // records are few and less then pagecounter limit
                if (lastpage <= PageCounterLimit)
                {
                    start = 1;
                    // Counter = start
                    endn = lastpage;
                }
                else
                {
                    start = lastpage - PageCounterLimit;
                    // Counter = start
                    endn = lastpage;
                }

            }
            else
            {
                //------------ for shifting sloat wize demine the start of a sloat
                //
                int Sloat = 0;
                if ((curPage / PageCounterLimit) == (curPage / PageCounterLimit))
                {
                    Sloat = (curPage / PageCounterLimit);
                    if (Sloat == 0)
                    {
                        Sloat = 1;
                    }
                }
                else
                {
                    Sloat = (curPage / PageCounterLimit) + 1;
                }

                start = (Sloat * PageCounterLimit) - (PageCounterLimit - 1);




                // start = curPage
                // Counter = curPage
                endn = start + PageCounterLimit;
            }


            string tmpString = "";

            string curQueryString = "?";




            for (int i = start; i <= endn; i += 1)
            {
                if (i == curPage)
                {
                    tmpString = tmpString + "<Div class='PagerCurPage'>" + i.ToString() + "</Div>";
                }
                else
                {
                    tmpString = tmpString + "<Div class='PagerPageLink'><a href='" + curQueryString + "Page=" + i.ToString() + "&id=" + ALID + "'>" + i.ToString() + "</a></Div>";
                }
                //  Counter = Counter + 1

            }
            if (curPage > 1)
            {
                tmpString = "<Div class='PagerPrevious'><a href='" + curQueryString + "Page=" + (curPage - 1).ToString() + "&id=" + ALID + "'><img src='/images/Pre.png' border=0 /></a></Div>" + tmpString;
                tmpString = "<Div class='Pagerfirst'><a href='" + curQueryString + "Page=1&id=" + ALID + "'><img src='/images/First.png' border=0 /> </a></Div>" + tmpString;
            }
            else
            {
                tmpString = "<Div class='PagerPreviousDisabled'><img src='/images/Pre.png' border=0 /></Div>" + tmpString;
                tmpString = "<Div class='PagerfirstDisabled'><img src='/images/First.png' border=0 /></Div>" + tmpString;
            }
            if (!(curPage == lastpage))
            {
                tmpString = tmpString + "<Div class='PagerNext'><a href='" + curQueryString + "Page=" + (curPage + 1).ToString() + "&id=" + ALID + "'><img src='/images/Next.png' border=0 /></a></Div>";
                tmpString = tmpString + "<Div class='PagerNext'><a href='" + curQueryString + "Page=" + lastpage + "&id=" + ALID + "'> <img src='/images/Last.png' border=0 /> </a></Div>";
            }
            else
            {
                tmpString = tmpString + "<Div class='PagerNextDisabled'><img src='/images/Next.png' border=0 /></Div>";
                tmpString = tmpString + "<Div class='PagerNextDisabled'><img src='/images/Last.png' border=0 /> </Div>";
            }
            return tmpString;

        }
    }

}
