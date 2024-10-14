/*
* jQuery Cascading Select Lists plug-in
*
* Author:  Dezi van Vuuren
*/

(function ($) {
    $.extend($.fn, {
        cascade: function (options) {
            var dependendentDdl;
            if (options.cascaded.indexOf("#") < 0) {
                dependendentDdl = $('#' + options.cascaded);
            }
            else {
                dependendentDdl = $(options.cascaded);
            }
            options = $.extend({}, $.fn.cascade.defaults, {
                source: options.source, // Source's url
                cascaded: options.cascaded // The ddl element that depends on this list
            }, options);    

            if (dependendentDdl.children().length == 0) {
                dependendentDdl.append('<option value="00000000-0000-0000-0000-000000000000">' + options.dependentStartingLabel + '</option>');
                if (options.disableUntilLoaded) {
                    dependendentDdl.attr('disabled', 'disabled');
                    $(dependendentDdl).trigger("chosen:updated");
                }
            }

            return this.each(function () {
                var sourceDdl = $(this);

                sourceDdl.change(function () {
                    var extraParams = {
                        timestamp: +new Date()
                    };

                    $.each(options.extraParams, function (key, param) {
                        extraParams[key] = typeof param == "function" ? param() : param;
                    });

                    var data = $.extend({ selected: $(this).val() }, extraParams);
                    var dependentSelection = dependendentDdl.val();

                    dependendentDdl.empty()
                        .attr('disabled', 'disabled')
                        .append('<option value="00000000-0000-0000-0000-000000000000">' + options.dependentLoadingLabel + '</option>');
                    $(dependendentDdl).trigger("chosen:updated");


                    if (options.spinnerImg) {
                        dependendentDdl.next('.' + options.spinnerClass).remove();

                        var spinner = $('<img />').attr('src', options.spinnerImg);
                        $('<span class="' + options.spinnerClass + '" />').append(spinner).insertAfter(dependendentDdl);
                    }

                    var responseFunc = function (response) {
                        dependendentDdl.empty().attr('disabled', null);
                        $(dependendentDdl).trigger("chosen:updated");
                        dependendentDdl.next('.' + options.spinnerClass).remove();
                        if (response.optionsList != undefined && response.optionsList != null && response.optionsList.length > 0) {
                            if (options.appendEmpry) {
                                dependendentDdl.append('<option value="00000000-0000-0000-0000-000000000000">' + options.emptyDescription + '</option>');
                            }

                            if (response.optionsList.length == 1 && options.appendEmpry == false) {
                                dependendentDdl.append('<option selected value=' + response.optionsList[0].value + '>' + response.optionsList[0].label + '</option>');
                                $(dependendentDdl).trigger('change');
                            }
                            else {
                                $.each(response.optionsList, function (i, item) {
                                    if (item.value == dependentSelection || dependentSelection.includes(item.value)) {
                                        dependendentDdl.append('<option selected value=' + item.value + '>' + item.label + '</option>');
                                    }
                                    else {
                                        dependendentDdl.append('<option value=' + item.value + '>' + item.label + '</option>');
                                    }
                                });
                                $(dependendentDdl).trigger('change');
                            }

                            if (options.successCallBack != null) {
                                options.successCallBack();
                            }
                        } else {
                            dependendentDdl.empty()
                                .attr('disabled', 'disabled')
                                .append('<option value="00000000-0000-0000-0000-000000000000">' + options.dependentNothingFoundLabel + '</option>');
                            $(dependendentDdl).trigger('change');

                        }
                        $(dependendentDdl).trigger("chosen:updated");
                    }
                    
                    if (options.httpMethod == "GET") {
                        $.get(options.source, data, responseFunc);
                    }
                    else if (options.httpMethod == "POST") {
                        $.post(options.source, data, responseFunc);
                    }
                })
            })
        }
    });

    $.fn.cascade.defaults = {
        sourceStartingLabel: "Select one first",
        dependentNothingFoundLabel: "No elements found",
        dependentStartingLabel: "Select one",
        dependentLoadingLabel: "Loading options",
        disableUntilLoaded: true,
        appendEmpry: false,
        emptyDescription: "All",
        spinnerClass: "cascading-select-spinner",
        extraParams: {},
        successCallBack: null,
        httpMethod: "GET"
    }
})(jQuery);