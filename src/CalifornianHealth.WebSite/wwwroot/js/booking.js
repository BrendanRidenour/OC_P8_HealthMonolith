(function ($) {
    $("form#select-consultant").submit(function (event) {
        event.preventDefault();

        var $self = $(this);

        var val = $self.find('select[name="consultant"]').val();

        $self.attr("action", val);

        this.submit();
    });
})(jQuery);