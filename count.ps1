$counts = @{}
foreach ($file in Get-ChildItem .)
 { get-content $file -ReadCount 1000 |
   foreach { $counts[$file] += $_.count}
 }

 $counts