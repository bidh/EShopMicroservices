namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSuccess);

    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is required");
        }
    }
    public class DeleteBasketCommandHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            //todo: delete basket from database
            return new DeleteBasketResult(true);
        }
    }
}
