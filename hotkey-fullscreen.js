const methodMap = [
	[
		'requestFullscreen',
		'exitFullscreen',
		'fullscreenElement',
		'fullscreenEnabled',
		'fullscreenchange',
		'fullscreenerror',
	],
	// New WebKit
	[
		'webkitRequestFullscreen',
		'webkitExitFullscreen',
		'webkitFullscreenElement',
		'webkitFullscreenEnabled',
		'webkitfullscreenchange',
		'webkitfullscreenerror',

	],
	// Old WebKit
	[
		'webkitRequestFullScreen',
		'webkitCancelFullScreen',
		'webkitCurrentFullScreenElement',
		'webkitCancelFullScreen',
		'webkitfullscreenchange',
		'webkitfullscreenerror',

	],
	[
		'mozRequestFullScreen',
		'mozCancelFullScreen',
		'mozFullScreenElement',
		'mozFullScreenEnabled',
		'mozfullscreenchange',
		'mozfullscreenerror',
	],
	[
		'msRequestFullscreen',
		'msExitFullscreen',
		'msFullscreenElement',
		'msFullscreenEnabled',
		'MSFullscreenChange',
		'MSFullscreenError',
	],
];
const nativeAPI = (() => {
	const unprefixedMethods = methodMap[0];
	const returnValue = {};
	for (const methodList of methodMap) {
		const exitFullscreenMethod = methodList?.[1];
		if (exitFullscreenMethod in document) {
			for (const [index, method] of methodList.entries()) {
				returnValue[unprefixedMethods[index]] = method;
			}
			return returnValue;
		}
	}

	return false;
})();

document.addEventListener('keydown', keyPressed);
function keyPressed(e) {
  if(e.code == "KeyF") {
	   var divObj = document.getElementById("theImage");
	  if(Boolean(document[nativeAPI.fullscreenElement])){
			const returnPromise = document[nativeAPI.exitFullscreen]();
	  }else{
				const returnPromise = divObj[nativeAPI.requestFullscreen](null);
	  }
  }
}
