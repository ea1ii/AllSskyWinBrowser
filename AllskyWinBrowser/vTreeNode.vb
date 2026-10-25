Public Class vTreeNode
    Public Property Data As Inode
    Public Property Parent As vTreeNode
    Public ReadOnly Property Children As List(Of vTreeNode)

    ' Constructor
    Public Sub New(data As Inode)
        Me.Data = data
        Me.Children = New List(Of vTreeNode)
    End Sub

    ' Add a child node
    Public Function AddChild(childData As Inode)
        Dim child As New vTreeNode(childData)
        child.Parent = Me
        Me.Children.Add(child)
        Return child
    End Function

    ' Get the first child of a specific type
    Public Function GetChildOfTtype(Of T As Inode)() As vTreeNode
        Return Children.FirstOrDefault(Function(c) TypeOf c.Data Is T)
    End Function

    ' Get the first child of a specific type that matches a predicate
    Public Function GetChild(Of T As Inode)(predicate As Func(Of T, Boolean)) As vTreeNode
        For Each c In Children
            If TypeOf c.Data Is T Then
                Dim d As T = DirectCast(c.Data, T)
                If predicate(d) Then
                    Return c
                End If
            End If
        Next
        Return Nothing
    End Function

End Class
