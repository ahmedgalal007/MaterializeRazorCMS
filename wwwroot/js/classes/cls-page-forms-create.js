/**
 * Class page forms Create
 */

'use strict';
import clsPageFormsBase from './helpers/cls-page-forms-base.js'
export class clsPageFormsCreate extends clsPageFormsBase {
  constructor(page, id, fields, isModal) {
    super(page, id, fields, isModal);
    this.FormType = "Create";
  }
}
window.customElements.define('cls-page-forms-create', clsPageFormsCreate, { extends: 'div' });

export default clsPageFormsCreate;
