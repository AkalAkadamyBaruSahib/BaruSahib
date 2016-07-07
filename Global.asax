<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }

    protected void Application_Error(Object sender, EventArgs e)
    {

        //handle all error logging here - then the custom error from web.config

        //will send the user to the friendly error page.

        Exception ex;

        if (Server.GetLastError().InnerException != null)
        {
            ex = Server.GetLastError().InnerException;
        }
        else
        {
            ex = Server.GetLastError();
        }

        DAL.DalAccessUtility.ExecuteNonQuery("INSERT INTO ERRORLOG VALUES (" + ex.InnerException + ",'" + ex.StackTrace + "',GETDATE())"); ;
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
