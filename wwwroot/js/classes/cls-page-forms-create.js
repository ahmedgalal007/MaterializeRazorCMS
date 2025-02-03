/**
 * Class page forms Create
 */

'use strict';
import clsPageFormsBase from './helpers/cls-page-forms-base.js'
export class clsPageFormsCreate extends clsPageFormsBase {
  constructor(page) {
    super(page);
    this.FormType = "Create";
  }
}

export default clsPageFormsCreate;
