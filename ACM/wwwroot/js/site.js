// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

var App = App || {};

App.BaseFunctions = {
    InitPage: function () {
        App.BaseFunctions.SetupUIUpdateConnection();
        App.BaseFunctions.SetupLoading();
    },
    SetMenuFocus: function (menuTab, menuSubTab) {
        var subClassHasSub = false;

        if (menuTab != null && menuTab != "") {
            $("#main-menu-navigation li.active").removeClass("active");
            subClassHasSub = $("#" + menuTab).hasClass("has-sub");

            if (subClassHasSub == true) {
                $("#" + menuTab).addClass("open");
            }
            else {
                $("#" + menuTab).addClass("active");
            }
        }
        if (menuSubTab != null && menuSubTab != "" && subClassHasSub == true) {
            $("#" + menuSubTab).addClass("active");
        }
    },
    UnbindLoading: function () {
        $(document).off('ajaxSend');
        $(document).off('ajaxStop');
        $(document).off('ajaxError');

        document.beforeunload = null;
    },
    SetupLoading: function () {
        window.onbeforeunload = function () {
            App.BaseFunctions.ShowLoading();
        };

        $(document).ajaxSend(function (e, xhr, opt) {
            App.BaseFunctions.ShowLoading();
        });
        $(document).ajaxStop(function (e, xhr, opt) {
            App.BaseFunctions.HideLoading();
        });
        $(document).ajaxError(function (e, xhr, opt) {
            App.BaseFunctions.HideLoading();
        });
    },
    ShowLoading: function () {
        $(".SpinnerContainer").show();
    },
    HideLoading: function () {
        $(".SpinnerContainer").hide();
    },
    uuidv4: function () {
        return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
            (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
        );
    },
    SetupUIUpdateConnection: function () {
        App.UIUpdateConnection = new signalR.HubConnectionBuilder()
            .withUrl("/uiUpdateHub")
            .withAutomaticReconnect()
            .build();

        App.UIUpdateConnection.on("UpdateUI", function (eventCode, data) {
            switch (eventCode) {
                case "NOTIFICATIONS": {
                    var usrNotifications = $("#userNotificationDDL");

                    $.get("/home/userNotifications", function (response) {
                        usrNotifications.html(response);
                    });
                } break;
                case "DOCUMENT_UPDATE_VIRUS_SCANNED": {
                    var dataObj = JSON.parse(data);

                    var event = new CustomEvent("document_updated_virusscan", { detail: dataObj });

                    document.dispatchEvent(event);
                } break;
                case "PRESENTATION_STARTED": {
                    var dataObj = JSON.parse(data);

                    var event = new CustomEvent("presentation_started", { detail: dataObj });

                    document.dispatchEvent(event);
                } break;
                case "PRESENTATION_STOPPED": {
                    var dataObj = JSON.parse(data);

                    var event = new CustomEvent("presentation_stopped", { detail: dataObj });

                    document.dispatchEvent(event);
                } break;
                case "PRESENTATION_PAGE_CHANGE": {
                    var dataObj = JSON.parse(data);

                    var event = new CustomEvent("presentation_page_changed", { detail: dataObj });

                    document.dispatchEvent(event);
                } break;
                case "VIDEO_UPLOAD_ENCODE_PROGRESS": {
                    var dataObj = JSON.parse(data);

                    var event = new CustomEvent("video_upload_encode_progress", { detail: dataObj });

                    document.dispatchEvent(event);
                } break;
                case "VIDEO_CALL_STARTED": {
                    var dataObj = JSON.parse(data);

                    var event = new CustomEvent("videocall_started", { detail: dataObj });

                    document.dispatchEvent(event);
                } break;
                case "VIDEO_CALL_STOPPED": {
                    var dataObj = JSON.parse(data);

                    var event = new CustomEvent("videocall_stopped", { detail: dataObj });

                    document.dispatchEvent(event);
                } break;
            }
        });

        App.UIUpdateConnection.start().catch(function (err) {
            return console.error(err.toString());
        });
        App.UIUpdateConnection.onclose(function (e) {
            App.UIUpdateConnection.start().catch(function (err) {
                return console.error(err.toString());
            });
        });
    }
};

// Write your JavaScript code.
$(function () {
    App.BaseFunctions.InitPage();
});
