using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IOrderService
    {
        bool CreateOrder(Order order, List<OrderDetail> orderDetail);
    }
    public class OrderService : IOrderService
    {
        IOrderRepository _orderRepository;
        IOrderDetailRepository _orderDetailRepository;
        IUnitOfWork _unitOfWork;
        public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IOrderDetailRepository orderDetailRepository)
        {
            this._orderRepository = orderRepository;
            this._orderDetailRepository = orderDetailRepository;
            this._unitOfWork = unitOfWork;
        }
        public bool CreateOrder(Order order,List<OrderDetail> orderDetail)
        {
            try
            {
                _orderRepository.Add(order);
                _unitOfWork.Commit();
                foreach (var orderDetailItem in orderDetail)
                {
                    orderDetailItem.OderID = order.ID;
                    _orderDetailRepository.Add(orderDetailItem);
                }
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
           
        }
    }
}
