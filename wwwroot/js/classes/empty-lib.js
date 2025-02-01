(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
    typeof define === 'function' && define.amd ? define(factory) :
      (
        global = typeof globalThis !== 'undefined' ? globalThis : global || self,
        (
          global.OtherLib = global.OtherLib || {},
          global.OtherLib.plugins = global.OtherLib.plugins || {},
          global.EmptyLib= factory()
        )
      );
})(this, (function () {
  'use strict';

}));
