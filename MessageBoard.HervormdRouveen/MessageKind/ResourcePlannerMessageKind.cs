using MessageBoard.Core;
using MessageBoard.Core.MessageKind;
using System.Collections.Generic;
using MessageBoard.Core.InformationKind;
using System.Text;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System;
using System.Configuration;

namespace MessageBoard.HervormdRouveen.MessageKind
{
	public class ResourcePlannerMessageKind : Core.MessageKind.MessageKind
	{
		public override List<MessageKindSetting> Settings
		{
			get
			{
				return new List<MessageKindSetting>
				{
					MessageKindSetting.Create("Title", "Titel", SettingKind.Text)
				};
			}
		}

		public override string Title
		{
			get
			{
				return "ResourcePlanner";
			}
		}

		public override string RenderGlobalScript(string dataUrl)
		{
			var result = @"messageBoard.HervormdRouveen = messageBoard.HervormdRouveen || {};
										 messageBoard.HervormdRouveen.ResourcePlanner = messageBoard.HervormdRouveen.ResourcePlanner || {
												refreshData: function(messageId, dataUrl)
												{
													$.ajax(dataUrl).done(function(data)
													{
														var table = $('[data-role=""resourcePlannerData""][data-value=""' + messageId + '""]');
															var html = '';
															if (data.length > 0)
															{
																for (var i = 0; i < data.length; i++)
																{
																	var allocation = data[i];
																	var start = allocation.Start;
																  var resourceDescription = allocation.ResourceDescription;
																	var description = allocation.Description;
																	var remark = allocation.Remark;
																	
																	html += '<tr><td style=""width: 120px;"">' + start + '</td><td style=""width: 200px;"">' + resourceDescription + '</td><td>' + description + '</td></tr>';

																	if (remark.length > 0)
																	{
																		html += '<tr><td>&nbsp;</td><td>&nbsp;</td><td style=""font-style: italic;"">' + remark + '</td><tr>';
																	}
																}
															}

															table.html(html);
													}).always(function()
													{
														setTimeout(function() { messageBoard.HervormdRouveen.ResourcePlanner.refreshData(messageId, dataUrl); }, 60 * 1000);
													});

												}
										};";

			return result.ToString();
		}

		public override string RenderInstanceScript(int messageId, MessageKindSettingList settings, string dataUrl)
		
		{
			var result = new StringBuilder();

			result.Append("$(function() { messageBoard.HervormdRouveen.ResourcePlanner.refreshData(" + messageId + ", '" + dataUrl + "'); } );");

			return result.ToString();
		}

		public override string RenderHTML(int messageId, MessageKindSettingList settings, IInformationRepository informationRepository, string dataUrl)
		{
			var title = string.Empty;
			var titleSetting = settings["Title"];
			if (titleSetting != null && !string.IsNullOrEmpty(titleSetting.StringValue))
			{
				title = titleSetting.StringValue;
			}

			var stringBuilder = new StringBuilder();
			stringBuilder.Append("<div class=\"panel panel-primary\">");
			if (!string.IsNullOrEmpty(title))
			{
				stringBuilder.Append(string.Format("<div class=\"panel-heading\">{0}</div>", title));
			}

			stringBuilder.Append("<table data-role=\"resourcePlannerData\" data-value=\"" + messageId + "\" style=\"width: 100%;margin: 10px;\"></table>");

			stringBuilder.Append("</div>");

			return stringBuilder.ToString();
		}

		public override object GetData(int messageId, MessageKindSettingList settings, NameValueCollection additionalData)
		{
			var result = new List<Allocation>();
			using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ResourcePlanner"].ConnectionString))
			{
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText = @"SELECT a.[Start], a.[Description], a.[Remark], r.[Description] ResourceDescription
FROM[Allocation] a
INNER JOIN[Resource] r
ON a.[ResourceId] = r.[ResourceId]
WHERE CONVERT(date, a.[Start]) = @CurrentDate
	AND a.[End] >= @CurrentDateTime
ORDER BY a.[Start], r.[Description]";
					command.Parameters.AddWithValue("CurrentDate", DateTimeOffset.Now.Date);
					command.Parameters.AddWithValue("CurrentDateTime", DateTimeOffset.Now);

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var allocation = new Allocation
							{
								Start = ((DateTimeOffset)reader["Start"]).ToString("HH:mm"),
								Description = (string)reader["Description"] as string ?? string.Empty,
								Remark = reader["Remark"] as string ?? string.Empty,
								ResourceDescription = (reader["ResourceDescription"] as string) ?? string.Empty
							};

							result.Add(allocation);
						}
					}
				}					

				connection.Close();
			}
			return result;
		}

		class Allocation
		{
			public string Start { get; set; }
			public string Description { get; set; }
			public string Remark { get; set; }

			public string ResourceDescription { get; set; }
		}
	}
}
