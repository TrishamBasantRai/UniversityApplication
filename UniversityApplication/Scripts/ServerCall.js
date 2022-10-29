class ServerCall {
    url;
    params;
    callType;

    constructor(args) {
        this.url = args.url;
        this.params = args.params;
        this.callType = args.callType;
    }

    xhrCall() {
        var me = this;

        return new Promise(function (resolve, reject) {
            var request = new XMLHttpRequest();
            request.open(me.callType, me.url, false);
            request.setRequestHeader("Content-Type", "application/json");

            request.onload = function () {
                if (request.status >= 200 && request.status < 300) {
                    resolve(JSON.parse(request.response));
                } else {
                    reject({
                        status: xhr.status,
                        statusText: xhr.statusText
                    });
                }
            };

            request.onerror = function () {
                reject({
                    status: request.status,
                    statusText: request.statusText
                });
            }

            request.send(me.params);
        });
    }
}

