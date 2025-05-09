/**
 * Class page dataTable
 */

'use strict';

import { clsInputBase } from './cls-input-base.js';
export class clsInputText extends clsInputBase {

  constructor(form, parent, name, id, caption, size, rounded = false) {
    super(form, parent, name id, caption, size, rounded = false);
  }
}

window.customElements.define('input-text', clsInputText, { extends: 'div' });
export default clsInputText;
