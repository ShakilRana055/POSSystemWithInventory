let selectors = {
	rightSideBarButton: $("#right_sidebarbtn"),
	rightSideNav: $("#right_sidebar"),
	pageContent: $(".page-content"),
	pageWrapper: $(".page-wrapper")
};
let pageWrapperWidth = selectors.pageWrapper.width();
let rightSideNavWidth = selectors.rightSideNav.width();
let width = pageWrapperWidth - rightSideNavWidth-30;

selectors.rightSideBarButton.on("click", function (e) {
	e.preventDefault();

    if (selectors.rightSideNav.hasClass("slideInRight")) {
        selectors.rightSideNav.removeClass('slideInRight fast');
		selectors.rightSideNav.addClass('slideOutRight fast');
		selectors.rightSideNav.css('display', 'block');
		Cookies.set('menu-state', 'closed');


		selectors.pageWrapper.css("width", "100%");
		selectors.pageWrapper.css("transition", "width .8s");

    } else if (selectors.rightSideNav.hasClass("slideOutRight")) {
        selectors.rightSideNav.removeClass('slideOutRight fast');
		selectors.rightSideNav.addClass('slideInRight fast');
		selectors.rightSideNav.css('display', 'block');
		Cookies.set('menu-state', 'open');

		selectors.pageWrapper.css("width", width);
		selectors.pageWrapper.css("transition", "width .8s");

	}
	else {

		if (Cookies.get('menu-state') === 'open') {
			selectors.rightSideNav.addClass('slideOutRight fast');
			Cookies.set('menu-state', 'closed');

			selectors.pageWrapper.css("width", "100%");
			selectors.pageWrapper.css("transition", "width .8s");

		}
		else {
			selectors.rightSideNav.css('display', 'block');
			selectors.rightSideNav.addClass('slideInRight fast');
			Cookies.set('menu-state', 'open');
			selectors.pageWrapper.css("width", width);
			selectors.pageWrapper.css("transition", "width .8s");

		}


    }

    $('input[type="checkbox"]').on("change", function (e) {
        e.preventDefault();

        let value = $(this).attr("value");

        if (value == "true") {
            $(this).attr("value", "false");
        }

        if (value == "false") {
            $(this).attr("value", "true");
        }
    });
});




/*!
RIGHT SIDE BAR........................
 */
; (function (factory) {
	var registeredInModuleLoader = false;
	if (typeof define === 'function' && define.amd) {
		define(factory);
		registeredInModuleLoader = true;
	}
	if (typeof exports === 'object') {
		module.exports = factory();
		registeredInModuleLoader = true;
	}
	if (!registeredInModuleLoader) {
		var OldCookies = window.Cookies;
		var api = window.Cookies = factory();
		api.noConflict = function () {
			window.Cookies = OldCookies;
			return api;
		};
	}
}(function () {
	function extend() {
		var i = 0;
		var result = {};
		for (; i < arguments.length; i++) {
			var attributes = arguments[i];
			for (var key in attributes) {
				result[key] = attributes[key];
			}
		}
		return result;
	}

	function init(converter) {
		function api(key, value, attributes) {
			var result;
			if (typeof document === 'undefined') {
				return;
			}

			// Write

			if (arguments.length > 1) {
				attributes = extend({
					path: '/'
				}, api.defaults, attributes);

				if (typeof attributes.expires === 'number') {
					var expires = new Date();
					expires.setMilliseconds(expires.getMilliseconds() + attributes.expires * 864e+5);
					attributes.expires = expires;
				}

				// We're using "expires" because "max-age" is not supported by IE
				attributes.expires = attributes.expires ? attributes.expires.toUTCString() : '';

				try {
					result = JSON.stringify(value);
					if (/^[\{\[]/.test(result)) {
						value = result;
					}
				} catch (e) { }

				if (!converter.write) {
					value = encodeURIComponent(String(value))
						.replace(/%(23|24|26|2B|3A|3C|3E|3D|2F|3F|40|5B|5D|5E|60|7B|7D|7C)/g, decodeURIComponent);
				} else {
					value = converter.write(value, key);
				}

				key = encodeURIComponent(String(key));
				key = key.replace(/%(23|24|26|2B|5E|60|7C)/g, decodeURIComponent);
				key = key.replace(/[\(\)]/g, escape);

				var stringifiedAttributes = '';

				for (var attributeName in attributes) {
					if (!attributes[attributeName]) {
						continue;
					}
					stringifiedAttributes += '; ' + attributeName;
					if (attributes[attributeName] === true) {
						continue;
					}
					stringifiedAttributes += '=' + attributes[attributeName];
				}
				return (document.cookie = key + '=' + value + stringifiedAttributes);
			}

			// Read

			if (!key) {
				result = {};
			}

			// To prevent the for loop in the first place assign an empty array
			// in case there are no cookies at all. Also prevents odd result when
			// calling "get()"
			var cookies = document.cookie ? document.cookie.split('; ') : [];
			var rdecode = /(%[0-9A-Z]{2})+/g;
			var i = 0;

			for (; i < cookies.length; i++) {
				var parts = cookies[i].split('=');
				var cookie = parts.slice(1).join('=');

				if (cookie.charAt(0) === '"') {
					cookie = cookie.slice(1, -1);
				}

				try {
					var name = parts[0].replace(rdecode, decodeURIComponent);
					cookie = converter.read ?
						converter.read(cookie, name) : converter(cookie, name) ||
						cookie.replace(rdecode, decodeURIComponent);

					if (this.json) {
						try {
							cookie = JSON.parse(cookie);
						} catch (e) { }
					}

					if (key === name) {
						result = cookie;
						break;
					}

					if (!key) {
						result[name] = cookie;
					}
				} catch (e) { }
			}

			return result;
		}

		api.set = api;
		api.get = function (key) {
			return api.call(api, key);
		};
		api.getJSON = function () {
			return api.apply({
				json: true
			}, [].slice.call(arguments));
		};
		api.defaults = {};

		api.remove = function (key, attributes) {
			api(key, '', extend(attributes, {
				expires: -1
			}));
		};

		api.withConverter = init;

		return api;
	}

	return init(function () { });
}));


if (Cookies.get('menu-state') === 'open') {
		$('#right_sidebar').css("right", "0px");
	$('#right_sidebar').css("display", "block");

	selectors.pageWrapper.css("width", width);
	selectors.pageWrapper.css("transition", "width .8s");
} 

//Cookies.set('CommonNav-state', 'open');

if (Cookies.get('CommonNav-state') === 'open') {
	$('#CommonNav').show();
} 

$('#CloseCommonNav').on('click', function () {
	var result = confirm('Are you sure want to close it? You can show it later from your settings');
	if (result) {
		Cookies.set('CommonNav-state', 'closed');
		$('#CommonNav').hide();
	}
});

