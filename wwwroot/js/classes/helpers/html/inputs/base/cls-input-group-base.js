/**
 * Class Input Base
 * <div class="mb-3">
 *   <label for="largeInput" class="form-label">Large input</label>
 *   <input id="largeInput" class="form-control form-control-lg" type="text" placeholder=".form-control-lg" />
 * </div>
 */
;
'use strict';
import { clsInputBaseOptions, clsInputBase } from './cls-input-base.js'
import('../../../../../../../../vendor/js/lodash.js/lodash.js')
  .then((mod2) => {
    // Logs "then() called"
    console.log(_.merge); // false
  });





export class clsInputGroupBase extends HTMLDivElement {
  /**
  * Creates a new InputBase object.
  * @param {HTMLFormElement} form - The form object.
  * @param {string} name - The name of the input.
  * @param {clsInputBaseOptions} options - The class extra options.
  * @param {string} type - The input type attribute value.
  */
  constructor(form, name, options = null, type = 'text') {
    super();
    this.form = form;
    this.name = name;
    //if (options === null || Object.keys(options).length < 1) { options = new clsInputBaseOptions(); }
    // this.options = { ...(new clsInputBaseOptions()), ...options};
    this.options = _.merge((new clsInputBaseOptions()), options);
    this.type = type;
    //this._ = require('../../../../../vendor/js/lodash.js/lodash.js');

    if (this.options.isGroup()) {
      console.log('The input is Group !!!');
    } else {
      console.log('The input is not a Group !!!');
    }
    this.className = "input-group input-group-merge"
    ///////////////////////////////////////////////
    this.createLabel();
    if (this.label) this.appendChild(this.label);
    ///////////////////////////////////////////////
    let input = document.createElement('input');
    input.className = "form-control";
    if (options.id) { input.setAttribute("id", id); }
    input.setAttribute("name", name);
    input.setAttribute("type", type,);
    input.setAttribute("placeholder", ".form-control-lg");
    if (options.size == "lg" || options.size == "sm") {
      input.className = input.className + " form-control-" + size;
    }
    if (options.rounded) {
      input.className = input.className + " rounded-pill";
    }

    ///////////////////////////////////////////////
    if (options.group.text.length > 0 || options.group.icon.length > 0) {
      let text = document.createElement('span');
      text.className = 'input-group-text';
      if (options.group.text.length > 0) text.innerHTML = options.group.text;
      if (options.group.icon.length > 0) {
        let icon = document.createElement('i');
        icon.className = options.group.icon;
        text.appendChild(icon);
      }
      if (options.group.prepend) this.appendChild(text);
      this.appendChild(input);
      if (!options.group.prepend) this.appendChild(text);
    } else {
      this.appendChild(input);
    }

    if (options.parent) {
      parent.appendChild(this);
    } else {
      form.appendChild(this);
    }
  }

  createLabel = function () {
    this.label = document.createElement('div');
    if (this.options.id) { this.label.setAttribute("for", this.options.id); }
    this.label.className = "form-label";
    this.label.innerText = this.options.caption ? this.options.caption : this.name;
  }
  createInput = function (caption) {

  }
  createPrepend = function (caption) {

  }

  createAppend = function (caption) {

  }
  isNullOrEmptyObject = function (variable) {
    return variable === null || (typeof variable === 'object' && Object.keys(variable).length === 0);
  }

  isObjectEmpty = function (objectName){
    return (
      objectName &&
      Object.keys(objectName).length === 0 &&
      objectName.constructor === Object
    );
  };
}

window.customElements.define('input-group-base', clsInputGroupBase, { extends: 'div' });
export default clsInputGroupBase;
