public class CatsPropsList : Category
{
    public CatsPropsList()
    {
        
    }

    public string GetCatsPropsList()
    {
        string[] PropsList = new string[EntriesCount];
        for (int Index=0; Index < EntriesCount; Index++)
        {
            PropsList[Index] = GetCategoryProps(Index);
        }
        return string.Join("&", PropsList);
    }
}