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
                p.TypeOfPlugin = KSPMM_Reloaded.Plugin.PluginType.RuntimeScript
                p.Version = New Version(2, 0, 0, 0)
                Return p
            End Get
        End Property
        <Serializable()> _
        Public Class InternalSetting
            Sub New()
            End Sub
            Sub New(ByVal _Setting As Object(), ByVal _ID As String)
                Setting = _Setting
                ID = _ID
            End Sub
            Public Property ID As String
            Public Property Setting As Object()
        End Class
        Public Sub Startup()
            If My.Settings.Settings = "" Then
                My.Settings.Settings = SaveSettings(New List(Of InternalSetting))
            End If
        End Sub
        Public Sub ChangeSettings(ByVal SettingMode As SettingMode, ByVal NewSetting As InternalSetting)
            SyncLock My.Settings.Settings
                'If My.Settings.Settings Is Nothing Then My.Settings.Settings = New ArrayList
                Dim sett = LoadSettings(My.Settings.Settings)
                Select Case SettingMode
                    Case Settings.SettingMode.CreateNew
                        For Each t As InternalSetting In sett
                            If t.ID = NewSetting.ID Then
                                Throw New Exception("Setting with ID already exists")
                                Exit Sub
                            End If
                        Next
                        sett.Add(NewSetting)
                        My.Settings.Settings = SaveSettings(sett)
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
                        My.Settings.Settings = SaveSettings(sett)
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
                        My.Settings.Settings = SaveSettings(sett)
                        My.Settings.Save()
                End Select
            End SyncLock
        End Sub
        Public Function ObtainSetting(ByVal ID As String) As InternalSetting
            Dim l As New List(Of InternalSetting)
            l = LoadSettings(My.Settings.Settings)
            If l Is Nothing Then Return Nothing
            For Each n As InternalSetting In l
                If n.ID = ID Then
                    Return n
                End If
            Next
            Return Nothing
        End Function

        Public Function LoadSettings(ByVal settings As String) As List(Of InternalSetting)
            Try
                Dim xml_serializer As New Xml.Serialization.XmlSerializer(GetType(List(Of InternalSetting)))
                Dim string_reader As New IO.StringReader(settings)
                Dim lel As List(Of InternalSetting) = _
                    DirectCast(xml_serializer.Deserialize(string_reader),  _
                        List(Of InternalSetting))
                string_reader.Close()
                Return lel
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Function SaveSettings(Of T)(ByVal List As List(Of T)) As String
            Dim xml_serializer As New Xml.Serialization.XmlSerializer(GetType(List(Of T)))
            Dim string_writer As New IO.StringWriter
            xml_serializer.Serialize(string_writer, List)

            Dim lel = string_writer.ToString()

            string_writer.Close()
            Return lel
        End Function
        Public Enum SettingMode
            CreateNew = 1
            CreateorUpdate = 2
            Update = 3
        End Enum
    End Module
End Namespace