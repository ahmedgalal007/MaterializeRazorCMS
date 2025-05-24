/**
 * Class page dataTable
 */

'use strict';
import { clsInputBaseOptions, clsInputBase } from '../base/cls-input-base.js';
import { clsInputGroupBase } from '../base/cls-input-group-base.js';

export class clsGroupInputText extends clsInputGroupBase {

  /**
* Creates a new InputBase object.
* @param {HTMLFormElement} form - The form object.
* @param {string} name - The name of the input.
* @param {clsInputBaseOptions} options - The class extra options.
* @param {string} type - The input type attribute value.
*/
  constructor(form, name, options = {}, type = 'text') {
    super(form, name, options);
  }
}

window.customElements.define('group-input-text', clsGroupInputText, { extends: 'div' });
export default clsGroupInputText;
