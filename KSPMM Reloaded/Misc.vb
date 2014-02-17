Imports Ionic.Zip
Module Misc
    Public Function GetTextFromZipEntry(ByVal entry As ZipEntry) As String
        Dim name = My.Computer.FileSystem.SpecialDirectories.Temp & "\"
        entry.Extract(name, ExtractExistingFileAction.OverwriteSilently)
        Dim t As New IO.StreamReader(name & "\" & entry.FileName.Replace("/"c, "\"c))
        Dim s = t.ReadToEnd()
        t.Close()
        'IO.Directory.Delete(name, True)
        IO.File.Delete(name & entry.FileName.Replace("/"c, "\"c))
        Return s
    End Function
End Module
