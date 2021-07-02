using Azure.Messaging.ServiceBus;
using AzureServiceBus.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OrderAPI.Messages;
using OrderAPI.Models;
using OrderAPI.Repository;
using OrderAPI.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly OrderRepository _orderRepository;

        private ServiceBusProcessor checkOutProcessor;
        private ServiceBusProcessor orderUpdatePaymentStatusProcessor;

        private readonly IConfiguration _configuration;
        private readonly IMessageBusService _messageBus;

        private readonly ServiceBusSettings _serviceBusSettings;

        public AzureServiceBusConsumer(OrderRepository orderRepository, IConfiguration configuration, IMessageBusService messageBus, IOptions<ServiceBusSettings> serviceBusSettings)
        {
            _orderRepository = orderRepository;
            _configuration = configuration;
            _messageBus = messageBus;
            _serviceBusSettings = serviceBusSettings.Value;
            var client = new ServiceBusClient(_serviceBusSettings.ConnectionString);
            checkOutProcessor = client.CreateProcessor(_serviceBusSettings.CheckoutMessageTopic);
            orderUpdatePaymentStatusProcessor = client.CreateProcessor(_serviceBusSettings.OrderUpdatePaymentResultTopic, _serviceBusSettings.SubscriptionCheckOut);
        }

        public async Task Start()
        {
            checkOutProcessor.ProcessMessageAsync += OnCheckOutMessageReceived;
            checkOutProcessor.ProcessErrorAsync += ErrorHandler;
            await checkOutProcessor.StartProcessingAsync();

            orderUpdatePaymentStatusProcessor.ProcessMessageAsync += OnOrderPaymentUpdateReceived;
            orderUpdatePaymentStatusProcessor.ProcessErrorAsync += ErrorHandler;
            await orderUpdatePaymentStatusProcessor.StartProcessingAsync();
        }
        public async Task Stop()
        {
            await checkOutProcessor.StopProcessingAsync();
            await checkOutProcessor.DisposeAsync();

            await orderUpdatePaymentStatusProcessor.StopProcessingAsync();
            await orderUpdatePaymentStatusProcessor.DisposeAsync();
        }
        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task OnCheckOutMessageReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            CheckoutHeaderDto checkoutHeaderDto = JsonConvert.DeserializeObject<CheckoutHeaderDto>(body);

            OrderHeader orderHeader = new OrderHeader()
            {
                UserId = checkoutHeaderDto.UserId,
                FirstName = checkoutHeaderDto.FirstName,
                LastName = checkoutHeaderDto.LastName,
                OrderDetails = new List<OrderDetails>(),
                CardNumber = checkoutHeaderDto.CardNumber,
                CouponCode = checkoutHeaderDto.CouponCode,
                CVV = checkoutHeaderDto.CVV,
                DiscountTotal = checkoutHeaderDto.DiscountTotal,
                Email = checkoutHeaderDto.Email,
                ExpiryMonthYear = checkoutHeaderDto.ExpiryMonthYear,
                OrderTime = DateTime.Now,
                OrderTotal = checkoutHeaderDto.OrderTotal,
                PaymentStatus = false,
                Phone = checkoutHeaderDto.Phone,
                PickupDateTime = checkoutHeaderDto.PickupDateTime
            };

            foreach(var detailList in checkoutHeaderDto.CartDetails)
            {
                OrderDetails orderDetails = new OrderDetails()
                {
                    ProductId = detailList.ProductId,
                    ProductName = detailList.Product.Name,
                    Price = detailList.Product.Price,
                    Count = detailList.Count
                };
                orderHeader.CartTotalItems += detailList.Count;
                orderHeader.OrderDetails.Add(orderDetails);
            }

            await _orderRepository.AddOrder(orderHeader);


            PaymentRequestMessage paymentRequestMessage = new PaymentRequestMessage()
            {
                Name = orderHeader.FirstName + " " + orderHeader.LastName,
                CardNumber = orderHeader.CardNumber,
                CVV = orderHeader.CVV,
                ExpiryMonthYear = orderHeader.ExpiryMonthYear,
                OrderId = orderHeader.OrderHeaderId,
                OrderTotal = orderHeader.OrderTotal,
                Email=orderHeader.Email
            };

            try
            {
                await _messageBus.PublishMessage(paymentRequestMessage, _serviceBusSettings.ConnectionString, _serviceBusSettings.OrderPaymentProcessTopics);
                await args.CompleteMessageAsync(args.Message);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        private async Task OnOrderPaymentUpdateReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            UpdatePaymentResultMessage paymentResultMessage = JsonConvert.DeserializeObject<UpdatePaymentResultMessage>(body);

            await _orderRepository.UpdateOrderPaymentStatus(paymentResultMessage.OrderId, paymentResultMessage.Status);
            await args.CompleteMessageAsync(args.Message);

        }
    }
}
