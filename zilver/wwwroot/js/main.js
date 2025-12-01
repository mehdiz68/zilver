(function ($) {
    $(document).ready(function () {

        /*===================================*
20. COUNTDOWN JS
*===================================*/
        $('.countdown_time').each(function () {
            var endTime = $(this).data('time');
            $(this).countdown(endTime, function (tm) {
                if (parseInt(tm.strftime('%D')) == 0) {
                    $(this).html(tm.strftime('<div class="countdown_box"><div class="countdown-wrap"><span class="countdown hours">%H</span><span class="cd_text">ساعت</span></div></div><div class="countdown_box"><div class="countdown-wrap"><span class="countdown minutes">%M</span><span class="cd_text">دقیقه</span></div></div><div class="countdown_box"><div class="countdown-wrap"><span class="countdown seconds">%S</span><span class="cd_text">ثانیه</span></div></div>'));
                }
                else {
                    $(this).html(tm.strftime('<div class="countdown_box"><div class="countdown-wrap"><span class="countdown days">%D </span><span class="cd_text">روز</span></div></div><div class="countdown_box"><div class="countdown-wrap"><span class="countdown hours">%H</span><span class="cd_text">ساعت</span></div></div><div class="countdown_box"><div class="countdown-wrap"><span class="countdown minutes">%M</span><span class="cd_text">دقیقه</span></div></div><div class="countdown_box"><div class="countdown-wrap"><span class="countdown seconds">%S</span><span class="cd_text">ثانیه</span></div></div>'));
                }
            });
        });

        jQuery(window).on('load', function () {
            setTimeout(function () {
                jQuery("#onload-popup").modal('show', {}, 500);
            }, 3000);

        });

        var host = "http://" + window.location.host + "/";

        jQuery('.search_input').keyup(function () {
            var tthis = jQuery(this);
            jQuery("#SearchList > div .content").html("");
            jQuery("#SearchList > div .prs").html("");
            jQuery("#SearchList > div .products").html("");
            var keyword = tthis.val().toString().trim();
            
            if (keyword.length > 1) {
                jQuery.ajax({
                    type: "POST",
                    url: "/Home/MainSearch3?keyword=" + keyword + "",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {

                        if (window.innerWidth > 768)
                            jQuery("body").addClass("sshe");
                        if (result.statusCode == 200) {
                            jQuery("#SearchList").fadeIn();
                            jQuery("#SearchList").addClass("flex");
                            //Render Result
                            var resultAsJson = JSON.parse(JSON.stringify(result.data));
                            if (resultAsJson.length > 0) {
                                for (var i in resultAsJson) {
                                    var Sr = resultAsJson[i];
                                    var Title = Sr.Title;
                                    var PageAddress = Sr.PageAddress;
                                    var Name = Sr.Name;
                                    var ID = Sr.Id;
                                    var Value = Sr.Value;
                                    var AttachementId = Sr.AttachementId;
                                    var ElementType = Sr.TypeId;

                                    switch (true) {
                                        case (ElementType == 1):
                                            //برند
                                            jQuery("#SearchList .ProductList .content").append("<div class='Brand'><a href='/b/" + ID + "/" + PageAddress + "' target='_blank'>همه کالاهای برند <label>" + Name + "</label></a></div>"); break;
                                        case (ElementType == 2):
                                            //گروه محصول
                                            jQuery("#SearchList .ProductList .content").append("<div class='Category'><a href='/s/" + ID + "/" + PageAddress + "' target='_blank'>همه کالاهای <label>" + Name + "</label></a></div>"); break;
                                        case (ElementType == 3):
                                            {
                                                //محصول
                                                if (Value != null)
                                                    jQuery("#SearchList .ProductList .content").append("<div class='Product'><a href='/s/" + ID + "/" + PageAddress + "?q=" + Title + ' ' + Value + "' target='_blank'>" + Title + " در دسته <label>" + Name + "</label></a></div>");
                                                else
                                                    jQuery("#SearchList .ProductList .content").append("<div class='Product'><a href='/s/" + ID + "/" + PageAddress + "?q=" + Title + "' target='_blank'>" + Title + " در دسته <label>" + Name + "</label></a></div>");
                                                break;
                                            }
                                        case (ElementType == 4): {
                                            jQuery("#SearchList .ProductList .prs").append("<div class='oProduct'><a href='/p/" + ID + "/" + PageAddress + "' target='_blank'><img src='/Content/UploadFiles/" + AttachementId + "' class='img-responsive' /> <label>" + Title + "</label></a></div>");

                                            break;
                                        }
                                    }
                                }

                                var jQuerycarousel = jQuery("#SearchList .ProductList .prs");
                                if (jQuerycarousel.hasClass("owl-loaded"))
                                    jQuerycarousel.removeClass("owl-loaded");
                                if (!jQuerycarousel.hasClass("owl-carousel")) {
                                    jQuerycarousel.addClass("owl-carousel owl-theme owl-drag");
                                    jQuerycarousel.owlCarousel({
                                        dots: false,
                                        loop: false,
                                        nav: true,
                                        margin: 5,
                                        responsive: { 0: { items: 1 }, 481: { items: 1 }, 768: { items: 2 }, 1199: { items: 2 } },
                                        navText: ['<i class="shemsh shemsh-arrow-left"></i>', '<i class="shemsh shemsh-arrow-right"></i>']
                                    });
                                }
                                else {
                                    jQuerycarousel.trigger("destroy.owl.carousel");
                                    jQuerycarousel.owlCarousel({
                                        dots: false,
                                        loop: false,
                                        nav: true,
                                        margin: 5,
                                        responsive: { 0: { items: 1 }, 481: { items: 1 }, 768: { items: 2 }, 1199: { items: 2 } },
                                        navText: ['<i class="shemsh shemsh-arrow-left"></i>', '<i class="shemsh shemsh-arrow-right"></i>']
                                    });
                                }
                            }
                            else {
                                jQuery("#SearchList .ProductList .content").append("<div class='Category'><a href='/Search?q=" + keyword + "' target='_blank'> نمایش همه نتایج برای <label>" + keyword + "</label></a></div>");

                            }

                        }


                        tthis.css("background-image", "");

                    }//Success
                });
            }
            else {
                jQuery("body").removeClass("sshe");
                jQuery("#SearchList").fadeOut();
                jQuery(".search_close").fadeOut();
                jQuery(".search__overlay").removeClass("is-active");
                jQuery("#SearchList").removeClass("flex");
                var jQuerycarousel = jQuery("#SearchList .ProductList .prs");
                if (jQuerycarousel.hasClass("owl-loaded"))
                    jQuerycarousel.removeClass("owl-loaded");
                if (!jQuerycarousel.hasClass("owl-carousel")) {
                    jQuerycarousel.addClass("owl-carousel owl-theme owl-drag");
                }
                else {
                    jQuerycarousel.trigger("destroy.owl.carousel");
                }
            }
        });
        jQuery(".search_input").keyup(function (e) {

            var tthis = jQuery(this);
            var keyword = tthis.val().toString().trim();
            if (keyword.length > 1 && e.keyCode != 13 && e.keyCode != 16 && e.keyCode != 17 && e.keyCode != 18)
                tthis.css("background-image", "url(" + host + "content/Default/images/ajax-loader.svg)").css("background-repeat", "no-repeat");
            else {
                tthis.css("background-image", "");
                var jQuerycarousel = jQuery("#SearchList .ProductList .prs");
                if (jQuerycarousel.hasClass("owl-loaded"))
                    jQuerycarousel.removeClass("owl-loaded");
                if (!jQuerycarousel.hasClass("owl-carousel")) {
                    jQuerycarousel.addClass("owl-carousel owl-theme owl-drag");
                }
                else {
                    jQuerycarousel.trigger("destroy.owl.carousel");
                }
            }
        });
        //jQuery("*").keydown(function (e) {
        jQuery(".search_close").click(function () {
            jQuery("body").removeClass("sshe");
            //if (e.which == 27) {
            jQuery(".search_input").val("");
            jQuery("#SearchList").fadeOut();
            jQuery(".search_close").fadeOut();
            jQuery(".search__overlay").removeClass("is-active");
            jQuery("#SearchList").removeClass("flex");
            jQuery(".search_input").css("background-image", "");

            var jQuerycarousel = jQuery("#SearchList .ProductList .prs");
            if (jQuerycarousel.hasClass("owl-loaded"))
                jQuerycarousel.removeClass("owl-loaded");
            if (!jQuerycarousel.hasClass("owl-carousel")) {
                jQuerycarousel.addClass("owl-carousel owl-theme owl-drag");
            }
            else {
                jQuerycarousel.trigger("destroy.owl.carousel");
            }

        });
        jQuery(document).mouseup(function (e) {
            var container = jQuery("#SearchList,.product_search_form");

            if (!container.is(e.target) // if the target of the click isn't the container...
                && container.has(e.target).length === 0) // ... nor a descendant of the container
            {
                jQuery("body").removeClass("sshe");
                jQuery("#SearchList").hide();
                jQuery(".search_input").val("");
                jQuery(".search_input").css("background-image", "");
                jQuery(".search_close").fadeOut();
                jQuery(".search__overlay").removeClass("is-active");
                jQuery("#SearchList").removeClass("flex");


                var jQuerycarousel = jQuery("#SearchList .ProductList .prs");
                if (jQuerycarousel.hasClass("owl-loaded"))
                    jQuerycarousel.removeClass("owl-loaded");
                if (!jQuerycarousel.hasClass("owl-carousel")) {
                    jQuerycarousel.addClass("owl-carousel owl-theme owl-drag");
                }
                else {
                    jQuerycarousel.trigger("destroy.owl.carousel");
                }

            }
        });

    $(".img-sensitive")
      .parent()
      .prepend(
        "<img src='/content/base/theme/images/sensitive.jpg' class='img-responsive sensitive-text' />"
      );
    $("body").on("click", ".sensitive-text", function () {
      var $this = $(this);

      $("html, body").animate(
        {
          scrollTop: $this.parent().find(".sensitive-text").offset().top,
        },
        800
      );
      $this.parent().find(".sensitive-text").hide();
      $this.parent().find(".img-sensitive").removeClass("img-sensitive");
    });
  
    $(document).mouseup(function (e) {
      var container = $("#SearchList");

      if (
        !container.is(e.target) && // if the target of the click isn't the container...
        container.has(e.target).length === 0
      ) {
        // ... nor a descendant of the container
        container.slideUp();
        $("#search-text").val("");
        $("#search-results-box").html("");
        $(".search-pup-up .search-loading").hide();
      }
    });

    // basaket
    $("#basket-header").click(function () {
      $("header .dropdown-menu-right").toggleClass("show");
    });
    // start menu mobile
    $(".header-mm").click(function (e) {
      e.preventDefault();
      $("#mask").fadeIn(500);
      $("#menumobile").addClass("come-menumobile");
    });
    $("#mask").click(function () {
      $(this).fadeOut(500);
      $("#menumobile").removeClass("come-menumobile");
      $(".sub-menu").removeClass("come-submenu");
      $("#sidebar").removeClass("active");
    });
    $("#nomenumobile").click(function () {
      $("#mask").fadeOut(500);
      $("#menumobile").removeClass("come-menumobile");
      $(".sub-menu").removeClass("come-submenu");
    });
    $("#menumobile .main-mm ul > .menu-item-has-children > a").append(
      "<span class='childer'><i></i></span>"
    );
    $("#menumobile .sub-menu").prepend(
      "<div class='title-sub-head'><span class='sub-closer float-left'><i></i></span><strong class='float-right title-subcome'>بازگشت</strong></div>"
    );
    $("#menumobile .sub-closer").click(function () {
      $(this).parent().parent().removeClass("come-submenu");
    });
    $("#menumobile .childer").click(function (e) {
      e.preventDefault();
      var textmenu = $(this).parent().text();
      $(this).parent().next().addClass("come-submenu");
      $(this)
        .parent()
        .next()
        .find(".title-sub-head")
        .find(".title-subcome")
        .html(textmenu);
    });
    // end menu mobile

    jQuery(".owl-testimonials").owlCarousel({
      slideSpeed: 5000,
      rtl: true,
      autoplay: true,
      autoplay: 30000,
      smartSpeed: 1000,
      loop: false,
      margin: 25,
      stagePadding: 10,
      dots: false,
      nav: true,
      items: 1,
      lazyLoad: true,
      navText: [
        "<i class='icon-chevron-thin-right'></i>",
        "<i class='icon-chevron-thin-left'></i>",
      ],
    });

    jQuery(".faq-carousel").owlCarousel({
      slideSpeed: 500,
      rtl: true,
      autoplay: true,
      autoplaySpeed: 3000,
      smartSpeed: 1000,
      loop: false,
      margin: 20,
      dots: true,
      nav: true,
      lazyLoad: true,
      navText: [
        "<i class='icon-Vector-Stroke-0'></i>",
        "<i class='icon-Vector--Stroke'></i>",
      ],
      responsive: {
        0: {
          items: 1.5,
          margin: 10,
          dots: true,
          nav: false,
        },
        450: {
          items: 2.5,
          margin: 10,
          dots: true,
          nav: false,
        },
        800: {
          items: 3.5,
          margin: 20,
          nav: true,
          dots: false,
        },
        992: {
          items: 4,
          margin: 25,
          nav: true,
          dots: false,
        },
        1200: {
          items: 5,
          margin: 30,
          nav: true,
          dots: false,
        },
      },
    });
    jQuery(".awards-carousel").owlCarousel({
      rtl: true,
      nav: false,
      dots: true,
      margin: 30,
      items: 1,
      stagePadding: 0,
      autoplay: true,
      autoplayTimeout: 5000,
      smartSpeed: 2000,
      navText: [
        "<i class='icon-chevron-thin-right'></i>",
        "<i class='icon-chevron-thin-left'></i>",
      ],
      responsive: {
        0: {
          items: 2,
          loop: true,
          dots: true,
        },
        475: {
          items: 2,
          loop: true,
          dots: true,
        },
        576: {
          items: 3,
          loop: true,
          dots: true,
        },
        992: {
          items: 4,
          dots: true,
        },
        1200: {
          items: 5,
          dots: true,
        },
      },
    });

    jQuery(".feature-carousel").owlCarousel({
      rtl: true,
      nav: true,
      dots: false,
      items: 1,
      margin: 24,
      stagePadding: 5,
      autoplay: true,
      autoplayTimeout: 5000,
      smartSpeed: 2000,
      navText: [
        "<i class='icon-Vector-Stroke-0'></i>",
        "<i class='icon-Vector--Stroke'></i>",
      ],
      responsive: {
        0: {
          items: 1,
        },
        475: {
          items: 1.5,
        },
        576: {
          items: 2,
        },
        992: {
          items: 3,
        },
        1200: {
          items: 4,
        },
        1400: {
          items: 6,
        },
      },
    });

    jQuery(".team-carousel").owlCarousel({
      rtl: true,
      nav: false,
      dots: true,
      margin: 30,
      items: 1,
      stagePadding: 0,
      autoplay: true,
      autoplayTimeout: 5000,
      smartSpeed: 2000,
      navText: [
        "<i class='icon-chevron-thin-right'></i>",
        "<i class='icon-chevron-thin-left'></i>",
      ],
      responsive: {
        0: {
          items: 1,
          loop: true,
          dots: true,
          margin: 10,
        },
        475: {
          items: 1.5,
          loop: true,
          dots: true,
          margin: 20,
        },
        576: {
          items: 2,
          loop: true,
          dots: true,
          margin: 20,
        },
        992: {
          items: 2.5,
          dots: true,
          margin: 40,
        },
        1200: {
          items: 3,
          dots: true,
          margin: 84,
        },
      },
    });
    jQuery(".categories-carousel").owlCarousel({
      rtl: true,
      nav: false,
      dots: false,
      margin: 0,
      items: 1,
      stagePadding: 0,
      autoplay: true,
      autoplayTimeout: 6000,
      smartSpeed: 2000,
      navText: [
        "<i class='icon-chevron-thin-right'></i>",
        "<i class='icon-chevron-thin-left'></i>",
      ],
      responsive: {
        0: {
          items: 1,
          loop: true,
        },
        320: {
          items: 2,
          loop: true,
        },
        576: {
          items: 2.5,
          loop: true,
        },
        992: {
          items: 3,
          loop: true,
        },
        1200: {
          items: 4,
          loop: true,
        },
      },
    });

    /*****************************************************
         gallery js
         *****************************************************/

    jQuery(".owl-gallery").owlCarousel({
      slideSpeed: 500,
      rtl: true,
      stagePadding: 10,
      autoplay: true,
      autoplaySpeed: 3000,
      smartSpeed: 1000,
      loop: false,
      margin: 10,
      dots: false,
      nav: true,
      lazyLoad: true,
      navText: [
        "<i class='icon-Vector-Stroke-0'></i>",
        "<i class='icon-Vector--Stroke'></i>",
      ],
      responsive: {
        0: {
          items: 1,
          margin: 10,
        },
        425: {
          items: 2,
          margin: 10,
        },
        768: {
          items: 3,
          margin: 20,
        },
        992: {
          items: 4,
          margin: 30,
        },
        1200: {
          items: 5,
          margin: 57,
        },
      },
    });

    /*****************************************************
          Single access post 
        *****************************************************/

    jQuery(".help-heading ul li a, .access-post a , .more-tax-desc").on(
      "click",
      function (e) {
        e.preventDefault();
        var hash = this.hash;
        jQuery("html, body").animate(
          {
            scrollTop: jQuery(hash).offset().top,
          },
          800
        );
      }
    );

    /*****************************************************
         seach pop up
         *****************************************************/

    jQuery(".header-search").click(function (e) {
      e.preventDefault();
      jQuery(".search-pup-up").fadeIn(500);
      jQuery(".search-pup-up").addClass("popup-search-active");
    });
    jQuery(".fd-outer").click(function (e) {
      e.preventDefault();
      jQuery(".search-pup-up").fadeOut(500);
      jQuery(".search-pup-up").removeClass("popup-search-active");
    });

    jQuery(".flex-control-thumbs").addClass("owl-carousel");
    jQuery(".flex-control-thumbs").owlCarousel({
      rtl: true,
      nav: true,
      dots: false,
      touchDrag: false,
      mouseDrag: false,
      margin: 15,
      stagePadding: 10,
      autoplay: false,
      //smartSpeed: 1000,
      navText: [
        "<i class='icon-chevron-thin-right'></i>",
        "<i class='icon-chevron-thin-left'></i>",
      ],
      responsive: {
        0: {
          items: 2,
        },
        576: {
          items: 3,
        },
      },
    });
  });

  /* FAQ SCRIPT ACCOTDION */

  jQuery(".tab-links li").click(function (e) {
    e.preventDefault();
    jQuery(".tab-links li").removeClass("active");
    jQuery(this).addClass("active");
    jQuery(".tab-content").removeClass("active in");
    var activeTab = jQuery(this).find("a").attr("href");
    jQuery(activeTab).addClass("active in");
  });

  /* FAQ ACCordion JS */

  jQuery(".accordion").click(function (e) {
    e.preventDefault();
    var $this = jQuery(this);
    if ($this.next().hasClass("show")) {
      $this.next().removeClass("show");
      $this.next().slideUp(350);
      $this.parent().removeClass("active");
      $this.parents(".faqs-item").removeClass("active");
    } else {
      $this.parent().addClass("active");
      $this.parents(".faqs-item").addClass("active");
      $this.parent().parent().find("li .inner").removeClass("show");
      $this.parent().parent().find("li .inner").slideUp(350);
      $this.next().toggleClass("show");
      $this.next().slideToggle(350);
    }
  });

  /*  JS For Gallery */
  if (jQuery(".owl-gallery").length > 0) {
    jQuery(".page_lightgallery").lightGallery({
      thumbnail: true,
      selector: ".gallery_item a",
      subHtmlSelectorRelative: true,
    });
  }
  if (jQuery(".awards-carousel").length > 0) {
    jQuery(".page_lightgallery").lightGallery({
      thumbnail: true,
      selector: ".gallery_item a",
      subHtmlSelectorRelative: true,
    });
  }

  /* JS for ALL sidebar */
  jQuery(".cat-item .children").slideUp();
  jQuery(".cat-item.current-cat-parent>.children").slideDown();
  jQuery(".cat-item.current-cat-ancestor>.children").slideDown();
  jQuery(".cat-item ")
    .children(".children")
    .after("<span class='caticon'></span>");
  jQuery(".cat-item.current-cat-parent ")
    .children(".caticon")
    .addClass("active");
  jQuery(".widget > ul > il > .caticon.active").each(function (index) {
    jQuery(this).hasClass("active", function () {
      jQuery(this).parent(".cat-item").addClass("active");
    });
  });
  jQuery(".cat-item.current-cat-ancestor ")
    .children(".caticon")
    .addClass("active");
  jQuery(".cat-item.current-cat ")
    .parents(".current-cat-parent")
    .addClass("active");
  jQuery(".caticon.active").parents(".widget > ul > li").addClass("active");
  jQuery(".current-cat").addClass("cat-item-select");
  jQuery(".children").parent().addClass("cat-parent");
  jQuery(".cat-parent > .caticon").each(function (index) {
    jQuery(this).on("click", function () {
      jQuery(this).toggleClass("active");
      jQuery(this).siblings(".children").slideToggle(300);
      jQuery(this).parent(".widget > ul > .cat-item").toggleClass("active");
    });
  });

  jQuery(".sidebar-btn").on("click", function () {
    jQuery(this).siblings("#sidebar").toggleClass("active");
    jQuery("#mask").fadeIn();
  });
  jQuery("#sidebar").on("click", ".sidebar-close", function () {
    jQuery(this).parent("#sidebar").toggleClass("active");
    jQuery("#mask").fadeOut();
  });
  jQuery(".next , .prev").parent("li").addClass("not-before");
  jQuery(".page-numbers.current").parent("li").addClass("active-before");
})(jQuery);
