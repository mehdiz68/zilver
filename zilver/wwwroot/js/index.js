$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/Home/GetTodayPrices",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var resultAsJson = JSON.parse(JSON.stringify(result.NewRow));
            const yValues = resultAsJson.map(function (item) { return item.price; });
            const xValues = resultAsJson.map(function (item) { return item.title; });

            new Chart("myChart", {
                type: "line",
                data: {
                    labels: xValues,
                    datasets: [
                        {
                            fill: false,
                            lineTension: 0,
                            backgroundColor: "rgba(0,0,255,1.0)",
                            borderColor: "rgba(0,0,255,0.1)",
                            data: yValues,
                        },
                    ],
                },
                options: {
                    legend: { display: false },
                    tooltips: {
                        rtl: true
                    },

                    //scales: {
                    // yAxes: [{ ticks: { min: 4000000, max: 5000000 } }],
                    //},
                },
            });



        }//Success
    });


    $(".slider-site").owlCarousel({
        slideSpeed: 5000,
        rtl: !0,
        autoplay: !0,
        autoplaySpeed: 500,
        smartSpeed: 1000,
        loop: !0,
        dots: !0,
        nav: !0,
        items: 1,
        lazyLoad: !0,
        navText: [
            "<i class='shemsh shemsh-arrow-right'></i>",
            "<i class='shemsh shemsh-arrow-left'></i>",
        ],
    });
    $("#specialoffers .row").owlCarousel({
        slideSpeed: 500,
        margin: 25,
        rtl: !0,
        autoplay: !1,
        loop: !0,
        dots: !0,
        nav: !0,
        items: 1,
        lazyLoad: !0,
        responsive: {
            0: {
                items: 1,
            },
            600: {
                items: 2,
            },
            1000: {
                items: 6,
            },
        },
        navText: [
            "<i class='shemsh shemsh-arrow-right'></i>",
            "<i class='shemsh shemsh-arrow-left'></i>",
        ],
    });
    $("#cats .row").owlCarousel({
        slideSpeed: 500,
        margin:25,
        rtl: !0,
        autoplay: !1,
        loop: !0,
        dots: !0,
        nav: !0,
        items: 1,
        lazyLoad: !0,
        responsive: {
            0: {
                items: 1,
            },
            600: {
                items: 2,
            },
            1000: {
                items: 4,
            },
        },
        navText: [
            "<i class='shemsh shemsh-arrow-right'></i>",
            "<i class='shemsh shemsh-arrow-left'></i>",
        ],
    });
    $("#products-n .row,#products-n-2 .row").owlCarousel({
        slideSpeed: 500,
        rtl: !0,
        margin: 250,
        autoplay: !1,
        loop: !0,
        dots: !0,
        nav: !0,
        items: 1,
        lazyLoad: !0,
        responsive: {
            0: {
                items: 1,
            },
            600: {
                items: 2,
            },
            1000: {
                items: 3,
            },
        },
        navText: [
            "<i class='shemsh shemsh-arrow-right'></i>",
            "<i class='shemsh shemsh-arrow-left'></i>",
        ],
    });
    $("#video .shemsh-video").click(function () {
        let vid = document.getElementById("main-video");
        vid.play();
        $("#video .shemsh-video").css("display", "none");
        $("#video .shemsh-pause").css("display", "flex");
    });
    $("#video .shemsh-pause").click(function () {
        let vid = document.getElementById("main-video");
        vid.pause();
        $("#video .shemsh-pause").css("display", "none");
        $("#video .shemsh-video").css("display", "flex");
    });
    if (screen.width <= 920) {
        $("#newProducts .row").owlCarousel({
            slideSpeed: 500,
            rtl: !0,
            margin: 250,
            autoplay: !0,
            loop: !0,
            dots: !1,
            nav: !0,
            items: 1,
            lazyLoad: !0,
            responsive: {
                0: {
                    items: 1,
                },
                600: {
                    items: 1,
                },
                1000: {
                    items: 1,
                },
            },
            navText: [
                "<i class='shemsh shemsh-arrow-right'></i>",
                "<i class='shemsh shemsh-arrow-left'></i>",
            ],
        });
    }
});
