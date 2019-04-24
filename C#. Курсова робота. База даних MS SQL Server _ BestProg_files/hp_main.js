this.fitie = function(t) {
    function e() {
        c.call(t, g + m, e);
        var a = { boxSizing: "content-box", display: "inline-block", overflow: "hidden" };
        "backgroundColor backgroundImage borderColor borderStyle borderWidth bottom fontSize lineHeight height left opacity margin position right top visibility width".replace(/\w+/g, function(t) { a[t] = l[t] }), d.border = d.margin = d.padding = 0, d.display = "block", d.height = d.width = "auto", d.opacity = 1;
        var h = t.videoWidth || t.width,
            s = t.videoHeight || t.height,
            u = h / s,
            v = document.createElement("object-fit");
        v.appendChild(t.parentNode.replaceChild(v, t));
        for (var p in a) v.runtimeStyle[p] = a[p];
        var b;
        "fill" === i ? f ? (d.width = o, d.height = n) : (d["-ms-transform-origin"] = "0% 0%", d["-ms-transform"] = "scale(" + o / h + "," + n / s + ")") : (r > u ? "contain" === i : "cover" === i) ? (b = n * u, d.width = Math.round(b) + "px", d.height = n + "px", d.marginLeft = Math.round((o - b) / 2) + "px") : (b = o / u, d.width = o + "px", d.height = Math.round(b) + "px", d.marginTop = Math.round((n - b) / 2) + "px")
    }
    var i = t.currentStyle["object-fit"];
    if (i && /^(contain|cover|fill)$/.test(i)) {
        var o = t.clientWidth,
            n = t.clientHeight,
            r = o / n,
            a = t.nodeName.toLowerCase(),
            d = t.runtimeStyle,
            l = t.currentStyle,
            h = t.addEventListener || t.attachEvent,
            c = t.removeEventListener || t.detachEvent,
            g = t.addEventListener ? "" : "on",
            f = "img" === a,
            m = f ? "load" : "loadedmetadata";
        h.call(t, g + m, e), t.complete && e()
    }
}, this.fitie.init = function() {
    if (document.body)
        for (var t = document.querySelectorAll("img,video"), e = -1; t[++e];) fitie(t[e]);
    else setTimeout(fitie.init)
}, /MSIE|Trident/.test(navigator.userAgent) && this.fitie.init();

if (!('remove' in Element.prototype)) {
    Element.prototype.remove = function() {
        if (this.parentNode) {
            this.parentNode.removeChild(this);
        }
    };
}

var contentContainer = document.querySelector('.content-container');

var atl = new TimelineMax();
var feed = {};
var config = {};
var products = [];

var productTimelines = [null];
var userInteraction = false;
var currentProductIndex = 1;
var bannerTimedOut = false;

function loadExternalConfig() {
    var script = document.createElement('script');
    script.onload = init;
    var cacheBurst = '?' + Math.random();
    script.src = dynamicContent.Microspot_FeedGenerator_v5b_Output_final[0].config_PriceStock.URL + cacheBurst;
    document.head.appendChild(script);
}


function consolidateData() {

    // Read the config string and place it in the config object
    if (feed['config']) {
        var jsonString = feed['config'].substring(1, feed['config'].length - 1)
        config = JSON.parse(jsonString);
    }

    // create a valid set of all products to display (mainly if product has a price and is in stock)
    for (var key in config_PriceStock) {
        if (config_PriceStock.hasOwnProperty(key) && typeof(config_PriceStock[key].price) === 'number' && isNaN(config_PriceStock[key].price) === false && config_PriceStock[key].outofstock === false) {
            var product = {}
            product.image = feed[key + "_image_url"]["Url"]
            product.title = feed[key + "_title"]
            product.legal = feed[key + "_legal"]
            product.cta = feed[key + "_cta"]
            product.stopper_text = feed[key + "_stopper_text"]
            product.stopper_percent = feed[key + "_stopper_percent"]
            product.brand_url = feed[key + "_brand_url"]["Url"]
            product.exit_url = feed[key + "_exit_url"]["Url"]
            product.price = config_PriceStock[key].price
            product.config = {}
            product.config = config[key]
            products.push(product)
        }
    }
}

function setDynamicData() {

    // set language
    setLanguage(feed['language']);

    // Create the containers for the products
    for (var i = 2; i <= products.length; i++) {

        var child = contentContainer.querySelector('#p1').cloneNode(true);
        child.id = 'p' + i;
        contentContainer.appendChild(child);
    }

    products.forEach(function(product, i) {

        var obj = contentContainer.querySelector('#p' + (i + 1));





        // image
        var image = obj.querySelector('.product-image')
        image.setAttribute('src', product.image);
        setTransformImage(image, product.config.image);

        // brand logo
        obj.querySelector('.brand-image').setAttribute('src', product.brand_url);

        // price
        var price = obj.querySelector('.product-price');
        price.innerText = getPrice(product.price);
        setTextTransform(price, product.config.price);

        // product title
        var title = obj.querySelector('.product-title');
        title.innerText = product.title;
        setTextTransform(title, product.config.title);

        // legal
        var legal = obj.querySelector('.legal');
        legal.innerText = product.legal;
        setTextTransform(legal, product.config.legal);

        //cta
        var cta = obj.querySelector('.cta')
        cta.innerText = product.cta;
        setCtaTransform(cta, product.config.cta);

        //stopper
        setStopper(obj, product.stopper_text, product.stopper_percent);
        // Exit
        obj.exitURL = product.exit_url;
        obj.addEventListener('click', function(e) {
            Enabler.exitOverride("dynamic Exit", e.target.exitURL);
        });

        // hide everything
        obj.style.display = 'none'
    })


}

function initTimeline() {

    atl.pause();

    atl.add('zero', 0);
    atl.add('intro', 0.5); //27 59

    atl.set(['.arrow-left', '.arrow-right'], { autoAlpha: 0 }, 0);
    atl.set('#hp_ad_container', { visibility: 'visibile' }, 0);
    atl.to('.hp_start_container', 1, { zIndex: 1, width: 218, transformOrigin: 'left top', ease: Power4.easeInOut }, 'intro');
    atl.to('.hp_start_logo', 1, { left: 40, transformOrigin: 'left top', ease: Power4.easeInOut }, 'intro');
    atl.to('.hp_start_logo_text', 1, {
        transformOrigin: 'left top',
        ease: Power4.easeInOut,
        onComplete: function() {
            if (products.length > 1) { TweenMax.to(['.arrow-left', '.arrow-right'], 1, { autoAlpha: 1 }, 0); }
            productTimelines[currentProductIndex].play(0);
        }
    }, 'intro');

    for (var i = 1; i <= products.length; i++) {
        var ptl = new TimelineMax();
        ptl.pause();
        var easeIn = Power3.easeOut;
        var easeOut = Power3.easeIn;
        ptl.add(function() {
            if (this.timeline._reversed) {
                productTimelines[currentProductIndex].play(0);
                this.timeline._reversed = false;
            }
            userInteraction = false;
        }, 0);
        ptl.add('in', 1.3);
        ptl.add('out', 4.1);

        ptl.set('#p' + i + ' .product-image-container', { scale: 0 }, 0);
        ptl.set('#p' + i + ' .stopper-container', { scale: 0 }, 0);
        //ptl.set( '#p' + i + ' .brand-image-container',   { scale: 0 }, 0);
        ptl.set('#p' + i + ' .product-price-container', { scale: 0 }, 0);
        ptl.set('#p' + i + ' .product-title-container', { yPercent: 500 }, 0);
        ptl.set('#p' + i + ' .legal-container', { yPercent: 500 }, 0);
        ptl.set('#p' + i + ' .cta-container', { yPercent: 500 }, 0);
        ptl.set('#p' + i + ' .product-image-container', { rotation: 0.01 }, 0);
        ptl.set('#p' + i, { display: 'block' }, 0.1);
        ptl.set('.content-slide', { xPercent: 0 }, 0.1);

        ptl.to('#p' + i + ' .product-image-container', 0.5, { rotation: 0.01, scale: 1, ease: Back.easeOut }, 0.1);
        ptl.to('#p' + i + ' .stopper-container', 0.5, { scale: 1, ease: Back.easeOut }, 0.2);
        //ptl.to( '#p' + i + ' .brand-image-container',   0.5, { scale: 1, ease: Back.easeOut }, 0.3);
        ptl.to('#p' + i + ' .product-price-container', 0.5, { scale: 1, ease: Back.easeOut }, 0.4);
        ptl.to('#p' + i + ' .product-title-container', 0.5, { yPercent: 0, ease: easeIn }, 0.5);
        ptl.to('#p' + i + ' .legal-container', 0.5, { yPercent: 0, ease: easeIn }, 0.6);
        ptl.to('#p' + i + ' .cta-container', 0.5, { yPercent: 0, ease: easeIn }, 0.7);

        ptl.add(function() {
            if (bannerTimedOut) {
                productTimelines[currentProductIndex].pause();
                productTimelines[currentProductIndex].seek(2);
            }
        }, 2);

        ptl.to('#p' + i + ' .cta-container', 0.3, { scale: 1.2, yoyo: true, repeat: 3 }, 2);
        ptl.to('#p' + i + ' .stopper-container', 0.5, { xPercent: -200, ease: easeOut }, 4.1);
        ptl.to('#p' + i + ' .product-image-container', 0.5, { xPercent: -200, ease: easeOut }, 4.2);
        //ptl.to( '#p' + i + ' .brand-image-container',   0.5, { yPercent: -200, ease: easeOut }, 4.3);
        ptl.to('#p' + i + ' .product-price-container', 0.5, { xPercent: 500, ease: easeOut }, 4.4);
        ptl.to('#p' + i + ' .cta-container', 0.5, { yPercent: 500, ease: easeOut }, 4.5);
        ptl.to('#p' + i + ' .legal-container', 0.5, { yPercent: 500, ease: easeOut }, 4.6);
        ptl.to('#p' + i + ' .product-title-container', 0.5, { yPercent: 500, ease: easeOut }, 4.7);
        ptl.set('#p' + i, { display: 'none' }, 5.3);
        ptl.add(function() {
            if (userInteraction === false) { currentProductIndex++; if (currentProductIndex > products.length) { currentProductIndex = 1; } }
            playNextProduct();
        }, 5.3);

        productTimelines.push(ptl);
    }

    document.getElementById('hp_ad_container').style.visibility = 'visible';

}

function initNavigation() {
    document.querySelector('.arrow-left').onclick = function() {
        userInteraction = true;
        productTimelines[currentProductIndex].pause();
        TweenMax.to('.content-slide', 0.3, { xPercent: -100, onComplete: playNextProduct });
        currentProductIndex--;
        if (currentProductIndex < 1) { currentProductIndex = products.length; }
    }

    document.querySelector('.arrow-right').onclick = function() {
        userInteraction = true;
        productTimelines[currentProductIndex].pause();
        TweenMax.to('.content-slide', 0.3, { xPercent: 100, onComplete: playNextProduct });
        currentProductIndex++;
        if (currentProductIndex > products.length) { currentProductIndex = 1; }
    }

}

// --------------------------------------------
//                 I N I T
// --------------------------------------------

function init() {
    // --------------------------------------------
    // IMPORTANT: this line is for local version
    // --------------------------------------------
    feed = dynamicContent.Microspot_FeedGenerator_v5b_Output_final[0];
    // For the live Version also delete the devDynamicContent Object completly in the index.html
    // This will reduce the filesize by approx 20KB!
    // Also please delete the the config_template Object at the end of this file for uploading to DCS, its just for showing the structure
    consolidateData();
    setDynamicData();
    initTimeline();
    initNavigation();
    // Start the timeline
    atl.play(0);
    setTimeout(function() {
        productTimelines[currentProductIndex].pause();
        productTimelines[currentProductIndex].seek(2);
        bannerTimedOut = true;
    }, 30000);
}



// --------------------------------------------
//             F U N C T I O N S
// --------------------------------------------

function playNextProduct() {
    TweenMax.set('.content-slide', { display: 'none' });
    productTimelines[currentProductIndex].play(0);
    userInteraction = false;
}


function getPrice(price) {
    price.toFixed(2);
    var chf = Math.floor(price)
    var cent = Math.round((price - chf) * 100) / 100;
    cent = cent.toFixed(2)
    chf = chf.toString();
    cent = (cent !== 0) ? cent.toString().substr(2, 2) : '00';
    chf = (chf.length > 3) ? [chf.slice(0, chf.length - 3), "'", chf.slice(chf.length - 3)].join('') : chf;
    return chf + '.' + cent;
}

function setStopper(obj, text, percent) {
    text = text.toLowerCase()
    obj.querySelector('.rabatt').style.visibility = 'hidden';
    obj.querySelector('.new').style.visibility = 'hidden';
    obj.querySelector('.deal').style.visibility = 'hidden';
    obj.querySelector('.sale').style.visibility = 'hidden';
    obj.querySelector('.promo').style.visibility = 'hidden';
    obj.querySelector('.cashback').style.visibility = 'hidden';
    switch (text) {
        case "rabatt":
            obj.querySelector('.rabatt').style.visibility = 'visible';
            obj.querySelector('.percent').textContent = percent + '%';
            break;
        case "new":
            obj.querySelector('.new').style.visibility = 'visible';
            break;
        case "deal":
            obj.querySelector('.deal').style.visibility = 'visible';
            break;
        case "sale":
            obj.querySelector('.sale').style.visibility = 'visible';
            break;
        case "promo":
            obj.querySelector('.promo').style.visibility = 'visible';
            break;
        case "cashback":
            obj.querySelector('.cashback').style.visibility = 'visible';
            break;
        default:
            obj.querySelector('.stopper-container').style.display = 'none';
    }
}



function setTransformImage(image, transformations) {
    var scale = (transformations['scale']) ? transformations['scale'] : 1;
    image.style.transform = 'scale(' + scale + ')';
    var x = (transformations['x']) ? transformations['x'] : 0;
    image.style.left = x + 'px';
    var y = (transformations['y']) ? transformations['y'] : 0;
    image.style.top = y + 'px';
}

function setTextTransform(text, transformations) {
    var fontsize = (transformations['fontsize'] !== 0 && transformations['fontsize'] !== undefined) ? transformations['fontsize'] : undefined;
    if (fontsize) {
        text.style.fontSize = fontsize + 'px';
    }
    var lineheight = (transformations['lineheight'] !== 0 && transformations['lineheight'] !== undefined) ? transformations['lineheight'] : undefined;
    if (lineheight) {
        text.style.lineHeight = lineheight + 'px';
        if (text.classList.contains('product-title')) { text.style.maxHeight = lineheight * 4 + 'px'; }
    }
    var color = (transformations['color'] !== "" && transformations['color'] !== undefined) ? transformations['color'] : undefined;
    if (color && validate_hex(color)) {
        text.style.color = color;
    }
}

function setCtaTransform(text, transformations) {
    var fontsize = (transformations['fontsize'] !== 0 && transformations['fontsize'] !== undefined) ? transformations['fontsize'] : undefined;
    if (fontsize) {
        text.style.fontSize = fontsize + 'px';
    } else {
        if (text.innerHTML.length > 8) {
            text.style.fontSize = '12px';
        }
    }
    var color = (transformations['color'] !== "" && transformations['color'] !== undefined) ? transformations['color'] : undefined;
    if (color && validate_hex(color)) {
        text.style.color = color;
    }
    var backgroundColor = (transformations['backgroundColor'] !== "" && transformations['backgroundColor'] !== undefined) ? transformations['backgroundColor'] : undefined;
    if (backgroundColor && validate_hex(backgroundColor)) {
        text.parentNode.style.backgroundColor = backgroundColor;
    }
}

function validate_hex(hex) {
    if (hex[0] != '#') { hex = '#' + hex }
    if (/(^#[0-9A-F]{6}$)|(^#[0-9A-F]{3}$)/i.test(hex)) {
        return hex;
    } else {
        return null;
    }
}

function setLanguage(lang) {
    lang = lang.toLowerCase();
    var claim = document.querySelector('.hp_start_logo_text');
    switch (lang) {
        case 'de':
            document.querySelector('body').classList.add('de');
            var elements = document.querySelectorAll('.fr, .it');
            for (var i = 0; i < elements.length; i++) {
                elements[i].remove();
            }
            break;
        case 'fr':
            document.querySelector('body').classList.add('fr');
            claim.innerHTML = 'Le prix fait la diff&eacute;rence.';
            var elements = document.querySelectorAll('.de, .it');
            for (var i = 0; i < elements.length; i++) {
                elements[i].remove();
            }
            break;
        case 'it':
            document.querySelector('body').classList.add('it');
            claim.innerHTML = '&Egrave; IL PREZZO A FARE LA DIFFERENZA.';
            var elements = document.querySelectorAll('.de, .fr');
            for (var i = 0; i < elements.length; i++) {
                elements[i].remove();
            }
            break;
    }
}
