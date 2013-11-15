<%@ Page Language="C#" CodeBehind="Default.aspx.cs" Inherits="Workshare.Samples.ConfigPageSample._Default" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Configuration Sample</title>
	<link rel="stylesheet" type="text/css" href="stylesheet.css" />
    <meta http-equiv="x-ua-compatible" content="IE=EmulateIE9" />

	<script type="text/javascript" src="bindows_gauges/bindows_gauges.js"></script>

	<script type="text/javascript">

		var form;
		var maxKB = 0;
		var maxMB = 0;
		var protocol;
		var chunksize;
		var intervalIDGauge1;
		var intervalIDGauge2;
		var intervalIDGauge3;
		var bytesPerSecGauge;
		var mbytesPerMinGauge;
		var percentCompleteGauge;

		function register(form, pro, chu) {
			this.form = form;
			this.protocol = pro;
			this.chunksize = chu;
		}

		function updateFormValues() {

			var selectedprotocol = document.getElementById("protocol");
			protocol.value = selectedprotocol.value;

			var selectedchunksize = document.getElementById("chunksize");
			chunksize.value = selectedchunksize.value;
		}

		function startPolling() {

			intervalIDGauge1 = setInterval(updateGauge, 1000);
			intervalIDGauge2 = setInterval(updateGauge1, 1000);
			intervalIDGauge3 = setInterval(updateGauge2, 1000);
		}

		function stopPolling() {

			clearInterval(intervalIDGauge1);
			clearInterval(intervalIDGauge2);
			clearInterval(intervalIDGauge3);

			updateGauge();
			updateGauge1();
			updateGauge2();

		}

		function onPerformClick() {

			$get('perform').disabled = 'disabled';
			$get('protocol').disabled = 'disabled';
			$get('chunksize').disabled = 'disabled';
			$get('updateConfig').disabled = 'disabled';

			updateFormValues();

			maxKB = 0;
			maxMB = 0;

			startPolling();

			this.form.submit();
		}

		function onComplete() {
			$get('perform').disabled = '';
			$get('protocol').disabled = '';
			$get('chunksize').disabled = '';
			$get('updateConfig').disabled = '';

			stopPolling();
		}

		function updateGaugeLabels() {
			var selectedprotocol = document.getElementById("protocol").value;
			if (selectedprotocol == 'namedPipe') {
				bytesPerSecGauge.unitlabel.setText('x10KB/sec');
				mbytesPerMinGauge.unitlabel.setText('x10MB/min');
			}
			else {
				bytesPerSecGauge.unitlabel.setText('KB/sec');
				mbytesPerMinGauge.unitlabel.setText('MB/min');
			}
		}

		function toggleGraphPanel(showGraph) {
			var graphDiv = document.getElementById('GraphPanelDiv');
			var gridDiv = document.getElementById('GridPanelDiv');

			if (showGraph) {
				graphDiv.style.display = 'block';
				gridDiv.style.display = 'none';
			}
			else {
				graphDiv.style.display = 'none';
				gridDiv.style.display = 'block';
			}
		}

		function toggleLoginSection(showLogin) {
			var loginSection = document.getElementById('LoginDiv');
			var mainConfigSection = document.getElementById('MainConfigPageDiv');

			if (showLogin) {
				loginSection.style.display = 'block';
				mainConfigSection.style.display = 'none';
			}
			else {
				loginSection.style.display = 'none';
				mainConfigSection.style.display = 'block';
			}
		}

		function updateStatus(message) {
			var statusPane = document.getElementById('status');

			statusPane.style.display = 'block';
			statusPane.innerHTML = message;
		}

		function onUpdateConfigClick() {

			var selectedprotocol = document.getElementById("protocol").value;
			var selectedchunksize = document.getElementById("chunksize").value;

			PageMethods.UpdateConfigFile(selectedprotocol, selectedchunksize);
			alert('Advanced Web Sample configuration has been updated.');
		}

		function toggleUpdateConfigButton(show) {
			if (show) {
				$get('updateConfig').style.display = 'block';
			}
			else {
				$get('updateConfig').style.display = 'none';
			}
		}

		function enablePerformer(enable) {
		    if (enable) {
		        $get('perform').disabled = '';
		    }
		    else {
		        $get('perform').disabled = 'disabled';
		    }
		}
	</script>

</head>
<body class="body">
	<form runat="server">
	<asp:ScriptManager ID="scriptManager" runat="server" EnablePageMethods="true" />
	<table border="0" width="100%" bordercolor="#666666" cellpadding="0" cellspacing="0"
		height="90%">
		<tr>
			<td valign="top">
				<!-- Login Section -->
				<div id="LoginDiv">
					<table border="0" width="100%" cellpadding="0" cellspacing="0">
						<!-- Header Image -->
						<tr>
							<td style="background-color: #FFFFFF; height: 1%;">
							</td>
							<td>
								<img src="img/admin-banner.gif" alt="Workshare" /><br />
							</td>
							<td background="img/admin-banner-grey.gif" width="60%">
								&nbsp;
							</td>
						</tr>
						<tr>
							<td style="background-color: #ffffff; width: 0.5%" valign="top">
								<img src="Img/nav-spacer.gif" />
								<br />
							</td>
							<td colspan="2" bgcolor="#003366" style="width: 99.5%;">
								<ajaxToolkit:TabContainer ID="TabContainer1" runat="server" CssClass="ajax__tab_yuitabview-theme">
									<!-- Login Tab -->
									<ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Login">
										<ContentTemplate>
											<div style="display: block">
												<table border="0" width="40%" cellpadding="4" cellspacing="0">
													<tr>
														<td colspan="2">
															&nbsp;
														</td>
													</tr>
													<tr>
														<td colspan="2">
															<div class="loginMainHeading">
																Welcome to Workshare Compare Server</div>
															<div class="loginNormal">
																<asp:Label ID="VersionLabel" runat="server" /></div>
														</td>
													</tr>
													<tr>
														<td colspan="2">
															<div class="loginHeading2">
																Please enter your login details below.</div>
														</td>
													</tr>
													<tr>
														<td colspan="2">
															&nbsp;
														</td>
													</tr>
													<tr>
														<td class="loginNormal">
															Username:
														</td>
														<td align="left">
															<asp:TextBox ID="UsernameTextBox" runat="server" Width="250px" Height="22px"></asp:TextBox>
														</td>
													</tr>
													<tr>
														<td class="loginNormal">
															Password:
														</td>
														<td align="left">
															<asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" Width="250px"
																Height="22px"></asp:TextBox>
														</td>
													</tr>
													<tr>
														<td class="loginNormal">
															Domain:
														</td>
														<td align="left">
															<asp:TextBox ID="DomainTextBox" runat="server" Width="250px" Height="22px"></asp:TextBox>
														</td>
													</tr>
													<tr>
														<td>
															&nbsp;
														</td>
														<td>
															<asp:Button ID="LoginButton" runat="server" Text="Log in" OnClick="LoginButton_Click" />
														</td>
													</tr>
													<tr>
														<td colspan="2">
															&nbsp;
														</td>
													</tr>
												</table>
											</div>
										</ContentTemplate>
									</ajaxToolkit:TabPanel>
								</ajaxToolkit:TabContainer>
							</td>
						</tr>
						<tr>
							<td colspan="2">
								&nbsp;
							</td>
						</tr>
						<tr>
							<td>
								&nbsp;
							</td>
							<td>
							</td>
						</tr>
					</table>
				</div>
				<!-- Main Config Page -->
				<div id="MainConfigPageDiv">
					<table border="0" width="100%" cellpadding="0" cellspacing="0">
						<!-- Header Image -->
						<tr>
							<td style="background-color: #FFFFFF; height: 1%;">
							</td>
							<td background="img/admin-banner-grey.gif">
								<img src="img/admin-banner.gif" alt="Workshare" /><br />
							</td>
							<td background="img/admin-banner-grey.gif">
								&nbsp;
							</td>
						</tr>
						<!-- Tab Control -->
						<tr>
							<td style="background-color: #ffffff; width: 0.5%" valign="top">
								<img src="Img/nav-spacer.gif" />
								<br />
							</td>
							<!-- Tab Control actually lives here -->
							<td colspan="2" bgcolor="#003366" style="width: 95.5%;">
								<ajaxToolkit:TabContainer runat="server" CssClass="ajax__tab_yuitabview-theme">
									<!-- Home Tab -->
									<ajaxToolkit:TabPanel runat="server" HeaderText="Home">
										<ContentTemplate>
											<div style="display: block">
												<table border="0" cellpadding="4" cellspacing="0" width="70%">
													<tr>
														<td colspan="3">
															<br />
															<div class="settingsHeading1">
																Content Overview
															</div>
														</td>
													</tr>
													<tr>
														<td colspan="3">
														    <asp:UpdatePanel ID="StatisticsPanel" runat="server" UpdateMode="Conditional">
															<ContentTemplate>
															<table cellpadding="0" cellspacing="0" border="0" width="100%">
																<tr>
																	<td>
																		<table border="1" style="border-color: #C2CAD1;" cellpadding="5" cellspacing="0"
																			width="100%">
																			<tr>
																				<td class="settingsHeading2" style="height: 20px;">
																					In the last hour:
																				</td>
																			</tr>
																			<tr>
																				<td>
																					<table border="0" cellpadding="2">
																						<tr>
																							<td class="settingsNormal">
																								No of comparisons:
																							</td>
																							<td class="settingsNormal">
																								<asp:Label ID="NoOfComparisonsInLastHour" runat="server" />
																							</td>
																						</tr>
																						<tr>
																							<td class="settingsNormal">
																								No. of bytes processed:
																							</td>
																							<td class="settingsNormal">
																								<asp:Label ID="NoOfBytesProcessInLastHour" runat="server" />
																								KB
																							</td>
																						</tr>
																						<tr>
																							<td class="settingsNormal">
																								Avg. comparison rate:
																							</td>
																							<td class="settingsNormal">
																								<asp:Label ID="AverageComparisonRateInLastHour" runat="server" />
																								KB/sec
																							</td>
																						</tr>
																						<tr>
																							<td class="settingsNormal">
																								Avg. comparison rate(with conversion):
																							</td>
																							<td class="settingsNormal">
																								<asp:Label ID="AverageComparisonRateInLastHourWithConversion" runat="server" />
																								KB/sec
																							</td>
																						</tr>
																						<tr>
																							<td>
																							</td>
																							<td>
																							</td>
																						</tr>
																					</table>
																				</td>
																			</tr>
																		</table>
																	</td>
																	<td width="10%">
																		&nbsp;
																	</td>
																	<td>
																		<table border="1" style="border-color: #C2CAD1;" cellpadding="5" cellspacing="0"
																			width="100%">
																			<tr>
																				<td class="settingsHeading2" style="height: 20px;">
																					So far:
																				</td>
																			</tr>
																			<tr>
																				<td>
																					<table border="0" cellpadding="2">
																						<tr>
																							<td class="settingsNormal">
																								No of comparisons:
																							</td>
																							<td class="settingsNormal">
																								<asp:Label ID="NoOfComparisons" runat="server" />
																							</td>
																						</tr>
																						<tr>
																							<td class="settingsNormal">
																								No. of bytes processed:
																							</td>
																							<td class="settingsNormal">
																								<asp:Label ID="NoOfBytesProcessed" runat="server" />
																								KB
																							</td>
																						</tr>
																						<tr>
																							<td class="settingsNormal">
																								Avg. comparison rate:
																							</td>
																							<td class="settingsNormal">
																								<asp:Label ID="AverageComparisonRate" runat="server" />
																								KB/sec
																							</td>
																						</tr>
																						<tr>
																							<td class="settingsNormal">
																								Avg. comparison rate(with conversion):
																							</td>
																							<td class="settingsNormal">
																								<asp:Label ID="AverageComparisonRateWithConversion" runat="server" />
																								KB/sec
																							</td>
																						</tr>
																						<tr>
																							<td>
																							</td>
																							<td>
																							</td>
																						</tr>
																					</table>
																				</td>
																			</tr>
																		</table>
																	</td>
																</tr>
																<tr>
																    <td colspan="3" align="right">
																        <asp:Button ID="RefreshStatisticsButton" runat="server" Text="Refresh Data" OnClick="RefreshStatisticsButton_Click"/>
																    </td>
																</tr>
															</table>
															</ContentTemplate>
															</asp:UpdatePanel>															
														</td>
													</tr>
													<tr>
														<td colspan="3">
															&nbsp;
															<iframe id="performanceFrame" name="performanceFrame" frameborder="0" scrolling="no"
																src="Performer.aspx" width="1%" style="display: none"></iframe>
														</td>
													</tr>
													<tr>
														<td colspan="3">
															<div class="settingsHeading1">
																Real Time Usage
															</div>
														</td>
													</tr>
													<tr>
														<td colspan="3">
															<asp:Panel ID="Panel5" runat="server" Style="position: relative; border: #C2CAD1 1px ridge;
																background-color: #394248; padding: 3px; white-space: nowrap;">
																<table border="0" cellpadding="3" cellspacing="0">
																	<tr>
																		<td class="settingsNormal">
																			<font color="white">Transport Protocol: </font>
																		</td>
																		<td valign="top">
																			<select id="protocol" onchange="updateGaugeLabels()">
																				<option value="HTTP">HTTP</option>
																				<option value="TCP">TCP</option>
																				<option value="namedPipe">NamedPipe</option>
																			</select>
																		</td>
																		<td>
																			&nbsp;&nbsp;
																		</td>
																		<td class="settingsNormal">
																			<font color="white">Chunk size (KB):</font>
																		</td>
																		<td>
																			<select id="chunksize">
																				<option value="256">256</option>
																				<option value="512" selected="selected">512</option>
																				<option value="1024">1024</option>
																				<option value="2048">2048</option>
																			</select>
																		</td>
																		<td>
																			&nbsp;&nbsp;
																		</td>
																		<td valign="top">
																			<input type="button" id="perform" value=" Perform Benchmarking" onclick="onPerformClick()" />
																		</td>
																		<td>
																			&nbsp;&nbsp;
																		</td>
																		<td valign="top">
																			<input type="button" id="updateConfig" value=" Update Configuration" onclick="onUpdateConfigClick()" />
																		</td>
																	</tr>
																</table>
															</asp:Panel>
														</td>
													</tr>
													<!-- Gauges go here -->
													<tr>
														<td>
															<table border="1" style="border-color: #C2CAD1;" cellpadding="4" cellspacing="0"
																width="100%">
																<tr>
																	<td class="settingsHeading2WithoutWeight" style="height: 20px;">
																		Data rate in KB
																	</td>
																</tr>
																<tr>
																	<td align="center" style="width: 250px; height: 250px">
																		<div id="gaugeDiv" style="width: 250; height: 250" />

																		<script type="text/javascript">

																			// Load the gauge into the div
																			bytesPerSecGauge = bindows.loadGaugeIntoDiv("bindows_gauges/bytes_per_sec.xml", "gaugeDiv");

																			// dynamically update the gauge at runtime
																			function updateGauge() {
																				PageMethods.GetDataRate(function(result) {
																					if (result) {
																						bytesPerSecGauge.needle.setValue(result.KBPS);
																						if (result.KBPS > maxKB)
																							maxKB = result.KBPS;

																						var strdata = 'Max: ' + maxKB + '   ';
																						bytesPerSecGauge.label.setText(strdata);
																					}
																				});
																			}

																			updateGauge();

																		</script>

																	</td>
																</tr>
															</table>
														</td>
														<td>
															<table border="1" style="border-color: #C2CAD1" cellpadding="4" cellspacing="0" width="100%">
																<tr>
																	<td class="settingsHeading2WithoutWeight" style="height: 20px;">
																		Data rate in MB
																	</td>
																</tr>
																<tr>
																	<td align="center" style="width: 250px; height: 250px">
																		<div id="gaugeDiv1" style="width: 250; height: 250" />

																		<script type="text/javascript">

																			// Load the gauge into the div
																			mbytesPerMinGauge = bindows.loadGaugeIntoDiv("bindows_gauges/mbytes_per_min.xml", "gaugeDiv1");

																			// dynamically update the gauge at runtime
																			function updateGauge1() {
																				PageMethods.GetDataRate(function(result) {
																					if (result) {
																						mbytesPerMinGauge.needle.setValue(result.MBPM);
																						if (result.MBPM > maxMB)
																							maxMB = result.MBPM;

																						var strdata = 'Max: ' + maxMB + '   ';
																						mbytesPerMinGauge.label.setText(strdata);
																					}
																				});
																			}

																			updateGauge1();
																		</script>

																	</td>
																</tr>
															</table>
														</td>
														<td>
															<table border="1" style="border-color: #C2CAD1;" cellpadding="4" cellspacing="0"
																width="100%">
																<tr>
																	<td class="settingsHeading2WithoutWeight" style="height: 20px;">
																		Percent complete
																	</td>
																</tr>
																<tr>
																	<td align="center" style="width: 250px; height: 250px">
																		<div id="gaugeDiv2" style="width: 250; height: 250" />

																		<script type="text/javascript">

																			// Load the gauge into the div
																			percentCompleteGauge = bindows.loadGaugeIntoDiv("bindows_gauges/percent_complete.xml", "gaugeDiv2");

																			// dynamically update the gauge at runtime
																			function updateGauge2() {
																				PageMethods.GetPercentComplete(function(result) {
																					if (result) {
																						percentCompleteGauge.needle.setValue(result.percentComplete);
																						var strdata = (result.percentComplete) + '   ';
																						percentCompleteGauge.label.setText(strdata);
																					}
																				});
																			}

																			updateGauge2();

																		</script>

																	</td>
																</tr>
															</table>
														</td>
														<td>
															<!-- Dummy gauge -->
															<table border="0" style="border-color: #C2CAD1; display: none" cellpadding="4" cellspacing="0">
																<tr>
																	<td align="center">
																		<div id="gaugeDiv3" style="width: 0; height: 0; display: none" />

																		<script type="text/javascript">

																			// Load the gauge into the div
																			var clock4 = bindows.loadGaugeIntoDiv("bindows_gauges/percent_complete.xml", "gaugeDiv3");

																		</script>

																	</td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td colspan="3">
															&nbsp;
														</td>
													</tr>
												</table>
											</div>
										</ContentTemplate>
									</ajaxToolkit:TabPanel>
									<!-- Comparison Data Tab -->
									<ajaxToolkit:TabPanel runat="server" HeaderText="Comparison Data">
										<ContentTemplate>
											<div style="display: block">
												<table border="0" cellpadding="0" cellspacing="0">
													<tr>
														<td>
															<br />
															<div class="settingsHeading1">
																Comparison Data
															</div>
															<br />
														</td>
													</tr>
													<tr>
														<td class="comparisonDataHeading1" style="height: 45px; padding-left: 15px">
															<asp:UpdatePanel ID="SelectionPanel" runat="server" UpdateMode="Conditional">
																<ContentTemplate>
																	<asp:Panel ID="Panel6" runat="server" Style="position: relative; background-color: #394248;
																		white-space: nowrap;">
																		<asp:Label runat="server" ID="SelectFormat" Text="View comparison data in: " />
																		<asp:DropDownList runat="server" ID="FormatList" AutoPostBack="true" Width="180"
																			OnSelectedIndexChanged="FormatList_SelectionChanged">
																			<asp:ListItem Text="Graphical Format" Value="Graphical Format" />
																			<asp:ListItem Text="Grid Format" Value="Grid Format" />
																		</asp:DropDownList>
																		&nbsp;&nbsp;
																		<asp:Button runat="server" ID="RefreshDateButton" Text="Refresh Date" OnClick="RefreshDateButton_Click"
																			UseSubmitBehavior="true" />
																		<asp:DropDownList runat="server" ID="DateFilterList" AutoPostBack="true" Width="210"
																			OnSelectedIndexChanged="DateFilterList_SelectionChanged">
																			<asp:ListItem Text="Show data for 30 days before" Value="Daily" Selected="True" />
																			<asp:ListItem Text="Show data for the year" Value="Monthly" />
																		</asp:DropDownList>
																		<asp:TextBox ID="EndTimeCalendar" runat="server" Width="120" OnTextChanged="Calendar_DateChanged" />
																		<asp:TextBox ID="EndTimeCalendarYear" runat="server" Width="120" OnTextChanged="Calendar_DateChanged"
																			Visible="false" />
																		<ajaxToolkit:CalendarExtender runat="server" ID="DataCalendar" TargetControlID="EndTimeCalendar"
																			Format="MM/dd/yyyy" PopupPosition="TopRight" />
																	</asp:Panel>
																</ContentTemplate>
															</asp:UpdatePanel>
														</td>
														<tr>
															<td>
																<!-- Grid View -->
																<div id="GridPanelDiv">
																	<asp:UpdatePanel ID="GridPanel" runat="server" UpdateMode="Conditional">
																		<ContentTemplate>
																			<div 
                                                                            style="height: 1px;">
																			</div>
																			<table border="1" cellpadding="3" cellspacing="0" width="100%" bordercolor="#C2CAD1">
																				<tr>
																					<td class="settingsHeading2" style="height: 45;">
																						<asp:Panel ID="Panel2" runat="server" Style="position: relative; background-color: #E6E9EB;
																							white-space: nowrap;">
																							<asp:Button runat="server" ID="RefreshTabularDataButton" Text="Refresh Data" OnClick="RefreshTabularDataButton_Click"
																								UseSubmitBehavior="true" />
																							<asp:DropDownList runat="server" ID="GridUsersList" OnSelectedIndexChanged="GridUsersList_SelectionChanged"
																								AutoPostBack="true" />
																							&nbsp;&nbsp;&nbsp;&nbsp;
																							<asp:Button runat="server" ID="PreviousDataPageButton" Text="Previous Page" OnClick="PreviousDataPageButton_Click"
																								UseSubmitBehavior="true" Width="100" />
																							<asp:Button runat="server" ID="NextDataPageButton" Text="Next Page" OnClick="NextDataPageButton_Click"
																								UseSubmitBehavior="true" Width="100" />
																							<asp:TextBox ID="PageIndexTextBox" runat="server" Text="1" Width="40" />
																							<asp:Button runat="server" ID="GoToPageButton" Text="Go To Page" OnClick="GoToPageButton_Click"
																								UseSubmitBehavior="true" Width="100" />
																							&nbsp;&nbsp;&nbsp;&nbsp;
																							<asp:Label runat="server" ID="PageStatusLabel" Height="10" CssClass="pageStatusLabel" />
																						</asp:Panel>
																					</td>
																				</tr>
																				<tr>
																					<td>
																						<asp:Panel ID="Panel3" runat="server" Style="position: relative; width: 800; min-height: 296px">
																							<asp:Table runat="server" ID="ComparisonDataTable" Width="800" border="1" CellPadding="0"
																								CellSpacing="0" BorderColor="#C2CAD1" Font-Size="X-Small">
																								<asp:TableHeaderRow runat="server" CssClass="settingsHeading2" Height="25">
																									<asp:TableHeaderCell ID="TableHeaderCell1" runat="server" Width="160">UserName</asp:TableHeaderCell>
																									<asp:TableHeaderCell ID="TableHeaderCell2" runat="server" Width="160">Date & Time</asp:TableHeaderCell>
																									<asp:TableHeaderCell ID="TableHeaderCell3" runat="server" Width="160">Redline (bytes)</asp:TableHeaderCell>
																									<asp:TableHeaderCell ID="TableHeaderCell4" runat="server" Width="160">Summary (bytes)</asp:TableHeaderCell>
																									<asp:TableHeaderCell ID="TableHeaderCell5" runat="server" Width="200">Total Execution Time (ms)</asp:TableHeaderCell>
																								</asp:TableHeaderRow>
																							</asp:Table>
																						</asp:Panel>
																					</td>
																				</tr>
																			</table>
																		</ContentTemplate>
																	</asp:UpdatePanel>
																</div>
																<!-- Graph View -->
																<div id="GraphPanelDiv">
																	<asp:UpdatePanel ID="GraphPanel" runat="server" UpdateMode="Conditional">
																		<ContentTemplate>
																			<div style="height: 1px;">
																			</div>
																			<table border="1" cellpadding="3" cellspacing="0" width="100%" bordercolor="#C2CAD1">
																				<tr>
																					<td class="settingsHeading2" style="height: 45;">
																						<asp:Panel ID="InputPanel" runat="server" Style="position: relative; background-color: #E6E9EB;
																							white-space: nowrap;">
																							<asp:Button runat="server" ID="RefreshGraphicalDataButton" Text="Refresh Data" OnClick="RefreshGraphicalDataButton_Click"
																								UseSubmitBehavior="true" />
																							<asp:DropDownList runat="server" ID="GraphUsersList" OnTextChanged="GraphUsersList_SelectionChanged"
																								AutoPostBack="true" />
																						</asp:Panel>
																					</td>
																				</tr>
																				<tr>
																					<td>
																						<asp:Panel ID="Panel1" runat="server" Style="position: relative; width: 800px; height: 350px">
																							<asp:Chart ID="timeChart" runat="server" Palette="BrightPastel" BackColor="#FFFFFF"
																								ImageType="Png" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" Width="800px"
																								Height="350px" BorderDashStyle="Solid" BackGradientStyle="None" BorderWidth="2"
																								BorderColor="181, 64, 1">
																								<Legends>
																									<asp:Legend Enabled="False" IsTextAutoFit="False" Name="Default" BackColor="Transparent"
																										Font="Trebuchet MS, 8.25pt, style=Bold">
																									</asp:Legend>
																								</Legends>
																								<BorderSkin SkinStyle="None"></BorderSkin>
																								<Series>
																									<asp:Series MarkerSize="8" BorderWidth="3" XValueType="DateTime" Name="Series1" ChartType="Line"
																										MarkerStyle="Circle" ShadowColor="#f69221" BorderColor="#f69221" Color="#f69221"
																										ShadowOffset="0" YValueType="Double">
																									</asp:Series>
																									<%--<asp:Series MarkerSize="9" BorderWidth="3" XValueType="Double" Name="Series2" ChartType="Line" MarkerStyle="Diamond" ShadowColor="Black" BorderColor="180, 26, 59, 105" Color="220, 224, 64, 10" ShadowOffset="2" YValueType="Double"></asp:Series>--%>
																								</Series>
																								<ChartAreas>
																									<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
																										BackSecondaryColor="White" BackColor="White" ShadowColor="Transparent" BackGradientStyle="TopBottom">
																										<Area3DStyle Rotation="25" Perspective="9" LightStyle="Realistic" Inclination="40"
																											IsRightAngleAxes="False" WallWidth="3" IsClustered="False" />
																										<AxisY LineColor="64, 64, 64, 64">
																											<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
																											<MajorGrid LineColor="64, 64, 64, 64" />
																										</AxisY>
																										<AxisX LineColor="64, 64, 64, 64">
																											<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
																											<MajorGrid LineColor="64, 64, 64, 64" />
																										</AxisX>
																									</asp:ChartArea>
																								</ChartAreas>
																							</asp:Chart>
																						</asp:Panel>
																					</td>
																				</tr>
																			</table>
																		</ContentTemplate>
																	</asp:UpdatePanel>
																</div>
															</td>
														</tr>
												</table>
											</div>
										</ContentTemplate>
									</ajaxToolkit:TabPanel>
									<!-- Settings Tab -->
									<ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="Settings">
										<ContentTemplate>
											<div style="display: block">
												<asp:UpdatePanel ID="ServiceSettings" runat="server" UpdateMode="Conditional">
													<ContentTemplate>
														<br />
														<table border="0" width="90%" cellpadding="4">
															<tr>
																<td>
																	<div class="settingsHeading1">
																		Compare Server Settings</div>
																</td>
															</tr>
															<tr>
																<td>
																	<asp:Panel ID="Panel4" runat="server" Style="position: relative; border: #C2CAD1 1px ridge;
																		background-color: #E6E9EB; padding: 3px; white-space: nowrap;">
																		<table>
																			<tr>
																				<td class="settingsHeading2WithoutBG">
																					HTTP Port :
																				</td>
																				<td>
																					<asp:TextBox ID="HttpPortTextBox" runat="server" Text="8080" Enabled="false" />
																				</td>
																				<td>
																					&nbsp;
																				</td>
																				<td class="settingsHeading2WithoutBG">
																				</td>
																				<td>
																				</td>
																			</tr>
																			<tr>
																				<td class="settingsHeading2WithoutBG">
																					TCP Port :
																				</td>
																				<td>
																					<asp:TextBox ID="TcpPortTextBox" runat="server" Text="8090" Enabled="false" />
																				</td>
																				<td>
																					&nbsp;
																				</td>
																				<td class="settingsHeading2WithoutBG">
																				</td>
																				<td>
																				</td>
																			</tr>
																			<tr>
																				<td class="settingsHeading2WithoutBG">
																					Service Version :
																				</td>
																				<td class="settingsNormal">
																					<asp:Label ID="ServiceVersionLabel" runat="server" />
																				</td>
																				<td>
																					&nbsp;
																				</td>
																				<td class="settingsHeading2WithoutBG">
																					Compositor Version:
																				</td>
																				<td class="settingsNormal">
																					<asp:Label ID="CompositorVersionLabel" runat="server" />
																				</td>
																			</tr>
																			<tr>
																				<td colspan="2">
																					<br />
																					<asp:Button runat="server" ID="DownloadSystemLogButton" Text="Download System Log"
																						OnClick="DownloadSystemLogButton_Click" UseSubmitBehavior="true" />
																				</td>
																			</tr>
																		</table>
																	</asp:Panel>
																</td>
															</tr>
														</table>
													</ContentTemplate>
												</asp:UpdatePanel>
												<br />
												<asp:UpdatePanel ID="ServiceStatus" runat="server" UpdateMode="Conditional">
													<ContentTemplate>
														<table width="90%">
															<tr>
																<td colspan="2" class="settingsHeading1">
																	Service Status
																</td>
															</tr>
															<tr>
																<td>
																	<asp:Panel ID="Panel7" runat="server" Style="position: relative; border: #C2CAD1 1px ridge;
																		background-color: #E6E9EB; padding: 3px; white-space: nowrap;">
																		<table>
																			<tr>
																				<td>
																					<asp:Label ID="ServiceStatusLabel" runat="server" Text="Getting service status ..."
																						CssClass="settingsNormal" />
																				</td>
																				<td style="width: 20px;">
																				</td>
																				<td>
																					<asp:Button ID="StartServiceButton" runat="server" Text="Start Service" OnClick="StartServiceButton_Click" />
																					<asp:Button ID="StopServiceButton" runat="server" Text="Stop Service" OnClick="StopServiceButton_Click" />
																					<asp:Button ID="RestartServiceButton" runat="server" Text="Restart Service" OnClick="RestartServiceButton_Click" />
																				</td>
																			</tr>
																		</table>
																	</asp:Panel>
																</td>
															</tr>
														</table>
													</ContentTemplate>
												</asp:UpdatePanel>
												<br />
												<table border="0" cellpadding="4" width="90%">
													<tr>
														<td colspan="2" class="settingsHeading1">
															Binding Settings
														</td>
													</tr>
													<tr>
														<td width="50%">
															<table style="border-color: #C2CAD1;" border="1" cellpadding="3" cellspacing="0"
																width="100%">
																<tr>
																	<td class="settingsHeading2">
																		HTTP Binding
																	</td>
																</tr>
																<tr>
																	<td>
																		<table border="0" cellpadding="2" cellspacing="0" width="100%">
																			<tr>
																				<td class="settingsNormal">
																					<b>Contract interface: </b>
																					<asp:Label ID="HttpContractInterface" runat="server" />
																				</td>
																			</tr>
																			<tr>
																				<td class="settingsNormal">
																					<b>URI: </b>
																					<asp:Label ID="HttpURI" runat="server" />
																				</td>
																			</tr>
																			<tr>
																				<td class="settingsNormal">
																					<b>Chunking supported:</b> Yes
																				</td>
																			</tr>
																			<tr>
																				<td class="settingsNormal">
																					<b>Chunking interface: </b>
																					<asp:Label ID="HttpChunkingInterface" runat="server" />
																				</td>
																			</tr>
																			<tr>
																				<td class="settingsNormal">
																					<b>Chunking URI: </b>
																					<asp:Label ID="HttpChunkingURI" runat="server" />
																				</td>
																			</tr>
																		</table>
																	</td>
																</tr>
															</table>
														</td>
														<td>
															<table style="border-color: #C2CAD1;" border="1" cellpadding="3" cellspacing="0"
																width="100%">
																<tr>
																	<td class="settingsHeading2">
																		TCP Binding
																	</td>
																</tr>
																<tr>
																	<td>
																		<table border="0" cellpadding="2" cellspacing="0" width="100%">
																			<tr>
																				<td class="settingsNormal">
																					<b>Contract interface: </b>
																					<asp:Label ID="TcpContractInterface" runat="server" />
																				</td>
																			</tr>
																			<tr>
																				<td class="settingsNormal">
																					<b>URI: </b>
																					<asp:Label ID="TcpURI" runat="server" />
																				</td>
																			</tr>
																			<tr>
																				<td class="settingsNormal">
																					<b>Chunking supported:</b> Yes
																				</td>
																			</tr>
																			<tr>
																				<td class="settingsNormal">
																					<b>Chunking interface: </b>
																					<asp:Label ID="TcpChunkingInterface" runat="server" />
																				</td>
																			</tr>
																			<tr>
																				<td class="settingsNormal">
																					<b>Chunking URI: </b>
																					<asp:Label ID="TcpChunkingURI" runat="server" />
																				</td>
																			</tr>
																		</table>
																	</td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td>
															<table style="border-color: #C2CAD1;" border="1" cellpadding="3" cellspacing="0"
																width="100%">
																<tr>
																	<td class="settingsHeading2">
																		Legacy HTTP Binding
																	</td>
																</tr>
																<tr>
																	<td>
																		<table border="0" cellpadding="2" cellspacing="0" width="100%">
																			<tr>
																				<td class="settingsNormal">
																					<b>Contract interface: </b>
																					<asp:Label ID="LegacyHttpContractInterface" runat="server" />
																				</td>
																			</tr>
																			<tr>
																				<td class="settingsNormal">
																					<b>URI: </b>
																					<asp:Label ID="LegacyHttpURI" runat="server" />
																				</td>
																			</tr>
																			<tr>
																				<td class="settingsNormal">
																					<b>Chunking supported: </b>No
																				</td>
																			</tr>
																			<tr>
																				<td class="settingsNormal" colspan="2">
																					&nbsp;
																				</td>
																			</tr>
																			<tr>
																				<td class="settingsNormal" colspan="2">
																					&nbsp;
																				</td>
																			</tr>
																		</table>
																	</td>
																</tr>
															</table>
														</td>
														<td>
															<table style="border-color: #C2CAD1;" border="1" cellpadding="3" cellspacing="0"
																width="100%">
																<tr>
																	<td class="settingsHeading2">
																		NamedPipe Binding
																	</td>
																</tr>
																<tr>
																	<td>
																		<table border="0" cellpadding="2" cellspacing="0" width="100%">
																			<tr>
																				<td class="settingsNormal">
																					<b>Contract interface: </b>
																					<asp:Label ID="NamedPipeContractInterface" runat="server" />
																				</td>
																			</tr>
																			<tr>
																				<td class="settingsNormal">
																					<b>URI: </b>
																					<asp:Label ID="NamedPipeURI" runat="server" />
																				</td>
																			</tr>
																			<tr>
																				<td class="settingsNormal">
																					<b>Chunking supported:</b> Yes
																				</td>
																			</tr>
																			<tr>
																				<td class="settingsNormal">
																					<b>Chunking interface: </b>
																					<asp:Label ID="NamedPipeChunkingInterface" runat="server" />
																				</td>
																			</tr>
																			<tr>
																				<td class="settingsNormal">
																					<b>Chunking URI: </b>
																					<asp:Label ID="NamedPipeChunkingURI" runat="server" />
																				</td>
																			</tr>
																		</table>
																	</td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</div>
										</ContentTemplate>
									</ajaxToolkit:TabPanel>
								</ajaxToolkit:TabContainer>
								<!-- Thats pretty much it -->
							</td>
						</tr>
					</table>
				</div>
			</td>
		</tr>
		<tr>
			<td>
				<div id="status" class="infopane" align="left" style="display: none">
				</div>
			</td>
		</tr>
	</table>
	<!-- Copyright Notice -->
	<table border="0" cellpadding="4" cellspacing="0" width="100%">
		<tr>
			<td>
				&nbsp;
			</td>
		</tr>
		<tr>
			<td class="copyrightNotice" style="width: 35px; height: 20px;">
				&nbsp;
			</td>
			<td class="copyrightNotice" style="width: 250px; height: 20px;">
				©2013 Workshare, Inc. All rights reserved.
			</td>
			<td class="copyrightNotice" style="width: 10px; height: 20px;">
				|
			</td>
			<td class="copyrightNotice" style="height: 20px;">
				<a href="http://www.workshare.com" target="_blank">www.workshare.com</a>
			</td>
		</tr>
		<tr>
			<td>
				&nbsp;
			</td>
		</tr>
	</table>
	</form>
</body>
</html>
