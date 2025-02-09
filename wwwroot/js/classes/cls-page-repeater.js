/**
 * Class page repeater
 */
/// <reference path="cls-page.js" />
'use strict';
export class clsPageRepeater {

  constructor(selector, page) {
    this.Selector = selector;
    this.Page = page;
    this.row = 1;
    this.$ = window.jQuery;
    this._config=this.Page.That._config;
    this.element = $(selector);
    if (this.element.length>0) {
      this.element.repeater({
        show: this._show,
        hide: this._hide,
        ready: function (setIndexes) {
          console.log('drop', this.ready);
        },
      });
    }
  }
  _show = function () {
    $(document).trigger('page-repeater-show', [$(this)]);
    $(this).slideDown();
  }
  on = function (event, handler) {
    let that = this
    $(document).on(
      event,
      {repeater:this},
      handler.bind(that)
    );
  }
  _hide = function (e) {
    confirm('Are you sure you want to delete this element?') && $(this).slideUp(e);
    fnOnHide ? null : fnOnHide(e);
  }
  UpdateRepeaterControls = function ($item) {
    const $ = window.jQuery;
    let fromControl = $item.find('.form-control, .form-select, .form-check-control');
    let formLabel = $item.find('.form-label');
    fromControl.each(function (i) {
      let entity = $(fromControl[i]).data("entity"), field = $(fromControl[i]).data("field");
      let id = `${entity}[${this.row}]_${field}`;
      let inputName = `${entity}[${this.row}][${field}]`;
      $(fromControl[i]).attr("data-idx", this.row),
        $(fromControl[i]).attr('id', id);
      $(fromControl[i]).attr('name', inputName);
      $(formLabel[i]).attr('for', id);
      //col++;
    });
    $item.find('button[data-bs-toggle="collapse"]').attr("data-bs-target", `#collapse-attribute-menu-${this.row}`).attr("aria-controls", `collapse-attribute-menu-${this.row}`);
    $item.find('[id^="collapse-attribute-menu-"]').attr("id", `collapse-attribute-menu-${this.row}`);

    $item.find('.select2').each(function () {
      const $select2 = $(this);
      $select2.select2({
        dropdownParent: $select2.parent()
      });
    });
  }
}

export default clsPageRepeater;
