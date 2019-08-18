using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Category
{
    DatabaseConnect Database = new DatabaseConnect();
    const int FieldsCount = 9;
    const int pName = 0;
    const int pCatNumber = 1;
    const int pSubCatNumber = 2;
    const int pCaption = 3;
    const int pDescription = 4;
    const int pViewed = 5;
    const int pIsPhotoAlbum = 6;    
    const int pIsArticle = 7;
    const int pIsTileGrid = 8;

    string EntrySource = "";
   
    public Category(int CategoryNumber)
    {
        EntrySource = GetNameCategory(CategoryNumber);
    }

    public Category()
    {
        EntrySource = Utilites.CategoryTable;
    }

    public string GetCategoryProps(int CatNumber, int Number = 0)
    {
        string[] CatsPropsList = new string[FieldsCount];
        string Name = GetNameCategory(CatNumber);
        CatsPropsList[pName] = Name;
        CatsPropsList[pCatNumber] = CatNumber.ToString();
        CatsPropsList[pSubCatNumber] = Number.ToString();
        CatsPropsList[pCaption] = Database.GetItemByID(Name, Number, "Caption");
        CatsPropsList[pIsTileGrid] = Database.GetItemByID(Name, Number, "IsTileGrid");
        CatsPropsList[pIsPhotoAlbum] = Database.GetItemByID(Name, Number, "IsPhotoAlbum");
        CatsPropsList[pDescription] = Database.GetItemByID(Name, Number, "Description");
        CatsPropsList[pIsArticle] = Database.GetItemByID(Name, Number, "IsArticle");
        CatsPropsList[pViewed] = Database.GetItemByID(Name, Number, "Viewed");
        return string.Join(";", CatsPropsList);
    }

    private string GetNameCategory(int Index)
    {
        return Database.GetItemByID(Utilites.CategoryTable, Index, "Name");
    }    
    
    public int EntriesCount
    {
        get
        {
            return Database.GetCountItems(EntrySource);
        }     
    }
}