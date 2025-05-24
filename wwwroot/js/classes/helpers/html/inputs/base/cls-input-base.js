/**
 * Class Input Base
 * <div class="mb-3">
 *   <label for="largeInput" class="form-label">Large input</label>
 *   <input id="largeInput" class="form-control form-control-lg" type="text" placeholder=".form-control-lg" />
 * </div>
 */
;
'use strict';
import('../../../../../../../vendor/js/lodash.js/lodash.js')
.then((mod2) => {
  // Logs "then() called"
  console.log(_.merge); // false
});

export class clsInputGroupPendOptions {
  /**
* Creates a new InputBase object.
* @param {string} text - The form object.
* @param {Array<string>} iconClasses - The name of the input.
* @param {boolean} iconFirst - The name of the input.
*/
  constructor( text = '', iconClasses = [], iconFirst = false) {
    this.text = text;
    this.iconClasses = iconClasses;
    this.iconFirst = iconFirst;
  }
  isEmpty = function () {
    return this.text === '' && this.iconClasses.length === 0;
  }
}
export class clsInputGroupOptions {
  /**
* Creates a new InputBase object.
* @param {string} type - The form object.
* @param {clsInputGroupPendOptions} prepend - The name of the input.
* @param {clsInputGroupPendOptions} append - The name of the input.
*/
  constructor(type = 'merge', prepend = {}, append = {})
  {
    this.type = type;
    this.prepend = { ...(new clsInputGroupPendOptions()), ...prepend } ;
    this.append = { ...(new clsInputGroupPendOptions()), ...append };
  }

}
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
* @param {clsInputGroupOptions} group - The input group options.
*/
  constructor(id = "", caption = "", placeholder = "", size = "", rounded = false, className = [], style = {}, group = {}) {
    this.id = id;
    this.caption = caption;
    this.placeholder = placeholder;
    this.size = size;
    this.rounded = rounded;
    this.className = className;
    this.style = style;
    this.group = { ...(new clsInputGroupOptions()), ...group };
    //if (group == null || Object.keys(group).length < 1) {
    //  this.group = new clsInputGroupOptions();
    //} else {
    //  this.group = group;
    //}
    // this.group = isNullOrEmpty(group)? new clsInputGroupOptions() : group;
  }
  isGroup = function () {
    return !this.group.prepend.isEmpty() || !this.group.append.isEmpty();
  }
  isNullOrEmpty = function (obj) {
    if (obj == null || Object.keys(obj).length <1) return true;
    return false;
  }
}

export class clsInputBase extends HTMLDivElement {
 /**
 * Creates a new InputBase object.
 * @param {HTMLFormElement} form - The form object.
 * @param {string} name - The name of the input.
 * @param {clsInputBaseOptions} options - The class extra options.
 * @param {string} type - The input type attribute value.
 */
  constructor(form, name, options = null, type='text') {
    super();

    //this._ = require('../../../../../vendor/js/lodash.js/lodash.js');

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
