using MessageBoard.Core;
using MessageBoard.Core.MessageKind;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MessageBoard.SCRouveen.MessageKind
{
	public class DayOverviewMessageKind : KNVBDataMessageKind
	{
		public override string Title
		{
			get { return "KNVB Dag programma"; }
		}

		public override List<Core.MessageKind.MessageKindSetting> Settings
		{
			get
			{
				return new List<MessageKindSetting>()
				{
					MessageKindSetting.Create("FacilityId", "Locatie ID", SettingKind.Text),
					MessageKindSetting.Create("ClubNumber", "KNVB Clubnummer", SettingKind.Text)					
				};
			}
		}

		public virtual bool ShowDressingRoom { get { return true; } }

		public virtual bool ShowReferee { get { return true; } }

		protected object GetScheduleForDate(string token, int? weekNumber)
		{
			var query = new NameValueCollection();

			if (weekNumber.HasValue)
			{
				query.Add("weeknummer", weekNumber.ToString());
			}

			var url = GenerateAbsoluteUrl("/wedstrijden", query, token);
			return ExecuteRequest(url);
		}

		protected int GetIso8601WeekOfYear(DateTime time)
		{
			// Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
			// be the same week# as whatever Thursday, Friday or Saturday are,
			// and we always get those right
			DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
			if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
			{
				time = time.AddDays(3);
			}

			// Return the week of our adjusted day
			return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
		}

		public override string RenderStyling()
		{
			return @" .SCRouveenDayOverviewHeader {
									background-color: #008347;
									border-radius: 10px;	
									margin: 10px;
									padding-left: 10px;
									color: #fff;
									display: flex;
								}

								.SCRouveenDayOverviewHeader > .greetingTitle {
									flex:	1;
									text-align: left;
								}														

								.SCRouveenDayOverviewHeader > .lastSyncTime {
									flex:	1;
									padding-right: 10px;
									text-align: right;
									font-size: small;
								}									

								.SCRouveenDayOverviewHeader > span {	
									margin: auto;
									flex: 2;
									text-align: center;
								}

								.SCRouveenDayOverviewHeader a {
									color: rgba(255, 255, 255, 0.30); 
									font-size: xx-large;
									margin-left: 10px;
									margin-right: 10px;
								}

								.SCRouveenDayOverviewHeader a.currentDay {
									color: #FFF;
								}

								.SCRouveenDayOverviewHeader a:hover {
									text-decoration: none;
									color: #FFF;
								}
								

								.SCRouveenDayOverview {
									display: flex;				
									flex-wrap: wrap;
									font-size: large;
								}

								@media (max-width: 1919px) {
									.SCRouveenDayOverview {
										font-size: large;
									}
								}

								@media (min-width: 1920px) {
									.SCRouveenDayOverview {
										font-size: x-large;
									}
								}

								.SCRouveenDayOverview .item { 
									border-radius: 15px 15px 10px 10px;
									background-color: rgba(255, 255, 255, 0.80); 
									margin: 10px; 
									border: 1px solid #008347;
									float: left;
									position: relative;
									overflow: hidden;
								}

								.SCRouveenDayOverview .item .cancel
								{
									position: absolute;
									width: 100%;
									height: 100%;
									border-radius: 10px;	
									background-color: rgba(192,192,192,0.5); 
									color: #FF0000;
									text-align: center;
								}

								.SCRouveenDayOverview .item .cancel div
								{
									font-weight: bold;	
									font-size: 150px;								
								}

								@media (max-width: 1919px) {
									.SCRouveenDayOverview .item .cancel div
									{
										padding-top: 20px;										
									}
								}

								@media (min-width: 1920px) {
									.SCRouveenDayOverview .item .cancel div
									{
										padding-top: 50px;
									}
								}

								.SCRouveenDayOverview .item.six {
									width: calc((100% - 120px) / 6);
								}

								.SCRouveenDayOverview .item.five {
									width: calc((100% - 100px) / 5);
									/*width: 364px;*/
								}

								.SCRouveenDayOverview .item.four {
									width: calc((100% - 80px) / 4);
									/*width: 460px;*/
								}

								.SCRouveenDayOverview .item.three {
									width: calc((100% - 80px) / 3);
									/*width: 620px;*/
								}

								.SCRouveenDayOverview .header {
									border-radius: 10px 10px 0px 0px;
									padding-left: 10px;
									padding-right: 10px;
									color: #FFF;
									background-color: #008347;
									font-weight: bold;
								}

								.SCRouveenDayOverview .header p	 { 
									clear: both;
									height: 0px;
									margin: 0px;
								}

								.SCRouveenDayOverview .dataContainer {
									margin: 10px;
								}

								.SCRouveenDayOverview .time { 
									float: left;
								}

								.SCRouveenDayOverview .field { 
									float: right;
								}

								.SCRouveenDayOverview .homeClub p, .SCRouveenDayOverview .guestClub p { 									
									clear: both;
								}

								.SCRouveenDayOverview .homeClub, .SCRouveenDayOverview .guestClub {
									margin-bottom: 10px;
								}

								.SCRouveenDayOverview .homeClub .logo { 
									float: left;
									margin-right: 5px;
								}

								.SCRouveenDayOverview .homeClub .data { 
									float: left;
									/*word-break: break-all;*/
								}

								.SCRouveenDayOverview .guestClub .logo { 
									float: left;
									margin-right: 5px;
								}

								.SCRouveenDayOverview .guestClub .data { 
									float: left;
									/*word-break: break-all;*/
								}

								.SCRouveenDayOverview .spacer { 
									height: 15px;
								}";
		}

		public override string RenderGlobalScript(string dataUrl)
		{
			return @"messageBoard.SCRouveen = messageBoard.SCRouveen || {}
							 messageBoard.SCRouveen.DayOverview = messageBoard.SCRouveen.DayOverview || {
								getData: function(messageId, dataUrl)
								{
									var localDataUrl = dataUrl;
									if (document.location.search != '')
									{
										localDataUrl = localDataUrl + '&' + document.location.search.substr(1);
									}
									
									$.ajax(localDataUrl)
									.done(function(data) { messageBoard.SCRouveen.DayOverview.renderData(messageId, data, dataUrl); })
									.fail(function() { console.log(arguments); });
								},

								updateQueryStringParameter: function(uri, key, value) {
									var re = new RegExp('([?&])' + key + '=.*?(&|$)', 'i');
									var separator = uri.indexOf('?') !== -1 ? '&' : '?';
									if (uri.match(re)) {
										return uri.replace(re, '$1' + key + '=' + value + '$2');
									}
									else {
										return uri + separator + key + '=' + value;
									}
								},

								createUrl: function(dateAsString) 
								{
									return messageBoard.SCRouveen.DayOverview.updateQueryStringParameter(window.location.href, 'currentDate', dateAsString);
								},

								renderData: function(messageId, data, dataUrl)
								{
									var messageContainer = $('[data-role=""messageContainer""][data-value=""' + messageId + '""]');
									
									var renderedHtml = '';
									var matchCount = data.Matches.length;
									
									var itemCss = '';

									if (matchCount > 0)
									{
										if (matchCount > 15)
										{
											itemCss = 'six';
										} else if (matchCount > 12)
										{
											itemCss = 'five';
										} else if (matchCount > 9)
										{
											itemCss = 'four';
										} else {
											itemCss = 'three';
										}

										for (var i = 0; i < matchCount; i++)
										{
											var match = data.Matches[i];
											var cancelHtml = '';
											if (match.Cancelled)
											{
												cancelHtml = '<div class=""cancel""><div>X</div></div>';
											}
											
											var itemHtml = '<div class=""item ' + itemCss + '"">\
																				' + cancelHtml + '\
																				<div class=""header""><div class=""time"">' + match.Time + '</div><div class=""field"">' + match.Field + '</div><p /></div>\
																				<div class=""dataContainer"">';
											
											itemHtml += '<div class=""homeClub""><div class=""logo""></div><div class=""data""><strong>' + match.HomeClub + '</strong>';
											if (data.ShowDressingRoom && match.Result == '')
											{
												itemHtml += '<br/>' + match.HomeClubDressingRoom;
											}
											itemHtml += '</div><p /></div>';

											itemHtml += '<div class=""guestClub""><div class=""logo""></div><div class=""data""><strong>' + match.GuestClub + '</strong>';
											if (data.ShowDressingRoom && match.Result == '')
											{
												itemHtml += '<br/>' + match.GuestClubDressingRoom;
											}
											itemHtml += '</div><p /></div>';

											/* When the game is over no need to show the referee. */
											if (data.ShowReferee && match.Result == '')
											{
												itemHtml += '<div>Official: ' + match.Referee + '</div>';
											}
	
											/* When referee is not shown show always the result, otherwise the screen is a little bit empty. */
											if (match.Result != '' || data.ShowReferee == false)
											{
												itemHtml += 	'<div>Uitslag: ' + match.Result + '</div>';
											}
														
											itemHtml += 	'		</div>\
																			</div>';
											renderedHtml += itemHtml;
										}
									} else {
										renderedHtml = '<div class=""item"">\
																		<div class=""header"">Geen programma bekend</div>\
																		<div class=""dataContainer"">Er is geen programma bekend</div>\
																		</div>';
									}

									messageContainer.html(renderedHtml);

									$('[data-role=""matchDate""]').html(data.DateAsString);
									
									var currentDate = new Date(); 
									var currentDateAsString = currentDate.getDate() + '-' + (currentDate.getMonth()+1) + '-' + currentDate.getFullYear() + ' ' + ('00' + currentDate.getHours()).slice(-2) + ':' + ('00' + currentDate.getMinutes()).slice(-2);

									$('[data-role=""messageContainerLastSyncTime""]').html(currentDateAsString);
									$('[data-role=""previousDay""]').click(function()
									{
										var newUrl = messageBoard.SCRouveen.DayOverview.createUrl(data.PreviousDate);
										window.location.href = newUrl;
										return false;
									});

									$('[data-role=""currentDay""]').click(function()
									{
										var newUrl = messageBoard.SCRouveen.DayOverview.createUrl('');
										window.location.href = newUrl;
										return false;
									});

									$('[data-role=""nextDay""]').click(function()
									{
										var newUrl = messageBoard.SCRouveen.DayOverview.createUrl(data.NextDate);
										window.location.href = newUrl;
										return false;
									});

									setTimeout(function(){ messageBoard.SCRouveen.DayOverview.getData(messageId, dataUrl); }, 1000 * 60)
								}	
							 };";
		}

		public override string RenderInstanceScript(int messageId, MessageKindSettingList settings, string dataUrl)
		{
			var script = @"messageBoard.SCRouveen.DayOverview.getData(" + messageId + ", '" + dataUrl + "');";
			return script;
		}

		public override string RenderHTML(int messageId, MessageKindSettingList settings, Core.InformationKind.IInformationRepository informationRepository, string dataUrl)
		{
			var html = string.Format("<div><div class=\"SCRouveenDayOverviewHeader\"><span class=\"greetingTitle\">Welkom bij SC Rouveen!</span> <span class=\"matchDateTitle\"><a href=\"#\" data-role=\"previousDay\">&#8678;</a><a href=\"#\" data-role=\"currentDay\" class=\"currentDay\">Programma voor <span data-role=\"matchDate\"></span></a><a href=\"#\" data-role=\"nextDay\">&#8680;</a></span><span class=\"lastSyncTime\">Laatst bijgewerkt op:<br/><span data-role=\"messageContainerLastSyncTime\"></span></span></div><div data-role=\"messageContainer\" data-value=\"{0}\" class=\"SCRouveenDayOverview\"></div></div>", messageId);

			return html;
		}

		public class Result
		{
			public DateTime Date { get; set; }
			public string DateAsString { get; set; }
			public List<dynamic> Matches { get; set; }
			public string PreviousDate { get; set; }
			public string NextDate { get; set; }

			public bool ShowDressingRoom { get; set; }
			public bool ShowReferee { get; set; }
		}

		public override object GetData(int messageId, Core.MessageKind.MessageKindSettingList settings, NameValueCollection additionalData)
		{
			int? weekNumber = null;
			var currentDateOverride = (string)additionalData["currentDate"];
			var currentDate = DateTime.Now.Date;
			if (currentDateOverride != null)
			{
				try
				{
					var year = int.Parse(currentDateOverride.Substring(0, 4));
					var month = int.Parse(currentDateOverride.Substring(5, 2));
					var day = int.Parse(currentDateOverride.Substring(8, 2));
					currentDate = new DateTime(year, month, day);

					weekNumber = GetIso8601WeekOfYear(currentDate);
				}
				catch
				{

				}
			}

			var token = GenerateInitialization();
			dynamic data = GetScheduleForDate(token, weekNumber);

			var currentDateAsString = currentDate.ToString("yyyy-MM-dd");
			var result = new Result
			{
				Date = currentDate,
				DateAsString = currentDate.ToString("dddd d MMMM"),
				Matches = new List<dynamic>(),
				PreviousDate = currentDate.AddDays(-1).ToString("yyyy-MM-dd"),
				NextDate = currentDate.AddDays(1).ToString("yyyy-MM-dd"),

				ShowDressingRoom = ShowDressingRoom,
				ShowReferee = ShowReferee
			};

			if (data.errorcode == 9995)
			{
				// No data.
				return result;
			}

			if (data.errorcode != 1000)
			{
				// Error.. No data available.
				throw new Exception("Error: " + data.message);
			}

			var facilityId = settings["FacilityId"];
			var clubNumber = settings["ClubNumber"];			

			foreach (dynamic match in data.List)
			{
				if (match.Datum != currentDateAsString)
				{
					continue;
				}

				if (facilityId != null
						&& !string.IsNullOrEmpty(facilityId.StringValue)
						&& match.Facility_Id != facilityId.StringValue)
				{
					continue;
				}

				var time = (string)match.Tijd;
				if (!string.IsNullOrEmpty(time))
				{
					if (time.Length == 4)
					{
						time = string.Format("{0}:{1}", time.Substring(0, 2), time.Substring(2));
					}
				}

				var matchResult = "";
				var puntenTeam1 = (string)match.PuntenTeam1;
				var puntenTeam2 = (string)match.PuntenTeam2;
				if (!string.IsNullOrEmpty(puntenTeam1) && !string.IsNullOrEmpty(puntenTeam2))
				{
					matchResult = string.Format("{0} - {1}", puntenTeam1, puntenTeam2);
				}

				var referee = (string)match.Scheidsrechter;
				if (!string.IsNullOrEmpty((string)match.Kleedkamer_official))
				{
					var glue = string.IsNullOrEmpty(referee) ? "" : ", ";
					referee = string.Concat(referee, glue, match.Kleedkamer_official);
				}

				bool cancelled = false;
				bool discontinued = false;
				if (!string.IsNullOrEmpty((string)match.Bijzonderheden))
				{
					switch (((string)match.Bijzonderheden).ToUpper())
					{
						case "AFG":
						case "ADO":
						case "ADB":
						case "BNO":
						case "TNO":
						case "SNO":
						case "TAS":
						case "NOB":
							cancelled = true;
							break;
						case "GOB":
						case "GOT":
						case "GVS":
						case "GWO":
						case "GWT":
						case "GWW":
						case "GWVU":
						case "GSU":
							discontinued = true;
							break;
					}
				}

				if (discontinued)
				{
					matchResult = "Gestaakt";
				}

				var field = "";
				if (clubNumber == null // We only show field information for home matches.
						|| string.IsNullOrEmpty(clubNumber.StringValue)
						|| clubNumber.StringValue == (string)match.ThuisClubNummer)
				{
					field = !string.IsNullOrEmpty((string)match.VeldClub) ? match.VeldClub : match.VeldKNVB;
				}

				result.Matches.Add(new
					{
						Time = time,
						HomeClub = match.ThuisClub,
						HomeClubLogo = match.ThuisLogo,
						HomeClubDressingRoom = !string.IsNullOrEmpty((string)match.Kleedkamer_thuis) ? match.Kleedkamer_thuis : "Kleedkamer: -",
						GuestClub = match.UitClub,
						GuestClubLogo = match.UitLogo,
						GuestClubDressingRoom = !string.IsNullOrEmpty((string)match.Kleedkamer_uit) ? match.Kleedkamer_uit : "Kleedkamer: -",
						Referee = referee,
						Field = field,
						Result = matchResult,
						Cancelled = cancelled
					});
			}

			result.Matches = result.Matches.OrderBy(m => m.Time)
					.ThenBy(m => m.HomeClub)
					.ToList();

			return result;
		}
	}
}

