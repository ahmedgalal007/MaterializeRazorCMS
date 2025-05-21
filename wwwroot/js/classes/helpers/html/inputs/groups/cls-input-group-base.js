/**
 * Class Input Base
 * <div class="mb-3">
 *   <label for="largeInput" class="form-label">Large input</label>
 *   <input id="largeInput" class="form-control form-control-lg" type="text" placeholder=".form-control-lg" />
 * </div>
 */
;
'use strict';
import { clsInputBaseOptions, clsInputBase } from '../cls-input-base.js'
import('../../../../../../vendor/js/lodash.js/lodash.js')
.then((mod2) => {
  // Logs "then() called"
  console.log(_.merge); // false
});





export class clsInputGroupBase extends HTMLDivElement {
 /**
 * Creates a new InputBase object.
 * @param {HTMLFormElement} form - The form object.
 * @param {string} name - The name of the input.
 * @param {clsInputGroupBaseOptions} options - The class extra options.
 * @param {string} type - The input type attribute value.
 */
  constructor(form, name, options = null, type='text') {
    super();

    //this._ = require('../../../../../vendor/js/lodash.js/lodash.js');

    if (options === null) { options = new clsInputBaseOptions(); }
    this.className = "input-group input-group-merge"
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
}

window.customElements.define('input-group-base', clsInputGroupBase, { extends: 'div' });
export default clsInputGroupBase;
