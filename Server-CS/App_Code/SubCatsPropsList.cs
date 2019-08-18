using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class SubCatsPropsList : Category
{
    private readonly int CategoryNumber = 0;

    public SubCatsPropsList(int CategoryNumber) : base (CategoryNumber)
    {
        this.CategoryNumber = CategoryNumber;
    }

    public string GetSubCatsPropsList
    {
        get
        {
            string[] PropsList = new string[EntriesCount];
            PropsList[0] = GetCategoryProps(CategoryNumber);
            for (int Index = 1; Index < EntriesCount; Index++)
            {
                PropsList[Index] = GetCategoryProps(CategoryNumber, Index);
            }
            return string.Join("&", PropsList);
        }
    }
}