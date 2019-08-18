using System;
using System.Web.UI;
using System.Collections;

public partial class Server : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Debug.WriteLine("Hello");
        RequestHandler RequestHandler = new RequestHandler(Request.QueryString);
        Response.AppendHeader("Access-Control-Allow-Origin", "*");
        string ResponseString = RequestHandler.GetResponseString();

        Response.Write(ResponseString);
    }
}