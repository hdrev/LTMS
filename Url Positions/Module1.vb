Imports System.IO
Imports System.Text.RegularExpressions

Module Module1

 Sub Main()

  Dim crawlfilepath As String
  crawlfilepath = "C:\Users\sestens\Downloads\project\WebCrawlCompletedMar9_2016.txt"
  Dim text As String = File.ReadAllText(crawlfilepath)
  Dim taglesstxt As String = StripTags(text) 'remove tags from the crawl
  Dim allUrlsRegex As New Regex("((https?|ftp|file)\://|www.)[A-Za-z0-9\.\-]+(/[A-Za-z0-9\?\&\=;\+!'\(\)\*\-\._~%]*)*", RegexOptions.IgnoreCase)
  Dim vi, i, findex, lindex, lineFeedsCount As Integer
  Dim url As String = allUrlsRegex.Match(text).ToString
  Dim textcopy As String = text
  i = 1
  findex = text.IndexOf(url)
  lindex = url.Length + findex - 1
  Console.WriteLine(i & " match: " & url & " position:" & findex & "-" & lindex & " date:" & Today & " time:" & TimeOfDay)
  lineFeedsCount = countLf(text)
  While i <> lineFeedsCount
   vi = text.IndexOf(vbLf)
   url = allUrlsRegex.Match(text, vi).ToString
   text = text.Substring(vi + 1)
   i = i + 1
   findex = textcopy.IndexOf(url)
   lindex = url.Length + findex - 1
   Console.WriteLine(i & " match: " & url & " position:" & findex & "-" & lindex & " date:" & Today & " time: " & TimeOfDay)
  End While
  Console.ReadLine()
  Dim txtcopy2 As String = textcopy
  Dim keep, discard As StreamWriter
  Dim stopWords As StreamReader
  'keep = New StreamWriter(File.Create("C:\Users\sestens\Downloads\project\keep.txt"))
  'discard = New StreamWriter(File.Create("C:\Users\sestens\Downloads\project\discard.txt"))
  stopWords = File.OpenText("C:\Users\sestens\Downloads\project\stopwords_en.txt")
  Do While stopWords.Peek >= 0
   taglesstxt = Regex.Replace(taglesstxt, "\b" & stopWords.ReadLine() & "\b", " ") 'remove stopwords
  Loop

  Console.Write(taglesstxt)
  Console.ReadLine()


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
