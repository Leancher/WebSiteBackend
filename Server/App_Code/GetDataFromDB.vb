Imports System.Diagnostics
Imports Microsoft.VisualBasic
Public Class GetDataFromDB
    Private Database As New DatabaseConnect()
    Sub New()

    End Sub
    Public Function GetCategoryProp(Optional CatNum As Integer = 0, Optional SubCatNum As Integer = 0) As String
        Dim Name = GetCategoriesNameList()(CatNum)
        Dim Caption = GetCaptionCategory(Name, SubCatNum)
        Dim Description = GetDescriptionCategory(Name, SubCatNum)
        Dim Viewed = GetViewedCategory(Name, SubCatNum)
        Dim IsPhotoAlbum = GetIsPhotoAlbumCategory(Name, SubCatNum)
        Dim IsArticle = GetIsArticleCategory(Name, SubCatNum)
        Dim IsTileGrid = GetIsTileGridCategory(Name, SubCatNum)
        Return Name + ";" + CatNum.ToString + ";" + SubCatNum.ToString + ";" + Caption + ";" + Description + ";" + Viewed + ";" + IsPhotoAlbum + ";" + IsArticle + ";" + IsTileGrid
    End Function
    Function GetCountItems(CategoryName As String) As Integer
        Return Database.GetCountItems(CategoryName)
    End Function
    Public Function GetSubCategoriesCount(CategoryName As String) As Integer
        Return GetCountItems(CategoryName)
    End Function
    Public Function GetCategoriesNameList() As String()
        Dim ArrayCategoriesName(GetCountItems(Config.CategoryTable) - 1) As String
        Return ArrayCategoriesName.Select(Function(item, index) GetNameItem(index)).ToArray
    End Function
    Function GetNameItem(Index As Integer) As String
        Return Database.GetItemByID(Config.CategoryTable, Index, "Name")
    End Function
    Function GetCaptionCategory(CategoryName As String, Index As Integer) As String
        Return Database.GetItemByID(CategoryName, Index, "Caption")
    End Function
    Function GetIsTileGridCategory(CategoryName As String, Index As Integer) As String
        Return Database.GetItemByID(CategoryName, Index, "IsTileGrid")
    End Function
    Function GetIsPhotoAlbumCategory(CategoryName As String, Index As Integer) As String
        Return Database.GetItemByID(CategoryName, Index, "IsPhotoAlbum")
    End Function
    Function GetDescriptionCategory(CategoryName As String, Index As Integer) As String
        Return Database.GetItemByID(CategoryName, Index, "Description")
    End Function
    Function GetIsArticleCategory(CategoryName As String, Index As Integer) As String
        Return Database.GetItemByID(CategoryName, Index, "IsArticle")
    End Function
    Function GetViewedCategory(CategoryName As String, Index As Integer) As String
        Return Database.GetItemByID(CategoryName, Index, "Viewed")
    End Function
    Function GetItemByNumber(CatNum As Integer, SubCatNum As Integer, PropName As String) As String
        Return Database.GetItemByID(CatNum, SubCatNum, PropName)
    End Function
End Class
