Imports System.IO
Imports System.Text.Json

Public Module ScoreManager
    ''' <summary>
    ''' Defines the path to the scores.json file, which is located in the base path specified in the configuration. This file is used to store and retrieve score data for various items.
    ''' </summary>
    Private ReadOnly scorePath As String = Path.Combine(cfg.BasePath, "scores.json")

    ''' <summary>
    ''' Holds the current scores loaded from the scores.json file. It is initialized as an empty ScoreFile with a dictionary to store item IDs and their corresponding score entries.
    ''' </summary>
    Public Scores As New ScoreFile With {.Items = New Dictionary(Of String, ScoreEntry)}

    ''' <summary>
    ''' Defines the different types of scores that can be assigned to items. Each score type is represented by an integer value, where Crap = 0, Pass = 1, Nice = 2, and Cool = 3.
    ''' </summary>
    Public Enum ScoreType
        Crap = 0
        Pass = 1
        Nice = 2
        Cool = 3
    End Enum

    ''' <summary>
    ''' Loads the scores from the JSON file. If the file does not exist or is invalid, it initializes an empty score dictionary.
    ''' </summary>
    Public Sub LoadScores()
        Try
            If File.Exists(scorePath) Then
                Dim json = File.ReadAllText(scorePath)
                Dim loaded = JsonSerializer.Deserialize(Of ScoreFile)(json)

                If loaded IsNot Nothing AndAlso loaded.Items IsNot Nothing Then
                    Scores = loaded
                End If
            End If

        Catch ex As Exception
            ' fallback to empty dictionary
            Scores = New ScoreFile With {.Items = New Dictionary(Of String, ScoreEntry)}
        End Try
    End Sub

    ''' <summary>
    ''' Saves the current scores to the JSON file. It writes to a temporary file first and then replaces the original file to ensure data integrity.
    ''' </summary>
    Public Sub SaveScores()
        Try
            Dim options As New JsonSerializerOptions With {
                .WriteIndented = True
            }

            Dim json = JsonSerializer.Serialize(Scores, options)

            Dim tmp = scorePath & ".tmp"
            File.WriteAllText(tmp, json)

            File.Copy(tmp, scorePath, True)
            File.Delete(tmp)

        Catch ex As Exception
            MsgBox("Failed to save scores.", MsgBoxStyle.Critical & MsgBoxStyle.OkOnly, "Score Manager")
        End Try
    End Sub

    ''' <summary>
    ''' Gets the score for a given item ID. If the item ID does not exist in the scores dictionary, it initializes it with a default score of Pass (1) and returns that value.
    ''' </summary>
    ''' <param name="itemId"></param>
    ''' <returns></returns>
    Public Function GetScore(itemId As String) As ScoreType
        If Scores.Items.ContainsKey(itemId) Then
            Return Scores.Items(itemId).Score
        Else
            Scores.Items(itemId) = New ScoreEntry With {.Score = ScoreType.Pass, .LastUpdated = DateTime.Now()}
            Return 0
        End If
    End Function

    ''' <summary>
    ''' Sets the score for a given item ID. If the item ID does not exist in the scores dictionary, it initializes it with a new ScoreEntry and sets the score to the provided value.
    ''' </summary>
    ''' <param name="itemId"></param>
    ''' <param name="value"></param>
    Public Sub SetScore(itemId As String, value As Integer)
        If Not Scores.Items.ContainsKey(itemId) Then
            Scores.Items(itemId) = New ScoreEntry()
        End If

        Scores.Items(itemId).Score = value
        Scores.Items(itemId).LastUpdated = DateTime.Now()
    End Sub

    ''' <summary>
    ''' Counts the number of items that have a specific score type. It iterates through the scores dictionary and increments the count for each item that matches the target score.
    ''' </summary>
    ''' <param name="targetScore"></param>
    ''' <returns></returns>
    Public Function CountScore(targetScore As ScoreType) As Integer
        Dim count As Integer = 0

        For Each kv In Scores.Items
            If kv.Value.Score = targetScore Then
                count += 1
            End If
        Next

        Return count
    End Function

    ''' <summary>
    ''' Retrieves a list of item IDs that have a specific score type. It iterates through the scores dictionary and adds the item IDs to the list if their score matches the target score.
    ''' </summary>
    ''' <param name="targetScore"></param>
    ''' 
    ''' </summary>
    ''' <param name="targetScore"></param>
    ''' <returns></returns>
    Public Function GetItemsByScore(targetScore As ScoreType) As List(Of String)
        Dim items As New List(Of String)
        For Each kv In Scores.Items
            If kv.Value.Score = targetScore Then
                items.Add(kv.Key)
            End If
        Next
        Return items
    End Function

    ''' <summary>
    ''' Removes the score entry for a given item ID from the scores dictionary. If the item ID exists, it is removed; otherwise, no action is taken.
    ''' </summary>
    ''' <param name="itemId"></param>
    Public Sub RemoveScore(itemId As String)
        If Scores.Items.ContainsKey(itemId) Then
            Scores.Items.Remove(itemId)
        End If
    End Sub

End Module

''' <summary>
''' Represents the structure of the score file, containing a dictionary of item IDs and their corresponding score entries.
''' </summary>
Public Class ScoreFile
    Public Property Items As Dictionary(Of String, ScoreEntry)
End Class

''' <summary>
''' Represents an individual score entry, containing the score value and the last updated timestamp.
''' </summary>
Public Class ScoreEntry
    Public Property Score As ScoreType
    Public Property LastUpdated As DateTime
End Class
