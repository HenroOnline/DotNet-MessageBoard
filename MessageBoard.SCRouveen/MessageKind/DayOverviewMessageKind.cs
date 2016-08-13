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
					MessageKindSetting.Create("FacilityId", "Locatie ID", SettingKind.Text)
				};
			}
		}

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
								}

								.SCRouveenDayOverviewHeader > div {
									float: right;
									padding-right: 10px;
								}									

								.SCRouveenDayOverview {
									display: flex;				
									flex-wrap: wrap;
									font-size: x-large;
								}

								.SCRouveenDayOverview .item { 
									border-radius: 15px 15px 10px 10px;
									background-color: rgba(255, 255, 255, 0.80); 
									margin: 10px; 
									border: 1px solid #008347;
									float: left;
									position: relative;
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
									padding-top: 50px;
									font-size: 150px;
									font-weight: bold;									
								}

								.SCRouveenDayOverview .item.six {
									width: 300px;
								}

								.SCRouveenDayOverview .item.five {
									width: 364px;
								}

								.SCRouveenDayOverview .item.four {
									width: 460px;
								}

								.SCRouveenDayOverview .item.three {
									width: 620px;
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
								}

								.SCRouveenDayOverview .guestClub .logo { 
									float: left;
									margin-right: 5px;
								}

								.SCRouveenDayOverview .guestClub .data { 
									float: left;
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

								renderData: function(messageId, data, dataUrl)
								{
									var messageContainer = $('[data-role=""messageContainer""][data-value=""' + messageId + '""]');
									
									var renderedHtml = '';
									var matchCount = data.length;
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
											var cancelHtml = '';
											if (data[i].Cancelled)
											{
												cancelHtml = '<div class=""cancel""><div>X</div></div>';
											}

											var itemHtml = '<div class=""item ' + itemCss + '"">\
																				' + cancelHtml + '\
																				<div class=""header""><div class=""time"">' + data[i].Time + '</div><div class=""field"">' + data[i].Field + '</div><p /></div>\
																				<div class=""dataContainer"">\
																					<div class=""homeClub""><div class=""logo""></div><div class=""data""><strong>' + data[i].HomeClub + '</strong><br/>' + data[i].HomeClubDressingRoom + '</div><p /></div>\
																					<div class=""guestClub""><div class=""logo""></div><div class=""data""><strong>' + data[i].GuestClub + '</strong><br/>' + data[i].GuestClubDressingRoom + '</div><p /></div>\
																					<div>Official: ' + data[i].Referee + '</div>\
																					<div>Uitslag: ' + data[i].Result + '</div>\
																				</div>\
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
									var currentDate = new Date(); 
									var currentDateAsString = currentDate.getDate() + '-' + (currentDate.getMonth()+1) + '-' + currentDate.getFullYear() + ' ' + ('00' + currentDate.getHours()).slice(-2) + ':' + ('00' + currentDate.getMinutes()).slice(-2);

									$('[data-role=""messageContainerHeaderTime""]').html(currentDateAsString);

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
			var html = string.Format("<div><div class=\"SCRouveenDayOverviewHeader\">Welkom bij SC Rouveen! <div data-role=\"messageContainerHeaderTime\"></div></div><div data-role=\"messageContainer\" data-value=\"{0}\" class=\"SCRouveenDayOverview\"></div></div>", messageId);

			return html;
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

			var result = new Collection<dynamic>();

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

			var currentDateAsString = currentDate.ToString("yyyy-MM-dd");
			
			var facilityId = settings["FacilityId"];

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
				

				result.Add(new
					{
						Time = time,
						HomeClub = match.ThuisClub,
						HomeClubLogo = match.ThuisLogo,
						HomeClubDressingRoom = !string.IsNullOrEmpty((string)match.Kleedkamer_thuis) ? match.Kleedkamer_thuis : "Kleedkamer: -",
						GuestClub = match.UitClub,
						GuestClubLogo = match.UitLogo,
						GuestClubDressingRoom = !string.IsNullOrEmpty((string)match.Kleedkamer_uit) ? match.Kleedkamer_uit : "Kleedkamer: -",
						Referee = referee,
						Field = !string.IsNullOrEmpty((string)match.VeldClub) ? match.VeldClub : match.VeldKNVB,
						Result = matchResult,
						Cancelled = cancelled
					});
			}

			return result.OrderBy(m => m.Time)
					.ThenBy(m => m.HomeClub);
		}
	}
}

