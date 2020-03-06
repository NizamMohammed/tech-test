/**
 * Create Date/Time Picker (for ASP.NET Maker 2020)
 * @license (C) 2019 e.World Technology Ltd.
 */
ew.dateTimePickerOptions={},ew.createDateTimePicker=function(e,a,t){if(!a.includes("$rowindex$")){var i=jQuery,r=ew.getElement(a,e),o=ew.getElement("sv_"+a,e),n=i(o||r),s="",c=ew.DATETIME_WITHOUT_SECONDS;if(r&&!n.data("DateTimePicker")&&!n.parent().data("DateTimePicker")){var d=function(e,a){return 5==e||9==e?a?9:5:6==e||10==e?a?10:6:7==e||11==e?a?11:7:12==e||15==e?a?15:12:13==e||16==e?a?16:13:14==e||17==e?a?17:14:e},p=(t=Object.assign({},ew.dateTimePickerOptions,t)).format;switch(p>100&&(p-=100,c=!0),0==p?p=ew.DATE_FORMAT_ID:1==p?p=d(ew.DATE_FORMAT_ID,!0):2==p&&(p=d(ew.DATE_FORMAT_ID,!1)),p){case 5:s="YYYY/MM/DD";break;case 6:s="MM/DD/YYYY";break;case 7:s="DD/MM/YYYY";break;case 9:s="YYYY/MM/DD HH:mm"+(c?"":":ss");break;case 10:s="MM/DD/YYYY HH:mm"+(c?"":":ss");break;case 11:s="DD/MM/YYYY HH:mm"+(c?"":":ss");break;case 12:s="YY/MM/DD";break;case 13:s="MM/DD/YY";break;case 14:s="DD/MM/YY";break;case 15:s="YY/MM/DD HH:mm"+(c?"":":ss");break;case 16:s="MM/DD/YY HH:mm"+(c?"":":ss");break;case 17:s="DD/MM/YY HH:mm"+(c?"":":ss")}s=s.replace(/\//g,ew.DATE_SEPARATOR).replace(/:/g,ew.TIME_SEPARATOR),t.format=s,t.locale||(t.locale=ew.LANGUAGE_ID.toLowerCase());var m=!i.isBoolean(t.inputGroup)||t.inputGroup;delete t.inputGroup,t.debug=t.debug||ew.DEBUG;var l={id:a,form:e,enabled:!0,inputGroup:m,options:t};i(function(){if(i(document).trigger("datetimepicker",[l]),l.enabled){if(!1!==l.inputGroup){var a=i('<button class="btn btn-default" type="button"><i class="far fa-calendar-alt"></i></button>').click(function(){n.removeClass("is-invalid")}),t="datetimepicker_"+e+n.attr("id");n.addClass("datetimepicker-input").attr("data-target","#"+t).wrap('<div class="input-group date" id="'+t+'" data-target-input="nearest"></div>').after(i('<div class="input-group-append" data-target="#'+t+'" data-toggle="datetimepicker"></div>').append(a)),n=n.parent().on("change.datetimepicker",function(e){e.date&&(r.value=e.date.format(l.options.format))})}else n.addClass("datetimepicker-input").attr({"data-toggle":"datetimepicker","data-target":"#"+n.attr("id")}).on("change.datetimepicker",function(e){e.date&&(r.value=e.date.format(l.options.format))});l.options.locale&&moment.locale()!=l.options.locale?loadjs(ew.RELATIVE_PATH+"moment/locale/"+l.options.locale+".js",function(){moment.localeData().postformat=function(e){return e},n.datetimepicker(l.options)}):n.datetimepicker(l.options)}})}}};