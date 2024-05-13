using AutoMapper;
using Cargodi.Business.DTOs.Staff.Operator;
using Cargodi.Business.Interfaces.Staff;
using Cargodi.DataAccess.Entities.Staff;
using Cargodi.DataAccess.Interfaces.Staff;

namespace Cargodi.Business.Services.Staff;

public class OperatorService : IOperatorService
{
	private readonly IOperatorRepository _operatorRepository;
	private readonly IMapper _mapper;

	public OperatorService(IOperatorRepository operatorRepository, IMapper mapper)
	{
		_operatorRepository = operatorRepository;
		_mapper = mapper;
	}

	public async Task<GetOperatorDto> CreateAsync(UpdateOperatorDto updateOperatorDto, long userId, CancellationToken cancellationToken)
	{
		var operatorEntity = _mapper.Map<Operator>(updateOperatorDto);

		operatorEntity.EmployDate = DateTime.UtcNow;
		operatorEntity.UserId = userId;
		operatorEntity.FireDate = null;

		var operatorResult = await _operatorRepository.CreateAsync(operatorEntity, cancellationToken);

		var @operator = await _operatorRepository.GetWithUserByIdAsync(operatorResult.Id, cancellationToken);

		return _mapper.Map<GetOperatorDto>(@operator);
	}

	public Task FireAsync(int id)
	{
		throw new NotImplementedException();
	}

	public Task<GetOperatorDto> GetByIdAsync(int id)
	{
		throw new NotImplementedException();
	}

}