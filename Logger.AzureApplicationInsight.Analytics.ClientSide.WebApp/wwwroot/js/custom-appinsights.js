// appInsights.js
(function () {
    var script = document.createElement('script');
    script.src = 'https://az416426.vo.msecnd.net/scripts/a/ai.0.js';
    script.onload = function () {
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

        // Example usage:
        appInsights.trackPageView(); // Track a page view
        appInsights.trackEvent('ButtonClick', { category: 'UI', label: 'Submit' }); // Track a custom event
        appInsights.trackMetric('PageLoadTime', 200); // Track a custom metric
    };
    document.head.appendChild(script);
})();
