# Changelog

All notable changes to this project will be documented in this file.

### v4.31.0 (2023-11-22)

**Feature**

- New log output
- Refactoring the BFF `cacher` module (cluster)
- Refactoring the `deploy` scripts

### v4.5.0 (2023-07-30)

**Feature**

- Remove `amplitudejs` and use native `Audio` instead of a player.

### v4.2.0 (2023-07-22)

**Feature**

- Using Sentry as a monitoring tool

### v4.1.0 (2023-07-21)

**Feature**

- Improve BFF cache
- Add ChatGPT comment

### v4.0.0 (2023-07-13)

**Feature**

- Update various dependencies
- Use `pnpm` instead of `yarn` as a package management tool
- Replacing the Twitter API Service
- Using es modules
- Using [`<script setup>`](https://vuejs.org/api/sfc-script-setup.html)
- Using [`Setup Store`](https://pinia.vuejs.org/core-concepts/#setup-stores)
- Remove SPA mode
- Remove NFT page
- Remove field `__v` from all models
- Rename field `create_at` to `created_at` and `update_at` to `updated_at`
- Rename field `article.thumb` to `article.thumbnail`
- Rename field `article.tag` to `article.tags`
- Rename field `article.category` to `article.categories`
- Rename field `article.disabled_comment` to `article.disabled_comments`
- Rename field `tag.articles_count` to `tag.article_count`
- Rename field `category.articles_count` to `category.article_count`

### v3.22.0 (2022-12-27)

**Feature**

- Improve github sponsors

### v3.21.0 (2022-12-23)

**Feature**

- Update various dependencies
- Upgrade `@vueuse/head` composable
- Improve `<verse>` paragraph style
- Improve meta tags `og:*`
- Keep axios v1.1.3
  - [AxiosError: unexpected end of file](https://github.com/axios/axios/issues/5346)
  - [after upgrading to "axios": "^1.2.1"](https://github.com/axios/axios/issues/5357)

### v3.20.0 (2022-10-13)

**Feature**

- Add markdown `<verse>` paragraph support

### v3.19.0 (2022-10-06)

**Feature**

- NFT page items

### v3.18.0 (2022-10-01)

**Feature**

- Remove merch page
- Remove freelancer page
- Add NFTs page (opensea.io)
- Add twitter and og meta
- Improve fullpage components
- Improve sponsor page
- Redesign lens page
- Refactoring archive page
- Refactoring mobile flow (infinitescroll)

### v3.16.1 (2022-09-28)

**Fix**

- Fix sponsor page hash ID

### v3.16.0 (2022-09-25)

**Feature**

- Improve PC aside search input
- Improve mobile header search mask

### v3.15.0 (2022-09-23)

**Feature**

- Upgrade `site` and `article` vote API
- Remove `identity` store legacy logic

### v3.14.7 (2022-09-18)

**Feature**

- Improve markdown `<u>` style

### v3.14.6 (2022-09-09)

**Feature**

- Improve lens page
- Improve page banners
- Refactoring sponsor component

### v3.14.4 (2022-09-08)

**Feature**

- Support Instagram video media
- Improve Lens page UX

### v3.14.2 (2022-09-05)

**Feature**

- Improve iframe style
- Improve merch page data structure

### v3.14.0 (2022-08-20)

**Feature**

- Upgrade vite to v3
- Improve about page modules
- Improve sponsor style
- Improve `mapbox-gl`
- remove `sanitize-html` and `dompurify` [#9200](https://github.com/vitejs/vite/issues/9200) [#560](https://github.com/apostrophecms/sanitize-html/issues/560)

**Fix**

- Index page Twitter module style

### v3.13.0 (2022-06-08)

**Feature**

- Redesign About page footprint module

### v3.12.0 (2022-05-29)

**Feature**

- Add `fetchStore` in stores
- Redesign About page
- Refactoring stores
- Remove Job page
- Remove AD in mobile flow page

### v3.11.0 (2022-05-01)

**Chore**

- Upgrade deps

**Feature**

- Improve `lozad` directive and composable.
- Improve `webfont` component.
- Improve `ulink` component.
- Improve image loading and error fallback status.

### v3.8.2 (2022-03-18)

**Feature**

- Add Sponsor page
- Upgrade Sponsor component (modal)
- Remove CSS `source-url` and `cdn-url`

### v3.8.0 (2022-03-15)

**Feature**

- Upgrade `lru-cache` to [`v7.x`](https://github.com/isaacs/node-lru-cache/blob/main/CHANGELOG.md#v7---2022-02)
- Improve `highlight.js` themes
- Improve styles

**Chore**

- Upgrade deps
- Remove `@vue/x` deps

### v3.6.30 (2022-03-02)

**Feature**

- Add feedback widget

### v3.6.27 (2022-02-20)

**Feature**

- Add instagram & twitter calendar to About page calendar
- Improve i18n & language
- Improve transforms
- `Udate` component

**Fix**

- Eslint with `@typescript-eslint`

### v3.6.24 (2022-02-17)

**Feature**

- About page GitHub cahrt to article calendar
- Music page to popup

### v3.6.1 (2022-01-16)

**Feature**

- Improve cache & cacher (Redis first & LRU second)

### v3.6.0 (2022-01-15)

**Feature**

- Announcement to Twitter
- improve BFF cacher logic

### v3.5.0 (2022-01-11)

**Feature**

- Bilibili to YouTube

### v3.4.5 (2022-01-05)

**Feature**

- Improve Article detail page

### v3.4.0 (2022-01-03)

**Feature**

- Add BFF proxy server
- Add Plogs on Lens page

### v3.3.6 (2022-01-02)

**Feature**

- Improve copyrighter when focus comment publisher

**Fix**

- Fix comment reply preview

### v3.3.4 (2022-01-01)

**Feature**

- Improve comment components

### v3.3.0 (2021-12-31)

**Feature**

- Disqus comment
- Remove comment blocklist
- Improve global config

### v3.2.15 (2021-12-22)

**Fix**

- Fix SSR render BUG

**Feature**

- Improve global styles
- Improve article detail page layout
- Improve article page like-share
- Improve article page skeleton
- Improve `comment` component
- Improve `loading` component

### v3.2.14 (2021-12-21)

**Fix**

- BFF tunnel service responser
- GTag events

**Feature**

- Improve background style
- Improve gtag events
- Improve state types `Extend`
- Add article language text on desktop flow

### v3.2.11 (2021-12-20)

**Fix**

- filters style url

### v3.2.10 (2021-12-20)

**Feature**

- Improve BFF server cache logic
- Serverless support 🚧

### v3.2.9 (2021-12-18)

**Fix**

- SSR `store.prefetch` [pass `pinia` instance](https://pinia.esm.dev/ssr/#using-the-store-outside-of-setup)

### v3.2.7 (2021-12-18)

**Fix**

- Wallpeper fetch on CSR mounted

### v3.2.6 (2021-12-17)

**Fix**

- Mobile flow page title

### v3.2.5 (2021-12-17)

**Feature**

- `HitHub` > `GitHub`
- Improve axios error infos
- Suspend `Wallpaper` service

### v3.2.4 (2021-12-17)

**Feature**

- Mobile pages
- `Archive` rename to `Flow`
- `Divider` component
- Improve universal styles
- Improve SSR cache logic

### v3.2.3 (2021-12-11)

**Feature**

- Improve email link
- Improve `job` page banner

### v3.2.2 (2021-12-11)3.2.3

**Feature**

- `public` resources
- Improve `archive` page
- Improve `search` input
- Improve `share` component

### v3.2.1 (2021-12-08)

**Fix**

- Fix pages banner height
- Fix PC aside AdSense

### v3.2.0 (2021-12-08)

**Feature**

- SSR > `usePrefetch`
- `Vuex` > `Pinia`
- Add Merch page
- Add Article nav in sidebar
- Refactoring SSR (by vite) done
- Refactoring Markdown parser
- Redesign Music player
- Redesign Comment component
- Improve About/Archive/Lens/Job pages
- Remove Desktop WebSocket
- Add SSR archive (RSS/Sitemap) function
- Upgrade Swiper (remove `vue-awesome-swiper`)

### v3.1.0 (2021-02-16)

**Feature**

- Upgrade deps
- Upgrade vite to 2.x
- Upgrade marked (sanitize)

### v3.0.3 (2020-12-24)

**Fix**

- Fix mobile search logic

**Improve**

- Improve body style for Google AdSense
- Improve music service (delay)

**Feature**

- Upgrade deps

### v3.0.2 (2020-12-16)

**Fix**

- Upgrade music service
- Hidden marked warn message (HACK)
- Add marked comment

**Feature**

- Add github chart server
- Add fortune page
- Add tunnel server for Music/BiliBili/Wallpaper

### v3.0.1 (2020-12-03)

**Fix**

- Console style
- Assets CDN url
- SSR prefetch \* validate
- Error page & 404 Page
- Upgrade deps

### v3.0.0 (2020-12-01)

### v3.0.0.beta-0.1 (2020-05-20)

**Upgrade**

- Nuxt.js to Vuniversal
- Vue -> Vue3
- ...

### v2.5.8 (2020-03-29)

**Update**

- Upgrade vue-awesome-swiper
- Upgrade `webpack.splitChunks` with `nuxt.config.js`

### v2.5.7 (2020-03-27)

**Update**

- Support i18n wallpaper

### v2.5.6 (2020-03-21)

**Add**

- Add ICP link
- Add font `SFMonoRegular` for code

**Update**

- Auto language by device
- Update README.md badges
- Update workflows config
- Update `code` style
- Upgrade Swiper component & filter styles
- Upgrade TypeScript config
- Upgrade ESlint config
- Upgrade /pc/layout/toolbox component to composition-api

**Removed**

- Removed `normalize.css`
- Removed `node-uuid`
- Removed `keep-alive`

### v2.5.4

- Replace App url to GitHub file URL
- Upgrade GA events

### v2.5.3

- Rplace RSS service with xml (nodepress)

### v2.5.2

- Add RSS service
- Improve About page
- Bugfix Comment editor

### v2.5.0

- TypeScript
- Improve styles
- Improve About page
- Improve emoji-rain component
- Improve relink transformer
- Add QR Code service
- Add MyMap module
- Remove RTC module
- Remove ColorBlock component
- Refactoring Music service
- Refactoring Theme to dark mode

### v2.4.9

- Update FUNDING.yml
- Update README.md
- Add instagram to navbar menu

### v2.4.8

- Improve CDN static
- Upgrade static style
- Update images style
- Add tobots.txt for CDN
- Pause `imageExt`

### v2.4.6

- Improve styles with PC / Mobile
- Improve Vlog page animation
- Improve PC Aside sticky logic
- Improve PC Aside ad control logic
- Improve PC Music and App page height logic
- Improve replace `@include hidden()`
- Remove `@css3-prefix xxx`
- Remove background component

### v2.4.5

- Improve mobile mask logic
- Improve styles with comment

### v2.4.4

- Remove project page
- Improve i18n languages
- Improve sponsor page
- Improve announcement component
- Update styles
- Update ads with banner & About page

### v2.4.3

- Improve sponsor page
- Replace project page to link

### v2.4.2

- Add sponsor page

### v2.4.1

- Update UA transformer
- Update article GA styles

### v2.4.0

- Upgrade dependencies
- Update js file modules
- Update package scripts
- Update logo effect
- Update style variables
- Update modules background
- Update console text and styles
- Add GitHub actions CI

### v2.3.0

- Update Nuxt to 2.8
- Update dependencies
- Update the api in the store to maintain a balance between coupling and abstraction
- Update ad slot links
- Optimize comment-related copy
- Optimize file organization
- Abandon the design of isomorphic enumeration constants
- Abandon custom webpack loader

### v2.2.3

- Update documentation
- Optimize the homepage Banner carousel: no longer support drag and scroll under PC, support direct click of images under H5
- Optimize the mobile presentation of the announcement module

### v2.2.2

- Fix the article page switching stuck bug
- Upgrade the PC sidebar ad slot to Alimama

### v2.2.1

- Optimize the ad slot class to prevent being blocked
- Update ads
- Update App Download page related content
- Add Banner ads
- Remove useless files

### v2.2.0

- Upgrade dependencies
- Optimize wallpaper business
- Optimize pop-up style details
- Correct copywriting
- Correct player fault tolerance

### 2.1.4

- Add lazy loading function (temporarily used for article details page and vlog list page)
- Fix the problem that marked does not support Chinese id
- Fix wallpaper humanizeUrl problem
- Fix the problem that lazy loading elements in the comment list do not take effect

### 2.1.2

- Upgrade Nuxt to 2.4.0
- Optimize module
- Add search keyword association
- Add new icon easter egg

### 2.1.0

- Optimize Store design
- Optimize Comment business module logic
- Optimize Coding style
- Optimize markdown parser
- Optimize structure organization
- Optimize the performance of category and tag under i18n
- Optimize Swiper defects under SPA
- Add Vlog business module
- Add skeleton screen instead of loading

### v2.0.0

- Optimize isomorphic constant logic
- Optimize advertising rendering and details
- Optimize mouse display effect
- Optimize copywriting details
- Upgrade Update Nuxt to 2
- Abandon some redundant plug-ins
- Standardize coding style
- Add wallpaper function
- Optimize forward proxy service during development
- Optimize component structure
- Re-abstract some methods
- Add buried data
- Update color matching details and specifications
- Add lyrics function
- Optimize layout details

### v1.2.6

- Fix APP download address
- Update service page copy
- Optimize menu button
- Update iconfont icon

### v1.2.5

- Update Nuxt to v1.4.2
- Update Vue vue-server-renderer vue-template-compiler to 2.5.17
- Fix typos in comment copy
- Add Logo light and shadow effects
- Add Header water ripples

### v1.2.4

- Optimize Most background layers are updated to gradient design

### v1.2.2

- Update iconfont
- Update copy details and geographic map
- Optimize description analysis in site map
- Optimize Temporarily turn off background animation

### v1.2.1

- Update copy
- Add friendly links
- Add "Socialist Core Values" function

### v1.2.0

- Improve the problem that theme variables are invalid and cannot be previewed in development mode
- Add performance mode switch, low-profile mode, turn off background animation, disable or delete rtc, turn off autoplay
- The loading cycle of the face recognition library is delayed when the WebRtc beauty switch is turned on
- Add source type marks (reprinted, original, mixed) to the article list and detail page
- Use the self-developed i18n module to complete the i18n adaptation

### v1.1.0

- The location of the app download button, the background color of the download button is changed to the module background color semi-transparent, the file cannot be placed on the server, it is too slow, and the mobile download page needs to be adapted
- http://music.163.com/#/playlist?id=638949385
- Fixed the player
- Upgraded Nuxt, and used offline-plugin to implement offline applications
- Added bullet screen function
- Optimized widescreen reading function
- Optimized the player
- Optimized the mobile style
- Comment 233333 Try it
- Optimize package dependency issues
- Add online video function based on WebRTC
- Add C++ version of TURN Server
- Add face recognition function
- Add a function called beauty
- Optimize the initialization of asynchronous task management
- Optimize code inspection
- Optimize some code details
- Optimize the player
- The shared clipboard needs to be refactored to remove the library https://surmon.me/article/80
- Add ads on the right
- PWA adaptation https://pwa.nuxtjs.org/modules/workbox.html
- Add multiple theme adaptation

### v1.0.0

- Add garbage collection program (idle-gc) to solve the memory overflow problem
- Add Baidu automatic push function
- Fix the problem caused by Duoshuo Https processing Bug
- The default article thumbnail needs to be designed as a <> icon
- fav.ico
- The slideshow needs to have a designed default thumbnail
- The article list images need to be coordinated with the Qiniu client settings
- The related article images are too large, so adjust one row to 5
- Cancel asynchronous loading of the slideshow?
- Articles on the article page list will wrap if they are too long
- Article views statistics index
- Display the total number of query results in the list head
- Tags include article statistics index
- Search function
- Article details require a filter to generate Tag internal links
- The site map page (used for user navigation) can fold and expand article descriptions, tags, and categories. By default, only 100 articles are displayed, and the rest can be expanded and beautified by clicking
- Each page hides the generated comment data for SEO
- A message-box component is used for error handling, and multiple texts are merged into json and displayed randomly
- Zhihu Weibo for about page
- Add Zhihu links to the sidebar
- Whether multiple list pages are combined into one component
- Article details page, related articles on the details page, details page components and data integration, code highlighting plug-in
- Keywords and descriptions on the article details page fail to cover head
- Duoshuo function needs to be integrated and effective
- The interface of popular articles needs to be integrated with Duoshuo
- README improvement
- vue2 has a bug
- vue-meta\_\_dangerouslyDisableSanitizers has a bug
- google statistics code
- Automatic deployment on the line
- Full site https
- A site map needs to generate an xml page
- Routing meta information needs to be defined (implemented by the 1.0 version of the middleware)
- Time and date clipping cannot be determined by the number of words
- The title of the article page is too small, and the code highlighting plug-in cancels the title
- Article comment callback
- Code highlighting In the part parsed by the highlighting plug-in (jsdom + marked), find the class of code and give lang to pre
- Smooth scrolling is required when scrolling
- Most https
- Synchronize articles to Duoshuo
- Add personal information to the console
- Images should have a hover gradient effect
- Images can be parsed when tags are parsed, and events can be bound to images, resulting in image pop-ups
- When the style of related articles is less than one line, it needs to be optimized
- Introducing the signature agreement in the article, based on the original
- The time is not displayed on the article list page
- Time field update
- When creating a relationship between tags and articles, first connect all a, set a new window to open, and nofollow, the tag array must be sorted first, and the one with more characters is first
- Creating relationships is also implemented through marked, no longer relying on the cheerio library
- Code highlighting through marked Implement line number and positioning effect
- Add and fix the right-click automatic copyright function
- error.vue does not support custom layout, 1.0 improvement
- Reverse proxy needs to make domain name judgment
- Add comment function
- Add mobile device adaptation
- watchTabActive
- Implementation of music player
- Email sharing has bugs
- Add linked to me
- Add app download page
