<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMetadata
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        TreeView1 = New TreeView()
        SuspendLayout()
        ' 
        ' TreeView1
        ' 
        TreeView1.BackColor = SystemColors.Control
        TreeView1.Dock = DockStyle.Fill
        TreeView1.Location = New Point(0, 0)
        TreeView1.Name = "TreeView1"
        TreeView1.Size = New Size(652, 671)
        TreeView1.TabIndex = 0
        TreeView1.TabStop = False
        ' 
        ' frmMetadata
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        AutoScroll = True
        ClientSize = New Size(652, 671)
        Controls.Add(TreeView1)
        KeyPreview = True
        MinimizeBox = False
        Name = "frmMetadata"
        ShowInTaskbar = False
        Text = "frmMetadata"
        TopMost = True
        ResumeLayout(False)
    End Sub

    Friend WithEvents TreeView1 As TreeView
End Class
