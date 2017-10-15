 var settings = {
            rows: 6,
            cols: 30,
            rowCssPrefix: 'row-',
            colCssPrefix: 'col-',
            seatWidth: 35,
            seatHeight: 35,
            seatCss: 'seat',
            selectedSeatCss: 'selectedSeat',
            selectingSeatCss: 'selectingSeat'
        };

        var init = function (reservedSeat,selectedSeat) {
            var str = [], seatNo, className;
            for (i = 0; i < settings.rows; i++) {
                for (j = 0; j < settings.cols; j++) {
                    seatNo = (i + j * settings.rows + 1);
                    className = settings.seatCss + ' ' + settings.rowCssPrefix + i.toString() + ' ' + settings.colCssPrefix + j.toString();
                    if ($.isArray(reservedSeat) && $.inArray(seatNo, reservedSeat) != -1) {
                        className += ' ' + settings.selectedSeatCss;
                    }
                    if ($.isArray(selectedSeat) && $.inArray(seatNo, selectedSeat) != -1) {
                        className += ' ' + settings.selectingSeatCss;
                    }
                    str.push('<li class="' + className + '"' +
                        'style="top:' + (i * settings.seatHeight).toString() + 'px;left:' + (j * settings.seatWidth).toString() + 'px">' +
                        '<a title="' + seatNo + '">' + seatNo + '</a>' +
                        '</li>');
                }
            }
            $('#place').html(str.join(''));
        };

        function ValidateIsSeatSelected() {
            var str = [], item;
            $.each($('#place li.' + settings.selectingSeatCss + ' a'), function (index, value) {
                item = $(this).attr('title');
                str.push(item);
            });
            $('#hdnSeatsSelected').val(str.join(','));

            if ($('#hdnSeatsSelected').val() == "" || $('#hdnSeatsSelected').val() == "") {
                alert("Please choose seats");
                return false;
            }
        }