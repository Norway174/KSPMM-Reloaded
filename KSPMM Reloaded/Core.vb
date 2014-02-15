Public Class Core
    Public Plugins As New List(Of IPlugin)
    Sub New(ByRef TabControl As TabControl, ByVal PluginTypesToLoad As Plugin.PluginType)
        For Each plug As Plugin In Internal.Core.ListOfInternalPlugins
            If plug.TypeOfPlugin = PluginTypesToLoad Or PluginTypesToLoad = Plugin.PluginType.All Then
                LoadPlugin(plug, TabControl)
                Plugins.Add(plug)
            End If
        Next
    End Sub
    Public Sub LoadPlugin(ByVal _Plugin As Plugin, ByRef TabControl As TabControl)
        Select Case _Plugin.TypeOfPlugin
            Case Plugin.PluginType.TabbedUserControl
                Dim tab As New TabPage()
                tab.Controls.Add(_Plugin.Control)
                tab.Controls(0).Dock = DockStyle.Fill
                tab.AutoScroll = True
                tab.Text = _Plugin.Name
                Main.tabctrlMain.TabPages.Add(tab)
            Case Plugin.PluginType.RuntimeStartup
                _Plugin.MainDelegate.Invoke()
            Case Plugin.PluginType.MainFormStartup
                _Plugin.MainDelegate.Invoke()
        End Select
    End Sub
End Class
Public Interface IPlugin
    Delegate Sub PluginDelegate()

    Property Name As String
    Property Version As Version
    Property PluginType As Plugin.PluginType
    Property MainDelegate As PluginDelegate
    Property Description As String
    Property Control As UserControl
End Interface
Public Class Plugin
    Implements IPlugin

    Public Property Name As String Implements IPlugin.Name

    Public Property Version As Version Implements IPlugin.Version

    Public Property TypeOfPlugin As PluginType Implements IPlugin.PluginType

    Public Property Description As String Implements IPlugin.Description

    Public Property Control As UserControl Implements IPlugin.Control

    Public Enum PluginType
        All = 0
        TabbedUserControl = 1
        RuntimeStartup = 2
        MainFormStartup = 3
    End Enum

    Public Property MainDelegate As IPlugin.PluginDelegate Implements IPlugin.MainDelegate
End Class
