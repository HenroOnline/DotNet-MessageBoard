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

		private const string SettingKeyHomeFacilityDescription = "HomeFacilityDescription";
		private const string SettingKeyTitle = "Title";
		private const string SettingKeyShowHomeMatches = "ShowHomeMatches";
		private const string SettingKeyShowAwayMatches = "ShowAwayMatches";
		private const string SettingKeyShowDressingRoom = "ShowDressingRoom";
		private const string SettingKeyShowReferee = "ShowReferee";

		public override List<Core.MessageKind.MessageKindSetting> Settings
		{
			get
			{
				return new List<MessageKindSetting>()
				{
					MessageKindSetting.Create(SettingKeyHomeFacilityDescription, "Thuis accommodatie", SettingKind.Text),
					MessageKindSetting.Create(SettingKeyTitle, "Titel", SettingKind.Text),
					MessageKindSetting.Create(SettingKeyShowHomeMatches, "Toon thuiswedstrijden", SettingKind.Boolean),
					MessageKindSetting.Create(SettingKeyShowAwayMatches, "Toon uitwedstrijden", SettingKind.Boolean),
					MessageKindSetting.Create(SettingKeyShowDressingRoom, "Toon kleedkamer", SettingKind.Boolean),
					MessageKindSetting.Create(SettingKeyShowReferee, "Toon scheidsrechter", SettingKind.Boolean)
				};
			}
		}

		protected object GetScheduleForDate(int weekOffset)
		{
			var query = new NameValueCollection();

			query.Add("weekoffset", weekOffset.ToString());
			// API is not always reliable when it comes to home and away matches.
			//query.Add("thuis", showHomeMatches ? KNVBDataMessageKind.BooleanYes : KNVBDataMessageKind.BooleanNo);
			//query.Add("uit", showAwayMatches ? KNVBDataMessageKind.BooleanYes : KNVBDataMessageKind.BooleanNo);

			var url = GenerateAbsoluteUrl("/programma", query);
			return ExecuteRequest(url);
		}

		protected object GetResultsForDate(int weekOffset)
		{
			var query = new NameValueCollection();

			query.Add("weekoffset", weekOffset.ToString());
			// API is not always reliable when it comes to home and away matches.
			//query.Add("thuis", showHomeMatches ? KNVBDataMessageKind.BooleanYes : KNVBDataMessageKind.BooleanNo);
			//query.Add("uit", showAwayMatches ? KNVBDataMessageKind.BooleanYes : KNVBDataMessageKind.BooleanNo);

			var url = GenerateAbsoluteUrl("/uitslagen", query);
			return ExecuteRequest(url);
		}

		protected object GetCancellationsForDate(int weekOffset)
		{
			var query = new NameValueCollection();

			query.Add("weekoffset", weekOffset.ToString());
			
			var url = GenerateAbsoluteUrl("/afgelastingen", query);
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

									$('[data-role=""matchDate""][data-value=""' + messageId + '""]').html((data.Title + ' ' + data.DateAsString).trim());
									
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
			var html = string.Format("<div><div class=\"SCRouveenDayOverviewHeader\"><span class=\"greetingTitle\">Welkom bij SC Rouveen!</span> <span class=\"matchDateTitle\"><a href=\"#\" data-role=\"previousDay\">&#8678;</a><a href=\"#\" data-role=\"currentDay\" class=\"currentDay\"><span data-role=\"matchDate\" data-value=\"{0}\"></span></a><a href=\"#\" data-role=\"nextDay\">&#8680;</a></span><span class=\"lastSyncTime\">Laatst bijgewerkt op:<br/><span data-role=\"messageContainerLastSyncTime\"></span></span></div><div data-role=\"messageContainer\" data-value=\"{0}\" class=\"SCRouveenDayOverview\"></div></div>", messageId);

			return html;
		}

		private bool GetBooleanValue(Core.MessageKind.MessageKindSettingList settings, string settingKey)
		{
			var result = false;
			var resultSetting = settings[settingKey];
			if (resultSetting != null)
			{
				result = resultSetting.BooleanValue;
			}
			return result;
		}

		private string GetStringValue(Core.MessageKind.MessageKindSettingList settings, string settingKey)
		{
			var result = string.Empty;
			var resultSetting = settings[settingKey];
			if (resultSetting != null)
			{
				result = resultSetting.StringValue;
			}

			return result;
		}

		public override object GetData(int messageId, Core.MessageKind.MessageKindSettingList settings, NameValueCollection additionalData)
		{
			int weekOffset = 0;
			var currentDateOverride = (string)additionalData["currentDate"];
			var dateToShow = DateTime.Now.Date;
			if (currentDateOverride != null)
			{
				try
				{
					var year = int.Parse(currentDateOverride.Substring(0, 4));
					var month = int.Parse(currentDateOverride.Substring(5, 2));
					var day = int.Parse(currentDateOverride.Substring(8, 2));
					dateToShow = new DateTime(year, month, day);

					if (dateToShow != DateTime.Now.Date)
					{
						var calculateWeekOffsetStartDate = DateTime.Now.Date;
						if (dateToShow > DateTime.Now.Date)
						{
							while (calculateWeekOffsetStartDate.AddDays(7) <= dateToShow)
							{
								calculateWeekOffsetStartDate = calculateWeekOffsetStartDate.AddDays(7);

								weekOffset += 1;
							}
						}
						else
						{
							while (calculateWeekOffsetStartDate >= dateToShow)
							{
								calculateWeekOffsetStartDate = calculateWeekOffsetStartDate.AddDays(-7);

								weekOffset -= 1;
							}
						}
					}
				}
				catch
				{

				}
			}

			var homeFacilityDescription = GetStringValue(settings, SettingKeyHomeFacilityDescription);
			var showHomeMatches = GetBooleanValue(settings, SettingKeyShowHomeMatches);
			var showAwayMatches = GetBooleanValue(settings, SettingKeyShowAwayMatches);
			
			var showDressingRoom = GetBooleanValue(settings, SettingKeyShowDressingRoom);
			var showReferee = GetBooleanValue(settings, SettingKeyShowReferee);

			var title = GetStringValue(settings, SettingKeyTitle);

			var dateToShowAsString = dateToShow.ToString("yyyy-MM-dd 00:00:00.00");
			var result = new Result
			{
				Title = title,
				Date = dateToShow,
				DateAsString = dateToShow.ToString("d MMMM"),
				Matches = new List<Match>(),
				PreviousDate = dateToShow.AddDays(-1).ToString("yyyy-MM-dd"),
				NextDate = dateToShow.AddDays(1).ToString("yyyy-MM-dd"),

				ShowDressingRoom = showDressingRoom,
				ShowReferee = showReferee
			};

			var matches = GetMatches(weekOffset, dateToShowAsString, homeFacilityDescription, showHomeMatches, showAwayMatches);

			UpdateResults(matches, weekOffset, dateToShowAsString, homeFacilityDescription, showHomeMatches, showAwayMatches);

			// De response van afgelastingen toon alleen een datum in een display formaat. Bijv. 14 jul.
			UpdateCancelled(matches, weekOffset, dateToShow.ToString("dd MMM"), homeFacilityDescription, showHomeMatches, showAwayMatches);

			result.Matches = matches.OrderBy(m => m.Time)
					.ThenBy(m => m.HomeClub)
					.ToList();

			return result;
		}

		public List<Match> GetMatches(int weekOffset, 
			string dateAsString,
			string homeFacilityDescription,
			bool showHomeMatches, 
			bool showAwayMatches)
		{
			var result = new List<Match>();
			dynamic data = GetScheduleForDate(weekOffset);
			foreach (dynamic match in data)
			{
				if (match.kaledatum != dateAsString)
				{
					continue;
				}

				// API is not always reliable when it comes to home and away matches.
				var isHomeMatch = string.Equals((string)match.accommodatie, homeFacilityDescription, StringComparison.InvariantCultureIgnoreCase);

				if (isHomeMatch && showHomeMatches == false)
				{
					continue;
				}

				if (!isHomeMatch && showAwayMatches == false)
				{
					continue;
				}

				var time = (string)match.aanvangstijd;

				var referee = (string)match.scheidsrechter;
				if (!string.IsNullOrEmpty((string)match.kleedkamerscheidsrechter))
				{
					var glue = string.IsNullOrEmpty(referee) ? "" : ", ";
					referee = string.Concat(referee, glue, match.kleedkamerscheidsrechter);
				}
				var field = string.Empty;
				if (isHomeMatch)
				{
					field = (string)match.veld;
				}

				result.Add(new Match
				{
					MatchId = match.wedstrijdcode,
					Time = time,
					HomeClub = match.thuisteam,
					HomeClubDressingRoom = !string.IsNullOrEmpty((string)match.kleedkamerthuisteam) ? match.kleedkamerthuisteam : "Kleedkamer: -",
					GuestClub = match.uitteam,
					GuestClubDressingRoom = !string.IsNullOrEmpty((string)match.kleedkameruitteam) ? match.kleedkameruitteam : "Kleedkamer: -",
					Referee = referee,
					Field = field,
					Result = string.Empty
				});
			}

			return result;
		}

		private void UpdateResults(List<Match> matches, 
			int weekOffset,
			string dateAsString,
			string homeFacilityDescription,
			bool showHomeMatches,
			bool showAwayMatches)
		{
			dynamic data = GetResultsForDate(weekOffset);

			var addedMatches = new List<Match>();
			foreach (dynamic matchResult in data)
			{
				// API is not always reliable when it comes to home and away matches.
				var isHomeMatch = string.Equals((string)matchResult.accommodatie, homeFacilityDescription, StringComparison.InvariantCultureIgnoreCase);

				if (isHomeMatch && showHomeMatches == false)
				{
					continue;
				}

				if (!isHomeMatch && showAwayMatches == false)
				{
					continue;
				}

				int wedstrijdCode = matchResult.wedstrijdcode;
				var match = matches.FirstOrDefault(m => m.MatchId == wedstrijdCode);
				if (match == null && matchResult.datum == dateAsString)
				{
					match = new Match
					{
						MatchId = wedstrijdCode,
						Time = matchResult.aanvangstijd,
						HomeClub = matchResult.thuisteam,
						GuestClub = matchResult.uitteam
					};
					addedMatches.Add(match);
				}

				if (match == null)
				{
					continue;
				}

				match.Result = matchResult.uitslag;
			}

			matches.AddRange(addedMatches);
		}

		private void UpdateCancelled(List<Match> matches,
			int weekOffset,
			string dateAsString,
			string homeFacilityDescription,
			bool showHomeMatches,
			bool showAwayMatches)
		{
			dynamic data = GetCancellationsForDate(weekOffset);

			var addedMatches = new List<Match>();
			foreach (dynamic cancelledMatch in data)
			{
				var isHomeMatch = string.Equals((string)cancelledMatch.accommodatie, homeFacilityDescription, StringComparison.InvariantCultureIgnoreCase);

				if (isHomeMatch && showHomeMatches == false)
				{
					continue;
				}

				if (!isHomeMatch && showAwayMatches == false)
				{
					continue;
				}

				int wedstrijdCode = cancelledMatch.wedstrijdcode;
				var match = matches.FirstOrDefault(m => m.MatchId == wedstrijdCode);
				if (match == null && string.Equals(cancelledMatch.datum, dateAsString, StringComparison.InvariantCultureIgnoreCase))
				{
					match = new Match
					{
						MatchId = wedstrijdCode,
						Time = cancelledMatch.aanvangstijd,
						HomeClub = cancelledMatch.thuisteam,
						GuestClub = cancelledMatch.uitteam
					};
					addedMatches.Add(match);
				}

				if (match == null)
				{
					continue;
				}

				match.Cancelled = true;
			}

			matches.AddRange(addedMatches);
		}

		public class Result
		{
			public string Title { get; set; }
			public DateTime Date { get; set; }
			public string DateAsString { get; set; }
			public List<Match> Matches { get; set; }
			public string PreviousDate { get; set; }
			public string NextDate { get; set; }

			public bool ShowDressingRoom { get; set; }
			public bool ShowReferee { get; set; }
		}

		public class Match
		{
			public Match()
			{
				Referee = string.Empty;
				Time = string.Empty;
				HomeClubDressingRoom = string.Empty;
				GuestClubDressingRoom = string.Empty;
				Field = string.Empty;
				Result = string.Empty;
			}
			
			public int MatchId { get; set; }
			public string Time { get; set; }
			public string HomeClub { get; set; }
			public string HomeClubDressingRoom { get; set; }
			public string GuestClub { get; set; }
			public string GuestClubDressingRoom { get; set; }
			public string Referee { get; set; }
			public string Field { get; set; }
			public string Result { get; set; }
			public bool Cancelled { get; set; }
		}
	}
}

