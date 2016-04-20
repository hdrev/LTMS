Imports System.IO
Imports System.Text.RegularExpressions

Module Module1

 Sub Main()

  Dim crawlpath As String
  crawlpath = "C:\Users\sestens\Downloads\project\WebCrawlCompletedMar9_2016.txt"
  Dim text As String = File.ReadAllText(crawlpath)
  Dim taglesstxt As String = StripTags(text)
  Dim otherRegex As New Regex("((https?|ftp|file)\://|www.)[A-Za-z0-9\.\-]+(/[A-Za-z0-9\?\&\=;\+!'\(\)\*\-\._~%]*)*", RegexOptions.IgnoreCase)
  Dim findex, lindex As Integer
  For Each m As Match In otherRegex.Matches(taglesstxt)
   findex = text.IndexOf(m.ToString)
   lindex = m.ToString.Length + findex - 1
   Console.WriteLine("match: " & m.ToString & " on position:" & findex & "-" & lindex & " date:" & Today & " time:" & TimeOfDay)
  Next
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
End Module
