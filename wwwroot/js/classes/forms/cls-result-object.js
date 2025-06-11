/**
 * Class Result Object
 */

'use strict';

export class clsPagination {
  /**
   * 
   * @param {number} total
   * @param {number} count
   * @param {number} current
   */
  constructor(total=0, count=10, current=1) {
    this.total = total;
    this.count = count;
    this.current = current;
  }
}
export class clsError {
  /**
   * 
   * @param {"reror"|"warning"} type
   * @param {string} message
   * @param {string | undefined} code
   */
  constructor(type="error", message="", code="") {
    this.type = type;
    this.message = message;
    this.code = code;
  }  
}
export class clsResultObject {
  /**
   * 
   * @param {any} data
   * @param {clsPagination} pagination
   * @param {Array<clsError>} errors
   */
  constructor(data = [], pagination = {}, errors=[] ) {
    this.data = data;
    this.pagination = pagination;
    this.errors = errors;

    hasErrors => this.errors.length > 0;

  }

}

export default clsResultObject
