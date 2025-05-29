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

export class clsInputValidationOptions {
 /**
* Creates a new InputBase object.
* @param {string} text - The validation text.
*/
  constructor(text = '') {
    this.text = text;
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
* @param {boolean} outlined - The input frame rounding false(default) | true. 
* @param {boolean} filled - The input frame rounding false(default) | true. 
* @param {Array<string>} classList - The input type attribute value.
* @param {clsInputValidationOptions} validation - The input validation otions.
* @param {JSON} style - The input type attribute value.
* @param {clsInputGroupOptions} group - The input group options.
* @param {string} typeName - The reguired object type namet.
*/
  constructor(id = "", caption = "", placeholder = "", size = "", outlined = true, filled = false, rounded = false, classList = [], validation = {}, style = {}, group = {}, typeName='TextField') {
    this.id = id;
    this.caption = caption;
    this.placeholder = placeholder;
    this.size = size;
    this.outlined = outlined;
    this.filled = filled;
    this.rounded = rounded;
    if(classList.length > 0) this.classList.add(...classList);
    this.validation = { ...(new clsInputValidationOptions()), ...validation };
    this.style = style;
    this.group = { ...(new clsInputGroupOptions()), ...group };
    this.typeName = typeName;
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
  hasValidation = function () {
    if (this.validation && this.validation.text.length > 0) return true;
    return false;
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
    this.label = null;
    this.input = null;
    this.form = form;
    this.name = name;
    this.options = _.merge((new clsInputBaseOptions()), options);
    this.type = type;
    this.parent = options.parent ? options.parent : form;
    //Flags
    this.labelFirst = this.options.size ? true : false;

    this.render();

  }

  render = function () {
    this.className = this.options.outlined ? "mb-4 form-floating form-floating-outline" : "mb-4";
    this.createLabel();
    this.createInput();

    if (this.labelFirst) this.appendChild(this.label);
    this.appendChild(this.input);
    if (!this.labelFirst) this.appendChild(this.label);

    this.parent.appendChild(this);
  }
  createLabel = function () {
    this.label = document.createElement('label');
    if (this.options.id) { this.label.setAttribute("for", this.options.id); }
    this.label.className = "floatingInput";
    this.label.innerText = this.options.caption ? this.options.caption : this.name;
  }
  createInput = function () {
    let input = document.createElement('input');
    input.classList.add("form-control");
    if (this.options.id) { input.setAttribute("id", this.options.id); }
    
    if (this.options.size == "lg" || this.options.size == "sm") {
      this.classList.remove("form-floating", "form-floating-outline");
      input.classList.add("form-control-" + this.options.size);
    }
    if (this.options.rounded) {
      input.classList.add("rounded-pill");
    }

    input.setAttribute("name", this.name);
    input.setAttribute("type", this.type,);
    input.setAttribute("placeholder", ".form-control-lg");
    this.input = input;
  }
}

window.customElements.define('input-base', clsInputBase, { extends: 'div' });
export default clsInputBase;
