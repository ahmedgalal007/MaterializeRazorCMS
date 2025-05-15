/**
 * Class Input Base
 * <div class="mb-3">
 *   <label for="largeInput" class="form-label">Large input</label>
 *   <input id="largeInput" class="form-control form-control-lg" type="text" placeholder=".form-control-lg" />
 * </div>
 */
'use strict';

import { lodash } from '../../../../../vendor/js/lodash.js/lodash.js'
export class clsInputBaseOptions {
  /**
* Creates a new InputBase object.
* @param {string} id - The form object.
* @param {string} caption - The name of the input.
* @param {string} placeholder - The name of the person.
* @param {string} size - The value "" for normal(default) | "sm" for small | "lg" for large.
* @param {boolean} rounded - The input frame rounding false(default) | true. 
* @param {Array<string>} className - The input type attribute value.
* @param {JSON} style - The input type attribute value.
*/
  constructor(id = "", caption = "", placeholder = "", size = "", rounded = false, className = [], style = {}) {
    this.id = id;
    this.caption = caption;
    this.placeholder = placeholder;
    this.size = size;
    this.rounded = rounded;
    this.className = className;
    this.style = style;
  }
}

export class clsInputBase extends HTMLDivElement {
 /**
 * Creates a new InputBase object.
 * @param {HTMLFormElement} form - The form object.
 * @param {string} name - The name of the input.
 * @param {string} label - The name of the person.
 * @param {clsInputBaseOptions} options - The class extra options.
 * @param {string} type - The input type attribute value.
 */
  constructor(form, name, options = null, type='text') {

    if (options === null) { options = new clsInputBaseOptions(); }
    this.className = "mb-3"
    ///////////////////////////////////////////////
    let label = document.createElement('div');
    if (options.id) { label.setAttribute("for", options.id); }
    label.className = "form-label";
    label.innerText = options.caption ? options.caption : name;
    this.appendChild(label);
    ///////////////////////////////////////////////
    let input = document.createElement('input');
    input.className = "form-control";
    if (options.id) { input.setAttribute("id", id); }
    input.setAttribute("name", name);
    input.setAttribute("type", type,);
    input.setAttribute("placeholder", ".form-control-lg");
    if (options.size == "lg" || options.size == "sm") {
      input.className = input.className + " form-control-"+ size;
    }
    if (options.rounded) {
      input.className = input.className + " rounded-pill";
    }
    this.appendChild(input);
    if (options.parent) {
      parent.appendChild(this);
    } else {
      form.appendChild(this);
    }
  }
}

window.customElements.define('input-base', clsInputBase, { extends: 'div' });
export default clsInputBase;
