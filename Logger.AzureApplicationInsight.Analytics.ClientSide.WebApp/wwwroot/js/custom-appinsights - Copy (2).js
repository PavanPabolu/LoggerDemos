var appInsights = window.appInsights || function (config) {
    function s(config) { t[config] = function () { var i = arguments; t.queue.push(function () { t[config].apply(t, i) }) } }
    var t = { config: config }, r = document, e = window, o = "script", i, a;
    for (e[o] = "https://az416426.vo.msecnd.net/scripts/a/ai.0.js", r[o] = r[o] || e[o], i = 0; 3 > i; i++) {
        a = r.createElement(o);
        a.src = e[o];
        a.async = !0;
        a.type = "text/javascript";
        r.getElementsByTagName(o)[0].parentNode.appendChild(a)
    }
    t.queue = [];
    t.version = config.version || "2.3";
    return t
}({
    instrumentationKey: "YOUR_INSTRUMENTATION_KEY"
});

window.appInsights = appInsights;

// Track a page view
appInsights.trackPageView();

// Track an event
appInsights.trackEvent({
    name: "WinGame",
    properties: {
        Game: currentGame.name,
        Difficulty: currentGame.difficulty
    },
    measurements: {
        Score: currentGame.score,
        Opponents: currentGame.opponentCount
    }
});

// Track a metric
appInsights.trackMetric({
    name: "GameScore",
    average: currentGame.score
});