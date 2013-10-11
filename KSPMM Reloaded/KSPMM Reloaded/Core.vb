Public Class Core
    Sub New()
#If DEBUG Then
        LoadPlugin(KSPMM_Reloaded.Internal.Core.Plugin) 'To see if it will make it work
#End If
        For Each plug As IPlugin In Internal.Internal.ListOfInternalPlugins
            LoadPlugin(plug)
        Next
    End Sub
    Friend Sub LoadPlugin(ByVal Plugin As IPlugin)
        Dim tab As New TabPage()
        tab.Controls.Add(Plugin.Control)
        tab.Controls(0).Dock = DockStyle.Fill
        tab.AutoScroll = True
        Main.tabctrlMain.TabPages.Add(tab)
    End Sub
End Class
Public Interface IPlugin
    Property Name As String
    Property Version As Version
    Property Description As String
    Property Control As UserControl
End Interface
