var common = {
    init: function () {
        common.registerEvent();
    },
    registerEvent: function () {
        $('.btnAddToCart').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            $.ajax({
                url: '/Cart/AddToCart',
                type: 'POST',
                data: {
                    productId: productId
                },
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        alert('Sản phẩm đã được thêm vào giỏ hàng !');
                    }
                }
            })
        })
        
    }

}
common.init();
