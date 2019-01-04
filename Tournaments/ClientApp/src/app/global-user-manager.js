"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
core_1.Injectable();
var GlobalUserManager = /** @class */ (function () {
    function GlobalUserManager(http) {
        this.http = http;
    }
    GlobalUserManager.prototype.setUser = function (user) {
        this.name = user.name;
        this.password = this.hash(user.password);
    };
    GlobalUserManager.prototype.getUser = function () {
        return { name: this.name, email: this.email, password: this.password };
    };
    GlobalUserManager.prototype.get = function (path) {
        //this.http.get(this.url + this.name + "/" + this.manager.hash(this.password)).subscribe(
        //  user => {
        //    this.manager.setUser({ name: this.name, email: undefined, password: this.password });
        //    console.log(this.manager.getUser());
        //  }, error => console.error(error));
    };
    GlobalUserManager.prototype.post = function (path, obj) {
        console.log(location.origin);
        this.http.post(location.origin + "api/" + path, obj).subscribe(function (obj) {
            console.log(obj);
        });
    };
    GlobalUserManager.prototype.hash = function (str) {
        return str;
    };
    return GlobalUserManager;
}());
exports.GlobalUserManager = GlobalUserManager;
//# sourceMappingURL=global-user-manager.js.map