''' <summary>
''' Represents a node in a tree structure, where each node can have multiple children and a reference to its parent. The node holds data of type Inode and provides methods to add children and retrieve specific child nodes based on type or predicate.
''' </summary>
Public Class vTreeNode
    ''' <summary>
    ''' Gets or sets the data associated with this tree node. The data is of type Inode, which can represent various types of information stored in the node.
    ''' </summary>
    ''' <returns></returns>
    Public Property Data As Inode
    ''' <summary>
    ''' Gets or sets the parent node of this tree node. The parent is also of type vTreeNode, allowing for hierarchical relationships between nodes in the tree structure.
    ''' </summary>
    ''' <returns></returns>
    Public Property Parent As vTreeNode

    ''' <summary>
    ''' Gets the list of child nodes for this tree node. Each child is also of type vTreeNode, enabling the creation of a tree structure where each node can have multiple children.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Children As List(Of vTreeNode)

    ''' <summary>
    ''' Initializes a new instance of the vTreeNode class with the specified data. The constructor sets the Data property and initializes the Children list to an empty list, allowing for the addition of child nodes later.
    ''' </summary>
    ''' <param name="data"></param>
    Public Sub New(data As Inode)
        Me.Data = data
        Me.Children = New List(Of vTreeNode)
    End Sub

    ''' <summary>
    ''' Adds a new child node to this tree node with the specified data. The method creates a new vTreeNode instance for the child, sets its parent to the current node, and adds it to the Children list. It returns the newly created child node.
    ''' </summary>
    ''' <param name="childData"></param>
    ''' <returns></returns>
    Public Function AddChild(childData As Inode)
        Dim child As New vTreeNode(childData)
        child.Parent = Me
        Me.Children.Add(child)
        Return child
    End Function

    ''' <summary>
    ''' Gets the first child node of this tree node that matches the specified type T. The method uses LINQ to find the first child whose Data property is of type T. If no matching child is found, it returns Nothing.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <returns></returns>
    Public Function GetChildOfTtype(Of T As Inode)() As vTreeNode
        Return Children.FirstOrDefault(Function(c) TypeOf c.Data Is T)
    End Function

    ''' <summary>
    ''' Gets the first child node of this tree node that matches the specified type T and satisfies the provided predicate. The method iterates through the Children list, checks if each child's Data property is of type T, and applies the predicate to determine if it matches. If a matching child is found, it returns that child; otherwise, it returns Nothing.
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
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
