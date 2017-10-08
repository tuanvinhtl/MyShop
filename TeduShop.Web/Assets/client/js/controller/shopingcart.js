var cart = {
    init: function () {
        cart.registerEvent();
        cart.loadData();
    },
    registerEvent: function () {
        $('.btnDeleteItem').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.removeItem(productId);
        });
        $('#buyMoreproduct').off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = "/";
        });
        $('#deleteAllCart').off('click').on('click', function (e) {
            e.preventDefault();
            cart.removeCart();

        });
        $('#checkOut').off('click').on('click', function (e) {
            e.preventDefault();
            $('#frmCheckOut').show();

        });
        $('#useInfoLoged').off('click').on('click', function () {
            if ($(this).prop('checked')) {
                cart.getInfoUserLogin()
            }
            else {
                $('#txtName').val('')
                $('#txtEmail').val('')
                $('#txtNumberPhone').val('')
                $('#txtOderAddress').val('')
            }


        });
        $('#submitConfirm').off('click').on('click', function (e) {
            e.preventDefault();
            cart.CreateOrder();
        });
        
        $('.txtQuantity').off('keyup').on('keyup', function () {
            var productId = parseInt($(this).data('id'));
            var quantity = parseInt($(this).val());
            var productPrice = parseFloat($(this).data('price'));
            var amount = quantity * productPrice;
            if (!isNaN(amount)) {
                $('#amount_' + productId).text(amount);
            }
            else {
                $('#amount_' + productId).text(1);
            }
            $('#totalAmount').text(cart.getTotalOrder());
            $('.simpleCart_total').text(cart.getTotalOrder());
            cart.updateQuantity();
        })
    },

    CreateOrder: function () {
        var orderObject = {

            CustumerName: $('#txtName').val(),
            CustummerEmail: $('#txtEmail').val(),
            CustummerAddress: $('#txtOderAddress').val(),
            CustummerMessage: $('#txtMessage').val(),
            CustummerMobile: $('#txtNumberPhone').val(),
        }
        $.ajax({
            url: '/Cart/CreateOrder',
            type: 'POST',
            data: {
                orderViewModel: JSON.stringify(orderObject)
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    cart.removeCart();
                }
                if (response.status==false) {
                    console.log('has a error. cant send order')
                }
            }
        })
    },
    getInfoUserLogin: function () {
        $.ajax({
            url: '/Cart/GetInfoUserLogin',
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var user = response.data;
                    $('#txtName').val(user.FullName)
                    $('#txtEmail').val(user.Email)
                    $('#txtNumberPhone').val(user.PhoneNumber)
                    $('#txtOderAddress').val(user.Address)
                }
            }
        })
    },
    updateQuantity: function () {
        var cartList = [];
        $.each($('.txtQuantity'), function (i, item) {
            cartList.push({
                CartId: $(item).data('id'),
                Quantity: $(item).val()
            });
        });
        $.ajax({
            url: '/Cart/Update',
            type: 'POST',
            data: {
                cartData: JSON.stringify(cartList)
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                }
            }
        })
    },
    getTotalOrder() {
        var listTextBox = $('.txtQuantity');
        var total = 0;
        $.each(listTextBox, function (i, item) {
            total += parseInt($(item).val()) * parseFloat($(item).data('price'))
        })
        return numeral(total).format('0,0');
    },
    loadData: function () {
        $.ajax({
            url: '/Cart/GetAll',
            type: 'GET',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var template = $('#cartTemplate').html();
                    var html = '';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            productImage: item.Product.Images,
                            productName: item.Product.Name,
                            productId: item.CartId,
                            productPrice: numeral(item.Product.Price).format('0,0'),
                            productQuantity: item.Quantity,
                            productAmount: numeral(item.Product.Price * item.Quantity).format('0,0')
                        });

                    });
                    if (html == '') {
                        $('#tableCart').hide();
                        $('#alertEmptyCart').show();
                    }
                    else {
                        $('#cartBody').html(html);
                        $('#totalAmount').text(cart.getTotalOrder);
                    }
                    cart.registerEvent();
                }
            }
        })
    },
    removeItem: function (productId) {
        $.ajax({
            url: '/Cart/RemoveItem',
            type: 'POST',
            data: {
                productId: productId
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                }
            }
        })
    },
    removeCart: function () {
        $.ajax({
            url: '/Cart/DeleteAll',
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                }
            }
        })
    }
}
cart.init();