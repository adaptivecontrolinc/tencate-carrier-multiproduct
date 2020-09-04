Public Class MimicDeviceAuger : Inherits MimicDeviceLeftRight
  Public Sub New()
    ' Set images for vibrate
    'ImageLeftOff = My.Resources.MimicAugerLeftOff
    'ImageLeftOn = My.Resources.MimicAugerLeftOn
    'ImageLeftFault = My.Resources.MimicAugerLeftOn

    'ImageRightOff = My.Resources.MimicAugerRightOff
    'ImageRightOn = My.Resources.MimicAugerRightOn
    'ImageRightFault = My.Resources.MimicAugerRightOn

    ' Set default values 
    Me.Orientation = EOrientation.Left
    Me.Name = "Auger"
    Me.Size = ImageLeftOff.Size

    ' Add a context menu for valve overrides
    ContextMenu = New ContextMenu
    ContextMenu.MenuItems.Add("Start Auger", Sub() MenuStart())
    ContextMenu.MenuItems.Add("Stop Auger", Sub() MenuStop())
    ContextMenu.MenuItems.Add("-")
  End Sub

  Private Sub MenuStart()
    If NormallyOn Then
      Value = False
    Else
      Value = True
    End If
  End Sub

  Private Sub MenuStop()
    If NormallyOn Then
      Value = True
    Else
      Value = False
    End If
  End Sub

End Class
