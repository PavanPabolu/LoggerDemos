// appInsights.js
(function () {

    // Load the Application Insights SDK
    var script = document.createElement('script');
    script.src = 'https://az416426.vo.msecnd.net/scripts/a/ai.0.js';
    script.async = true;
    document.getElementsByTagName('head')[0].appendChild(script);

    // Initialize the Application Insights SDK
    var appInsights = window.appInsights || function (config) {
        function trackEvent(eventName, properties, measurements) {
            appInsights.queue.push(function () {
                appInsights.trackEvent({ name: eventName, properties: properties, measurements: measurements });
            });
        }

        function trackMetric(metricName, value) {
            appInsights.queue.push(function () {
                appInsights.trackMetric({ name: metricName, average: value });
            });
        }

        return {
            config: config,
            trackPageView: function () {
                appInsights.queue.push(function () {
                    appInsights.trackPageView();
                });
            },
            trackEvent: trackEvent,
            trackMetric: trackMetric,
            queue: []
        };
    }({
        instrumentationKey: appIns_constr || "YOUR_INSTRUMENTATION_KEY"
    });

    appInsights.config.disableExceptionTracking = false;

    window.appInsights = appInsights;
})();

// /*
// Example usage
var currentGame = {
    name: "My Game",
    difficulty: "Hard",
    score: 100,
    opponentCount: 5
};

// Track a page view
appInsights.trackPageView();

// Track an event
appInsights.trackEvent("WinGame", {
    Game: currentGame.name,
    Difficulty: currentGame.difficulty
}, {
    Score: currentGame.score,
    Opponents: currentGame.opponentCount
});

// Track a metric
appInsights.trackMetric("GameScore", currentGame.score);

// */