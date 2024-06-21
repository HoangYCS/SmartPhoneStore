async function updateSelect(url, selectId, idProperty = null, textProperty = null, customTextFunction = null, urlCreate = null, multi = false) {
    try {
        const response = await $.ajax({
            url: url,
            type: 'GET'
        });

        var $select = $('#' + selectId);

        // Read preselect keys from the data attribute and handle null/undefined cases
        var preselectKeysAttr = $select.data('preselect-keys');
        var preselectKeys = preselectKeysAttr ? preselectKeysAttr.toString().split(',') : [];

        $select.empty();

        $.each(response, function (index, item) {
            var value = idProperty ? item[idProperty] : item;
            var text = customTextFunction ? customTextFunction(item) : (textProperty ? item[textProperty] : item);

            var $option = $('<option></option>').text(text).val(value);
            if (preselectKeys.includes(value.toString())) {
                $option.attr('selected', 'selected');
            }
            $select.append($option);
        });

        if (urlCreate) {
            if (customTextFunction) {
                $select.select2({
                    tags: true,
                    multiple: multi,
                    createTag: function (params) {
                        var term = $.trim(params.term);

                        if (term === '') {
                            return null;
                        }

                        return {
                            id: term,
                            text: term,
                            newTag: true
                        };
                    }
                }).on('select2:selecting', function (e) {
                    if (e.params.args.data.newTag) {
                        var name = e.params.args.data.text;
                        $.ajax({
                            type: 'POST',
                            url: urlCreate,
                            data: { name },
                            success: function (response) {
                                $select.append(new Option(name, response.key, true, true)).trigger('change');
                            },
                            error: function (xhr, status, error) {
                                console.log(error);
                            }
                        });
                        $('.select2-search__field').val('').focus();
                        e.preventDefault();
                    }
                });
            } else {
                $select.select2({
                    tags: true,
                    multiple: multi,
                    createTag: function (params) {
                        var term = $.trim(params.term);

                        if (term === '') {
                            return null;
                        }

                        return {
                            id: term,
                            text: term,
                            newTag: true
                        };
                    }
                }).on('select2:selecting', function (e) {
                    if (e.params.args.data.newTag) {
                        var name = e.params.args.data.text;
                        $.ajax({
                            type: 'POST',
                            url: urlCreate,
                            data: { [textProperty]: name },
                            success: function (response) {
                                if (response.success) {
                                    $select.append(new Option(name, response.key, true, true)).trigger('change');
                                } else {
                                    alert(response.notification)
                                }
                            },
                            error: function (xhr, status, error) {
                                console.log(error);
                            }
                        });
                        $('.select2-search__field').val('').focus();
                        e.preventDefault();
                    }
                });
            }
        } else {
            $select.select2({
                tags: true,
                createTag: function (params) {
                    var term = $.trim(params.term);

                    if (term === '') {
                        return null;
                    }

                    return {
                        id: term,
                        text: term,
                        newTag: true
                    };
                },
                templateResult: function (data) {
                    var $result = $("<span></span>");
                    $result.text(data.text);

                    //if (data.newTag) {
                    //    $result.append(" <em>(new)</em>");
                    //}

                    return $result;
                }
            });
        }

        $select.trigger('change');

    } catch (error) {
        console.log("Yêu cầu AJAX thất bại");
        console.log("Trạng thái: " + error.status);
        console.log("Lỗi: " + error.statusText);
    }
}