/**
 * Class page dataTable
 */

'use strict';

import { clsInputBase, clsInputBaseOptions } from '../base/cls-input-base.js';

export class clsBasicInputText extends clsInputBase {

  /**
* Creates a new InputBase object.
* @param {HTMLFormElement} form - The form object.
* @param {string} name - The name of the input.
* @param {string} label - The name of the person.
* @param {clsInputBaseOptions} options - The class extra options.
* @param {string} type - The input type attribute value.
*/
  constructor(form, name, options = {}, type = 'text') {
    super(form, name, options);
  }
}

window.customElements.define('input-text', clsBasicInputText, { extends: 'div' });
export default clsBasicInputText;
