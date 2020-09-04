Public Class Mimic

  Public Property ControlCode As ControlCode

  Private ReadOnly Property Remoted() As Boolean
    Get
      Return Runtime.Remoting.RemotingServices.IsTransparentProxy(ControlCode)
    End Get
  End Property

  Public Sub New()
    ' This call is required by the designer.
    InitializeComponent()

    Me.DoubleBuffered = True
    Me.AutoScaleMode = Windows.Forms.AutoScaleMode.None

    ' Add any initialization after the InitializeComponent() call.
  End Sub

  Public Sub OnControlCodeRefreshed()
    If ControlCode Is Nothing Then Exit Sub
    pageDistributor.ControlCode = ControlCode
    pageQueue.ControlCode = ControlCode
    pageDistributor.UpdateMimic()
    pageQueue.UpdateMimic()
  End Sub

  Private Sub toolStripMain_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolStripMain.ItemClicked
    ' If they haven't clicked on a button do nothing
    If Not (TypeOf e.ClickedItem Is ToolStripButton) Then Exit Sub

    ' Set checked state - only allow one checked at a time
    SetChecked(DirectCast(e.ClickedItem, ToolStripButton))

    ' Crikey
    SetMimicPage("page" & e.ClickedItem.Name.Substring(15))            ' toolStripButton
  End Sub

  Private Sub SetChecked(ByVal button As ToolStripButton)
    ' Uncheck all buttons 
    Dim tb As ToolStripButton
    For Each item As ToolStripItem In toolStripMain.Items
      If TypeOf item Is ToolStripButton Then
        tb = DirectCast(item, ToolStripButton)
        tb.Checked = False
        tb.CheckState = CheckState.Unchecked
      End If
    Next
  End Sub

  Private Sub SetMimicPage(pageName As String)
    ShowPage(pageName)
    Me.ActiveControl = toolStripMain
    toolStripMain.Focus()
  End Sub

  Public Sub ShowPage(pageName As String)
    For Each item In Me.Controls
      If TypeOf item Is UserControl Then
        Dim page = DirectCast(item, UserControl)
        If page.Name = pageName Then
          page.Visible = True
        Else
          page.Visible = False
        End If
      End If
    Next

  End Sub

End Class
