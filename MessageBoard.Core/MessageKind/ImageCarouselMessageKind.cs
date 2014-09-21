using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoard.Core.MessageKind
{
	public class ImageCarouselMessageKind : MessageKind
	{
		public override string Title
		{
			get { return "Afbeelding slide show"; }
		}

		public override List<MessageKindSetting> Settings
		{
			get
			{
				return new List<MessageKindSetting>()
				{
					MessageKindSetting.Create("InformationHeader", "Afbeeldingen", SettingKind.Information)
				};
			}
		}

		public override string RenderGlobalScript()
		{
			return @"messageBoard.messages.imageCarouselMessageKind = messageBoard.messages.imageCarouselMessageKind || 
								{
									init: function()
									{
										var imageCarousels = $('div[data-role=""MessageBoard.Core.MessageKind.ImageCarouselMessageKind""]');
										imageCarousels.each(function()
										{
											var imageCarousel = $(this);

											var availableImages = $('img', imageCarousel);
											if (availableImages.length == 0)
											{
												return;
											}

											if (availableImages.length > 1)
											{
												messageBoard.messages.imageCarouselMessageKind.showNextImage(imageCarousel);
											}
											else 
											{
												$(availableImages[0]).show();
											}
										});
									},

									showNextImage: function(imageCarousel)
									{
										var availableImages = $('img', imageCarousel);
										var currentVisibleImage = availableImages.filter(':visible');
										var currentImageIndex = -1;
										if (currentVisibleImage != undefined && currentVisibleImage != null)
										{
											currentImageIndex = availableImages.index(currentVisibleImage);
										}

										if (currentImageIndex == -1 || currentImageIndex == (availableImages.length - 1))
										{
											currentImageIndex = 0;
										}
										else
										{
											currentImageIndex += 1;
										}	

										messageBoard.messages.imageCarouselMessageKind.toggleImage(currentVisibleImage, $(availableImages[currentImageIndex]));

										setTimeout(function() { messageBoard.messages.imageCarouselMessageKind.showNextImage(imageCarousel);  }, 5000);
									},

									toggleImage: function(imageToHide, imageToShow)
									{										
										console.log(imageToHide);
										if (imageToHide != undefined && imageToHide != null && imageToHide.length > 0)
										{
											imageToHide.fadeToggle('slow', 'linear', function () {
												imageToShow.fadeToggle('slow', 'linear');
											});
										}
										else
										{
											imageToShow.show();
										}
									}
								};

								$(function() { messageBoard.messages.imageCarouselMessageKind.init(); } );";
		}

		public override string RenderHTML(int messageId, MessageKindSettingList settings, InformationKind.IInformationRepository informationRepository)
		{
			var informationHeaderSetting = settings["InformationHeader"];
			if (informationHeaderSetting == null || informationHeaderSetting.IntValue == 0)
			{
				return string.Empty;
			}

			var informationKind = informationRepository.RetrieveInformationKind(informationHeaderSetting.IntValue);
			if (informationKind == null)
			{
				return string.Empty;
			}

			var informationData = informationRepository.RetrieveData(informationHeaderSetting.IntValue);
			var informationHTML = informationKind.RenderHTML(informationData);

			if (string.IsNullOrWhiteSpace(informationHTML))
			{
				return string.Empty;
			}

			return string.Format("<div data-role=\"MessageBoard.Core.MessageKind.ImageCarouselMessageKind\" data-id=\"{0}\">{1}</div>", messageId, informationHTML);
		}
	}
}
