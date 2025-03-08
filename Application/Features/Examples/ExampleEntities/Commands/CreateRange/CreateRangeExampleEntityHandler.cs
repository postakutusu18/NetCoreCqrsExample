using Application.Features.Examples.ExampleEntities.Commands.Create;

namespace Application.Features.Examples.ExampleEntities.Commands.CreateRange;

public class CreateRangeExampleEntityHandler : IRequestHandler<CreateRangeExampleEntityCommand, IDataResult<bool>>
{
    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
    private readonly ExampleEntityRules _exampleEntityRules;

    public CreateRangeExampleEntityHandler(IUnitOfWorkAsync unitOfWorkAsync, ExampleEntityRules exampleEntityRules)
    {
        _unitOfWorkAsync = unitOfWorkAsync;
        _exampleEntityRules = exampleEntityRules;
    }

    public async Task<IDataResult<bool>> Handle(CreateRangeExampleEntityCommand request, CancellationToken cancellationToken)
    {
        var examples = request.Names.Select(name => new ExampleEntity { Name = name }).ToList();
        var exampleEntity = request.Names.Adapt<ICollection<ExampleEntity>>();
        var createdExampleEntity =  _unitOfWorkAsync.ExampleEntityRepository.AddRangeAsync(exampleEntity);
        await _unitOfWorkAsync.SaveAsync();
        CreatedExampleEntityResponse createdBrandResponse = createdExampleEntity.Adapt<CreatedExampleEntityResponse>();

        return new SuccessDataResult<bool>(true);
    }
}
