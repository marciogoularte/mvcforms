(function($) {

    /* Create a sortable collection object
       @param  options  string - a command, optionally followed by additional parameters or
                        Object - settings for attaching a new sortable collection
       @return  jQuery object */
    $.fn.sortablecollection = function(options) {
        if (typeof options == 'string') {
            var otherArgs = Array.prototype.slice.call(arguments, 1),
                method = $.fn.sortablecollection['_' + options];
            return this.each(function() {
                method.apply(this, otherArgs);
            });
        } else {
            settings = $.extend({}, options);
            return this.each(function() {
                $(this).sortable(settings);
                $(this).data('name', settings['name']);
            });
        }
    };

    $.extend($.fn.sortablecollection, {
    
        /* Add an item to the collection
           @param  value    string - value to add to collection
           @param  [label]  string - string to use as label */
        _add: function(value, label) {
            label = label || value;
            var element = $('<li class="ui-state-default" />')
                .text(label)
                .append($('<input type="hidden" name="' + $(this).data('name') +'" />').val(value));
            $(element).appendTo($(this));
            $(this).sortable('refresh');
        }
    });

})(jQuery);
