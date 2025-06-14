///reference(./cls-page.js)
/**
 * Class page forms Edit
 */

'use strict';
import clsPageFormsBase from '../helpers/cls-page-forms-base.js'
export class clsPageFormsEdit extends clsPageFormsBase {
  constructor(page, id, fields, isModal) {
    super(page, id, fields, isModal);
    this.FormType = "Edit";
  }
  
}

window.customElements.define('cls-page-forms-edit', clsPageFormsEdit, { extends: 'div' });

export default clsPageFormsEdit;
