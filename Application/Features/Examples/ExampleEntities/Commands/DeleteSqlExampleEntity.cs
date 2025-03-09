
namespace Application.Features.Examples.ExampleEntities.Commands;


public class DeleteSqlExampleEntity : IRequestHandler<DeleteSqlExampleEntityCommand, IResult>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ILocalizationService _localizationService;

    public DeleteSqlExampleEntity(IUnitOfWorkAsync unitOfWorkAsync, ILocalizationService localizationService)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _localizationService = localizationService;
    }

    public async Task<IResult> Handle(DeleteSqlExampleEntityCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWorkAsync.ExampleEntityRepository.DeleteSqlRawAsync(request.query);
        string message = await _localizationService.GetLocalizedAsync(
            result > 0 ? ExampleEntiesMessages.SuccessDeleted : ExampleEntiesMessages.ErrorDeleted, ExampleEntiesMessages.SectionName);
        if (result > 0)
            return new SuccessResult(message);
        return new ErrorResult(message);

    }
}

public record DeleteSqlExampleEntityCommand(string query) : IRequest<IResult>;
