Imports System.Text.Json

Public Class frmMetadata

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(metadataPath As String)
        Me.New
        Me.Text = "Metadata Viewer - " & metadataPath

        If Not IO.File.Exists(metadataPath) Then Me.Close()

        Dim jsonString = IO.File.ReadAllText(metadataPath)
        Dim doc = JsonDocument.Parse(jsonString)


        TreeView1.Nodes.Clear()
        AddJsonElement(TreeView1.Nodes, doc.RootElement)
        TreeView1.ExpandAll()
    End Sub

    Private Sub AddJsonElement(nodes As TreeNodeCollection, element As JsonElement, Optional name As String = Nothing)
        Dim nodeText = If(name, element.ToString())
        Dim node = nodes.Add(nodeText)

        Select Case element.ValueKind
            Case JsonValueKind.Object
                For Each prop In element.EnumerateObject()
                    AddJsonElement(node.Nodes, prop.Value, prop.Name)
                Next

            Case JsonValueKind.Array
                Dim i = 0
                For Each item In element.EnumerateArray()
                    AddJsonElement(node.Nodes, item, $"[{i}]")
                    i += 1
                Next

            Case Else
                node.Text = $"{name}: {element}"
        End Select
    End Sub

    Private Sub frmMetadata_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class