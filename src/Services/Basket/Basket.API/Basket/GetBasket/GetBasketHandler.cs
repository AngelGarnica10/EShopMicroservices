﻿namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart Cart);
    public class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery quey, CancellationToken cancellationToken)
        {
            //TODO: get basket from db
            //var basket = await _repository.GetBasket(request.UserName);
            return new GetBasketResult(new ShoppingCart("swn"));
        }
    }
}