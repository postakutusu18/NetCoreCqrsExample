
namespace Application.Features.Examples.ExampleEntities.Commands.Create;

public class CreateExampleEntityHandler : IRequestHandler<CreateExampleEntityCommand, IDataResult<CreatedExampleEntityResponse>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ExampleEntityRules _exampleEntityRules;

    public CreateExampleEntityHandler(IUnitOfWorkAsync unitOfWorkAsync, ExampleEntityRules exampleEntityRules)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _exampleEntityRules = exampleEntityRules;
    }

    public async Task<IDataResult<CreatedExampleEntityResponse>> Handle(CreateExampleEntityCommand request, CancellationToken cancellationToken)
    {
        await _exampleEntityRules.ExampleEntityNameCanNotBeDuplicatedWhenInserted(request.Name);
        var exampleEntity = request.Adapt<ExampleEntity>();
        ExampleEntity createdExampleEntity = await _unitOfWorkAsync.ExampleEntityRepository.AddAsync(exampleEntity);
        await _unitOfWorkAsync.SaveAsync();
        CreatedExampleEntityResponse createdBrandResponse = createdExampleEntity.Adapt<CreatedExampleEntityResponse>();

        return new SuccessDataResult<CreatedExampleEntityResponse>(createdBrandResponse);
    }
}