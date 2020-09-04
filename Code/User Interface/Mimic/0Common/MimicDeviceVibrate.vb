Public Class MimicDeviceVibrate : Inherits MimicDeviceLeftRight
  Public Sub New()
    ' Set images for vibrate
    'ImageLeftOff = My.Resources.MimicVibrateOff
    'ImageLeftOn = My.Resources.MimicVibrateOn
    'ImageLeftFault = My.Resources.MimicVibrateOn

    'ImageRightOff = My.Resources.MimicVibrateOff
    'ImageRightOn = My.Resources.MimicVibrateOn
    'ImageRightFault = My.Resources.MimicVibrateOn

    ' Set default values 
    Me.Orientation = EOrientation.Left
    Me.Name = "Vibrate"
    Me.Size = ImageLeftOff.Size

    ' Add a context menu for valve overrides
    ContextMenu = New ContextMenu
    ContextMenu.MenuItems.Add("Start Vibrate", Sub() MenuStart())
    ContextMenu.MenuItems.Add("Stop Vibrate", Sub() MenuStop())
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
