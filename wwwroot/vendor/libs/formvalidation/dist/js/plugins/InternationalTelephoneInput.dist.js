/*
 * ATTENTION: The "eval" devtool has been used (maybe by default in mode: "development").
 * This devtool is neither made for production nor for readable output files.
 * It uses "eval()" calls to create a separate source file in the browser devtools.
 * If you are trying to read the output file, select a different devtool (https://webpack.js.org/configuration/devtool/)
 * or disable the default devtool with "devtool: false".
 * If you are looking for production-ready output files, see mode: "production" (https://webpack.js.org/configuration/mode/).
 */
(function webpackUniversalModuleDefinition(root, factory) {
	if(typeof exports === 'object' && typeof module === 'object')
		module.exports = factory();
	else if(typeof define === 'function' && define.amd)
		define([], factory);
	else {
		var a = factory();
		for(var i in a) (typeof exports === 'object' ? exports : root)[i] = a[i];
	}
})(self, () => {
return /******/ (() => { // webpackBootstrap
/******/ 	var __webpack_modules__ = ({

/***/ "./wwwroot/vendor/libs/formvalidation/dist/js/plugins/InternationalTelephoneInput.js":
/*!*******************************************************************************************!*\
  !*** ./wwwroot/vendor/libs/formvalidation/dist/js/plugins/InternationalTelephoneInput.js ***!
  \*******************************************************************************************/
/***/ (function(module, exports, __webpack_require__) {

eval("var __WEBPACK_AMD_DEFINE_FACTORY__, __WEBPACK_AMD_DEFINE_RESULT__;function _typeof(o) { \"@babel/helpers - typeof\"; return _typeof = \"function\" == typeof Symbol && \"symbol\" == typeof Symbol.iterator ? function (o) { return typeof o; } : function (o) { return o && \"function\" == typeof Symbol && o.constructor === Symbol && o !== Symbol.prototype ? \"symbol\" : typeof o; }, _typeof(o); }\n/**\r\n * FormValidation (https://formvalidation.io), v1.10.0 (2236098)\r\n * The best validation library for JavaScript\r\n * (c) 2013 - 2021 Nguyen Huu Phuoc <me@phuoc.ng>\r\n */\n\n(function (global, factory) {\n  ( false ? 0 : _typeof(exports)) === 'object' && \"object\" !== 'undefined' ? module.exports = factory() :  true ? !(__WEBPACK_AMD_DEFINE_FACTORY__ = (factory),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ = (typeof __WEBPACK_AMD_DEFINE_FACTORY__ === 'function' ?\n\t\t(__WEBPACK_AMD_DEFINE_FACTORY__.call(exports, __webpack_require__, exports, module)) :\n\t\t__WEBPACK_AMD_DEFINE_FACTORY__),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__)) : (0);\n})(this, function () {\n  'use strict';\n\n  function _classCallCheck(instance, Constructor) {\n    if (!(instance instanceof Constructor)) {\n      throw new TypeError(\"Cannot call a class as a function\");\n    }\n  }\n  function _defineProperties(target, props) {\n    for (var i = 0; i < props.length; i++) {\n      var descriptor = props[i];\n      descriptor.enumerable = descriptor.enumerable || false;\n      descriptor.configurable = true;\n      if (\"value\" in descriptor) descriptor.writable = true;\n      Object.defineProperty(target, descriptor.key, descriptor);\n    }\n  }\n  function _createClass(Constructor, protoProps, staticProps) {\n    if (protoProps) _defineProperties(Constructor.prototype, protoProps);\n    if (staticProps) _defineProperties(Constructor, staticProps);\n    Object.defineProperty(Constructor, \"prototype\", {\n      writable: false\n    });\n    return Constructor;\n  }\n  function _defineProperty(obj, key, value) {\n    if (key in obj) {\n      Object.defineProperty(obj, key, {\n        value: value,\n        enumerable: true,\n        configurable: true,\n        writable: true\n      });\n    } else {\n      obj[key] = value;\n    }\n    return obj;\n  }\n  function _inherits(subClass, superClass) {\n    if (typeof superClass !== \"function\" && superClass !== null) {\n      throw new TypeError(\"Super expression must either be null or a function\");\n    }\n    subClass.prototype = Object.create(superClass && superClass.prototype, {\n      constructor: {\n        value: subClass,\n        writable: true,\n        configurable: true\n      }\n    });\n    Object.defineProperty(subClass, \"prototype\", {\n      writable: false\n    });\n    if (superClass) _setPrototypeOf(subClass, superClass);\n  }\n  function _getPrototypeOf(o) {\n    _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf.bind() : function _getPrototypeOf(o) {\n      return o.__proto__ || Object.getPrototypeOf(o);\n    };\n    return _getPrototypeOf(o);\n  }\n  function _setPrototypeOf(o, p) {\n    _setPrototypeOf = Object.setPrototypeOf ? Object.setPrototypeOf.bind() : function _setPrototypeOf(o, p) {\n      o.__proto__ = p;\n      return o;\n    };\n    return _setPrototypeOf(o, p);\n  }\n  function _isNativeReflectConstruct() {\n    if (typeof Reflect === \"undefined\" || !Reflect.construct) return false;\n    if (Reflect.construct.sham) return false;\n    if (typeof Proxy === \"function\") return true;\n    try {\n      Boolean.prototype.valueOf.call(Reflect.construct(Boolean, [], function () {}));\n      return true;\n    } catch (e) {\n      return false;\n    }\n  }\n  function _assertThisInitialized(self) {\n    if (self === void 0) {\n      throw new ReferenceError(\"this hasn't been initialised - super() hasn't been called\");\n    }\n    return self;\n  }\n  function _possibleConstructorReturn(self, call) {\n    if (call && (_typeof(call) === \"object\" || typeof call === \"function\")) {\n      return call;\n    } else if (call !== void 0) {\n      throw new TypeError(\"Derived constructors may only return object or undefined\");\n    }\n    return _assertThisInitialized(self);\n  }\n  function _createSuper(Derived) {\n    var hasNativeReflectConstruct = _isNativeReflectConstruct();\n    return function _createSuperInternal() {\n      var Super = _getPrototypeOf(Derived),\n        result;\n      if (hasNativeReflectConstruct) {\n        var NewTarget = _getPrototypeOf(this).constructor;\n        result = Reflect.construct(Super, arguments, NewTarget);\n      } else {\n        result = Super.apply(this, arguments);\n      }\n      return _possibleConstructorReturn(this, result);\n    };\n  }\n  var t = FormValidation.Plugin;\n  var e = /*#__PURE__*/function (_t) {\n    _inherits(e, _t);\n    var _super = _createSuper(e);\n    function e(t) {\n      var _this;\n      _classCallCheck(this, e);\n      _this = _super.call(this, t);\n      _this.intlTelInstances = new Map();\n      _this.countryChangeHandler = new Map();\n      _this.fieldElements = new Map();\n      _this.opts = Object.assign({}, {\n        autoPlaceholder: \"polite\",\n        utilsScript: \"\"\n      }, t);\n      _this.validatePhoneNumber = _this.checkPhoneNumber.bind(_assertThisInitialized(_this));\n      _this.fields = typeof _this.opts.field === \"string\" ? _this.opts.field.split(\",\") : _this.opts.field;\n      return _this;\n    }\n    _createClass(e, [{\n      key: \"install\",\n      value: function install() {\n        var _this2 = this;\n        this.core.registerValidator(e.INT_TEL_VALIDATOR, this.validatePhoneNumber);\n        this.fields.forEach(function (t) {\n          _this2.core.addField(t, {\n            validators: _defineProperty({}, e.INT_TEL_VALIDATOR, {\n              message: _this2.opts.message\n            })\n          });\n          var s = _this2.core.getElements(t)[0];\n          var i = function i() {\n            return _this2.core.revalidateField(t);\n          };\n          s.addEventListener(\"countrychange\", i);\n          _this2.countryChangeHandler.set(t, i);\n          _this2.fieldElements.set(t, s);\n          _this2.intlTelInstances.set(t, intlTelInput(s, _this2.opts));\n        });\n      }\n    }, {\n      key: \"uninstall\",\n      value: function uninstall() {\n        var _this3 = this;\n        this.fields.forEach(function (t) {\n          var s = _this3.countryChangeHandler.get(t);\n          var i = _this3.fieldElements.get(t);\n          var n = _this3.getIntTelInstance(t);\n          if (s && i && n) {\n            i.removeEventListener(\"countrychange\", s);\n            _this3.core.disableValidator(t, e.INT_TEL_VALIDATOR);\n            n.destroy();\n          }\n        });\n      }\n    }, {\n      key: \"getIntTelInstance\",\n      value: function getIntTelInstance(t) {\n        return this.intlTelInstances.get(t);\n      }\n    }, {\n      key: \"checkPhoneNumber\",\n      value: function checkPhoneNumber() {\n        var _this4 = this;\n        return {\n          validate: function validate(t) {\n            var _e = t.value;\n            var s = _this4.getIntTelInstance(t.field);\n            if (_e === \"\" || !s) {\n              return {\n                valid: true\n              };\n            }\n            return {\n              valid: s.isValidNumber()\n            };\n          }\n        };\n      }\n    }]);\n    return e;\n  }(t);\n  e.INT_TEL_VALIDATOR = \"___InternationalTelephoneInputValidator\";\n  return e;\n});\n\n//# sourceURL=webpack://Materialize/./wwwroot/vendor/libs/formvalidation/dist/js/plugins/InternationalTelephoneInput.js?");

/***/ })

/******/ 	});
/************************************************************************/
/******/ 	// The module cache
/******/ 	var __webpack_module_cache__ = {};
/******/ 	
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/ 		// Check if module is in cache
/******/ 		var cachedModule = __webpack_module_cache__[moduleId];
/******/ 		if (cachedModule !== undefined) {
/******/ 			return cachedModule.exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = __webpack_module_cache__[moduleId] = {
/******/ 			// no module.id needed
/******/ 			// no module.loaded needed
/******/ 			exports: {}
/******/ 		};
/******/ 	
/******/ 		// Execute the module function
/******/ 		__webpack_modules__[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/ 	
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/ 	
/************************************************************************/
/******/ 	
/******/ 	// startup
/******/ 	// Load entry module and return exports
/******/ 	// This entry module is referenced by other modules so it can't be inlined
/******/ 	var __webpack_exports__ = __webpack_require__("./wwwroot/vendor/libs/formvalidation/dist/js/plugins/InternationalTelephoneInput.js");
/******/ 	
/******/ 	return __webpack_exports__;
/******/ })()
;
});