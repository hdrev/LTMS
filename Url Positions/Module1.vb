Imports System.IO
Imports System.Text.RegularExpressions
Imports HtmlAgilityPack

Module Module1

 Sub Main()

  Dim crawlpath As String
  crawlpath = "C:\Users\sestens\Downloads\project\WebCrawlCompletedMar9_2016.txt"
  Dim text As String = File.ReadAllText(crawlpath)
  Console.ReadLine()
  Console.WriteLine("CRAWL TEST, GET URLS")
  Console.WriteLine("first 0A appearence = " & text.IndexOf(vbLf)) '0A is the LF character that identifies an url in the crawl
  Dim otherRegex As New Regex("((https?|ftp|file)\://|www.)[A-Za-z0-9\.\-]+(/[A-Za-z0-9\?\&\=;\+!'\(\)\*\-\._~%]*)*", RegexOptions.IgnoreCase)
  Dim findex, lindex As Integer
  For Each m As Match In otherRegex.Matches(text)
   findex = text.IndexOf(m.ToString)
   lindex = m.ToString.Length + findex - 1
   Console.WriteLine("match: " & m.ToString & "on position:" & findex & "-" & lindex)
  Next



  'Dim inindex As Integer = text.IndexOf(vbLf)
  'Dim substr As String = text.Substring(inindex + 2)
  'Dim enindex As Integer = substr.IndexOf(ControlChars.Quote)
  'Dim urlstr As String = substr.Substring(0, enindex)

  'Dim begins, ends As Integer
  'begins = text.IndexOf(urlstr)
  'ends = urlstr.Length + begins - 1
  ''Console.WriteLine("url: " & urlstr & " - starts in:" & begins & " ends in:" & ends)

  'While (inindex > 0)

  ' Console.WriteLine("url: " & urlstr & " - starts in:" & begins & " ends in:" & ends)
  ' substr = substr.Substring(ends + 1) 'create a substring from the end of the url to the end of the file
  ' inindex = substr.IndexOf(vbLf) 'find the next LF character
  ' enindex = substr.IndexOf(ControlChars.Quote, 4)
  ' urlstr = substr.Substring(inindex, enindex)
  ' begins = text.IndexOf(urlstr)
  ' ends = urlstr.Length + begins - 1

  'End While

  Console.ReadLine()

 End Sub

 Function findStartEnd(ByVal text As String)

  Using outputfile As New StreamWriter("C:\Users\sestens\Downloads\project\output.txt")
   Dim hrefRegex As New Regex("<A[^>]*?HREF\s*=\s*""([^""]+)""[^>]*?>([\s\S]*?)<\/A>", RegexOptions.IgnoreCase)
   Dim otherRegex As New Regex("<a\s+href\s*=\s*""?([^"" >]+)""?>(.+)</a>", RegexOptions.IgnoreCase)
   Dim startindex, length, endindex As Integer


   For Each m As Match In otherRegex.Matches(text)
    Console.WriteLine("There was a match")
    startindex = m.Index
    length = m.Length
    endindex = length + startindex - 1
    outputfile.WriteLine("url:=" & m.ToString() & " starting position = " & startindex & " ending position = " & endindex)

   Next
  End Using
  Return 0
 End Function


End Module
