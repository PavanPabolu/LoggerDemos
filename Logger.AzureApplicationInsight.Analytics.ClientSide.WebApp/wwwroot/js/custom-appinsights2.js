/*
(function () {
    // Load Application Insights script
    var script = document.createElement('script');
    script.src = 'https://az416426.vo.msecnd.net/scripts/b/ai.2.min.js';
    script.async = true;
    document.getElementsByTagName('head')[0].appendChild(script);

    script.onload = function () {
        // Initialize Application Insights
        var appInsights = window.appInsights || initializeAppInsights({
            instrumentationKey: appIns_constr || "YOUR_INSTRUMENTATION_KEY"
        });

        // Attach to global window object
        window.appInsights = appInsights;

        // Track initial page view
        appInsights.trackPageView();

        // Track custom events and metrics
        trackCustomEvent(appInsights, 'MyCustomEvent', { customProperty: 'customValue' });
        trackCustomMetric(appInsights, 'MyCustomMetric', 42);
        trackDeviceInformation(appInsights);
    };

    function initializeAppInsights(config) {
        var t = { config: config };
        t.queue = [];
        t.cookie = document.cookie;

        // Helper to enqueue methods
        function createQueueFunction(method) {
            t[method] = function () {
                var args = arguments;
                t.queue.push(function () { t[method].apply(t, args); });
            };
        }

        // Define methods to queue
        var methods = ['trackEvent', 'trackException', 'trackMetric', 'trackPageView', 'trackTrace', 'trackDependency', 'setAuthenticatedUserContext', 'clearAuthenticatedUserContext'];
        methods.forEach(createQueueFunction);

        // Load and apply script
        var script = document.createElement('script');
        script.src = config.url || 'https://az416426.vo.msecnd.net/scripts/b/ai.2.min.js';
        script.async = true;
        document.getElementsByTagName('head')[0].appendChild(script);

        script.onload = function () {
            t.queue.forEach(function (queuedFunc) {
                queuedFunc();
            });
        };

        return t;
    }

    function trackCustomEvent(appInsights, eventName, properties) {
        appInsights.trackEvent({
            name: eventName,
            properties: properties
        });
    }

    function trackCustomMetric(appInsights, metricName, value) {
        appInsights.trackMetric({
            name: metricName,
            average: value
        });
    }

    function trackDeviceInformation(appInsights) {
        var deviceInfo = {
            deviceName: navigator.userAgent,
            screenResolution: screen.width + 'x' + screen.height
        };
        appInsights.trackEvent({ name: 'DeviceInformation', properties: deviceInfo });
    }
})();
*/




(function () {
    var appInsights = window.appInsights || function (config) {
        function r(config) {
            t[config] = function () {
                var i = arguments;
                t.queue.push(function () {
                    t[config].apply(t, i);
                });
            };
        }
        var t = { config: config }, u = document, e = window, o = "script", s = u.createElement(o), i, f;
        s.src = config.url || "https://az416426.vo.msecnd.net/scripts/b/ai.2.min.js";
        u.getElementsByTagName(o)[0].parentNode.appendChild(s);
        t.cookie = u.cookie;
        t.queue = [];
        t.version = "2.0";
        for (var m = ["Event", "Exception", "Metric", "PageView", "Trace", "Dependency"]; m.length;) {
            r("track" + m.pop());
        }
        if (!config.disableExceptionTracking) {
            m = "onerror";
            r("_" + m);
            i = e[m];
            e[m] = function (config, r, u, e, o) {
                var s = i && i(config, r, u, e, o);
                s !== !0 && t["_" + m]({ message: config, url: r, lineNumber: u, columnNumber: e, error: o });
                return s;
            };
        }
        return t;
    }({
        instrumentationKey: appIns_constr 
    });

    window.appInsights = appInsights;
    appInsights.queue && appInsights.queue.forEach(function (call) { call(); });
})();

window.appInsights.trackPageView();
