$s = "Panelak"
$r = "Panelak"

Get-ChildItem "C:\Users\miros\source\repos\Panelak\Panelak.Framework" -Recurse -Filter *.* | % {
  (Get-Content $_.FullName) `
    | % { $_ -replace [regex]::Escape($s), $r } `
    | Set-Content $_.FullName
}
