(this.webpackJsonpespresso = this.webpackJsonpespresso || []).push([
  [0],
  {
    112: function (e) {
      e.exports = JSON.parse('{"serverUrl":"http://b893fcf81004.ngrok.io"}');
    },
    113: function (e) {
      e.exports = JSON.parse('{"serverUrl":"http://localhost:8000"}');
    },
    114: function (e) {
      e.exports = JSON.parse('{"serverUrl":""}');
    },
    115: function (e) {
      e.exports = JSON.parse('{"serverUrl":""}');
    },
    128: function (e, t, a) {
      e.exports = { container: 'touchable_container__29tNB' };
    },
    130: function (e, t, a) {
      e.exports = { container: 'body_container__1VOCi' };
    },
    133: function (e, t, a) {
      e.exports = { container: 'main_container__2sVsS' };
    },
    136: function (e, t, a) {
      e.exports = a(209);
    },
    16: function (e, t, a) {
      e.exports = {
        wrapperLink: 'article_wrapperLink__3zsS1',
        featuredMain: 'article_featuredMain__1f0SL',
        latest: 'article_latest__31GPf',
        coverImage: 'article_coverImage__dFzId',
        title: 'article_title__i7kUj',
        featuredRegular: 'article_featuredRegular__2DAgt',
        coverImageWrapper: 'article_coverImageWrapper__2sYf_',
        newsPortalWrapper: 'article_newsPortalWrapper__1uNEP',
        newsPortalIcon: 'article_newsPortalIcon__eeodb',
        textWrapper: 'article_textWrapper__DPUyl',
      };
    },
    191: function (e, t, a) {
      var n = {
        './settings': 54,
        './settings.development': 112,
        './settings.development.json': 112,
        './settings.json': 54,
        './settings.local': 113,
        './settings.local.json': 113,
        './settings.production': 114,
        './settings.production.json': 114,
        './settings.test': 115,
        './settings.test.json': 115,
      };
      function r(e) {
        var t = i(e);
        return a(t);
      }
      function i(e) {
        if (!a.o(n, e)) {
          var t = new Error("Cannot find module '" + e + "'");
          throw ((t.code = 'MODULE_NOT_FOUND'), t);
        }
        return n[e];
      }
      (r.keys = function () {
        return Object.keys(n);
      }),
        (r.resolve = i),
        (e.exports = r),
        (r.id = 191);
    },
    208: function (e, t, a) {},
    209: function (e, t, a) {
      'use strict';
      a.r(t);
      var n,
        r = a(0),
        i = a.n(r),
        c = a(127),
        s = a.n(c),
        o = a(57),
        l = a(35),
        u = a(20),
        m = a.n(u),
        g = a(128),
        f = a.n(g),
        d = function (e) {
          var t = e.onClick,
            a = e.children,
            n = e.className,
            r = e.type,
            c = void 0 === r ? 'button' : r,
            s = Object(l.a)(e, ['onClick', 'children', 'className', 'type']),
            o = i.a.useCallback(
              function (e) {
                t && t(e);
              },
              [t]
            );
          return i.a.createElement(
            'button',
            Object.assign(
              { onClick: o, type: c, className: m()(f.a.container, n) },
              s
            ),
            a
          );
        },
        p = a(4),
        _ = a(10),
        v = a(37),
        b = function (e) {
          return i.a.createElement(
            'svg',
            Object.assign(
              {
                xmlns: 'http://www.w3.org/2000/svg',
                xmlnsXlink: 'http://www.w3.org/1999/xlink',
                width: '25px',
                height: '25px',
                viewBox: '0 0 25 25',
                version: '1.1',
              },
              e
            ),
            i.a.createElement(
              'title',
              null,
              '3BA9149F-222F-43D4-9E44-836E9369ABC0'
            ),
            i.a.createElement(
              'g',
              {
                id: 'Web',
                stroke: 'none',
                strokeWidth: '1',
                fill: 'none',
                fillRule: 'evenodd',
              },
              i.a.createElement(
                'g',
                {
                  id: 'Artboard-Copy-4',
                  transform: 'translate(-1396.000000, -21.000000)',
                  fill: '#000000',
                  fillRule: 'nonzero',
                },
                i.a.createElement(
                  'g',
                  {
                    id: 'Budicon-Outline/Arrows/arrow-right',
                    transform: 'translate(1396.000000, 21.000000)',
                  },
                  i.a.createElement('path', {
                    d:
                      'M23,12.493856 C23,12.6264885 22.9474242,12.7537096 22.8535,12.847356 L15.8535,19.847356 C15.6573106,20.0368421 15.3454576,20.0341322 15.1525907,19.8412653 C14.9597238,19.6483984 14.9570139,19.3365455 15.1465,19.140356 L21.293,12.993856 L1.5,12.993856 C1.22385763,12.993856 1,12.7699984 1,12.493856 C1,12.2177136 1.22385763,11.993856 1.5,11.993856 L21.293,11.993856 L15.1465,5.84735601 C14.9570139,5.65116657 14.9597238,5.33931362 15.1525907,5.14644672 C15.3454576,4.95357982 15.6573106,4.95086991 15.8535,5.14035601 L22.8535,12.140356 C22.9474242,12.2340024 23,12.3612235 23,12.493856 Z',
                    id: 'Shape',
                  })
                )
              )
            )
          );
        },
        h = a(23),
        E = a.n(h);
      !(function (e) {
        (e.FORWARDS = 'FORWARDS'), (e.BACKWARDS = 'BACKWARDS');
      })(n || (n = {}));
      var w = function (e, t, a) {
          var r = a.findIndex(function (t) {
            return t === e;
          });
          return -1 === r ||
            (0 === r && t === n.BACKWARDS) ||
            (r === a.length - 1 && t === n.FORWARDS)
            ? null
            : a[t === n.BACKWARDS ? r - 1 : r + 1];
        },
        I = function (e) {
          var t = e.categories,
            a = e.routes,
            r = Object(_.g)(),
            c = Object(_.h)(),
            s = Object(_.i)().path,
            l = void 0 === s ? '' : s,
            u = i.a.useCallback(
              function (e) {
                r.push(e);
              },
              [r]
            ),
            g = function (e) {
              var t = e.currentTarget.dataset.action;
              if (t) {
                var n = w(l, t, a);
                null !== n && u(n);
              }
            };
          i.a.useEffect(
            function () {
              var e = function (e) {
                var t = e.key;
                if ('ArrowLeft' === t || 'ArrowRight' === t) {
                  var r = w(l, 'ArrowLeft' === t ? n.BACKWARDS : n.FORWARDS, a);
                  if (null === r) return;
                  u(r);
                }
              };
              return (
                window.addEventListener('keydown', e),
                function () {
                  window.removeEventListener('keydown', e);
                }
              );
            },
            [r, l, a, u]
          );
          var f = i.a.useCallback(
              function (e) {
                e.preventDefault(), '/' !== c.pathname && u('/');
              },
              [c.pathname, u]
            ),
            h = function (e) {
              e.preventDefault();
              var t = e.currentTarget.dataset.url;
              'undefined' !== typeof t && u(t);
            };
          return i.a.createElement(
            p.Flex,
            {
              className: E.a.container,
              justifyContent: 'space-between',
              alignItems: 'center',
              fluid: !0,
            },
            i.a.createElement(
              v.b,
              { to: '/', className: E.a.logoLink, onClick: f },
              i.a.createElement('img', {
                src: '/logo512.png',
                alt: 'Espresso logo',
                className: E.a.logo,
              })
            ),
            i.a.createElement(
              p.Flex,
              { className: E.a.items },
              a.map(function (e, a) {
                var n = t[a];
                return i.a.createElement(
                  v.c,
                  {
                    key: n.id,
                    to: e,
                    'data-url': e,
                    isActive: function () {
                      return l === e;
                    },
                    activeClassName: E.a.navItem__active,
                    className: E.a.navItem,
                    onClick: h,
                  },
                  i.a.createElement(
                    p.Text,
                    {
                      className: m()(
                        E.a.navItemText,
                        Object(o.a)({}, E.a.navItem__activeText, l === e)
                      ),
                      weight: 'semibold',
                      size: 'caption',
                    },
                    n.name
                  )
                );
              })
            ),
            i.a.createElement(
              p.Flex,
              { className: E.a.arrowsWrapper },
              i.a.createElement(
                d,
                { disabled: '' === l, className: E.a.arrowBtn },
                i.a.createElement(b, {
                  className: E.a.arrowLeft,
                  'data-action': n.BACKWARDS,
                  onClick: g,
                })
              ),
              i.a.createElement(
                d,
                { disabled: l === a[a.length - 1], className: E.a.arrowBtn },
                i.a.createElement(b, { 'data-action': n.FORWARDS, onClick: g })
              )
            )
          );
        },
        x = a(34),
        y = a.n(x),
        O = a(58),
        k = a(75),
        N = a(30),
        T = a(135),
        j = a(130),
        S = a.n(j),
        C = function (e) {
          var t = e.children;
          return i.a.createElement(
            p.Flex,
            { flexDirection: 'column', className: S.a.container },
            t
          );
        },
        P = a(11),
        A = a(39);
      function F() {
        var e = Object(A.a)([
          '\n  query GET_FEATURED_ARTICLES($take: Int = 10, $skip: Int = 0, $minTimestamp: String = "", $newsPortalIds: String = "", $categoryIds: String = "") {\n    featuredArticles(\n        take: $take,\n        skip: $skip,\n        minTimestamp: $minTimestamp,\n        newsPortalIds: $newsPortalIds,\n        categoryIds: $categoryIds,\n    ) ',
          ' {\n      articles {\n        id\n        url\n        title\n        imageUrl\n        publishDateTime\n        newsPortal {\n          name\n          iconUrl\n        }\n        categories {\n          id\n          name\n          color\n        }\n      }\n    }\n  }\n',
        ]);
        return (
          (F = function () {
            return e;
          }),
          e
        );
      }
      var L = function (e) {
          return Object(P.gql)(
            F(),
            e
              ? '@rest(type: "GetFeaturedArticlesQueryResponse", path: "/articles/featured?take={args.take}&skip={args.skip}&minTimestamp={args.minTimestamp}&newsPortalIds={args.newsPortalIds}&categoryIds={args.categoryIds}")'
              : ''
          );
        },
        D = { gql: L(!1), rest: L(!0) };
      function R() {
        var e = Object(A.a)([
          '\n  query GET_LATEST_ARTICLES($take: Int = 5, $skip: Int = 0, $minTimestamp: String = "", $newsPortalIds: String = "", $categoryIds: String = "", $titleSearchQuery: String = "") {\n    latestArticles(\n      take: $take,\n      skip: $skip,\n      minTimestamp: $minTimestamp,\n      newsPortalIds: $newsPortalIds,\n      categoryIds: $categoryIds,\n      titleSearchQuery: $titleSearchQuery\n    ) ',
          ' {\n      articles {\n        id\n        url\n        title\n        imageUrl\n        publishDateTime\n        newsPortal {\n          name\n          iconUrl\n        }\n        categories {\n          id\n          name\n          color\n        }\n      }\n    }\n  }\n',
        ]);
        return (
          (R = function () {
            return e;
          }),
          e
        );
      }
      var W = function (e) {
          return Object(P.gql)(
            R(),
            e
              ? '@rest(type: "GetLatestArticlesQueryResponse", path: "/articles?take={args.take}&skip={args.skip}&minTimestamp={args.minTimestamp}&newsPortalIds={args.newsPortalIds}&categoryIds={args.categoryIds}")'
              : ''
          );
        },
        U = { gql: W(!1), rest: W(!0) },
        B = a(132),
        $ = a(47),
        z = a.n($),
        M = a(16),
        q = a.n(M),
        G = function (e) {
          var t = e.href,
            a = e.className,
            n = e.children;
          return i.a.createElement(
            'a',
            {
              href: t,
              className: m()(q.a.wrapperLink, a),
              target: '_blank',
              rel: 'noopener noreferrer',
            },
            n
          );
        },
        K = {
          Featured: function (e) {
            var t = e.article,
              a = e.className,
              n = e.variant,
              r = Object(l.a)(e, ['article', 'className', 'variant']),
              c = t.url,
              s = t.imageUrl,
              o = t.title,
              u = t.newsPortal,
              g = t.publishDateTime;
            return 'main' === n
              ? i.a.createElement(
                  G,
                  Object.assign({}, r, {
                    href: c,
                    className: m()(q.a.featuredMain, a),
                  }),
                  i.a.createElement(p.Image, {
                    src: s || '/assets/images/logo.png',
                    fallbackSrc: '/assets/images/logo.png',
                    className: q.a.coverImage,
                  }),
                  i.a.createElement(
                    p.Flex,
                    { flexDirection: 'column' },
                    i.a.createElement(
                      p.Text,
                      {
                        title: o,
                        className: q.a.title,
                        weight: 'bold',
                        align: 'left',
                        size: 'h1',
                      },
                      o.length > 120
                        ? ''.concat(o.substring(0, 120).trim(), '...')
                        : o
                    ),
                    i.a.createElement(
                      p.Flex,
                      { alignItems: 'center' },
                      i.a.createElement(p.Image, {
                        src: u.iconUrl || '/assets/images/logo.png',
                        fallbackSrc: '/assets/images/logo.png',
                        className: q.a.newsPortalIcon,
                      }),
                      i.a.createElement(
                        p.Text,
                        { size: 'caption' },
                        ''.concat(u.name, ' \u2022 ').concat(z()(g).fromNow())
                      )
                    )
                  )
                )
              : i.a.createElement(
                  G,
                  Object.assign({}, r, {
                    href: c,
                    className: m()(q.a.featuredRegular, a),
                  }),
                  i.a.createElement(
                    p.Flex,
                    { className: q.a.coverImageWrapper },
                    i.a.createElement(
                      p.Flex,
                      {
                        className: q.a.newsPortalWrapper,
                        alignItems: 'center',
                      },
                      i.a.createElement(p.Image, {
                        src: u.iconUrl || '/assets/images/logo.png',
                        fallbackSrc: '/assets/images/logo.png',
                        className: q.a.newsPortalIcon,
                      }),
                      i.a.createElement(
                        p.Text,
                        { size: 'small', color: 'white' },
                        u.name
                      )
                    ),
                    i.a.createElement(p.Flex, {
                      className: q.a.coverImage,
                      style: {
                        backgroundImage: 'url('.concat(
                          s || '/assets/images/logo.png',
                          ')'
                        ),
                      },
                      disableStyles: !0,
                    })
                  ),
                  i.a.createElement(
                    p.Text,
                    { title: o, weight: 'bold', align: 'left' },
                    o.length > 80
                      ? ''.concat(o.substring(0, 80).trim(), '...')
                      : o
                  )
                );
          },
          Latest: function (e) {
            var t = e.article,
              a = t.url,
              n = t.imageUrl,
              r = t.title,
              c = t.newsPortal,
              s = t.publishDateTime,
              o = e.className,
              u = Object(l.a)(e, ['article', 'className']);
            return i.a.createElement(
              G,
              Object.assign({}, u, { href: a, className: m()(q.a.latest, o) }),
              i.a.createElement(p.Image, {
                src: n || '/assets/images/logo.png',
                fallbackSrc: '/assets/images/logo.png',
                className: q.a.coverImage,
              }),
              i.a.createElement(
                p.Flex,
                { flexDirection: 'column', className: q.a.textWrapper },
                i.a.createElement(
                  p.Text,
                  {
                    title: r,
                    className: q.a.title,
                    weight: 'bold',
                    align: 'left',
                  },
                  r.length > 80
                    ? ''.concat(r.substring(0, 80).trim(), '...')
                    : r
                ),
                i.a.createElement(
                  p.Text,
                  { size: 'caption', align: 'left' },
                  ''.concat(c.name, ' \u2022 ').concat(z()(s).fromNow())
                )
              )
            );
          },
        },
        H = a(29),
        Q = a(41),
        J = a.n(Q),
        V = i.a.memo(function (e) {
          var t = e.className,
            a = e.size,
            n = void 0 === a ? 48 : a,
            r = Object(l.a)(e, ['className', 'size']);
          return i.a.createElement(
            'svg',
            Object.assign(
              {
                className: m()(J.a.loadingSpinner, t),
                width: ''.concat(n, 'px'),
                height: ''.concat(n, 'px'),
                viewBox: '0 0 66 66',
                xmlns: 'http://www.w3.org/2000/svg',
              },
              r
            ),
            i.a.createElement('circle', {
              className: J.a.circle,
              fill: 'none',
              strokeWidth: '6',
              strokeLinecap: 'round',
              cx: '33',
              cy: '33',
              r: '30',
            })
          );
        }),
        Y = i.a.memo(function () {
          return (
            Object(H.useFullLifecyle)(
              function () {
                document.body.style.overflow = 'hidden';
              },
              function () {
                document.body.style.removeProperty('overflow');
              }
            ),
            i.a.createElement(
              p.Flex,
              {
                className: J.a.loader,
                flexDirection: 'column',
                justifyContent: 'center',
                alignItems: 'center',
              },
              i.a.createElement(V, { className: J.a.spinner }),
              i.a.createElement(
                p.Text,
                {
                  className: J.a.text,
                  align: 'center',
                  weight: 'semibold',
                  size: 'h3',
                },
                'U\u010ditavanje...'
              )
            )
          );
        }),
        X = a(48),
        Z = a.n(X),
        ee = function (e) {
          var t = e.articles,
            a = e.loading,
            n = Object(B.a)(t),
            r = n[0],
            c = n.slice(1);
          return i.a.createElement(
            p.Flex,
            { className: Z.a.container, flexDirection: 'column', fluid: !0 },
            i.a.createElement(
              p.Text,
              {
                weight: 'bold',
                align: 'left',
                color: 'muted',
                size: 'caption',
                transform: 'uppercase',
                className: Z.a.subtitle,
              },
              'Izdvojeno'
            ),
            a
              ? i.a.createElement(
                  p.Flex,
                  { flexOut: !0 },
                  i.a.createElement(V, { size: 24 })
                )
              : i.a.createElement(
                  p.Flex,
                  { flexDirection: 'column' },
                  r &&
                    i.a.createElement(K.Featured, {
                      className: Z.a.mainArticle,
                      article: r,
                      variant: 'main',
                    }),
                  i.a.createElement(
                    p.Flex,
                    {
                      className: Z.a.list,
                      justifyContent: 'space-between',
                      flexWrap: 'wrap',
                    },
                    c.map(function (e) {
                      return i.a.createElement(K.Featured, {
                        key: e.id,
                        article: e,
                        variant: 'regular',
                      });
                    })
                  )
                )
          );
        },
        te = a(42),
        ae = a.n(te),
        ne = function (e) {
          var t = e.articles,
            a = e.onRefetch,
            n = e.loading,
            r = e.refetching,
            c = i.a.useRef(!1),
            s = Object(H.useWindowDimensions)().height,
            o = i.a.useMemo(
              function () {
                var e = s - 138;
                return {
                  LIST_HEIGHT: e,
                  VISIBLE_LIST_ITEM_COUNT: Math.floor(e / 108),
                };
              },
              [s]
            ),
            l = o.LIST_HEIGHT,
            u = o.VISIBLE_LIST_ITEM_COUNT,
            m = (function () {
              var e = Object(O.a)(
                y.a.mark(function e(t) {
                  var n, r, i;
                  return y.a.wrap(function (e) {
                    for (;;)
                      switch ((e.prev = e.next)) {
                        case 0:
                          if (
                            ((n = t.currentTarget),
                            (r = n.scrollTop),
                            (i = n.scrollHeight),
                            !(r + 108 * u >= i - 540) || c.current)
                          ) {
                            e.next = 5;
                            break;
                          }
                          return (c.current = !0), (e.next = 5), a();
                        case 5:
                        case 'end':
                          return e.stop();
                      }
                  }, e);
                })
              );
              return function (t) {
                return e.apply(this, arguments);
              };
            })();
          return (
            i.a.useEffect(
              function () {
                c.current = !1;
              },
              [t.length]
            ),
            i.a.createElement(
              p.Flex,
              { className: ae.a.container, disableStyles: !0 },
              i.a.createElement(
                p.Text,
                {
                  weight: 'bold',
                  align: 'left',
                  color: 'muted',
                  size: 'caption',
                  transform: 'uppercase',
                  className: ae.a.subtitle,
                },
                'Najnovije'
              ),
              n
                ? i.a.createElement(
                    p.Flex,
                    { flexOut: !0 },
                    i.a.createElement(V, { size: 24 })
                  )
                : i.a.createElement(
                    p.Flex,
                    {
                      flexDirection: 'column',
                      onScroll: m,
                      className: ae.a.listWrapper,
                      style: { height: l },
                    },
                    i.a.createElement(
                      p.Flex,
                      {
                        flexDirection: 'column',
                        className: ae.a.list,
                        style: { height: 108 * t.length },
                      },
                      t.map(function (e) {
                        return i.a.createElement(K.Latest, {
                          key: e.id,
                          article: e,
                          className: ae.a.article,
                        });
                      }),
                      r && i.a.createElement(V, { size: 24 })
                    )
                  )
            )
          );
        },
        re = a(133),
        ie = a.n(re),
        ce = function (e) {
          var t = e.category,
            a = e.newsPortalIds,
            n = i.a.useRef(0),
            c = Object(r.useState)([]),
            s = Object(T.a)(c, 2),
            o = s[0],
            l = s[1],
            u = -1 === +t.id ? void 0 : +t.id,
            m = Object(P.useQuery)(D.gql, {
              variables: Object(N.a)(
                Object(N.a)({}, u ? { categoryIds: ''.concat(u) } : {}),
                {},
                { newsPortalIds: a.join(',') }
              ),
            }),
            g = Object(P.useQuery)(U.rest, {
              variables: Object(N.a)(
                Object(N.a)({}, u ? { categoryIds: ''.concat(u) } : {}),
                {},
                { take: 20, skip: n.current, newsPortalIds: a.join(',') }
              ),
              onCompleted: function (e) {
                l(function (t) {
                  return [].concat(
                    Object(k.a)(t),
                    Object(k.a)(e.latestArticles.articles)
                  );
                });
              },
            }),
            f = i.a.useCallback(
              Object(O.a)(
                y.a.mark(function e() {
                  return y.a.wrap(function (e) {
                    for (;;)
                      switch ((e.prev = e.next)) {
                        case 0:
                          return (
                            (n.current += 20),
                            (e.next = 3),
                            g.refetch({ skip: n.current })
                          );
                        case 3:
                        case 'end':
                          return e.stop();
                      }
                  }, e);
                })
              ),
              [g]
            );
          return (
            i.a.useEffect(
              function () {
                n.current = 0;
              },
              [t.id]
            ),
            i.a.createElement(
              C,
              null,
              i.a.createElement(
                p.Flex,
                { className: ie.a.container },
                i.a.createElement(ee, {
                  articles: m.data ? m.data.featuredArticles.articles : [],
                  loading: m.loading,
                }),
                i.a.createElement(ne, {
                  articles: o,
                  onRefetch: f,
                  loading: g.loading,
                  refetching: g.networkStatus === P.NetworkStatus.refetch,
                })
              )
            )
          );
        };
      function se() {
        var e = Object(A.a)([
          '\n  query GET_CONFIGURATION {\n    webConfiguration ',
          ' {\n      categories {\n        id\n        name\n        color\n        url\n      }\n      newsPortalIds\n    }\n  }\n',
        ]);
        return (
          (se = function () {
            return e;
          }),
          e
        );
      }
      var oe,
        le = function (e) {
          return Object(P.gql)(
            se(),
            e
              ? '@rest(type: "GetWebConfigurationQueryResponse", path: "/web-configuration")'
              : ''
          );
        },
        ue = { gql: le(!1), rest: le(!0) },
        me = function () {
          var e = Object(_.i)().path,
            t = void 0 === e ? '' : e,
            a = Object(P.useQuery)(ue.gql),
            n = a.loading,
            r = a.data,
            c = i.a.useMemo(
              function () {
                var e = r
                    ? (function (e) {
                        return e.filter(function (e) {
                          return 'Lokalno' !== e.name;
                        });
                      })(r.webConfiguration.categories)
                    : [],
                  a = e.map(function (e) {
                    return e.url.replace('/', '');
                  }),
                  n = (function (e, t, a) {
                    var n = t.findIndex(function (t) {
                      return t === e;
                    });
                    if (-1 !== n) {
                      var r = a[n];
                      if (r) return r;
                    }
                  })(t, a, e);
                return {
                  allCategories: e,
                  allRoutes: a,
                  newsPortalIds: r ? r.webConfiguration.newsPortalIds : [],
                  currentCategory: n,
                };
              },
              [r, t]
            ),
            s = c.allCategories,
            o = c.allRoutes,
            l = c.newsPortalIds,
            u = c.currentCategory;
          return (
            Object(H.useDocumentTitle)(function () {
              return u
                ? ''.concat(u.name, ' - ').concat('Espresso')
                : 'Espresso';
            }),
            n
              ? i.a.createElement(Y, null)
              : u && 0 !== o.length && o.includes(t)
              ? i.a.createElement(
                  i.a.Fragment,
                  null,
                  i.a.createElement(I, {
                    categories: s,
                    routes: o,
                    onRouteChange: function () {},
                  }),
                  i.a.createElement(ce, { newsPortalIds: l, category: u })
                )
              : i.a.createElement('div', null, 'Page not found.')
          );
        },
        ge = function () {
          return i.a.createElement(
            v.a,
            null,
            i.a.createElement(
              _.d,
              null,
              i.a.createElement(_.b, {
                path: '/home',
                exact: !0,
                component: function () {
                  return i.a.createElement(_.a, { to: '/' });
                },
              }),
              i.a.createElement(_.b, {
                path: '/:path?',
                exact: !0,
                component: me,
              }),
              i.a.createElement(_.b, {
                component: function () {
                  return i.a.createElement('div', null, 'Page not found.');
                },
              })
            )
          );
        },
        fe = a(134),
        de = a(65),
        pe = a(66);
      !(function (e) {
        (e.LOCAL = 'local'),
          (e.DEVELOPMENT = 'development'),
          (e.PRODUCTION = 'production'),
          (e.TEST = 'test');
      })(oe || (oe = {}));
      var _e = (function () {
          function e(t, a, n) {
            var r = this;
            Object(de.a)(this, e),
              (this.environment = void 0),
              (this.settings = void 0),
              (this.environment = n),
              (this.settings = Object(N.a)(Object(N.a)({}, t), a)),
              Object.keys(this.settings).forEach(function (e) {
                var n;
                Object.defineProperty(r, e, {
                  value: null !== (n = a[e]) && void 0 !== n ? n : t[e],
                });
              });
          }
          return (
            Object(pe.a)(e, [
              {
                key: 'getProperty',
                value: function (e) {
                  if (!Object.prototype.hasOwnProperty.call(this.settings, e))
                    throw new Error('Missing key.');
                  return this.settings[e];
                },
              },
            ]),
            e
          );
        })(),
        ve = a(54),
        be = (function () {
          function e() {
            Object(de.a)(this, e);
          }
          return (
            Object(pe.a)(e, null, [
              {
                key: 'getConfiguration',
                value: function () {
                  return (
                    this.configuration || this.buildConfiguration(),
                    this.configuration
                  );
                },
              },
              {
                key: 'buildConfiguration',
                value: function () {
                  var e = Object({
                      NODE_ENV: 'production',
                      PUBLIC_URL: '',
                      WDS_SOCKET_HOST: void 0,
                      WDS_SOCKET_PATH: void 0,
                      WDS_SOCKET_PORT: void 0,
                    }).REACT_APP_ENVIRONMENT,
                    t = this.getAppSettingsFileName(e),
                    n = a(191)('./'.concat(t)),
                    r = new _e(ve, n, e);
                  this.configuration = r;
                },
              },
              {
                key: 'getAppSettingsFileName',
                value: function (e) {
                  if (Object.values(oe).includes(e))
                    return 'settings.'.concat(e, '.json');
                  throw new Error('Undefined Environment!');
                },
              },
            ]),
            e
          );
        })();
      be.configuration = void 0;
      var he = be.getConfiguration(),
        Ee = he.getProperty('headers'),
        we = he.getProperty('serverUrl'),
        Ie = new fe.RestLink({
          headers: Ee,
          credentials: 'omit',
          uri: ''.concat(we, '/api'),
        }),
        xe = new P.HttpLink({
          uri: ''.concat(we, '/graphql'),
          headers: Ee,
          credentials: 'omit',
        }),
        ye = new P.InMemoryCache(),
        Oe = new P.ApolloClient({
          cache: ye,
          headers: Ee,
          link: P.ApolloLink.from([Ie, xe]),
        }),
        ke = '#000000',
        Ne = '#1D1D1B',
        Te = '#D7545A',
        je = '#FFFFFF';
      z.a.locale('hr');
      var Se = function () {
          var e = Object(H.useStringHelpers)().hexToRgba;
          return i.a.createElement(
            P.ApolloProvider,
            { client: Oe },
            i.a.createElement(
              p.TextConfigProvider,
              {
                config: {
                  colors: {
                    primary: ke,
                    secondary: e(Ne, 0.4),
                    white: '#FFFFFF',
                    black: '#000000',
                    muted: e(Ne, 0.4),
                    error: Te,
                    success: je,
                  },
                },
              },
              i.a.createElement(ge, null)
            )
          );
        },
        Ce = a(74),
        Pe = a.n(Ce);
      Boolean(
        'localhost' === window.location.hostname ||
          '[::1]' === window.location.hostname ||
          window.location.hostname.match(
            /^127(?:\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}$/
          )
      );
      a(208);
      var Ae = be.getConfiguration();
      (Pe.a.defaults.baseURL = Ae.getProperty('serverUrl')),
        (Pe.a.defaults.headers = Ae.getProperty('headers')),
        s.a.render(
          i.a.createElement(i.a.StrictMode, null, i.a.createElement(Se, null)),
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
    23: function (e, t, a) {
      e.exports = {
        container: 'navbar_container__1bbBA',
        logoLink: 'navbar_logoLink__33f5T',
        logo: 'navbar_logo__1nWOE',
        items: 'navbar_items__1vLL2',
        navItem: 'navbar_navItem__1bmye',
        navItem__active: 'navbar_navItem__active__PP6FB',
        navItem__activeText: 'navbar_navItem__activeText__1YAiD',
        navItemText: 'navbar_navItemText__2qY7z',
        arrowsWrapper: 'navbar_arrowsWrapper__L637-',
        arrowBtn: 'navbar_arrowBtn__3UPIA',
        arrowLeft: 'navbar_arrowLeft__20nKw',
      };
    },
    41: function (e, t, a) {
      e.exports = {
        loadingSpinner: 'loader_loadingSpinner__vMYOn',
        rotator: 'loader_rotator__35mpy',
        circle: 'loader_circle__1l4bC',
        dash: 'loader_dash__1g9v0',
        loader: 'loader_loader__2sbTU',
        text: 'loader_text__TUPgW',
      };
    },
    42: function (e, t, a) {
      e.exports = {
        container: 'latest_articles_container__1y2vf',
        subtitle: 'latest_articles_subtitle__1SBPw',
        listWrapper: 'latest_articles_listWrapper__1nDdj',
        article: 'latest_articles_article__3YABa',
        virtualizedList: 'latest_articles_virtualizedList__3rigk',
      };
    },
    48: function (e, t, a) {
      e.exports = {
        container: 'featured_articles_container__1cBop',
        subtitle: 'featured_articles_subtitle__2cy3m',
        mainArticle: 'featured_articles_mainArticle__3oDl5',
        list: 'featured_articles_list__w9jar',
      };
    },
    54: function (e) {
      e.exports = JSON.parse(
        '{"headers":{"espresso-api-key":"09ed7f5c-00bb-4c2d-9051-1c12de62abf9","espresso-api-version":"1.4","version":"1.1.0","device-type":"7"}}'
      );
    },
  },
  [[136, 1, 2]],
]);
//# sourceMappingURL=main.20cead8e.chunk.js.map
