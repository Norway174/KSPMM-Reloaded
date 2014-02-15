Imports System.IO
Imports Ionic.Zip

Namespace Internal
    Public Module ModIO
        Public UC As New ModIO_UC
        Public ReadOnly Property Plugin As Plugin
            Get
                Dim p As New Plugin
                p.Control = UC
                p.Description = "Basic Mod I/O"
                p.Name = "Modding"
                p.TypeOfPlugin = KSPMM_Reloaded.Plugin.PluginType.TabbedUserControl
                p.Version = New Version(1, 1, 0, 0)
                Return p
            End Get
        End Property
        Private Flags() As String = {"Flags", "Parts", "Props", "Resources", "Sounds", "Spaces", "PluginData", "Plugins", "Textures"}
        Public Mods As New List(Of Modification)
        Public Sub AddMod(ByVal Modification As Modification)
            Modification.Index = Mods.Count
            Mods.Add(Modification)
            SaveModsToSettings(Mods)
            UC.RebuildTree()
        End Sub
        Public Function CheckIfStructureSupported(ByVal Modification As Modification) As Boolean
            Using z As New ZipFile(Modification.Filename)
                Dim b As Boolean = False
                For Each e As ZipEntry In z.Entries
                    If e.IsDirectory AndAlso Flags.Contains(e.FileName.Remove(e.FileName.LastIndexOf("/"c))) Then
                        Return True
                    End If
                Next
            End Using
            Return False
            'MsgBox("File structure not currently supported. This is not even a beta, so you should've been expecting it.", MsgBoxStyle.Exclamation)
        End Function
        Public Sub RemoveMod(ByVal Modification As Modification)
            Mods.Remove(Modification)
            SaveModsToSettings(Mods)
            UC.RebuildTree()
        End Sub
        Public Function LoadModsFromSettings() As List(Of Modification)
            'Return Settings.ObtainSetting("Mods").Setting(0)
            If My.Settings.Mods = "" Then
                My.Settings.Mods = Settings.SaveSettings(New List(Of Modification))
                My.Settings.Save()
                Return New List(Of Modification)
            End If
            Dim xml_serializer As New Xml.Serialization.XmlSerializer(GetType(List(Of Modification)))
            Dim string_reader As New IO.StringReader(My.Settings.Mods)
            Dim lel As List(Of Modification) = _
                DirectCast(xml_serializer.Deserialize(string_reader),  _
                    List(Of Modification))
            string_reader.Close()
            Return lel
            'Return Settings.LoadSettings(My.Settings.Mods)
        End Function
        Public Sub SaveModsToSettings(ByVal sett As List(Of Modification))
            'Settings.ChangeSettings(SettingMode.CreateorUpdate, New InternalSetting({Mods}, "Mods"))
            My.Settings.Mods = Settings.SaveSettings(sett)
            My.Settings.Save()
        End Sub
        Public Function LoadMods(ByVal KSPDir As String) As Boolean
            Try
                For Each m As Modification In Mods
                    Using z As ZipFile = m.GetZipFile
                        Dim name As String = (KSPDir & "\GameData\" & z.Name.Remove(z.Name.LastIndexOf("."c)).Remove(0, z.Name.LastIndexOf("\"c) + 1))
                        Directory.CreateDirectory(name)
                        m.ModFolderExtracted = name
                        For Each entry As ZipEntry In z.Entries
                            For Each e As ModSelection In m.ModSelections
                                If entry.FileName = e.ModEntryName AndAlso e.Use Then
                                    entry.Extract(name, ExtractExistingFileAction.OverwriteSilently)
                                End If
                            Next
                        Next
                        'm.GetZipFile.ExtractAll(name, ExtractExistingFileAction.OverwriteSilently)
                    End Using
                Next
                SaveModsToSettings()
                Return True
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
            Return False
        End Function
        Public Function UnloadMods() As Boolean
            Try
                For Each m As Modification In Mods
                    If Directory.Exists(m.ModFolderExtracted) Then
                        Directory.Delete(m.ModFolderExtracted, True)
                        m.ModFolderExtracted = True
                    End If
                Next
                SaveModsToSettings()
                Return True
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
            Return False
        End Function

        Private Sub SaveModsToSettings()
            Throw New NotImplementedException
        End Sub

    End Module
    Public Structure ModSelection
        Sub New(ByVal _ModEntryName As String, ByVal _Use As Boolean)
            ModEntryName = _ModEntryName
            Use = _Use
        End Sub
        Property Use As Boolean
        Property ModEntryName As String
    End Structure
    <Serializable()> _
    Partial Public Class Modification
        Sub New()
        End Sub
        Sub New(ByVal Filename As String, ByVal Compression As Compression)
            _filename = Filename
            _comp = Compression
            Using z As New ZipFile(_filename)
                For Each e As ZipEntry In z.Entries
                    Dim m As New ModSelection
                    m.ModEntryName = e.FileName
                    m.Use = False
                    ModSelections.Add(m)
                Next
            End Using
        End Sub

        Public Property ModSelections As New List(Of ModSelection)

        Public Function GetZipFile() As ZipFile
            Return New ZipFile(Filename)
        End Function

        Public Property Index As Integer
        Public Property ModFolderExtracted As String

        Private _comp As Compression
        Public ReadOnly Property Compression As Compression
            Get
                Return _comp
            End Get
        End Property

        Private _filename As String
        Public ReadOnly Property Filename As String
            Get
                Return _filename
            End Get
        End Property
    End Class
    Public Enum Compression
        Zip = 1
        Other = 2
    End Enum
End Namespace
#Region "Beta"
Namespace Internal.BrokenBeta
    Public Module ModIO
        Public Mods As New List(Of Modification)
        Public Sub LoadMod(ByVal Modification As Modification)
            Mods.Add(Modification)
        End Sub
        Public Sub RemoveMod(ByVal Modification As Modification)
            Mods.Remove(Modification)
        End Sub
    End Module
    Public Class Modification
        Implements IDisposable

        Private UsedNames As New List(Of String)

        Sub New(ByVal Filename As String, ByVal Compression As Compression, Optional ByVal LoadIntoMemory As Boolean = False)
            _filename = Filename
            _comp = Compression
            If LoadIntoMemory Then
                Dim read As ULong = 0
                Dim stream As New FileStream(Filename, FileMode.Open)
                Do
                    Dim buffer(4095) As Byte
                    Dim count As Integer = stream.Read(buffer, 0, 4096)
                    If count = 0 Then Exit Do
                    Array.Resize(_bytes, _bytes.Count + count)
                    read += count
                Loop
                stream.Close()
                _mem = True
            Else

            End If
        End Sub
        Sub New(ByVal ByteArray As Byte())
            Array.Resize(_bytes, ByteArray.Count)
            _bytes = ByteArray
            _mem = True
        End Sub

        Public Function GetZipFile() As ZipFile
            Dim s As String
            If isLoadedIntoMemory Then
                s = getNewTempName()
                Dim st As New FileStream(s, FileMode.CreateNew)
                st.Write(ByteArray, 0, ByteArray.Count)
            Else
                s = Filename
            End If
            Return New ZipFile(s)
        End Function

        Private Function getNewTempName() As String
            Dim constn As String = My.Computer.FileSystem.SpecialDirectories.Temp & "\KSPMM Reloaded\temp_"
            For i = 0 To Integer.MaxValue
                If File.Exists(constn & i.ToString) = False Then
                    UsedNames.Add(constn & i.ToString)
                    Return constn & i.ToString
                End If
            Next
            Return Nothing
        End Function

        Private _bytes(0) As Byte
        Public ReadOnly Property ByteArray As Byte()
            Get
                Return _bytes
            End Get
        End Property

        Private _comp As Compression
        Public ReadOnly Property Compression As Compression
            Get
                Return _comp
            End Get
        End Property

        Private _mem As Boolean = False
        Public ReadOnly Property isLoadedIntoMemory As Boolean
            Get
                Return _mem
            End Get
        End Property

        Private _filename As String
        Public ReadOnly Property Filename As String
            Get
                Return _filename
            End Get
        End Property

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: dispose managed state (managed objects).
                    For Each d As String In UsedNames
                        If File.Exists(d) Then
                            File.Delete(d)
                        End If
                    Next
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
                _bytes = Nothing
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
    Public Enum Compression
        Zip = 1
        Other = 2
    End Enum
End Namespace
#End Region