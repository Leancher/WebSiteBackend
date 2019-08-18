Imports System.Windows.Media.Imaging
Imports System.IO
Imports System.Diagnostics

Partial Class Server
    Inherits Page
    Private Database As New DatabaseConnect()
    Private GetDataFromDB As New GetDataFromDB()
    Private CatNumber As String
    Private SubCatNumber As String
    Private AlbumID As String
    Private Sub Server_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim Command As String = Request.QueryString("Command")
        CatNumber = Request.QueryString("cat")
        SubCatNumber = Request.QueryString("subCat")
        AlbumID = Request.QueryString("album")
        Dim ResponseString As String = ""
        'Для возможности отправки с другого сайта
        Response.AppendHeader("Access-Control-Allow-Origin", "*")
        Select Case Command
            Case "TestCommand"
                ResponseString = "TestResponse"
            Case "getCategoriesList"
                ResponseString = GetCategoriesList()
            Case "getCurrentCategory"
                ResponseString = GetCategory(Val(CatNumber))
            Case "GetCountView"
                ResponseString = GetCountView()
            Case "getPhotosList"
                ResponseString = GetPhotosList()
            Case "DescriptionPhoto"
                ResponseString = GetDataFromExif()
            Case "getCountView"
                ResponseString = GetCountView()
            Case "getNotesPreview"
                ResponseString = GetNotesPreview()
            Case "getSingleNote"
                ResponseString = GetSingleNote()
        End Select
        Response.Write(ResponseString)
    End Sub
    Private Function GetCategoriesList() As String
        Dim NameList As String() = GetDataFromDB.GetCategoriesNameList()
        For Index = 0 To NameList.Count - 1
            Dim Name = NameList(Index)
            NameList(Index) = GetDataFromDB.GetCategoryProp(Index)
        Next Index
        Return String.Join("&", NameList)
    End Function
    Private Function GetCategory(CategoryNumber As Integer) As String
        Dim CatName = GetDataFromDB.GetCategoriesNameList()(CategoryNumber)
        Dim SubCatCount As Integer = GetDataFromDB.GetSubCategoriesCount(CatName)
        Dim ArrayItems(SubCatCount - 1) As String
        ArrayItems(0) = GetDataFromDB.GetCategoryProp(CategoryNumber)
        If SubCatCount > 1 Then
            For Index = 1 To SubCatCount - 1
                ArrayItems(Index) = GetDataFromDB.GetCategoryProp(CategoryNumber, Index)
            Next Index
        End If
        Return String.Join("&", ArrayItems)
    End Function
    Private Function GetCountView() As String
        Dim NameList As String() = GetDataFromDB.GetCategoriesNameList()
        Dim MainArray(NameList.Count - 1) As String
        For Index = 0 To NameList.Count - 1
            MainArray(Index) = GetCategory(Index)
        Next Index
        Return String.Join("&", MainArray)
    End Function
    Private Function GetPhotosList() As String
        Dim CatName = GetDataFromDB.GetCategoriesNameList()(CatNumber)
        Dim PhotoPath As String = Config.GetAppPath() + "\public\Pictures\" + CatName + "\Album" + SubCatNumber + "Preview"
        If Not Directory.Exists(PhotoPath) Then Directory.CreateDirectory(PhotoPath)
        Dim ListPhoto As String() = Directory.GetFiles(PhotoPath, "*.jpg").Select(Function(item) Path.GetFileName(item)).ToArray
        Return String.Join("&", ListPhoto)
    End Function

    Private Function GetNotesPreview() As String
        Database.DatabaseOpen()
        Dim TableName = "MyNotes"
        Dim CountItems = Database.GetCountItems(TableName)
        Dim ArrayItems(CountItems) As String
        For Index = 1 To CountItems
            Dim Path = Config.GetAppPath() + "\src\Content" + "\" + "MyNote" + Index.ToString + ".txt"
            Dim FileInfo As New FileInfo(Path)
            If FileInfo.Exists = True Then
                Dim Caption = Database.GetItemByID(TableName, Index, "Caption")
                Using reader As New StreamReader(Path)
                    ArrayItems(Index) = Caption + ";" + Left(reader.ReadToEnd(), 300)
                End Using
            End If
        Next Index
        Database.DatabaseClose()
        Return String.Join("&", ArrayItems)
    End Function
    Private Function GetSingleNote() As String
        Dim NumNote = Request.QueryString("note")
        Dim Path = Config.GetAppPath() + "\src\Content" + "\" + "MyNote" + NumNote.ToString + ".txt"
        Dim FileInfo As New FileInfo(Path)
        If FileInfo.Exists = True Then
            Using reader As New StreamReader(Path)
                Return reader.ReadToEnd()
            End Using
        End If
        Return ""
    End Function

    Private Function GetDataFromExif() As String
        Dim Path As String = Config.AppPath + "Pictures\" + CatNumber + "\Album" + AlbumID
        Dim ResponseString As String = ""
        Try
            Dim ListPhoto As String() = Directory.GetFiles(Path)
            For Each Photo In ListPhoto
                Dim FStream As New FileStream(Photo, FileMode.Open, FileAccess.Read)
                Dim FDecoder As New JpegBitmapDecoder(FStream, BitmapCreateOptions.IgnoreColorProfile, BitmapCacheOption.Default)
                Dim Metadata As New BitmapMetadata("jpg")
                Metadata = FDecoder.Frames(0).Metadata.Clone()
                ResponseString = ResponseString + ";" + Metadata.Comment
                FStream.Close()
            Next Photo
        Catch ex As Exception

        End Try
        ResponseString = Right(ResponseString, ResponseString.Length - 1)
        Return ResponseString
    End Function
End Class