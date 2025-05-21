/**
 * Class page dataTable
 */

'use strict';

import { clsInputBase, clsInputBaseOptions } from './cls-input-base.js';

export class clsInputNumber extends clsInputBase {

  /**
* Creates a new InputBase object.
* @param {HTMLFormElement} form - The form object.
* @param {string} name - The name of the input.
* @param {string} label - The name of the person.
* @param {clsInputBaseOptions} options - The class extra options.
* @param {string} type - The input type attribute value.
*/
  constructor(form, name, options = {}, type = 'date') {
    super(form, name, options);
  }
}

window.customElements.define('input-number', clsInputNumber, { extends: 'div' });
export default clsInputNumber;
