Imports MySql.Data.MySqlClient
Imports System.Text.Json
Public Class JSON
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim WebClient As New Net.WebClient
        WebClient.Encoding = Encoding.UTF8
        Dim url As String = "https://tcgbusfs.blob.core.windows.net/dotapp/youbike/v2/youbike_immediate.json"
        Dim response As String = WebClient.DownloadString(url)

        Dim data As 自行車即時資訊() = JsonSerializer.Deserialize(Of 自行車即時資訊())(response)
        Dim conn As New MySqlConnection
        Dim myConnectionString As String
        myConnectionString = "server=127.0.0.1;uid=root;pwd=;database=taipeibicycle;CharSet=utf8"

        Try
            conn.ConnectionString = myConnectionString
            conn.Open()
        Catch ex As MySqlException
            Label1.Text = ex.Message
        End Try

        Dim cmd As New MySqlCommand
        cmd.Connection = conn
        cmd.CommandText = "TRUNCATE TABLE `自行車即時資訊` "
        cmd.ExecuteNonQuery()

        For i As Integer = 0 To data.Length - 1
            cmd.CommandText = "INSERT INTO `自行車即時資訊` (`sno`, `sna`, `tot`, `sbi`, `sarea`, `mday`, `lat`, `Ing`, `ar`, `sareaen`, `snaen`, `aren`, `bemp`, `act`, `srcUpdateTime`, `updateTime`, `infoTime`, `infoDate`) VALUES ('" & data(i).sno & "', '" & data(i).sna & "', " & data(i).tot & ", " & data(i).sbi & ", '" & data(i).sarea & "', '" & data(i).mday & "', " & data(i).lat & ", " & data(i).Ing & ", '" & data(i).ar & "', '" & data(i).sareaen & "', '" & data(i).snaen.Replace(Chr(39), "") & "', '" & data(i).aren.Replace(Chr(39), "") & "', " & data(i).bemp & ", '" & data(i).act & "', '" & data(i).srcUpdateTime & "', '" & data(i).updateTime & "', '" & data(i).infoTime & "', '" & data(i).infoDate & "')"
            cmd.ExecuteNonQuery()
        Next
        cmd.Dispose()
        conn.Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim WebClient As New Net.WebClient
        Dim url As String = "https://data.taipei/api/frontstage/tpeod/dataset/resource.download?rid=87f1d055-1289-4f01-80b4-bdfd9a1f692f"
        Dim response As String = WebClient.DownloadString(url)
        Dim Rows As String() = response.Split(vbCrLf) 'LF(\n), CRLF(\r\n)

        'Connect MySql
        Dim conn As New MySqlConnection
        Dim myConnectionString As String

        myConnectionString = "server=127.0.0.1;uid=root;pwd=;database=taipeibicycle;CharSet=utf8"

        Try
            conn.ConnectionString = myConnectionString
            conn.Open()
        Catch ex As MySqlException
            Label1.Text = ex.Message
        End Try
        Dim Cells As String()

        Dim cmd As New MySqlCommand
        cmd.Connection = conn
        cmd.CommandText = "TRUNCATE `車道景點`"
        cmd.ExecuteNonQuery()

        For i As Integer = 1 To Rows.Length - 2
            Cells = Rows(i).Replace("""", "").Split(",")
            cmd.CommandText = "INSERT INTO `車道景點` (`ID`, `riverside_park`, `scenic_spot`, `latitude`, `longitude`, `description`) VALUES (NULL, '" & Cells(0) & "', '" & Cells(1) & "', '" & Cells(2) & "', '" & Cells(3) & "', '" & Cells(4) & "')"
            cmd.ExecuteNonQuery()
        Next

        cmd.Dispose()
        conn.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim WebClient As New Net.WebClient
        Dim url As String = "https://data.taipei/api/frontstage/tpeod/dataset/resource.download?rid=9a0ad3e0-9d5f-4c4b-9ab1-28bd6ad2d357"
        Dim response As String = WebClient.DownloadString(url)
        Dim Rows As String() = response.Split(vbCrLf) 'LF(\n), CRLF(\r\n)

        'Connect MySql
        Dim conn As New MySqlConnection
        Dim myConnectionString As String

        myConnectionString = "server=127.0.0.1;uid=root;pwd=;database=taipeibicycle;CharSet=utf8"

        Try
            conn.ConnectionString = myConnectionString
            conn.Open()
        Catch ex As MySqlException
            Label1.Text = ex.Message
        End Try
        Dim Cells As String()

        Dim cmd As New MySqlCommand
        cmd.Connection = conn
        cmd.CommandText = "TRUNCATE `年度美食`"
        cmd.ExecuteNonQuery()

        For i As Integer = 1 To Rows.Length - 2
            Cells = Rows(i).Replace("""", "").Split(",")
            cmd.CommandText = "INSERT INTO `年度美食` (`序號`, `區域`, `店家`, `報名組別`, `營業地址`, `longitude`, `latitude`) VALUES (" & Cells(0) & ", '" & Cells(1) & "', '" & Cells(2) & "', '" & Cells(3) & "', '" & Cells(4) & "', " & Cells(5) & ", " & Cells(6) & ")"
            cmd.ExecuteNonQuery()
        Next

        cmd.Dispose()
        conn.Close()
    End Sub
End Class

Class 自行車即時資訊
    Property sno As String
    Property sna As String
    Property tot As Integer
    Property sbi As Integer
    Property sarea As String
    Property mday As String
    Property lat As Double
    Property Ing As Double
    Property ar As String
    Property sareaen As String
    Property snaen As String
    Property aren As String
    Property bemp As Integer
    Property act As String
    Property srcUpdateTime As String
    Property updateTime As String
    Property infoTime As String
    Property infoDate As String

    Sub New(sno As String, sna As String, tot As Integer, sbi As Integer, sarea As String, mday As String, lat As Double, Ing As Double, ar As String, sareaen As String, snaen As String, aren As String, bemp As Integer, act As String, srcUpdateTime As String, updateTime As String, infoTime As String, infoDate As String)
        Me.sno = sno
        Me.sna = sna
        Me.tot = tot
        Me.sbi = sbi
        Me.sarea = sarea
        Me.mday = mday
        Me.lat = lat
        Me.Ing = Ing
        Me.ar = ar
        Me.sareaen = sareaen
        Me.snaen = snaen
        Me.aren = aren
        Me.bemp = bemp
        Me.act = act
        Me.srcUpdateTime = srcUpdateTime
        Me.updateTime = updateTime
        Me.infoTime = infoTime
        Me.infoDate = infoDate
    End Sub
End Class

Class 年度美食
    Sub New()

    End Sub
End Class
