String.prototype.replaceAll = function (replace, to) {
    var regEx = new RegExp(replace, 'g');
    return this.replace(regEx, to);
};