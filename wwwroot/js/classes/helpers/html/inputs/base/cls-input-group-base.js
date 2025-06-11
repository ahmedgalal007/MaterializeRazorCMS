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
  #value = null;
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

    
    ///////////////////////////////////////////////
    this.createLabel();
    this.createPrepend();
    this.createInput();
    this.createAppend();
    ///////////////////////////////////////////////
    
    this.render();
    

    if (options.parent) {
      parent.appendChild(this);
    } else {
      form.appendChild(this);
    }
  }

  render = function () {
    let isGroup = this.options.isGroup();
    this.className = isGroup ? "mb-3 input-group input-group-merge" : "mb-3 form-floating form-floating-outline";
    if (isGroup && this.prepend) this.appendChild(this.prepend);
    let container = isGroup ? document.createElement('div') : this;
    if (isGroup) container.className = "form-floating form-floating-outline";
    if (this.input) container.appendChild(this.input);
    if (this.label) container.appendChild(this.label);
    if (isGroup) this.appendChild(container);
    if (isGroup && this.appended) this.appendChild(this.appended);
    /// validation
    if (!isGroup && this.options.hasValidation()) {
      let txtValidate = document.createElement('div');
      txtValidate.className = 'form-text';
      txtValidate.innerText = this.options.validation.text;
      this.appendChild(txtValidate);
    }
  }

  get value() {
    this.#value = this.input.value
    return this.#value;
  }
  set value(val) {
    this.#value = val;
    this.input.value = this.#value;
  }
  createLabel = function () {
    this.label = document.createElement('label');
    if (this.options.id) { this.label.setAttribute("for", this.options.id); }
    this.label.className = "form-label";
    this.label.innerText = this.options.caption ? this.options.caption : this.name;
  }
  createInput = function () {
    let input = document.createElement('input');
    input.className = "form-control";
    if (this.options.id) { input.setAttribute("id", this.options.id); }
    input.setAttribute("name", this.name);
    input.setAttribute("type", this.type,);
    input.setAttribute("placeholder", ".form-control-lg");
    if (this.options.size == "lg" || this.options.size == "sm") {
      input.className = input.className + " form-control-" + size;
    }
    if (this.options.rounded) {
      input.className = input.className + " rounded-pill";
    }
    this.input = input;
  }
  createPrepend = function (caption) {
    if (this.options.isGroup() && !this.options.group.prepend.isEmpty()) {
      let text = document.createElement('span');
      text.className = 'input-group-text';
      let opt = this.options.group.prepend;
      if (opt.text.length > 0) text.innerHTML = opt.text;
      if (opt.icon && opt.icon.length > 0) {
        let icon = document.createElement('i');
        icon.className = opt.icon;
        text.appendChild(icon);
      }
      this.prepend = text;
    }
  }

  createAppend = function (caption) {
    if (this.options.isGroup() && !this.options.group.append.isEmpty()) {
      let text = document.createElement('span');
      text.className = 'input-group-text';
      let opt = this.options.group.append;
      if (opt.text.length > 0) text.innerHTML = opt.text;
      if (opt.icon && opt.icon.length > 0) {
        let icon = document.createElement('i');
        icon.className = opt.icon;
        text.appendChild(icon);
      }
      this.appended = text;
    }
  }
  isNullOrEmptyObject = function (variable) {
    return variable === null || ((typeof variable === 'object' || typeof variable === 'string') && Object.keys(variable).length === 0);
  }

  isObjectEmpty = function (objectName){
    return (
      objectName &&
      Object.keys(objectName).length === 0 &&
      objectName.constructor === Object
    );
  };

  render2 = function () {
    //if (this.options.isGroup()) {
    //  this.className = "input-group input-group-merge"
    //  if (this.prepend) this.appendChild(this.prepend);
    //  let ItemGroup = document.createElement('div');
    //  ItemGroup.className = "form-floating form-floating-outline";
    //  if (this.input) ItemGroup.appendChild(this.input);
    //  if (this.label) ItemGroup.appendChild(this.label);
    //  this.appendChild(ItemGroup);
    //  if (this.appended) this.appendChild(this.appended);
    //} else {
    //  this.className = "form-floating form-floating-outline";
    //  if (this.input) this.appendChild(this.input);
    //  if (this.label) this.appendChild(this.label);
    //  if (this.options.hasValidation()) {
    //    let txtValidate = document.createElement('div');
    //    txtValidate.className = 'form-text';
    //    txtValidate.innerText = this.options.validation.text;
    //    this.appendChild(txtValidate);
    //  }
    // }
  }
}

window.customElements.define('input-group-base', clsInputGroupBase, { extends: 'div' });
export default clsInputGroupBase;
