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
                p.Version = New Version(1, 0, 0, 0)
                Return p
            End Get
        End Property
        Private Flags() As String = {"Flags", "Parts", "Props", "Resources", "Sounds", "Spaces", "PluginData", "Plugins", "Textures"}
        Public Mods As New List(Of Modification)
        Public ModFileNames As New List(Of String)
        Public Sub AddMod(ByVal Modification As Modification)
            Dim z = Modification.GetZipFile
            Dim b As Boolean = False
            For Each e As ZipEntry In z.Entries
                If e.IsDirectory AndAlso Flags.Contains(e.FileName.Remove(e.FileName.LastIndexOf("/"c))) Then
                    Mods.Add(Modification)
                    SaveModsToSettings()
                    Return
                End If
            Next
            MsgBox("File structure not currently supported. This is not even a beta, so you should've been expecting it.", MsgBoxStyle.Exclamation)
        End Sub
        Public Sub RemoveMod(ByVal Modification As Modification)
            Mods.Remove(Modification)
            SaveModsToSettings()
        End Sub
        Public Function LoadModsFromSettings() As List(Of Modification)
            Return Settings.ObtainSetting("Mods").Setting(0)
        End Function
        Public Function LoadModNamesFromSettings() As List(Of String)
            Return Settings.ObtainSetting("ModNames").Setting(0)
        End Function
        Public Sub SaveModsToSettings()
            Settings.ChangeSettings(SettingMode.CreateorUpdate, New InternalSetting({Mods}, "Mods"))
        End Sub
        Public Sub SaveModNamesToSettings()
            Settings.ChangeSettings(SettingMode.CreateorUpdate, New InternalSetting({ModFileNames}, "ModNames"))
        End Sub
        Public Function LoadMods(ByVal KSPDir As String) As Boolean
            Try
                For Each m As Modification In Mods
                    Dim z As ZipFile = m.GetZipFile
                    Dim name As String = (KSPDir & "\GameData\" & z.Name.Remove(z.Name.LastIndexOf("."c)).Remove(0, z.Name.LastIndexOf("\"c) + 1))
                    Directory.CreateDirectory(name)
                    ModFileNames.Add(name)
                    m.GetZipFile.ExtractAll(name, ExtractExistingFileAction.OverwriteSilently)
                Next
                SaveModNamesToSettings()
                Return True
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
            Return False
        End Function
        Public Function UnloadMods() As Boolean
            Try
                For Each m As String In ModFileNames
                    Directory.Delete(m, True)
                Next
                ModFileNames.Clear()
                SaveModNamesToSettings()
                Return True
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
            Return False
        End Function
    End Module
    Partial Public Class Modification
        Sub New(ByVal Filename As String, ByVal Compression As Compression)
            _filename = Filename
            _comp = Compression
        End Sub

        Public Function GetZipFile() As ZipFile
            Return New ZipFile(Filename)
        End Function

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