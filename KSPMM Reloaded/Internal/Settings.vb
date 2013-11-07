Namespace Internal
    Public Class Setting
        Public Property Node As TreeNode
        Public Property UserControl As UserControl

        Sub New(ByVal _Node As TreeNode, ByVal UC As UserControl)
            Node = _Node
            UserControl = UC
        End Sub
    End Class
    Public Module Settings
        Public ReadOnly Property Plugin As Plugin
            Get
                Dim p As New Plugin
                p.MainDelegate = New IPlugin.PluginDelegate(AddressOf Startup)
                p.Description = "Basic Program-wide settings"
                p.Name = "Settings"
                p.TypeOfPlugin = KSPMM_Reloaded.Plugin.PluginType.RuntimeStartup
                p.Version = New Version(1, 0, 0, 0)
                Return p
            End Get
        End Property
        Public Class InternalSetting
            Sub New(ByVal _Setting As Object(), ByVal _ID As String)
                Setting = _Setting
                ID = _ID
            End Sub
            Public Property ID As String
            Public Property Setting As Object()
        End Class
        Public Sub Startup()
            If My.Settings.Settings Is Nothing Then
                My.Settings.Settings = New ArrayList()
            End If
        End Sub
        Public Sub ChangeSettings(ByVal SettingMode As SettingMode, ByVal NewSetting As InternalSetting)
            SyncLock My.Settings.Settings
                If My.Settings.Settings Is Nothing Then My.Settings.Settings = New ArrayList
                Dim sett As New List(Of InternalSetting)
                ConvertList(LoadSettings(My.Settings.Settings), sett)
                Select Case SettingMode
                    Case Settings.SettingMode.CreateNew
                        For Each t As InternalSetting In sett
                            If t.ID = NewSetting.ID Then
                                Throw New Exception("Setting with ID already exists")
                                Exit Sub
                            End If
                        Next
                        My.Settings.Settings.Add(NewSetting)
                        My.Settings.Save()
                    Case Settings.SettingMode.Update
                        Dim i As Integer = 0
                        Dim e As Boolean = False
                        For Each t As InternalSetting In sett
                            If t.ID = NewSetting.ID Then
                                sett(i) = NewSetting
                                e = True
                                Exit For
                            End If
                            i += 1
                        Next
                        If e = False Then Throw New ArgumentException("Setting does not exist already")
                        My.Settings.Settings = UnloadSettings(sett)
                        My.Settings.Save()
                    Case Settings.SettingMode.CreateorUpdate
                        Dim i As Integer = 0
                        Dim e As Boolean = False
                        For Each t As InternalSetting In sett
                            If t.ID = NewSetting.ID Then
                                sett(i) = NewSetting
                                e = True
                                Exit For
                            End If
                            i += 1
                        Next
                        If e = False Then sett.Add(NewSetting)
                        My.Settings.Settings = UnloadSettings(sett)
                        My.Settings.Save()
                End Select
            End SyncLock
        End Sub
        Public Function ObtainSetting(ByVal ID As String) As InternalSetting
            Dim l As New List(Of InternalSetting)
            ConvertList(LoadSettings(My.Settings.Settings), l)
            For Each n As InternalSetting In l
                If n.ID = ID Then
                    Return n
                End If
            Next
            Return Nothing
        End Function
        Public Sub ConvertList(Of T, V)(ByVal List As List(Of T), ByRef NewList As List(Of V))
            For Each lel As Object In List
                NewList.Add(lel)
            Next
            Return
        End Sub
        Public Function LoadSettings(ByVal ArrayList As ArrayList) As List(Of Object)
            If My.Settings.Settings Is Nothing Then My.Settings.Settings = New ArrayList
            Dim l As New List(Of Object)
            For Each ll As Object In ArrayList
                l.Add(ll)
            Next
            Return l
        End Function
        Public Function UnloadSettings(Of T)(ByVal List As List(Of T)) As ArrayList
            Dim l As New ArrayList
            For Each ll As T In List
                l.Add(ll)
            Next
            Return l
        End Function
        Public Enum SettingMode
            CreateNew = 1
            CreateorUpdate = 2
            Update = 3
        End Enum
    End Module
End Namespace