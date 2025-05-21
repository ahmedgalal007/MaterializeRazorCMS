/// <reference path="../../../vendor/js/lodash.js/lodash.js" />

/**
 * Class page forms base
 */

'use strict';

import col from './cls-page-forms-grid-col.js';
import row from './cls-page-forms-grid-row.js';
//import { clsModal, inputs } from './html/index.js';
export class clsPageFormsGrid extends HTMLDivElement {
  constructor(page, options) {
  //constructor() {
    super();
    this.Page = page;
    this.Page.ready(this.init.bind(this), 1000);
    //this.init();
  }

  init() {

  }
  
}

window.customElements.define('cls-page-forms-grid', clsPageFormsGrid, { extends: 'div' });

export default clsPageFormsGrid;
