$folder = (resolve-path 'c:\inetpub\posh\vacation').Path

$bgcolor = 'tan'
$TopTextFont = 'verdana'
$TopText = 'Photos from my Vacation!'

foreach ($photo in (get-childitem $folder | % Name)) {
    $Table += "<td><img src=`"./vacation/$photo`" width=300></td>"
}



"<h2><font face=$TopTextFont>$TopText<font></h2>
<body bgcolor = $bgcolor>
<hr>
<table>
   <tr>
       $Table
   </tr>
</table>
<hr>
</body>
"