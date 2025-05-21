/// <reference path="../../../vendor/js/lodash.js/lodash.js" />

/**
 * Class page forms base
 */

'use strict';

//import clsPageFormsForm from './cls-page-forms-form.js';
//import { clsModal, inputs } from './html/index.js';
export class clsPageFormsGridCol extends HTMLDivElement {
  constructor(grid, options) {
    //constructor() {
    super();
    this.Grid = grid;
    this.Grid.Page.ready(this.init.bind(this), 1000);
    //this.init();
  }

  init() {

  }

}

window.customElements.define('cls-page-forms-grid-col', clsPageFormsGridCol, { extends: 'div' });

export default clsPageFormsGrid;
