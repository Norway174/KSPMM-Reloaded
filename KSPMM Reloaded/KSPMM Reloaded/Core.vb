Public Class Core
    Public Plugins As New List(Of IPlugin)
    Sub New(ByRef TabControl As TabControl)
        For Each plug As IPlugin In Internal.Core.ListOfInternalPlugins
            LoadPlugin(plug, TabControl)
            Plugins.Add(plug)
        Next
    End Sub
    Public Sub LoadPlugin(ByVal Plugin As IPlugin, ByRef TabControl As TabControl)
        Dim tab As New TabPage()
        tab.Controls.Add(Plugin.Control)
        tab.Controls(0).Dock = DockStyle.Fill
        tab.AutoScroll = True
        tab.Text = Plugin.Name
        Main.tabctrlMain.TabPages.Add(tab)
    End Sub
End Class
Public Interface IPlugin
    Property Name As String
    Property Version As Version
    Property Description As String
    Property Control As UserControl
End Interface
Public Class Plugin
    Implements IPlugin
    Public Property Control As UserControl Implements IPlugin.Control

    Public Property Description As String Implements IPlugin.Description

    Public Property Name As String Implements IPlugin.Name

    Public Property Version As Version Implements IPlugin.Version
End Class
Public MustInherit Class ClassLibrary

End Class
