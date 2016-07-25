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
									border-radius: 25px;	
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
								}

								.SCRouveenDayOverview .item { 
									border-radius: 25px;
									background-color: #FFF; 
									margin: 10px; 
									float: left;
								}
								.SCRouveenDayOverview .header {
									border-radius: 15px 15px 0px 0px;
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
								}

							 .SCRouveenDayOverview img {
									max-width: 68px;
									max-height: 68px;
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
									for (var i = 0; i < data.length; i++)
									{
										var itemHtml = '<div class=""item"">\
																			<div class=""header""><div class=""time"">' + data[i].Time + '</div><div class=""field"">' + data[i].Field + '</div><p /></div>\
																			<div class=""dataContainer"">\
																				<div class=""homeClub""><div class=""logo""><img src=""' + data[i].HomeClubLogo + '"" alt="""" /></div><div class=""data"">' + data[i].HomeClub + '<br/>KK: ' + data[i].HomeClubDressingRoom + '</div><p /></div>\
																				<div class=""guestClub""><div class=""logo""><img src=""' + data[i].GuestClubLogo + '"" alt="""" /></div><div class=""data"">' + data[i].GuestClub + '<br/>KK: ' + data[i].GuestClubDressingRoom + '</div><p /></div>\
																				<div>Scheidsrechter: ' + data[i].Referee + '</div>\
																			</div>\
																		</div>';
										renderedHtml += itemHtml;
									}

									messageContainer.html(renderedHtml);
									var currentDate = new Date(); 
									var currentDateAsString = currentDate.getDate() + '-' + (currentDate.getMonth()+1) + '-' + currentDate.getFullYear() + ' ' + currentDate.getHours() + ':' + currentDate.getMinutes();

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
				
				result.Add(new
					{
						Time = time,
						HomeClub = match.ThuisClub,
						HomeClubLogo = match.ThuisLogo,
						HomeClubDressingRoom = match.Kleedkamer_thuis,
						GuestClub = match.UitClub,
						GuestClubLogo = match.UitLogo,
						GuestClubDressingRoom  = match.Kleedkamer_uit,
						Referee = match.Scheidsrechter,
						RefereeDressingRoom = match.Kleedkamer_official,
						Field = !string.IsNullOrEmpty((string)match.VeldClub) ? match.VeldClub : match.VeldKNVB
					});				
			}

			return result.OrderBy(m => m.Time)
					.ThenBy(m => m.HomeClub);
		}
	}
}
