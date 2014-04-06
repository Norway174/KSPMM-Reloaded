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
                p.Version = New Version(2, 0, 0, 0)
                Return p
            End Get
        End Property
        Public ReadOnly Property StartupPlugin As Plugin
            Get
                Dim p As New Plugin
                p.Name = "Modding Startup"
                p.TypeOfPlugin = KSPMM_Reloaded.Plugin.PluginType.RuntimeScript
                p.MainDelegate = AddressOf Startup
                p.Version = New Version(1, 0, 0, 0)
                Return p
            End Get
        End Property

        Private Flags() As String = {"Flags", "Parts", "Props", "Resources", "Sounds", "Spaces", "PluginData", "Plugins", "Textures"}
        Public Mods As New List(Of Modification)
        Public ModDir As String = My.Computer.FileSystem.CurrentDirectory & "\MODS"
        Public Sub Startup()
            If IO.Directory.Exists(ModDir) = False Then
                IO.Directory.CreateDirectory(ModDir)
            End If
            Internal.ModIO.Mods = Internal.ModIO.LoadModsFromSettings()
            ReEvaluateStatus()
        End Sub
        Public Sub AddMod(ByVal Modification As Modification)
            Modification.ID = Mods.Count
            Dim m = Modification
            Dim newname As String = ModDir & "\" & Modification.Filename.Remove(0, Modification.Filename.LastIndexOf("\"c))
            File.Copy(Modification.Filename, newname, True)
            m.Filename = newname
            Mods.Add(m)
            ReEvaluateStatus()
            SaveModsToSettings(Mods)
            UC.BuildTree()
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
        End Function
        Public Sub RemoveMod(ByVal Modification As Modification)
            Try
                IO.File.Delete(Modification.Filename)
            Catch ex As Exception
            End Try
            Mods.Remove(Modification)
            ReEvaluateStatus()
            SaveModsToSettings(Mods)
            UC.BuildTree()
        End Sub
        Public Function LoadModsFromSettings() As List(Of Modification)
            Try
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
            Catch
            End Try
            Return New List(Of Modification)
        End Function
        Public Sub ReEvaluateStatus()
            Dim i = 0
            For Each m As Modification In Mods
                If m.Status = ModStatus.Installed Then Continue For
                If IO.File.Exists(m.Filename) = False Then
                    Mods(i).Status = ModStatus.Missing
                End If
                i += 1
            Next
        End Sub
        Public Sub SaveModsToSettings(ByVal sett As List(Of Modification))
            My.Settings.Mods = Settings.SaveSettings(sett)
            My.Settings.Save()
        End Sub
        Public Function LoadMods(ByVal KSPDir As String) As Boolean
            Dim i = 0
            Log("Initiallizing...")
            For Each m As Modification In Mods
                UC.TreeView1.Nodes(i).ImageIndex = 5
                UC.TreeView1.Nodes(i).SelectedImageIndex = 5
            Next
            i = 0
            For Each m As Modification In Mods
                UC.prgOverall.Maximum = Mods.Count
                UC.prgOverall.Value = i + 1
                If m.Use Then
                    Log("Loading " & m.Filename.Remove(0, m.Filename.LastIndexOf("\"c) + 1))
                    UC.TreeView1.Nodes(i).ImageIndex = 6
                    UC.TreeView1.Nodes(i).SelectedImageIndex = 6
                Else
                    Log("Ignoring " & m.Filename.Remove(0, m.Filename.LastIndexOf("\"c) + 1))
                    UC.TreeView1.Nodes(i).ImageIndex = 7
                    UC.TreeView1.Nodes(i).SelectedImageIndex = 7
                    Continue For
                End If
                Dim name = ""
                Try
                    Dim ii = 0
                    Using z As ZipFile = m.GetZipFile
                        Dim b As Boolean = True
                        name = (KSPDir & "\GameData\" & z.Name.Remove(z.Name.LastIndexOf("."c)).Remove(0, z.Name.LastIndexOf("\"c) + 1))
                        Directory.CreateDirectory(name)
                        m.ModFolderExtracted = name
                        For Each entry As ZipEntry In z.Entries
                            UC.prgIndividual.Maximum = z.Entries.Count
                            UC.prgIndividual.Value = ii + 1
                            Log("Extracting " & entry.FileName)
                            Try
                                entry.Extract(name, ExtractExistingFileAction.Throw)
                            Catch ex As Exception
                                Log("Failed: File already exists")
                                Directory.Delete(name)
                                UC.TreeView1.Nodes(i).ImageIndex = 9
                                UC.TreeView1.Nodes(i).SelectedImageIndex = 9
                                MsgBox(ex.Message, MsgBoxStyle.Critical)
                                b = False
                                Exit For
                            End Try
                            ii += 1
                            If b = True Then m.Status = ModStatus.Installed
                        Next
                    End Using
                    UC.TreeView1.Nodes(i).ImageIndex = 8
                    UC.TreeView1.Nodes(i).SelectedImageIndex = 8
                Catch ex As Exception
                    Directory.Delete(name)
                    UC.TreeView1.Nodes(i).ImageIndex = 10
                    UC.TreeView1.Nodes(i).SelectedImageIndex = 10
                    MsgBox(ex.Message, MsgBoxStyle.Critical)
                End Try
                i += 1
            Next
            Log("Complete")
            SaveModsToSettings(Mods)
            UC.RebuildTree()
            Return True
        End Function
        Public Sub Log(ByVal NewLog As String)
            UC.StatusUpdate(NewLog)
        End Sub
        Public Function UnloadMods() As Boolean
            Try
                For Each m As Modification In Mods
                    If Directory.Exists(m.ModFolderExtracted) Then
                        Directory.Delete(m.ModFolderExtracted, True)
                        m.ModFolderExtracted = False
                    End If
                    m.Status = ModStatus.Uninstalled
                Next
                SaveModsToSettings(Mods)
                UC.RebuildTree()
                Return True
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
            Return False
        End Function
    End Module
    <Serializable()> _
    Partial Public Class Modification
        Sub New()
        End Sub
        Sub New(ByVal _Filename As String, ByVal _Compression As Compression)
            Filename = _Filename
            Name = _Filename
            If _Compression = Internal.Compression.Auto Then
                Dim f As New IO.FileInfo(_Filename)
                If f.Extension = ".zip" Then
                    'Internal.AddMod(New Internal.Modification(_Filename, Internal.Compression.Zip))
                    _Compression = Internal.Compression.Zip
                ElseIf f.Extension = ".kspmm" Then
                    'Internal.AddMod(New Internal.Modification(_Filename, Internal.Compression.KSPMM))
                    _Compression = Internal.Compression.KSPMM
                Else
                    'Internal.AddMod(New Internal.Modification(_Filename, Internal.Compression.Other))
                    _Compression = Internal.Compression.Other
                End If
            Else
                Compression = _Compression
            End If
            Status = ModStatus.Uninstalled
            Use = False
            Select Case Compression
                Case Internal.Compression.Zip

                Case Internal.Compression.KSPMM
                    Using z = GetZipFile()
                        For Each e As ZipEntry In z.Entries
                            If e.FileName = "info.xml" Then
                                Dim i As New InfoParser(Misc.GetTextFromZipEntry(e))
                                Name = i._nname
                            End If
                        Next
                    End Using
                Case Internal.Compression.Other
                    Throw New NotImplementedException("Only ZIP supported at this time")
            End Select
        End Sub
        Public Property Use As Boolean

        Public Function GetZipFile() As ZipFile
            Return New ZipFile(Filename)
        End Function

        Public Property Name As String
        Public Property Status As ModStatus

        Public Property ID As Integer
        Public Property ModFolderExtracted As String

        Public Property Compression As Compression
        Public Property Filename As String
    End Class
    Public Enum Compression
        Auto = 0
        Zip = 1
        KSPMM = 2
        Other = 3
    End Enum
    Public Enum ModStatus
        Missing = -1
        Uninstalled = 0
        Installed = 1
    End Enum
End Namespace