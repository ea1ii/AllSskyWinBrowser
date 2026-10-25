Public Class Tree
    Public Property Root As vTreeNode

    ''' <summary>
    ''' Initializes a new instance of the Tree class with the specified root data.
    ''' </summary>
    ''' <param name="rootData"></param>
    Public Sub New(rootData As Inode)
        Root = New vTreeNode(rootData)
    End Sub

    ''' <summary>
    ''' Adds a child node to the root of the tree.
    ''' </summary>
    ''' <param name="childData"></param>
    ''' <returns></returns>
    Public Function AddToRoot(childData As Inode) As vTreeNode
        Return Root.AddChild(childData)
    End Function

    ''' <summary>
    ''' Traverses the tree in depth-first order and yields each node.
    ''' </summary>
    ''' <returns></returns>
    Public Iterator Function Traverse() As IEnumerable(Of vTreeNode)
        For Each node In TraverseNode(Root)
            Yield node
        Next
    End Function

    ''' <summary>
    ''' Recursively traverses the tree starting from the specified node and yields each node in depth-first order.
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    Private Iterator Function TraverseNode(node As vTreeNode) As IEnumerable(Of vTreeNode)
        Yield node
        For Each child In node.Children
            For Each subNode In TraverseNode(child)
                Yield subNode
            Next
        Next
    End Function

    ''' <summary>
    ''' Finds the first node in the tree that matches the specified predicate.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function Find(predicate As Func(Of vTreeNode, Boolean)) As vTreeNode
        For Each node In Traverse()
            If predicate(node) Then Return node
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Finds all nodes in the tree that match the specified predicate and returns them as a list.
    ''' </summary>
    ''' <param name="predicate"></param>
    ''' <returns></returns>
    Public Function FindAll(predicate As Func(Of vTreeNode, Boolean)) As List(Of vTreeNode)
        Dim results As New List(Of vTreeNode)
        For Each node In Traverse()
            If predicate(node) Then results.Add(node)
        Next
        Return results
    End Function

    ''' <summary>
    ''' Counts the total number of nodes in the tree.
    ''' </summary>
    ''' <returns></returns>
    Public Function Count() As Integer
        Return Traverse().Count()
    End Function

    ''' <summary>
    ''' Returns a list of all nodes in the tree in depth-first order.
    ''' </summary>
    ''' <returns></returns>
    Public Function ToList() As List(Of vTreeNode)
        Return Traverse().ToList()
    End Function

    ''' <summary>
    ''' Gets or creates a YearNode under the specified root node. If a YearNode with the specified year already exists, it returns that node; otherwise, it creates a new YearNode and adds it as a child of the root node.
    ''' </summary>
    ''' <param name="root"></param>
    ''' <param name="year"></param>
    ''' <returns></returns>
    Public Function GetOrCreateYear(root As vTreeNode, year As String) As vTreeNode
        Dim existing = root.GetChild(Of YearNode)(Function(y) y.Year = year)
        If existing IsNot Nothing Then Return existing

        Return root.AddChild(New YearNode() With {.Year = year})
    End Function

    ''' <summary>
    ''' Gets or creates a MonthNode under the specified YearNode. If a MonthNode with the specified month already exists, it returns that node; otherwise, it creates a new MonthNode and adds it as a child of the YearNode.
    ''' </summary>
    ''' <param name="yearNode"></param>
    ''' <param name="month"></param>
    ''' <returns></returns>
    Public Function GetOrCreateMonth(yearNode As vTreeNode, month As String) As vTreeNode
        Dim existing = yearNode.GetChild(Of MonthNode)(Function(m) m.Month = month)
        If existing IsNot Nothing Then Return existing

        Return yearNode.AddChild(New MonthNode() With {.Month = month})
    End Function

    ''' <summary>
    ''' Gets or creates a DayNode under the specified MonthNode. If a DayNode with the specified day already exists, it returns that node; otherwise, it creates a new DayNode and adds it as a child of the MonthNode.
    ''' </summary>
    ''' <param name="monthNode"></param>
    ''' <param name="day"></param>
    ''' <returns></returns>
    Public Function GetOrCreateDay(monthNode As vTreeNode, day As String) As vTreeNode
        Dim existing = monthNode.GetChild(Of DayNode)(Function(d) d.Day = day)
        If existing IsNot Nothing Then Return existing

        Return monthNode.AddChild(New DayNode() With {.Day = day})
    End Function

End Class
