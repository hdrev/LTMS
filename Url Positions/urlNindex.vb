Public Class urlNindex

 Public urlstr As String
 Public ind As Integer

 Property Url() As String
  Get
   Return urlstr
  End Get
  Set(value As String)
   urlstr = value
  End Set
 End Property

 Property Index() As Integer
  Get
   Return ind
  End Get
  Set(value As Integer)
   urlstr = value
  End Set
 End Property

End Class
