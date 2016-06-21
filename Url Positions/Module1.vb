Imports System.IO
Imports System.Text.RegularExpressions

Module Module1

 Sub Main()

  Dim crawlfilepath As String
  crawlfilepath = "C:\Users\sestens\Downloads\project\WebCrawlCompletedMar9_2016.txt"
  Dim text As String = File.ReadAllText(crawlfilepath) 'read the craw file
  Dim taglesstxt As String = StripTags(text) 'remove tags from the crawl
  Dim allUrlsRegex As New Regex("((https?|ftp|file)\://|www.)[A-Za-z0-9\.\-]+(/[A-Za-z0-9\?\&\=;\+!'\(\)\*\-\._~%]*)*", RegexOptions.IgnoreCase) 'regex to match urls
  Dim vi, i, findex, lindex, lineFeedsCount As Integer
  Dim url As String = allUrlsRegex.Match(text).ToString
  Dim textcopy As String = text 'make a copy of the crawl file
  i = 1
  findex = text.IndexOf(url)
  lindex = url.Length + findex - 1
  Console.WriteLine(i & " match: " & url & " position:" & findex & "-" & lindex & " date:" & Today & " time:" & TimeOfDay)
  lineFeedsCount = countLf(text) 'count # of line feed characters which identify number of urls
  While i <> lineFeedsCount
   vi = text.IndexOf(vbLf)
   url = allUrlsRegex.Match(text, vi).ToString
   text = text.Substring(vi + 1)
   i = i + 1
   findex = textcopy.IndexOf(url)
   lindex = url.Length + findex - 1
   Console.WriteLine(i & " match: " & url & " position:" & findex & "-" & lindex & " date:" & Today & " time: " & TimeOfDay) 'output into console the url and its details
  End While
  Console.ReadLine()
  Dim txtcopy2 As String = textcopy
  Dim line As String
  Dim keep, discard As StreamWriter
  Dim stopWords As StreamReader
  keep = New StreamWriter(File.Create("C:\Users\sestens\Downloads\project\keep.txt")) 'create a keep words file
  discard = New StreamWriter(File.Create("C:\Users\sestens\Downloads\project\discard.txt")) 'create a discard words file
  stopWords = File.OpenText("C:\Users\sestens\Downloads\project\stopwords_en.txt") 'open the stopwords file
  Dim lineregex As Regex
  Dim matches As MatchCollection
  Do While stopWords.Peek >= 0
   line = stopWords.ReadLine() 'read a stopword from file
   lineregex = New Regex("\b" & line & "\b")
   matches = lineregex.Matches(taglesstxt)
   For Each match As Match In matches
    discard.WriteLine(match.ToString & " length:" & match.ToString.Length & " start:" & textcopy.IndexOf(match.ToString) & " date:" & Today & " time:" & TimeOfDay) 'start position relative to textcopy
   Next
   taglesstxt = Regex.Replace(taglesstxt, "\b" & line & "\b", " ") 'replace stopwords with a blank space
  Loop

  Dim wordregex As New Regex("\b(\S)+\b") 'regex to match words in strings
  matches = wordregex.Matches(taglesstxt)
  For Each match As Match In matches
   'write word in file
   keep.WriteLine(match.ToString & vbTab & vbTab & "length:" & match.ToString.Length & vbTab & "start:" & textcopy.IndexOf(match.ToString) & vbTab & "date:" & Today & vbTab & vbTab & "time: " & TimeOfDay) 'start position relative to textcopy
  Next

  'close text files
  keep.Close()
  discard.Close()
  stopWords.Close()


 End Sub

 Function StripTags(ByVal html As String) As String
  ' Remove HTML, javascript and css.
  html = Regex.Replace(html, "<.*?>", "") 'removes html
  html = Regex.Replace(html, "\<[^\>]*\>", "")
  html = Regex.Replace(html, "-.*?>", "")
  html = Regex.Replace(html, " [ ] ", "")
  html = Regex.Replace(html, "{.*?}", "")
  html = Regex.Replace(html, """", "")
  html = Regex.Replace(html, "\|", "")
  html = Regex.Replace(html, "\)", "")
  html = Regex.Replace(html, "\;", "")
  html = Regex.Replace(html, "\(", "")
  html = Regex.Replace(html, "\?", "")
  html = Regex.Replace(html, "\+", "")
  html = Regex.Replace(html, "\=", "")
  html = Regex.Replace(html, "\%", "")
  html = Regex.Replace(html, "\&", "")
  html = Regex.Replace(html, "\}", "")
  html = Regex.Replace(html, "\(.*?\)", "")
  html = Regex.Replace(html, "<style[^>]*>[\s\S]*?</style>", "") 'removes css
  html = Regex.Replace(html, "<script.*?</script>", "", RegexOptions.IgnoreCase Or RegexOptions.Singleline) 'removes javasacript, using second regex found in beansofsoftware
  html = Regex.Replace(html, "<!--[\s\S]*?-->", "")
  html = Regex.Replace(html, "<[^>]*>", "")
  Return html
 End Function

 Function countLf(text As String)
  Dim lf As New Regex(vbLf)
  Dim count As Integer = 0
  For Each i As Match In lf.Matches(text)
   count = count + 1
  Next

  Return count
 End Function
End Module
