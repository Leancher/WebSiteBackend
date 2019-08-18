Public Class Config
    Public Shared WebPath As String = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)
    Public Shared SiteName As String = "myleancher.ru"
    Public Shared AppPath As String = AppDomain.CurrentDomain.BaseDirectory
    Public Shared DefaultPage As String = "/Default.aspx"
    Public Shared PageFolder As String = "Page/"
    Public Shared PicturesFolder As String = "Pictures"
    Public Shared PreviewFolder As String = "Pictures/Preview"
    Public Shared ContentPhotoFolder As String = "Pictures/Content"
    Public Shared ShowError As String = ""
    Public Shared CategoryTable As String = "CategoryList"
    Public Shared CategoryMain As String = "Main"
    Public Shared SiteTitle As String = "Leancher web site"

    Public Shared Function GetAppPath() As String
        Dim dirInfo As New System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)
        Return dirInfo.Parent.FullName
    End Function
End Class
