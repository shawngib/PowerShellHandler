function Add-Title ($text, $style = 'h1'){
"<$style>$text</$style>"
}

function Add-HorizontalRule {
'<hr>'
}

function Add-Image ($source, $size) {
"<img src=`"$source`" width=$size>"
}

function Add-Text ($text, $style) {
"<p style=`"$style`">$text</p>"
}

function Add-Head ($title, $link) {
"<title>$title</title>"
"<link rel=`"stylesheet`" href=`"$link`" type=`"text/css`">"
}

function Add-DIV ($id, $content) {
"<div id=`"$id`">$content</div>"
}

function Add-Link ($url, $text) {
"<a href=`"$url`">$text</a>"
}