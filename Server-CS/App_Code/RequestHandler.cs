using System.Collections.Specialized;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;

public class RequestHandler
{
    string Command = "";
    int CategoryNumber = 0;
    int SubCatNumber = 0;
    delegate string DelegateCommandHandler();
    NameValueCollection QueryString;
    Dictionary<string, DelegateCommandHandler> Commands = new Dictionary<string, DelegateCommandHandler>();

    public RequestHandler(NameValueCollection QueryString)
    {

        this.QueryString = QueryString;
    }

    public string GetResponseString()
    {
        InitCommands();
        QueryStringParser();      
        DelegateCommandHandler CommandHandler;
        if (Commands.ContainsKey(Command))
        {
            CommandHandler = Commands[Command];
            return CommandHandler();
        }

        return "Wrong command";
    }

    void InitCommands()
    {
        Commands.Add("getCategoriesList", GetCatsPropsList);
        Commands.Add("getCurrentCategory", GetCurrentCategory);
    }

    void QueryStringParser()
    {
        //Устанавливаем значение по-умолчанию        
        Command = QueryString.Get("Command");
        if (Command == null) Command = "default";
        int.TryParse(QueryString.Get("cat"), out CategoryNumber);
        int.TryParse(QueryString.Get("subCat"), out SubCatNumber);
    }

    private string GetCatsPropsList()
    {
        CatsPropsList CatsPropsList = new CatsPropsList();
        return CatsPropsList.GetCatsPropsList();
    }

    private string GetCurrentCategory()
    {
        SubCatsPropsList SubCatsPropsList = new SubCatsPropsList(CategoryNumber);
        return SubCatsPropsList.GetSubCatsPropsList;
    }
}