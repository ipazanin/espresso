(this['webpackJsonpreact-boilerplate-typescript'] =
  this['webpackJsonpreact-boilerplate-typescript'] || []).push([
  [0],
  {
    3: function (e, n, t) {
      e.exports = t(8);
    },
    8: function (e, n, t) {
      'use strict';
      t.r(n);
      var a = t(0),
        o = t.n(a),
        r = t(2),
        c = t.n(r),
        i = function (e) {
          var n = e.name;
          return o.a.createElement('h5', null, n);
        },
        l = function (e) {
          var n = e.events;
          return o.a.createElement(
            'div',
            null,
            n.map(function (e) {
              var n = e.name;
              return o.a.createElement(i, { key: n, name: n });
            })
          );
        };
      console.log(
        Object({
          NODE_ENV: 'production',
          PUBLIC_URL: '',
          WDS_SOCKET_HOST: void 0,
          WDS_SOCKET_PATH: void 0,
          WDS_SOCKET_PORT: void 0,
          REACT_APP_ENVIRONMENT: '',
        })
      );
      var E = function () {
          return o.a.createElement(
            'div',
            null,
            o.a.createElement(
              'div',
              null,
              Object({
                NODE_ENV: 'production',
                PUBLIC_URL: '',
                WDS_SOCKET_HOST: void 0,
                WDS_SOCKET_PATH: void 0,
                WDS_SOCKET_PORT: void 0,
                REACT_APP_ENVIRONMENT: '',
              }).REACT_APP_TEST
            ),
            o.a.createElement(l, {
              events: [{ name: 'First event' }, { name: 'Second event' }],
            })
          );
        },
        u = function () {
          return o.a.createElement('div', null, o.a.createElement(E, null));
        };
      Boolean(
        'localhost' === window.location.hostname ||
          '[::1]' === window.location.hostname ||
          window.location.hostname.match(
            /^127(?:\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}$/
          )
      );
      c.a.render(
        o.a.createElement(o.a.StrictMode, null, o.a.createElement(u, null)),
        document.getElementById('root')
      ),
        'serviceWorker' in navigator &&
          navigator.serviceWorker.ready
            .then(function (e) {
              e.unregister();
            })
            .catch(function (e) {
              console.error(e.message);
            });
    },
  },
  [[3, 1, 2]],
]);
//# sourceMappingURL=main.5d374146.chunk.js.map
