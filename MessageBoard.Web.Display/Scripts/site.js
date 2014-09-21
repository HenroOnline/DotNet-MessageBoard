var messageBoard = messageBoard ||
	{
		init: function(currentBoardId, managementBaseUrl, displayUrl)
		{
			$.connection.hub.url = managementBaseUrl + "/signalr/hubs";

			var content = $.connection.contentHub;
			
			content.client.refreshContent = function (boardId) {
				// Add the message to the page.
				if (boardId == 0 || boardId == currentBoardId)
				{
					messageBoard.refreshPage(displayUrl);
				}
			};

			// Start the connection.
			$.connection.hub.start();
			
			$.connection.hub.disconnected(function() {
				setTimeout(function() {
					$.connection.hub.start();
				}, 5000); // Restart connection after 5 seconds.
			});
		},

		refreshPage: function (url) {
			// Prevent caching...
			window.location.href = url + '?ts=' + new Date().getTime();
		}
	};

messageBoard.slides = messageBoard.slides ||
	{
		init: function () {
			var availableSlides = $("div[data-slide]");
			var currentVisibleSlide = availableSlides.filter(":visible");			
			messageBoard.slides.setVisibleTimeout(currentVisibleSlide);
		},

		gotoNextSlide: function () {
			var availableSlides = $("div[data-slide]");
			var currentVisibleSlide = availableSlides.filter(":visible");

			var currentIndex = availableSlides.index(currentVisibleSlide);

			if (currentIndex == availableSlides.length - 1) {
				messageBoard.slides.switchSlide(currentVisibleSlide, $(availableSlides[0]));
			}
			else {
				messageBoard.slides.switchSlide(currentVisibleSlide, $(availableSlides[currentIndex + 1]));
			}
		},


		switchSlide: function (oldSlide, newSlide) {
			oldSlide.fadeToggle("slow", "linear", function () {
				newSlide.fadeToggle("slow", "linear", function () {
					messageBoard.slides.setVisibleTimeout(newSlide);
				});
			});
		},

		setVisibleTimeout: function (slide) {			
			var durationInSeconds = parseInt(slide.attr("data-duration")) * 1000;
			setTimeout(messageBoard.slides.gotoNextSlide, durationInSeconds);
		}
	};

messageBoard.messages = messageBoard.messages || {};