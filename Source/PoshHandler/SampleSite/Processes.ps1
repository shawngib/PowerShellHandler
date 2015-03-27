$processes = Get-Process
Foreach($process in $processes)
{
$content = $content + @"
		<tr>
			<td>$($process.Name)</td>
			<td>$($process.Description)</td>
			<td>$($process.CPU)9</td>
		</tr>
"@
}

$Head = @"
		<title>Sample POSH Handler</title>
		<meta http-equiv="content-type" content="text/html; charset=utf-8" />
		<meta name="description" content="" />
		<meta name="keywords" content="" />
"@

$Body =@"
				<section>
						<h4>Table</h4>
						<h5>Default</h5>
						<div class="table-wrapper">
							<table>
								<thead>
									<tr>
										<th>Process</th>
										<th>Description</th>
										<th>CPU</th>
									</tr>
								</thead>
								<tbody>
									$content
								</tbody>
								<tfoot>
									<tr>
										<td colspan="2"></td>
										<td>Complete</td>
									</tr>
								</tfoot>
							</table>
						</div>
			  </section>
"@

ConvertTo-Html -Body $Body -Head $Head
